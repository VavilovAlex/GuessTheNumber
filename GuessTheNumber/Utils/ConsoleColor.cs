namespace GuessTheNumber.Utils;

public static class ConsoleColor
{
    public static void SetPrimary()
    {
        Console.BackgroundColor = System.ConsoleColor.Black;
        Console.ForegroundColor = System.ConsoleColor.Gray;
    }

    public static void SetSelected()
    {
        Console.BackgroundColor = System.ConsoleColor.White;
        Console.ForegroundColor = System.ConsoleColor.Black;
    }

    public static void SetDisabled()
    {
        Console.BackgroundColor = System.ConsoleColor.Black;
        Console.ForegroundColor = System.ConsoleColor.DarkGray;
    }
}