using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartJastram.Services.Managers;



namespace SmartJastram.Models
{
    /// <summary>
    /// Clase que representa los datos técnicos de los diferentes modelos de propulsores.
    /// </summary>
    public class BUType
    {
 
        /// <summary>
        /// Identificador único del propulsor
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Designación o nombre corto del propulsor
        /// </summary>
        public string Designacion { get; set; }

        /// <summary>
        /// Valor i del propulsor (4 cifras, 3 decimales)
        /// </summary>
        public decimal i { get; set; }

        /// <summary>
        /// Valor n1 del propulsor
        /// </summary>
        public int n1 { get; set; }

        /// <summary>
        /// Valor n2 del propulsor
        /// </summary>
        public int n2 { get; set; }

        /// <summary>
        /// Valor B del propulsor
        /// </summary>
        public int B { get; set; }

        /// <summary>
        /// Valor C del propulsor
        /// </summary>
        public int C { get; set; }

        /// <summary>
        /// Valor D del propulsor
        /// </summary>
        public int D { get; set; }

        /// <summary>
        /// Valor E del propulsor
        /// </summary>
        public int E { get; set; }

        /// <summary>
        /// Valor F del propulsor
        /// </summary>
        public int F { get; set; }

        /// <summary>
        /// Valor a del propulsor
        /// </summary>
        public int a { get; set; }

        /// <summary>
        /// Valor d(m6) del propulsor
        /// </summary>
        public int d_m6 { get; set; }

        /// <summary>
        /// Valor A Standard del propulsor
        /// </summary>
        public int A_Standard { get; set; }

        /// <summary>
        /// Valor A min del propulsor
        /// </summary>
        public int A_min { get; set; }

        /// <summary>
        /// Valor s del propulsor
        /// </summary>
        public int s { get; set; }

        /// <summary>
        /// Valor L del propulsor
        /// </summary>
        public int L { get; set; }

        /// <summary>
        /// Valor l1 del propulsor
        /// </summary>
        public int l1 { get; set; }

        /// <summary>
        /// Valor l2 del propulsor
        /// </summary>
        public int l2 { get; set; }

        /// <summary>
        /// Valor Gear del propulsor
        /// </summary>
        public int Gear { get; set; }

        /// <summary>
        /// Valor Coupling del propulsor
        /// </summary>
        public int Coupling { get; set; }

        /// <summary>
        /// Valor Propeller del propulsor
        /// </summary>
        public int Propeller { get; set; }

        /// <summary>
        /// Valor Tunnel del propulsor
        /// </summary>
        public int Tunnel { get; set; }

        /// <summary>
        /// Valor per Meter del propulsor
        /// </summary>
        public int per_Meter { get; set; }

        /// <summary>
        /// Valor Motor Found del propulsor
        /// </summary>
        public int Motor_Found { get; set; }

        /// <summary>
        /// Valor Oil Volume Gear del propulsor
        /// </summary>
        public int Oil_Volume_Gear { get; set; }

        /// <summary>
        /// Valor DNV k=1 dp del propulsor
        /// </summary>
        public int DNV_k1_dp { get; set; }

        /// <summary>
        /// Valor Pull-up Plate Wheel del propulsor (3 cifras, 2 decimales)
        /// </summary>
        public decimal Pullup_Plate_Wheel { get; set; }

        /// <summary>
        /// Valor Pull-up Propeller del propulsor (3 cifras, 2 decimales)
        /// </summary>
        public decimal Pullup_Propeller { get; set; }

        /// <summary>
        /// Tipo de acoplamiento del propulsor
        /// </summary>
        public string Coupling_type { get; set; }

        /// <summary>
        /// Constructor sin parámetros
        /// </summary>
        


        private BUTypeManage butm = new BUTypeManage();

        public BUType()
        {
        }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        public BUType(int id, string designacion, decimal i_val, int n1_val, int n2_val, int b, int c, int d, int e, int f,
                    int a_val, int d_m6_val, int a_standard, int a_min, int s_val, int l, int l1_val, int l2_val,
                    int gear, int coupling, int propeller, int tunnel, int per_meter, int motor_found, int oil_volume_gear,
                    int dnv_k1_dp, decimal pullup_plate_wheel, decimal pullup_propeller, string coupling_type)
        {
            ID = id;
            Designacion = designacion;
            i = i_val;
            n1 = n1_val;
            n2 = n2_val;
            B = b;
            C = c;
            D = d;
            E = e;
            F = f;
            a = a_val;
            d_m6 = d_m6_val;
            A_Standard = a_standard;
            A_min = a_min;
            s = s_val;
            L = l;
            l1 = l1_val;
            l2 = l2_val;
            Gear = gear;
            Coupling = coupling;
            Propeller = propeller;
            Tunnel = tunnel;
            per_Meter = per_meter;
            Motor_Found = motor_found;
            Oil_Volume_Gear = oil_volume_gear;
            DNV_k1_dp = dnv_k1_dp;
            Pullup_Plate_Wheel = pullup_plate_wheel;
            Pullup_Propeller = pullup_propeller;
            Coupling_type = coupling_type;
        }
        public BUType(string designacion, decimal i_val, int n1_val, int n2_val, int b, int c, int d, int e, int f,
                   int a_val, int d_m6_val, int a_standard, int a_min, int s_val, int l, int l1_val, int l2_val,
                   int gear, int coupling, int propeller, int tunnel, int per_meter, int motor_found, int oil_volume_gear,
                   int dnv_k1_dp, decimal pullup_plate_wheel, decimal pullup_propeller, string coupling_type)
        {
            Designacion = designacion;
            i = i_val;
            n1 = n1_val;
            n2 = n2_val;
            B = b;
            C = c;
            D = d;
            E = e;
            F = f;
            a = a_val;
            d_m6 = d_m6_val;
            A_Standard = a_standard;
            A_min = a_min;
            s = s_val;
            L = l;
            l1 = l1_val;
            l2 = l2_val;
            Gear = gear;
            Coupling = coupling;
            Propeller = propeller;
            Tunnel = tunnel;
            per_Meter = per_meter;
            Motor_Found = motor_found;
            Oil_Volume_Gear = oil_volume_gear;
            DNV_k1_dp = dnv_k1_dp;
            Pullup_Plate_Wheel = pullup_plate_wheel;
            Pullup_Propeller = pullup_propeller;
            Coupling_type = coupling_type;
        }
        /// <summary>
        /// Constructor de prueba
        /// 
        public BUType(string designacion, decimal i_val, int n1_val, int n2_val)
        {
            Designacion = designacion;
            i = i_val;
            n1 = n1_val;
            n2 = n2_val;
        }
        public List<BUType> GetPropulsores()
        {
            return butm.SelectAll();
        }
    }
}