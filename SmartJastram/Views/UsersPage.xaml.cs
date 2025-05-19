using System.Windows.Controls;
using SmartJastram.Models;
using SmartJastram.ViewModels;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para AdminUsuariosPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private readonly UsersPageViewModel _viewModel;

        public UsersPage(User currentUsuario)
        {
            InitializeComponent();
            _viewModel = new UsersPageViewModel(currentUsuario);
            DataContext = _viewModel;

            // Suscribirse a eventos de navegación
            _viewModel.NavigateToNewUsuarioRequested += OnNavigateToNewUsuario;
            _viewModel.EditUsuarioRequested += OnEditUsuario;
        }

        private void OnNavigateToNewUsuario()
        {
            NavigationService?.Navigate(new NewEditUserPage(_viewModel.CurrentUser));
        }

        private void OnEditUsuario(User usuario)
        {
            NavigationService?.Navigate(new NewEditUserPage(_viewModel.CurrentUser, usuario));
        }
    }
}