using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship_MobileApp.NET.Maui.ViewModels;
using Battleship_MobileApp.NET.Maui.Views.Controls;
using SeaBattle.MAUI.ViewModels;

namespace Battleship_MobileApp.NET.Maui.Views;

public partial class PreparationPage : ContentPage
{
    private readonly PreparationViewModel _viewModel;
    public PreparationPage(PreparationViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        BuildUI();
    }

    private void BuildUI()
    {
        var mainLayout = new VerticalStackLayout
        {
            Padding = 10, Spacing = 10
        };

        var shipyardLayout = new HorizontalStackLayout
        {
            Spacing = 5,
            HorizontalOptions = LayoutOptions.Center
        };

        foreach (var shipModel in _viewModel.AvailableShips)
        {
            var shipView = new DraggableShipsView(shipModel);
            var dragGesture = new DragGestureRecognizer();
            dragGesture.DragStarting += (s, e) =>
            {
                e.Data.Properties["ShipId"] = shipModel.Id;
                e.Data.Properties["ShipSize"] = shipModel.Size;
            };
            shipView.GestureRecognizers.Add(dragGesture);
            shipyardLayout.Children.Add(shipView);
        }
        
        var playerBoardGrid = new Grid { ColumnSpacing = 1, RowSpacing = 1 };
        for (int i = 0; i < 10; i++)
        {
            playerBoardGrid.RowDefinitions.Add(new RowDefinition (GridLength.Auto));
            playerBoardGrid.ColumnDefinitions.Add(new ColumnDefinition (GridLength.Auto));
        }

        foreach (var cellModel in _viewModel.PlayerBoard.Cells) 
        {
            var cellView = new BoardCellView(cellModel);

            var dropGesture = new DropGestureRecognizer
            {
                AllowDrop = true
            };

            dropGesture.DropCommand = _viewModel.PlaceShipCommand;
            cellView.GestureRecognizers.Add(dropGesture);

            playerBoardGrid.Add(cellView, cellModel.X, cellModel.Y);
        }
        
        mainLayout.Children.Add(new Label { Text = "Your Ships", FontAttributes = FontAttributes.Bold });
        mainLayout.Children.Add(shipyardLayout);
        mainLayout.Children.Add(new Frame { Content = playerBoardGrid, Padding = 2, BorderColor = Colors.Black});
        
        this.Content = mainLayout;
    }
}