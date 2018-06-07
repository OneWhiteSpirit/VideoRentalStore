using System;
using System.Windows.Forms;
using VideoRentalStore.GUI_Classes;
using System.Data.SqlServerCe;

namespace VideoRentalStore
{
    public partial class ViewClientCardForDistribution : Form
    {
        private Form parentForm;

        static string connectionString = "Data Source=" + Environment.CurrentDirectory + "\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        SqlCeCommand cmd = new SqlCeCommand();

        private int ClientId = FilmDistribution.ClientId;
        private int FilmId = FilmDistribution.FilmId;


        public ViewClientCardForDistribution(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;

            ShowData();
            RetrivingClient();
            FillOneRowInGrid();
            UpdateDistrInfoInBD();
        }

        public ViewClientCardForDistribution()
        {
        }

        private void ShowData()
        {
            ClientDataGridView.ColumnCount = 8;
            ClientDataGridView.Columns[0].Name = "Инв. № фильма";
            ClientDataGridView.Columns[1].Name = "Название";
            ClientDataGridView.Columns[2].Name = "Дата выдачи";
            ClientDataGridView.Columns[3].Name = "Дата возврата";
            ClientDataGridView.Columns[4].Name = "Кол-во дней опоздания";
            ClientDataGridView.Columns[5].Name = "Цена проката";
            ClientDataGridView.Columns[6].Name = "Сумма пени";
            ClientDataGridView.Columns[7].Name = "Итоговая сумма";

            for (int i = 0; i < 5; i++)
                ClientDataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void RetrivingClient()
        {
            string queryString =
            "SELECT Фамилия + ' ' + Имя + ' ' + Отчество, Пол, Возраст, [Дата регистрации] FROM ClientPersonalCard WHERE Id='" + ClientId + "'";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    this.ClientTextBox.Text = (reader[0]).ToString();
                    this.GenderTextBox.Text = (reader["Пол"]).ToString();
                    this.AgeTextBox.Text = (reader["Возраст"]).ToString();
                    this.RegistrDateTextBox.Text = (reader["Дата регистрации"]).ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillOneRowInGrid()
        {
            string queryString = "SELECT TOP 1 Film.Жанр, Film.Id, Film.Название, Film.[Тип носителя], BusyFilmCopy.CopyId, BusyFilmCopy.DistrDate FROM Film, BusyFilmCopy WHERE BusyFilmCopy.ClientId IS NULL AND BusyFilmCopy.FilmId=Film.Id AND BusyFilmCopy.FilmId='" + FilmId + "';";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeResultSet rs = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);

                string filmInvNum = "";

                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        int FilmId = Convert.ToInt32(rs["Id"]);
                        int CopyId = Convert.ToInt32(rs["CopyId"]);

                        if (FilmId >= 1 && FilmId < 10)
                        {
                            if (CopyId >= 1 && CopyId < 10)
                                filmInvNum = (rs["Жанр"]).ToString().Substring(0, 2).ToUpper() + "00" + FilmId + "0" + CopyId;
                            if (CopyId >= 10 && CopyId < 100)
                                filmInvNum = (rs["Жанр"]).ToString().Substring(0, 2).ToUpper() + "00" + FilmId + "" + CopyId;
                        }
                        else if (FilmId >= 10 && FilmId < 100)
                        {
                            if (CopyId >= 1 && CopyId < 10)
                                filmInvNum = (rs["Жанр"]).ToString().Substring(0, 2).ToUpper() + "0" + FilmId + "0" + CopyId;
                            if (CopyId >= 10 && CopyId < 100)
                                filmInvNum = (rs["Жанр"]).ToString().Substring(0, 2).ToUpper() + "0" + FilmId + "" + CopyId;
                        }
                        else if (FilmId >= 100 && FilmId < 1000)
                        {
                            if (CopyId >= 1 && CopyId < 10)
                                filmInvNum = (rs["Жанр"]).ToString().Substring(0, 2).ToUpper() + "" + FilmId + "0" + CopyId;
                            if (CopyId >= 10 && CopyId < 100)
                                filmInvNum = (rs["Жанр"]).ToString().Substring(0, 2).ToUpper() + "" + FilmId + "" + CopyId;
                        }

                        string filmPrice = new Film((rs["Тип носителя"]).ToString()).RentalPrice.ToString();
                        DateTime distributionDate = DateTime.Now;
                        //Convert.ToDateTime(rs["DistrDate"]);
                        //DateTime returnedDate = DateTime.Now;
                        //Convert.ToDateTime(rs["ReturnDate"]);
                        int daysCount = 0;
                        //Rental.GetExpireDays(distributionDate, returnedDate);
                        ClientDataGridView.Rows.Add(new string[] { filmInvNum, (rs["Название"]).ToString(), distributionDate.ToShortDateString(), "-"/*returnedDate.ToShortDateString()*/, daysCount.ToString(), filmPrice, "-"/*Rental.Penny.ToString()*/, filmPrice/*Rental.totalRentalAmount(filmPrice, distributionDate, returnedDate).ToString()*/});
                        this.DebtTextBox.Text = "Нет";
                    }
                }


                rs.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDistrInfoInBD()
        {
            string query = "UPDATE BusyFilmCopy SET ClientId='"+ ClientId +"', DistrDate='"+ DateTime.Today.ToShortDateString() +"' WHERE ClientId IS NULL AND FilmId='"+FilmId+ "' AND CopyId in (SELECT TOP 1 CopyId FROM BusyFilmCopy WHERE ClientId IS NULL AND FilmId='" + FilmId + "');";

            cmd.CommandText = query;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ViewClientCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
        }
    }
}
