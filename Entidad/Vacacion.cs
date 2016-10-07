using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Vacacion
    {
        public int IdVacacion { get; set; }
        public string fechaInicioVacacion { get; set; }
        public string fechaFinVacacion { get; set; }
        public string descripcionVacacion { get; set; }
        public int IdPersonal { get; set; }
        public Encargado encargado { get; set; }
        public Vacacion()
        {
            encargado = new Encargado();
        }
    }
}
