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
    public class CCProvincia
    {
        public List<Provincia> listarProvincia()
        {
            CDProvincia oCDProvincia = new CDProvincia();
            return oCDProvincia.listarProvincia();
        }

        public Provincia buscarProvincia(int IdProvincia)
        {
            CDProvincia oCDProvincia = new CDProvincia();
            return oCDProvincia.buscarProvincia(IdProvincia);
        }
        public List<Provincia> listar_provincia_por_departamento(int departamento_id) 
        {
            CDProvincia oCDProvincia = new CDProvincia();
            return oCDProvincia.Listar_provincia_por_departamento(departamento_id);
        }
        public bool insertarProvincia(Provincia oProvincia)
        {
            CDProvincia oCDProvincia = new CDProvincia();
            return oCDProvincia.insertarProvincia(oProvincia);
        }

        public bool actualizarProvincia(Provincia oProvincia)
        {
            CDProvincia oCDProvincia = new CDProvincia();
            return oCDProvincia.actualizarProvincia(oProvincia);
        }

        public bool eliminarProvincia(int IdProvincia)
        {
            CDProvincia oCDProvincia = new CDProvincia();
            return oCDProvincia.eliminarProvincia(IdProvincia);
        }
    }
}
