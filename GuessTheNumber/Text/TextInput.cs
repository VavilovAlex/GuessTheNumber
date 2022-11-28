using System.Text;
using System.Text.RegularExpressions;

namespace GuessTheNumber.Text;

public enum FilterMode
{
    None, BlackList, WhiteList, RegexChar, RegexString, Function
}

public delegate bool Check(string str);
public class TextInput
{
    private char[]? _whitelist;
    private char[]? _blacklist;
    private Regex? _regex = null;
    private Check? _check = null;
    private FilterMode _mode;
    
    private StringBuilder _string = new ();
    private int _position;
    public TextInput(FilterMode mode = FilterMode.None)
    {
        _mode = mode;
    }
    
    public TextInput(FilterMode mode, char[] chars)
    {
        _mode = mode;
        if (mode == FilterMode.BlackList)
        {
            _blacklist = chars;
        }
        else
        {
            _whitelist = chars;
        }
    }

    public TextInput(Check check)
    {
        _mode = FilterMode.Function;
        _check = check;
    }
    
    public TextInput(FilterMode mode, Regex regex)
    {
        _mode = mode;
        _regex = regex;
    }
    
    public string ReadLine()
    {

        _string = new StringBuilder();
        _position = 0;
        while (true)
        {
            var input = Console.ReadKey(true);

            switch (input.Key)
            {
                case ConsoleKey.Backspace:
                    HandleBackSpace();
                    break;
                case ConsoleKey.Enter:
                    if(_string.Length > 0)
                    {
                        return _string.ToString();
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    HandleLeft();
                    break;
                case ConsoleKey.RightArrow:
                    HandleRight();
                    break;
                case ConsoleKey.Tab:
                    break;
                default:
                    HandleChar(input.KeyChar);
                    break;
            }
        }
    }

    private void HandleBackSpace()
    {
        if (_position <= 0 || !CanRemove()) return;
        
        _string.Remove(_position - 1, 1);
        
        Console.Write("\b \b");
        _position--;
        
        RewriteAfterRemoval();
    }

    private void HandleLeft()
    {
        if (_position <= 0) return;
        
        _position--;
        Console.Write("\b");
    }
    private void HandleRight()
    {
        if (_position >= _string.Length) return;
        
        _position++;
        Console.Write(_string[_position - 1]);
    }

    private void RewriteAfterRemoval()
    {
        if (_position == _string.Length) return;
        
        // Initial string abc1efg
        // 1 is removed
        // After removing a character, there is a gap in the string.
        // abcd| efg
        // We need to write the rest of the string 1 character to the left.
        // abcdefg|g
        for (var i = _position; i < _string.Length; i++)
        {
            Console.Write(_string[i]);
        }
        
        // Now we need to remove last character.
        // abcdefg|
        Console.Write(" ");
        
        // And move the cursor back to the correct position.
        // abcd|efg
        for (var i = _position; i <= _string.Length; i++)
        {
            Console.Write("\b");
        }
    }

    private void RewriteAfterInsert()
    {
        if (_position == _string.Length) return;
        
        //Initial string qwe|rty
        //After insertion, the string is qwe1|ty but should be qwe1|rty
        //We need to write the rest of the string 1 character to the right.
        //qwer1rty|
        for (var i = _position; i < _string.Length; i++)
        {
            Console.Write(_string[i]);
        }
        //And move cursor back to the correct position.
        //qwer1|ty
        for (var i = _position; i < _string.Length - 1; i++)
        {
            Console.Write("\b");
        }
    }
    private bool IsAllowed(char c)
    {
        var str = _string.ToString().Insert(_position, c.ToString());  

        switch (_mode)
        {
            case FilterMode.None:
                return true;
            case FilterMode.BlackList:
                return !_blacklist!.Contains(c);
            case FilterMode.WhiteList:
                return _whitelist!.Contains(c);
            case FilterMode.RegexChar:
                return _regex!.IsMatch(c.ToString());
            case FilterMode.RegexString:
                return _regex!.IsMatch(str);
            case FilterMode.Function:
                return _check!(str);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private bool CanRemove()
    {
        if(_string.Length == 1)
        {
            return true;
        }

        var str = _string.ToString();
        
        if (_position != 0)
        {
            str = str.Remove(_position - 1, 1);
        }

        return _mode switch
        {
            FilterMode.RegexString => _regex!.IsMatch(str),
            FilterMode.Function => _check!(str),
            _ => true
        };
    }
    private void HandleChar(char c)
    {
        if (c == '\0' || !IsAllowed(c)) return;
        
        //If we are at the end of the string, we can just append the character.
        if(_position == _string.Length)
        {
            _string.Append(c);
            Console.Write(c);
        }
        else //Otherwise we need to insert the character.
        {
            _string.Insert(_position, c);
            RewriteAfterInsert();
        }
        _position++;
    }
}