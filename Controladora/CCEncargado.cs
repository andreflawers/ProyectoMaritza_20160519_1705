using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CCEncargado
    {
        public List<Encargado> listarEncargado()
        {
            CDEncargado oCDEncargado = new CDEncargado();
            return oCDEncargado.listarEncarga();
        }
    }
}
