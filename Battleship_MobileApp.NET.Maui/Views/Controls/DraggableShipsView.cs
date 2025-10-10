using Battleship_MobileApp.NET.Maui.Models;

namespace Battleship_MobileApp.NET.Maui.Views.Controls;

public class DraggableShipsView : Border
{
    private readonly Ship _shipModel;

    public DraggableShipsView(Ship shipModel)
    {
        _shipModel = shipModel;
        this.BindingContext = _shipModel;

        _shipModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(Ship.IsPlaced))
            {
                this.IsVisible = !_shipModel.IsPlaced;
            }
        };

        this.BackgroundColor = Colors.DarkGray;
        this.Stroke = Colors.Black;
        this.WidthRequest = 40 * _shipModel.Size;
        this.HeightRequest = 40;
        this.Content = new Label
        {
            Text = $"{_shipModel.Size}",
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };
    }
}