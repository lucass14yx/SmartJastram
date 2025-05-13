using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartJastram.Models;
using SmartJastram.Views;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Usuario _currentUser;

        public MainWindow(Usuario currentUser)
        {
            InitializeComponent();

            _currentUser = currentUser;
            UserInfoText.Text = $"Usuario: {_currentUser.NombreCompleto()}, {_currentUser.RolTipo}";

            // Configurar visibilidad de botones según el rol del usuario
            ConfigurarPermisos(_currentUser.RolID);

            // Cargar página inicial por defecto
            NavigateToPropulsoresPage(this, null);
        }

        private void ConfigurarPermisos(int rolID)
        {
            // Obtener todos los botones del menú
            var buttons = ((StackPanel)((Border)((Grid)((Grid)this.Content).Children[1]).Children[0]).Child).Children;

            // Por defecto, el operario solo ve Propulsores y Formularios
            if (rolID == 1) // Operario
            {
                // Esconder botones que no corresponden al rol
                for (int i = 2; i < buttons.Count; i++)
                {
                    ((Button)buttons[i]).Visibility = Visibility.Collapsed;
                }
            }
            else if (rolID == 2) // Administrador
            {
                // El administrador no puede ver la gestión de usuarios
                ((Button)buttons[4]).Visibility = Visibility.Collapsed;
            }
            // El Superadministrador (rolID == 3) ve todo
        }

        private void NavigateToPropulsoresPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PropulsoresPage(_currentUser));
        }

        //private void NavigateToFormulariosPage(object sender, RoutedEventArgs e)
        //{
        //    MainFrame.Navigate(new FormulariosPage(_currentUser));
        //}

        //private void NavigateToProyectosPage(object sender, RoutedEventArgs e)
        //{
        //    MainFrame.Navigate(new ProyectosPage(_currentUser));
        //}

        //private void NavigateToInformesPage(object sender, RoutedEventArgs e)
        //{
        //    MainFrame.Navigate(new InformesPage(_currentUser));
        //}

        private void NavigateToUsersPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminUsuariosPage(_currentUser));
        }
    }
}
