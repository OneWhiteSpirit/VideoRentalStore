using System;
using System.Data.SqlServerCe;
using System.Windows.Forms;

namespace VideoRentalStore.Logic_Classes
{
    class Storage
    {
        static string connectionString = "Data Source=" + Environment.CurrentDirectory + "\\VRSDB.sdf";
        SqlCeConnection connection = new SqlCeConnection(@connectionString);
        SqlCeCommand cmd = new SqlCeCommand();

        public void InsertClientInformationIntoDB()
        {
            //Client client = new Client();

            //string query = "INSERT INTO ClientPersonalCard (Фамилия, Имя, Отчество, Пол, Возраст, [Дата регистрации]) VALUES ('"
            //    + client.Surname + "','" + client.Name + "','" + client.Patronymic + "','" +
            //    client.Gender + "','" + client.Age + "','" + client.RegistrationDate + "')";

            //cmd.CommandText = query;
            //cmd.Connection = connection;

            //try
            //{
            //    connection.Open();
            //    cmd.ExecuteNonQuery();
            //    connection.Close();
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
