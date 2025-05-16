using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SmartJastram.Models;
using SmartJastram.Services.Managers;

namespace SmartJastram.Views
{
    public partial class NewUsuarioPage : Page
    {
        private Usuario _currentUser;
        private readonly UsuarioManage _usuarioManager = new UsuarioManage();

        public NewUsuarioPage(Usuario currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            CargarRoles();

            // Conectar eventos de los botones  
            var btnGuardar = (Button)FindName("btnGuardar");
            var btnCancelar = (Button)FindName("btnCancelar");

            if (btnGuardar != null) btnGuardar.Click += BtnGuardar_Click;
            if (btnCancelar != null) btnCancelar.Click += BtnCancelar_Click;
            
        }
        public NewUsuarioPage(Usuario currentUser, Usuario userToEdit)
        {
            InitializeComponent();
            _currentUser = currentUser;
            CargarRoles();

            // Conectar eventos de los botones  
            var btnGuardar = (Button)FindName("btnGuardar");
            var btnCancelar = (Button)FindName("btnCancelar");

            if (btnGuardar != null) btnGuardar.Click += BtnGuardar_Click;
            if (btnCancelar != null) btnCancelar.Click += BtnCancelar_Click;

        }

        private void CargarRoles()
        {
            // Puedes cargar esto desde la base de datos si lo prefieres  
            cmbRol.Items.Clear();
            cmbRol.Items.Add(new ComboBoxItem { Content = "Operario", Tag = 1 });
            cmbRol.Items.Add(new ComboBoxItem { Content = "Administrador", Tag = 2 });
            cmbRol.Items.Add(new ComboBoxItem { Content = "Superadministrador", Tag = 3 });
            cmbRol.SelectedIndex = 0;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                {
                    MessageBox.Show("Por favor, complete todos los campos correctamente.",
                                  "Validación",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                    return;
                }

                if (!ValidarPassword())
                {
                    MessageBox.Show("Las contraseñas no coinciden.",
                                  "Validación",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                    return;
                }

                var nuevoUsuario = CrearUsuarioDesdeFormulario();
                _usuarioManager.Insert(nuevoUsuario);

                MessageBox.Show("Usuario creado exitosamente.",
                              "Éxito",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);

                // Volver a la página anterior  
                NavigationService.Navigate(new AdminUsuariosPage(_currentUser));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el usuario: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar? Los datos no guardados se perderán.",
                              "Confirmar",
                              MessageBoxButton.YesNo,
                              MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                NavigationService?.GoBack();
            }
        }

        private bool ValidarCampos()
        {
            // Validación básica de campos obligatorios  
            return !string.IsNullOrWhiteSpace(txtNombre.Text) &&
                   !string.IsNullOrWhiteSpace(txtApellidos.Text) &&
                   !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                   txtPassword.Password.Length >= 6 && // Mínimo 6 caracteres  
                   txtTelefono.Text.Length == 9 && // Número de teléfono válido  
                   cmbRol.SelectedItem != null;
        }

        private bool ValidarPassword()
        {
            return txtPassword.Password == txtPasswordRepeat.Password;
        }

        private Usuario CrearUsuarioDesdeFormulario()
        {
            var rolSeleccionado = (ComboBoxItem)cmbRol.SelectedItem;
            int rolId = (int)rolSeleccionado.Tag;

            return new Usuario(
                nombre: txtNombre.Text.Trim(),
                apellidos: txtApellidos.Text.Trim(),
                numeroTelefonico: int.Parse(txtTelefono.Text),
                email: txtEmail.Text.Trim(),
                contraseña: txtPassword.Password,
                rolID: rolId
            );
        }
    }
}