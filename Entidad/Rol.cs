using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Rol
    {
        public int NumRol { get; set; }
        public int IdRol { get; set; }
        public String nombreRol { get; set; }
        public int cantidadInterfaces { get; set; }
        public int NumInterfaz { get; set; }
        public ArrayList IdInterfaz = new ArrayList();
        public ArrayList nombreInterfaz = new ArrayList();

        public Rol()
        {
        }

        public Rol(int r_num, int r_id, string r_nombre, int r_i_cantidad, int r_i_num, ArrayList r_i_id, ArrayList r_i_nombre)
        {
            this.NumRol = r_num;
            this.IdRol = r_id;
            this.nombreRol = r_nombre;
            this.cantidadInterfaces = r_i_cantidad;
            this.NumInterfaz = r_i_num;
            this.IdInterfaz = r_i_id;
            this.nombreInterfaz = r_i_nombre;
        }
    }
}
