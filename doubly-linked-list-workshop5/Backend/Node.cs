namespace Backend;

// One element of the list: it stores a value and links to the next and previous nodes
public class Node<T>
{
    public T Data; 
    public Node<T>? Next, Prev;

    // Creates a node with its value and no links yet
    public Node(T data) { Data = data; }
}
