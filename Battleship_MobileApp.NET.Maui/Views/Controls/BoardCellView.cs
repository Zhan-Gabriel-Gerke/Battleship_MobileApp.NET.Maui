using GameCell = Battleship_MobileApp.NET.Maui.Models.Cell;
using Battleship_MobileApp.NET.Maui.Models.Enums;
namespace Battleship_MobileApp.NET.Maui.Views.Controls;

public class BoardCellView : Border
{
    private readonly GameCell _cellModel;

    public BoardCellView(GameCell cellModel)
    {
        _cellModel = cellModel;
        this.BindingContext = _cellModel;

        _cellModel.PropertyChanged += (sender, args) =>
        {
            // React to changes
            if (args.PropertyName == nameof(Models.Cell.State) || args.PropertyName == nameof(Models.Cell.DisplayState))
            {
                UpdateColor();
            }
        };

        //visual setup
        this.Stroke = Colors.DarkBlue;
        this.StrokeThickness = 1;
        this.WidthRequest = 40;
        this.HeightRequest = 40;
        UpdateColor();
    }

    public void UpdateColor()
    {
        this.BackgroundColor = _cellModel.DisplayState switch
        {
            CellState.Ship => Colors.DarkSlateGray,
            CellState.Hit => Colors.Red,
            CellState.Miss => Colors.DarkGray,
            _ => Colors.LightBlue
        };
    }
    
}