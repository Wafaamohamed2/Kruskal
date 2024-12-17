internal class Program
{
    private static void Main(string[] args)
    {
        int vertices = 5;
        List<Edge> edges = new List<Edge>
        {
            new Edge { Source = 0, Destination = 1, Weight = 10 },
            new Edge { Source = 0, Destination = 2, Weight = 6 },
            new Edge { Source = 0, Destination = 3, Weight = 5 },
            new Edge { Source = 1, Destination = 3, Weight = 15 },
            new Edge { Source = 2, Destination = 3, Weight = 4 }
        };

        List<Edge> mst = KruskalMST.Kruskal(vertices, edges);

        Console.WriteLine("Edges in the Minimum Spanning Tree:");
        foreach (var edge in mst)
        {
            Console.WriteLine($"{edge.Source} -- {edge.Destination} == {edge.Weight}");
        }
    }
    class Edge : IComparable<Edge>
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Weight { get; set; }

        public int CompareTo(Edge other)
        {
            return this.Weight.CompareTo(other.Weight);
        }
    }

    class UnionFind
    {
        private int[] parent;
        private int[] rank;

        public UnionFind(int size)
        {
            parent = new int[size];
            rank = new int[size];
            for (int i = 0; i < size; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        public int Find(int node)
        {
            if (parent[node] != node)
            {
                parent[node] = Find(parent[node]);
            }
            return parent[node];
        }
        public void Union(int u, int v)
        {
            int rootU = Find(u);
            int rootV = Find(v);

            if (rootU != rootV)
            {
                if (rank[rootU] > rank[rootV])
                {
                    parent[rootV] = rootU;
                }
                else if (rank[rootU] < rank[rootV])
                {
                    parent[rootU] = rootV;
                }
                else
                {
                    parent[rootV] = rootU;
                    rank[rootU]++;
                }
            }
        }
    }
    
            class KruskalMST
            {
              public static List<Edge> Kruskal(int vertices, List<Edge> edges)
              {
                edges.Sort();
                UnionFind unionFind = new UnionFind(vertices);
                List<Edge> mst = new List<Edge>();

                foreach (var edge in edges)
                {
                    if (unionFind.Find(edge.Source) != unionFind.Find(edge.Destination))
                    {
                        unionFind.Union(edge.Source, edge.Destination);
                        mst.Add(edge);

                        if (mst.Count == vertices - 1)
                        {
                            break;
                        }
                    }
                }

                return mst;
              }
            }
    
}