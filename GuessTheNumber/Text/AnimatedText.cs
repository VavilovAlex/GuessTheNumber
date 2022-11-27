namespace GuessTheNumber.Text;

public enum Alignment
{
    Left,
    Center,
    Right
}

public class AnimatedText
{
    private readonly string _text;
    private readonly int _x;
    private readonly int _y;

    public AnimatedText(string text, int x, int y, Alignment alignment = Alignment.Left)
    {
        _text = text;

        _x = x;
        _y = y;

        //   Te|xt    Center
        // Text|      Right
        //     |Text  Left

        switch (alignment)
        {
            case Alignment.Center:
                var messageHalfLength = _text.Length / 2;
                _x -= messageHalfLength;
                break;
            case Alignment.Right:
                _x = x - text.Length;
                break;
        }
    }

    public void SlowTyping(int delay = 50)
    {
        OnAnimationStarted();

        Console.SetCursorPosition(_x, _y);

        foreach (var t in _text)
        {
            if (IsAnimationSkipped()) break;
                
            Console.Write(t);
            
            Thread.Sleep(delay);
        }
        
        OnAnimationFinished();
    }

    public void RandomTyping(int delay = 50)
    {
        OnAnimationStarted();

        var text = new char[_text.Length];

        for (var i = 0; i < _text.Length; i++)
        {
            if (IsAnimationSkipped()) break;

            var index = 0;

            do
            {
                index = new Random().Next(0, _text.Length);
            } while (text[index] != '\0');

            text[index] = _text[index];

            //Replace '\0' with ' ' if not last
            var withSpaces = text
                .Select((c, i) => c == '\0' && i != _text.Length - 1 ? ' ' : c)
                .ToArray();

            Console.SetCursorPosition(_x, _y);
            Console.Write(withSpaces);
            
            Thread.Sleep(delay);
        }
        
        OnAnimationFinished();
    }

    public void FallingText(int delay = 100)
    {
        OnAnimationStarted();
        
        const int height = 5;
        var emptyLine = new string(' ', _text.Length);

        for (var frame = 0; frame < _text.Length + height; frame++)
        {
            // Clear the lines above the text
            for (var j = 0; j < height; j++)
            {
                Console.SetCursorPosition(_x, _y - j - 1);
                Console.Write(emptyLine);
            }

            if (IsAnimationSkipped()) break;

            for (var charIndex = 0; charIndex < _text.Length; charIndex++)
            {
                var offset = charIndex - frame + height;

                switch (offset)
                {
                    case < 0: // Stop characters from falling infinitely
                        offset = 0;
                        break;
                    case > height: // Dont print characters that are too high
                        continue;
                }

                var x = _x + charIndex;
                var y = _y - offset;

                if (y < 0) continue;

                Console.SetCursorPosition(x, y);
                Console.Write(_text[charIndex]);
            }

            Thread.Sleep(delay);
        }
        
        OnAnimationFinished();
    }

    public void ImFeelingLucky()
    {
        var rand = new Random().Next(0, 3);
        switch (rand)
        {
            case 0:
                SlowTyping();
                break;
            case 1:
                RandomTyping();
                break;
            case 2:
                FallingText();
                break;
        }
    }

    private void OnAnimationStarted()
    {
        var w = Console.WindowWidth;
        var h = Console.WindowHeight;

        var x = w - 40;
        var y = h - 2;
        
        Console.SetCursorPosition(x, y);
        
        Console.Write("Press any key to skip animation...");
    }

    private void OnAnimationFinished()
    {
        var w = Console.WindowWidth;
        var h = Console.WindowHeight;

        var x = w - 40;
        var y = h - 2;
        
        Console.SetCursorPosition(x, y);
        
        Console.Write("                                  ");
    }

    private bool IsAnimationSkipped()
    {
        if (!Console.KeyAvailable) return false;
        Console.SetCursorPosition(_x, _y);
        Console.Write(_text);
        Console.ReadKey(true);
        return true;
    }
}