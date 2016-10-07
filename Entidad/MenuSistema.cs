using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class MenuSistema
    {
        public int NumMenu { get; set; }
        public int IdModulo { get; set; }
        public String nombreModulo { get; set; }
        public int IdInterfaz { get; set; }
        public String nombreInterfaz { get; set; }
        public String estadoInterfaz { get; set; }

        public MenuSistema()
        {
        }

        public MenuSistema(int me_num, int me_idModulo, string me_nombreModulo, int me_idInterfaz,
            string me_nombreInterfaz, string me_descripcion)
        {
            this.NumMenu = me_num;
            this.IdModulo = me_idModulo;
            this.nombreModulo = me_nombreModulo;
            this.IdInterfaz = me_idInterfaz;
            this.nombreInterfaz = me_nombreInterfaz;
            this.estadoInterfaz = estadoInterfaz;
        }
    }
}
