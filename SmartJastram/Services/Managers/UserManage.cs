using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ReadingClub.persistence;
using SmartJastram.Helpers;
using SmartJastram.Models;

namespace SmartJastram.Services.Managers
{
    /// <summary>
    /// Clase encargada de gestionar las operaciones de base de datos para los usuarios
    /// </summary>
    public class UserManage
    {
        /// <summary>
        /// Obtiene todos los usuarios de la base de datos
        /// </summary>
        /// <returns>Lista de todos los usuarios</returns>
        public ObservableCollection<User> SelectAll()
        {
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT * FROM smartjastramapp.usuarios;");
            ObservableCollection<User> usuarios = new ObservableCollection<User>();

            foreach (List<Object> c in aux)
            {
                usuarios.Add(new User(
                    Convert.ToInt32(c[0]),     // ID
                    c[1].ToString(),          // Nombre
                    c[2].ToString(),          // Apellidos
                    Convert.ToInt32(c[3]),    // NumeroTelefonico
                    c[4].ToString(),          // Email
                    c[5].ToString(),          // Contraseña
                    Convert.ToInt32(c[6])     // RolID
                ));
            }
            return usuarios;
        }

        /// <summary>
        /// Obtiene el último ID usado en la tabla Usuarios
        /// </summary>
        /// <returns>El valor del último ID o 0 si no hay registros</returns>
        public int GetLastId()
        {
            int id = 0;
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT MAX(ID) FROM smartjastramapp.usuarios;");
            if (aux.Count > 0 && aux[0] is List<object> fila && fila.Count > 0 && fila[0] != DBNull.Value)
            {
                id = Convert.ToInt32(fila[0]);
            }
            return id;
        }

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="usuario">El objeto Usuario a insertar</param>
        /// <param name="contraseñaSinCifrar">Si es true, cifrará la contraseña antes de insertar</param>
        public void Insert(User usuario)
        {
            DBBroker dbBroker = DBBroker.obtenerAgente();
            string query = @"INSERT INTO smartjastramapp.usuarios 
                            (Nombre, Apellidos, NumeroTelefonico, Email, Contraseña, RolID) 
                            VALUES 
                            (@Nombre, @Apellidos, @NumeroTelefonico, @Email, @Contraseña, @RolID);";

            // Si la contraseña no está cifrada, aplicamos SHA512
            string contraseña = SecurityHelper.EncodeSHA(usuario.Contraseña);

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Nombre", usuario.Nombre},
                {"@Apellidos", usuario.Apellidos},
                {"@NumeroTelefonico", usuario.NumeroTelefonico},
                {"@Email", usuario.Email},
                {"@Contraseña", contraseña},
                {"@RolID", usuario.RolID}
            };

            dbBroker.modificar(query, parameters);
        }

        /// <summary>
        /// Actualiza un usuario existente en la base de datos
        /// </summary>
        /// <param name="usuario">El objeto Usuario con los datos actualizados</param>
        /// <param name="contraseñaSinCifrar">Si es true, cifrará la contraseña antes de actualizar</param>
        public void Modify(User usuario, Boolean hasNewPassword = true)
        {
            DBBroker dbBroker = DBBroker.obtenerAgente();
            string query = @"UPDATE smartjastramapp.usuarios SET 
                            Nombre = @Nombre, 
                            Apellidos = @Apellidos, 
                            NumeroTelefonico = @NumeroTelefonico, 
                            Email = @Email, 
                            Contraseña = @Contraseña, 
                            RolID = @RolID 
                            WHERE ID = @ID;";

            // Si la contraseña no está cifrada, aplicamos SHA512
            string contraseña = usuario.Contraseña;
            if (hasNewPassword)
            {
               contraseña = SecurityHelper.EncodeSHA(usuario.Contraseña);
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@ID", usuario.ID},
                {"@Nombre", usuario.Nombre},
                {"@Apellidos", usuario.Apellidos},
                {"@NumeroTelefonico", usuario.NumeroTelefonico},
                {"@Email", usuario.Email},
                {"@Contraseña", contraseña},
                {"@RolID", usuario.RolID}
            };

            dbBroker.modificar(query, parameters);
        }

        /// <summary>
        /// Elimina un usuario de la base de datos
        /// </summary>
        /// <param name="usuario">El objeto Usuario a eliminar</param>
        public void Delete(User usuario)
        {
            DBBroker dbBroker = DBBroker.obtenerAgente();
            string query = "DELETE FROM smartjastramapp.usuarios WHERE ID = @ID;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@ID", usuario.ID}
            };
            dbBroker.modificar(query, parameters);
        }

        /// <summary>
        /// Busca un usuario por su ID
        /// </summary>
        /// <param name="id">ID del usuario a buscar</param>
        /// <returns>El objeto Usuario o null si no se encuentra</returns>
        public User FindById(int id)
        {
            DBBroker dbBroker = DBBroker.obtenerAgente();
            string query = "SELECT * FROM smartjastramapp.usuarios WHERE ID = @ID;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@ID", id}
            };
            List<Object> aux = dbBroker.leer(query, parameters);

            if (aux.Count > 0 && aux[0] is List<object> c && c.Count > 0)
            {
                return new User(
                    Convert.ToInt32(c[0]),     // ID
                    c[1].ToString(),          // Nombre
                    c[2].ToString(),          // Apellidos
                    Convert.ToInt32(c[3]),    // NumeroTelefonico
                    c[4].ToString(),          // Email
                    c[5].ToString(),          // Contraseña
                    Convert.ToInt32(c[6])     // RolID
                );
            }
            return null;
        }

        /// <summary>
        /// Busca usuarios por email
        /// </summary>
        /// <param name="email">Email o parte de él para buscar</param>
        /// <returns>Lista de usuarios que coinciden con la búsqueda</returns>
        public List<User> FindByEmail(string email)
        {
            DBBroker dbBroker = DBBroker.obtenerAgente();
            string query = "SELECT * FROM smartjastramapp.usuarios WHERE Email LIKE @Email;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Email", $"%{email}%"}
            };
            List<Object> aux = dbBroker.leer(query, parameters);
            List<User> resultados = new List<User>();

            foreach (List<Object> c in aux)
            {
                resultados.Add(new User(
                    Convert.ToInt32(c[0]),     // ID
                    c[1].ToString(),          // Nombre
                    c[2].ToString(),          // Apellidos
                    Convert.ToInt32(c[3]),    // NumeroTelefonico
                    c[4].ToString(),          // Email
                    c[5].ToString(),          // Contraseña
                    Convert.ToInt32(c[6])     // RolID
                ));
            }
            return resultados;
        }

        /// <summary>
        /// Busca usuarios por nombre o apellidos
        /// </summary>
        /// <param name="nombre">Nombre o apellidos para buscar</param>
        /// <returns>Lista de usuarios que coinciden con la búsqueda</returns>
        public List<User> FindByName(string nombre)
        {
            DBBroker dbBroker = DBBroker.obtenerAgente();
            string query = @"SELECT * FROM smartjastramapp.usuarios 
                            WHERE Nombre LIKE @Nombre OR Apellidos LIKE @Apellidos;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Nombre", $"%{nombre}%"},
                {"@Apellidos", $"%{nombre}%"}
            };
            List<Object> aux = dbBroker.leer(query, parameters);
            List<User> resultados = new List<User>();

            foreach (List<Object> c in aux)
            {
                resultados.Add(new User(
                    Convert.ToInt32(c[0]),     // ID
                    c[1].ToString(),          // Nombre
                    c[2].ToString(),          // Apellidos
                    Convert.ToInt32(c[3]),    // NumeroTelefonico
                    c[4].ToString(),          // Email
                    c[5].ToString(),          // Contraseña
                    Convert.ToInt32(c[6])     // RolID
                ));
            }
            return resultados;
        }

        /// <summary>
        /// Verifica las credenciales de un usuario
        /// </summary>
        /// <param name="email">Email del usuario</param>
        /// <param name="contraseña">Contraseña del usuario (ya cifrada con SHA512)</param>
        /// <returns>El objeto Usuario si las credenciales son válidas, null en caso contrario</returns>
        public User Autenticar(string email, string contraseña)
        {
            DBBroker dbBroker = DBBroker.obtenerAgente();
            string query = "SELECT * FROM smartjastramapp.usuarios WHERE Email = @Email AND Contraseña = @Contraseña;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Email", email},
                {"@Contraseña", contraseña}
            };
            List<Object> aux = dbBroker.leer(query, parameters);

            if (aux.Count > 0 && aux[0] is List<object> c && c.Count > 0)
            {
                return new User(
                    Convert.ToInt32(c[0]),     // ID
                    c[1].ToString(),          // Nombre
                    c[2].ToString(),          // Apellidos
                    Convert.ToInt32(c[3]),    // NumeroTelefonico
                    c[4].ToString(),          // Email
                    c[5].ToString(),          // Contraseña
                    Convert.ToInt32(c[6])     // RolID
                );
            }
            return null;
        }
    }
}