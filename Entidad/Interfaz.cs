using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Interfaz
    {
        public int NumInterfaz { get; set; }
        public int IdInterfaz { get; set; }
        public String nombreInterfaz { get; set; }
        public String estadoInterfaz { get; set; }
        public int NumModulo { get; set; }
        public int IdModulo { get; set; }
        public String nombreModulo { get; set; }

        public Interfaz()
        {
        }

        public Interfaz(int i_num, int i_id, string i_nombre, string i_estado, int i_m_num, int i_m_id, String i_m_nombre)
        {
            this.NumInterfaz = i_num;
            this.IdInterfaz = i_id;
            this.nombreInterfaz = i_nombre;
            this.estadoInterfaz = i_estado;
            this.NumModulo = i_m_num;
            this.IdModulo = i_m_id;
            this.nombreModulo = i_m_nombre;
        }
    }
}
