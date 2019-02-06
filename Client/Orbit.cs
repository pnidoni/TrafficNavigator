using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public class Orbit
    {
        public Orbit()
        {
            ConnectedOrbits = new List<Orbit>();
        }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int Distance { get; set; }
        public int NumberOfCraters { get; set; }
        public List<Orbit> ConnectedOrbits { get; set; }
    }
}
