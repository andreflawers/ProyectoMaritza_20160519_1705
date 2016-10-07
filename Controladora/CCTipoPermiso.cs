using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Modelo;

namespace Controladora
{
   public class CCTipoPermiso
    {
        public List<TipoPermiso> listarTipoPermiso()
        {
            CDTipoPermiso oCDTipoPermiso = new CDTipoPermiso();
            return oCDTipoPermiso.listarTipoPermiso();
        }

        public bool insertarTipoPermiso(TipoPermiso oTipoPermiso)
        {
            CDTipoPermiso oCDTipoPermiso = new CDTipoPermiso();
            return oCDTipoPermiso.insertarTipoPermiso(oTipoPermiso);
        }

        public bool actualizarTipoPermiso(TipoPermiso oTipoPermiso)
        {
            CDTipoPermiso oCDTipoPermiso = new CDTipoPermiso();
            return oCDTipoPermiso.actualizarTipoPermiso(oTipoPermiso);
        }

        public bool eliminarTipoPermiso(int IdTipoPermiso)
        {
            CDTipoPermiso oCDTipoPermiso = new CDTipoPermiso();
            return oCDTipoPermiso.eliminarTipoPermiso(IdTipoPermiso);
        }
    }
}
