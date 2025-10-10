using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Battleship_MobileApp.NET.Maui.Models;

public class Ship : INotifyPropertyChanged
{
    private int _hits;
    public int Size { get; }
    public event PropertyChangedEventHandler PropertyChanged;
    
    public int Hits
    {
        get => _hits;
        private set
        {
            if (_hits != value)
            {
                _hits = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSunk));
            }
        }
    }

    public bool IsSunk => Hits >= Size;

    public Ship(int size)
    {
        Size = size;
        Hits = 0;
    }

    public void AddHit()
    {
        if (!IsSunk)
        {
            Hits++;
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}