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
    public class CDRemuneracion
    {
        public List<Remuneracion> listarRemuneracion()
        {

            String procedure = "sp_sel_remuneracion_listar";
            try
            {
                List<Remuneracion> oListRemuneracion = new List<Remuneracion>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Remuneracion oRemuneracion = new Remuneracion();
                    oRemuneracion.IdRemuneracion = ((int)oSqlDataReader["IdRemuneracion"]);
                    oRemuneracion.nombreRemuneracion = ((String)oSqlDataReader["nombreRemuneracion"]);
                    oRemuneracion.montoRemuneracion = ((Decimal)(oSqlDataReader["montoRemuneracion"]));
                    oListRemuneracion.Add(oRemuneracion);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListRemuneracion;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public bool insertarRemuneracion(Remuneracion oRemuneracion)
        {
            String pro_inserta = "sp_ins_remuneracion_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreRemuneracion", SqlDbType.VarChar, 30).Value = oRemuneracion.nombreRemuneracion;
                oSqlCommand.Parameters.Add("@montoRemuneracion", SqlDbType.Decimal, 10).Value = oRemuneracion.montoRemuneracion;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarRemuneracion(Remuneracion oRemuneracion)
        {
            String pro_actualiza = "sp_upd_remuneracion_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;

                SqlCommand.Parameters.Add("@IdRemuneracion", SqlDbType.Int).Value = oRemuneracion.IdRemuneracion;
                SqlCommand.Parameters.Add("@nombreRemuneracion", SqlDbType.VarChar, 30).Value = oRemuneracion.nombreRemuneracion;
                SqlCommand.Parameters.Add("@montoRemuneracion", SqlDbType.Decimal, 10).Value = oRemuneracion.montoRemuneracion;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarRemuneracion(int IdRemuneracion)
        {
            String pro_eliminar = "sp_del_remuneracion_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdRemuneracion", SqlDbType.Int).Value = IdRemuneracion;
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
