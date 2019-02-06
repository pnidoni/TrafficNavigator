using System;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    public class RouteSuggestor : BaseSuggestor
    {
        private readonly List<List<Orbit>> _possiblePathList;
        private readonly List<Vehicle> _vehicles;
        private readonly List<WeatherImpact> _weatherImpactList;

        public RouteSuggestor(List<List<Orbit>> possiblePathList,
            List<Vehicle> vehicles,
            List<WeatherImpact> weatherImpactList)
        {
            _possiblePathList = possiblePathList;
            _vehicles = vehicles;
            _weatherImpactList = weatherImpactList;
        }

        public void CalculateTimeForEachVehicle()
        {
            CalculateTimeForWeather(WeatherType.Sunny);
            CalculateTimeForWeather(WeatherType.Rainy);
            CalculateTimeForWeather(WeatherType.Windy);
        }

        private void CalculateTimeForWeather(WeatherType weatherType)
        {
            Console.WriteLine("*********" + weatherType.ToString() + " Weather**********");
            foreach (Vehicle vehicle in _vehicles)
            {
                var res = ShortestPath(vehicle, weatherType, _weatherImpactList, _possiblePathList);
                PrintOutput(res, vehicle);
            }
        }

        public void PrintOutput(Dictionary<string, double> value, Vehicle vehicle)
        {
            if (value != null && value.Count > 0)
            {
                foreach (var d in value)
                {
                    Console.WriteLine("By {0},", vehicle.Name);
                    Console.WriteLine("     {0} minutes - {1}", d.Value, d.Key);
                }
            }
            else
            {
                Console.WriteLine("Not Supported by {0}", vehicle.Name);
            }
        }
    }


}
