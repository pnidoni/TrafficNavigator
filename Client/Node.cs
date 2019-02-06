using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public class Node
    {
        public string Name { get; set; }
        public List<Orbit> ConnectedOrbits { get; set; }
        public bool isVisited { get; set; }
    }
}
