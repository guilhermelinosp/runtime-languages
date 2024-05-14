using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class Node
{
    public int ID { get; set; }
    public Node[] Children { get; set; } = Array.Empty<Node>(); // Initialize Children as an empty array
}

public class Program
{
    // Generate a tree with the specified number of nodes
    public static Node GenerateTree(int nodeCount)
    {
        Node root = new Node { ID = 1 };
        Random rand = new Random();
        GenerateNode(root, 1, (int)Math.Log2(nodeCount), rand);
        return root;
    }

    // Recursively generate tree nodes
    public static void GenerateNode(Node parent, int depth, int remainingNodes, Random rand)
    {
        if (remainingNodes <= 0 || depth <= 0)
            return;

        int childrenCount = 2;
        if (remainingNodes < 2)
            childrenCount = remainingNodes;

        parent.Children = new Node[childrenCount];
        for (int i = 0; i < childrenCount; i++)
        {
            Node child = new Node { ID = rand.Next(int.MinValue, int.MaxValue) };
            parent.Children[i] = child;
            GenerateNode(child, depth - 1, remainingNodes / childrenCount, rand);
            remainingNodes -= remainingNodes / childrenCount;
        }
    }

    // Measure memory usage
    public static long MeasureMemory()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return GC.GetTotalMemory(false);
        }
        else
        {
            return GC.GetTotalMemory(true);
        }
    }

    public static void Main()
    {
        // Measure initial memory usage
        long initialMemory = MeasureMemory();

        // Measure execution time
        Stopwatch stopwatch = Stopwatch.StartNew();

        int nodeCount = 1000000;
        Node root = GenerateTree(nodeCount);

        // Measure execution time
        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;

        // Measure final memory usage
        long finalMemory = MeasureMemory();

        // Calculate memory difference
        long memoryUsage = finalMemory - initialMemory;

        // Print results
        Console.WriteLine($"Execution time: {elapsed.TotalMilliseconds} milliseconds");
        Console.WriteLine($"Memory usage: {memoryUsage} bytes");
    }
}
