using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Sucursal:Distrito
    {
        public int IdSucursal { get; set; }
        public String nombreSucursal { get; set; }
        public String direccionSucursal { get; set; }
        public String telefonoSucursal { get; set; }
        public String rucSucursal { get; set; }

        public Sucursal()
        {

        }
    }
}
