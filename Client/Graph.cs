using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public class Graph
    {
        private readonly List<Orbit> inputOrbits;
        private readonly List<Node> nodes;

        public Graph(List<Orbit> input)
        {
            this.inputOrbits = input;
            nodes = GetNodes(inputOrbits);
        }

        public List<List<Orbit>> GetAllPossiblePaths(string s, string d)
        {
            var possibleOrbits = FindAllPaths(s, d, this.nodes);
            List<string> lst = new List<string>();
            foreach (Orbit orbit in possibleOrbits)
            {
                PrintPaths(orbit, orbit.Name, lst);
            }

            List<List<Orbit>> possiblePaths = new List<List<Orbit>>();
            foreach (string str in lst)
            {
                string[] arr = str.Split("->");
                var resOrbits = this.inputOrbits.Where(a => arr.Contains(a.Name)).ToList();
                possiblePaths.Add(resOrbits);
            }
            return possiblePaths;
        }

        public void PrintPaths(Orbit orbit, string str, List<string> lst)
        {
            if (orbit.ConnectedOrbits.Any())
            {
                foreach (Orbit t in orbit.ConnectedOrbits)
                {
                    PrintPaths(t, str + "->" + t.Name, lst);
                }
            }
            else
            {
                lst.Add(str);
                //Console.WriteLine(str);
            }
        }

        public List<Orbit> FindAllPaths(string s, string d, List<Node> nodes)
        {
            List<Orbit> result = new List<Orbit>();

            Node srcNode = nodes.FirstOrDefault(a => a.Name == s);
            if (srcNode.ConnectedOrbits.Any())
            {
                foreach (Orbit orbit in srcNode.ConnectedOrbits)
                {
                    Orbit newOrbit = new Orbit()
                    {
                        Destination = orbit.Destination,
                        Source = orbit.Source,
                        Name = orbit.Name,
                        Distance = orbit.Distance,
                        NumberOfCraters = orbit.NumberOfCraters
                    };

                    string dest = newOrbit.Destination == s ? newOrbit.Source : newOrbit.Destination;

                    if (dest != d)
                    {
                        FindAllPathsUtil(dest, d, nodes, new List<string>() { srcNode.Name }, newOrbit);
                    }
                    if (dest == d || newOrbit.ConnectedOrbits.Any())
                    {
                        result.Add(newOrbit);
                    }
                }
            }
            return result;
        }

        public void FindAllPathsUtil(string s, string d, List<Node> nodes,
            List<string> visitedNodes, Orbit resultOrbit)
        {
            Node srcNode = nodes.FirstOrDefault(a => a.Name == s);
            visitedNodes.Add(srcNode.Name);
            if (srcNode.ConnectedOrbits.Any())
            {
                foreach (Orbit orbit in srcNode.ConnectedOrbits)
                {
                    Orbit newOrbit = new Orbit()
                    {
                        Destination = orbit.Destination,
                        Source = orbit.Source,
                        Name = orbit.Name,
                        Distance = orbit.Distance,
                        NumberOfCraters = orbit.NumberOfCraters
                    };

                    string dest = newOrbit.Destination == s ? newOrbit.Source : newOrbit.Destination;
                    if (visitedNodes.Contains(dest))
                    {
                        continue;
                    }

                    if (dest != d)
                    {
                        FindAllPathsUtil(dest, d, nodes, visitedNodes, newOrbit);
                    }
                    if (dest == d || newOrbit.ConnectedOrbits.Any())
                    {
                        resultOrbit.ConnectedOrbits.Add(newOrbit);
                    }
                }
            }
            else
            {
                return;
            }
        }

        public List<Node> GetNodes(List<Orbit> orbits)
        {
            List<Node> nodes = new List<Node>();
            if (orbits.Any())
            {
                for (int i = 0; i < orbits.Count; i++)
                {
                    Node node = nodes.FirstOrDefault(a => a.Name == orbits[i].Source);
                    if (node == null)
                    {
                        node = new Node()
                        {
                            Name = orbits[i].Source,
                            ConnectedOrbits = new List<Orbit>() { orbits[i] }
                        };
                        nodes.Add(node);
                    }
                    else
                    {
                        node.ConnectedOrbits.Add(orbits[i]);
                    }

                    node = nodes.FirstOrDefault(a => a.Name == orbits[i].Destination);
                    if (node == null)
                    {
                        node = new Node()
                        {
                            Name = orbits[i].Destination,
                            ConnectedOrbits = new List<Orbit>() { orbits[i] }
                        };
                        nodes.Add(node);
                    }
                    else
                    {
                        node.ConnectedOrbits.Add(orbits[i]);
                    }
                }
            }
            return nodes;
        }
    }
}
