using System;
using System.Windows.Forms;
using System.Data.SqlServerCe;
namespace VideoRentalStore.GUI_Classes
{
    public partial class FilmReturn : Form
    {
        private Form parentForm;

        static string connectionString = "Data Source=" + Environment.CurrentDirectory + "\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        SqlCeCommand cmd = new SqlCeCommand();

        static public int ClientId;

        public FilmReturn(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            RetrivingClients();

            //Поиск по первому слову в ComboBox
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Startpage startpage = this.Owner as Startpage;
            startpage.Visible = true;
            Close();
        }

        private void ReturnFilmButton_Click(object sender, EventArgs e)
        {
            ViewClientCardForReturn viewCard = new ViewClientCardForReturn(this);
            viewCard.Owner = this;
            this.Visible = false;
            viewCard.ShowDialog();

            RefreshView();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt16(comboBox1.SelectedIndex) + 1;
            ReturnFilmButton.Enabled = true;
        }

        private void RefreshView()
        {
            comboBox1.Text = "Выберите или введите фамилию клиента";

            ReturnFilmButton.Enabled = false;
        }

        private void RetrivingClients()
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

        private void ClientFilms_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
        }
    }
}
