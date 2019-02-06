using System;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    public class RouteSuggestor
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
            Console.WriteLine("*********Sunny Weather**********");
            foreach (Vehicle vehicle in _vehicles)
            {
                FastestPath(vehicle, WeatherType.Sunny);
            }
            Console.WriteLine("*********Rainy Weather**********");
            foreach (Vehicle vehicle in _vehicles)
            {
                FastestPath(vehicle, WeatherType.Rainy);
            }
            Console.WriteLine("*********Windy Weather**********");
            foreach (Vehicle vehicle in _vehicles)
            {
                FastestPath(vehicle, WeatherType.Windy);
            }
        }

        public void FastestPath(Vehicle vehicle,
            WeatherType weatherType)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            if (!vehicle.SupportedWeather.Contains(weatherType))
            {
                Console.WriteLine("Not Supported by {0}", vehicle.Name);
                return;
            }

            WeatherImpact wi = _weatherImpactList.FirstOrDefault(a => a.Weather == weatherType);

            foreach (List<Orbit> orbits in _possiblePathList)
            {
                double time = 0.0;
                string path = "";
                foreach (Orbit orbit in orbits)
                {
                    time += TimeToCrossTheOrbit(vehicle, wi, orbit, weatherType).Value;
                    path = path + "->" + orbit.Name;
                }
                result.Add(path, time);
            }

            double min = result.Min(a => a.Value);
            Console.WriteLine("By {0},", vehicle.Name);
            Console.WriteLine("     {0} minutes - {1}", min, result.FirstOrDefault(a => a.Value == min).Key);
        }

        public double? TimeToCrossTheOrbit(Vehicle vehicle,
            WeatherImpact weatherImpact,
            Orbit orbit,
            WeatherType weatherType)
        {

            double time = (orbit.Distance / vehicle.Speed);
            if (weatherImpact == null)
            {
                time = time + (orbit.NumberOfCraters * vehicle.TimeRequiredToCrossCrater);
            }
            else
            {
                time = time + ((orbit.NumberOfCraters + (orbit.NumberOfCraters * weatherImpact.PercentageChangeInNumberOfCraters / 100))
                               * vehicle.TimeRequiredToCrossCrater);
            }
            return time;
        }
    }


}
