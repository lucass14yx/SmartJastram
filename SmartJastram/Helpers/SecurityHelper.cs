using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartJastram.Helpers
{
    public static class SecurityHelper
    {
        public static string CifraSHA(string cadena)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(cadena);

                // Sacar hash
                byte[] hash_bytes = sha512.ComputeHash(bytes);

                // Pasar a hexadecimal
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash_bytes)
                {
                    sb.Append(b.ToString("x2")); // Para hacerlo Hexadecimal
                }

                return sb.ToString();
            }
        }
    }
}
