using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Permiso
    {
        public int IdPermiso { get; set; }
        public string justPermiso { get; set; }
        public string fechaIniPermiso { get; set; }
        public int IdPersonal { get; set; }
        public string fechaFinPermiso { get; set; }
        public TipoPermiso tipoPermiso { get; set; }

        public Permiso()
        {
            tipoPermiso = new TipoPermiso();
        }
    }
}
