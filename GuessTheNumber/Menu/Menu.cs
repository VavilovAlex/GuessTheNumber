namespace GuessTheNumber.Menu;
using ConsoleColor = GuessTheNumber.Utils.ConsoleColor;

public class Menu
{
    private List<MenuItem> _items;
    private int _selectedIndex = 0;

    private int _x = 0;
    private int _y = 0;

    public Menu(List<MenuItem> items, int x, int y)
    {
        _items = items;
        _selectedIndex = 0;

        _x = x;
        _y = y;
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
    
    public void Enter()
    {
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
}