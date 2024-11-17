
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bingo
{
    internal class SiblingsCommand
    {
        private readonly RelationshipGraph graph;

        public SiblingsCommand(RelationshipGraph graph) => this.graph = graph;

        public void Execute(string person)
        {
            var node = graph.GetNode(person);
            if (node == null)
            {
                Console.WriteLine($"{person} not found.");
                return;
            }

            var parents = node.GetEdges("hasParent").Select(e => e.To()).ToList();
            var siblings = new HashSet<string>();

            foreach (var parent in parents)
            {
                var parentNode = graph.GetNode(parent);
                siblings.UnionWith(parentNode.GetEdges("hasChild").Where(e => e.To() != person).Select(e => e.To()));
            }

            Console.WriteLine($"Siblings of {person}: " + string.Join(", ", siblings));
        }
    }
}
