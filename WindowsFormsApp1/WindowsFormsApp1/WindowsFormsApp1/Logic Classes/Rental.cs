using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalStore.Logic_Classes
{
    class Rental
    {
        private string NameOfRentalStore = "Bunny store";

        private DateTime RentalDate { get; set; }

        private const int NumberOfRentalDays = 7;

        static public double Penny = 1.5;

        private DateTime ReturnedDate { get; set; }

        static public double GetTotalRentalAmount(string filmPrice, DateTime rentalDate, DateTime returnedDate)
        {
            double price = Convert.ToDouble(filmPrice);
            int duration = returnedDate.Day - rentalDate.Day;

            if (duration <= NumberOfRentalDays)
                return price;
            else
                return price + (double)duration * Penny;
        }

        static public int GetExpireDays(DateTime rentalDate, DateTime returnedDate)
        {
            int daysCount = returnedDate.Day - rentalDate.Day;

            if (daysCount <= NumberOfRentalDays)
                return 0;
            else
                return daysCount;
        }

        static public double CountOfPenny(DateTime rentalDate, DateTime returnedDate)
        {
            int duration = returnedDate.Day - rentalDate.Day;

            double penny = Penny;

            penny *= (double)duration;

            return penny;
        }

        public Rental(string newNameOfRentalStore, DateTime newRentalDate,
            DateTime newReturnedDate)
        {
            this.NameOfRentalStore = newNameOfRentalStore;
            this.RentalDate = newRentalDate;
            this.ReturnedDate = newReturnedDate;
        }

        public Rental()
        {
        }
    }
}
