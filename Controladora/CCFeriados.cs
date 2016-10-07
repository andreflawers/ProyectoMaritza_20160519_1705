using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CCFeriados
    {
        public List<Feriados> listarFeriados()
        {
            CDFeriados oCDFeriados = new CDFeriados();
            return oCDFeriados.listarFeriados();
        }

        public bool insertarFeriados(Feriados oFeriados)
        {
            CDFeriados oCDFeriados = new CDFeriados();
            return oCDFeriados.insertarFeriados(oFeriados);
        }

        public bool actualizarFeriados(Feriados oFeriados)
        {
            CDFeriados oCDFeriados = new CDFeriados();
            return oCDFeriados.actualizarFeriados(oFeriados);
        }

        public bool eliminarFeriados(int idFeriado)
        {
            CDFeriados oCDFeriados = new CDFeriados();
            return oCDFeriados.eliminarFeriados(idFeriado);
        }
    }
}
