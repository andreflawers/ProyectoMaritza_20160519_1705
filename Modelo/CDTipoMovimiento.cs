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
    public class CDTipoMovimiento
    {
        public List<TipoMovimiento> listarTipoMovimiento()
        {

            String procedure = "sp_sel_tipoMovimiento_listar";
            try
            {
                List<TipoMovimiento> oListTipoMovimiento = new List<TipoMovimiento>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oCDConnection = new AdministracionConexion();
                oSqlConnection = oCDConnection.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    TipoMovimiento oTipoMovimiento = new TipoMovimiento();
                    oTipoMovimiento.IdTipoMovimiento = ((int)oSqlDataReader["IdTipoMovimiento"]);
                    oTipoMovimiento.nombreTipoMovimiento = ((String)oSqlDataReader["nombreTipoMovimiento"]);
                    oListTipoMovimiento.Add(oTipoMovimiento);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListTipoMovimiento;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public bool insertarTipoMovimiento(TipoMovimiento oTipoMovimiento)
        {
            String pro_inserta = "sp_ins_tipoMovimiento_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oCDConnection = new AdministracionConexion();
                oSqlConnection = oCDConnection.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreTipoMovimiento", SqlDbType.VarChar, 30).Value = oTipoMovimiento.nombreTipoMovimiento;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarTipoMovimiento(TipoMovimiento oTipoMovimiento)
        {
            String pro_actualiza = "sp_upd_tipoMovimiento_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oCDConnection = new AdministracionConexion();
                oSqlConnection = oCDConnection.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;

                SqlCommand.Parameters.Add("@IdTipoMovimiento", SqlDbType.Int).Value = oTipoMovimiento.IdTipoMovimiento;
                SqlCommand.Parameters.Add("@nombreTipoMovimiento", SqlDbType.VarChar, 30).Value = oTipoMovimiento.nombreTipoMovimiento;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarTipoMovimiento(int IdTipoMovimiento)
        {
            String pro_eliminar = "sp_del_tipoMovimiento_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oCDConnection = new AdministracionConexion();
                oSqlConnection = oCDConnection.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdTipoMovimiento", SqlDbType.Int).Value = IdTipoMovimiento;
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
