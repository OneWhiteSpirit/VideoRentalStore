using System;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using VideoRentalStore.Logic_Classes;

namespace VideoRentalStore.GUI_Classes
{
    public partial class ViewClientCardForReturn : Form
    {
        private Form parentForm;

        static string connectionString = "Data Source=" + Environment.CurrentDirectory + "\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        SqlCeCommand cmd = new SqlCeCommand();

        private int ClientId = FilmReturn.ClientId;

        public ViewClientCardForReturn(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            ShowData();
            RetrivingClient();
            FillOneRowInGrid();
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
            string queryString = "SELECT Film.Жанр, Film.Id, Film.Название, Film.[Тип носителя], BusyFilmCopy.CopyId, BusyFilmCopy.DistrDate FROM Film, BusyFilmCopy WHERE BusyFilmCopy.ClientId='"+ ClientId +"' AND BusyFilmCopy.FilmId=Film.Id;";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeResultSet rs = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);

                short flag = 0;
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
                        DateTime distributionDate = Convert.ToDateTime(rs["DistrDate"]);
                        DateTime returnedDate = DateTime.Now;
                        int daysCount = Rental.GetExpireDays(distributionDate, returnedDate);


                        if (daysCount != 0)
                        {
                            flag++;
                            ClientDataGridView.Rows.Add(new string[] { filmInvNum, (rs["Название"]).ToString(), distributionDate.ToShortDateString(), returnedDate.ToShortDateString(), daysCount.ToString(), filmPrice, Rental.CountOfPenny(distributionDate, returnedDate).ToString(), Rental.GetTotalRentalAmount(filmPrice, distributionDate, returnedDate).ToString() });
                        }
                        else
                        {
                            ClientDataGridView.Rows.Add(new string[] { filmInvNum, (rs["Название"]).ToString(), distributionDate.ToShortDateString(), returnedDate.ToShortDateString(), daysCount.ToString(), filmPrice, "-", Rental.GetTotalRentalAmount(filmPrice, distributionDate, returnedDate).ToString() });
                        }

                        if (flag != 0)
                        {
                            this.DebtTextBox.Text = "Да";
                        }
                        else
                        {
                            this.DebtTextBox.Text = "Нет";
                        }
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

        private void ReturnFilmCopy(int FilmId, int CopyId)
        {
            string query = "UPDATE BusyFilmCopy SET ClientId=NULL, DistrDate=NULL WHERE ClientId='" + ClientId + "' AND FilmId='" + FilmId + "' AND CopyId='" + CopyId + "';";

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

        private void AddNewRecordIntoClientHistory(int ClientItem, string InventNum,
            string FilmName, string DistrDate, string ReturnDate, int CountOfDay,
            string FilmPrice, string Penny, string TotalAmount)
        {
            string query = "insert into ClientHistory (ClientId, InventNum, FilmName, DistrDate, " +
                "ReturnDate, CountOfDay, FilmPrice, Penny, TotalAmount) values ('" +
                ClientId + "','"+ InventNum + "','" + FilmName + "','" + DistrDate +
                "','" + ReturnDate + "','" + CountOfDay + "','" + FilmPrice + "','" +
                Penny + "','" + TotalAmount + "');";

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

        private void ViewClientCardForReturn_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
        }

        private void ClientDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int FilmId = int.Parse(this.ClientDataGridView.CurrentRow.Cells[0].Value.ToString().Substring(2, 3));
            int CopyId = int.Parse(this.ClientDataGridView.CurrentRow.Cells[0].Value.ToString().Substring(5, 2));

            AddNewRecordIntoClientHistory(ClientId,
                this.ClientDataGridView.Rows[e.RowIndex].Cells["Инв. № фильма"].Value.ToString(),
                this.ClientDataGridView.Rows[e.RowIndex].Cells["Название"].Value.ToString(),
                this.ClientDataGridView.Rows[e.RowIndex].Cells["Дата выдачи"].Value.ToString(),
                this.ClientDataGridView.Rows[e.RowIndex].Cells["Дата возврата"].Value.ToString(),
                Convert.ToInt16(this.ClientDataGridView.Rows[e.RowIndex].Cells["Кол-во дней опоздания"].Value.ToString()),
                this.ClientDataGridView.Rows[e.RowIndex].Cells["Цена проката"].Value.ToString(),
                this.ClientDataGridView.Rows[e.RowIndex].Cells["Сумма пени"].Value.ToString(),
                this.ClientDataGridView.Rows[e.RowIndex].Cells["Итоговая сумма"].Value.ToString());



            ReturnFilmCopy(FilmId, CopyId);
            ClientDataGridView.Rows.Clear();
            FillOneRowInGrid();
            
            
            MessageBox.Show("Фильм был возвращён! База обновлена.");
        }
    }
}
