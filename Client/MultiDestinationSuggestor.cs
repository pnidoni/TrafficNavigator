using System;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    public class MultiDestinationSuggestor : BaseSuggestor
    {
        private readonly Graph _graph;
        private readonly List<Vehicle> _vehicles;
        private readonly List<WeatherImpact> _weatherImpactList;
        private readonly string[] _destArr;

        public MultiDestinationSuggestor(Graph graph,
            List<Vehicle> vehicles,
            List<WeatherImpact> weatherImpactList,
            string[] destArr)
        {
            _graph = graph;
            _vehicles = vehicles;
            _weatherImpactList = weatherImpactList;
            _destArr = destArr;
        }

        public void CalculateTimeForEachVehicle()
        {
            Console.WriteLine("*************Output**************");
            CalculateTimeForWeather(WeatherType.Sunny);
            CalculateTimeForWeather(WeatherType.Rainy);
            CalculateTimeForWeather(WeatherType.Windy);
        }

        private void CalculateTimeForWeather(WeatherType weatherType)
        {
            Console.WriteLine("*********" + weatherType.ToString() + " Weather**********");
            foreach (Vehicle vehicle in _vehicles)
            {
                FastestPath(vehicle, weatherType);
            }
        }

        public void FastestPath(Vehicle vehicle, WeatherType weatherType)
        {
            if (!vehicle.SupportedWeather.Contains(weatherType))
            {
                Console.WriteLine("Not Supported by {0}", vehicle.Name);
                return;
            }

            WeatherImpact wi = _weatherImpactList.FirstOrDefault(a => a.Weather == weatherType);

            var source = _destArr[0];

            Dictionary<string, double> result = new Dictionary<string, double>();

            for (int i = 1; i < _destArr.Length; i++)
            {
                var res = ShortestPath(vehicle, weatherType, _weatherImpactList, _graph.GetAllPossiblePaths(source, _destArr[i]));
                result.Add(res.First().Key, res.First().Value);
                source = _destArr[i];
            }

            double totalTime = result.Sum(a => a.Value);
            string route = "";
            foreach (var d in result)
            {
                route = route + "->" + d.Key;
            }
            
            Console.WriteLine("By {0},", vehicle.Name);
            Console.WriteLine("     {0} minutes - {1}", totalTime, route);
        }

    }
}
