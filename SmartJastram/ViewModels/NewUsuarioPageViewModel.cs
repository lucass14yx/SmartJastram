using SmartJastram.Models;
using SmartJastram.Services.Managers;
using System.Windows.Input;
using System.Windows;
using System;


namespace SmartJastram.ViewModels
{
    public class NewUsuarioPageViewModel : BaseViewModel
    {
        private readonly UsuarioManage _manager;
        private readonly Usuario _currentUser;
        private Usuario _usuarioToEdit;
        private bool _isEditMode;

        #region Propiedades de datos
        private string _nombre;
        public string Nombre
        {
            get => _nombre;
            set
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }

        private string _apellidos;
        public string Apellidos
        {
            get => _apellidos;
            set
            {
                _apellidos = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private int _rolID;
        public int RolID
        {
            get => _rolID;
            set
            {
                _rolID = value;
                OnPropertyChanged();
            }
        }

       

        private string _pageTitle;
        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                _pageTitle = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Comandos
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        #endregion

        #region Eventos
        public event Action<Usuario> NavigateBackRequested;
        public event Action<Usuario, Usuario> UsuarioSaved;
        #endregion

        public NewUsuarioPageViewModel(Usuario currentUser, Usuario usuarioToEdit = null)
        {
            _currentUser = currentUser;
            _manager = new UsuarioManage();
            _usuarioToEdit = usuarioToEdit;
            _isEditMode = usuarioToEdit != null;

            // Inicializar comandos
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);

            // Configurar título de la página
            PageTitle = _isEditMode ? "Editar Usuario" : "Nuevo Usuario";

            // Valores por defecto
            
            RolID = 1; // Rol por defecto (Operario)

            // Si estamos en modo edición, cargar los datos del usuario
            if (_isEditMode)
            {
                CargarDatosUsuario();
            }
        }

        private void CargarDatosUsuario()
        {
            if (_usuarioToEdit == null) return;

            Nombre = _usuarioToEdit.Nombre;
            Apellidos = _usuarioToEdit.Apellidos;
            Email = _usuarioToEdit.Email;
            // No cargamos la contraseña por seguridad
            RolID = _usuarioToEdit.RolID;
        }

        private bool CanExecuteSave(object obj)
        {
            // Validar que los campos obligatorios no estén vacíos
            return !string.IsNullOrWhiteSpace(Nombre) &&
                   
                   (!string.IsNullOrWhiteSpace(Password) || _isEditMode);
        }

        private void ExecuteSave(object obj)
        {
            try
            {
                // Validar los datos ingresados
                if (!ValidarCampos())
                {
                    MessageBox.Show("Por favor, complete todos los campos obligatorios correctamente.",
                                  "Datos incompletos",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                    return;
                }

                // Crear el objeto Usuario con los datos del formulario
                Usuario usuario = CrearUsuarioDesdeFormulario();

                if (_isEditMode)
                {
                    // Actualizar el usuario existente
                    usuario.ID = _usuarioToEdit.ID;
                    _manager.Modify(usuario);
                    MessageBox.Show("Usuario actualizado correctamente.",
                                  "Operación exitosa",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                }
                else
                {
                    // Insertar nuevo usuario
                    _manager.Insert(usuario);
                    MessageBox.Show("Usuario guardado correctamente.",
                                  "Operación exitosa",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                }

                // Notificar que se ha guardado un usuario
                UsuarioSaved?.Invoke(_currentUser, usuario);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error en el formato de los datos: " + ex.Message,
                              "Error de formato",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el usuario: " + ex.Message,
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void ExecuteCancel(object obj)
        {
            // Preguntar si desea cancelar la operación
            MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea cancelar? Los datos no guardados se perderán.",
                                                    "Confirmar cancelación",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                NavigateBackRequested?.Invoke(_currentUser);
            }
        }

        private bool ValidarCampos()
        {
            // Validar que los campos obligatorios no estén vacíos
            if (string.IsNullOrWhiteSpace(Email))
            {
                return false;
            }

            // Si no estamos en modo edición, la contraseña es obligatoria
            if (!_isEditMode && string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            // Validar formato de email si se ha proporcionado
            if (!string.IsNullOrWhiteSpace(Email) && !IsValidEmail(Email))
            {
                MessageBox.Show("El formato del correo electrónico no es válido.",
                              "Error de validación",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private Usuario CrearUsuarioDesdeFormulario()
        {
            // Si estamos en modo edición y no se ha cambiado la contraseña, mantener la existente
            string passwordToUse = Password;
            if (_isEditMode && string.IsNullOrWhiteSpace(Password))
            {
                passwordToUse = _usuarioToEdit.Contraseña;
            }
            else if (!string.IsNullOrWhiteSpace(Password))
            {
                // Encriptar la nueva contraseña
                passwordToUse = SmartJastram.Helpers.SecurityHelper.CifraSHA(Password);
            }

            return new Usuario
            {
                Nombre = Nombre,
                Apellidos = Apellidos,
                Email = Email,
                Contraseña = passwordToUse,
                RolID = RolID,
            };
        }

        public Usuario CurrentUser => _currentUser;
    }
}