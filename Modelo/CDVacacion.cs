using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CDVacacion
    {
        public List<Vacacion> obtenerVacaciones(int personal_id)
        {
            String procedure = "sp_lis_vacaciones";
            try
            {
                List<Vacacion> oListVacaciones = new List<Vacacion>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@personal_id", SqlDbType.Int).Value = personal_id;
                oSqlCommand.Parameters.Add("@ano", SqlDbType.Int).Value = int.Parse(DateTime.Now.Year.ToString());
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Vacacion oVacaciones = new Vacacion();
                    oVacaciones.IdVacacion = ((int)oSqlDataReader["IdVacaciones"]);
                    oVacaciones.fechaInicioVacacion = Convert.ToDateTime(oSqlDataReader["fechaInicioVacaciones"]).ToString("dd-MM-yyyy");
                    oVacaciones.fechaFinVacacion = Convert.ToDateTime(oSqlDataReader["fechaFinVacaciones"]).ToString("dd-MM-yyyy");
                    oVacaciones.descripcionVacacion = ((String)oSqlDataReader["descripcionVacaciones"]);
                    oVacaciones.encargado.IdEncargado = ((int)oSqlDataReader["IdEncargado"]);
                    oVacaciones.encargado.personal_apellido_paterno = ((String)oSqlDataReader["apellidoPaternoPersonal"]); ;
                    oVacaciones.encargado.personal_apellido_materno = ((String)oSqlDataReader["apellidoMaternoPersonal"]); ;
                    oVacaciones.encargado.personal_nombre = ((String)oSqlDataReader["nombrePersonal"]);
                    oListVacaciones.Add(oVacaciones);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListVacaciones;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        public bool eliminarVacaciones(int IdVacaciones)
        {
            String pro_eliminar = "sp_del_vacaciones";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@IdVacaciones", SqlDbType.Int).Value = IdVacaciones;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool actualizarVacaciones(Vacacion oVacacion)
        {
            String pro_actualiza = "sp_upd_vacaciones_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@IdVacaciones", SqlDbType.Int).Value = oVacacion.IdVacacion;
                oSqlCommand.Parameters.Add("@fechaInicioVacaciones", SqlDbType.DateTime).Value = Convert.ToDateTime(oVacacion.fechaInicioVacacion);
                oSqlCommand.Parameters.Add("@fechaFinVacaciones", SqlDbType.DateTime).Value = Convert.ToDateTime(oVacacion.fechaFinVacacion);
                oSqlCommand.Parameters.Add("@descripcionVacaciones", SqlDbType.VarChar, 40).Value = oVacacion.descripcionVacacion;
                oSqlCommand.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = oVacacion.IdPersonal;
                oSqlCommand.Parameters.Add("@IdEncargado", SqlDbType.Int).Value = oVacacion.encargado.IdEncargado;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool insertarVacaciones(Vacacion oVacacion)
        {
            String pro_inserta = "sp_ins_vacaciones_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@fechaInicioVacaciones", SqlDbType.DateTime).Value = Convert.ToDateTime(oVacacion.fechaInicioVacacion);
                oSqlCommand.Parameters.Add("@fechaFinVacaciones", SqlDbType.DateTime).Value = Convert.ToDateTime(oVacacion.fechaFinVacacion);
                oSqlCommand.Parameters.Add("@descripcionVacaciones", SqlDbType.VarChar, 40).Value = oVacacion.descripcionVacacion;
                oSqlCommand.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = oVacacion.IdPersonal;
                oSqlCommand.Parameters.Add("@IdEncargado", SqlDbType.Int).Value = oVacacion.encargado.IdEncargado;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
