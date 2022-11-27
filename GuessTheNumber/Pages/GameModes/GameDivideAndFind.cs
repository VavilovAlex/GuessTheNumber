using System.Text;
using GuessTheNumber.Text;
using ConsoleColor = GuessTheNumber.Utils.ConsoleColor;

namespace GuessTheNumber.Pages.GameModes;

public class GameDivideAndFind : GameMode
{
    private readonly int _number;
    private int _left;
    private int _right = 100;
    private int _position;

    private readonly int _startX;
    private readonly int _startY;

    private int _tries = 5;

    private string[] _lines;

    public GameDivideAndFind()
    {
        _number = new Random().Next(0, 100);
        CutNumbersIntoLines(0, 100, 4);

        _startX = HorizontalCenter - _lines![0].Length / 2;
        _startY = VerticalCenter - 4;
    }

    private void CutNumbersIntoLines(int from, int to, int lines)
    {
        var linesLength = (to - from) / lines;

        _lines = new string[lines];
        var longestLine = 0;
        for (var l = 0; l < lines; l++)
        {
            var line = new StringBuilder();
            var offset = linesLength * l;
            for (var i = offset; i < offset + linesLength; i++)
            {
                if (i < 10)
                {
                    line.Append('0');
                }

                line.Append(i).Append(' ');
            }

            _lines[l] = line.ToString();
            if (_lines[l].Length > longestLine)
            {
                longestLine = _lines[l].Length;
            }
        }
    }

    private void IntroAnimation()
    {
        for (var i = 0; i < 4; i++)
        {
            var topOffset = 2 - i * 2;
            var text = new AnimatedText(_lines[3 - i], HorizontalCenter, VerticalCenter + topOffset, Alignment.Center);
            text.FallingText(10);
        }
    }

    private void MoveSelection(int x, int y)
    {
        ConsoleColor.SetSelected();

        Console.SetCursorPosition(_startX + x * 3, _startY + y * 2);
        var num = _position.ToString().PadLeft(2, '0');

        Console.Write(num);

        Thread.Sleep(200 - (_right - _left));

        ConsoleColor.SetPrimary();

        Console.SetCursorPosition(_startX + x * 3, _startY + y * 2);
        Console.Write(num);
    }

    private bool HandleClick()
    {
        if (!Console.KeyAvailable) return false;
        var key = Console.ReadKey(true).Key;
        switch (key)
        {
            case ConsoleKey.Escape:
                return true;
            case ConsoleKey.Enter or ConsoleKey.Spacebar:
            {
                _tries--;
                if (_number == _position)
                {
                    Won();
                    return true;
                }

                if (_tries <= 0)
                {
                    Lost();
                    return true;
                }

                ChangeLimit();
                break;
            }
        }

        return false;
    }

    public override void Display()
    {
        Console.Clear();

        IntroAnimation();
        
        Console.SetCursorPosition(_startX, _startY - 3);
        
        ConsoleColor.SetDisabled();
        Console.Write("Press Enter or SpaceBar to make a guess.");

        var endX = _startX + _lines![0].Length;
        
        while (true)
        {
            Console.SetCursorPosition(endX - 3, _startY - 3);
            
            Console.Write($"{6-_tries}/5");
            
            var x = _position % 25;
            var y = _position / 25;

            MoveSelection(x, y);

            if (HandleClick()) break;

            _position++;

            if (_position >= _right)
            {
                _position = _left;
            }
        }

        App.ChangePage(new MainPage());
    }

    private void RedrawDisabled(bool left)
    {
        var from = left ? _left : _position;
        var to = left ? _position : _right;

        for (var i = from; i < to; i++)
        {
            var x = i % 25;
            var y = i / 25;

            Console.SetCursorPosition(_startX + x * 3, _startY + y * 2);
            var num = i.ToString().PadLeft(2, '0');

            Console.Write(num);
        }
    }

    private void ChangeLimit()
    {
        ConsoleColor.SetDisabled();

        if (_number < _position)
        {
            RedrawDisabled(false);

            _right = _position;
        }
        else
        {
            RedrawDisabled(true);

            _left = _position;
        }

        ConsoleColor.SetPrimary();
    }
}