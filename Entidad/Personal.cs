using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Personal
    {
        public int personal_id { get; set; }
        public String personal_apellido_paterno { get; set; }
        public String personal_apellido_materno { get; set; }
        public String personal_nombre { get; set; }
        public String personal_planilla { get; set; }
        public int sucursal_id { get; set; }
        public String sucursal_nombre { get; set; }
        public int  area_id { get; set; }
        public int tipo_documento_id { get; set; }
        public String numero_documento { get; set; }
        public String personal_direccion { get; set; }
        public String  personal_telefono{get;set;}
        public String personal_fecha_nacimiento { get; set; }
        public int nivel_escolaridad_id { get; set; }
        public String persona_fecha_de_ingreso { get; set; }
        public int salario_id { get; set; }
        public double salario_monto { get; set; }
        public double salario_remuneraciones { get; set; }
        public int personal_distrito_id { get; set; }
        public String personal_distrito_nombre { get; set; }
        public String personal_estado_civil { get; set; }
        public String personal_sexo { get; set; }
        public int cargo_id { get; set; }

    }
}
