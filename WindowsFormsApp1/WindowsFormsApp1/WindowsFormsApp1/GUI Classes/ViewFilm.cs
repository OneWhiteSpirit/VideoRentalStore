using System;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;
using System.Drawing;

namespace VideoRentalStore.GUI_Classes
{
    public partial class ViewFilm : Form
    {
        private Form parentForm;

        static string connectionString = "Data Source=" + Environment.CurrentDirectory + "\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        SqlCeCommand cmd = new SqlCeCommand();

        public ViewFilm(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        public ViewFilm()
        {
        }

        private void BackButtonViewFilm_Click(object sender, EventArgs e)
        {
            Startpage startpage = this.Owner as Startpage;
            startpage.Visible = true;
            Close();
        }

        private void ViewFilm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
        }

        private void GetPreviousPartsOfFilm(int FilmId)
        {
            string query = "select * FROM Film WHERE Id like '" + FilmId + "%'";

            byte[] image = null;
            byte[] image1 = null;
            byte[] image2 = null;
            byte[] image3 = null;
            
            try
            {
                connection.Open();
                SqlCeCommand cmd = new SqlCeCommand(query, connection);

                SqlCeDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    NameOfTheFilmTextBox.Text = reader["Название"].ToString();
                    CategoryOfFilmTextBox.Text = reader["Жанр"].ToString();
                    ActorsTextBox.Text = reader["Актёры"].ToString();
                    DirectorTextBox.Text = reader["Режиссёр"].ToString();
                    ProducingCountryTextBox.Text = reader["Страна"].ToString();
                    YearOfIssueTextBox.Text = reader["Год"].ToString();
                    FilmForAdultsTextBox.Text = reader["Целевая аудитория 18+"].ToString();
                    NumberOfCopiesTextBox.Text = reader["Кол-во копий"].ToString();
                    TheTypeOfMediaTextBox.Text = reader["Тип носителя"].ToString();
                    SummaryTextBox.Text = reader["Описание"].ToString();
                    LengthInMinutes.Text = reader["Продолжительность"].ToString();
                    Rating.Text = reader["Рейтинг"].ToString();
                    LangForVoiceTextBox.Text = reader["Язык озвучки"].ToString();
                    
                    try
                    {
                        image = (byte[])reader["Photo1"];
                        image1 = (byte[])reader["Photo2"];
                        image2 = (byte[])reader["Photo3"];
                        image3 = (byte[])reader["Photo4"];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Отсутствует изображение.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    if (image == null)
                        pictureBox.Image = null;
                    else
                    {
                        MemoryStream memoryStream = new MemoryStream(image);
                        pictureBox.Image = Image.FromStream(memoryStream);
                    }

                    if (image1 == null)
                        pictureBox1.Image = null;
                    else
                    {
                        MemoryStream memoryStream = new MemoryStream(image1);
                        pictureBox1.Image = Image.FromStream(memoryStream);
                    }

                    if (image2 == null)
                        pictureBox2.Image = null;
                    else
                    {
                        MemoryStream memoryStream = new MemoryStream(image2);
                        pictureBox2.Image = Image.FromStream(memoryStream);
                    }

                    if (image3 == null)
                        pictureBox3.Image = null;
                    else
                    {
                        MemoryStream memoryStream = new MemoryStream(image3);
                        pictureBox3.Image = Image.FromStream(memoryStream);
                    }
                }
                else
                {
                    CategoryOfFilmTextBox.Clear();
                    NameOfTheFilmTextBox.Clear();
                    ActorsTextBox.Clear();
                    DirectorTextBox.Clear();
                    ProducingCountryTextBox.Clear();
                    YearOfIssueTextBox.Clear();
                    FilmForAdultsTextBox.Clear();
                    NumberOfCopiesTextBox.Clear();
                    TheTypeOfMediaTextBox.Clear();
                    SummaryTextBox.Clear();
                    LengthInMinutes.Clear();
                    Rating.Clear();
                    LangForVoiceTextBox.Clear();
                }


                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string[] subStr = { "" };

            //Извлечение id фильма из datagridview
            if (dataGridView1.SelectedCells.Count > 0)
            {
                subStr = dataGridView1.SelectedCells[0].Value.ToString().Split('=');

                int FilmId = Convert.ToInt16(subStr[1]);

                GetPreviousPartsOfFilm(FilmId);
            }
        }
    }
}
