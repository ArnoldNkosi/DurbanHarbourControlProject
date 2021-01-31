using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BusinessLayer
{
    public class Boat
    {
        private string name;
        private double speed;
        public bool enroute { get; set; }
        private Location location;

        public Boat(string name, double speed, bool enroute, Location location)
        {
            this.name = name;
            this.speed = speed;
            this.enroute = enroute;
            this.location = location;
        }
        public Boat()
        {

        }

        public Location Location
        {
            get { return location; }
            set { location = value; }
        }

        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
