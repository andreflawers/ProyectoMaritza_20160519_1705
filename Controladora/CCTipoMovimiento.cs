using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CCTipoMovimiento
    {
        public List<TipoMovimiento> listarTipoMovimiento()
        {
            CDTipoMovimiento oCDTipoMovimiento = new CDTipoMovimiento();
            return oCDTipoMovimiento.listarTipoMovimiento();
        }

        public bool insertarTipoMovimiento(TipoMovimiento oTipoMovimiento)
        {
            CDTipoMovimiento oCDTipoMovimiento = new CDTipoMovimiento();
            return oCDTipoMovimiento.insertarTipoMovimiento(oTipoMovimiento);
        }

        public bool actualizarTipoMovimiento(TipoMovimiento oTipoMovimiento)
        {
            CDTipoMovimiento oCDTipoMovimiento = new CDTipoMovimiento();
            return oCDTipoMovimiento.actualizarTipoMovimiento(oTipoMovimiento);
        }

        public bool eliminarTipoMovimiento(int IdTipoMovimiento)
        {
            CDTipoMovimiento oCDTipoMovimiento = new CDTipoMovimiento();
            return oCDTipoMovimiento.eliminarTipoMovimiento(IdTipoMovimiento);
        }
    }
}
