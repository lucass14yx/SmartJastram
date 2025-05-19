using System.Windows.Controls;
using SmartJastram.Models;
using SmartJastram.ViewModels;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para NewPropulsorPage.xaml
    /// </summary>
    public partial class NewEditThrusterPage : Page
    {
        private NewEditThrusterPageViewModel _viewModel;

        public NewEditThrusterPage(User currentUser)
        {
            InitializeComponent();

            // Inicializar el ViewModel
            _viewModel = new NewEditThrusterPageViewModel(currentUser);
            DataContext = _viewModel;

            // Suscribirse a eventos del ViewModel
            _viewModel.NavigateBackRequested += OnNavigateBack;
            _viewModel.PropulsorSaved += OnPropulsorSaved;
        }

        public NewEditThrusterPage(User currentUser, BUType propulsorToEdit)
        {
            InitializeComponent();

            // Inicializar el ViewModel en modo edición
            _viewModel = new NewEditThrusterPageViewModel(currentUser, propulsorToEdit);
            DataContext = _viewModel;

            // Suscribirse a eventos del ViewModel
            _viewModel.NavigateBackRequested += OnNavigateBack;
            _viewModel.PropulsorSaved += OnPropulsorSaved;
        }

        private void OnNavigateBack(User currentUser)
        {
            // Navegar de vuelta a la página de propulsores
            NavigationService.Navigate(new ThrustersPage(currentUser));
        }

        private void OnPropulsorSaved(User currentUser, BUType propulsor)
        {
            // Navegar de vuelta a la página de propulsores después de guardar
            NavigationService.Navigate(new ThrustersPage(currentUser));
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