using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CCMovimiento
    {
        public List<Movimiento> obtenerMovimiento(int personal_id)
        {
            CDMovimiento oCDMovimientos = new CDMovimiento();
            return oCDMovimientos.obtenerMovimiento(personal_id);
        }
        public bool eliminarMovimiento(int IdMovimiento)
        {
            CDMovimiento oCDMovimientos = new CDMovimiento();
            return oCDMovimientos.eliminarMovimiento(IdMovimiento);
        }
        public bool actualizarMovimiento(Movimiento oMovimientos)
        {
            CDMovimiento oCDMovimientos = new CDMovimiento();
            return oCDMovimientos.actualizarMovimiento(oMovimientos);
        }
        public bool insertarMovimiento(Movimiento oMovimientos)
        {
            CDMovimiento oCDMovimientos = new CDMovimiento();
            return oCDMovimientos.insertarMovimiento(oMovimientos);
        }
    }
}
