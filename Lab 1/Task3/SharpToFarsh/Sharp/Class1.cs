namespace Sharp;

// This class represents a directed
// graph using adjacency list
// representation
public class Graph
{
// No. of vertices    
    private int _V;

//Adjacency Lists
    LinkedList<int>[] _adj;

    public Graph(int V)
    {
        _adj = new LinkedList<int>[V];
        for (int i = 0; i < _adj.Length; i++)
        {
            _adj[i] = new LinkedList<int>();
        }

        _V = V;
    }

// Function to add an edge into the graph
    public void AddEdge(int v, int w)
    {
        _adj[v].AddLast(w);
    }

// Prints BFS traversal from a given source s
    public void BFS(int s)
    {
        // Mark all the vertices as not
        // visited(By default set as false)
        bool[] visited = new bool[_V];
        for (int i = 0; i < _V; i++)
            visited[i] = false;

        // Create a queue for BFS
        LinkedList<int> queue = new LinkedList<int>();

        // Mark the current node as
        // visited and enqueue it
        visited[s] = true;
        queue.AddLast(s);

        while (queue.Any())
        {
            // Dequeue a vertex from queue
            // and print it
            s = queue.First();
            Console.Write(s + " ");
            queue.RemoveFirst();

            // Get all adjacent vertices of the
            // dequeued vertex s. If a adjacent
            // has not been visited, then mark it
            // visited and enqueue it
            LinkedList<int> list = _adj[s];

            foreach (var val in list)
            {
                if (!visited[val])
                {
                    visited[val] = true;
                    queue.AddLast(val);
                }
            }
        }
    }

// A function used by DFS
    public void DFSUtil(int v, bool[] visited)
    {
        // Mark the current node as visited
        // and print it
        visited[v] = true;
        Console.Write(v + " ");

        // Recur for all the vertices
        // adjacent to this vertex
        LinkedList<int> vList = _adj[v];
        foreach (var n in vList)
        {
            if (!visited[n])
                DFSUtil(n, visited);
        }
    }

// The function to do DFS traversal.
// It uses recursive DFSUtil()
    public void DFS(int v)
    {
        // Mark all the vertices as not visited
        // (set as false by default in c#)
        bool[] visited = new bool[_V];

        // Call the recursive helper function
        // to print DFS traversal
        DFSUtil(v, visited);
    }
}

// This code is contributed by anv89