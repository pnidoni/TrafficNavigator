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
            Console.WriteLine("*********Sunny Weather**********");
            foreach (Vehicle vehicle in _vehicles)
            {
                FastestPath(vehicle, WeatherType.Sunny);
            }
            Console.WriteLine();
            Console.WriteLine("*********Rainy Weather**********");
            foreach (Vehicle vehicle in _vehicles)
            {
                FastestPath(vehicle, WeatherType.Rainy);
            }
            Console.WriteLine();
            Console.WriteLine("*********Windy Weather**********");
            foreach (Vehicle vehicle in _vehicles)
            {
                FastestPath(vehicle, WeatherType.Windy);
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
                Dictionary<string, double> intermediateResult = new Dictionary<string, double>();
                var possibleOrbits = _graph.GetAllPossiblePaths(source, _destArr[i]);
                foreach (List<Orbit> orbits in possibleOrbits)
                {
                    double time = 0.0;
                    string path = "";
                    foreach (Orbit orbit in orbits)
                    {
                        time += TimeToCrossTheOrbit(vehicle, wi, orbit, weatherType).Value;
                        path = path + "->" + orbit.Name;
                    }
                    intermediateResult.Add(path, time);
                }
                double min = intermediateResult.Min(a => a.Value);
                result.Add(intermediateResult.FirstOrDefault(a => a.Value == min).Key, min);
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
