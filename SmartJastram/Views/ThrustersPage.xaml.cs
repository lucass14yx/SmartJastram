using System.Windows;
using System.Windows.Controls;
using SmartJastram.Models;
using SmartJastram.ViewModels;
using SmartJastram.Views;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para PropulsoresPage.xaml
    /// </summary>
    public partial class ThrustersPage : Page
    {
        private ThrustersPageViewModel _viewModel;

        public ThrustersPage(User currentUser)
        {
            InitializeComponent();

            // Inicializar el ViewModel y establecerlo como DataContext
            _viewModel = new ThrustersPageViewModel(currentUser);
            this.DataContext = _viewModel;

            // Suscribirse a los eventos del ViewModel
            _viewModel.NavigateToNewPropulsorRequested += OnNavigateToNewPropulsor;
            _viewModel.EditPropulsorRequested += OnEditPropulsor;
        }

        private void OnNavigateToNewPropulsor()
        {
            // Navegar a la página de nuevo propulsor
            NavigationService.Navigate(new NewEditThrusterPage(_viewModel.CurrentUser));
        }

        private void OnEditPropulsor(BUType propulsorToEdit)
        {
            // Navegar a la página de edición de propulsor
            // Puedes crear una página de edición o reutilizar la de nuevo propulsor
            // pasando el propulsor a editar
            NavigationService.Navigate(new NewEditThrusterPage(_viewModel.CurrentUser, propulsorToEdit));
        }

        // Método para limpiar los eventos al descargar la página
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            // Desuscribirse de los eventos para evitar memory leaks
            if (_viewModel != null)
            {
                _viewModel.NavigateToNewPropulsorRequested -= OnNavigateToNewPropulsor;
                _viewModel.EditPropulsorRequested -= OnEditPropulsor;
            }
        }
    }
}
