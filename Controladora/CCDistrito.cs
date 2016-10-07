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
    public class CCDistrito
    {
        public List<Distrito> listarDistrito()
        {
            CDDistrito oCDDistrito = new CDDistrito();
            return oCDDistrito.listarDistrito();
        }
        public List<Distrito> listar_distrito_por_provincia(int provincia_id) 
        {
            CDDistrito oCDDistrito = new CDDistrito();
            return oCDDistrito.listar_distrito_por_provincia(provincia_id);
        }

        public bool insertarDistrito(Distrito oDistrito)
        {
            CDDistrito oCDDistrito = new CDDistrito();
            return oCDDistrito.insertarDistrito(oDistrito);
        }

        public bool actualizarDistrito(Distrito oDistrito)
        {
            CDDistrito oCDDistrito = new CDDistrito();
            return oCDDistrito.actualizarDistrito(oDistrito);
        }

        public bool eliminarDistrito(int IdDistrito)
        {
            CDDistrito oCDDistrito = new CDDistrito();
            return oCDDistrito.eliminarDistrito(IdDistrito);
        }
    }
}
