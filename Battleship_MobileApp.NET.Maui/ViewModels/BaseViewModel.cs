using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Battleship_MobileApp.NET.Maui.ViewModels;

public class BaseViewModel
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}