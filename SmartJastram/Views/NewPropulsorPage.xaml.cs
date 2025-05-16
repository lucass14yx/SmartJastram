using System.Windows.Controls;
using SmartJastram.Models;
using SmartJastram.ViewModels;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para NewPropulsorPage.xaml
    /// </summary>
    public partial class NewPropulsorPage : Page
    {
        private NewPropulsorPageViewModel _viewModel;

        public NewPropulsorPage(Usuario currentUser)
        {
            InitializeComponent();

            // Inicializar el ViewModel
            _viewModel = new NewPropulsorPageViewModel(currentUser);
            DataContext = _viewModel;

            // Suscribirse a eventos del ViewModel
            _viewModel.NavigateBackRequested += OnNavigateBack;
            _viewModel.PropulsorSaved += OnPropulsorSaved;
        }

        public NewPropulsorPage(Usuario currentUser, BUType propulsorToEdit)
        {
            InitializeComponent();

            // Inicializar el ViewModel en modo edición
            _viewModel = new NewPropulsorPageViewModel(currentUser, propulsorToEdit);
            DataContext = _viewModel;

            // Suscribirse a eventos del ViewModel
            _viewModel.NavigateBackRequested += OnNavigateBack;
            _viewModel.PropulsorSaved += OnPropulsorSaved;
        }

        private void OnNavigateBack(Usuario currentUser)
        {
            // Navegar de vuelta a la página de propulsores
            NavigationService.Navigate(new PropulsoresPage(currentUser));
        }

        private void OnPropulsorSaved(Usuario currentUser, BUType propulsor)
        {
            // Navegar de vuelta a la página de propulsores después de guardar
            NavigationService.Navigate(new PropulsoresPage(currentUser));
        }

        private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Desuscribirse de los eventos para evitar memory leaks
            if (_viewModel != null)
            {
                _viewModel.NavigateBackRequested -= OnNavigateBack;
                _viewModel.PropulsorSaved -= OnPropulsorSaved;
            }
        }
    }
}