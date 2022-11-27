namespace GuessTheNumber.Pages;

public abstract class Page
{
    public App App = null!;
    protected static int HorizontalCenter { get; } = Console.WindowWidth / 2;
    protected static int VerticalCenter { get; } = Console.WindowHeight / 2;
    public abstract void Display();
}