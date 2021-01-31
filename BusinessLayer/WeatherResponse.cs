using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BusinessLayer
{
    public class WeatherResponse
    {
        public Coord Coord { get; set; }
        public Weather[] Weather { get; set; }
        public string Base { get; set; }

        public Main Main { get; set; }
        public double Visability { get; set; }
        public Wind Wind { get; set; }
        public Rain Rain { get; set; }
        public Clouds Clouds { get; set; }
        public double dt { get; set; }

        public Sys Sys { get; set; }

        public int timezone { get; set; }
        public double id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }

    }
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }
    public class Weather
    {
        private int id;
        private string main;
        private string description;
        private string icon;

        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Main
        {
            get { return main; }
            set { main = value; }
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }

    public class Main
    {
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }

    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Rain
    {
        public double testing { get; set; }
    }
    public class Clouds
    {
        public int all { get; set; }
    }
    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public double sunrise { get; set; }
        public double sunset { get; set; }
    }
}
