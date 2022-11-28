using GuessTheNumber.Menu;
using GuessTheNumber.Pages.GameModes;
using GuessTheNumber.Text;

namespace GuessTheNumber.Pages;

public class MainPage : Page
{
    public override void Display()
    {
        Console.Clear();

        var x = HorizontalCenter - 5;
        var y = VerticalCenter - 5;

        var greeting = new AnimatedText($"Hello, {App.User}!", x, y);
        greeting.SlowTyping();

        y += 2;

        var items = new List<MenuItem>
        {
            new("Play \"Classic\"", () => App.ChangePage(new GameClassic())),
            new("Play \"Divide and find\"", () => App.ChangePage(new GameDivideAndFind())),
            new("Change name", () => App.ChangePage(new StartPage())),
            new("Exit", Bye)
        };

        var menu = new ConsoleMenu(items, x, y);

        menu.Enter();
    }

    private void Bye()
    {
        Console.Clear();
        
        var x = HorizontalCenter;
        var y = VerticalCenter;

        var message = new AnimatedText($"Goodbye, {App.User}!", x, y, Alignment.Center);

        message.RandomTyping();
        
        Thread.Sleep(500);
        
        Environment.Exit(0);
    }
}