using System;

namespace VideoRentalStore
{
    class Client
    {
        private string name;
        private string surname;
        private string patronymic;
        private string gender;
        private int age;
        private DateTime registrationDate;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Patronymic
        {
            get { return patronymic; }
            set { patronymic = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public DateTime RegistrationDate
        {
            get { return registrationDate; }
            set { registrationDate = value; }
        }

        public Client(string newName, string newSurname, string newPatronymic, string newGender,
            int newAge, DateTime newRegistrationDate)
        {
            this.name = newName;
            this.surname = newSurname;
            this.patronymic = newPatronymic;
            this.gender = newGender;
            this.age = newAge;
            this.registrationDate = newRegistrationDate;
        }
    }
}
