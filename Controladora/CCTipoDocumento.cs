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
    public class CCTipoDocumento
    {
        public List<TipoDocumento> listarTipoDocumento()
        {
            CDTipoDocumento oCDTipoDocumento = new CDTipoDocumento();
            return oCDTipoDocumento.listarTipoDocumento();
        }

        public TipoDocumento buscarTipoDocumento(int IdTipoDocumento)
        {
            CDTipoDocumento oCDTipoDocumento = new CDTipoDocumento();
            return oCDTipoDocumento.buscarTipoDocumento(IdTipoDocumento);
        }

        public bool insertarTipoDocumento(TipoDocumento oCETipoDocumento)
        {
            CDTipoDocumento oCDTipoDocumento = new CDTipoDocumento();
            return oCDTipoDocumento.insertarTipoDocumento(oCETipoDocumento);
        }

        public bool actualizarTipoDocumento(TipoDocumento oCETipoDocumento)
        {
            CDTipoDocumento oCDTipoDocumento = new CDTipoDocumento();
            return oCDTipoDocumento.actualizarTipoDocumento(oCETipoDocumento);
        }

        public bool eliminarTipoDocumento(int IdTipoDocumento)
        {
            CDTipoDocumento oCDTipoDocumento = new CDTipoDocumento();
            return oCDTipoDocumento.eliminarTipoDocumento(IdTipoDocumento);
        }
    }
}
