using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Descuento
    {
         public int IdDescuento { get; set; }
        public String nombreDescuento { get; set; }
        public Decimal montoDescuento { get; set; }

        public Descuento()
        {

        }
    }
}
