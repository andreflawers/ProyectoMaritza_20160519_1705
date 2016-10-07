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
    public class CDDescuento
    {
        public List<Descuento> listarDescuento()
        {

            String procedure = "sp_sel_descuento_listar";
            try
            {
                List<Descuento> oListDescuento= new List<Descuento>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Descuento oDescuento = new Descuento();
                    oDescuento.IdDescuento = ((int)oSqlDataReader["IdDescuento"]);
                    oDescuento.nombreDescuento = ((String)oSqlDataReader["nombreDescuento"]);
                    oDescuento.montoDescuento = ((Decimal)(oSqlDataReader["montoDescuento"]));
                    oListDescuento.Add(oDescuento);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListDescuento;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public bool insertarDescuento(Descuento oDescuento)
        {
            String pro_inserta = "sp_ins_descuento_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreDescuento", SqlDbType.VarChar, 30).Value = oDescuento.nombreDescuento;
                oSqlCommand.Parameters.Add("@montoDescuento", SqlDbType.Decimal, 10).Value = oDescuento.montoDescuento;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarDescuento(Descuento oDescuento)
        {
            String pro_actualiza = "sp_upd_descuento_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;


                SqlCommand.Parameters.Add("@IdDescuento", SqlDbType.Int).Value = oDescuento.IdDescuento;
                SqlCommand.Parameters.Add("@nombreDescuento", SqlDbType.VarChar, 30).Value = oDescuento.nombreDescuento;
                SqlCommand.Parameters.Add("@montoDescuento", SqlDbType.Decimal, 10).Value = oDescuento.montoDescuento;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarDescuento(int IdDescuento)
        {
            String pro_eliminar = "sp_del_descuento_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdDescuento", SqlDbType.Int).Value = IdDescuento;
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
