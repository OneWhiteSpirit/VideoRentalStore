using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlServerCe;
using VideoRentalStore.GUI_Classes;
using System.Drawing;

namespace VideoRentalStore
{
    public partial class SearchFormForClients : Form
    {
        private Form parentForm;

        static string connectionString = "Data Source=" + Environment.CurrentDirectory + "\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        SqlCeCommand cmd = new SqlCeCommand();
        DataTable dataT = new DataTable();

        public static int rowClientId = -1;

        public SearchFormForClients(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;

            ShowData();
        }

        private void ShowData()
        {
            cmd.CommandText = "select Id, Фамилия, Имя, Отчество, Пол, Возраст, [Дата регистрации] from ClientPersonalCard";
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(cmd);
                adapter.Fill(dataT);
                SearchDataGridView.DataSource = dataT;
                for (int i = 0; i < 7; i++)
                    SearchDataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Startpage startpage = this.Owner as Startpage;
            startpage.Visible = true;
            Close();
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SearchComboBox.Text == "Id")
            {
                SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Фамилия, Имя, Отчество, Пол, Возраст, [Дата регистрации] FROM ClientPersonalCard WHERE Id LIKE '" + SearchTextBox.Text + "%'", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                SearchDataGridView.DataSource = dataTable;
            }
            else if (SearchComboBox.Text == "Имя")
            {
                SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Фамилия, Имя, Отчество, Пол, Возраст, [Дата регистрации] FROM ClientPersonalCard WHERE Имя LIKE '" + SearchTextBox.Text + "%'", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                SearchDataGridView.DataSource = dataTable;
            }
            else if (SearchComboBox.Text == "Фамилия")
            {
                SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Фамилия, Имя, Отчество, Пол, Возраст, [Дата регистрации] FROM ClientPersonalCard WHERE Фамилия LIKE '" + SearchTextBox.Text + "%'", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                SearchDataGridView.DataSource = dataTable;
            }
            else if (SearchComboBox.Text == "Отчество")
            {
                SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Фамилия, Имя, Отчество, Пол, Возраст, [Дата регистрации] FROM ClientPersonalCard WHERE Отчество LIKE '" + SearchTextBox.Text + "%'", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                SearchDataGridView.DataSource = dataTable;
            }
            else if (SearchComboBox.Text == "Пол")
            {
                SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Фамилия, Имя, Отчество, Пол, Возраст, [Дата регистрации] FROM ClientPersonalCard WHERE Пол LIKE '" + SearchTextBox.Text + "%'", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                SearchDataGridView.DataSource = dataTable;
            }
            else if (SearchComboBox.Text == "Возраст")
            {
                SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Фамилия, Имя, Отчество, Пол, Возраст, [Дата регистрации] FROM ClientPersonalCard WHERE Возраст LIKE '" + SearchTextBox.Text + "%'", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                SearchDataGridView.DataSource = dataTable;
            }
            else if (SearchComboBox.Text == "Дата регистрации")
            {
                SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Фамилия, Имя, Отчество, Пол, Возраст, [Дата регистрации] FROM ClientPersonalCard WHERE [Дата регистрации] LIKE '%" + SearchTextBox.Text + "%'", connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                SearchDataGridView.DataSource = dataTable;
            }
        }

        private void SearchFormForClients_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
        }

        private void RetrivingInfoFromBusyFilmCopy(ViewClientCard clientCard, int clientId)
        {
            string queryString = "SELECT Film.Жанр, Film.Id, Film.Название, Film.[Тип носителя], BusyFilmCopy.CopyId, BusyFilmCopy.DistrDate FROM Film, BusyFilmCopy WHERE BusyFilmCopy.ClientId='" + clientId + "' AND BusyFilmCopy.FilmId=Film.Id;";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeResultSet rs = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);

                string filmInvNum = "";
                short flag = 0;

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
                        //DateTime returnedDate = DateTime.Now;
                        //Convert.ToDateTime(rs["ReturnDate"]);
                        int daysCount = Rental.GetExpireDays(distributionDate, DateTime.Now);

                        if (daysCount != 0)
                        {
                            flag++;
                            clientCard.ClientDataGridView.Rows.Add(new string[] { filmInvNum, (rs["Название"]).ToString(), distributionDate.ToShortDateString(), "-", daysCount.ToString(), filmPrice, Rental.CountOfPenny(distributionDate, DateTime.Now).ToString(), Rental.GetTotalRentalAmount(filmPrice, distributionDate, DateTime.Now).ToString() });
                        }
                        else
                        {
                            clientCard.ClientDataGridView.Rows.Add(new string[] { filmInvNum, (rs["Название"]).ToString(), distributionDate.ToShortDateString(), "-", daysCount.ToString(), filmPrice, "-", Rental.GetTotalRentalAmount(filmPrice, distributionDate, DateTime.Now).ToString() });
                        }

                        if (flag != 0)
                        {
                            clientCard.DebtTextBox.Text = "Да";
                        }
                        else
                        {
                            clientCard.DebtTextBox.Text = "Нет";
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

        private void RetrivingHistoryInformation(ViewClientCard clientCard, int ClientId)
        {
            string queryString = "SELECT InventNum, FilmName, DistrDate, ReturnDate, CountOfDay, FilmPrice, Penny, TotalAmount FROM ClientHistory WHERE ClientId='" + ClientId + "';";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeResultSet rs = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);

                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        string[] rowI = new string[] { (rs["InventNum"]).ToString(), (rs["FilmName"]).ToString(),
                        (rs["DistrDate"]).ToString(), (rs["ReturnDate"]).ToString(), (rs["CountOfDay"]).ToString(),
                        (rs["FilmPrice"]).ToString(), (rs["Penny"]).ToString(), (rs["TotalAmount"]).ToString() };

                        clientCard.ClientDataGridView.Rows.Add(rowI);

                        
                        for (int i = 0; i < clientCard.ClientDataGridView.RowCount; i++)
                        {
                            DataGridViewRow dataGridViewRow = clientCard.ClientDataGridView.Rows[i];
                            if (dataGridViewRow.IsNewRow) continue;

                            if (dataGridViewRow.Cells["Дата возврата"].Value.ToString() != "-")
                                dataGridViewRow.DefaultCellStyle.BackColor = Color.BlueViolet;
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

        private void SearchDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewClientCard clientCard = new ViewClientCard(this);

            rowClientId = Convert.ToInt16(this.SearchDataGridView.CurrentRow.Cells["Id"].Value);

            RetrivingInfoFromBusyFilmCopy(clientCard, rowClientId);

            RetrivingHistoryInformation(clientCard, rowClientId);

            clientCard.ClientTextBox.Text = this.SearchDataGridView.CurrentRow.Cells["Фамилия"].Value.ToString() + " " + this.SearchDataGridView.CurrentRow.Cells["Имя"].Value.ToString() + " " + this.SearchDataGridView.CurrentRow.Cells["Отчество"].Value.ToString();
            clientCard.GenderTextBox.Text = this.SearchDataGridView.CurrentRow.Cells["Пол"].Value.ToString();
            clientCard.AgeTextBox.Text = this.SearchDataGridView.CurrentRow.Cells["Возраст"].Value.ToString();
            clientCard.RegistrDateTextBox.Text = this.SearchDataGridView.CurrentRow.Cells["Дата регистрации"].Value.ToString();

            clientCard.Show();
        }

        private void SearchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchTextBox.Enabled = true;
        }
    }
}