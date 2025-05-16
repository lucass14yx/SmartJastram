using System.Windows.Controls;
using SmartJastram.Models;
using SmartJastram.ViewModels;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para NewUsuarioPage.xaml
    /// </summary>
    public partial class NewUsuarioPage : Page
    {
        private NewUsuarioPageViewModel _viewModel;

        public NewUsuarioPage(Usuario currentUser)
        {
            InitializeComponent();

            // Inicializar el ViewModel
            _viewModel = new NewUsuarioPageViewModel(currentUser);
            DataContext = _viewModel;

            // Suscribirse a eventos del ViewModel
            _viewModel.NavigateBackRequested += OnNavigateBack;
            _viewModel.UsuarioSaved += OnUsuarioSaved;
        }

        public NewUsuarioPage(Usuario currentUser, Usuario usuarioToEdit)
        {
            InitializeComponent();

            // Inicializar el ViewModel en modo edición
            _viewModel = new NewUsuarioPageViewModel(currentUser, usuarioToEdit);
            DataContext = _viewModel;

            // Suscribirse a eventos del ViewModel
            _viewModel.NavigateBackRequested += OnNavigateBack;
            _viewModel.UsuarioSaved += OnUsuarioSaved;
        }

        private void OnNavigateBack(Usuario currentUser)
        {
            // Navegar de vuelta a la página de administración de usuarios
            NavigationService.Navigate(new AdminUsuariosPage(currentUser));
        }

        private void OnUsuarioSaved(Usuario currentUser, Usuario usuario)
        {
            // Navegar de vuelta a la página de administración de usuarios después de guardar
            NavigationService.Navigate(new AdminUsuariosPage(currentUser));
        }

        private void Page_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Desuscribirse de los eventos para evitar memory leaks
            if (_viewModel != null)
            {
                _viewModel.NavigateBackRequested -= OnNavigateBack;
                _viewModel.UsuarioSaved -= OnUsuarioSaved;
            }
        }
    }
}