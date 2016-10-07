using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Modulo
    {
        public int NumModulo { get; set; }
        public int IdModulo { get; set; }
        public String nombreModulo { get; set; }

        public Modulo()
        {
        }

        public Modulo(int m_num, int m_id, string m_descripcion)
        {
            this.NumModulo = m_num;
            this.IdModulo = m_id;
            this.nombreModulo = m_descripcion;
        }
    }
}
