using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace SmartJastram.Helpers
{
    public static class Base64Helper
    {
       
        /// <summary>
        /// Converts a Base64 string back to a byte array.
        /// </summary>
        /// <param name="base64String">The Base64 string to convert.</param>
        /// <returns>A byte array representation of the Base64 string.</returns>
        private static byte[] FromBase64String(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        public static BitmapImage FromBase64ToImage(string base64String)
        {
            try
            {
                byte[] imageBytes = FromBase64String(base64String);
                var stream = new MemoryStream(imageBytes);

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();

                return image;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to convert Base64 to image", ex);
            }
        }
        public static byte[] FromBase64toPDF(string base64String)
        {
            try
            {
                if (string.IsNullOrEmpty(base64String))
                {
                    throw new ArgumentException("La cadena Base64 no puede estar vacía.");
                }

                // Convertir Base64 a bytes
                return FromBase64String(base64String);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al convertir Base64 a PDF: {ex.Message}");
            }
        }



    }
}
