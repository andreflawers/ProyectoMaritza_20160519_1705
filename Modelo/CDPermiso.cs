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
    public class CDPermiso
    {
        public List<Permiso> obtenerPermiso(int personal_id)
        {
            String procedure = "sp_lis_permiso";
            try
            {
                List<Permiso> oListPermiso = new List<Permiso>();
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
                    Permiso oPermiso = new Permiso();
                    oPermiso.IdPermiso = ((int)oSqlDataReader["IdPermiso"]);
                    oPermiso.fechaIniPermiso = Convert.ToDateTime(oSqlDataReader["fechaInicioPermiso"]).ToString("dd-MM-yyyy");
                    oPermiso.fechaFinPermiso = Convert.ToDateTime(oSqlDataReader["fechaFinPermiso"]).ToString("dd-MM-yyyy");
                    oPermiso.justPermiso = ((String)oSqlDataReader["justificacionPermiso"]);
                    oPermiso.IdPersonal = ((int)oSqlDataReader["IdPersonal"]);
                    oPermiso.tipoPermiso.IdTipoPermiso = ((int)oSqlDataReader["IdTipoPermiso"]);
                    oPermiso.tipoPermiso.nombreTipoPermiso = ((String)oSqlDataReader["nombreTipoPermiso"]);

                    oListPermiso.Add(oPermiso);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListPermiso;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        public bool eliminarPermiso(int IdPermiso)
        {
            String pro_eliminar = "sp_del_permiso";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@IdPermiso", SqlDbType.Int).Value = IdPermiso;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool actualizarPermiso(Permiso oPermiso)
        {
            String pro_actualiza = "sp_upd_permiso_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@IdPermiso", SqlDbType.Int).Value = oPermiso.IdPermiso;
                oSqlCommand.Parameters.Add("@fechaInicioPermiso", SqlDbType.DateTime).Value = Convert.ToDateTime(oPermiso.fechaIniPermiso);
                oSqlCommand.Parameters.Add("@fechaFinPermiso", SqlDbType.DateTime).Value = Convert.ToDateTime(oPermiso.fechaFinPermiso);
                oSqlCommand.Parameters.Add("@justificacionPermiso", SqlDbType.VarChar, 40).Value = oPermiso.justPermiso;
                oSqlCommand.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = oPermiso.IdPersonal;
                oSqlCommand.Parameters.Add("@idTipoPermiso", SqlDbType.Int).Value = oPermiso.tipoPermiso.IdTipoPermiso;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool insertarPermiso(Permiso oPermiso)
        {
            String pro_inserta = "sp_ins_permiso_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@fechaInicioPermiso", SqlDbType.DateTime).Value = Convert.ToDateTime(oPermiso.fechaIniPermiso);
                oSqlCommand.Parameters.Add("@fechaFinPermiso", SqlDbType.DateTime).Value = Convert.ToDateTime(oPermiso.fechaFinPermiso);
                oSqlCommand.Parameters.Add("@justificacionPermiso", SqlDbType.VarChar, 40).Value = oPermiso.justPermiso;
                oSqlCommand.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = oPermiso.IdPersonal;
                oSqlCommand.Parameters.Add("@idTipoPermiso", SqlDbType.Int).Value = oPermiso.tipoPermiso.IdTipoPermiso;
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
