using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public class Vehicle
    {
        public string Name { get; set; }
        public double Speed { get; set; }
        public double TimeRequiredToCrossCrater { get; set; }
        public List<WeatherType> SupportedWeather { get; set; }
    }
}
