using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartJastram.Models;
using SmartJastram.ViewModels;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        private ThrustersPageViewModel _viewModel;
        public ReportsPage(User currentUser)
        {
            InitializeComponent();
            // Inicializar el ViewModel y establecerlo como DataContext
            _viewModel = new ThrustersPageViewModel(currentUser);
            this.DataContext = _viewModel;
        }
    }
}
