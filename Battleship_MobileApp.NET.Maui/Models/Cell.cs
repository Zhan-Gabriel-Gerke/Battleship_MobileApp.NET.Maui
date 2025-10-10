using System.ComponentModel;
using System.Runtime.CompilerServices;
using Battleship_MobileApp.NET.Maui.Models.Enums;

namespace Battleship_MobileApp.NET.Maui.Models;

public class Cell : INotifyPropertyChanged
{
    private string _shipId;
    private CellState _state;
    private bool _isRevealed = false;
 
    public event PropertyChangedEventHandler PropertyChanged;
    public int X { get; }
    public int Y { get; }

    public string ShipId
    {
        get => _shipId;
        set
        {
            if (_shipId != value)
            {
                _shipId = value;
                OnPropertyChanged();
            }
        }
    }
    
    //Property which allows to check the real state
    public CellState State
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                _state = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayState));
            }
        }
    }

    public CellState DisplayState
    {
        get
        {
            if (_isRevealed)
            {
                return State;
            }
            else
            {
                return State == CellState.Miss ? CellState.Miss : CellState.Empty;
            }
        }
    }

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
        State = CellState.Empty;
    }
    
    public void Reveal()
    {
        if (!_isRevealed)
        {
            _isRevealed = true;
            OnPropertyChanged(nameof(DisplayState));
        }
    }
    
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//inform UI about changes
    }
}