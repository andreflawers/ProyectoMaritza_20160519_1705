using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CCRemuneracion
    {
        public List<Remuneracion> listarRemuneracion()
        {
            CDRemuneracion oCDRemuneracion = new CDRemuneracion();
            return oCDRemuneracion.listarRemuneracion();
        }

        public bool insertarRemuneracion(Remuneracion oRemuneracion)
        {
            CDRemuneracion oCDRemuneracion = new CDRemuneracion();
            return oCDRemuneracion.insertarRemuneracion(oRemuneracion);
        }

        public bool actualizarRemuneracion(Remuneracion oRemuneracion)
        {
            CDRemuneracion oCDRemuneracion = new CDRemuneracion();
            return oCDRemuneracion.actualizarRemuneracion(oRemuneracion);
        }

        public bool eliminarRemuneracion(int IdRemuneracion)
        {
            CDRemuneracion oCDRemuneracion = new CDRemuneracion();
            return oCDRemuneracion.eliminarRemuneracion(IdRemuneracion);
        }
    }
}
