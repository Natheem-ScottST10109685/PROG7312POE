using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10109685Prog7312POE
{
    /// <summary>
    /// Enhanced Graph to track relationships between service requests
    /// Includes BFS, DFS, and Minimum Spanning Tree algorithms
    /// </summary>
    public class ServiceRequestGraph
    {
        private Dictionary<int, List<GraphEdge>> adjacencyList;
        private Dictionary<int, ServiceRequest> requestLookup;

        public ServiceRequestGraph()
        {
            adjacencyList = new Dictionary<int, List<GraphEdge>>();
            requestLookup = new Dictionary<int, ServiceRequest>();
        }

        public void AddRequest(ServiceRequest request)
        {
            if (!adjacencyList.ContainsKey(request.Id))
            {
                adjacencyList[request.Id] = new List<GraphEdge>();
                requestLookup[request.Id] = request;
            }
        }

        public void AddEdge(int fromId, int toId, int weight)
        {
            if (adjacencyList.ContainsKey(fromId) && adjacencyList.ContainsKey(toId))
            {
                // Check if edge already exists
                if (!adjacencyList[fromId].Any(e => e.ToId == toId))
                {
                    adjacencyList[fromId].Add(new GraphEdge(fromId, toId, weight));
                    adjacencyList[toId].Add(new GraphEdge(toId, fromId, weight));
                }
            }
        }

        /// <summary>
        /// Breadth-First Search traversal starting from a request ID
        /// Returns list of request IDs in BFS order
        /// </summary>
        public List<int> BFS(int startId)
        {
            if (!adjacencyList.ContainsKey(startId))
                return new List<int>();

            List<int> visited = new List<int>();
            Queue<int> queue = new Queue<int>();
            HashSet<int> seen = new HashSet<int>();

            queue.Enqueue(startId);
            seen.Add(startId);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                visited.Add(current);

                foreach (var edge in adjacencyList[current].OrderBy(e => e.Weight))
                {
                    if (!seen.Contains(edge.ToId))
                    {
                        queue.Enqueue(edge.ToId);
                        seen.Add(edge.ToId);
                    }
                }
            }

            return visited;
        }

        /// <summary>
        /// Depth-First Search traversal starting from a request ID
        /// Returns list of request IDs in DFS order
        /// </summary>
        public List<int> DFS(int startId)
        {
            if (!adjacencyList.ContainsKey(startId))
                return new List<int>();

            List<int> visited = new List<int>();
            HashSet<int> seen = new HashSet<int>();
            DFSRecursive(startId, visited, seen);
            return visited;
        }

        private void DFSRecursive(int nodeId, List<int> visited, HashSet<int> seen)
        {
            seen.Add(nodeId);
            visited.Add(nodeId);

            foreach (var edge in adjacencyList[nodeId].OrderBy(e => e.Weight))
            {
                if (!seen.Contains(edge.ToId))
                {
                    DFSRecursive(edge.ToId, visited, seen);
                }
            }
        }

        /// <summary>
        /// Get requests related to a specific request
        /// </summary>
        public List<int> GetRelatedRequests(int requestId)
        {
            if (!adjacencyList.ContainsKey(requestId))
                return new List<int>();

            return adjacencyList[requestId]
                .OrderByDescending(e => e.Weight)
                .Select(e => e.ToId)
                .Take(5)
                .ToList();
        }

        /// <summary>
        /// Minimum Spanning Tree using Kruskal's Algorithm
        /// Returns optimal processing order for related service requests
        /// </summary>
        public List<MSTEdge> GetMinimumSpanningTree()
        {
            List<MSTEdge> allEdges = new List<MSTEdge>();

            // Collect all unique edges
            HashSet<string> addedEdges = new HashSet<string>();
            foreach (var kvp in adjacencyList)
            {
                foreach (var edge in kvp.Value)
                {
                    string edgeKey = GetEdgeKey(edge.FromId, edge.ToId);
                    if (!addedEdges.Contains(edgeKey))
                    {
                        allEdges.Add(new MSTEdge
                        {
                            FromId = edge.FromId,
                            ToId = edge.ToId,
                            Weight = edge.Weight,
                            FromRequest = requestLookup[edge.FromId],
                            ToRequest = requestLookup[edge.ToId]
                        });
                        addedEdges.Add(edgeKey);
                    }
                }
            }

            // Sort edges by weight (descending for priority - higher weight = stronger connection)
            allEdges = allEdges.OrderByDescending(e => e.Weight).ToList();

            // Kruskal's algorithm with Union-Find
            UnionFind uf = new UnionFind(adjacencyList.Keys.ToList());
            List<MSTEdge> mst = new List<MSTEdge>();

            foreach (var edge in allEdges)
            {
                if (uf.Find(edge.FromId) != uf.Find(edge.ToId))
                {
                    mst.Add(edge);
                    uf.Union(edge.FromId, edge.ToId);

                    // MST complete when we have n-1 edges
                    if (mst.Count == adjacencyList.Count - 1)
                        break;
                }
            }

            return mst;
        }

        /// <summary>
        /// Get optimal processing order based on MST
        /// Groups related requests for efficient batch processing
        /// </summary>
        public List<List<int>> GetOptimalProcessingOrder()
        {
            var mst = GetMinimumSpanningTree();
            var components = new List<List<int>>();

            if (mst.Count == 0)
            {
                // No connections - return all as individual groups
                foreach (var id in adjacencyList.Keys)
                {
                    components.Add(new List<int> { id });
                }
                return components;
            }

            // Build connected components from MST
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            foreach (var id in adjacencyList.Keys)
            {
                graph[id] = new List<int>();
            }

            foreach (var edge in mst)
            {
                graph[edge.FromId].Add(edge.ToId);
                graph[edge.ToId].Add(edge.FromId);
            }

            // Find connected components using DFS
            HashSet<int> visited = new HashSet<int>();
            foreach (var startId in adjacencyList.Keys)
            {
                if (!visited.Contains(startId))
                {
                    List<int> component = new List<int>();
                    DFSComponent(startId, graph, visited, component);
                    components.Add(component);
                }
            }

            return components;
        }

        private void DFSComponent(int nodeId, Dictionary<int, List<int>> graph, HashSet<int> visited, List<int> component)
        {
            visited.Add(nodeId);
            component.Add(nodeId);

            foreach (var neighbor in graph[nodeId])
            {
                if (!visited.Contains(neighbor))
                {
                    DFSComponent(neighbor, graph, visited, component);
                }
            }
        }

        private string GetEdgeKey(int id1, int id2)
        {
            return id1 < id2 ? $"{id1}-{id2}" : $"{id2}-{id1}";
        }

        /// <summary>
        /// Get traversal statistics for analysis
        /// </summary>
        public GraphStatistics GetStatistics(int startId)
        {
            var bfsResult = BFS(startId);
            var dfsResult = DFS(startId);

            return new GraphStatistics
            {
                TotalNodes = adjacencyList.Count,
                TotalEdges = adjacencyList.Sum(kvp => kvp.Value.Count) / 2,
                BFSReachableNodes = bfsResult.Count,
                DFSReachableNodes = dfsResult.Count,
                BFSTraversal = bfsResult,
                DFSTraversal = dfsResult
            };
        }

        // ==================== SUPPORTING CLASSES ====================

        public class GraphEdge
        {
            public int FromId { get; set; }
            public int ToId { get; set; }
            public int Weight { get; set; }

            public GraphEdge(int fromId, int toId, int weight)
            {
                FromId = fromId;
                ToId = toId;
                Weight = weight;
            }
        }

        public class MSTEdge
        {
            public int FromId { get; set; }
            public int ToId { get; set; }
            public int Weight { get; set; }
            public ServiceRequest FromRequest { get; set; }
            public ServiceRequest ToRequest { get; set; }
        }

        public class GraphStatistics
        {
            public int TotalNodes { get; set; }
            public int TotalEdges { get; set; }
            public int BFSReachableNodes { get; set; }
            public int DFSReachableNodes { get; set; }
            public List<int> BFSTraversal { get; set; }
            public List<int> DFSTraversal { get; set; }
        }

        /// <summary>
        /// Union-Find data structure for Kruskal's MST algorithm
        /// </summary>
        private class UnionFind
        {
            private Dictionary<int, int> parent;
            private Dictionary<int, int> rank;

            public UnionFind(List<int> nodes)
            {
                parent = new Dictionary<int, int>();
                rank = new Dictionary<int, int>();

                foreach (int node in nodes)
                {
                    parent[node] = node;
                    rank[node] = 0;
                }
            }

            public int Find(int x)
            {
                if (parent[x] != x)
                {
                    parent[x] = Find(parent[x]); // Path compression
                }
                return parent[x];
            }

            public void Union(int x, int y)
            {
                int rootX = Find(x);
                int rootY = Find(y);

                if (rootX == rootY)
                    return;

                // Union by rank
                if (rank[rootX] < rank[rootY])
                {
                    parent[rootX] = rootY;
                }
                else if (rank[rootX] > rank[rootY])
                {
                    parent[rootY] = rootX;
                }
                else
                {
                    parent[rootY] = rootX;
                    rank[rootX]++;
                }
            }
        }
    }
}