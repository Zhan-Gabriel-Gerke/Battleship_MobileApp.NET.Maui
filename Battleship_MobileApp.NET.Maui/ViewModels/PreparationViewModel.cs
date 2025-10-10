using Battleship_MobileApp.NET.Maui.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Battleship_MobileApp.NET.Maui.Models;
using Battleship_MobileApp.NET.Maui.ViewModels;
using GameCell = Battleship_MobileApp.NET.Maui.Models.Cell;

namespace SeaBattle.MAUI.ViewModels
{
    public class PreparationViewModel : BaseViewModel
    {
        private readonly ShipPlacementService _shipPlacementService;
        private Ship _selectedShip;

        public GameBoard PlayerBoard { get; }
        public ObservableCollection<Ship> AvailableShips { get; }

        public Ship SelectedShip
        {
            get => _selectedShip;
            set
            {
                _selectedShip = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectShipCommand { get; }
        public ICommand PlaceShipCommand { get; }
        public ICommand FinalizePlacementCommand { get; }

        public PreparationViewModel(ShipPlacementService shipPlacementService)
        {
            _shipPlacementService = shipPlacementService;
            PlayerBoard = new GameBoard();
            
            AvailableShips = new ObservableCollection<Ship>
            {
                new Ship(4, "ship_4_0"),
                new Ship(3, "ship_3_0"), new Ship(3, "ship_3_1"),
                new Ship(2, "ship_2_0"), new Ship(2, "ship_2_1"), new Ship(2, "ship_2_2"),
                new Ship(1, "ship_1_0"), new Ship(1, "ship_1_1"), new Ship(1, "ship_1_2"), new Ship(1, "ship_1_3")
            };

            SelectShipCommand = new Command<Ship>(ship => SelectedShip = ship);

            PlaceShipCommand = new Command<GameCell>(targetCell =>
            {
                if (SelectedShip == null || targetCell == null) return;

                int orientation = 0; 

                if (_shipPlacementService.CanPlaceShip(PlayerBoard, targetCell.X, targetCell.Y, SelectedShip.Size, orientation))
                {
                    _shipPlacementService.PlaceShip(PlayerBoard, targetCell.X, targetCell.Y, SelectedShip.Size, orientation);
                    AvailableShips.Remove(SelectedShip);
                    SelectedShip = null;
                }
                else
                {
                    Shell.Current.DisplayAlert("Error", "You can't place a ship here.", "OK");
                }
            });

            FinalizePlacementCommand = new Command(async () =>
            {
                if (!AvailableShips.Any())
                {
                    var navigationParameter = new Dictionary<string, object>
                    {
                        { "PlayerBoard", PlayerBoard }
                    };
                    await Shell.Current.GoToAsync("///GamePage", navigationParameter);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Wait!", "You must place all ships before starting.", "OK");
                }
            });
        }
    }
}