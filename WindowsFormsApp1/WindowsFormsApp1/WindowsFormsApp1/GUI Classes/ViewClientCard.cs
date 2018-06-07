using System;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace VideoRentalStore.GUI_Classes
{
    public partial class ViewClientCard : Form
    {
        private Form parentForm;

        static string connectionString = "Data Source=" + Environment.CurrentDirectory + "\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        SqlCeCommand cmd = new SqlCeCommand();

        public ViewClientCard(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;

            ShowData();
            RetrivingClient();
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

        private void ViewClientCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Visible = true;
        }

        private void RetrivingClient()
        {
            string queryString =
            "SELECT Фамилия + ' ' + Имя + ' ' + Отчество, Пол, Возраст, [Дата регистрации] FROM ClientPersonalCard WHERE Id='" + SearchFormForClients.rowClientId + "'";

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

        private void ToExelBtn_Click(object sender, EventArgs e)
        {
            saveAsExcelDocument();
        }

        private void saveAsExcelDocument()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Сохраните как Excel документ";
            saveFileDialog.Filter = "Excel документ(.xlsx)|*.xlsx|Excel документ 97-2003(.xls)|*.xls";
            saveFileDialog.FileName = "Карта клиента (" + this.ClientTextBox.Text + ")";

            if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                Excel.Application excelApplication = new Excel.Application();
                excelApplication.Visible = true;
                excelApplication.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i <= 5; i++)
                    excelApplication.get_Range("B" + i, "C" + i).Merge();

                excelApplication.Cells[1, 1] = "Клиент: ";
                excelApplication.Cells[2, 1] = "Пол: ";
                excelApplication.Cells[3, 1] = "Возраст: ";
                excelApplication.Cells[4, 1] = "Дата регистрации: ";
                excelApplication.Cells[5, 1] = "Имеется ли задолжность: ";

                excelApplication.Cells[1, 2] = this.ClientTextBox.Text;
                excelApplication.Cells[2, 2] = this.GenderTextBox.Text;
                excelApplication.Cells[3, 2] = this.AgeTextBox.Text;
                excelApplication.Cells[4, 2] = this.RegistrDateTextBox.Text;
                excelApplication.Cells[5, 2] = this.DebtTextBox.Text;

                excelApplication.Columns[1].ColumnWidth = 25;
                excelApplication.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                for (int i = 1; i < ClientDataGridView.ColumnCount + 1; i++)
                {
                    excelApplication.Cells[7, i] = ClientDataGridView.Columns[i - 1].HeaderText;
                    excelApplication.Columns[i + 1].ColumnWidth = 22;
                }

                for (int i = 0; i < ClientDataGridView.RowCount; i++)
                {
                    for (int j = 0; j < ClientDataGridView.ColumnCount; j++)
                    {
                        if (ClientDataGridView.Rows[i].DefaultCellStyle.BackColor != Color.BlueViolet)
                        {
                            excelApplication.Cells[i + 8, j + 1] = ClientDataGridView.Rows[i].Cells[j].Value.ToString();                            
                        }
                        else
                        {
                            excelApplication.Cells[i + 8, j + 1] = ClientDataGridView.Rows[i].Cells[j].Value.ToString();
                            excelApplication.Cells[i + 8, j + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.BlueViolet);
                        }
                    }
                }

                excelApplication.ActiveWorkbook.SaveCopyAs(saveFileDialog.FileName.ToString());
                excelApplication.ActiveWorkbook.Saved = true;
                //excelApplication.Quit();
            }
        }
    }
}
