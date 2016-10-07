using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Horario
    {
        public int personal_id { get; set; }
        public String personal_dni { get; set; }
        public String personal_nombre { get; set; }
        public int horario_id { get; set; }
        public String descripcion_horario { get; set; }
        public String horario_inicial { get; set; }
        public String horario_final { get; set; }
        public String fecha_inicial { get; set; }
        public String fecha_final { get; set; }
        public int sucursal_id { get; set; }
        public String sucursal_nombre { get; set; }
    }
}
