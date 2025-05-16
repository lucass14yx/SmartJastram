using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SmartJastram.Models;
using SmartJastram.Services.Managers;

namespace SmartJastram.ViewModels
{
    public class AdminUsuariosPageViewModel : BaseViewModel
    {
        private Usuario _currentUser;
        private ObservableCollection<Usuario> _usuarios;
        private Usuario _selectedUsuario;
        private readonly UsuarioManage _usuarioManage;

        public Usuario CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public ObservableCollection<Usuario> Usuarios
        {
            get => _usuarios;
            set => SetProperty(ref _usuarios, value);
        }

        public Usuario SelectedUsuario
        {
            get => _selectedUsuario;
            set
            {
                if (SetProperty(ref _selectedUsuario, value))
                {
                    // Opcionalmente, notificar cambios en comandos que dependan de SelectedUsuario
                }
            }
        }

        /// <summary>
        /// Determina si el usuario actual tiene derechos para gestionar usuarios (añadir, editar, eliminar).
        /// Vincular a esta propiedad para la visibilidad de elementos de la UI (ej. usando BooleanToVisibilityConverter).
        /// </summary>
        public bool CanManageUsuarios => CurrentUser != null && CurrentUser.RolID == 3; // Solo Superadministrador

        // Comandos
        public ICommand NavigateToNewUsuarioCommand { get; }
        public ICommand EditUsuarioCommand { get; }
        public ICommand DeleteUsuarioCommand { get; }
        public ICommand RefreshUsuariosCommand { get; }

        // Eventos para navegación
        public event Action NavigateToNewUsuarioRequested;
        public event Action<Usuario> EditUsuarioRequested;

        public AdminUsuariosPageViewModel(Usuario currentUser)
        {
            CurrentUser = currentUser;
            _usuarioManage = new UsuarioManage();
            Usuarios = new ObservableCollection<Usuario>();

            // Inicializar comandos
            NavigateToNewUsuarioCommand = new RelayCommand(ExecuteNavigateToNewUsuario, CanExecuteNavigateToNewUsuario);
            EditUsuarioCommand = new RelayCommand(ExecuteEditUsuario, CanExecuteEditOrDeleteUsuario);
            DeleteUsuarioCommand = new RelayCommand(ExecuteDeleteUsuario, CanExecuteEditOrDeleteUsuario);
            RefreshUsuariosCommand = new RelayCommand(LoadUsuarios);

            // Cargar usuarios al iniciar
            LoadUsuarios(null);
            OnPropertyChanged(nameof(CanManageUsuarios));
        }

        private void LoadUsuarios(object parameter)
        {
            try
            {
                var usuariosList = _usuarioManage.SelectAll();

                Usuarios.Clear();
                if (usuariosList != null)
                {
                    foreach (var usuario in usuariosList)
                    {
                        Usuarios.Add(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los usuarios: {ex.Message}", "Error de Carga", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteNavigateToNewUsuario(object parameter)
        {
            return CanManageUsuarios;
        }

        private void ExecuteNavigateToNewUsuario(object parameter)
        {
            NavigateToNewUsuarioRequested?.Invoke();
        }

        private bool CanExecuteEditOrDeleteUsuario(object parameter)
        {
            return CanManageUsuarios && parameter is Usuario;
        }

        private void ExecuteEditUsuario(object parameter)
        {
            if (parameter is Usuario usuarioToEdit)
            {
                EditUsuarioRequested?.Invoke(usuarioToEdit);
            }
        }

        private void ExecuteDeleteUsuario(object parameter)
        {
            if (parameter is Usuario usuarioToDelete)
            {
                var result = MessageBox.Show($"¿Está seguro que desea eliminar el usuario '{usuarioToDelete.Nombre} {usuarioToDelete.Apellidos}'?",
                                           "Confirmar Eliminación",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _usuarioManage.Delete(usuarioToDelete);
                        Usuarios.Remove(usuarioToDelete);

                        if (SelectedUsuario == usuarioToDelete)
                        {
                            SelectedUsuario = null; // Limpiar selección si era el eliminado
                        }
                        MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el usuario: {ex.Message}", "Error de Eliminación", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}