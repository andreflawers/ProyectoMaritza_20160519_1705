using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Modelo;

namespace Controladora
{
    public class CCEmpresa
    {
        public List<Empresa> listarEmpresa()
        {
            CDEmpresa oCDEmpresa = new CDEmpresa();
            return oCDEmpresa.listarEmpresa();
        }

        public Empresa buscarEmpresa(String rucEmpresa)
        {
            CDEmpresa oCDEmpresa = new CDEmpresa();
            return oCDEmpresa.buscarEmpresa(rucEmpresa);
        }

        public bool insertarEmpresa(Empresa oEmpresa)
        {
            CDEmpresa oCDEmpresa = new CDEmpresa();
            return oCDEmpresa.insertarEmpresa(oEmpresa);
        }

        public bool actualizarEmpresa(Empresa oEmpresa)
        {
            CDEmpresa oCDEmpresa = new CDEmpresa();
            return oCDEmpresa.actualizarEmpresa(oEmpresa);
        }

        public bool eliminarEmpresa(String rucEmpresa)
        {
            CDEmpresa oCDEmpresa = new CDEmpresa();
            return oCDEmpresa.eliminarArea(rucEmpresa);
        }
    }
}
