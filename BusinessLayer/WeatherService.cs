using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BusinessLayer
{
    public class WeatherService
    {
        public static double getWeatherData()
        {
            double windspeed = 0.00;
            string apikey = ConfigurationManager.AppSettings.Get("apiKey");
            try
            {
                var httpClient = new RestClient("http://api.openweathermap.org/data/2.5/");
                var request = new RestRequest("weather?q=durban&appid="+apikey);
                var response = httpClient.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string callbackResponse = response.Content;
                    WeatherResponse weatherData = JsonConvert.DeserializeObject<WeatherResponse> (callbackResponse);
                    if (weatherData.Wind.speed != null) windspeed = weatherData.Wind.speed;
                }
            }
            catch (Exception ex)
            {

                throw ;
            }
            return windspeed;
        }
    }
}
