using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Boleta
    {
        public int IdBoleta { get; set; }
        public int tardanzas { get; set; }
        public int trabajados { get; set; }
        public int dominicales { get; set; }
        public string fechaEmision { get; set; }
        public decimal netoAcobrarBoleta { get; set; }
        public Remuneracion remuneracion { get; set; }
        public Personal personal { get; set; }
        public Cargo cargo { get; set; }

        public decimal montoBoletaPlanilla { get; set; }
        public Descuento descuento { get; set; }
        public DescuentoPlanilla descuentoPlanilla { get; set; }
        public int feriados { get; set; }

        public int dias_p { get; set; }
        public decimal monto_p { get; set; }
        public int dias_v { get; set; }
        public Boleta()
        {
            remuneracion = new Remuneracion();
            personal = new Personal();
            cargo = new Cargo();
            descuento = new Descuento();
            descuentoPlanilla = new DescuentoPlanilla();
        }
    }
}
