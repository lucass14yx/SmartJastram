using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadingClub.persistence;
using SmartJastram.Models;

namespace SmartJastram.Services.Managers
{
    /// <summary>
    /// Clase encargada de gestionar las operaciones de base de datos para los tipos de propulsores (BUType)
    /// </summary>
    public class BUTypeManage
    {
        /// <summary>
        /// Obtiene todos los tipos de propulsores de la base de datos
        /// </summary>
        /// <returns>Lista de todos los tipos de propulsores</returns>
        public List<BUType> SelectAll()
        {
            BUType buType = null;
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT * FROM smartjastramapp.butypes;");
            List<BUType> buTypes = new List<BUType>();

            foreach (List<Object> c in aux)
            {
                buType = new BUType(
                    Convert.ToInt32(c[0]),             // ID
                    c[1].ToString(),                   // Designacion
                    Convert.ToDecimal(c[2]),           // i
                    Convert.ToInt32(c[3]),             // n1
                    Convert.ToInt32(c[4]),             // n2
                    Convert.ToInt32(c[5]),             // B
                    Convert.ToInt32(c[6]),             // C
                    Convert.ToInt32(c[7]),             // D
                    Convert.ToInt32(c[8]),             // E
                    Convert.ToInt32(c[9]),             // F
                    Convert.ToInt32(c[10]),            // a
                    Convert.ToInt32(c[11]),            // d_m6
                    Convert.ToInt32(c[12]),            // A_Standard
                    Convert.ToInt32(c[13]),            // A_min
                    Convert.ToInt32(c[14]),            // s
                    Convert.ToInt32(c[15]),            // L
                    Convert.ToInt32(c[16]),            // l1
                    Convert.ToInt32(c[17]),            // l2
                    Convert.ToInt32(c[18]),            // Gear
                    Convert.ToInt32(c[19]),            // Coupling
                    Convert.ToInt32(c[20]),            // Propeller
                    Convert.ToInt32(c[21]),            // Tunnel
                    Convert.ToInt32(c[22]),            // per_Meter
                    Convert.ToInt32(c[23]),            // Motor_Found
                    Convert.ToInt32(c[24]),            // Oil_Volume_Gear
                    Convert.ToInt32(c[25]),            // DNV_k1_dp
                    Convert.ToDecimal(c[26]),           // Pullup_Plate_Wheel
                    Convert.ToDecimal(c[27]),           // Pullup_Propeller
                    c[28].ToString()                   // Coupling_type
                );
                buTypes.Add(buType);
            }
            return buTypes;
        }

        /// <summary>
        /// Obtiene el último ID usado en la tabla BUType
        /// </summary>
        /// <returns>El valor del último ID o 0 si no hay registros</returns>
        public int GetLastId()
        {
            int id = 0;
            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT MAX(ID) FROM smartjastramapp.butypes;");
            if (aux.Count > 0 && aux[0] is List<object> fila && fila.Count > 0 && fila[0] != DBNull.Value)
            {
                id = Convert.ToInt32(fila[0]);
            }
            return id;
        }

        /// <summary>
        /// Inserta un nuevo tipo de propulsor en la base de datos
        /// </summary>
        /// <param name="buType">El objeto BUType a insertar</param>
        public void Insert(BUType buType)
        {
            DBBroker dbBroker = DBBroker.obtenerAgente();
            string query = "INSERT INTO smartjastramapp.butypes (Designacion, i, n1, n2, B, C, D, E, F, a, d_m6, A_Standard, " +
                           "A_min, s, L, l1, l2, Gear, Coupling, Propeller, Tunnel, per_Meter, Motor_Found, Oil_Volume_Gear, " +
                           "DNV_k1_dp, Pullup_Plate_Wheel, Pullup_Propeller, Coupling_type) VALUES (" +
                           "@Designacion, @i, @n1, @n2, @B, @C, @D, @E, @F, @a, @d_m6, @A_Standard, " +
                           "@A_min, @s, @L, @l1, @l2, @Gear, @Coupling, @Propeller, @Tunnel, @per_Meter, @Motor_Found, " +
                           "@Oil_Volume_Gear, @DNV_k1_dp, @Pullup_Plate_Wheel, @Pullup_Propeller, @Coupling_type);";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Designacion", buType.Designacion},
                {"@i", buType.i},
                {"@n1", buType.n1},
                {"@n2", buType.n2},
                {"@B", buType.B},
                {"@C", buType.C},
                {"@D", buType.D},
                {"@E", buType.E},
                {"@F", buType.F},
                {"@a", buType.a},
                {"@d_m6", buType.d_m6},
                {"@A_Standard", buType.A_Standard},
                {"@A_min", buType.A_min},
                {"@s", buType.s},
                {"@L", buType.L},
                {"@l1", buType.l1},
                {"@l2", buType.l2},
                {"@Gear", buType.Gear},
                {"@Coupling", buType.Coupling},
                {"@Propeller", buType.Propeller},
                {"@Tunnel", buType.Tunnel},
                {"@per_Meter", buType.per_Meter},
                {"@Motor_Found", buType.Motor_Found},
                {"@Oil_Volume_Gear", buType.Oil_Volume_Gear},
                {"@DNV_k1_dp", buType.DNV_k1_dp},
                {"@Pullup_Plate_Wheel", buType.Pullup_Plate_Wheel},
                {"@Pullup_Propeller", buType.Pullup_Propeller},
                {"@Coupling_type", buType.Coupling_type}
            };

            dbBroker.modificar(query, parameters);
        }

        /// <summary>
        /// Modifica un tipo de propulsor existente en la base de datos
        /// </summary>
        /// <param name="buType">El objeto BUType con los datos actualizados</param>
        public void Modify(BUType buType)
        {
            DBBroker dbBroker = DBBroker.obtenerAgente();

            string query = "UPDATE smartjastramapp.butypes SET " +
                          "Designacion = @Designacion, " +
                          "i = @i, " +
                          "n1 = @n1, " +
                          "n2 = @n2, " +
                          "B = @B, " +
                          "C = @C, " +
                          "D = @D, " +
                          "E = @E, " +
                          "F = @F, " +
                          "a = @a, " +
                          "d_m6 = @d_m6, " +
                          "A_Standard = @A_Standard, " +
                          "A_min = @A_min, " +
                          "s = @s, " +
                          "L = @L, " +
                          "l1 = @l1, " +
                          "l2 = @l2, " +
                          "Gear = @Gear, " +
                          "Coupling = @Coupling, " +
                          "Propeller = @Propeller, " +
                          "Tunnel = @Tunnel, " +
                          "per_Meter = @per_Meter, " +
                          "Motor_Found = @Motor_Found, " +
                          "Oil_Volume_Gear = @Oil_Volume_Gear, " +
                          "DNV_k1_dp = @DNV_k1_dp, " +
                          "Pullup_Plate_Wheel = @Pullup_Plate_Wheel, " +
                          "Pullup_Propeller = @Pullup_Propeller, " +
                          "Coupling_type = @Coupling_type " +
                          "WHERE ID = @ID;";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Designacion", buType.Designacion},
                {"@i", buType.i},
                {"@n1", buType.n1},
                {"@n2", buType.n2},
                {"@B", buType.B},
                {"@C", buType.C},
                {"@D", buType.D},
                {"@E", buType.E},
                {"@F", buType.F},
                {"@a", buType.a},
                {"@d_m6", buType.d_m6},
                {"@A_Standard", buType.A_Standard},
                {"@A_min", buType.A_min},
                {"@s", buType.s},
                {"@L", buType.L},
                {"@l1", buType.l1},
                {"@l2", buType.l2},
                {"@Gear", buType.Gear},
                {"@Coupling", buType.Coupling},
                {"@Propeller", buType.Propeller},
                {"@Tunnel", buType.Tunnel},
                {"@per_Meter", buType.per_Meter},
                {"@Motor_Found", buType.Motor_Found},
                {"@Oil_Volume_Gear", buType.Oil_Volume_Gear},
                {"@DNV_k1_dp", buType.DNV_k1_dp},
                {"@Pullup_Plate_Wheel", buType.Pullup_Plate_Wheel},
                {"@Pullup_Propeller", buType.Pullup_Propeller},
                {"@Coupling_type", buType.Coupling_type},
                {"@ID", buType.ID}
            };

            dbBroker.modificar(query, parameters);
        }

        /// <summary>
        /// Elimina un tipo de propulsor de la base de datos
        /// </summary>
        /// <param name="buType">El objeto BUType a eliminar</param>
        public void Delete(BUType buType)
        {
            DBBroker dbBroker = DBBroker.obtenerAgente();
            string query = "DELETE FROM smartjastramapp.butypes WHERE ID = @ID;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@ID", buType.ID}
            };
            dbBroker.modificar(query, parameters);
        }

        /// <summary>
        /// Busca un tipo de propulsor por su ID
        /// </summary>
        /// <param name="id">ID del propulsor a buscar</param>
        /// <returns>El objeto BUType o null si no se encuentra</returns>
        public BUType FindById(int id)
        {
            DBBroker dbBroker = DBBroker.obtenerAgente();
            string query = "SELECT * FROM smartjastramapp.butypes WHERE ID = @ID;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@ID", id}
            };
            List<Object> aux = dbBroker.leer(query, parameters);

            if (aux.Count > 0 && aux[0] is List<object> c && c.Count > 0)
            {
                return new BUType(
                    Convert.ToInt32(c[0]),             // ID
                    c[1].ToString(),                   // Designacion
                    Convert.ToDecimal(c[2]),           // i
                    Convert.ToInt32(c[3]),             // n1
                    Convert.ToInt32(c[4]),             // n2
                    Convert.ToInt32(c[5]),             // B
                    Convert.ToInt32(c[6]),             // C
                    Convert.ToInt32(c[7]),             // D
                    Convert.ToInt32(c[8]),             // E
                    Convert.ToInt32(c[9]),             // F
                    Convert.ToInt32(c[10]),            // a
                    Convert.ToInt32(c[11]),            // d_m6
                    Convert.ToInt32(c[12]),            // A_Standard
                    Convert.ToInt32(c[13]),            // A_min
                    Convert.ToInt32(c[14]),            // s
                    Convert.ToInt32(c[15]),            // L
                    Convert.ToInt32(c[16]),            // l1
                    Convert.ToInt32(c[17]),            // l2
                    Convert.ToInt32(c[18]),            // Gear
                    Convert.ToInt32(c[19]),            // Coupling
                    Convert.ToInt32(c[20]),            // Propeller
                    Convert.ToInt32(c[21]),            // Tunnel
                    Convert.ToInt32(c[22]),            // per_Meter
                    Convert.ToInt32(c[23]),            // Motor_Found
                    Convert.ToInt32(c[24]),            // Oil_Volume_Gear
                    Convert.ToInt32(c[25]),            // DNV_k1_dp
                    Convert.ToDecimal(c[26]),           // Pullup_Plate_Wheel
                    Convert.ToDecimal(c[27]),           // Pullup_Propeller
                    c[28].ToString()                   // Coupling_type
                );
            }

            return null;
        }

        /// <summary>
        /// Busca tipos de propulsores por su designación
        /// </summary>
        /// <param name="designacion">Designación o parte de ella para buscar</param>
        /// <returns>Lista de objetos BUType que coinciden con la búsqueda</returns>
        public List<BUType> FindByDesignacion(string designacion)
        {
            List<BUType> results = new List<BUType>();
            DBBroker dbBroker = DBBroker.obtenerAgente();
            string query = "SELECT * FROM smartjastramapp.butypes WHERE Designacion LIKE @Designacion;";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Designacion", $"%{designacion}%"}
            };
            List<Object> aux = dbBroker.leer(query, parameters);

            foreach (List<Object> c in aux)
            {
                BUType buType = new BUType(
                    Convert.ToInt32(c[0]),             // ID
                    c[1].ToString(),                   // Designacion
                    Convert.ToDecimal(c[2]),           // i
                    Convert.ToInt32(c[3]),             // n1
                    Convert.ToInt32(c[4]),             // n2
                    Convert.ToInt32(c[5]),             // B
                    Convert.ToInt32(c[6]),             // C
                    Convert.ToInt32(c[7]),             // D
                    Convert.ToInt32(c[8]),             // E
                    Convert.ToInt32(c[9]),             // F
                    Convert.ToInt32(c[10]),            // a
                    Convert.ToInt32(c[11]),            // d_m6
                    Convert.ToInt32(c[12]),            // A_Standard
                    Convert.ToInt32(c[13]),            // A_min
                    Convert.ToInt32(c[14]),            // s
                    Convert.ToInt32(c[15]),            // L
                    Convert.ToInt32(c[16]),            // l1
                    Convert.ToInt32(c[17]),            // l2
                    Convert.ToInt32(c[18]),            // Gear
                    Convert.ToInt32(c[19]),            // Coupling
                    Convert.ToInt32(c[20]),            // Propeller
                    Convert.ToInt32(c[21]),            // Tunnel
                    Convert.ToInt32(c[22]),            // per_Meter
                    Convert.ToInt32(c[23]),            // Motor_Found
                    Convert.ToInt32(c[24]),            // Oil_Volume_Gear
                    Convert.ToInt32(c[25]),            // DNV_k1_dp
                    Convert.ToDecimal(c[26]),           // Pullup_Plate_Wheel
                    Convert.ToDecimal(c[27]),           // Pullup_Propeller
                    c[28].ToString()                   // Coupling_type
                );
                results.Add(buType);
            }

            return results;
        }
    }
}
