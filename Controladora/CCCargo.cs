using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CCCargo
    {
        public List<Cargo> listarCargo()
        {
            CDCargo oCDCargo = new CDCargo();
            return oCDCargo.listarCargo();
        }

        public bool insertarCargo(Cargo oCargo)
        {
            CDCargo oCDCargo = new CDCargo();
            return oCDCargo.insertarCargo(oCargo);
        }

        public bool actualizarCargo(Cargo oCargo)
        {
            CDCargo oCDCargo = new CDCargo();
            return oCDCargo.actualizarCargo(oCargo);
        }

        public bool eliminarCargo(int IdCargo)
        {
            CDCargo oCDCargo = new CDCargo();
            return oCDCargo.eliminarCargo(IdCargo);
        }
    }
}
