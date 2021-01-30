using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.BusinessLayer;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public enum TypeOfBoat
        {
            speedboat = 1,
            sailboat = 2,
            cargoShip = 3
        }

        bool shipInTheHarbour = false;
        double windSpeed = 0.00;
        public Form1()
        {
            InitializeComponent();
            Random random = new Random();
            int value = random.Next(0, 2);
            //Ship has arrived
            List<Boat> CurrentBoatsAtThePerimeter = new List<Boat>();
            Boat boat= new Boat();
            boat = ShipRandomlyArrives(value);
            CurrentBoatsAtThePerimeter.Add(boat);
        }


        public TravellingTime CalculateTravelTime(Boat boat)
        {
            TravellingTime travellingTime = new TravellingTime();
            double distanceFromPerimeterToDock = 10;
            double timeTaken = distanceFromPerimeterToDock / boat.Speed;
            travellingTime.DepartureTime  = DateTime.Now;
            travellingTime.ArrivalTime  = DateTime.Now.AddMinutes(timeTaken);
            if (DateTime.Now == travellingTime.ArrivalTime)
            {
                shipInTheHarbour = false;
            }
            return travellingTime;
        }

     
        public bool CheckHarbourClearance(List<Boat> listofBoats)
        {
            Harbour harbourArea = new Harbour();
            harbourArea.Dock = new Location(-29.867997 , 31.014409);
            harbourArea.Perimeter = new Location(-29.867993, 31.058299);

            List<Boat> boats = listofBoats;
            for (int i = 0; i < boats.Count; i++)
            {
                if (boats[i].Location == harbourArea.Dock)
                {
                    CalculateTravelTime(boats[i]);
                }
            }


            SavingBoatArrival();

            return shipInTheHarbour;
        }

        public void SavingBoatArrival()
        {

        }



        //A ship randomly arriving at the harbour.
        public Boat ShipRandomlyArrives(int value)
        {
            Boat boat = new Boat();
            boat.Location = new Location(-29.867997, 31.014409);
            TypeOfBoat boatType = new TypeOfBoat();
            value = (int)boatType;

            switch (value)
            {
                case 0:
                    {
                        boat.Speed = 30.0;
                        boat.Name = "speedboat";
                        return boat;
                    }

                case 1:
                    {
                        boat.Speed = 15.0;
                        boat.Name = "sailboat";
                        return boat;
                    }

                case 2:
                    {
                        boat.Speed = 5.0;
                        boat.Name = "cargoShip";
                        return boat;
                    }
            }
            return boat;
        }


        public double CheckWindSpeed(string city)
        {
            string apiUrl = "api.openweathermap.org/data/2.5/weather ? q ={" + city + " }&appid ={d86b929dc7cb67f9c96c0c0259f6b0a4}";
            return windSpeed;
        }
    }
}
