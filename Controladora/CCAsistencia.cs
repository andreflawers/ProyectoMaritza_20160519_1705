using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Modelo;
using System.Data;
using System.Data.SqlClient;

namespace Controladora
{
    public class CCAsistencia
    {
        public static void Asistencia_Grabar(Asistencia obj_asistencia, Result_transaccion obj_transac)
        {
            SqlConnection conn = null;

            try
            {
                conn = new AdministracionConexion().getConexion();
                CDAsistencia.Asistencia_Ingresar(conn, obj_asistencia);
                obj_transac.resultado = 1;
                obj_transac.new_codigo = obj_asistencia.msg;
                obj_transac.msg_error = "";
            }
            catch (Exception ex)
            {
                obj_transac.resultado = 0;
                obj_transac.msg_error = ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }
        }
        public static string Asistencia_Consultar_datos(string dni, Result_transaccion obj_transac)
        {
            string apellido = "";
            SqlConnection conn = null;

            try
            {
                conn = new AdministracionConexion().getConexion();
                apellido = CDAsistencia.Asistencia_Consulta_Datos(conn, dni);

                obj_transac.resultado = 1;
                obj_transac.msg_error = "";
            }
            catch (Exception ex)
            {
                obj_transac.resultado = 0;
                obj_transac.msg_error = "Error!!! No se pudo consultar los datos" + ex.Message;
            }
            finally
            {
                AdministracionConexion.CloseConexion(conn);
            }

            return apellido;
        }
        public DataTable Asistencia_Listar(DateTime diaAsis)
        {
            CDAsistencia oCDAsistencia = new CDAsistencia();
            return oCDAsistencia.Asistencia_Listar_Diaria(diaAsis);
        }
        public DataTable Asistencia_Listar_PorFecha(string obj_desde, string obj_hasta)
        {
            CDAsistencia oCDAsistencia = new CDAsistencia();
            return oCDAsistencia.Asistencia_Listar_PorFecha(obj_desde, obj_hasta);
        }
        public DataTable Asistencia_Listar_PorHorarioySucursal(string obj_descripcion, string obj_nombre)
        {
            CDAsistencia oCDAsistencia = new CDAsistencia();
            return oCDAsistencia.Asistencia_Listar_PorHorarioySucursal(obj_descripcion, obj_nombre);
        }
        public DataTable Asistencia_Listar_PorPersonal(string obj_apellido, string obj_cargo)
        {
            CDAsistencia oCDAsistencia = new CDAsistencia();
            return oCDAsistencia.Asistencia_Listar_PorPersonal(obj_apellido, obj_cargo);
        }
        public DataTable Asistencia_Listar_PorFechayPersonal(string obj_desde, string obj_hasta, string obj_apellido, string obj_cargo)
        {
            CDAsistencia oCDAsistencia = new CDAsistencia();
            return oCDAsistencia.Asistencia_Listar_PorFechayPersonal(obj_desde, obj_hasta, obj_apellido, obj_cargo);
        }
        public DataTable Asistencia_Listar_PorFechayHorario(string obj_desde, string obj_hasta, string obj_descripcion, string obj_nombre)
        {
            CDAsistencia oCDAsistencia = new CDAsistencia();
            return oCDAsistencia.Asistencia_Listar_PorFechayHorario(obj_desde, obj_hasta, obj_descripcion, obj_nombre);
        }
        public DataTable Asistencia_Listar_PorHorarioyPersonal(string obj_descripcion, string obj_nombre, string obj_apellido, string obj_cargo)
        {
            CDAsistencia oCDAsistencia = new CDAsistencia();
            return oCDAsistencia.Asistencia_Listar_PorHorarioyPersonal(obj_descripcion, obj_nombre, obj_apellido, obj_cargo);
        }
        public DataTable Asistencia_Listar_PorFecha_HorarioyPersonal(string obj_desde, string obj_hasta, string obj_descripcion, string obj_nombre, string obj_apellido, string obj_cargo)
        {
            CDAsistencia oCDAsistencia = new CDAsistencia();
            return oCDAsistencia.Asistencia_Listar_PorFecha_HorarioyPersonal(obj_desde, obj_hasta, obj_descripcion, obj_nombre, obj_apellido, obj_cargo);
        }
    }
}
