namespace Battleship_MobileApp.NET.Maui.Models;

public class Player
{
    public string Name { get; }
    public GameBoard Board { get; }

    public Player(string name)
    {
        Name = name;
        Board = new GameBoard();
    }
}