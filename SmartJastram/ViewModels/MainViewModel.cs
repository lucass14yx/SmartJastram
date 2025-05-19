using SmartJastram.Models;
using SmartJastram.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartJastram.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private User _currentUser;
        private Page _currentPage;

        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public Page CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public ICommand NavigateToPropulsoresCommand { get; }
        public ICommand NavigateToUsersCommand { get; }

        public MainViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            NavigateToPropulsoresCommand = new RelayCommand(NavigateToPropulsores);
            NavigateToUsersCommand = new RelayCommand(NavigateToUsers);

            // Página inicial
            NavigateToPropulsores(null);
        }

        private void NavigateToPropulsores(object parameter)
        {
            CurrentPage = new ThrustersPage(CurrentUser);
        }

        private void NavigateToUsers(object parameter)
        {
            if (CurrentUser.RolID == 3) // Solo Superadministrador
            {
                CurrentPage = new UsersPage(CurrentUser);
            }
            else
            {
                MessageBox.Show("No tiene permisos para acceder a esta sección.", "Acceso denegado", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}