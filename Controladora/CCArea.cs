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
    public class CCArea
    {
        public List<Area> listarArea()
        {
            CDArea oCDArea = new CDArea();
            return oCDArea.listarArea();
        }

        public Area buscarArea(int IdArea)
        {
            CDArea oCDArea = new CDArea();
            return oCDArea.buscarArea(IdArea);
        }

        public bool insertarArea(Area oArea)
        {
            CDArea oCDArea = new CDArea();
            return oCDArea.insertarArea(oArea);
        }

        public bool actualizarArea(Area oArea)
        {
            CDArea oCDArea = new CDArea();
            return oCDArea.actualizarArea(oArea);
        }

        public bool eliminarArea(int IdArea)
        {
            CDArea oCDArea = new CDArea();
            return oCDArea.eliminarArea(IdArea);
        }
    }
}
