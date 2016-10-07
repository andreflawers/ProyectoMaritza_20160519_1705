using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Modelo;
using System.Data;

namespace Controladora
{
    public class CCSucursal
    {
        public List<Sucursal> listarSucursal()
        {
            CDSucursal oCDSucursal = new CDSucursal();
            return oCDSucursal.listarSucursal();
        }

        public bool insertarSucursal(Sucursal oSucursal)
        {
            CDSucursal oCDSucursal = new CDSucursal();
            return oCDSucursal.insertarSucursal(oSucursal);
        }

        public bool actualizarSucursal(Sucursal oSucursal)
        {
            CDSucursal oCDSucursal = new CDSucursal();
            return oCDSucursal.actualizarSucursal(oSucursal);
        }

        public bool eliminarSucursal(int IdSucursal)
        {
            CDSucursal oCDSucursal = new CDSucursal();
            return oCDSucursal.eliminarSucursal(IdSucursal);
        }
    }
}
