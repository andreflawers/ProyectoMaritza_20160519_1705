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
    public class CCNivelInstruccion
    {
        public List<NivelInstruccion> listarNivelInstruccion()
        {
            CDNivelInstruccion oCDNivelInstruccion = new CDNivelInstruccion();
            return oCDNivelInstruccion.listarNivelInstruccion();
        }

        public NivelInstruccion buscarNivelInstruccion(int IdNivelInstruccion)
        {
            CDNivelInstruccion oCDNivelInstruccion = new CDNivelInstruccion();
            return oCDNivelInstruccion.buscarNivelInstruccion(IdNivelInstruccion);
        }

        public bool insertarNivelInstruccion(NivelInstruccion oNivelInstruccion)
        {
            CDNivelInstruccion oCDNivelInstruccion = new CDNivelInstruccion();
            return oCDNivelInstruccion.insertarNivelInstruccion(oNivelInstruccion);
        }

        public bool actualizarNivelInstruccion(NivelInstruccion oNivelInstruccion)
        {
            CDNivelInstruccion oCDNivelInstruccion = new CDNivelInstruccion();
            return oCDNivelInstruccion.actualizarNivelInstruccion(oNivelInstruccion);
        }

        public bool eliminarNivelInstruccion(int IdNivelInstruccion)
        {
            CDNivelInstruccion oCDNivelInstruccion = new CDNivelInstruccion();
            return oCDNivelInstruccion.eliminarNivelInstruccion(IdNivelInstruccion);
        }
    }
}
