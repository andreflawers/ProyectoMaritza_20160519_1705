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
    public class CDBoleta
    {
        public Boleta consultarBoleta(string num_doc, int mes, int ano)
        {
            try
            {
                Boleta oBoleta = new Boleta();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand cmd = new SqlCommand("sp_lis_boleta", oSqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@num_doc", SqlDbType.VarChar, 15).Value = num_doc;
                cmd.Parameters.Add("@mes", SqlDbType.Int).Value = mes;
                cmd.Parameters.Add("@ano", SqlDbType.Int).Value = ano;
                SqlDataAdapter oSqlDataAdapter = new SqlDataAdapter(cmd);
                SqlDataReader dr_result = cmd.ExecuteReader();
                if (dr_result.HasRows)
                {
                    dr_result.Read();
                    oBoleta.personal.personal_id = int.Parse(dr_result["id_personal"].ToString());
                    oBoleta.personal.personal_apellido_paterno = dr_result["apellidoPaternoPersonal"].ToString();
                    oBoleta.personal.personal_apellido_materno = dr_result["apellidoMaternoPersonal"].ToString();
                    oBoleta.personal.personal_nombre = dr_result["nombrePersonal"].ToString();
                    oBoleta.personal.personal_planilla = dr_result["planillaPersonal"].ToString();
                    oBoleta.cargo.nombreCargo = dr_result["nombreCargo"].ToString();
                    oBoleta.cargo.montoSalarioCargo = decimal.Parse(dr_result["montoSalarioCargo"].ToString());
                    oBoleta.trabajados = int.Parse(dr_result["asistencia"].ToString());
                    oBoleta.dominicales = int.Parse(dr_result["domingos"].ToString());
                    oBoleta.tardanzas = int.Parse(dr_result["tardanza"].ToString());
                    oBoleta.feriados = int.Parse(dr_result["feriados"].ToString());
                    oBoleta.dias_p = int.Parse(dr_result["dias_permiso"].ToString());
                    oBoleta.monto_p = decimal.Parse(dr_result["monto_permiso"].ToString());
                    oBoleta.dias_v = int.Parse(dr_result["dias_vacaciones"].ToString());
                }
                return oBoleta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DescuentoPlanilla> consultarDescuentoPlanilla(string parametro)
        {
            String procedure = "sp_lis_descuento_planilla";
            try
            {
                List<DescuentoPlanilla> oListDescuentoPlanilla = new List<DescuentoPlanilla>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                //oSqlCommand.Parameters.Add("@num_doc", SqlDbType.VarChar, 15).Value = num_doc;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    DescuentoPlanilla oDescuentoPlanilla = new DescuentoPlanilla();
                    oDescuentoPlanilla.IdDescuentoPlanilla = ((int)oSqlDataReader["IdDescuentoPlanilla"]);
                    oDescuentoPlanilla.nombreDescuentoPlanilla = ((string)oSqlDataReader["nombreDescuentoPlanilla"]);
                    oDescuentoPlanilla.montoDescuento = ((decimal)oSqlDataReader["montoDescuento"]);
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
        public List<Remuneracion> consultarBoletaRemuneracion(string num_doc)
        {
            String procedure = "sp_lis_boleta_remuneracion";
            try
            {
                List<Remuneracion> oListRemuneracion = new List<Remuneracion>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@num_doc", SqlDbType.VarChar, 15).Value = num_doc;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Remuneracion oRemuneracion = new Remuneracion();
                    oRemuneracion.nombreRemuneracion = ((String)oSqlDataReader["nombreRemuneracion"]);
                    oRemuneracion.montoRemuneracion = ((decimal)oSqlDataReader["montoRemuneracion"]);
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


        //Insertar Boleta
        public void insertar_boleta(string id_boleta, DateTime fecha_emision_boleta, decimal neto_a_cobrar, int id_personal)
        {
            String procedure = "sp_ins_boleta_insertar";
            SqlConnection oSqlConnection = new SqlConnection();
            AdministracionConexion oAdministracionConexion = new AdministracionConexion();
            oSqlConnection = oAdministracionConexion.getConexion();
            try
            {
                using (SqlCommand cmd = new SqlCommand(procedure, oSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@numeroBoleta", SqlDbType.Char, 10).Value = id_boleta;
                    cmd.Parameters.Add("@fechaEmisionBoleta", SqlDbType.Date).Value = fecha_emision_boleta;
                    cmd.Parameters.Add("@netoAcobrarBoleta", SqlDbType.Decimal).Value = neto_a_cobrar;
                    cmd.Parameters.Add("@idPersonal", SqlDbType.Int).Value = id_personal;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Insertar Boleta Planilla
        public void insertar_boleta_planilla(int id_descuento_planilla, string id_boleta, decimal monto_boleta_planilla)
        {
            String procedure = "sp_insertar_boleta_planilla_insertar";
            SqlConnection oSqlConnection = new SqlConnection();
            AdministracionConexion oAdministracionConexion = new AdministracionConexion();
            oSqlConnection = oAdministracionConexion.getConexion();
            try
            {
                using (SqlCommand cmd = new SqlCommand(procedure, oSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idDescuentoPlanilla", SqlDbType.Int).Value = id_descuento_planilla;
                    cmd.Parameters.Add("@numeroBoleta", SqlDbType.Char, 10).Value = id_boleta;
                    cmd.Parameters.Add("@montoBoletaPlanilla", SqlDbType.Decimal).Value = monto_boleta_planilla;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Insertar Boleta Detalle
        public void insertar_boleta_detalle(int id_personal, string id_boleta, int mes, int ano)
        {
            String procedure = "sp_ins_boleta_detalle_insertar";
            SqlConnection oSqlConnection = new SqlConnection();
            AdministracionConexion oAdministracionConexion = new AdministracionConexion();
            oSqlConnection = oAdministracionConexion.getConexion();
            try
            {
                using (SqlCommand cmd = new SqlCommand(procedure, oSqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idPersonal", SqlDbType.Int).Value = id_personal;
                    cmd.Parameters.Add("@numeroBoleta", SqlDbType.Char, 10).Value = id_boleta;
                    cmd.Parameters.Add("@mes", SqlDbType.Int).Value = mes;
                    cmd.Parameters.Add("@ano", SqlDbType.Int).Value = ano;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Obtener descuento planilla


        //Listar movimientos
        public List<Movimiento> consultarMovimientos(string num_doc, int mes, int ano)
        {
            String procedure = "sp_lis_movimientos_personal";
            try
            {
                List<Movimiento> oListMovimiento = new List<Movimiento>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@num_Doc", SqlDbType.VarChar, 15).Value = num_doc;
                oSqlCommand.Parameters.Add("@mes", SqlDbType.Int).Value = mes;
                oSqlCommand.Parameters.Add("@ano", SqlDbType.Int).Value = ano;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Movimiento oMovimiento = new Movimiento();

                    oMovimiento.montoMovimiento = ((decimal)oSqlDataReader["monto"]);
                    oMovimiento.tipoMovimiento.IdTipoMovimiento = ((int)oSqlDataReader["idTipoMovimiento"]);
                    oMovimiento.tipoMovimiento.nombreTipoMovimiento = ((string)oSqlDataReader["nombreTipoMovimiento"]);
                    oListMovimiento.Add(oMovimiento);
                }
                oSqlDataReader.Close();
                oSqlConnection.Close();
                return oListMovimiento;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }
        //Consultar Descuentos
        public List<Descuento> consultarDescuentos(string num_doc)
        {
            String procedure = "sp_lis_boleta_descuento";
            try
            {
                List<Descuento> oListDescuento = new List<Descuento>();
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand oSqlCommand = new SqlCommand(procedure, oSqlConnection);
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.Add("@num_doc", SqlDbType.VarChar, 15).Value = num_doc;
                SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader();
                while (oSqlDataReader.Read())
                {
                    Descuento oDescuento = new Descuento();
                    oDescuento.nombreDescuento = ((String)oSqlDataReader["nombreDescuento"]);
                    oDescuento.montoDescuento = ((decimal)oSqlDataReader["montoDescuento"]);
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


        //consultar numero boleta
        public String consultar_NumBoleta(string num_bol)
        {

            try
            {
                string num_boleta = "";
                SqlConnection oSqlConnection = new SqlConnection();
                AdministracionConexion oAdministracionConexion = new AdministracionConexion();
                oSqlConnection = oAdministracionConexion.getConexion();
                SqlCommand cmd = new SqlCommand("sp_consultar_boleta", oSqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@numeroBoleta", SqlDbType.Char).Value = num_bol;
                SqlDataReader oSqlDataReader = cmd.ExecuteReader();

                if (oSqlDataReader.Read())
                {

                    num_boleta = ((String)oSqlDataReader["numeroBoleta"]);


                }
                return num_boleta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
