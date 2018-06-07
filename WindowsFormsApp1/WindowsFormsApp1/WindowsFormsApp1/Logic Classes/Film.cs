using System.Collections.Generic;

namespace VideoRentalStore
{
    class Film 
    {
        private string InventoryNumberOfFilm;
        private string theTypeOfMedia;

        public string NameOfTheFilm { get; set; }

        public List<string> Actors;

        public string Director { get; set; }

        public string ProducingCountry { get; set; }

        public int YearOfIssue { get; set; }

        public string IsThisFilmForAdults { get; set; }

        public int NumberOfCopies { get; set; }

        public string TheTypeOfMedia
        {
            get
            {
                return theTypeOfMedia;
            }
            set
            {
                theTypeOfMedia = value;
            }
        }

        public string Summary { get; set; }

        public int LengthInMinutes { get; set; }

        public float Rating { get; set; }

        public string LanguagesForVoice { get; set; }

        public float RentalPrice { get; set; }


        public Film(string InventNum, string NameOfTheFilm, List<string> Actors, string Director,
            string ProducingCountry, int YearOfIssue, string IsThisFilmForAdults, int NumberOfCopies,
            string TheTypeOfMedia, string Summary, int LengthInMinutes, float Rating, 
            string LanguagesForVoice, float RentalPrice)
        {
            this.InventoryNumberOfFilm = InventNum;
            this.NameOfTheFilm = NameOfTheFilm;
            this.Actors = Actors;
            this.Director = Director;
            this.ProducingCountry = ProducingCountry;
            this.YearOfIssue = YearOfIssue;
            this.IsThisFilmForAdults = IsThisFilmForAdults;
            this.NumberOfCopies = NumberOfCopies;
            this.TheTypeOfMedia = TheTypeOfMedia;
            this.Summary = Summary;
            this.LengthInMinutes = LengthInMinutes;
            this.Rating = Rating;
            this.LanguagesForVoice = LanguagesForVoice;

            if (TheTypeOfMedia == "Кассета")
                this.RentalPrice = 10;
            else if (TheTypeOfMedia == "DVD")
                this.RentalPrice = 20;
            else if (TheTypeOfMedia == "Blu-ray")
                this.RentalPrice = 30;
        }

        public Film(string TheTypeOfMedia)
        {
            this.TheTypeOfMedia = TheTypeOfMedia;

            if (TheTypeOfMedia == "Кассета")
                this.RentalPrice = 10;
            else if (TheTypeOfMedia == "DVD")
                this.RentalPrice = 20;
            else if (TheTypeOfMedia == "Blu-ray")
                this.RentalPrice = 30;
        }
    }
}
