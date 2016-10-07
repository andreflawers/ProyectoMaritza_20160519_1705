using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CCPermiso
    {
        public List<Permiso> obtenerPermisos(int personal_id)
        {
            CDPermiso oCDPermiso = new CDPermiso();
            return oCDPermiso.obtenerPermiso(personal_id);
        }
        public bool eliminarPermiso(int IdPermiso)
        {
            CDPermiso oCDPermiso = new CDPermiso();
            return oCDPermiso.eliminarPermiso(IdPermiso);
        }
        public bool actualizarPermiso(Permiso oPermiso)
        {
            CDPermiso oCDPermiso = new CDPermiso();
            return oCDPermiso.actualizarPermiso(oPermiso);
        }
        public bool insertarPermiso(Permiso oPermiso)
        {
            CDPermiso oCDPermiso = new CDPermiso();
            return oCDPermiso.insertarPermiso(oPermiso);
        }
        public List<TipoPermiso> listarTipoPermiso()
        {
            CDTipoPermiso oCDTipoPermiso = new CDTipoPermiso();
            return oCDTipoPermiso.listarTipoPermiso();
        }
    }
}
