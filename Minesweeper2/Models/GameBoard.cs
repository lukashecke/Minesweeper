using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper2.Models
{
    class GameBoard : ModelBase
    {
        public enum BoardSize { Beginner, Medium, Difficult }
        public int Width { get; set; }
        public int Height { get; set; }
        public int numberOfMines;
        public int NumberOfMines
        {
            get
            {
                return this.numberOfMines;
            }
            set
            {
                this.numberOfMines = value;
                this.OnPropertyChanged();
            }
        }
        private ObservableCollection<Tile> tiles;
        public ObservableCollection<Tile> Tiles
        {
            get
            {
                return this.tiles;
            }
            set
            {
                this.tiles = value;
                this.OnPropertyChanged();
            }
        }
        /// <summary>
        /// Minimum GameBoard ist 9x9 Tiles.
        /// </summary>
        /// <param name="width">9 for everything < 8</param>
        /// <param name="height">9 for everything < 8</param>
        public GameBoard(BoardSize boardSize)
        {
            switch (boardSize)
            {
                case BoardSize.Beginner:
                    Width = 9;
                    Height = 9;
                    NumberOfMines = 10;
                    break;
                case BoardSize.Medium:
                    Width = 16;
                    Height = 19;
                    NumberOfMines = 40;
                    break;
                case BoardSize.Difficult:
                    Width = 30;
                    Height = 16;
                    NumberOfMines = 99;
                    break;
            }
            Tiles = CreateTiles(Width, Height);
            SetRandomMines();
            CalculateAdjacentMines();
        }

        private void CalculateAdjacentMines()
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tiles[i].AdjacentMines = GetAmountAdjacentMines(Tiles[i]);
            }
        }

        private int GetAmountAdjacentMines(Tile tile)
        {
            List<Tile> adjacentTiles = GetAdjacentTiles(tile);
            int counter = 0;
            foreach (var item in adjacentTiles)
            {
                if (item.HasMine)
                {
                    counter++;
                }
            }
            return counter;
        }

        private List<Tile> GetAdjacentTiles(Tile tile)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (var item in Tiles)
            {
                if (Math.Abs(item.X - tile.X) < 2 && Math.Abs(item.Y - tile.Y) < 2 && tile.ID != item.ID)
                {
                    tiles.Add(item);
                }
            }
            return tiles;
        }

        private void SetRandomMines()
        {
            List<int> randomTileIndices = GetRandomNumbers(NumberOfMines, Tiles.First().ID, Tiles.Last().ID);
            foreach (Tile tile in Tiles)
            {
                if (randomTileIndices.Contains(tile.ID))
                {
                    tile.HasMine = true;
                }
            }
        }

        private List<int> GetRandomNumbers(int amount, int from, int to)
        {
            Random random = new Random();
            List<int> randoms = new List<int>();
            for (int i = 0; i < amount; i++)
            {
                int randomIndex = random.Next(from,to+1);
                while (randoms.Contains(randomIndex)) // no doubles
                {
                    randomIndex = random.Next(from, to + 1);
                }
                randoms.Add(randomIndex);
            }

            return randoms;
        }

        private ObservableCollection<Tile> CreateTiles(int width, int height)
        {
            ObservableCollection<Tile> tilesList = new ObservableCollection<Tile>();
            int index = 1;
            for (int y = 1; y <= height; y++)
            {
                for (int x = 1; x <= width; x++)
                {
                    tilesList.Add(new Tile(index, x, y));
                    index++;
                }
            }
            return tilesList;
        }
    }
}
