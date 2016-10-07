using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CCDescuentoPlanilla
    {
        public List<DescuentoPlanilla> listarDescuentoPlanilla()
        {
            CDDescuentoPlanilla oCDDescuentoPlanilla = new CDDescuentoPlanilla();
            return oCDDescuentoPlanilla.listarDescuentoPlanilla();
        }

        public bool insertarDescuentoPlanilla(DescuentoPlanilla oDescuentoPlanilla)
        {
            CDDescuentoPlanilla oCDDescuentoPlanilla = new CDDescuentoPlanilla();
            return oCDDescuentoPlanilla.insertarDescuentoPlanilla(oDescuentoPlanilla);
        }

        public bool actualizarDescuentoPlanilla(DescuentoPlanilla oDescuentoPlanilla)
        {
            CDDescuentoPlanilla oCDDescuentoPlanilla = new CDDescuentoPlanilla();
            return oCDDescuentoPlanilla.actualizarDescuentoPlanilla(oDescuentoPlanilla);
        }

        public bool eliminarDescuentoPlanilla(int IdDescuentoPlanilla)
        {
            CDDescuentoPlanilla oCDDescuentoPlanilla = new CDDescuentoPlanilla();
            return oCDDescuentoPlanilla.eliminarDescuentoPlanilla(IdDescuentoPlanilla);
        }
    }
}
