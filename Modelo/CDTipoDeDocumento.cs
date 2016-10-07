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
    public class CDTipoDocumento
    {
        public List<TipoDocumento> listarTipoDocumento()
        {

            String procedure = "sp_sel_tipoDocumento_listar";
            try
            {
                List<TipoDocumento> oListCETipoDeDocumento = new List<TipoDocumento>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    TipoDocumento oCETipoDocumento = new TipoDocumento();
                    oCETipoDocumento.IdTipoDocumento = ((int)oSqlDataReader["IdTipoDocumento"]);
                    oCETipoDocumento.nombreTipoDocumento = ((String)oSqlDataReader["nombreTipoDocumento"]);
                    oCETipoDocumento.longitudTipoDocumento = ((int)oSqlDataReader["longitudTipoDocumento"]);
                    oListCETipoDeDocumento.Add(oCETipoDocumento);
                }
                oSqlDataReader.Close();
                oSqlDataReader.Close();
                return oListCETipoDeDocumento;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public TipoDocumento buscarTipoDocumento(int IdTipoDocumento)
        {
            String procedure = "sp_sel_tipoDocumento_buscar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand SqlCommand = new SqlCommand(procedure, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdTipoDocumento", SqlDbType.Int).Value = IdTipoDocumento;
                SqlDataReader oSqlDataReader = SqlCommand.ExecuteReader();
                TipoDocumento oCETipoDocumento = new TipoDocumento();
                oCETipoDocumento.nombreTipoDocumento = "";
                while (oSqlDataReader.Read())
                {
                    oCETipoDocumento.IdTipoDocumento = ((int)oSqlDataReader["IdTipoDocumento"]);
                    oCETipoDocumento.nombreTipoDocumento = ((String)oSqlDataReader["nombreTipoDocumento"]);

                    oSqlDataReader.Close();
                    oSqlConnection.Close();
                    return oCETipoDocumento;
                }
                return oCETipoDocumento;
            }
            catch
            {
                return null;
            }
        }

        public bool insertarTipoDocumento(TipoDocumento oCETipoDocumento)
        {
            String pro_inserta = "sp_ins_tipoDocumento_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreTipoDocumento", SqlDbType.VarChar, 40).Value = oCETipoDocumento.nombreTipoDocumento;
                oSqlCommand.Parameters.Add("@longitudTipoDocumento", SqlDbType.Int).Value = oCETipoDocumento.longitudTipoDocumento;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarTipoDocumento(TipoDocumento oCETipoDocumento)
        {
            String pro_actualiza = "sp_upd_tipoDocumento_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;

                SqlCommand.Parameters.Add("@IdTipoDocumento", SqlDbType.Int).Value = oCETipoDocumento.IdTipoDocumento;
                SqlCommand.Parameters.Add("@nombreTipoDocumento", SqlDbType.VarChar, 40).Value = oCETipoDocumento.nombreTipoDocumento;
                SqlCommand.Parameters.Add("@longitudTipoDocumento", SqlDbType.Int).Value = oCETipoDocumento.longitudTipoDocumento;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarTipoDocumento(int IdTipoDocumento)
        {
            String pro_eliminar = "sp_del_tipoDocumento_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdTipoDocumento", SqlDbType.Int).Value = IdTipoDocumento;
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
