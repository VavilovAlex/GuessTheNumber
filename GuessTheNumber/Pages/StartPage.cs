using System.Text.RegularExpressions;
using GuessTheNumber.Text;

namespace GuessTheNumber.Pages;

public class StartPage : Page
{
    private string ReadName()
    {
        Console.Clear();
        Console.SetCursorPosition(HorizontalCenter - 5, VerticalCenter - 5);
        Console.Write("Name: ");

        var input = new TextInput(FilterMode.RegexString, new Regex("^[A-Za-z0-9]{0,20}$"));
        return input.ReadLine();
    }

    public override void Display()
    {
        var name = ReadName();

        var user = new User(name);

        App.User = user;

        App.ChangePage(new MainPage());
    }
}