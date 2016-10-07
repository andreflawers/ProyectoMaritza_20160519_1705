using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CCDescuento
    {
        public List<Descuento> listarDescuento()
        {
            CDDescuento oCDDescuento = new CDDescuento();
            return oCDDescuento.listarDescuento();
        }

        public bool insertarDescuento(Descuento oDescuento)
        {
            CDDescuento oCDDesuento = new CDDescuento();
            return oCDDesuento.insertarDescuento(oDescuento);
        }

        public bool actualizarDescuento(Descuento oDescuento)
        {
            CDDescuento oCDDescuento = new CDDescuento();
            return oCDDescuento.actualizarDescuento(oDescuento);
        }

        public bool eliminarDescuento(int IdDescuento)
        {
            CDDescuento oCDDescuento = new CDDescuento();
            return oCDDescuento.eliminarDescuento(IdDescuento);
        }
    }
}
