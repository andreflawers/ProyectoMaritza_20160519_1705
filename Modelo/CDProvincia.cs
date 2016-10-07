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
    public class CDProvincia
    {
        public List<Provincia> listarProvincia()
        {

            String procedure = "sp_sel_provincia_listar";
            try
            {
                List<Provincia> oListProvincia = new List<Provincia>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Provincia oProvincia = new Provincia();
                    oProvincia.IdProvincia = ((int)oSqlDataReader["IdProvincia"]);
                    oProvincia.nombreProvincia = ((String)oSqlDataReader["nombreProvincia"]);
                    oProvincia.IdDepartamento = ((int)oSqlDataReader["IdDepartamento"]);
                    oProvincia.nombreDepartamento = ((String)oSqlDataReader["nombreDepartamento"]);
                    oListProvincia.Add(oProvincia);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListProvincia;

            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public Provincia buscarProvincia(int IdProvincia)
        {
            String procedure = "sp_sel_provincia_buscar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand SqlCommand = new SqlCommand(procedure, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdProvincia", SqlDbType.Int).Value = IdProvincia;
                SqlDataReader oSqlDataReader = SqlCommand.ExecuteReader();
                Provincia oProvincia = new Provincia();
                oProvincia.nombreProvincia = "";
                while (oSqlDataReader.Read())
                {
                    oProvincia.IdProvincia = ((int)oSqlDataReader["IdProvincia"]);
                    oProvincia.nombreProvincia = ((String)oSqlDataReader["nombreProvincia"]);
                    oProvincia.nombreDepartamento = ((String)oSqlDataReader["nombreDepartamento"]);

                    oSqlDataReader.Close();
                    oSqlConnection.Close();
                    return oProvincia;
                }
                return oProvincia;
            }
            catch
            {
                return null;
            }
        }

        public bool insertarProvincia(Provincia oProvincia)
        {
            String pro_inserta = "sp_ins_provincia_registro";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@nombreProvincia", SqlDbType.VarChar, 50).Value = oProvincia.nombreProvincia;
                oSqlCommand.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = oProvincia.IdDepartamento;
                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizarProvincia(Provincia oProvincia)
        {
            String pro_actualiza = "sp_upd_provincia_modificar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;

                SqlCommand.Parameters.Add("@IdProvincia", SqlDbType.Int).Value = oProvincia.IdProvincia;
                SqlCommand.Parameters.Add("@nombreProvincia", SqlDbType.VarChar, 50).Value = oProvincia.nombreProvincia;
                SqlCommand.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = oProvincia.IdDepartamento;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarProvincia(int IdProvincia)
        {
            String pro_eliminar = "sp_del_provincia_eliminar";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdProvincia", SqlDbType.Int).Value = IdProvincia;
                SqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<Provincia> Listar_provincia_por_departamento(int departamento_id) 
        {
            String procedure = "sp_sel_provincia_by_id";
            try
            {
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();

                SqlCommand SqlCommand = new SqlCommand(procedure, oSqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@departamento_id", SqlDbType.Int).Value = departamento_id;
                SqlDataReader oSqlDataReader = SqlCommand.ExecuteReader();
                List<Provincia> oListProvincia = new List<Provincia>();
                while (oSqlDataReader.Read())
                {
                    Provincia oProvincia = new Provincia();
                    oProvincia.IdProvincia = ((int)oSqlDataReader["IdProvincia"]);
                    oProvincia.nombreProvincia = ((String)oSqlDataReader["nombreProvincia"]);
                    oListProvincia.Add(oProvincia);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListProvincia;
            }
            catch
            {
                return null;
            }
        }
    }
}
