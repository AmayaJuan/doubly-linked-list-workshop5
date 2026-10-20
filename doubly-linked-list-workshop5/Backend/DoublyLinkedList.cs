namespace Backend;

public class DoublyLinkedList<T> where T : IComparable<T>
{
    Node<T>? _head, _tail;
    int direction = 1; // 1 = ascending, -1 = descending

    public Node<T>? Head => _head;

    // Inserts keeping the current order of the list
    public void Add(T data)
    {
        var node = new Node<T>(data);
        var current = _head;
        while(current != null && direction * data.CompareTo(current.Data) >= 0)
            current = current.Next;

        if(current == null) // goes at the end 
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

    public string Forward()
    {
        var items = new List<string>();
        for (var n = _head; n != null; n = n.Next)
            items.Add(n.Data.ToString()!);

        return string.Join(", ", items);
    }

    public string Backward() 
    {
        var items = new List<string>();
        for (var n = _tail; n != null; n = n.Prev)
            items.Add(n.Data.ToString()!);

        return string.Join(", ", items);
    }

    // Reverses the links of the list (it ends up from highest to lowest)
    public void SortDescending()
    {
        for (var n = _head; n != null; n = n.Prev) // n.Prev is the old Next
            (n.Next, n.Prev) = (n.Prev, n.Next);
        (_head, _tail) = (_tail, _head);
        direction = -1;
    }

    Node<T> Find(T data)
    {
        for (var n = _head; n != null; n = n.Next)
            if (n.Data.CompareTo(data) == 0) return n;
        return null!;
    }

    public bool Exists(T data) => Find(data) != null;

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

    public bool RemoveOne(T data) 
    { 
        var n = Find(data);
        if(n == null) return false;

        Unlink(n);
        return true;
    }

    public int RemoveAll(T data)
    {
        int count = 0;
        while(RemoveOne(data))
            count++;
        return count;
    }
}
