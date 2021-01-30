using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BusinessLayer
{
   public class Location
    {
        private double longitude;
        private double latitude;
        private string searchValue;

        public Location(double longitude, double lat)
        {
            this.longitude = longitude;
            this.latitude = lat;
        }

        public Location()
        {

        }
        public string SearchValue
        {
            get { return searchValue; }
            set { searchValue = value; }
        }



        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }



        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

    }
}
