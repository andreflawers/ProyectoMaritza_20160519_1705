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
    public class CDFeriados
    {
        public List<Feriados> listarFeriados()
        {

            String procedure = "sp_sel_feriados_listar";
            try
            {
                List<Feriados> oListFeriados = new List<Feriados>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Feriados oFeriados = new Feriados();
                    oFeriados.idFeriado = ((int)oSqlDataReader["idFeriado"]);
                    oFeriados.fechaFeriado = ((String)Convert.ToDateTime(oSqlDataReader["fechaFeriado"]).ToString().Substring(0,10));
                    oListFeriados.Add(oFeriados);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListFeriados;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public bool insertarFeriados(Feriados oFeriados)
        {
            String pro_inserta = "sp_ins_feriados_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@fechaFeriado", SqlDbType.Date).Value = oFeriados.fechaFeriado;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarFeriados(Feriados oFeriados)
        {
            String pro_actualiza = "sp_upd_feriados_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;

                SqlCommand.Parameters.Add("@idFeriado", SqlDbType.Int).Value = oFeriados.idFeriado;
                SqlCommand.Parameters.Add("@fechaFeriado", SqlDbType.Date).Value = oFeriados.fechaFeriado;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarFeriados(int idFeriado)
        {
            String pro_eliminar = "sp_del_feriados_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@idFeriado", SqlDbType.Int).Value = idFeriado;
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
