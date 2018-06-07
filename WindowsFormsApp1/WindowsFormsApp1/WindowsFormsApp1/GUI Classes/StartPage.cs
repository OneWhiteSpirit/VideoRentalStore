using System;
using System.Windows.Forms;
using VideoRentalStore.GUI_Classes;
using System.Data.SqlServerCe;

namespace VideoRentalStore
{
    public partial class Startpage : Form
    {
        static string datapath = Environment.CurrentDirectory;
        static string connectionString = "Data Source="+datapath+"\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        SqlCeCommand cmd = new SqlCeCommand();

        public Startpage()
        {
            InitializeComponent();
            RefreshFilmLabel();
            RefreshClientLabel();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddNewClient newForm = new AddNewClient(this);
            newForm.Owner = this;
            this.Visible = false;
            newForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewFilm newAddNewFilm = new AddNewFilm(this);
            newAddNewFilm.Owner = this;
            this.Visible = false;
            newAddNewFilm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FilmDistribution newChooseClient = new FilmDistribution(this);
            newChooseClient.Owner = this;
            this.Visible = false;
            newChooseClient.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FilmReturn newFilmReturn = new FilmReturn(this);
            newFilmReturn.Owner = this;
            this.Visible = false;
            newFilmReturn.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SearchFormForClients newSearch = new SearchFormForClients(this);
            newSearch.Owner = this;
            this.Visible = false;
            newSearch.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchFormForFilms newSearch = new SearchFormForFilms(this);
            newSearch.Owner = this;
            this.Visible = false;
            newSearch.Show();
        }


        public void RefreshFilmLabel()
        {
            string queryString =
            "SELECT Id FROM Film";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            int FilmCount = 0;

            try
            {
                connection.Open();
                SqlCeDataReader reader = cmd.ExecuteReader();
                

                while (reader.Read())
                {
                    FilmCount++;
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            label1.Text = "Количетсво фильмов: " + FilmCount;
        }

        public void RefreshClientLabel()
        {
            string queryString =
            "SELECT Id FROM ClientPersonalCard";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            int ClientCount = 0;

            try
            {
                connection.Open();
                SqlCeDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    ClientCount++;
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            label2.Text = "Количество клиентов: " + ClientCount;
        }
    }
}
