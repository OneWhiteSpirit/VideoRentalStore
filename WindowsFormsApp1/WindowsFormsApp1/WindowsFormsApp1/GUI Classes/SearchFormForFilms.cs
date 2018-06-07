using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Data.SqlServerCe;
using System.Collections.Generic;

namespace VideoRentalStore.GUI_Classes
{
    public partial class SearchFormForFilms : Form
    {
        private Form parentForm;

        static string connectionString = "Data Source=" + Environment.CurrentDirectory + "\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        SqlCeCommand cmd = new SqlCeCommand();
        DataTable dataTable = new DataTable();

        private List<string> previousPartsOfFilmBuffer = new List<string>();

        public SearchFormForFilms(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;

            ShowData();
        }

        private void ShowData()
        {
            cmd.CommandText = "SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film";
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(cmd);
                adapter.Fill(dataTable);
                SearchDataGridView.DataSource = dataTable;

                //Выравнивание по загаловкам колонки datagridview
                for (int i = 0; i < 14; i++)
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
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE Id like '%" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Название")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE Название like '%" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Жанр")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE Жанр like '" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Актер")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE Актёры like '%" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Режиссёр")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE Режиссёр like '%" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Страна")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE Страна like '" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Год")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE Год like '" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Фильм для взрослых")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE [Целевая аудитория 18+] like '" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Количество копий")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE [Кол-во копий] like '" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Тип носителя")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE [Тип носителя] like '" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Время")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE Продолжительность like '" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Рейтинг")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE Рейтинг like '" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
            else if (SearchComboBox.Text == "Язык озвучки")
                using (SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT Id, Название, Жанр, Актёры, Режиссёр, Страна, Год, [Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки] FROM Film WHERE [Язык озвучки] like '" + SearchTextBox.Text + "%'", connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    SearchDataGridView.DataSource = dataTable;
                }
        }

        private void SearchFormForFilms_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
        }

        private void FillDatagridviewPreviousPartsOfFilm(ViewFilm viewFilm, int clickFilmId)
        {
            //Очищение дублирующихся строк
            while (viewFilm.dataGridView1.Rows.Count != 0)
            {
                viewFilm.dataGridView1.Rows.Remove(viewFilm.dataGridView1.Rows[viewFilm.dataGridView1.Rows.Count - 1]);
            }

            string queryString = "select PreviousPartsOfFilm.FirstPartOfFilm, PreviousPartsOfFilm.OtherPartOfFilm, Film.Название, Film.Год from PreviousPartsOfFilm, Film WHERE PreviousPartsOfFilm.FirstPartOfFilm='" + clickFilmId + "' AND PreviousPartsOfFilm.OtherPartOfFilm=Film.Id;";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeDataReader reader = cmd.ExecuteReader();

                string PreviousPartsOfFilm = "";

                while (reader.Read())
                {
                    PreviousPartsOfFilm = (reader["Название"]).ToString() + " (" + (reader["Год"]).ToString() + ")   |  " + "FilmId=" + reader["OtherPartOfFilm"].ToString();

                    viewFilm.dataGridView1.Rows.Add(PreviousPartsOfFilm);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewFilm viewFilm = new ViewFilm(this);

            viewFilm.dataGridView1.ColumnCount = 1;
            //Выравнивание по загаловкам колонки datagridview
            viewFilm.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            int rowID = 0;

            try
            {
                rowID = Convert.ToInt16(this.SearchDataGridView.CurrentRow.Cells[0].Value.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Нажмите на строку, где содержется информация о фильме.", ex.Message);
            }

            string query = "select * FROM Film WHERE Id like '" + rowID + "%'";

            byte[] image = null;
            byte[] image1 = null;
            byte[] image2 = null;
            byte[] image3 = null;

            FillDatagridviewPreviousPartsOfFilm(viewFilm, rowID);

            try
            {
                connection.Open();
                SqlCeCommand cmd = new SqlCeCommand(query, connection);

                SqlCeDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    viewFilm.NameOfTheFilmTextBox.Text = reader["Название"].ToString();
                    viewFilm.CategoryOfFilmTextBox.Text = reader["Жанр"].ToString();
                    viewFilm.ActorsTextBox.Text = reader["Актёры"].ToString();
                    viewFilm.DirectorTextBox.Text = reader["Режиссёр"].ToString();
                    viewFilm.ProducingCountryTextBox.Text = reader["Страна"].ToString();
                    viewFilm.YearOfIssueTextBox.Text = reader["Год"].ToString();
                    viewFilm.FilmForAdultsTextBox.Text = reader["Целевая аудитория 18+"].ToString();
                    viewFilm.NumberOfCopiesTextBox.Text = reader["Кол-во копий"].ToString();
                    viewFilm.TheTypeOfMediaTextBox.Text = reader["Тип носителя"].ToString();
                    viewFilm.SummaryTextBox.Text = reader["Описание"].ToString();
                    viewFilm.LengthInMinutes.Text = reader["Продолжительность"].ToString();
                    viewFilm.Rating.Text = reader["Рейтинг"].ToString();
                    viewFilm.LangForVoiceTextBox.Text = reader["Язык озвучки"].ToString();

                    foreach (var item in previousPartsOfFilmBuffer)
                    {
                        viewFilm.dataGridView1.Rows.Add(item);
                    }

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
                        viewFilm.pictureBox.Image = null;
                    else
                    {
                        MemoryStream memoryStream = new MemoryStream(image);
                        viewFilm.pictureBox.Image = Image.FromStream(memoryStream);
                    }

                    if (image1 == null)
                        viewFilm.pictureBox1.Image = null;
                    else
                    {
                        MemoryStream memoryStream = new MemoryStream(image1);
                        viewFilm.pictureBox1.Image = Image.FromStream(memoryStream);
                    }

                    if (image2 == null)
                        viewFilm.pictureBox2.Image = null;
                    else
                    {
                        MemoryStream memoryStream = new MemoryStream(image2);
                        viewFilm.pictureBox2.Image = Image.FromStream(memoryStream);
                    }

                    if (image3 == null)
                        viewFilm.pictureBox3.Image = null;
                    else
                    {
                        MemoryStream memoryStream = new MemoryStream(image3);
                        viewFilm.pictureBox3.Image = Image.FromStream(memoryStream);
                    }
                }
                else
                {
                    viewFilm.CategoryOfFilmTextBox.Clear();
                    viewFilm.NameOfTheFilmTextBox.Clear();
                    viewFilm.ActorsTextBox.Clear();
                    viewFilm.DirectorTextBox.Clear();
                    viewFilm.ProducingCountryTextBox.Clear();
                    viewFilm.YearOfIssueTextBox.Clear();
                    viewFilm.FilmForAdultsTextBox.Clear();
                    viewFilm.NumberOfCopiesTextBox.Clear();
                    viewFilm.TheTypeOfMediaTextBox.Clear();
                    viewFilm.SummaryTextBox.Clear();
                    viewFilm.LengthInMinutes.Clear();
                    viewFilm.Rating.Clear();
                    viewFilm.LangForVoiceTextBox.Clear();
                    MessageBox.Show("Это изображение не доступно.");
                }


                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            previousPartsOfFilmBuffer.Clear();

            viewFilm.ShowDialog();
        }

        private void SearchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchTextBox.Enabled = true;
        }
    }
}
