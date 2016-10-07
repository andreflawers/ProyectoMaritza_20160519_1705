using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using System.Data;
using System.Data.SqlClient;

namespace Modelo
{
    public class CDHorario
    {
        public List<Horario> listar_personal_dni() 
        {
            String consulta_listar = "sp_sel_empleados_seleccionar_dni";
            try
            {
                SqlConnection oSQLCxn = new SqlConnection();
                AdministracionConexion osqlConexion = new AdministracionConexion();
                oSQLCxn = osqlConexion.getConexion();
                //para ejecutar un prodimiento almacenado el primer parametro la consulta, el segundo la conexion
                SqlCommand oSqlcommand = new SqlCommand(consulta_listar, oSQLCxn);
                oSqlcommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlcommand.ExecuteReader();
                List<Horario> listarPersonal = new List<Horario>();
                while (oSqlDataReader.Read())
                {
                    Horario objHorario = new Horario();
                    objHorario.personal_id = ((int)oSqlDataReader["IdPersonal"]);
                    objHorario.personal_dni = ((String)oSqlDataReader["numeroDocumentoPersonal"]);
                    /*objHorario.sucursal_id = ((int)oSqlDataReader["IdSucursal"]);
                    objHorario.sucursal_nombre = ((String)oSqlDataReader["nombreSucursal"]);
                    objHorario.horario_id = ((int)oSqlDataReader["IdHorario"]);
                    objHorario.horario_inicial = ((String)oSqlDataReader["horaInicioHorario"].ToString());
                    objHorario.horario_final = ((String)oSqlDataReader["horaFinHorario"].ToString());
                    objHorario.descripcion_horario = ((String)oSqlDataReader["descripcionHorario"]);*/
                    listarPersonal.Add(objHorario);
                }
                oSqlDataReader.Close();
                oSQLCxn.Close();
                return listarPersonal;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Horario> busqueda_por_dni(int personal_dni) 
        {
            String consulta_listar = "sp_sel_persona_por_dni";
            try
            {
                SqlConnection oSQLCxn = new SqlConnection();
                AdministracionConexion osqlConexion = new AdministracionConexion();
                oSQLCxn = osqlConexion.getConexion();
                //para ejecutar un prodimiento almacenado el primer parametro la consulta, el segundo la conexion
                SqlCommand oSqlcommand = new SqlCommand(consulta_listar, oSQLCxn);
                oSqlcommand.CommandType = CommandType.StoredProcedure;
                oSqlcommand.Parameters.Add("@numeroDocumentoPersonal", SqlDbType.VarChar,15).Value = personal_dni;
                SqlDataReader oSqlDataReader = oSqlcommand.ExecuteReader();
                List<Horario> listarPersonal = new List<Horario>();
                while (oSqlDataReader.Read())
                {
                    Horario objHorario = new Horario();
                    objHorario.personal_id = ((int)oSqlDataReader["IdPersonal"]);
                    objHorario.personal_dni = ((String)oSqlDataReader["numeroDocumentoPersonal"]);
                    objHorario.sucursal_id = ((int)oSqlDataReader["IdSucursal"]);
                    objHorario.sucursal_nombre = ((String)oSqlDataReader["nombreSucursal"]);
                   // objHorario.a_paterno = ((String)oSqlDataReader["apellidoPaternoPersonal"]);
                   // objHorario.a_materno = ((String)oSqlDataReader["apellidoMaternoPersonal"]);
                   // objHorario.a_nombre = ((String)oSqlDataReader["nombrePersonal"]);

                    String a_paterno = (String)oSqlDataReader["apellidoPaternoPersonal"];
                    String a_materno = (String)oSqlDataReader["apellidoMaternoPersonal"];
                    String a_nombre = (String)oSqlDataReader["nombrePersonal"];
                    objHorario.personal_nombre = a_paterno + " " + a_materno + " " + a_nombre;
                    //objHorario.personal_nombre = objHorario.a_paterno + "" + objHorario.a_materno + "" + objHorario.a_nombre;
                    if (oSqlDataReader["horaInicioHorario"].ToString().Length==0)
                    {
                        objHorario.horario_id = 0;
                        objHorario.horario_inicial = "no existe";
                        objHorario.horario_final = "no existe";
                        objHorario.fecha_inicial = "no existe";
                        objHorario.fecha_final = "no existe";
                        objHorario.descripcion_horario = "no existe";
                    }
                    else
                    {
                        objHorario.horario_id = ((int)oSqlDataReader["IdHorario"]);
                        objHorario.horario_inicial = ((String)oSqlDataReader["horaInicioHorario"].ToString());
                        objHorario.horario_final = ((String)oSqlDataReader["horaFinHorario"].ToString());
                        objHorario.fecha_inicial = ((String)oSqlDataReader["fechaInicio"].ToString());
                        objHorario.fecha_final = ((String)oSqlDataReader["fechaFin"].ToString());
                        objHorario.descripcion_horario = ((String)oSqlDataReader["descripcionHorario"]);
                    }
                    listarPersonal.Add(objHorario);
                }

                oSqlDataReader.Close();
                oSQLCxn.Close();
                return listarPersonal;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable listar_horarios() 
        {
            String consulta_listar = "sp_sel_horario_seleccionar";
            try
            {
                //abrir conexion
                AdministracionConexion osqlConexion = new AdministracionConexion();
                //para ejecutar un prodimiento almacenado el primer parametro la consulta, el segundo la conexion
                SqlCommand oSqlcommand = new SqlCommand(consulta_listar, osqlConexion.getConexion());
                /*Este sirve para llenar datos en una tabla o cualquier conjunto*/
                SqlDataAdapter osqlAdapter = new SqlDataAdapter(oSqlcommand);
                DataTable oDatosEnTabla = new DataTable();
                /*Llenar la tabla*/
                osqlAdapter.Fill(oDatosEnTabla);
                return oDatosEnTabla;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool insertar_horario_personal(int personal_id, String horario_inicial,String horario_final, String fecha_inicial, String fecha_final, String  horario_descripcion) 
        {
            String pro_inserta = "sp_ins_Personal_Horario";
            try
            {
                SqlConnection oSQLCxn = new SqlConnection();
                AdministracionConexion oCDSQLCxn = new AdministracionConexion();

                oSQLCxn = oCDSQLCxn.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSQLCxn);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = personal_id;
                oSqlCommand.Parameters.Add("@horaInicioHorario", SqlDbType.Time, 7).Value = horario_inicial;
                oSqlCommand.Parameters.Add("@horaFinHorario", SqlDbType.Time, 7).Value = horario_final;
                oSqlCommand.Parameters.Add("@fechaInicio", SqlDbType.Date).Value = fecha_inicial;
                oSqlCommand.Parameters.Add("@fechaFin", SqlDbType.Date).Value = fecha_final;
                oSqlCommand.Parameters.Add("@descripcionHorario", SqlDbType.VarChar, 40).Value = horario_descripcion;
                oSqlCommand.ExecuteNonQuery();
                oSQLCxn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool modifica_horario_personal(int horario_id, String horario_inicial, String horario_final, String fecha_inicial, String fecha_final, String horario_descripcion) 
        {
            String pro_actualiza = "sp_upd_Horario_modificar";
            try
            {
                SqlConnection oSQLCxn = new SqlConnection();
                AdministracionConexion oCDSQLCxn = new AdministracionConexion();

                oSQLCxn = oCDSQLCxn.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(pro_actualiza, oSQLCxn);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@IdHorario", SqlDbType.Int).Value = horario_id;
                oSqlCommand.Parameters.Add("@horaInicioHorario", SqlDbType.Time, 7).Value = horario_inicial;
                oSqlCommand.Parameters.Add("@horaFinHorario", SqlDbType.Time, 7).Value = horario_final;
                oSqlCommand.Parameters.Add("@fechaInicio", SqlDbType.Date).Value = fecha_inicial.ToString();
                oSqlCommand.Parameters.Add("@fechaFin", SqlDbType.Date).Value = fecha_final.ToString();
                oSqlCommand.Parameters.Add("@descripcionHorario", SqlDbType.VarChar, 40).Value = horario_descripcion;
                oSqlCommand.ExecuteNonQuery();
                oSQLCxn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool eliminar_horario(int horario_id) 
        {
            String pro_eliminar = "sp_del_Horario_eliminar";
            try
            {
                SqlConnection oSQLCxn = new SqlConnection();
                AdministracionConexion oCDSQLCxn = new AdministracionConexion();

                oSQLCxn = oCDSQLCxn.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSQLCxn);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@horario_id", SqlDbType.Int).Value = horario_id;
                SqlCommand.ExecuteNonQuery();
                oSQLCxn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
