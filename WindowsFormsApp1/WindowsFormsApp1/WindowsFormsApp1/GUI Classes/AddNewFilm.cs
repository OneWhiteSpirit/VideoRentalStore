using System;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlServerCe;
using System.Linq;

namespace VideoRentalStore
{
    public partial class AddNewFilm : Form
    {
        private Form parentForm;
        static string connectionString = "Data Source=" + Environment.CurrentDirectory + "\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        string imageLocation = "", imageLocation1 = "", imageLocation2 = "", imageLocation3 = "";

        SqlCeCommand cmd = new SqlCeCommand();
        int addFlag = -1;

        public AddNewFilm(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;

            //Запрет на изменение в ComboBox
            ProducingCountryComboBox.KeyPress += (sender, e) => e.Handled = true;
            LanguagesForVoiceComboBox.KeyPress += (sender, e) => e.Handled = true;
            FilmForAdultsComboBox.KeyPress += (sender, e) => e.Handled = true;
            TheTypeOfMediaComboBox.KeyPress += (sender, e) => e.Handled = true;

            CategoryComboBox();
            FillComboBoxOfPreviousPartsOfFilm();

            //Поиск по первому слову в ComboBox
            CategoryOfFilmComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CategoryOfFilmComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;

            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void AddNewFilm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
        }

        private void CategoryComboBox()
        {
            string queryString =
            "SELECT CategoryName FROM FilmCategoryName";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeDataReader reader = cmd.ExecuteReader();
                string CategoryOfFilm = "";

                while (reader.Read())
                {
                    CategoryOfFilm = (reader[0]).ToString();
                    CategoryOfFilmComboBox.Items.Add(CategoryOfFilm);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void InsertToFilm()
        {
            string query = "INSERT INTO Film (Название, Жанр, Актёры, Режиссёр, Страна, Год, " +
                "[Целевая аудитория 18+], [Кол-во копий], [Тип носителя], Описание, Продолжительность, Рейтинг, [Язык озвучки]) VALUES ('" + NameOfTheFilmTextBox.Text + "','" + CategoryOfFilmComboBox.Text +
                "','" + ActorsTextBox.Text + "','" + DirectorTextBox.Text +
                "','" + ProducingCountryComboBox.Text + "','" + int.Parse(YearOfIssueTextBox.Text) + "','" + FilmForAdultsComboBox.Text +
                "','" + int.Parse(NumberOfCopiesTextBox.Text) + "','" + TheTypeOfMediaComboBox.Text + "','" + SummaryTextBox.Text +
                "','" + int.Parse(LengthInMinutes.Text) + "','" + Rating.Text + "','" + LanguagesForVoiceComboBox.Text + "')";

            cmd.CommandText = query;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                addFlag = cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (GetLastId() == -1)
            {
                return;
            }
            else
            {
                for (int i = 1; i <= Convert.ToInt16(NumberOfCopiesTextBox.Text); i++)
                    InsertToBusyFilmCopy(GetLastId(), i);
            }
            
            //Проходим по всему ComboBox и достаём FilmId, после этого заполняем таблицу предыдущих частей
            for(int i = 0; i < listBox.Items.Count; i++)
            {
                string []subStr = listBox.Items[i].ToString().Split('=');
                int id = Convert.ToInt16(subStr[1]);
                FillTablePreviousPartsOfFilm(id);
                comboBox1.Items.Clear();
            }
            

            CategoryOfFilmComboBox.SelectedIndex = -1;
            CategoryOfFilmComboBox.Text = "Выберите или введите новый";
            NameOfTheFilmTextBox.Clear();
            ActorsTextBox.Clear();
            DirectorTextBox.Clear();
            ProducingCountryComboBox.SelectedIndex = -1;
            ProducingCountryComboBox.Text = "Выберите";
            YearOfIssueTextBox.Clear();
            FilmForAdultsComboBox.SelectedIndex = -1;
            FilmForAdultsComboBox.Text = "Выберите";
            NumberOfCopiesTextBox.Clear();
            TheTypeOfMediaComboBox.SelectedIndex = -1;
            TheTypeOfMediaComboBox.Text = "Выберите";
            SummaryTextBox.Clear();
            LengthInMinutes.Clear();
            Rating.Clear();
            LanguagesForVoiceComboBox.SelectedIndex = -1;
            LanguagesForVoiceComboBox.Text = "Выберите";
            listBox.Items.Clear();
            pictureBox.Image = null;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
        }

        private int GetLastId()
        {
            string queryString =
            "SELECT Id FROM Film";
            cmd.CommandText = queryString;
            cmd.Connection = connection;

            int colID = -1;

            try
            {
                connection.Open();
                SqlCeDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    colID = Convert.ToInt16(reader[0]);
                }
                reader.Close();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return colID;
        }

        private void InsertImagesToFilm()
        {
            byte[] image = null;
            byte[] image1 = null;
            byte[] image2 = null;
            byte[] image3 = null;

            FileStream fileStream = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            image = binaryReader.ReadBytes((int)fileStream.Length);

            FileStream fileStream1 = new FileStream(imageLocation1, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader1 = new BinaryReader(fileStream1);
            image1 = binaryReader1.ReadBytes((int)fileStream1.Length);

            FileStream fileStream2 = new FileStream(imageLocation2, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader2 = new BinaryReader(fileStream2);
            image2 = binaryReader2.ReadBytes((int)fileStream2.Length);

            FileStream fileStream3 = new FileStream(imageLocation3, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader3 = new BinaryReader(fileStream3);
            image3 = binaryReader3.ReadBytes((int)fileStream3.Length);


            string query = "UPDATE Film SET Photo1=@image, Photo2=@image1, Photo3=@image2, Photo4=@image3 WHERE Id in (SELECT MAX(Id) FROM Film)";
            cmd = new SqlCeCommand(query, connection);

            cmd.Parameters.Add(new SqlCeParameter("@image", image));
            cmd.Parameters.Add(new SqlCeParameter("@image1", image1));
            cmd.Parameters.Add(new SqlCeParameter("@image2", image2));
            cmd.Parameters.Add(new SqlCeParameter("@image3", image3));

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

        private void InsertToBusyFilmCopy(int FilmId, int CopyId)
        {
            string query = "INSERT INTO BusyFilmCopy (FilmId, CopyId) VALUES ('" + FilmId + "','" + CopyId + "');";

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

        #region Button click Events

        private void BackButtonAddNewClient_Click(object sender, EventArgs e)
        {
            Startpage startpage = this.Owner as Startpage;
            startpage.Visible = true;
            Close();
        }

        private void SavebtnAddNewFilm_Click(object sender, EventArgs e)
        {
            int flag1 = 0, flag2 = 0, flag3 = 0;

            if (imageLocation == imageLocation1 || imageLocation == imageLocation2
                || imageLocation == imageLocation3 || imageLocation1 == imageLocation2
                || imageLocation1 == imageLocation3 || imageLocation3 == imageLocation2)
            {
                MessageBox.Show("Не должно быть одинаковых картинок!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag3++;
            }
            else
                flag3 = 0;


            if (pictureBox.Image == null || pictureBox1.Image == null || pictureBox2.Image == null || pictureBox3.Image == null)
            {
                MessageBox.Show("Добавьте все картинки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag1++;
            }
            else
                flag1 = 0;


            if (Rating.TextLength == 0 || NameOfTheFilmTextBox.TextLength == 0 ||
                ActorsTextBox.TextLength == 0 || DirectorTextBox.TextLength == 0 ||
                YearOfIssueTextBox.TextLength == 0 || NumberOfCopiesTextBox.TextLength == 0 ||
                SummaryTextBox.TextLength == 0 || LengthInMinutes.TextLength == 0 ||
                FilmForAdultsComboBox.SelectedIndex == -1 || CategoryOfFilmComboBox.SelectedIndex == -1 ||
                LanguagesForVoiceComboBox.SelectedIndex == -1 ||
                TheTypeOfMediaComboBox.SelectedIndex == -1 || ProducingCountryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag2++;
            }
            else
                flag2 = 0;


            if (flag1 == 0 && flag2 == 0 && flag3 == 0)
            {
                InsertToFilm();
                InsertImagesToFilm();
                FillComboBoxOfPreviousPartsOfFilm();
                MessageBox.Show("Данные добавлены в базу!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion

        #region Event for TextBox and others fields

        private void LengthInMinutes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.Handled = true;

            if (Char.IsControl(e.KeyChar))
                return;

            if (Char.IsDigit(e.KeyChar))
            {
                if (LengthInMinutes.Text.Length == 3)
                    e.Handled = true;
                return;
            }

            e.Handled = true;
        }

        private void NameOfTheFilmTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                return;
            if (e.KeyChar == '.')// ввод дефиса, двоеточия, двойных кавычек, точки, +
                return;
            if (e.KeyChar == ':')
                return;
            if (e.KeyChar == '-')
                return;
            if (e.KeyChar == '+')
                return;
            if (e.KeyChar == '"')
                return;
            if (e.KeyChar == 32) // ввод пробела
                return;
            if (Char.IsControl(e.KeyChar))
                return;
            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void ActorsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
                return;
            if (e.KeyChar == 32) // ввод пробела
                return;
            if (e.KeyChar == ',')
                return;
            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void DirectorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32) // ввод пробела
                return;
            if (Char.IsControl(e.KeyChar))
                return;
            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void YearOfIssueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                if (YearOfIssueTextBox.TextLength == 4)
                    e.Handled = true;
                return;
            }

            if (Char.IsControl(e.KeyChar))
                return;

            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;

            e.Handled = true;
        }

        private void YearOfIssueTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidYearOfIssue(YearOfIssueTextBox.Text, out errorMsg))
            {
                // Отменит событие и выберит текст, который будет исправлен пользователем.
                e.Cancel = true;
                YearOfIssueTextBox.Select(0, YearOfIssueTextBox.Text.Length);

                // ErrorProvider Установит ошибку с отображаемым текстом.
                this.ErrorProvider.SetError(YearOfIssueTextBox, errorMsg);
            }
        }

        private void YearOfIssueTextBox_Validated(object sender, EventArgs e)
        {
            // Если все условия выполнены, очистите ErrorProvider от ошибок.
            ErrorProvider.SetError(YearOfIssueTextBox, "");
        }

        private bool ValidYearOfIssue(string yearOfIssue, out string errorMessage)
        {
            short flag = 0;
            int year = 0;

            if (!int.TryParse(yearOfIssue, out year))
            {
                errorMessage = "";
                flag++;
                return true;
            }

            if (year >= 1940 && year <= 2018)
            {
                errorMessage = "";
                return true;
            }

            errorMessage = "Год должен попадать в диапазон." +
               "('1940 - 2018 года')";
            return false;
        }

        private void NumberOfCopiesTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                if (NumberOfCopiesTextBox.Text.Length == 1)
                    e.Handled = true;
                return;
            }

            if (Char.IsControl(e.KeyChar))
                return;

            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;

            e.Handled = true;
        }

        private void SummaryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
                return;
            if (e.KeyChar == 32) // ввод пробела
                return;
            if (e.KeyChar == ',')
                return;
            if (e.KeyChar == '.')
                return;
            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void CategoryOfFilmComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
                return;
            if (e.KeyChar == 32) // ввод пробела
                return;
            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;

        }

        private void Rating_KeyPress(object sender, KeyPressEventArgs e)
        {
            string s = Rating.Text;
            int countOfDigit = s.Count(c => c >= '0' && c <= '9');
            int countOfChar = s.Count(c => c == ',');

            if (e.KeyChar == ',')
            {
                if (countOfChar >= 1)
                    e.Handled = true;
                return;
            }

            if (Char.IsDigit(e.KeyChar))
            {
                if (countOfDigit >= 2)
                    e.Handled = true;
                return;
            }

            if (Char.IsControl(e.KeyChar))
                return;

            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;

            e.Handled = true;
        }

        private void Rating_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidRating(Rating.Text, out errorMsg))
            {
                // Отменит событие и выберит текст, который будет исправлен пользователем.
                e.Cancel = true;
                Rating.Select(0, Rating.Text.Length);

                // ErrorProvider Установит ошибку с отображаемым текстом.
                this.ErrorProvider1.SetError(Rating, errorMsg);
            }
        }

        private void Rating_Validated(object sender, EventArgs e)
        {
            // Если все условия выполнены, очистите ErrorProvider от ошибок.
            ErrorProvider1.SetError(Rating, "");
        }

        private bool ValidRating(string Rating, out string errorMessage)
        {
            short flag = 0;
            double rating = 0;

            if (!double.TryParse(Rating, out rating))
            {
                errorMessage = "";
                flag++;
                return true;
            }

            if (Rating.IndexOf(',') == 0)
            {
                errorMessage = "Рейтинг должен попадать в диапазон ('0.0 - 9.9 баллов')";
                return false;
            }

            if (Rating.IndexOf(',') == Rating.Length - 1)
            {
                errorMessage = "Рейтинг должен попадать в диапазон ('0.0 - 9.9 баллов')";
                return false;
            }

            if (rating >= 0.0 && rating <= 9.9)
            {
                errorMessage = "";
                return true;
            }

            errorMessage = "Рейтинг должен попадать в диапазон." +
               "('0.0 - 9.9 баллов')";
            return false;
        }

        private void LengthInMinutes_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidLengthInMinutes(LengthInMinutes.Text, out errorMsg))
            {
                // Отменит событие и выберит текст, который будет исправлен пользователем.
                e.Cancel = true;
                LengthInMinutes.Select(0, LengthInMinutes.Text.Length);

                // ErrorProvider Установит ошибку с отображаемым текстом.
                this.ErrorProvider1.SetError(LengthInMinutes, errorMsg);
            }
        }

        private bool ValidLengthInMinutes(string LengthInMinutes, out string errorMessage)
        {
            short flag = 0;
            int length = 0;

            if (!int.TryParse(LengthInMinutes, out length))
            {
                errorMessage = "";
                flag++;
                return true;
            }

            if (length >= 1 && length <= 240)
            {
                errorMessage = "";
                return true;
            }

            errorMessage = "Продолжительность фильма должна попадать в диапазон ('1 - 240 мин')";
            return false;
        }

        private void LengthInMinutes_Validated(object sender, EventArgs e)
        {
            // Если все условия выполнены, очистите ErrorProvider от ошибок.
            ErrorProvider1.SetError(LengthInMinutes, "");
        }

        #endregion

        #region PictureBox Click Event

        private void pictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg files(*.jpg)|*.jpg|png files(*.png)|*.png|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageLocation = openFileDialog.FileName.ToString();
                pictureBox.ImageLocation = imageLocation;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg files(*.jpg)|*.jpg|png files(*.png)|*.png|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageLocation1 = openFileDialog.FileName.ToString();
                pictureBox1.ImageLocation = imageLocation1;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg files(*.jpg)|*.jpg|png files(*.png)|*.png|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageLocation2 = openFileDialog.FileName.ToString();
                pictureBox2.ImageLocation = imageLocation2;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpg files(*.jpg)|*.jpg|png files(*.png)|*.png|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageLocation3 = openFileDialog.FileName.ToString();
                pictureBox3.ImageLocation = imageLocation3;
            }
        }

        #endregion

        private void CategoryOfFilmComboBox_Click(object sender, EventArgs e)
        {
            CategoryOfFilmComboBox.Text = "";
        }

        private void FillComboBoxOfPreviousPartsOfFilm()
        {
            comboBox1.Items.Clear();
            string queryString =
            "SELECT Название + ' ' + Год, Id FROM Film";

            cmd.CommandText = queryString;
            cmd.Connection = connection;

            try
            {
                connection.Open();
                SqlCeResultSet reader = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
                string FilmName = "";
                int FilmId = 0;

                while (reader.Read())
                {
                    FilmName = (reader[0]).ToString();
                    FilmId = Convert.ToInt16(reader[1]);

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

                    comboBox1.Items.Add(FilmName + "  |  " + "FilmId=" + FilmId);
                }
                reader.Close();
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox.Items.Add(comboBox1.SelectedItem);
        }

        private void FillTablePreviousPartsOfFilm(int filmPartId)
        {
            string queryString = "insert into PreviousPartsOfFilm (FirstPartOfFilm, OtherPartOfFilm) values ('" + GetLastId() + "','" + filmPartId + "');";

            cmd.CommandText = queryString;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1)
                listBox.Items.RemoveAt(listBox.SelectedIndex);
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        private void RefreshView()
        {
            comboBox1.Text = "Введите первые буквы названия фильма продолжения";
            CategoryOfFilmComboBox.Text = "Выберите или введите новый";
        }

        private void AddNewFilm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (addFlag > 0)
            {
                Startpage SP = Owner as Startpage;
                SP.RefreshFilmLabel();
            }
        }

        private void AddNewCategoryName()
        {
            string queryString =
            "insert into FilmCategoryName (CategoryName) values ('" + CategoryOfFilmComboBox.Text + "')";

            cmd.CommandText = queryString;
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

        private void CategoryAdd_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(CategoryOfFilmComboBox.Text))
            {
                if (CategoryOfFilmComboBox.Items.Contains(CategoryOfFilmComboBox.Text))
                    MessageBox.Show("Уже имеется!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (CategoryOfFilmComboBox.Text != "Выберите или введите новый" && CategoryOfFilmComboBox.Text != "")
                {
                    CategoryOfFilmComboBox.Items.Add(CategoryOfFilmComboBox.Text);

                    AddNewCategoryName();

                    CategoryOfFilmComboBox.Text = "Выберите или введите новый";

                    MessageBox.Show("Новый жанр добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
