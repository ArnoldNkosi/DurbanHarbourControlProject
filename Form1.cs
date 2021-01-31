using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        double distanceFromPerimeterToDock = 10;
        List<Boat> CurrentBoatsAtThePerimeter = new List<Boat>();
        TravellingTime travellingTime = new TravellingTime();


        public Form1()
        {
            InitializeComponent();
            DisplayGoogleMap();
            StartTheProcess();

             // Stop the timer from running.

            if (CurrentBoatsAtThePerimeter.Count > 0)
            {  RequestPermissionToTravel(CurrentBoatsAtThePerimeter); 
            }
        }
        public void CalculateTravelTime(Boat boat)
        {
            if (shipInTheHarbour)
            {
                var nextStartDateTime = DateTime.Now.AddDays(5);
                double millisecondsToWait = (nextStartDateTime - DateTime.Now).TotalMilliseconds;
                System.Threading.Timer timer = new System.Threading.Timer( (o) => { CheckWindSpeed(); },
                null, (uint)millisecondsToWait, 0);
            }
            if (!shipInTheHarbour)
            {
                double timeTaken = distanceFromPerimeterToDock / boat.Speed;
                this.travellingTime.DepartureTime = DateTime.Now;
                this.travellingTime.ArrivalTime = DateTime.Now.AddHours(timeTaken);
                boat.enroute = true;

                var nextStartDateTime = DateTime.Now.AddHours(timeTaken);
                double millisecondsToWait = (nextStartDateTime - DateTime.Now).TotalMilliseconds;
                System.Threading.Timer timer = new System.Threading.Timer((o) => { UpdatePortTraffic(boat); },
                null, (uint)millisecondsToWait, 0);
            }
        }
        public void UpdatePortTraffic(Boat boat)
        {
            shipInTheHarbour = false;
            boat.enroute = false;
        }

        public bool CheckHarbourClearance(List<Boat> listofBoats)
        {
            return shipInTheHarbour;
        }

        public bool RequestPermissionToTravel(List<Boat> listofBoats)
        {
            Harbour harbourArea = new Harbour();
            harbourArea.Dock = new Location(-29.867997, 31.014409);
            harbourArea.Perimeter = new Location(-29.867993, 31.058299);

            List<Boat> boats = listofBoats;
            for (int i = 0; i < boats.Count; i++)
            {
                if (boats[i].Name == "sailboat")
                {
                    double windspeed = CheckWindSpeed();
                    if (10 < windspeed && windspeed > 30)
                        CalculateTravelTime(boats[i]);
                }
                else
                {
                    CalculateTravelTime(boats[i]);
                }
                boats[i].Location = harbourArea.Dock;
                SavingBoatArrival(boats[i]);
                CurrentBoatsAtThePerimeter.Remove(boats[i]);
            }
            return shipInTheHarbour;
        }

        public void SavingBoatArrival(Boat boat)
        {
            try
            {
                //Database functionality to save boat details once it has arrived to the dock.
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ShipScheduling()
        {
            Random random = new Random();
            int value = random.Next(1, 3);
            //Ship has arrived
             ShipRandomlyArrives(value);
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
                case 1:
                    {
                        boat.Speed = 30.0;
                        boat.Name = "speedboat";
                        return boat;
                    }

                case 2:
                    {
                        boat.Speed = 15.0;
                        boat.Name = "sailboat";
                        return boat;
                    }

                case 3:
                    {
                        boat.Speed = 5.0;
                        boat.Name = "cargoShip";
                        return boat;
                    }
            }
            if (boat.Name != null) { this.CurrentBoatsAtThePerimeter.Add(boat); }
            
            return boat;
        }


        public double CheckWindSpeed()
        {
            windSpeed = WeatherService.getWeatherData();
            return windSpeed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var checkWindSpeedPeriodically = DateTime.Now.AddDays(5);
                double millisecondsToWait = (checkWindSpeedPeriodically - DateTime.Now).TotalMilliseconds;
                System.Threading.Timer timer = new System.Threading.Timer((o) => { CheckWindSpeed();},
                null, (uint)millisecondsToWait, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pardon me, but an error has ocurred" + ex.Message.ToString());
            }
        }

        private void DisplayGoogleMap()
        {
            //Link copied directly from the page, I just simply want to display it.
            webBrowser1.Navigate("https://www.google.com/maps/place/Maydon+Wharf+St,+Maydon+Wharf,+Durban,+4001/@-29.8728755,31.006163,17z/data=!3m1!4b1!4m13!1m7!3m6!1s0x1ef7a9019b2fa7ad:0x9ec214ee099c63d8!2sHarbour,+Durban!3b1!8m2!3d-29.8723388!4d31.0249093!3m4!1s0x1ef7a99798347769:0x36fab2048461c3ab!8m2!3d-29.8728775!4d31.0083545");
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void StartTheProcess()
        {
            Random ran = new Random();
            var nextStartDateTime = DateTime.Now.AddSeconds(ran.Next(1, 10));
            double millisecondsToWait = (nextStartDateTime - DateTime.Now).TotalMilliseconds;
            System.Threading.Timer timer = new System.Threading.Timer((o) => { ShipScheduling(); },
            null, (uint)millisecondsToWait, 0);
            Thread.Sleep(3500); // Wait a bit over 4 seconds.
            timer.Change(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
            Thread.Sleep(9000);
            timer.Change(-1, -1);
        }
    }
}
