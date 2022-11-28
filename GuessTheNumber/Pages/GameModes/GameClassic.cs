using GuessTheNumber.Text;
using ConsoleColor = GuessTheNumber.Utils.ConsoleColor;

namespace GuessTheNumber.Pages.GameModes;

public class GameClassic : GameMode
{
    private int _number = 0;
    private readonly int[] _guesses = new int[5];
    private int _tries = 5;

    private const int From = 1;
    private const int To = 100;

    public override void Display()
    {
        _number = new Random().Next(From, To + 1);

        var won = false;

        while (_tries > 0)
        {
            var guess = AskForGuess();
            _guesses[^_tries] = guess;

            if (guess == _number)
            {
                won = true;
                break;
            }

            _tries--;
        }

        if (won) Won();
        else Lost();

        App.ChangePage(new MainPage());
    }

    private int AskForGuess()
    {
        Console.Clear();


        for (var i = 0; i < _guesses.Length - _tries; i++)
        {
            Console.SetCursorPosition(HorizontalCenter - 5, VerticalCenter - 2 + i);

            var hint = _guesses[i] > _number ? "Too high" : "Too low";

            Console.Write($"{_guesses[i]} is {hint}");
        }

        ConsoleColor.SetDisabled();

        Console.SetCursorPosition(5, 2);
        Console.Write($"Try to guess the number between {From} and {To}");

        Console.SetCursorPosition(5, 3);
        Console.Write("Tries left: ");

        ConsoleColor.SetPrimary();
        Console.Write($"{_tries}/5");

        Console.SetCursorPosition(HorizontalCenter - 5, VerticalCenter - 5);
        Console.Write($"Your guess: ");


        var input = new TextInput((str) =>
        {
            if (str == "") return true;
            if (!int.TryParse(str, out var val)) return false;
            return val >= From && val <= To;
        });

        return int.Parse(input.ReadLine());
    }
}