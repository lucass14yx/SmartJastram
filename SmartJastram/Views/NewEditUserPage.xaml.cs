using System.Windows.Controls;
using SmartJastram.Models;
using SmartJastram.ViewModels;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para NewUsuarioPage.xaml
    /// </summary>
    public partial class NewEditUserPage : Page
    {
        private NewEditUserPageViewModel _viewModel;

        public NewEditUserPage(User currentUser)
        {
            InitializeComponent();

            // Inicializar el ViewModel
            _viewModel = new NewEditUserPageViewModel(currentUser);
            DataContext = _viewModel;

            // Suscribirse a eventos del ViewModel
            _viewModel.NavigateBackRequested += OnNavigateBack;
            _viewModel.UsuarioSaved += OnUsuarioSaved;
        }

        public NewEditUserPage(User currentUser, User usuarioToEdit)
        {
            InitializeComponent();

            // Inicializar el ViewModel en modo edición
            _viewModel = new NewEditUserPageViewModel(currentUser, usuarioToEdit);
            DataContext = _viewModel;

            // Suscribirse a eventos del ViewModel
            _viewModel.NavigateBackRequested += OnNavigateBack;
            _viewModel.UsuarioSaved += OnUsuarioSaved;
        }

        private void OnNavigateBack(User currentUser)
        {
            // Navegar de vuelta a la página de administración de usuarios
            NavigationService.Navigate(new UsersPage(currentUser));
        }

        private void OnUsuarioSaved(User currentUser, User usuario)
        {
            // Navegar de vuelta a la página de administración de usuarios después de guardar
            NavigationService.Navigate(new UsersPage(currentUser));
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