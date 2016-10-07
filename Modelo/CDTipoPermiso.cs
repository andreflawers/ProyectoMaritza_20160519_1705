using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using System.Data.SqlClient;
using System.Data;

namespace Modelo
{
    public class CDTipoPermiso
    {
        public List<TipoPermiso> listarTipoPermiso()
        {

            String procedure = "sp_sel_tipoPermiso_listar";
            try
            {
                List<TipoPermiso> oListTipoPermiso = new List<TipoPermiso>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    TipoPermiso oTipoPermiso = new TipoPermiso();
                    oTipoPermiso.IdTipoPermiso = ((int)oSqlDataReader["IdTipoPermiso"]);
                    oTipoPermiso.nombreTipoPermiso = ((String)oSqlDataReader["nombreTipoPermiso"]);
                    oTipoPermiso.remuneracionTipoPermiso = ((Decimal)(oSqlDataReader["remuneracionTipoPermiso"]));
                    oListTipoPermiso.Add(oTipoPermiso);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListTipoPermiso;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public bool insertarTipoPermiso(TipoPermiso oTipoPermiso)
        {
            String pro_inserta = "sp_ins_tipoPermiso_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreTipoPermiso", SqlDbType.VarChar, 50).Value = oTipoPermiso.nombreTipoPermiso;
                oSqlCommand.Parameters.Add("@remuneracionTipoPermiso", SqlDbType.Decimal, 10).Value = oTipoPermiso.remuneracionTipoPermiso;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarTipoPermiso(TipoPermiso oTipoPermiso)
        {
            String pro_actualiza = "sp_upd_tipoPermiso_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;

                SqlCommand.Parameters.Add("@IdTipoPermiso", SqlDbType.Int).Value = oTipoPermiso.IdTipoPermiso;
                SqlCommand.Parameters.Add("@nombreTipoPermiso", SqlDbType.VarChar, 50).Value = oTipoPermiso.nombreTipoPermiso;
                SqlCommand.Parameters.Add("@remuneracionTipoPermiso", SqlDbType.Decimal, 10).Value = oTipoPermiso.remuneracionTipoPermiso;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarTipoPermiso(int IdTipoPermiso)
        {
            String pro_eliminar = "sp_del_tipoPermiso_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdTipoPermiso", SqlDbType.Int).Value = IdTipoPermiso;
                SqlCommand.ExecuteNonQuery();
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
