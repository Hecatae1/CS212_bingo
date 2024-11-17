
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bingo
{
    internal class BingoCommand
    {
        private readonly RelationshipGraph graph;

        public BingoCommand(RelationshipGraph graph) => this.graph = graph;

        public void Execute(string start, string end)
        {
            var predecessors = new Dictionary<string, string>();
            var queue = new Queue<string>();
            queue.Enqueue(start);
            predecessors[start] = null;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current == end) break;

                var currentNode = graph.GetNode(current);
                foreach (var edge in currentNode.GetEdges())
                {
                    if (!predecessors.ContainsKey(edge.To()))
                    {
                        predecessors[edge.To()] = current;
                        queue.Enqueue(edge.To());
                    }
                }
            }

            if (!predecessors.ContainsKey(end))
            {
                Console.WriteLine($"No path found between {start} and {end}");
                return;
            }

            var path = new List<string>();
            for (var at = end; at != null; at = predecessors[at])
            {
                path.Add(at);
            }
            path.Reverse();

            for (int i = 0; i < path.Count - 1; i++)
            {
                var label = graph.GetNode(path[i]).GetEdges().First(e => e.To() == path[i + 1]).Label;
                Console.WriteLine($"{path[i]} is a {label} of {path[i + 1]}");
            }
        }
    }
}
