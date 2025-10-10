using Battleship_MobileApp.NET.Maui.Models;
using Battleship_MobileApp.NET.Maui.Models.Enums;

namespace Battleship_MobileApp.NET.Maui.Services;

public class ShipPlacementService
{
    public bool CanPlaceShip(GameBoard board, int startX, int startY, int size, int orientation)
    {
        if (orientation == 0)
        {
            if (startX + size > board.Width) return false;
        }
        else
        {
            if (startY + size > board.Height) return false;
        }

        //check if ship overlapped another ship
        for (int i = 0; i < size; i++)
        {
            int x = startX + (orientation == 0 ? i : 0);
            int y = startY + (orientation == 1 ? i : 0);

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    var cell = board.GetCell(x + dx, y + dy);
                    if (cell != null && cell.State == CellState.Ship)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void PlaceShip(GameBoard board, int startX, int startY, int size, int orientation)
    {
        for (int i = 0; i < size; i++)
        {
            int x = startX + (orientation == 0 ? i : 0);
            int y = startY + (orientation == 1 ? i : 0);
            var cell = board.GetCell(x, y);
            if (cell != null)
            {
                cell.State = CellState.Ship;
            }
        }
    }
    
}