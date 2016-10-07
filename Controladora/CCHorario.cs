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
    public class CCHorario
    {
        public List<Horario> listar_personal_dni() 
        {
            CDHorario objCDPersonal = new CDHorario();
            return objCDPersonal.listar_personal_dni();
        }
        public List<Horario> busqueda_por_dni(int personal_dni) 
        {
            CDHorario objCDHorario = new CDHorario();
            return objCDHorario.busqueda_por_dni(personal_dni);
        }
        public DataTable listar_horario() 
        {
            CDHorario objCDHorario = new CDHorario();
            return objCDHorario.listar_horarios();
        }
        public bool insertar_horario_personal(int personal_id, String horario_inicial,String horario_final,String fecha_inicial,String fecha_final, String descripcion_horario) 
        {
            CDHorario objCDHroario = new CDHorario();
            return objCDHroario.insertar_horario_personal(personal_id, horario_inicial, horario_final, fecha_inicial, fecha_final, descripcion_horario);
        }
        public bool modificar_horario_personal(int horario_id, String horario_inicial, String horario_final,String fecha_inicial,String fecha_final, String descripcion_horario) 
        {
            CDHorario objCDHorario = new CDHorario();
            return objCDHorario.modifica_horario_personal(horario_id, horario_inicial, horario_final,fecha_inicial,fecha_final, descripcion_horario);
        }
        public bool eliminar_horario(int horario_id) 
        {
            CDHorario objCDHorario=new CDHorario();
            return objCDHorario.eliminar_horario(horario_id);
        }
    }
}
