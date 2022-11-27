using GuessTheNumber.Text;

namespace GuessTheNumber.Pages.GameModes;

public abstract class GameMode : Page
{
    protected void Won()
    {
        Console.Clear();
        
        var message = new AnimatedText($"Congratulations, {App.User}! You WON!", 
            HorizontalCenter, VerticalCenter - 5, Alignment.Center);
        
        message.ImFeelingLucky();

        Console.ReadKey();
    }

    protected void Lost()
    {
        Console.Clear();
        
        //Same "Congratulations" as in the winning situation to confuse the player
        var message = new AnimatedText($"Congratulations, {App.User}! You LOST!", 
            HorizontalCenter, VerticalCenter - 5, Alignment.Center);
        
        message.ImFeelingLucky();
        
        Console.ReadKey();
    }
}