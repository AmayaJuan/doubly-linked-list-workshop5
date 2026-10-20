namespace Backend;

// Helper methods that calculate statistics of a list (mode and chart)
public static class Stadistics
{
    // Returns each different value with the number of times it appears.
    // The list is sorted, so repeated items are always together
    static List<(T Data, int Count)> Frequencies<T>(DoublyLinkedList<T> list) where T : IComparable<T>
    {
        var result = new List<(T Data, int Count)>();
        for (var n = list.Head; n != null; n = n.Next)
        {
            if (result.Count > 0 && result[^1].Data.CompareTo(n.Data) == 0)
                result[^1] = (n.Data, result[^1].Count + 1);
            else
                result.Add((n.Data, 1));
        }
        return result;
    }

    // Returns the mode(s): the value(s) that appear the most times
    public static List<T> Nodes<T>(DoublyLinkedList<T> list) where T : IComparable<T>
    {
        var freq = Frequencies(list);
        if (freq.Count == 0)
            return new List<T>();

        int max = freq.Max(x => x.Count);
        return freq.Where(x => x.Count == max).Select(x => x.Data).ToList();
    }

    // Builds the sentence that describes the mode(s), e.g. "The mode is 6." or "The modes are 3 and 6."
    public static string NodeText<T>(DoublyLinkedList<T> list) where T : IComparable<T>
    {
        var nodes = Nodes(list);
        if (nodes.Count == 0)
            return "The list is empty";
        if (nodes.Count == 1)
            return $"The node is {nodes[0]}";

        return $"The modes are {string.Join(", ", nodes.Take(nodes.Count - 1))} and {nodes[^1]}.";
    } 

    // Builds the chart text: a title and one line per value with one * per occurrence.
    // The values are padded to the same width so the asterisks line up
    public static string Chart<T>(DoublyLinkedList<T> list) where T : IComparable<T>
    {
        var freq = Frequencies(list);
        int width = freq.Count == 0 ? 0 : freq.Max(x => x.Data!.ToString()!.Length);
        var lines = freq.Select(x => $"{x.Data!.ToString()!.PadRight(width)} {new string('*', x.Count)}");
        return string.Join("\n", lines);
    }
}