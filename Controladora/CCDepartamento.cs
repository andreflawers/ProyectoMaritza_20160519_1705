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
    public class CCDepartamento
    {
        public List<Departamento> listarDepartamento()
        {
            CDDepartamento oCDDepartamento = new CDDepartamento();
            return oCDDepartamento.listarDepartamento();
        }

        public Departamento buscarDepartamento(int IdDepartamento)
        {
            CDDepartamento oCDDepartamento = new CDDepartamento();
            return oCDDepartamento.buscarDepartamento(IdDepartamento);
        }

        public bool insertarDepartamento(Departamento oDepartamento)
        {
            CDDepartamento oCDDepartamento = new CDDepartamento();
            return oCDDepartamento.insertarDepartamento(oDepartamento);
        }

        public bool actualizarDepartamento(Departamento oDepartamento)
        {
            CDDepartamento oCDDepartamento = new CDDepartamento();
            return oCDDepartamento.actualizarDepartamento(oDepartamento); ;
        }

        public bool eliminarDepartamento(int IdDepartamento)
        {
            CDDepartamento oCDDepartamento = new CDDepartamento();
            return oCDDepartamento.eliminarDepartamento(IdDepartamento);
        }
    }
}
