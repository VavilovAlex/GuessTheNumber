using GuessTheNumber.Text;

namespace GuessTheNumber.Pages.GameModes;

public class GameClassic : GameMode
{
    private int _number = 0;
    private int[] _guesses = new int[5];
    private int _tries = 5;

    private int AskForGuess()
    {
        Console.Clear();
        

        for (var i = 0; i < _guesses.Length - _tries; i++)
        {
            Console.SetCursorPosition(HorizontalCenter - 5, VerticalCenter - 2 + i);
            
            var hint = _guesses[i] > _number ? "Too high" : "Too low";
            
            Console.Write($"{_guesses[i]} is {hint}");
        }

        
        Console.SetCursorPosition(HorizontalCenter - 5, VerticalCenter - 5);
        Console.Write("Guess: ");
        

        var input = new TextInput((str) =>
        {
            if (str == "") return true;
            if (!int.TryParse(str, out var val)) return false;
            return val is >= 0 and <= 100;
        });
        
        return int.Parse(input.ReadLine());
    }
    public override void Display()
    {
        _number = new Random().Next(1, 100);

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

        if (won)
        {
            Won();
        } else{
            Lost();    
        }
        
        Thread.Sleep(1000);
        
        App.ChangePage(new MainPage());
    }
}