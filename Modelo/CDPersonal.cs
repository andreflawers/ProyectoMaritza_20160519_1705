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
    public class CDPersonal
    {
        public DataTable listar_personal()
        {
            String consulta_listar = "sp_listar_personal";
            try
            {
                //abrir conexion
                AdministracionConexion osqlConexion = new AdministracionConexion();
                //para ejecutar un prodimiento almacenado el primer parametro la consulta, el segundo la conexion
                SqlCommand oSqlcommand = new SqlCommand(consulta_listar, osqlConexion.getConexion());
                /*Este sirve para llenar datos en una tabla o cualquier conjunto*/
                SqlDataAdapter osqlAdapter = new SqlDataAdapter(oSqlcommand);
                DataTable oDatosEnTabla = new DataTable();
                /*Llenar la tabla*/
                osqlAdapter.Fill(oDatosEnTabla);
                return oDatosEnTabla;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int insertar_personal(Personal objPersonal) 
        {
            String pro_inserta = "ins_personal_registrar";
            try
            {
                SqlConnection oSQLCxn = new SqlConnection();
                AdministracionConexion oCDSQLCxn = new AdministracionConexion();

                oSQLCxn = oCDSQLCxn.getConexion();

                SqlCommand oSqlCommand = new SqlCommand(pro_inserta, oSQLCxn);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                //oSqlCommand.Parameters.Add("@color_id", SqlDbType.Int).Value = oColor.color_id;
                oSqlCommand.Parameters.Add("@devolvera", SqlDbType.Int).Direction = ParameterDirection.Output;
                oSqlCommand.Parameters.Add("@apellidoPaternoPersonal", SqlDbType.VarChar, 50).Value = objPersonal.personal_apellido_paterno;
                oSqlCommand.Parameters.Add("@apellidoMaternoPersonal", SqlDbType.VarChar, 50).Value = objPersonal.personal_apellido_materno;
                oSqlCommand.Parameters.Add("@nombrePersonal", SqlDbType.VarChar, 50).Value = objPersonal.personal_nombre;
                oSqlCommand.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = objPersonal.sucursal_id;
                oSqlCommand.Parameters.Add("@IdArea", SqlDbType.Int).Value = objPersonal.area_id;
                oSqlCommand.Parameters.Add("@IdTipoDocumento", SqlDbType.Int).Value = objPersonal.tipo_documento_id;
                oSqlCommand.Parameters.Add("@numeroDocumentoPersonal", SqlDbType.VarChar, 15).Value = objPersonal.numero_documento;
                oSqlCommand.Parameters.Add("@direccionPersonal", SqlDbType.VarChar, 50).Value = objPersonal.personal_direccion;
                oSqlCommand.Parameters.Add("@telefonoPersonal", SqlDbType.Char, 10).Value = objPersonal.personal_telefono;
                oSqlCommand.Parameters.Add("@fechaNacimientoPersonal", SqlDbType.Date).Value = objPersonal.personal_fecha_nacimiento;
                oSqlCommand.Parameters.Add("@IdNivelInstruccion", SqlDbType.Int).Value = objPersonal.nivel_escolaridad_id;
                oSqlCommand.Parameters.Add("@fechaIngresoPersonal", SqlDbType.Date).Value = objPersonal.persona_fecha_de_ingreso;
                oSqlCommand.Parameters.Add("@IdCargo", SqlDbType.Int).Value = objPersonal.cargo_id;
                oSqlCommand.Parameters.Add("@IdDistrito", SqlDbType.Int).Value = objPersonal.personal_distrito_id;
                oSqlCommand.Parameters.Add("@estadoCivilPersonal", SqlDbType.VarChar, 15).Value = objPersonal.personal_estado_civil;
                oSqlCommand.Parameters.Add("@sexoPersonal", SqlDbType.Char,1).Value = objPersonal.personal_sexo;
                oSqlCommand.Parameters.Add("@planillaPersonal", SqlDbType.Char, 1).Value = objPersonal.personal_planilla[0]; 
                oSqlCommand.ExecuteNonQuery();
                int devuelve=int.Parse(oSqlCommand.Parameters["@devolvera"].Value.ToString());
              
                oSQLCxn.Close();
                return devuelve;
            }
            catch
            {
                return -1;
            }
        }
        public int actualizar_personal(Personal objPersonal) 
        {
            String pro_actualiza = "sp_upd_personal_actualizar";
            try
            {
                SqlConnection oSQLCxn = new SqlConnection();
                AdministracionConexion oCDSQLCxn = new AdministracionConexion();

                oSQLCxn = oCDSQLCxn.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_actualiza, oSQLCxn);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@devolvera", SqlDbType.Int).Direction = ParameterDirection.Output;
                SqlCommand.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = objPersonal.personal_id;
                SqlCommand.Parameters.Add("@apellidoPaternoPersonal", SqlDbType.VarChar, 50).Value = objPersonal.personal_apellido_paterno;
                SqlCommand.Parameters.Add("@apellidoMaternoPersonal", SqlDbType.VarChar, 50).Value = objPersonal.personal_apellido_materno;
                SqlCommand.Parameters.Add("@nombrePersonal", SqlDbType.VarChar, 50).Value = objPersonal.personal_nombre;
                SqlCommand.Parameters.Add("@IdSucursal", SqlDbType.Int).Value = objPersonal.sucursal_id;
                SqlCommand.Parameters.Add("@IdArea", SqlDbType.Int).Value = objPersonal.area_id;
                SqlCommand.Parameters.Add("@IdTipoDocumento", SqlDbType.Int).Value = objPersonal.tipo_documento_id;
                SqlCommand.Parameters.Add("@numeroDocumentoPersonal", SqlDbType.VarChar, 15).Value = objPersonal.numero_documento;
                SqlCommand.Parameters.Add("@direccionPersonal", SqlDbType.VarChar, 50).Value = objPersonal.personal_direccion;
                SqlCommand.Parameters.Add("@telefonoPersonal", SqlDbType.Char, 10).Value = objPersonal.personal_telefono;
                SqlCommand.Parameters.Add("@fechaNacimientoPersonal", SqlDbType.Date).Value = objPersonal.personal_fecha_nacimiento;
                SqlCommand.Parameters.Add("@IdNivelInstruccion", SqlDbType.Int).Value = objPersonal.nivel_escolaridad_id;
                SqlCommand.Parameters.Add("@fechaIngresoPersonal", SqlDbType.Date).Value = objPersonal.persona_fecha_de_ingreso;
                SqlCommand.Parameters.Add("@IdCargo", SqlDbType.Int).Value = objPersonal.cargo_id;
                SqlCommand.Parameters.Add("@IdDistrito", SqlDbType.Int).Value = objPersonal.personal_distrito_id;
                SqlCommand.Parameters.Add("@estadoCivilPersonal", SqlDbType.VarChar, 15).Value = objPersonal.personal_estado_civil;
                SqlCommand.Parameters.Add("@sexoPersonal", SqlDbType.Char,1).Value = objPersonal.personal_sexo;
                SqlCommand.Parameters.Add("@planillaPersonal", SqlDbType.Char, 1).Value = objPersonal.personal_planilla[0];
                SqlCommand.ExecuteNonQuery();
                int devuelve = int.Parse(SqlCommand.Parameters["@devolvera"].Value.ToString());
                oSQLCxn.Close();
                return devuelve;
            }
            catch
            {
                return -1;
            }
        }
        public bool eliminar_personal(int personal_id) 
        {
            String pro_eliminar = "sp_del_personal_eliminar";
            try
            {
                SqlConnection oSQLCxn = new SqlConnection();
                AdministracionConexion oCDSQLCxn = new AdministracionConexion();

                oSQLCxn = oCDSQLCxn.getConexion();
                SqlCommand SqlCommand = new SqlCommand(pro_eliminar, oSQLCxn);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Add("@IdPersonal", SqlDbType.Int).Value = personal_id;
                SqlCommand.ExecuteNonQuery();
                oSQLCxn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Personal consultarPersonal(string num_doc)
        {
            try
            {
                Personal oPersonal = new Personal();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand cmd = new SqlCommand("sp_bus_personal_numero_documento_personal", oSqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@num_doc", SqlDbType.VarChar, 15).Value = num_doc;
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(cmd);
                SqlDataReader dr_result = cmd.ExecuteReader();
                if (dr_result.HasRows)
                {
                    dr_result.Read();
                    oPersonal.personal_id = int.Parse(dr_result["IdPersonal"].ToString());
                    oPersonal.personal_apellido_paterno = dr_result["apellidoPaternoPersonal"].ToString();
                    oPersonal.personal_apellido_materno = dr_result["apellidoMaternoPersonal"].ToString();
                    oPersonal.personal_nombre = dr_result["nombrePersonal"].ToString();
                }
                return oPersonal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
