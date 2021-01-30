using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BusinessLayer
{
    public class TravellingTime
    {
        private DateTime departureTime;
        private DateTime dateTime;

        public TravellingTime()
        {

        }
        public DateTime ArrivalTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        public DateTime DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }

    }
}
