using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para WindowForPDF.xaml
    /// </summary>
    public partial class WindowForPDF : Window
    {
        private byte[] _pdfBytes;
        private string _tempFilePath;

        public WindowForPDF()
        {
            InitializeComponent();
            this.Loaded += WindowForPDF_Loaded;
            this.Closed += WindowForPDF_Closed;
        }

        public WindowForPDF(string pdfBase64, string titulo = "Documento PDF") : this()
        {
            this.Title = titulo;
            _pdfBytes = Convert.FromBase64String(pdfBase64);
        }

        public WindowForPDF(byte[] pdfBytes, string titulo = "Documento PDF") : this()
        {
            this.Title = titulo;
            _pdfBytes = pdfBytes;
            InitializeComponent();
            this.Loaded += WindowForPDF_Loaded;
            this.Closed += WindowForPDF_Closed;
        }

        private void WindowForPDF_Loaded(object sender, RoutedEventArgs e)
        {
            if (_pdfBytes != null)
            {
                LoadPdfFromBytes(_pdfBytes);
            }
        }

        private void WindowForPDF_Closed(object sender, System.EventArgs e)
        {
            // Limpiar archivo temporal al cerrar la ventana
            CleanupTempFile();
        }

        private void LoadPdfFromBytes(byte[] pdfBytes)
        {
            try
            {
                // Crear un archivo temporal para el PDF
                _tempFilePath = Path.Combine(Path.GetTempPath(), $"temp_pdf_{Guid.NewGuid()}.pdf");
                File.WriteAllBytes(_tempFilePath, pdfBytes);

                // Navegar al archivo PDF en el WebBrowser
                pdfViewer.Navigate(_tempFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el PDF: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CleanupTempFile()
        {
            try
            {
                if (!string.IsNullOrEmpty(_tempFilePath) && File.Exists(_tempFilePath))
                {
                    File.Delete(_tempFilePath);
                }
            }
            catch (Exception ex)
            {
                // Log error silently - no need to show error to user for cleanup
                System.Diagnostics.Debug.WriteLine($"Error al eliminar archivo temporal: {ex.Message}");
            }
        }

        private void BtnSavePdf_Click(object sender, RoutedEventArgs e)
        {
            if (_pdfBytes == null)
            {
                MessageBox.Show("No hay datos PDF para guardar.", "Advertencia",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Archivos PDF|*.pdf",
                DefaultExt = "pdf",
                FileName = "documento.pdf"
            };

            if (saveDialog.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllBytes(saveDialog.FileName, _pdfBytes);
                    MessageBox.Show($"PDF guardado exitosamente en:\n{saveDialog.FileName}",
                        "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar el PDF: {ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CmbZoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // El zoom se maneja a través del navegador web interno
            // Esta funcionalidad puede requerir JavaScript para controlar el zoom del PDF
            if (pdfViewer != null && pdfViewer.Document != null)
            {
                try
                {
                    string zoomLevel = ((ComboBoxItem)cmbZoom.SelectedItem).Content.ToString().Replace("%", "");
                    string script = $"document.body.style.zoom = '{int.Parse(zoomLevel)}%';";
                    pdfViewer.InvokeScript("eval", script);
                }
                catch (Exception ex)
                {
                    // Zoom control might not work in all cases
                    System.Diagnostics.Debug.WriteLine($"Error al aplicar zoom: {ex.Message}");
                }
            }
        }
    }
}