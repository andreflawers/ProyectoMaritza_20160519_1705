using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Provincia : Departamento
    {
        public int IdProvincia { get; set; }
        public String nombreProvincia { get; set; }

        public Provincia()
        {

        }
    }
}
