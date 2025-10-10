using System.Collections.ObjectModel;

namespace Battleship_MobileApp.NET.Maui.Models;

public class GameBoard
{
    public int Width { get; }
    public int Height { get; }
    
    public ObservableCollection<Cell> Cells { get; }

    public GameBoard(int width = 10, int height = 10)
    {
        Width = width;
        Height = height;
        Cells = new ObservableCollection<Cell>();

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Cells.Add(new Cell(x, y));
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return null;
        }
        return Cells[y * Width + x];
    }
}