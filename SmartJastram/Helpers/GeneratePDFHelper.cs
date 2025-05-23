using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelectPdf;
using SmartJastram.Models;
using SmartJastram.Properties;

namespace SmartJastram.Helpers
{
    public static class GeneratePDFHelper
    {
        
        public static string GeneratePdfAsBase64_butypes(BUType bu)
        {
            string logoPath = "SmartJastram.Resources.jastram_logo.png";
            try
            {
                // Leer la plantilla HTML
                string htmlResourceName = "SmartJastram.Resources.HtmlTemplates.BU_type.html";
                string htmlTemplate = ReadEmbeddedResource(htmlResourceName); ;

                string htmlContent = htmlTemplate
                                    .Replace("@{triggerBody()?['BUType']}", bu.Designacion)
                                    .Replace("@{triggerBody()?['GearRatio']}", bu.i.ToString())
                                    .Replace("@{triggerBody()?['N1Value']}", bu.n1.ToString())
                                    .Replace("@{triggerBody()?['N2Value']}", bu.n2.ToString())
                                    .Replace("@{triggerBody()?['B']}", bu.B.ToString())
                                    .Replace("@{triggerBody()?['C']}", bu.C.ToString())
                                    .Replace("@{triggerBody()?['D']}", bu.D.ToString())
                                    .Replace("@{triggerBody()?['E']}", bu.E.ToString())
                                    .Replace("@{triggerBody()?['F']}", bu.F.ToString())
                                    .Replace("@{triggerBody()?['a']}", bu.a.ToString())
                                    .Replace("@{triggerBody()?['A_Standard']}", bu.A_Standard.ToString())
                                    .Replace("@{triggerBody()?['A_Min']}", bu.A_min.ToString())
                                    .Replace("@{triggerBody()?['s']}", bu.s.ToString())
                                    .Replace("@{triggerBody()?['L']}", bu.L.ToString())
                                    .Replace("@{triggerBody()?['l1']}", bu.l1.ToString())
                                    .Replace("@{triggerBody()?['l2']}", bu.l2.ToString())
                                    .Replace("@{triggerBody()?['Gear']}", bu.Gear.ToString())
                                    .Replace("@{triggerBody()?['Coupling']}", bu.Coupling.ToString())
                                    .Replace("@{triggerBody()?['Propeller']}", bu.Propeller.ToString())
                                    .Replace("@{triggerBody()?['Tunnnel']}", bu.Tunnel.ToString()) // Corrige "Tunnnel" en la plantilla si es necesario
                                    .Replace("@{triggerBody()?['per_Meter']}", bu.per_Meter.ToString())
                                    .Replace("@{triggerBody()?['Motor_Found']}", bu.Motor_Found.ToString())
                                    .Replace("@{triggerBody()?['Oil_Volume_Gear']}", bu.Oil_Volume_Gear.ToString())
                                    .Replace("@{triggerBody()?['DNV_k1_dp']}", bu.DNV_k1_dp.ToString())
                                    .Replace("@{triggerBody()?['Pullup_Plate_Wheel']}", bu.Pullup_Plate_Wheel.ToString())
                                    .Replace("@{triggerBody()?['Pullup_Propeller']}", bu.Pullup_Propeller.ToString())
                                    .Replace("@{formatDateTime(utcNow(),'dd.MM.yyyy')}", DateTime.UtcNow.ToString("dd.MM.yyyy"))
                                    .Replace("@{dataUri(body('Get_file_content_(JastramLogo)'))}", ConvertEmbeddedImageToDataUri(logoPath, "image/png"));
                // Configurar SelectPdf para convertir HTML a PDF
                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.PdfPageSize = PdfPageSize.A4;
                converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                

                // Convertir HTML a PDF y obtener los bytes
                PdfDocument pdf = converter.ConvertHtmlString(htmlContent);
                byte[] pdfBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    pdf.Save(ms);
                    pdfBytes = ms.ToArray();
                }
                pdf.Close();

                // Convertir el PDF a Base64
                return Convert.ToBase64String(pdfBytes);



            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar el PDF: {ex.Message}");
            }
        }
        private static string ReadEmbeddedResource(string resourceName)
        {
            var assembly = typeof(GeneratePDFHelper).Assembly;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new FileNotFoundException($"No se encontró el recurso embebido: {resourceName}");

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        private static string ConvertEmbeddedImageToDataUri(string resourceName, string mimeType)
        {
            var assembly = typeof(GeneratePDFHelper).Assembly;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new FileNotFoundException($"No se encontró la imagen embebida: {resourceName}");

                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    return $"data:{mimeType};base64,{base64}";
                }
            }
        }
        private static void SavePdfToDatabase(string pdfBase64)
        {
            
        }
    }
}
