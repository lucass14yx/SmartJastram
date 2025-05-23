﻿using SmartJastram.Models;
using SmartJastram.Services.Managers;
using System.Windows.Input;
using System.Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Collections.Generic;


namespace SmartJastram.ViewModels
{
    public class NewEditUserPageViewModel : BaseViewModel
    {
        private readonly UserManage _manager;
        private readonly User _currentUser;
        private User _usuarioToEdit;
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
        private string _repetirPassword;
        public string RepetirPassword
        {
            get => _repetirPassword;
            set
            {
                _repetirPassword = value;
                OnPropertyChanged();
            }
        }
        private int _numeroTelefonico;
        public int NumeroTelefonico
        {
            get => _numeroTelefonico;
            set
            {
                _numeroTelefonico = value;
                OnPropertyChanged();
            }
        }

        public class RolItem
        {
            public int ID { get; set; }
            public string Nombre { get; set; }
        }
        private ObservableCollection<RolItem> _roles;
        public ObservableCollection<RolItem> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
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
        public event Action<User> NavigateBackRequested;
        public event Action<User, User> UsuarioSaved;
        #endregion

        public NewEditUserPageViewModel(User currentUser, User usuarioToEdit = null)
        {
            _currentUser = currentUser;
            _manager = new UserManage();
            _usuarioToEdit = usuarioToEdit;
            _isEditMode = usuarioToEdit != null;

            // Inicializar comandos
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);

            // Configurar título de la página
            PageTitle = _isEditMode ? "Editar Usuario" : "Nuevo Usuario";

            // Inicializar la colección de roles
            InitializeRoles();

            // Valores por defecto

            RolID = 1; // Rol por defecto (Operario)

            // Si estamos en modo edición, cargar los datos del usuario
            if (_isEditMode)
            {
                CargarDatosUsuario();
            }
        }
        private void InitializeRoles()
        {
            // Crear una clase simple para los roles
            var rolesList = new List<RolItem>
            {
                new RolItem { ID = 1, Nombre = "Operario" },
                new RolItem { ID = 2, Nombre = "Administrador" },
                new RolItem { ID = 3, Nombre = "Superadministrador" }
            };

            Roles = new ObservableCollection<RolItem>(rolesList);
        }

        private void CargarDatosUsuario()
        {
            if (_usuarioToEdit == null) return;

            Nombre = _usuarioToEdit.Nombre;
            Apellidos = _usuarioToEdit.Apellidos;
            Email = _usuarioToEdit.Email;
            NumeroTelefonico = _usuarioToEdit.NumeroTelefonico;
            RolID = _usuarioToEdit.RolID;
        }

        private bool CanExecuteSave(object obj)
        {
            // Validar que los campos obligatorios no estén vacíos
            return !string.IsNullOrWhiteSpace(Nombre) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   ((!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(RepetirPassword)) || _isEditMode);
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
                User usuario = CrearUsuarioDesdeFormulario();

                if (_isEditMode)
                {
                    // Actualizar el usuario existente
                    usuario.ID = _usuarioToEdit.ID;
                    if (usuario.Contraseña == Password)
                    {
                        _manager.Modify(usuario);
                    }
                    else
                    {
                        _manager.Modify(usuario, false);
                    }
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
            if (Password != RepetirPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden.",
                              "Error de validación",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
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

        private User CrearUsuarioDesdeFormulario()
        {
            // Si estamos en modo edición y no se ha cambiado la contraseña, mantener la existente
            string passwordToUse = Password;
            if (_isEditMode && string.IsNullOrWhiteSpace(Password))
            {
                passwordToUse = _usuarioToEdit.Contraseña;
            }
            

            return new User
            {
                Nombre = Nombre,
                Apellidos = Apellidos,
                Email = Email,
                NumeroTelefonico = NumeroTelefonico,
                Contraseña = passwordToUse,
                RolID = RolID,
            };
        }

        public User CurrentUser => _currentUser;
    }
}