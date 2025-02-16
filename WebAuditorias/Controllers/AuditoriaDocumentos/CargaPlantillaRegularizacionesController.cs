using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Data.OleDb;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using WebAuditorias.Models;

namespace WebAuditorias.Controllers.AuditoriaDocumentos
{
    public class CargaPlantillaRegularizacionesController
    {
        public string CargaPlantillaRegularizaciones(string filename, string sheetName, Int16 empresaCodigo, int auditoriaCodigo, Int16 tareaCodigo, Int16 plantillaCodigo, string referencia)
        {
            string constr;
            string jsonString;
            string response = "";
            double valorDecimal;
            DateTime fechaTabla;

            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            CultureInfo cultures = new CultureInfo("es-EC");

            constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                filename.Trim() +
                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
            OleDbConnection con = new OleDbConnection(constr);

            try
            {
                response = ValidarPlantilla(filename, sheetName);

                if (response.Trim() != "")
                    return response;

                OleDbCommand oconn = new OleDbCommand("Select * From [" + sheetName + "$]", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable data = new DataTable();
                sda.Fill(data);
                con.Close();

                foreach (DataRow row in data.Rows)
                {
                    if (row[0] != null && row[0].ToString().Trim() != "")
                    {
                        Plantilla_Regularizaciones regularizacion = new Plantilla_Regularizaciones
                        {
                            Mes = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Fecha = !DateTime.TryParse(row[1].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[1].ToString()),
                            Detalle = row[2] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Monto = !double.TryParse(row[3].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[3].ToString()),
                            Motivo = row[4] == DBNull.Value || row[4].ToString().Trim() == "" ? "" : row[4].ToString(),
                            Banco_Ingreso = row[5] == DBNull.Value || row[5].ToString().Trim() == "" ? "" : row[5].ToString(),
                            Banco_Regularizar = row[6] == DBNull.Value || row[6].ToString().Trim() == "" ? "" : row[6].ToString(),
                            Cuenta = row[7] == DBNull.Value || row[7].ToString().Trim() == "" ? "" : row[7].ToString(),
                            Estado = row[8] == DBNull.Value || row[8].ToString().Trim() == "" ? "" : row[8].ToString(),
                            Soporte = row[9] == DBNull.Value || row[9].ToString().Trim() == "" ? "" : row[9].ToString(),
                        };

                        jsonString = JsonConvert.SerializeObject(regularizacion);

                        parametro.ad_empresa = empresaCodigo;
                        parametro.ad_auditoria = auditoriaCodigo;
                        parametro.ad_tarea = tareaCodigo;
                        parametro.ad_codigo = 0;
                        parametro.ad_plantilla = plantillaCodigo;
                        parametro.ad_referencia = referencia.ToUpper();
                        parametro.ad_registro = jsonString;
                        parametro.ad_auditoria_origen = 0;
                        parametro.ad_responsable = 0;
                        parametro.ad_estado = "A";
                        parametro.ad_usuario_creacion = "usuario";
                        parametro.ad_fecha_creacion = DateTime.Now;
                        parametro.ad_usuario_actualizacion = "usuario";
                        parametro.ad_fecha_actualizacion = DateTime.Now;

                        _controller.Ingreso(parametro);
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                con.Close();
                response = ex.Message;
                return response;
            }
        }

        private string ValidarPlantilla(string filename, string sheetName)
        {
            string constr;
            string jsonString;
            string response = "";
            int linea = 1;
            double valorDecimal;
            DateTime fechaTabla;

            CultureInfo cultures = new CultureInfo("es-EC");
            List<ValidaPlantilla> validaPlantilla = new List<ValidaPlantilla>();
            List<CampoPlantilla> campoPlantilla = new List<CampoPlantilla>();

            constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                filename.Trim() +
                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
            OleDbConnection con = new OleDbConnection(constr);

            try
            {
                OleDbCommand oconn = new OleDbCommand("Select * From [" + sheetName + "$]", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable data = new DataTable();
                sda.Fill(data);
                con.Close();

                foreach (DataRow row in data.Rows)
                {
                    if (row[0] != null && row[0].ToString().Trim() != "")
                    {
                        Plantilla_Regularizaciones regularizacion = new Plantilla_Regularizaciones
                        {
                            Mes = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Fecha = !DateTime.TryParse(row[1].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[1].ToString()),
                            Detalle = row[2] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Monto = !double.TryParse(row[3].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[3].ToString()),
                            Motivo = row[4] == DBNull.Value || row[4].ToString().Trim() == "" ? "" : row[4].ToString(),
                            Banco_Ingreso = row[5] == DBNull.Value || row[5].ToString().Trim() == "" ? "" : row[5].ToString(),
                            Banco_Regularizar = row[6] == DBNull.Value || row[6].ToString().Trim() == "" ? "" : row[6].ToString(),
                            Cuenta = row[7] == DBNull.Value || row[7].ToString().Trim() == "" ? "" : row[7].ToString(),
                            Estado = row[8] == DBNull.Value || row[8].ToString().Trim() == "" ? "" : row[8].ToString(),
                            Soporte = row[9] == DBNull.Value || row[9].ToString().Trim() == "" ? "" : row[9].ToString(),
                        };

                        campoPlantilla = ValidarRegistroRegularizacion(regularizacion);

                        if (campoPlantilla.Count > 0)
                            validaPlantilla.Add(new ValidaPlantilla() { Linea = linea, Campos = campoPlantilla });

                        linea++;
                    }
                }

                if (validaPlantilla != null && validaPlantilla.Count > 0)
                {
                    jsonString = JsonConvert.SerializeObject(validaPlantilla);
                    return jsonString;
                }
                else
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                con.Close();
                response = ex.Message;
                return response;
            }
        }

        public List<CampoPlantilla> ValidarRegistroRegularizacion(Plantilla_Regularizaciones registro)
        {
            List<CampoPlantilla> respuesta = new List<CampoPlantilla>();

            if (registro.Mes.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Mes", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Fecha.Year == 1900)
                respuesta.Add(new CampoPlantilla() { Campo = "Fecha", Mensaje = ErroresPlantilla.FechaNoValida });

            if (registro.Detalle.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Detalle", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Monto < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Monto", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Motivo.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Motivo", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Banco_Ingreso.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Banco_Ingreso", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Banco_Regularizar.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Banco_Regularizar", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Cuenta.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Cuenta", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Estado.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Estado", Mensaje = ErroresPlantilla.CampoVacio });

            return respuesta;
        }
    }
}