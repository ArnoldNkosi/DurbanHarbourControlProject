using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.BusinessLayer;
using Timer = System.Windows.Forms.Timer;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form,IHarbourControl
    {
        public enum TypeOfBoat
        {
            speedboat,
            sailboat,
            cargoShip
        }

        bool shipInTheHarbour = false;
        double windSpeed = 0.00;
        double distanceFromPerimeterToDock = 10;
        List<Boat> CurrentBoatsAtThePerimeter = new List<Boat>();
        TravellingTime travellingTime = new TravellingTime();
        DateTime nextStartDateTime = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
            DisplayGoogleMap();
            StartTheProcess();
            if (CurrentBoatsAtThePerimeter.Count > 0)
            {  
                RequestPermissionToTravel(CurrentBoatsAtThePerimeter); 
            }
        }
        public void CalculateTravelTime(Boat boat)
        {
            double timeTaken = distanceFromPerimeterToDock / boat.Speed;
            this.travellingTime.DepartureTime = DateTime.Now;
            this.travellingTime.ArrivalTime = DateTime.Now.AddHours(timeTaken);

            //Check if there's currently a ship inside the harbour
            if (this.shipInTheHarbour)
            {
               //We have to wait for the boat to reach the dock from the perimeter and it will do so in the nextStartDate.
                double millisecondsToWait = (this.nextStartDateTime - DateTime.Now).TotalMilliseconds;
                System.Threading.Timer timer = new System.Threading.Timer( (o) => { CheckBoatInsideHarbour(boat);},
                null, (uint)millisecondsToWait, 0);

                //Once the startDate is equals to the current date. The next boat can enter the harbour.
                if (this.nextStartDateTime == DateTime.Now)
                {
                    CheckBoatInsideHarbour(boat);
                    BoatisTravelling(timeTaken, boat);
                }
            }

            //If there's no ship then this boat can enter
            else 
            {
                CheckBoatInsideHarbour(boat);
                BoatisTravelling(timeTaken, boat);
            }
        }

        //This will wait for the boat to reach the dock before clearing out the port traffic.
        public void BoatisTravelling(double timeTaken,Boat boat)
        {
            //Adding minutes to make it better for testing purposes, but for the actual code I'd have to add hours.
            this.nextStartDateTime = DateTime.Now.AddMinutes(timeTaken);
            double millisecondsToWait = (nextStartDateTime - DateTime.Now).TotalMilliseconds;
            System.Threading.Timer timer = new System.Threading.Timer((o) => { UpdatePortTraffic(boat); },
            null, (uint)millisecondsToWait, 0);
        }

        //Clearing out the port here.
        public void UpdatePortTraffic(Boat boat)
        {
            this.shipInTheHarbour = false;
            boat.enroute = false;
        }


        //Current boat has entered the harbour, so other boats have to wait for this one to reach the dock first.
        public void CheckBoatInsideHarbour(Boat boat)
        {
                this.shipInTheHarbour = true;
                boat.enroute = true;
        }

        //Boats need to check if they enter the harbour just yet.
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
                    if ((10 < windspeed) && (windspeed > 30))
                        CalculateTravelTime(boats[i]);
                }
                else
                {
                    CalculateTravelTime(boats[i]);
                }
                boats[i].Location = harbourArea.Dock;
                //will save the boat to the database once it has arrived.
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

        //A ship randomly arriving at the harbour.
        public Boat ShipRandomlyArrives()
        {
            Random random = new Random();
            int value = random.Next(1, 3);
            Boat boat = new Boat();
           
           
            boat.Location = new Location(-29.867997, 31.014409);
            TypeOfBoat boatType = new TypeOfBoat();
          //  value = (int)boatType;

            switch (value)
            {
                case 1:
                    {
                        boat.Speed = 30.0;
                        boat.Name = "speedboat";
                        break;
                    }

                case 2:
                    {
                        boat.Speed = 15.0;
                        boat.Name = "sailboat";
                        break;
                    }

                case 3:
                    {
                        boat.Speed = 5.0;
                        boat.Name = "cargoShip";
                        break;
                    }
            }
            if (boat.Name != null) 
            { 
                this.CurrentBoatsAtThePerimeter.Add(boat); 
            }
            return boat;
        }


        //Checking the windspeed in durban currently.
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

        //Using this to display the map on the webbrowser.
        private void DisplayGoogleMap()
        {
            //Link copied directly from the page, I just simply want to display it.
            string mapUrl = ConfigurationManager.AppSettings.Get("mapUrl");
            webBrowser1.Navigate(mapUrl);
            webBrowser1.ScriptErrorsSuppressed = true;
        }
        private void StartTheProcess()
        {
            CheckWindSpeedPeriodically();
            Random ran = new Random();
            var nextStartDateTime = DateTime.Now.AddSeconds(ran.Next(5, 10));
            double millisecondsToWait = (nextStartDateTime - DateTime.Now).TotalMilliseconds;
            System.Threading.Timer timer = new System.Threading.Timer((o) => { ShipRandomlyArrives(); },
            null, (uint)millisecondsToWait, 0);
            Thread.Sleep(3500); // Wait a bit over 4 seconds.
            timer.Change(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
            Thread.Sleep(9000);
            timer.Change(-1, -1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanks for trying out our beta version");
            Environment.Exit(0);    
        }

        private Timer timer1;
        public void CheckWindSpeedPeriodically()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(button1_Click);
            timer1.Interval = 2000; // in miliseconds
            timer1.Start();
        }      
    }
}
