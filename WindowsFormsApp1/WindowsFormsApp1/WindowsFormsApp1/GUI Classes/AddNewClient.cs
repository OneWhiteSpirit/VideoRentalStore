using System;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using VideoRentalStore.Logic_Classes;

namespace VideoRentalStore
{
    public partial class AddNewClient : Form
    {
        private Form parentForm;

        static string connectionString = "Data Source="+Environment.CurrentDirectory+"\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        SqlCeCommand cmd = new SqlCeCommand();

        int addFlag = -1;

        public AddNewClient(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;

            //Запрет на ввод в ComboBox
            genderComboBox.KeyPress += (sender, e) => e.Handled = true;

            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.MaxDate = DateTime.Today;
        }

        private void InsertAndUpdateDB()
        {
            string query = "INSERT INTO ClientPersonalCard (Фамилия, Имя, Отчество, Пол, Возраст, [Дата регистрации]) VALUES ('" + surnameTextBox.Text + "','" + nameTextBox.Text + "','" + patronymicTextBox.Text + "','" + genderComboBox.Text + "','" + int.Parse(ageTextBox.Text) + "','" + dateTimePicker1.Text + "')";

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

            nameTextBox.Clear();
            surnameTextBox.Clear();
            patronymicTextBox.Clear();
            genderComboBox.SelectedIndex = -1;
            ageTextBox.Clear();
            dateTimePicker1.Value = DateTime.Now.Date;
        }

        private void AddNewClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
        }

        #region Click buttons Events

        private void btnBack_Click(object sender, EventArgs e)
        {
            Startpage startpage = this.Owner as Startpage;
            startpage.Visible = true;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (nameTextBox.TextLength == 0 || surnameTextBox.TextLength == 0 ||
            patronymicTextBox.TextLength == 0 || ageTextBox.TextLength == 0 ||
            genderComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                InsertAndUpdateDB();
                MessageBox.Show("Информация сохранена", "Сохранить", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Event for TextBox fields

        private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
                return;
            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void surnameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
                return;
            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void patronymicTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar))
                return;
            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void ageTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                if (ageTextBox.Text.Length == 3)
                    e.Handled = true;
                return;
            }

            if (Char.IsControl(e.KeyChar))
                return;

            if (!Char.IsLetter(e.KeyChar))
                e.Handled = true;

            e.Handled = true;
        }

        private void ageTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidAge(ageTextBox.Text, out errorMsg))
            {
                // Отменит событие и выберит текст, который будет исправлен пользователем.
                e.Cancel = true;
                ageTextBox.Select(0, ageTextBox.Text.Length);

                // ErrorProvider Установит ошибку с отображаемым текстом.
                this.ErrorProvider.SetError(ageTextBox, errorMsg);
            }
        }

        private void ageTextBox_Validated(object sender, System.EventArgs e)
        {
            // Если все условия выполнены, очистите ErrorProvider от ошибок.
            ErrorProvider.SetError(ageTextBox, "");
        }

        private bool ValidAge(string Age, out string errorMessage)
        {
            short flag = 0;
            int age = 0;

            if (!int.TryParse(Age, out age))
            {
                errorMessage = "";
                flag++;
                return true;
            }

            if (age >= 6 && age <= 100)
            {
                errorMessage = "";
                return true;
            }

            errorMessage = "Возраст должен попадать в диапазон." +
               "('6 - 100 лет')";
            return false;
        }

        #endregion

        private void AddNewClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (addFlag > 0)
            {
                Startpage SP = Owner as Startpage;
                SP.RefreshClientLabel();
            }
        }

        private void genderComboBox_Click(object sender, EventArgs e)
        {
            genderComboBox.Text = "";
        }
    }
}
