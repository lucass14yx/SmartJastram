using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using SmartJastram.Models;
using SmartJastram.Services.Managers;
using System.IO;

namespace SmartJastram.ViewModels
{
    public class NewPropulsorPageViewModel : BaseViewModel
    {
        private readonly BUTypeManage _manager;
        private readonly Usuario _currentUser;
        private BUType _propulsorToEdit;
        private bool _isEditMode;

        #region Propiedades de datos
        private string _designacion;
        public string Designacion
        {
            get => _designacion;
            set
            {
                _designacion = value;
                OnPropertyChanged();
            }
        }

        private decimal _i;
        public decimal I
        {
            get => _i;
            set
            {
                _i = value;
                OnPropertyChanged();
            }
        }

        private int _n1;
        public int N1
        {
            get => _n1;
            set
            {
                _n1 = value;
                OnPropertyChanged();
            }
        }

        private int _n2;
        public int N2
        {
            get => _n2;
            set
            {
                _n2 = value;
                OnPropertyChanged();
            }
        }

        private int _s;
        public int S
        {
            get => _s;
            set
            {
                _s = value;
                OnPropertyChanged();
            }
        }

        private int _l;
        public int L
        {
            get => _l;
            set
            {
                _l = value;
                OnPropertyChanged();
            }
        }

        private int _l1;
        public int L1
        {
            get => _l1;
            set
            {
                _l1 = value;
                OnPropertyChanged();
            }
        }

        private int _l2;
        public int L2
        {
            get => _l2;
            set
            {
                _l2 = value;
                OnPropertyChanged();
            }
        }

        private int _b;
        public int B
        {
            get => _b;
            set
            {
                _b = value;
                OnPropertyChanged();
            }
        }

        private int _c;
        public int C
        {
            get => _c;
            set
            {
                _c = value;
                OnPropertyChanged();
            }
        }

        private int _d;
        public int D
        {
            get => _d;
            set
            {
                _d = value;
                OnPropertyChanged();
            }
        }

        private int _e;
        public int E
        {
            get => _e;
            set
            {
                _e = value;
                OnPropertyChanged();
            }
        }

        private int _f;
        public int F
        {
            get => _f;
            set
            {
                _f = value;
                OnPropertyChanged();
            }
        }

        private int _a;
        public int A
        {
            get => _a;
            set
            {
                _a = value;
                OnPropertyChanged();
            }
        }

        private int _dM6;
        public int DM6
        {
            get => _dM6;
            set
            {
                _dM6 = value;
                OnPropertyChanged();
            }
        }

        private int _aStandard;
        public int AStandard
        {
            get => _aStandard;
            set
            {
                _aStandard = value;
                OnPropertyChanged();
            }
        }

        private int _aMin;
        public int AMin
        {
            get => _aMin;
            set
            {
                _aMin = value;
                OnPropertyChanged();
            }
        }

        private int _gear;
        public int Gear
        {
            get => _gear;
            set
            {
                _gear = value;
                OnPropertyChanged();
            }
        }

        private int _coupling;
        public int Coupling
        {
            get => _coupling;
            set
            {
                _coupling = value;
                OnPropertyChanged();
            }
        }

        private int _propeller;
        public int Propeller
        {
            get => _propeller;
            set
            {
                _propeller = value;
                OnPropertyChanged();
            }
        }

        private int _tunnel;
        public int Tunnel
        {
            get => _tunnel;
            set
            {
                _tunnel = value;
                OnPropertyChanged();
            }
        }

        private int _perMeter;
        public int PerMeter
        {
            get => _perMeter;
            set
            {
                _perMeter = value;
                OnPropertyChanged();
            }
        }

        private int _motorFound;
        public int MotorFound
        {
            get => _motorFound;
            set
            {
                _motorFound = value;
                OnPropertyChanged();
            }
        }

        private int _oilVolumeGear;
        public int OilVolumeGear
        {
            get => _oilVolumeGear;
            set
            {
                _oilVolumeGear = value;
                OnPropertyChanged();
            }
        }

        private int _dnvK1dp;
        public int DNVK1dp
        {
            get => _dnvK1dp;
            set
            {
                _dnvK1dp = value;
                OnPropertyChanged();
            }
        }

        private decimal _pullupPlateWheel;
        public decimal PullupPlateWheel
        {
            get => _pullupPlateWheel;
            set
            {
                _pullupPlateWheel = value;
                OnPropertyChanged();
            }
        }

        private decimal _pullupPropeller;
        public decimal PullupPropeller
        {
            get => _pullupPropeller;
            set
            {
                _pullupPropeller = value;
                OnPropertyChanged();
            }
        }

        private string _couplingType;
        public string CouplingType
        {
            get => _couplingType;
            set
            {
                _couplingType = value;
                OnPropertyChanged();
            }
        }
        private string _img;
        public string Img
        {
            get => _img;
            set
            {
                _img = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _imagenPropulsor;
        public BitmapImage ImagenPropulsor
        {
            get => _imagenPropulsor;
            set
            {
                _imagenPropulsor = value;
                OnPropertyChanged(nameof(ImagenPropulsor));
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
        public ICommand ChooseImageCommand { get; private set; }
        #endregion

        #region Eventos
        public event Action<Usuario> NavigateBackRequested;
        public event Action<Usuario, BUType> PropulsorSaved;
        #endregion

        public NewPropulsorPageViewModel(Usuario currentUser, BUType propulsorToEdit = null)
        {
            _currentUser = currentUser;
            _manager = new BUTypeManage();
            _propulsorToEdit = propulsorToEdit;
            _isEditMode = propulsorToEdit != null;

            // Inicializar comandos
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel);
            ChooseImageCommand = new RelayCommand(ExecuteChooseImage);

            // Configurar título de la página
            PageTitle = _isEditMode ? "Editar Propulsor" : "Nuevo Propulsor";

            // Si estamos en modo edición, cargar los datos del propulsor
            if (_isEditMode)
            {
                CargarDatosPropulsor();
            }
        }

        private void CargarDatosPropulsor()
        {
            if (_propulsorToEdit == null) return;

            Designacion = _propulsorToEdit.Designacion;
            I = _propulsorToEdit.i;
            N1 = _propulsorToEdit.n1;
            N2 = _propulsorToEdit.n2;
            S = _propulsorToEdit.s;
            L = _propulsorToEdit.L;
            L1 = _propulsorToEdit.l1;
            L2 = _propulsorToEdit.l2;
            B = _propulsorToEdit.B;
            C = _propulsorToEdit.C;
            D = _propulsorToEdit.D;
            E = _propulsorToEdit.E;
            F = _propulsorToEdit.F;
            A = _propulsorToEdit.a;
            DM6 = _propulsorToEdit.d_m6;
            AStandard = _propulsorToEdit.A_Standard;
            AMin = _propulsorToEdit.A_min;
            Gear = _propulsorToEdit.Gear;
            Coupling = _propulsorToEdit.Coupling;
            Propeller = _propulsorToEdit.Propeller;
            Tunnel = _propulsorToEdit.Tunnel;
            PerMeter = _propulsorToEdit.per_Meter;
            MotorFound = _propulsorToEdit.Motor_Found;
            OilVolumeGear = _propulsorToEdit.Oil_Volume_Gear;
            DNVK1dp = _propulsorToEdit.DNV_k1_dp;
            PullupPlateWheel = _propulsorToEdit.Pullup_Plate_Wheel;
            PullupPropeller = _propulsorToEdit.Pullup_Propeller;
            CouplingType = _propulsorToEdit.Coupling_type;
            Img = _propulsorToEdit.IMG;
        }

        private bool CanExecuteSave(object obj)
        {
            // Validar que los campos obligatorios no estén vacíos
            return !string.IsNullOrWhiteSpace(Designacion);
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

                // Crear el objeto BUType con los datos del formulario
                BUType propulsor = CrearPropulsorDesdeFormulario();

                if (_isEditMode)
                {
                    // Actualizar el propulsor existente
                    propulsor.ID = _propulsorToEdit.ID;
                    _manager.Modify(propulsor);
                    MessageBox.Show("Propulsor actualizado correctamente.",
                                  "Operación exitosa",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                }
                else
                {
                    // Insertar nuevo propulsor
                    _manager.Insert(propulsor);
                    MessageBox.Show("Propulsor guardado correctamente.",
                                  "Operación exitosa",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                }

                // Notificar que se ha guardado un propulsor
                PropulsorSaved?.Invoke(_currentUser, propulsor);
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
        private void ExecuteChooseImage(object obj)
        {
            
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Seleccionar Imagen",
                    Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        // Cargar la imagen seleccionada y mostrarla en el control Image
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(openFileDialog.FileName);
                        bitmap.EndInit();

                        byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                        Img = Convert.ToBase64String(imageBytes);

                        ImagenPropulsor = bitmap;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar la imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            
        }

        private bool ValidarCampos()
        {
            // Validar que el campo Designación no esté vacío
            if (string.IsNullOrWhiteSpace(Designacion))
            {
                return false;
            }

            // Aquí podrías agregar más validaciones específicas para cada campo
            // Por ejemplo, rangos válidos para valores numéricos, etc.

            return true;
        }

        private BUType CrearPropulsorDesdeFormulario()
        {
            return new BUType(
                designacion: Designacion,
                i_val: I,
                n1_val: N1,
                n2_val: N2,
                b: B,
                c: C,
                d: D,
                e: E,
                f: F,
                a_val: A,
                d_m6_val: DM6,
                a_standard: AStandard,
                a_min: AMin,
                s_val: S,
                l: L,
                l1_val: L1,
                l2_val: L2,
                gear: Gear,
                coupling: Coupling,
                propeller: Propeller,
                tunnel: Tunnel,
                per_meter: PerMeter,
                motor_found: MotorFound,
                oil_volume_gear: OilVolumeGear,
                dnv_k1_dp: DNVK1dp,
                pullup_plate_wheel: PullupPlateWheel,
                pullup_propeller: PullupPropeller,
                coupling_type: CouplingType,
                img: Img
            );
        }

        public Usuario CurrentUser => _currentUser;

        
    }

    
}