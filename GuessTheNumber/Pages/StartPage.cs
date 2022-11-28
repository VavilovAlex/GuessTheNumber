using System.Text.RegularExpressions;
using GuessTheNumber.Text;

namespace GuessTheNumber.Pages;

public class StartPage : Page
{
    public override void Display()
    {
        var name = ReadName();

        var user = new User(name);

        App.User = user;

        App.ChangePage(new MainPage());
    }
    
    private static string ReadName()
    {
        Console.Clear();
        Console.SetCursorPosition(HorizontalCenter - 5, VerticalCenter - 5);
        Console.Write("Your name: ");

        var input = new TextInput(FilterMode.RegexString, new Regex("^[A-Za-z0-9]{0,20}$"));
        return input.ReadLine();
    }
}