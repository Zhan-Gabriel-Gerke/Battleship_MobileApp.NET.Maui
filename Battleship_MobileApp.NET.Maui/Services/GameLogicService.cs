using Battleship_MobileApp.NET.Maui.Models;
using Battleship_MobileApp.NET.Maui.Models.Enums;

namespace Battleship_MobileApp.NET.Maui.Services;

public class ShotResult
{
    public bool IsHit { get; set; }
    public bool IsGameOver { get; set; }
}
public class GameLogicService
{
    //the method takes cell and cords
    public ShotResult ProcessShot(GameBoard board, int x, int y)
    {
        var cell = board.GetCell(x, y);
        var result = new ShotResult();

        //checks the cell is cell valid
        if (cell == null || cell.State == CellState.Hit || cell.State == CellState.Miss)
        {
            return null;
        }
        
        //show the cell
        cell.Reveal();

        //check if there has been a ship and change the statement
        if (cell.State == CellState.Ship)
        {
            cell.State = CellState.Hit;
            result.IsHit = true;

            result.IsGameOver = CheckForGameOver(board);
        }
        else
        {
            cell.State = CellState.Miss;
            result.IsHit = false;
        }
        return result;
    }

    public bool CheckForGameOver(GameBoard board)
    {
        return !board.Cells.Any(c => c.State == CellState.Ship);
    }
}