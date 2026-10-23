namespace Backend;

// Generic doubly linked list that always keeps its elements sorted
public class DoublyLinkedList<T> where T : IComparable<T>
{
    Node<T>? _head, _tail;
    int direction = 1; // 1 = ascending, -1 = descending

    // Gives access to the first node so other classes can walk the list
    public Node<T>? Head => _head;

    // Adds a value in the correct position, according to the current order of the list
    public void Add(T data)
    {
        var node = new Node<T>(data);
        var current = _head;
        while (current != null && direction * data.CompareTo(current.Data) >= 0)
            current = current.Next;

        if (current == null) // goes at the end 
        {
            node.Prev = _tail;
            if (_tail != null)
                _tail.Next = node;
            else
                _head = node;

            _tail = node;
        }
        else // goes before "current"
        {
            node.Next = current;
            node.Prev = current.Prev;

            if (current.Prev != null)
                current.Prev.Next = node;
            else
                _head = node;

            current.Prev = node;
        }
    }

    // Returns the values from the first node to the last, separated by commas
    public string Forward()
    {
        var items = new List<string>();
        for (var n = _head; n != null; n = n.Next)
            items.Add(n.Data.ToString()!);

        return string.Join(", ", items);
    }

    // Returns the values from the last node to the first, separated by commas
    public string Backward()
    {
        var items = new List<string>();
        for (var n = _tail; n != null; n = n.Prev)
            items.Add(n.Data.ToString()!);

        return string.Join(", ", items);
    }

    // Reverses the links of every node so the list ends up from highest to lowest
    public void SortDescending()
    {
        for (var n = _head; n != null; n = n.Prev) // n.Prev is the old Next
        {
            var temp = n.Next;
            n.Next = n.Prev;
            n.Prev = temp;
        }

        var aux = _head;
        _head = _tail;
        _tail = aux;
        direction = -1;
    }

    // Returns the first node that has the value, or null if it is not in the list
    Node<T> Find(T data)
    {
        for (var n = _head; n != null; n = n.Next)
            if (n.Data.CompareTo(data) == 0) return n;
        return null!;
    }

    // Tells if at least one node has the value
    public bool Exists(T data) => Find(data) != null;

    // Takes a node out of the list by reconnecting its neighbors
    void Unlink(Node<T> n)
    {
        if (n.Prev != null)
            n.Prev.Next = n.Next;
        else
            _head = n.Next;

        if (n.Next != null)
            n.Next.Prev = n.Prev;
        else
            _tail = n.Prev;
    }

    // Removes the first occurrence of the value; returns false if it was not found
    public bool RemoveOne(T data)
    {
        var n = Find(data);
        if (n == null) return false;

        Unlink(n);
        return true;
    }

    // Removes every occurrence of the value and returns how many were removed
    public int RemoveAll(T data)
    {
        int count = 0;
        while (RemoveOne(data))
            count++;
        return count;
    }
}