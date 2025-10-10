using Battleship_MobileApp.NET.Maui.Models;
using System.Windows.Input;
using Battleship_MobileApp.NET.Maui.Models.Enums;
using Battleship_MobileApp.NET.Maui.Services;
using Battleship_MobileApp.NET.Maui.ViewModels;
using GameCell = Battleship_MobileApp.NET.Maui.Models.Cell;

namespace SeaBattle.MAUI.ViewModels
{
    [QueryProperty(nameof(PlayerBoard), "PlayerBoard")]
    public class GameViewModel : BaseViewModel
    {
        private readonly GameLogicService _gameLogicService;
        private string _statusMessage;
        private bool _isPlayerTurn = true;
        private GameBoard _playerBoard;

        public GameBoard OpponentBoard { get; }

        public GameBoard PlayerBoard
        {
            get => _playerBoard;
            set
            {
                _playerBoard = value;
                
                // Reveal the player's own ships so they can see them.
                foreach (var cell in _playerBoard.Cells)
                {
                    if (cell.State == CellState.Ship)
                    {
                        cell.Reveal();
                    }
                }
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand ShotCommand { get; }

        public GameViewModel(GameLogicService gameLogicService, ShipPlacementService shipPlacementService)
        {
            _gameLogicService = gameLogicService;

            OpponentBoard = new GameBoard();
            shipPlacementService.PlaceShipsRandomly(OpponentBoard);
            
            StatusMessage = "Your turn. Fire a shot!";
            ShotCommand = new Command<GameCell>(async (cell) => await OnShot(cell), (cell) => _isPlayerTurn);
        }

        private async Task OnShot(GameCell targetCell)
        {
            if (targetCell == null || !_isPlayerTurn) return;

            var result = _gameLogicService.ProcessShot(OpponentBoard, targetCell.X, targetCell.Y);
            if (result == null) return;

            if (result.IsGameOver)
            {
                await Shell.Current.DisplayAlert("Game Over", "You Win!", "Play Again");
                await Shell.Current.GoToAsync("///MainMenuPage");
                return;
            }

            if (result.IsHit)
            {
                StatusMessage = "It's a HIT! Fire again.";
            }
            else
            {
                StatusMessage = "Miss. Opponent's turn...";
                _isPlayerTurn = false;
                ((Command)ShotCommand).ChangeCanExecute();

                await Task.Delay(1000);
                OpponentTurn();
            }
        }
        
        private async void OpponentTurn()
        {
            var random = new Random();
            var unrevealedCells = PlayerBoard.Cells.Where(c => !c.IsRevealed).ToList();
            if (!unrevealedCells.Any()) return;

            var targetCell = unrevealedCells[random.Next(unrevealedCells.Count)];
            var result = _gameLogicService.ProcessShot(PlayerBoard, targetCell.X, targetCell.Y);

            if (result.IsGameOver)
            {
                await Shell.Current.DisplayAlert("Game Over", "You Lose!", "Play Again");
                await Shell.Current.GoToAsync("///MainMenuPage");
                return;
            }

            if(result.IsHit)
            {
                 StatusMessage = "Opponent scored a HIT! Their turn again.";
                 await Task.Delay(1000);
                 OpponentTurn();
            }
            else
            {
                StatusMessage = "Opponent missed. Your turn!";
                _isPlayerTurn = true;
                ((Command)ShotCommand).ChangeCanExecute();
            }
        }
    }
}