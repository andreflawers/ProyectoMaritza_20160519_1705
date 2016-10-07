using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CCBoleta
    {
        public Boleta consultarBoleta(string num_doc, int mes, int ano)
        {
            CDBoleta oCDBoleta = new CDBoleta();
            return oCDBoleta.consultarBoleta(num_doc, mes, ano);
        }
        public List<Remuneracion> consultarBoletaRemuneracion(string num_doc)
        {
            CDBoleta oCDBoleta = new CDBoleta();
            return oCDBoleta.consultarBoletaRemuneracion(num_doc);
        }

        public string consultarNumBoleta(string num_bol)
        {
            CDBoleta oCDBoleta = new CDBoleta();
            return oCDBoleta.consultar_NumBoleta(num_bol);
        }

        public List<DescuentoPlanilla> consultarDescuentoPlanilla(string parametro)
        {
            CDBoleta oCDBoleta = new CDBoleta();
            return oCDBoleta.consultarDescuentoPlanilla(parametro);
        }


        public void insertar_boleta(int id_personal, DateTime fecha_emision_boleta, decimal neto_a_cobrar, string id_boleta, int id_descuento_planilla, decimal monto_boleta_planilla, int mes, int ano)
        {
            CDBoleta oCDBoleta = new CDBoleta();
            oCDBoleta.insertar_boleta(id_boleta, fecha_emision_boleta, neto_a_cobrar, id_personal);
            oCDBoleta.insertar_boleta_detalle(id_personal, id_boleta, mes, ano);
            if (monto_boleta_planilla > 0)
            {
                oCDBoleta.insertar_boleta_planilla(id_descuento_planilla, id_boleta, monto_boleta_planilla);
            }
        }



        public List<Movimiento> consultarMovimientos(string num_doc, int mes, int ano)
        {
            CDBoleta oCDBoleta = new CDBoleta();
            return oCDBoleta.consultarMovimientos(num_doc, mes, ano);
        }
        public List<Descuento> consultarDescuentos(string num_doc)
        {
            CDBoleta oCDBoleta = new CDBoleta();
            return oCDBoleta.consultarDescuentos(num_doc);
        }
    }
}
