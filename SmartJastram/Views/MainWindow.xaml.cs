using SmartJastram.Models;
using SmartJastram.ViewModels;
using System.Windows;

namespace SmartJastram.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(Usuario currentUser)
        {
            InitializeComponent();
            DataContext = new MainViewModel(currentUser); // Inyecta el usuario
        }
    }
}