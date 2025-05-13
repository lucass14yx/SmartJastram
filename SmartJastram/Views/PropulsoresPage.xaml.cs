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
using Org.BouncyCastle.Cms;
using SmartJastram.Models;
using SmartJastram.Services.Managers;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para PropulsoresPage.xaml
    /// </summary>
    public partial class PropulsoresPage : Page
    {
        private Usuario _currentUser;
        private List<BUType> _propulsores= new List<BUType>();

        public PropulsoresPage(Usuario currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            /// Aquí se verifica si el usuario tiene permisos para ver el botón
            if (_currentUser.RolID > 1 ) // Si el rol es mayor que 1 (Operario), se muestra el botón
            {
                btnNewThruster.Visibility = Visibility.Visible;
                dgtcActions.Visibility = Visibility.Visible;
            }
            

            // Aquí cargarías los datos de la base de datos
            CargarPropulsores();
        }

        public void CargarPropulsores()
        {
            // Esta función simula la carga de datos
            // En un caso real, se conectaría a la base de datos
            // y se obtendrían los datos de los propulsores

            BUType emptyBUType = new BUType();

            _propulsores = emptyBUType.GetPropulsores();


            // Asignar los datos al DataGrid
            ((DataGrid)this.FindName("BUTypesDataGrid")).ItemsSource = _propulsores;
        }
        /// <summary>  
        /// Método que permite insertar un nuevo propulsor en la base de datos.  
        /// </summary>  
        /// <param name="nuevoPropulsor">Objeto que contiene los datos del nuevo propulsor.</param>  
        private void InsertarPropulsor(BUType nuevoPropulsor)
        {
            // Aquí se implementaría la lógica para insertar el nuevo propulsor en la base de datos.  
            // Por ejemplo, se podría llamar a un servicio o repositorio para guardar los datos.  
            MessageBox.Show($"Propulsor {nuevoPropulsor.Designacion} insertado correctamente.");
        }
        /// <summary>
        /// Maneja el evento de clic en el botón Nuevo Propulsor
        /// </summary>
        private void BtnNuevoPropulsor_Click(object sender, RoutedEventArgs e)
        {
            // Navegar a la página de nuevo propulsor
            NavigationService.Navigate(new NewPropulsorPage(_currentUser));
        }
        private void BtnEditBUType_Click(object sender, RoutedEventArgs e)
        {
            // Edita en base de datos y Datagrid
           
        }
        private void BtnDeleteBUType_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int id)
            {
                // Confirmar eliminación
                var result = MessageBox.Show("¿Está seguro que desea eliminar este propulsor?",
                                           "Confirmar eliminación",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Eliminar de la base de datos
                        var manager = new BUTypeManage();
                        var propulsor = _propulsores.FirstOrDefault(p => p.ID == id);

                        if (propulsor != null)
                        {
                            manager.Delete(propulsor);

                            // Eliminar de la lista local
                            _propulsores.Remove(propulsor);

                            // Actualizar el DataGrid
                            BUTypesDataGrid.ItemsSource = null;
                            BUTypesDataGrid.ItemsSource = _propulsores;

                            MessageBox.Show("Propulsor eliminado correctamente.",
                                          "Éxito",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el propulsor: {ex.Message}",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }

                }

            }
        }

    }
}
