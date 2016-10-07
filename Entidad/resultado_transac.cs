using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class resultado_transac
    {
        public String msg_error { get; set; }
        public string new_codigo { get; set; }

        public List<String> oList { get; set; }

        public int nro { get; set; }

        public resultado_transac()
        {
        }
    }
}
