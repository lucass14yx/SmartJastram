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
using SmartJastram.Services.Managers;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para NewPropulsorPage.xaml
    /// </summary>
    public partial class NewPropulsorPage : Page
    {
        private Usuario _currentUser;
        private BUTypeManage manager;

        public NewPropulsorPage(Usuario currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            manager = new BUTypeManage();
        }

        /// <summary>
        /// Maneja el evento de clic en el botón Guardar
        /// </summary>
        /// <param name="sender">Objeto que generó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
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

                // Crear el objeto BUType con los datos del formulario
                BUType nuevoPropulsor = CrearPropulsorDesdeFormulario();

                // Insertar en la base de datos
                manager.Insert(nuevoPropulsor);

                MessageBox.Show("Propulsor guardado correctamente.",
                              "Operación exitosa",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);

                // Volver a la página de propulsores
                // Aquí se podría navegar a la página de propulsores o actualizar la lista (Por esta razon no usamos GoBack)
                NavigationService.Navigate(new PropulsoresPage(_currentUser));


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
                MessageBox.Show("Error al guardar el propulsor: " + ex.Message,
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el botón Cancelar
        /// </summary>
        /// <param name="sender">Objeto que generó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            // Preguntar si desea cancelar la operación
            MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea cancelar? Los datos no guardados se perderán.",
                                                    "Confirmar cancelación",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        /// <summary>
        /// Valida que todos los campos requeridos tengan datos y en el formato correcto
        /// </summary>
        /// <returns>True si todos los campos son válidos, False en caso contrario</returns>
        private bool ValidarCampos()
        {
            // Validar que el campo Designación no esté vacío
            if (string.IsNullOrWhiteSpace(txtDesignacion.Text))
            {
                return false;
            }

            // Validar que los campos numéricos tengan valores válidos
            // Se podría agregar validación adicional para cada campo si es necesario

            // Ejemplo de validación para campos numéricos enteros
            if (!ValidarEntero(txtN1.Text) || !ValidarEntero(txtN2.Text) ||
                !ValidarEntero(txtB.Text) || !ValidarEntero(txtC.Text) ||
                !ValidarEntero(txtD.Text) || !ValidarEntero(txtE.Text) ||
                !ValidarEntero(txtF.Text) || !ValidarEntero(txtA.Text) ||
                !ValidarEntero(txtDM6.Text) || !ValidarEntero(txtAStandard.Text) ||
                !ValidarEntero(txtAMin.Text) || !ValidarEntero(txtS.Text) ||
                !ValidarEntero(txtL.Text) || !ValidarEntero(txtL1.Text) ||
                !ValidarEntero(txtL2.Text) || !ValidarEntero(txtGear.Text) ||
                !ValidarEntero(txtCoupling.Text) || !ValidarEntero(txtPropeller.Text) ||
                !ValidarEntero(txtTunnel.Text) || !ValidarEntero(txtPerMeter.Text) ||
                !ValidarEntero(txtMotorFound.Text) || !ValidarEntero(txtOilVolumeGear.Text) ||
                !ValidarEntero(txtDNVK1dp.Text))
            {
                return false;
            }

            // Ejemplo de validación para campos numéricos decimales
            if (!ValidarDecimal(txtI.Text) || !ValidarDecimal(txtPullupPlateWheel.Text) ||
                !ValidarDecimal(txtPullupPropeller.Text))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que una cadena se pueda convertir a entero
        /// </summary>
        /// <param name="valor">Cadena a validar</param>
        /// <returns>True si es un entero válido, False en caso contrario</returns>
        private bool ValidarEntero(string valor)
        {
            return int.TryParse(valor, out _);
        }

        /// <summary>
        /// Valida que una cadena se pueda convertir a decimal (float)
        /// </summary>
        /// <param name="valor">Cadena a validar</param>
        /// <returns>True si es un decimal válido, False en caso contrario</returns>
        private bool ValidarDecimal(string valor)
        {
            return decimal.TryParse(valor, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _);
        }

        /// <summary>
        /// Crea un objeto BUType con los datos ingresados en el formulario
        /// </summary>
        /// <returns>Un nuevo objeto BUType con los datos del formulario</returns>
        private BUType CrearPropulsorDesdeFormulario()
        {
            return new BUType(
                designacion: txtDesignacion.Text,
                i_val: decimal.Parse(txtI.Text),
                n1_val: int.Parse(txtN1.Text),
                n2_val: int.Parse(txtN2.Text),
                b: int.Parse(txtB.Text),
                c: int.Parse(txtC.Text),
                d: int.Parse(txtD.Text),
                e: int.Parse(txtE.Text),
                f: int.Parse(txtF.Text),
                a_val: int.Parse(txtA.Text),
                d_m6_val: int.Parse(txtDM6.Text),
                a_standard: int.Parse(txtAStandard.Text),
                a_min: int.Parse(txtAMin.Text),
                s_val: int.Parse(txtS.Text),
                l: int.Parse(txtL.Text),
                l1_val: int.Parse(txtL1.Text),
                l2_val: int.Parse(txtL2.Text),
                gear: int.Parse(txtGear.Text),
                coupling: int.Parse(txtCoupling.Text),
                propeller: int.Parse(txtPropeller.Text),
                tunnel: int.Parse(txtTunnel.Text),
                per_meter: int.Parse(txtPerMeter.Text),
                motor_found: int.Parse(txtMotorFound.Text),
                oil_volume_gear: int.Parse(txtOilVolumeGear.Text),
                dnv_k1_dp: int.Parse(txtDNVK1dp.Text),
                pullup_plate_wheel: decimal.Parse(txtPullupPlateWheel.Text),
                pullup_propeller: decimal.Parse(txtPullupPropeller.Text),
                coupling_type: txtCouplingType.Text
            );
        }
    }
}