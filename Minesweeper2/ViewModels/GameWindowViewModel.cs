using Minesweeper2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Minesweeper2.ViewModels
{
    class GameWindowViewModel : ViewModelBase
    {
        private GameBoard gameBoard;
        public GameBoard GameBoard
        {
            get
            {
                return this.gameBoard;
            }
            set
            {
                this.gameBoard = value;
                this.OnPropertyChanged();
            }
        }
        public ICommand TileClickCommand { get; set; }
        public ICommand SetFlagCommand { get; set; }
        public GameWindowViewModel()
        {
            GameBoard = new GameBoard(GameBoard.BoardSize.Beginner);
            TileClickCommand = new RelayCommand(ExecuteTileClick, CanExecuteTileClick);
            SetFlagCommand = new RelayCommand(ExecuteSetFlag, CanExecuteSetFlag);
        }

        private bool CanExecuteSetFlag(object arg)
        {
            int tileID = Convert.ToInt32(arg);
            return !GameBoard.Tiles[tileID - 1].IsRevealed;
        }

        private void ExecuteSetFlag(object obj)
        {
            int tileID = Convert.ToInt32(obj);
            if (!GameBoard.Tiles[tileID - 1].IsFlagged)
            {
                GameBoard.Tiles[tileID - 1].IsFlagged = true;
                GameBoard.Tiles[tileID - 1].Image = "/Images/flag.png";
                GameBoard.NumberOfMines--;
            }
            else
            {
                GameBoard.Tiles[tileID - 1].IsFlagged = false;
                GameBoard.Tiles[tileID - 1].Image = "/Images/default.png";
                GameBoard.NumberOfMines++;
            }
        }

        private bool CanExecuteTileClick(object arg)
        {
            int tileID = Convert.ToInt32(arg);
            return !GameBoard.Tiles[tileID - 1].IsRevealed;
        }

        private void ExecuteTileClick(object obj)
        {
            int tileID = Convert.ToInt32(obj);
            var tile = GameBoard.Tiles[tileID - 1];
            tile.IsRevealed = true;
            if (tile.HasMine)
            {
                tile.Image = "/Images/mine.png";
                MessageBox.Show("You lose!", "Boom!");
            }
            else if (tile.AdjacentMines != 0)
            {
                switch (tile.AdjacentMines)
                {
                    case 1:
                        tile.Image = "/Images/one.png";
                        break;
                    case 2:
                        tile.Image = "/Images/two.png";
                        break;
                    case 3:
                        tile.Image = "/Images/three.png";
                        break;
                    case 4:
                        tile.Image = "/Images/four.png";
                        break;
                    case 5:
                        tile.Image = "/Images/five.png";
                        break;
                    case 6:
                        tile.Image = "/Images/six.png";
                        break;
                    case 7:
                        tile.Image = "/Images/seven.png";
                        break;
                    case 8:
                        tile.Image = "/Images/eight.png";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                tile.Image = "/Images/empty.png";
                foreach (Tile tileChek in GameBoard.Tiles)
                {
                    if (tileChek.IsRevealed == false && Math.Abs(tileChek.X - tile.X) < 2 && Math.Abs(tileChek.Y - tile.Y) < 2)
                    {
                        ExecuteTileClick(tileChek.ID);
                    }
                }
            }
            // Check for win
            int i = 0;
            foreach (var item in GameBoard.Tiles)
            {
                if (!item.IsRevealed)
                {
                    i++;
                }
            }
            if (i == 10)
            {
                MessageBox.Show("You win!", "Congrats!");
            }
        }
    }
}
