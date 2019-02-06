using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public class BaseSuggestor
    {
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

        public Dictionary<string, double> ShortestPath(Vehicle vehicle,
            WeatherType weatherType,
            List<WeatherImpact> weatherImpactList,
            List<List<Orbit>> possiblePaths)
        {
            if (!vehicle.SupportedWeather.Contains(weatherType))
            {
                return null;
            }

            WeatherImpact wi = weatherImpactList.FirstOrDefault(a => a.Weather == weatherType);

            Dictionary<string, double> result = new Dictionary<string, double>();

            foreach (List<Orbit> orbits in possiblePaths)
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
            string key = result.FirstOrDefault(a => a.Value == min).Key;

            var res = new Dictionary<string, double>();
            res.Add(key, min);

            return res;
        }
    }
}
