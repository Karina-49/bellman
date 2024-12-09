using System;
using System.Collections.Generic;

namespace BellmanFordAlgorithm
{
    class Edge
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Weight { get; set; }

        public Edge(int source, int destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }
    }

    class Graph
    {
        public List<Edge> Edges { get; set; }
        public int VerticesCount { get; set; }

        public Graph(int verticesCount)
        {
            VerticesCount = verticesCount;
            Edges = new List<Edge>();
        }

        public void AddEdge(int source, int destination, int weight)
        {
            Edges.Add(new Edge(source, destination, weight));
        }
    }

    class BellmanFord
    {
        public static bool Run(Graph graph, int source, out int[] distances)
        {
            int V = graph.VerticesCount;
            distances = new int[V];

            // Step 1: Initialize distances
            for (int i = 0; i < V; i++)
            {
                distances[i] = int.MaxValue;
            }
            distances[source] = 0;

            // Step 2: Relax edges V - 1 times
            for (int i = 0; i < V - 1; i++)
            {
                foreach (var edge in graph.Edges)
                {
                    if (distances[edge.Source] != int.MaxValue && distances[edge.Source] + edge.Weight < distances[edge.Destination])
                    {
                        distances[edge.Destination] = distances[edge.Source] + edge.Weight;
                    }
                }
            }

            // Step 3: Check for negative weight cycles
            foreach (var edge in graph.Edges)
            {
                if (distances[edge.Source] != int.MaxValue && distances[edge.Source] + edge.Weight < distances[edge.Destination])
                {
                    Console.WriteLine("Graph contains a negative weight cycle.");
                    return false;
                }
            }

            return true;
        }
    }

    class Program
    {
        static void Main()
        {
            Graph graph = new Graph(5);

            // Adding edges: define the graph here
            graph.AddEdge(0, 1, 4);   // A -> B
            graph.AddEdge(0, 2, 2);   // A -> C
            graph.AddEdge(1, 2, 1);   // B -> C
            graph.AddEdge(1, 3, 5);   // B -> D
            graph.AddEdge(2, 4, 10);  // C -> E
            graph.AddEdge(3, 4, -7);  // D -> E (negative weight)

            int source = 0; // Starting from vertex A
            if (BellmanFord.Run(graph, source, out int[] distances))
            {
                Console.WriteLine("Shortest distances from source:");
                for (int i = 0; i < distances.Length; i++)
                {
                    Console.WriteLine($"Distance from A to {i}: {distances[i]}");
                }
            }
            else
            {
                Console.WriteLine("The graph contains a negative weight cycle.");
            }
        }
    }
}
