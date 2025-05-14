using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para PropulsoresPage.xaml
    /// </summary>
    public partial class AdminUsuariosPage : Page
    {
        private Usuario _currentUser;
        private ObservableCollection<Usuario> _usuarios = new ObservableCollection<Usuario>();

        public AdminUsuariosPage(Usuario currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            // Aquí cargarías los datos de la base de datos
            CargarUsuarios();
        }

        public void CargarUsuarios()
        {
            // Esta función simula la carga de datos
            // En un caso real, se conectaría a la base de datos
            // y se obtendrían los datos de los propulsores

            Usuario emptyUsuario = new Usuario();

            _usuarios = emptyUsuario.GetUsuarios();


            // Asignar los datos al DataGrid
            ((DataGrid)this.FindName("UsuariosDataGrid")).ItemsSource = _usuarios;
        }
        /// <summary>  
        /// Método que permite insertar un nuevo propulsor en la base de datos.  
        /// </summary>  
        /// <param name="nuevoPropulsor">Objeto que contiene los datos del nuevo propulsor.</param>  
        private void InsertarUsuario(Usuario nuevoUsuario)
        {
            // Aquí se implementaría la lógica para insertar el nuevo propulsor en la base de datos.  
            // Por ejemplo, se podría llamar a un servicio o repositorio para guardar los datos.  
            MessageBox.Show($"Usuario {nuevoUsuario.Email} insertado correctamente.");
        }
        /// <summary>
        /// Maneja el evento de clic en el botón Nuevo Propulsor
        /// </summary>
        private void BtnNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            // Navegar a la página de nuevo propulsor
            NavigationService.Navigate(new NewUsuarioPage(_currentUser));
        }
    }
}
