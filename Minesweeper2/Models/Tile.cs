using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper2.Models
{
    class Tile : ModelBase
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool HasMine { get; set; }
        public int AdjacentMines { get; set; }

        private bool isRevealed;
        public bool IsRevealed
        {
            get
            {
                return this.isRevealed;
            }
            set
            {
                this.isRevealed = value;
                this.OnPropertyChanged();
            }
        }
        private bool isFlagged;
        public bool IsFlagged
        {
            get
            {
                return this.isFlagged;
            }
            set
            {
                this.isFlagged = value;
                this.OnPropertyChanged();
            }
        }
        private string image;
        public string Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
                this.OnPropertyChanged();
            }
        }
        public Tile(int id, int x, int y)
        {
            ID = id;
            X = x;
            Y = y;

            Image = "/Images/default.png";
        }
    }
}
