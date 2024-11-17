
using System;

using System.Collections.Generic;
using System.Linq;

namespace Bingo
{
    internal class Orphan
    {
        private readonly RelationshipGraph graph;

        public Orphan(RelationshipGraph graph) => this.graph = graph;

        public void Execute()
        {
            var orphans = new List<string>();
            foreach (var person in graph.nodes)
            {
                if (!person.GetEdges("hasParent").Any())
                {
                    orphans.Add(person.Name);
                }
            }
            Console.WriteLine("Orphans: " + string.Join(", ", orphans));
        }
    }
}
