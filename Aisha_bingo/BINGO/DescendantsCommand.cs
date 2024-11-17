
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;


namespace Bingo
{
    internal class DescendantsCommand
    {
        private readonly RelationshipGraph graph;

        public DescendantsCommand(RelationshipGraph graph) => this.graph = graph;

        public void Execute(string person)
        {
            var node = graph.GetNode(person);
            if (node == null)
            {
                Console.WriteLine($"{person} not found.");
                return;
            }

            var descendants = new Dictionary<int, List<string>>();
            var queue = new Queue<(string, int)>();
            queue.Enqueue((person, 0));

            while (queue.Count > 0)
            {
                var (current, level) = queue.Dequeue();
                var currentNode = graph.GetNode(current);

                foreach (var edge in currentNode.GetEdges("hasChild"))
                {
                    if (!descendants.ContainsKey(level + 1))
                    {
                        descendants[level + 1] = new List<string>();
                    }
                    descendants[level + 1].Add(edge.To());
                    queue.Enqueue((edge.To(), level + 1));
                }
            }

            foreach (var level in descendants.Keys)
            {
                Console.WriteLine($"{new string(' ', level * 2)}Level {level}: " + string.Join(", ", descendants[level]));
            }
        }
    }
}
