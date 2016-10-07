using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DescuentoPlanilla
    {
         public int IdDescuentoPlanilla { get; set; }
        public String nombreDescuentoPlanilla { get; set; }
        public Decimal montoDescuento { get; set; }

        public DescuentoPlanilla()
        {

        }
    }
}
