using System;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using VideoRentalStore.GUI_Classes;

namespace VideoRentalStore.GUI_Classes
{
    public partial class FilmDistribution : Form
    {
        private Form parentForm;

        static public int ClientId = -1;
        static public int FilmId = -1;

        static string connectionString = "Data Source=" + Environment.CurrentDirectory + "\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        SqlCeCommand cmd = new SqlCeCommand();

        public FilmDistribution(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;

            FormClientDBtoComboBox();
            FromFilmDBtoComboBox();

            //Поиск по первому слову в ComboBox
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;

            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void FilmDistribution_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
        }

        private void FormClientDBtoComboBox()
        {
            string queryString = "SELECT Фамилия + ' ' +  Имя + ' ' + Отчество FROM ClientPersonalCard";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeDataReader reader = cmd.ExecuteReader();

                string ClientFullName = "";

                while (reader.Read())
                {
                    ClientFullName = (reader[0]).ToString();
                    comboBox1.Items.Add(ClientFullName);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FromFilmDBtoComboBox()
        {
            string queryString =
            "SELECT Название + ' ' + Год FROM Film";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeDataReader reader = cmd.ExecuteReader();
                string FilmName = "";

                while (reader.Read())
                {
                    FilmName = (reader[0]).ToString();

                    String[] substrings = FilmName.Split(' ');
                    FilmName = "";
                    for (int i = 0; i < substrings.Length; i++)
                    {
                        if (i == substrings.Length - 1)
                        {
                            FilmName += "(" + substrings[i] + ")";
                            break;
                        }

                        FilmName += substrings[i] + " ";
                    }

                    comboBox2.Items.Add(FilmName);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckTheAvailabilityOfTheMovie(int id)
        {
            string queryString =
            "SELECT count(*) FROM BusyFilmCopy WHERE ClientId IS NULL AND FilmId='" + id + "'";
            cmd.CommandText = queryString;
            cmd.Connection = connection;

            int i = 0;

            try
            {
                connection.Open();
                SqlCeResultSet rs = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);

                while (rs.Read())
                {
                    i = Convert.ToInt16(rs[0]);
                }

                rs.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (i == 0)
            {
                MessageBox.Show("Извините, копии данного фильма закончились. Выберите другой фильм.");
                NextButton.Enabled = false;
                return false;
            }
            else
            {
                NextButton.Enabled = true;
                return true;
            }
        }

        private void CountTheNumberOfClientFilms()
        {
            string queryString =
            "SELECT FilmId FROM BusyFilmCopy WHERE ClientId='" + ClientId + "';";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            int i = 0;

            try
            {
                connection.Open();
                SqlCeResultSet rs = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);

                if (rs.HasRows)
                {
                    while (rs.Read())
                        i++;
                }
                else
                    i = 0;
                rs.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            if (i >= 0 && i < 5)
            {
                comboBox2.Enabled = true;
            }
            else
            {
                NextButton.Enabled = false;
                comboBox2.Enabled = false;
                MessageBox.Show("Извините, кол-во фильмов на руках превышено. Верните хотя бы один фильм.");
            }
        }

        private void CheckAge(int FilmId, int ClientId)
        {
            string queryString =
            "SELECT Film.[Целевая аудитория 18+], ClientPersonalCard.Возраст FROM Film, ClientPersonalCard WHERE Film.Id='" + FilmId + "' AND ClientPersonalCard.Id='" + ClientId + "'";
            cmd.CommandText = queryString;
            cmd.Connection = connection;

            string isFilmFor18 = "";
            int clientAge = 0;

            try
            {
                connection.Open();
                SqlCeResultSet rs = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);

                while (rs.Read())
                {
                    isFilmFor18 = rs[0].ToString();
                    clientAge = Convert.ToInt16(rs[1]);
                }

                rs.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (isFilmFor18 == "Нет")
            {
                NextButton.Enabled = true;
            }
            else if (isFilmFor18 == "Да" && clientAge < 18)
            {
                NextButton.Enabled = false;
                MessageBox.Show("Извините, но ваш возраст не соответствует возрастному ограничению фильма.");
            }
            else if (isFilmFor18 == "Да" && clientAge >= 18)
            {
                NextButton.Enabled = true;
            }
        }

        private bool DontDoubleCopy(int FilmId, int ClientId)
        {
            string queryString =
            "SELECT FilmId, ClientId FROM BusyFilmCopy WHERE FilmId='" + FilmId + "' AND ClientId='" + ClientId + "'";
            cmd.CommandText = queryString;
            cmd.Connection = connection;

            int count = 0;

            try
            {
                connection.Open();
                SqlCeResultSet rs = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);

                if (rs.HasRows)
                {
                    count++;
                }

                rs.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
            if (count != 0)
            {
                NextButton.Enabled = false;
                MessageBox.Show("Извините, у вас уже есть копия данного фильма.");
                return false;
            }
            else
            {
                NextButton.Enabled = true;
                return true;
            }
        
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClientId = comboBox1.SelectedIndex + 1;

            CountTheNumberOfClientFilms();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilmId = comboBox2.SelectedIndex + 1;

            if (CheckTheAvailabilityOfTheMovie(FilmId) == true)
                if (DontDoubleCopy(FilmId, ClientId) == true)
                    CheckAge(FilmId, ClientId);
        }

        private void RefreshView()
        {
            comboBox1.Text = "Выберите или введите фамилию клиента";
            comboBox2.Text = "Выберите или введите начало названия фильма";

            comboBox2.Enabled = false;
            NextButton.Enabled = false;
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "";
        }

        private void BackButtonAddNewClient_Click(object sender, EventArgs e)
        {
            Startpage startpage = this.Owner as Startpage;
            startpage.Visible = true;
            Close();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            ViewClientCardForDistribution viewCard = new ViewClientCardForDistribution(this);
            viewCard.Owner = this;
            this.Visible = false;
            viewCard.Show();

            RefreshView();

            MessageBox.Show("Выбранный фильм был выдан. Информация в базе была обновлена.");
        }
    }
}
