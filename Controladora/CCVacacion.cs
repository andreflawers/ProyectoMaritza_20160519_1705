using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CCVacacion
    {
        public List<Vacacion> obtenerVacaciones(int personal_id)
        {
            CDVacacion oCDVacaciones = new CDVacacion();
            return oCDVacaciones.obtenerVacaciones(personal_id);
        }
        public bool eliminarVacaciones(int IdVacaciones)
        {
            CDVacacion oCDVacaciones = new CDVacacion();
            return oCDVacaciones.eliminarVacaciones(IdVacaciones);
        }
        public bool actualizarVacaciones(Vacacion oVacaciones)
        {
            CDVacacion oCDVacaciones = new CDVacacion();
            return oCDVacaciones.actualizarVacaciones(oVacaciones);
        }
        public bool insertarVacaciones(Vacacion oVacaciones)
        {
            CDVacacion oCDVacaciones = new CDVacacion();
            return oCDVacaciones.insertarVacaciones(oVacaciones);
        }
    }
}
