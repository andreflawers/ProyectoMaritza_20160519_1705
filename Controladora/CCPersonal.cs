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
    public class CCPersonal
    {
        public DataTable listar_personal() 
        {
            CDPersonal objCDPersonal = new CDPersonal();
            return objCDPersonal.listar_personal();
        }
        public int insertar_personal(Personal objPersonal) 
        {
            CDPersonal objCDPersonal = new CDPersonal();
            return objCDPersonal.insertar_personal(objPersonal);
        }
        public int actualizar_personal(Personal objPersonal) 
        {
            CDPersonal objCDPersonal = new CDPersonal();
            return objCDPersonal.actualizar_personal(objPersonal);
        }
        public bool eliminar_personal(int personal_id) 
        {
            CDPersonal objCDPersonal = new CDPersonal();
            return objCDPersonal.eliminar_personal(personal_id);
        }
        public Personal consultarPersonal(string num_doc)
        {
            CDPersonal oCDPersonal = new CDPersonal();
            return oCDPersonal.consultarPersonal(num_doc);
        }
    }
}
