using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.BusinessLayer;

namespace WindowsFormsApp1
{
    interface IHarbourControl
    {
        void CalculateTravelTime(Boat boat);
        void UpdatePortTraffic(Boat boat);
        bool RequestPermissionToTravel(List<Boat> listofBoats);
        void SavingBoatArrival(Boat boat);
        Boat ShipRandomlyArrives();
        double CheckWindSpeed();
    }
}
