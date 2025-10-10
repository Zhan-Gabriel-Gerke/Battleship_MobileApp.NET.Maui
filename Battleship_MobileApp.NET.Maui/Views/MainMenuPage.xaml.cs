using Battleship_MobileApp.NET.Maui.ViewModels;

namespace Battleship_MobileApp.NET.Maui.Views;

public partial class MainMenuPage : ContentPage
{
    public MainMenuPage(MainMenuViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        BuildUI();
    }

    private void BuildUI()
    {
        var startGameButton = new Button()
        {
            Text = "Start Game",
            FontSize = 24,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        
        startGameButton.SetBinding(Button.CommandProperty, nameof(MainMenuViewModel.StartGameCommand));
        
        this.Content = startGameButton;
    }
}