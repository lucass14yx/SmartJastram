using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SmartJastram.Views
{
    /// <summary>
    /// Lógica de interacción para WindowForImg.xaml
    /// </summary>
    public partial class WindowForImg : Window
    {
        public WindowForImg()
        {
            InitializeComponent();
        }

        public WindowForImg(BitmapImage imagen, string titulo = "Imagen") : this()
        {
            this.Title = titulo;
            imgVisor.Source = imagen;
        }
    }
}