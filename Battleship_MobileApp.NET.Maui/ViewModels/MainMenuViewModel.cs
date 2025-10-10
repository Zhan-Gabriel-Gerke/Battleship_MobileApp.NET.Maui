using System.Windows.Input;
namespace Battleship_MobileApp.NET.Maui.ViewModels;

public class MainMenuViewModel
{
    public ICommand StartGameCommand { get; }

    public MainMenuViewModel()
    {
        StartGameCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("///PreparationPage");
        });
    }
}