using Backend;

// Workshop #5 - Linked Lists
// Shows the menu in a loop and calls the list methods until the user chooses 0
var list = new DoublyLinkedList<string>();
int option;

do
{
    Console.WriteLine("\n==============================\n" +
                      "   DOUBLY LINKED LIST - MENU\n" +
                      "==============================\n" +
                      "1. Add\n2. Show forward\n3. Show backward\n" +
                      "4. Sort descending\n5. Show mode(s)\n6. Show chart\n" +
                      "7. Exists\n8. Remove one occurrence\n9. Remove all occurrences\n" +
                      "0. Exit\n" +
                      "------------------------------");
    Console.Write("Option: ");
    int.TryParse(Console.ReadLine(), out option);

    switch(option)
    {
        case 1:
            list.Add(Ask());
            break;
        case 2:
            Console.WriteLine(list.Forward());
            break;
        case 3:
            Console.WriteLine(list.Backward());
            break;
        case 4:
            list.SortDescending();
            Console.WriteLine("List sorted descending.");
            break;
        case 5:
            Console.WriteLine(Stadistics.ModeText(list));
            break;
        case 6:
            Console.WriteLine(Stadistics.Chart(list));
            break;
        case 7:
            var x = Ask();
            Console.WriteLine(list.Exists(x) ? $"{x} exists." : $"{x} does not exist.");
            break;
        case 8:
            var y = Ask();
            Console.WriteLine(list.RemoveOne(y) ? "Removed." : $"{y} does not exist.");
            break;
        case 9:
            var z = Ask();
            int removed = list.RemoveAll(z);
            Console.WriteLine(removed > 0 ? $"{removed} occurrence(s) removed." : $"{z} does not exist.");
            break;
        case 0:
            Console.WriteLine("Saliendo...");
            break;
        default:
            Console.WriteLine("Opcion invalida!");
            break;
    }

} while (option != 0);

// Asks the user for a value and returns what was typed
static string Ask()
{
    Console.Write("Value: ");
    return Console.ReadLine() ?? "";
}