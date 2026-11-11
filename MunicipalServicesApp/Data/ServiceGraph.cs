using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Data
{
    public enum ServiceEdgeKind
    {
        DependsOn = 0,
        Related = 1
    }

    /// <summary>One request reached by a breadth-first walk, and how many steps away it is.</summary>
    public class RelatedStep
    {
        public string Reference { get; set; }

        public int Steps { get; set; }
    }

    /// <summary>
    /// Directed graph of service requests. DependsOn points at the request that must finish first.
    /// Related is stored in both directions for requests that share an area.
    /// </summary>
    public class ServiceGraph
    {
        private readonly Dictionary<string, List<ServiceEdge>> outgoing = new Dictionary<string, List<ServiceEdge>>(StringComparer.Ordinal);

        public void AddEdge(string from, string to, ServiceEdgeKind kind)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to) || from == to)
            {
                return;
            }

            List<ServiceEdge> edges;
            if (!outgoing.TryGetValue(from, out edges))
            {
                edges = new List<ServiceEdge>();
                outgoing.Add(from, edges);
            }

            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].To == to && edges[i].Kind == kind)
                {
                    return;
                }
            }

            edges.Add(new ServiceEdge { To = to, Kind = kind });
        }

        /// <summary>Open neighbours, nearest first. The start request is not included.</summary>
        public List<RelatedStep> BreadthFirst(string start, HashSet<string> openReferences)
        {
            List<RelatedStep> found = new List<RelatedStep>();
            if (string.IsNullOrEmpty(start) || openReferences == null || !openReferences.Contains(start))
            {
                return found;
            }

            HashSet<string> seen = new HashSet<string>(StringComparer.Ordinal);
            Queue<string> queue = new Queue<string>();
            Dictionary<string, int> depth = new Dictionary<string, int>(StringComparer.Ordinal);
            seen.Add(start);
            queue.Enqueue(start);
            depth[start] = 0;

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                List<ServiceEdge> edges;
                if (!outgoing.TryGetValue(current, out edges))
                {
                    continue;
                }

                List<ServiceEdge> ordered = new List<ServiceEdge>(edges);
                ordered.Sort(delegate(ServiceEdge a, ServiceEdge b)
                {
                    return string.Compare(a.To, b.To, StringComparison.Ordinal);
                });

                for (int i = 0; i < ordered.Count; i++)
                {
                    string next = ordered[i].To;
                    if (seen.Contains(next) || !openReferences.Contains(next))
                    {
                        continue;
                    }

                    seen.Add(next);
                    int steps = depth[current] + 1;
                    depth[next] = steps;
                    queue.Enqueue(next);
                    found.Add(new RelatedStep { Reference = next, Steps = steps });
                }
            }

            return found;
        }

        /// <summary>
        /// Depth-first post-order along DependsOn edges. The root cause is first.
        /// The start reference is last.
        /// </summary>
        public List<string> DependencyChain(string start)
        {
            List<string> chain = new List<string>();
            HashSet<string> visited = new HashSet<string>(StringComparer.Ordinal);
            Visit(start, visited, chain);
            return chain;
        }

        private void Visit(string reference, HashSet<string> visited, List<string> chain)
        {
            if (string.IsNullOrEmpty(reference) || visited.Contains(reference))
            {
                return;
            }

            visited.Add(reference);
            List<string> prerequisites = new List<string>();
            List<ServiceEdge> edges;
            if (outgoing.TryGetValue(reference, out edges))
            {
                for (int i = 0; i < edges.Count; i++)
                {
                    if (edges[i].Kind == ServiceEdgeKind.DependsOn)
                    {
                        prerequisites.Add(edges[i].To);
                    }
                }
            }

            prerequisites.Sort(StringComparer.Ordinal);
            for (int i = 0; i < prerequisites.Count; i++)
            {
                Visit(prerequisites[i], visited, chain);
            }

            chain.Add(reference);
        }

        private sealed class ServiceEdge
        {
            public string To;
            public ServiceEdgeKind Kind;
        }
    }
}
