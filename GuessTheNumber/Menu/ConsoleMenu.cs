namespace GuessTheNumber.Menu;
using ConsoleColor = GuessTheNumber.Utils.ConsoleColor;

public class ConsoleMenu
{
    private readonly List<MenuItem> _items;
    private int _selectedIndex;

    private readonly int _x;
    private readonly int _y;

    public ConsoleMenu(List<MenuItem> items, int x, int y)
    {
        _items = items;
        _selectedIndex = 0;

        _x = x;
        _y = y;
    }
    
    public void Enter()
    {
        Hint();
        Draw();
        while (true)
        {
            var ch = Console.ReadKey(true).Key;
            
            switch (ch)
            {
                case ConsoleKey.W or ConsoleKey.UpArrow:
                    Prev();
                    break;
                case ConsoleKey.S or ConsoleKey.DownArrow:
                    Next();
                    break;
                case ConsoleKey.Enter or ConsoleKey.D or ConsoleKey.RightArrow:
                    _items[_selectedIndex].OnSelected();
                    return;
            }

            Draw();
        }
    }

    private void Draw()
    {
        for (var i = 0; i < _items.Count; i++)
        {
            var item = _items[i];

            if (_selectedIndex == i)
            {
                ConsoleColor.SetSelected();
            }
            else
            {
                ConsoleColor.SetPrimary();
            }

            Console.SetCursorPosition(_x, _y + i);
            Console.WriteLine(item.Title);
        }
        ConsoleColor.SetPrimary();
    }

    private void Prev()
    {
        if (_selectedIndex > 0)
        {
            _selectedIndex--;
        }
    }

    private void Next()
    {
        if (_selectedIndex < _items.Count - 1)
        {
            _selectedIndex++;
        }
    }
    
    private static void Hint()
    {
        ConsoleColor.SetDisabled();
        Console.SetCursorPosition(5,2);
        Console.Write("Use W/S or Up/Down arrows to navigate");
        Console.SetCursorPosition(5,3);
        Console.Write("'Enter' | 'Right arrow' | 'D' to select");
        ConsoleColor.SetPrimary();
    }
}