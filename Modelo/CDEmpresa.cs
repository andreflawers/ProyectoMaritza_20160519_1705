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
    public class CDEmpresa
    {
        public List<Empresa> listarEmpresa()
        {

            String procedure = "sp_sel_empresa_listar";
            try
            {
                List<Empresa> oListEmpresa = new List<Empresa>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Empresa oEmpresa = new Empresa();
                    oEmpresa.rucEmpresa = ((String)oSqlDataReader["rucEmpresa"]);
                    oEmpresa.nombreEmpresa = ((String)oSqlDataReader["nombreEmpresa"]);
                    oEmpresa.direccionEmpresa = ((String)oSqlDataReader["direccionEmpresa"]);
                    oEmpresa.descripcionEmpresa = ((String)oSqlDataReader["descripcionEmpresa"]);
                    oListEmpresa.Add(oEmpresa);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListEmpresa;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public Empresa buscarEmpresa(String rucEmpresa)
        {
            String procedure = "sp_sel_empresa_buscar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand SqlCommand = new SqlCommand(procedure, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@rucEmpresa", SqlDbType.Char, 11).Value = rucEmpresa;
                SqlDataReader oSqlDataReader = SqlCommand.ExecuteReader();
                Empresa oEmpresa = new Empresa();
                oEmpresa.descripcionEmpresa = "";
                while (oSqlDataReader.Read())
                {
                    oEmpresa.rucEmpresa = ((String)oSqlDataReader["rucEmpresa"]);
                    oEmpresa.nombreEmpresa = ((String)oSqlDataReader["nombreEmpresa"]);
                    oEmpresa.direccionEmpresa = ((String)oSqlDataReader["direccionEmpresa"]);
                    oEmpresa.descripcionEmpresa = ((String)oSqlDataReader["descripcionEmpresa"]);

                    oSqlDataReader.Close();
                    oSqlConnection.Close();
                    return oEmpresa;
                }
                return oEmpresa;
            }
            catch
            {
                return null;
            }
        }

        public bool insertarEmpresa(Empresa oEmpresa)
        {
            String pro_inserta = "sp_ins_empresa_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@rucEmpresa", SqlDbType.Char, 11).Value = oEmpresa.rucEmpresa;
                oSqlCommand.Parameters.Add("@nombreEmpresa", SqlDbType.VarChar, 40).Value = oEmpresa.nombreEmpresa;
                oSqlCommand.Parameters.Add("@direccionEmpresa", SqlDbType.VarChar, 40).Value = oEmpresa.direccionEmpresa;
                oSqlCommand.Parameters.Add("@descripcionEmpresa", SqlDbType.VarChar, 40).Value = oEmpresa.descripcionEmpresa;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarEmpresa(Empresa oEmpresa)
        {
            String pro_actualiza = "sp_upd_empresa_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@rucEmpresa", SqlDbType.Char, 11).Value = oEmpresa.rucEmpresa;
                SqlCommand.Parameters.Add("@nombreEmpresa", SqlDbType.VarChar, 40).Value = oEmpresa.nombreEmpresa;
                SqlCommand.Parameters.Add("@direccionEmpresa", SqlDbType.VarChar, 40).Value = oEmpresa.direccionEmpresa;
                SqlCommand.Parameters.Add("@descripcionEmpresa", SqlDbType.VarChar, 40).Value = oEmpresa.descripcionEmpresa;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarArea(String rucEmpresa)
        {
            String pro_eliminar = "sp_del_empresa_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@rucEmpresa", SqlDbType.Char, 11).Value = rucEmpresa;
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
