using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using SmartJastram.Views;


namespace SmartJastram
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //// Configurar la cultura para usar coma como separador decimal
            //CultureInfo culture = new CultureInfo("es-ES");
            //culture.NumberFormat.NumberDecimalSeparator = ",";
            //culture.NumberFormat.NumberGroupSeparator = ".";

            //// Aplicar la cultura a toda la aplicación
            //Thread.CurrentThread.CurrentCulture = culture;
            //Thread.CurrentThread.CurrentUICulture = culture;
            //CultureInfo.DefaultThreadCurrentCulture = culture;
            //CultureInfo.DefaultThreadCurrentUICulture = culture;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            base.OnStartup(e);

            
        }
    }
}
