using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ReadingClub.persistence
{
    public class DBBroker
    {
        private static DBBroker _instancia;
        private static MySqlConnection conexion;
        private const String cadenaConexion = "server=localhost;database=smartjastramapp;uid=root;pwd=toor";

        private DBBroker()
        {
            DBBroker.conexion = new MySqlConnection(DBBroker.cadenaConexion);
        }

        public static DBBroker obtenerAgente()
        {
            if (DBBroker._instancia == null)
            {
                DBBroker._instancia = new DBBroker();
            }
            return DBBroker._instancia;
        }

        // Método original para compatibilidad
        public List<Object> leer(String sql)
        {
            return leer(sql, null);
        }

        // Nuevo método con parámetros
        public List<Object> leer(String sql, Dictionary<string, object> parametros)
        {
            List<Object> resultado = new List<object>();

            try
            {
                conectar();
                using (MySqlCommand com = new MySqlCommand(sql, conexion))
                {
                    if (parametros != null)
                    {
                        foreach (var param in parametros)
                        {
                            com.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    using (MySqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<Object> fila = new List<object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                fila.Add(reader.IsDBNull(i) ? null : reader.GetValue(i));
                            }
                            resultado.Add(fila);
                        }
                    }
                }
            }
            finally
            {
                desconectar();
            }

            return resultado;
        }

        // Método original para compatibilidad
        public int modificar(String sql)
        {
            return modificar(sql, null);
        }

        // Nuevo método con parámetros
        public int modificar(String sql, Dictionary<string, object> parametros)
        {
            int resultado = 0;

            try
            {
                conectar();
                using (MySqlCommand com = new MySqlCommand(sql, conexion))
                {
                    if (parametros != null)
                    {
                        foreach (var param in parametros)
                        {
                            com.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    resultado = com.ExecuteNonQuery();
                }
            }
            finally
            {
                desconectar();
            }

            return resultado;
        }

        private void conectar()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
        }

        private void desconectar()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}