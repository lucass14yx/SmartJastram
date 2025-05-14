using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartJastram.Services.Managers;


namespace SmartJastram.Models
{
    /// <summary>
    /// Clase que representa un usuario del sistema
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Apellidos del usuario
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Número telefónico del usuario (9 dígitos)
        /// </summary>
        public int NumeroTelefonico { get; set; }

        /// <summary>
        /// Correo electrónico del usuario (debe ser único)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        public string Contraseña { get; set; }

        /// <summary>
        /// ID del rol asignado al usuario
        /// </summary>
        public int RolID { get; set; }

        public string RolTipo { get; set; }

        public string NombreCompleto { get; set; }


        public UsuarioManage um = new UsuarioManage();



        /// <summary>
        /// Constructor sin parámetros
        /// </summary>
        public Usuario()
        {
        }

        /// <summary>
        /// Constructor con parámetros básicos
        /// </summary>
        public Usuario(string nombre, string apellidos, int numeroTelefonico, string email, string contraseña, int rolID)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            NumeroTelefonico = numeroTelefonico;
            Email = email;
            Contraseña = contraseña;
            RolID = rolID;
            TipoRol();
            NombreCompleto = UneNombre();
        }

        /// <summary>
        /// Constructor con todos los parámetros incluyendo ID
        /// </summary>
        public Usuario(int id, string nombre, string apellidos, int numeroTelefonico, string email, string contraseña, int rolID)
        {
            ID = id;
            Nombre = nombre;
            Apellidos = apellidos;
            NumeroTelefonico = numeroTelefonico;
            Email = email;
            Contraseña = contraseña;
            RolID = rolID;
            TipoRol();
            NombreCompleto = UneNombre();
        }

        /// <summary>
        /// Devuelve el nombre completo del usuario
        /// </summary>
        public string UneNombre()
        {
            return $"{Nombre} {Apellidos}";
        }
        private void TipoRol()
        {
            if (RolID == 1)
            {
                RolTipo = "Operario";
            }
            else if (RolID == 2)
            {
                RolTipo = "Administrador";
            }
            else if (RolID == 3)
            {
                RolTipo = "Superadministrador";
            }
        }
        public ObservableCollection<Usuario> GetUsuarios()
        {
            return um.SelectAll();
        }
    }
}