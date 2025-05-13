using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SmartJastram.Models;
using SmartJastram.Views;
using SmartJastram.Services.Managers;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UsuarioManage _usuarioManager;

        public LoginWindow()
        {
            InitializeComponent();
            _usuarioManager = new UsuarioManage();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, introduce tu correo electrónico y contraseña.",
                    "Campos requeridos",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Cifrar la contraseña antes de verificarla con la base de datos
                string passwordCifrada = cifraSHA(password);

                // Autenticar usuario usando el manager de usuarios
                Usuario usuario = _usuarioManager.Autenticar(email, passwordCifrada);

                if (usuario != null)
                {
                    // Autenticación exitosa
                    MainWindow mainWindow = new MainWindow(usuario);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    // Autenticación fallida
                    MessageBox.Show("Correo electrónico o contraseña incorrectos.",
                        "Error de autenticación",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar iniciar sesión: " + ex.Message,
                    "Error del sistema",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                // En un entorno de producción, considera registrar esta excepción en un log
            }
        }

        // Ya no necesitamos este método porque usamos UsuarioManage.Autenticar

        /// <summary>
        /// Cifra una cadena usando el algoritmo SHA512
        /// </summary>
        /// <param name="cadena">Cadena a cifrar</param>
        /// <returns>Cadena cifrada en formato hexadecimal</returns>
        private static string cifraSHA(string cadena)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(cadena);

                // Sacar hash
                byte[] hash_bytes = sha512.ComputeHash(bytes);

                //Pasar hexadecimal
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash_bytes)
                {
                    sb.Append(b.ToString("x2"));//Para hacerlo Hexadecimal
                }

                return sb.ToString();
            }
        }
    }
}