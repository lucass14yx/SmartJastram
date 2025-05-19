using SmartJastram.Models;
using SmartJastram.Services.Managers;
using SmartJastram.Helpers;
using System.Windows;
using System.Windows.Input;
using SmartJastram.Views;
using System;

namespace SmartJastram.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {
        private readonly UsuarioManage _usuarioManager;

        private string _email;
        private string _password;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _usuarioManager = new UsuarioManage();
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object parameter)
        {
            // Habilita el botón solo si hay texto en email y contraseña
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        private void ExecuteLogin(object parameter)
        {
            try
            {
                string passwordCifrada = SecurityHelper.EncodeSHA(Password);
                Usuario usuario = _usuarioManager.Autenticar(Email, passwordCifrada);

                if (usuario != null)
                {
                    // Abre la ventana principal
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MainWindow mainWindow = new MainWindow(usuario);
                        mainWindow.Show();
                        CloseWindow();
                    });
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is LoginWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}