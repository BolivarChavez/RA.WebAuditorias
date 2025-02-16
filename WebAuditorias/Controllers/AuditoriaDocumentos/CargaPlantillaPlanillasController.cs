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
    public class CargaPlantillaPlanillasController
    {
        public string CargaPlantillaPlanillas(string filename, string sheetName, Int16 empresaCodigo, int auditoriaCodigo, Int16 tareaCodigo, Int16 plantillaCodigo, string referencia)
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
                        Plantilla_Planillas planilla = new Plantilla_Planillas
                        {
                            Mes = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Fecha_Pago_Cash = !DateTime.TryParse(row[1].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[1].ToString()),
                            Lote = row[2] == DBNull.Value || row[2].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Remuneracion_Cash = !double.TryParse(row[3].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[3].ToString()),
                            Remuneracion_Cheque = !double.TryParse(row[4].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[4].ToString()),
                            Remuneracion_Total = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Fecha_Pago = !DateTime.TryParse(row[6].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[6].ToString()),
                            Honorarios_Planilla = !double.TryParse(row[7].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[7].ToString()),
                            Honorarios_Incentivos = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Honorarios_Total = !double.TryParse(row[9].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[9].ToString()),
                            Pagado = !double.TryParse(row[10].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[10].ToString()),
                            Honorarios_Cesantes = !double.TryParse(row[11].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[11].ToString()),
                            Diferencia = !double.TryParse(row[12].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[12].ToString()),
                            Fecha_Pago_Gratificacion = !DateTime.TryParse(row[13].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[13].ToString()),
                            Gratificaciones = !double.TryParse(row[14].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[14].ToString()),
                            Numero_Informe = row[15] == DBNull.Value || row[15].ToString().Trim() == "" ? "" : row[15].ToString(),
                            Observaciones = row[16] == DBNull.Value || row[16].ToString().Trim() == "" ? "" : row[16].ToString()
                        };

                        jsonString = JsonConvert.SerializeObject(planilla);

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
                        Plantilla_Planillas planilla = new Plantilla_Planillas
                        {
                            Mes = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Fecha_Pago_Cash = !DateTime.TryParse(row[1].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[1].ToString()),
                            Lote = row[2] == DBNull.Value || row[2].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Remuneracion_Cash = !double.TryParse(row[3].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[3].ToString()),
                            Remuneracion_Cheque = !double.TryParse(row[4].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[4].ToString()),
                            Remuneracion_Total = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Fecha_Pago = !DateTime.TryParse(row[6].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[6].ToString()),
                            Honorarios_Planilla = !double.TryParse(row[7].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[7].ToString()),
                            Honorarios_Incentivos = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Honorarios_Total = !double.TryParse(row[9].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[9].ToString()),
                            Pagado = !double.TryParse(row[10].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[10].ToString()),
                            Honorarios_Cesantes = !double.TryParse(row[11].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[11].ToString()),
                            Diferencia = !double.TryParse(row[12].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[12].ToString()),
                            Fecha_Pago_Gratificacion = !DateTime.TryParse(row[13].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[13].ToString()),
                            Gratificaciones = !double.TryParse(row[14].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[14].ToString()),
                            Numero_Informe = row[15] == DBNull.Value || row[15].ToString().Trim() == "" ? "" : row[15].ToString(),
                            Observaciones = row[16] == DBNull.Value || row[16].ToString().Trim() == "" ? "" : row[16].ToString()
                        };

                        campoPlantilla = ValidarRegistroPlanilla(planilla);

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

        public List<CampoPlantilla> ValidarRegistroPlanilla(Plantilla_Planillas registro)
        {
            List<CampoPlantilla> respuesta = new List<CampoPlantilla>();

            if (registro.Mes.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Mes", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Fecha_Pago_Cash.Year == 1900)
                respuesta.Add(new CampoPlantilla() { Campo = "Fecha_Pago_Cash", Mensaje = ErroresPlantilla.FechaNoValida });

            if (registro.Fecha_Pago.Year == 1900)
                respuesta.Add(new CampoPlantilla() { Campo = "Fecha_Pago", Mensaje = ErroresPlantilla.FechaNoValida });

            return respuesta;
        }
    }
}