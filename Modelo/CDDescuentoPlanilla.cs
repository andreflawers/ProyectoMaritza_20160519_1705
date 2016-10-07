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
    public class CDDescuentoPlanilla
    {
        public List<DescuentoPlanilla> listarDescuentoPlanilla()
        {

            String procedure = "sp_sel_descuentoPlanilla_listar";
            try
            {
                List<DescuentoPlanilla> oListDescuentoPlanilla = new List<DescuentoPlanilla>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    DescuentoPlanilla oDescuentoPlanilla = new DescuentoPlanilla();
                    oDescuentoPlanilla.IdDescuentoPlanilla = ((int)oSqlDataReader["IdDescuentoPlanilla"]);
                    oDescuentoPlanilla.nombreDescuentoPlanilla = ((String)oSqlDataReader["nombreDescuentoPlanilla"]);
                    oDescuentoPlanilla.montoDescuento = ((Decimal)(oSqlDataReader["montoDescuento"]));
                    oListDescuentoPlanilla.Add(oDescuentoPlanilla);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListDescuentoPlanilla;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public bool insertarDescuentoPlanilla(DescuentoPlanilla oDescuentoPlanilla)
        {
            String pro_inserta = "sp_ins_descuentoPlanilla_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreDescuentoPlanilla", SqlDbType.VarChar, 30).Value = oDescuentoPlanilla.nombreDescuentoPlanilla;
                oSqlCommand.Parameters.Add("@montoDescuento", SqlDbType.Decimal, 10).Value = oDescuentoPlanilla.montoDescuento;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarDescuentoPlanilla(DescuentoPlanilla oDescuentoPlanilla)
        {
            String pro_actualiza = "sp_upd_descuentoPlanilla_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;


                SqlCommand.Parameters.Add("@IdDescuentoPlanilla", SqlDbType.Int).Value = oDescuentoPlanilla.IdDescuentoPlanilla;
                SqlCommand.Parameters.Add("@nombreDescuentoPlanilla", SqlDbType.VarChar, 30).Value =oDescuentoPlanilla.nombreDescuentoPlanilla;
                SqlCommand.Parameters.Add("@montoDescuento", SqlDbType.Decimal, 10).Value = oDescuentoPlanilla.montoDescuento;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarDescuentoPlanilla(int IdDescuentoPlanilla)
        {
            String pro_eliminar = "sp_del_descuentoPlanilla_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdDescuentoPlanilla", SqlDbType.Int).Value = IdDescuentoPlanilla;
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
