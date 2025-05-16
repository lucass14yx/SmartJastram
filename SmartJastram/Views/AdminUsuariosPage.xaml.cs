using System.Windows.Controls;
using SmartJastram.Models;
using SmartJastram.ViewModels;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para AdminUsuariosPage.xaml
    /// </summary>
    public partial class AdminUsuariosPage : Page
    {
        private readonly AdminUsuariosPageViewModel _viewModel;

        public AdminUsuariosPage(Usuario currentUsuario)
        {
            InitializeComponent();
            _viewModel = new AdminUsuariosPageViewModel(currentUsuario);
            DataContext = _viewModel;

            // Suscribirse a eventos de navegación
            _viewModel.NavigateToNewUsuarioRequested += OnNavigateToNewUsuario;
            _viewModel.EditUsuarioRequested += OnEditUsuario;
        }

        private void OnNavigateToNewUsuario()
        {
            NavigationService?.Navigate(new NewUsuarioPage(_viewModel.CurrentUser));
        }

        private void OnEditUsuario(Usuario usuario)
        {
            NavigationService?.Navigate(new NewUsuarioPage(_viewModel.CurrentUser, usuario));
        }
    }
}