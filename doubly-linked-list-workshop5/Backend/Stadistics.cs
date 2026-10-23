namespace Backend;

// Helper methods that calculate statistics of a list (mode and chart)
public static class Stadistics
{
    // Counts how many nodes in a row, starting at "start", have the same value.
    // The list is sorted, so repeated items are always together
    static int CountRun<T>(Node<T> start) where T : IComparable<T>
    {
        int count = 0;
        Node<T>? n = start;
        while (n != null && n.Data.CompareTo(start.Data) == 0)
        {
            count++;
            n = n.Next;
        }
        return count;
    }

    // Returns the mode(s): the value(s) that appear the most times
    public static List<T> Nodes<T>(DoublyLinkedList<T> list) where T : IComparable<T>
    {
        int max = 0;
        var n = list.Head;
        while (n != null) // First pass: find the highest number of repetitions
        {
            int count = CountRun(n);
            if (count > max)
                max = count;
            for (int i = 0; i < count; i++)
                n = n!.Next;
        }

        var modes = new List<T>();
        n = list.Head;
        while (n != null) // Second pass: keep every value that reaches that number
        {
            int count = CountRun(n);
            if (count == max)
                modes.Add(n.Data);
            for (int i = 0; i < count; i++)
                n = n!.Next;
        }
        return modes;
    }

    // Builds the sentence that describes the mode(s), e.g. "The mode is 6." or "The modes are 3 and 6."
    public static string ModeText<T>(DoublyLinkedList<T> list) where T : IComparable<T>
    {
        var modes = Nodes(list);
        if (modes.Count == 0)
            return "The list is empty.";
        if (modes.Count == 1)
            return $"The mode is {modes[0]}.";

        return $"The modes are {string.Join(", ", modes.Take(modes.Count - 1))} and {modes[^1]}.";
    }

    // Builds the chart text: one line per value with one * per occurrence.
    // The values are padded to the same width so the asterisks line up
    public static string Chart<T>(DoublyLinkedList<T> list) where T : IComparable<T>
    {
        int width = 0;
        for (var n = list.Head; n != null; n = n.Next) // Find the length of the longest value
            if (n.Data!.ToString()!.Length > width)
                width = n.Data.ToString()!.Length;

        var lines = new List<string>();
        var current = list.Head;
        while (current != null) // One line for each different value
        {
            int count = CountRun(current);
            lines.Add($"{current.Data!.ToString()!.PadRight(width)} {new string('*', count)}");
            for (int i = 0; i < count; i++)
                current = current!.Next;
        }
        return string.Join("\n", lines);
    }
}