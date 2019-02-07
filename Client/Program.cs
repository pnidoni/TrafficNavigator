using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Orbit> inputOrbits = new List<Orbit>
            {
                new Orbit()
                {
                    Destination = "MarathHalli",
                    Distance = 18,
                    Name = "Orbit 1",
                    NumberOfCraters = 20,
                    Source = "Silk board"
                },
                new Orbit()
                {
                    Destination = "MarathHalli",
                    Distance = 20,
                    Name = "Orbit 2",
                    NumberOfCraters = 10,
                    Source = "Silk board"
                },
                new Orbit()
                {
                    Destination = "KR Puram",
                    Distance = 30,
                    Name = "Orbit 3",
                    NumberOfCraters = 15,
                    Source = "Silk board"
                },
                new Orbit()
                {
                    Destination = "MarathHalli",
                    Distance = 25,
                    Name = "Orbit 4",
                    NumberOfCraters = 18,
                    Source = "KR Puram"
                },
                new Orbit()
                {
                    Destination = "MarathHalli",
                    Distance = 25,
                    Name = "Orbit 5",
                    NumberOfCraters = 18,
                    Source = "Dommaluru"
                },
                new Orbit()
                {
                    Destination = "Dommaluru",
                    Distance = 25,
                    Name = "Orbit 6",
                    NumberOfCraters = 18,
                    Source = "Silk board"
                },
                new Orbit()
                {
                    Destination = "Dommaluru",
                    Distance = 25,
                    Name = "Orbit 7",
                    NumberOfCraters = 18,
                    Source = "KR Puram"
                },
                new Orbit()
                {
                    Destination = "Dommaluru",
                    Distance = 25,
                    Name = "Orbit 8",
                    NumberOfCraters = 18,
                    Source = "Hebbal"
                },
                new Orbit()
                {
                    Destination = "Yeshwantpur",
                    Distance = 25,
                    Name = "Orbit 9",
                    NumberOfCraters = 18,
                    Source = "Market"
                }
            };

            var vehicles = new List<Vehicle>
            {
                new Vehicle()
                {
                    Name = "Bike",
                    Speed = 10.0/60.0,
                    TimeRequiredToCrossCrater = 2,
                    SupportedWeather = new List<WeatherType>() { WeatherType.Sunny }
                },
                new Vehicle()
                {
                    Name = "Auto",
                    Speed = 12.0/60.0,
                    TimeRequiredToCrossCrater = 1,
                    SupportedWeather = new List<WeatherType>() { WeatherType.Sunny, WeatherType.Rainy}
                },
                new Vehicle()
                {
                    Name = "Car",
                    Speed = 20.0/60.0,
                    TimeRequiredToCrossCrater = 3,
                    SupportedWeather = new List<WeatherType>() { WeatherType.Sunny, WeatherType.Rainy,WeatherType.Windy  }
                }
            };

            List<WeatherImpact> impact = new List<WeatherImpact>()
            {
                new WeatherImpact()
                {
                    PercentageChangeInNumberOfCraters = -10,
                    Weather = WeatherType.Sunny
                },
                new WeatherImpact()
                {
                    PercentageChangeInNumberOfCraters = 20,
                    Weather = WeatherType.Rainy
                }
            };

            while(true)
            {
                Console.WriteLine();
                Console.WriteLine("*********************************************");
                Console.WriteLine("Enter Choice");
                Console.WriteLine("1.Single Destination\t 2.Multiple Destination");
                var choice = Console.ReadLine().Trim();

                Graph graph = null;
                bool isValid = false;
                List<Node> nodes = null;
                switch (choice)
                {
                    case "1":
                    
                        Console.WriteLine("Enter Source");
                        var source = Console.ReadLine().Trim();

                        Console.WriteLine("Enter Destination");
                        var dest = Console.ReadLine().Trim();
                        Console.WriteLine();

                        graph = new Graph(inputOrbits);
                        var possiblePaths = graph.GetAllPossiblePaths(source, dest);

                        nodes = graph.GetNodes(inputOrbits);
                        List<string> lst = new List<string>(){source, dest};
                        isValid = lst.All(a => nodes.Any(n => n.Name == a));

                        if (!isValid)
                            break;

                        RouteSuggestor rs = new RouteSuggestor(possiblePaths, vehicles, impact);
                        rs.CalculateTimeForEachVehicle();

                        break;

                    case "2":
                        Console.WriteLine("Enter the path from source to destination seperated by commas");
                        Console.WriteLine("e.g A,B,C");
                        var d = Console.ReadLine().Trim().Split(',');
                        Console.WriteLine();

                        graph = new Graph(inputOrbits);
                        nodes = graph.GetNodes(inputOrbits);
                        isValid = d.All(a => nodes.Any(n => n.Name == a));

                        if (!isValid)
                            break;

                        MultiDestinationSuggestor md = new MultiDestinationSuggestor(graph, vehicles, impact, d);
                        md.CalculateTimeForEachVehicle();

                        break;

                    default:
                        break;
                }
            }

        }

    }
}
