using Minesweeper2.ViewModels;
using System.Windows;

namespace Minesweeper2.Views
{
    /// <summary>
    /// Interaktionslogik für StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            this.DataContext = new GameWindowViewModel();
            InitializeComponent();
        }
    }
}
