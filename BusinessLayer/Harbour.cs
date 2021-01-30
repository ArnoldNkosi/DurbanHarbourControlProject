using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BusinessLayer
{
    class Harbour
    {
        private Location dock;
        private Location perimeter;
        public Harbour()
        {

        }

        public Location Perimeter
        {
            get { return perimeter; }
            set { perimeter = value; }
        }

        public Location Dock
        {
            get { return dock; }
            set { dock = value; }
        }

    }

}
