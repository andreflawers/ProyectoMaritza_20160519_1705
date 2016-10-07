using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Movimiento
    {
        public int IdMovimiento { get; set; }
        public int IdPersonal { get; set; }
        public string descripcionMovimiento { get; set; }
        public string fechaMovimiento { get; set; }
        public decimal montoMovimiento { get; set; }
        public TipoMovimiento tipoMovimiento { get; set; }

        public Movimiento()
        {
            tipoMovimiento = new TipoMovimiento();
        }
    }
}
