using SmartJastram.Helpers;
using SmartJastram.Models;
using SmartJastram.Services.Managers;
using SmartJastram.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace SmartJastram.ViewModels
{
    public class PropulsoresPageViewModel : BaseViewModel
    {
        private Usuario _currentUser;
        private ObservableCollection<BUType> _propulsores;
        private BUType _selectedPropulsor;
        private readonly BUTypeManage _buTypeManager;

        public Usuario CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public ObservableCollection<BUType> Propulsores
        {
            get => _propulsores;
            set => SetProperty(ref _propulsores, value);
        }

        public BUType SelectedPropulsor
        {
            get => _selectedPropulsor;
            set
            {
                if (SetProperty(ref _selectedPropulsor, value))
                {
                    // Optionally, raise CanExecuteChanged for commands that depend on SelectedPropulsor
                    // if they are not using CommandParameter.
                }
            }
        }

        /// <summary>
        /// Determines if the current user has rights to manage propulsors (add, edit, delete).
        /// Bind to this property for UI element visibility (e.g., using a BooleanToVisibilityConverter).
        /// </summary>
        public bool CanManagePropulsores => CurrentUser != null && CurrentUser.RolID > 1; // RolID > 1 means  higher than Operario

        public ICommand NavigateToNewPropulsorCommand { get; }
        public ICommand EditPropulsorCommand { get; }
        public ICommand DeletePropulsorCommand { get; }
        public ICommand RefreshPropulsoresCommand { get; }
        public ICommand VerImagenCommand { get; }

        public event Action NavigateToNewPropulsorRequested;
        public event Action<BUType> EditPropulsorRequested;

        public PropulsoresPageViewModel(Usuario currentUser)
        {
            CurrentUser = currentUser;
            _buTypeManager = new BUTypeManage();
            Propulsores = new ObservableCollection<BUType>();

            NavigateToNewPropulsorCommand = new RelayCommand(ExecuteNavigateToNewPropulsor, CanExecuteNavigateToNewPropulsor);
            EditPropulsorCommand = new RelayCommand(ExecuteEditPropulsor, CanExecuteEditOrDeletePropulsor);
            DeletePropulsorCommand = new RelayCommand(ExecuteDeletePropulsor, CanExecuteEditOrDeletePropulsor);
            RefreshPropulsoresCommand = new RelayCommand(LoadPropulsores);
            VerImagenCommand = new RelayCommand(ExecuteVerImagen);

            LoadPropulsores(null); // Load initial data
            OnPropertyChanged(nameof(CanManagePropulsores)); // Notify that CanManagePropulsores might have changed
        }

        private void LoadPropulsores(object parameter)
        {
            try
            {

                var propulsoresList = _buTypeManager.SelectAll(); // Or similar method in BUTypeManage

                Propulsores.Clear();
                if (propulsoresList != null)
                {
                    foreach (var propulsor in propulsoresList)
                    {
                        Propulsores.Add(propulsor);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los propulsores: {ex.Message}", "Error de Carga", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteNavigateToNewPropulsor(object parameter)
        {
            return CanManagePropulsores;
        }

        private void ExecuteNavigateToNewPropulsor(object parameter)
        {
            NavigateToNewPropulsorRequested?.Invoke();
        }

        private bool CanExecuteEditOrDeletePropulsor(object parameter)
        {
            // This predicate works if the command parameter is the BUType instance from the DataGrid row.
            return CanManagePropulsores && parameter is BUType;
        }

        private void ExecuteEditPropulsor(object parameter)
        {
            if (parameter is BUType propulsorToEdit)
            {
                EditPropulsorRequested?.Invoke(propulsorToEdit);
            }
        }

        private void ExecuteDeletePropulsor(object parameter)
        {
            if (parameter is BUType propulsorToDelete)
            {
                var result = MessageBox.Show($"¿Está seguro que desea eliminar el propulsor '{propulsorToDelete.Designacion}'?",
                                           "Confirmar Eliminación",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _buTypeManager.Delete(propulsorToDelete); // Assumes BUTypeManage.Delete takes the object or its ID
                        Propulsores.Remove(propulsorToDelete);

                        if (SelectedPropulsor == propulsorToDelete)
                        {
                            SelectedPropulsor = null; // Clear selection if it was the deleted one
                        }
                        MessageBox.Show("Propulsor eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar el propulsor: {ex.Message}", "Error de Eliminación", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void ExecuteVerImagen(object parameter)
        {
            if (parameter is BUType propulsor)
            {
                WindowForImg imgWin = new WindowForImg(Base64Helper.FromBase64ToImage(propulsor.IMG),propulsor.Designacion);
                imgWin.Show();
                
            }
        }
    }
}