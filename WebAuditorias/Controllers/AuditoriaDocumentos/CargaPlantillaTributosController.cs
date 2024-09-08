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
    public class CargaPlantillaTributosController
    {
        public string CargaPlantillaTributos(string filename, string sheetName, Int16 empresaCodigo, int auditoriaCodigo, Int16 tareaCodigo, Int16 plantillaCodigo, string referencia)
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
                        Plantilla_Tributos tributo = new Plantilla_Tributos
                        {
                            Fecha = !DateTime.TryParse(row[0].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[0].ToString()),
                            Periodo = row[1] == DBNull.Value || row[1].ToString().Trim() == "" ? "" : row[1].ToString(),
                            Tributo = row[2] == DBNull.Value || row[2].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Tributo_Resultante = !double.TryParse(row[3].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[3].ToString()),
                            Intereses = !double.TryParse(row[4].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[4].ToString()),
                            Total_Pagar = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Forma_Pago = row[6] == DBNull.Value || row[6].ToString().Trim() == "" ? "" : row[6].ToString(),
                            Egreso = row[7] == DBNull.Value || row[7].ToString().Trim() == "" ? "" : row[7].ToString(),
                            Fecha_Informe = !DateTime.TryParse(row[8].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[8].ToString()),
                            Numero_Informe = row[9] == DBNull.Value || row[9].ToString().Trim() == "" ? "" : row[9].ToString(),
                            Observaciones = row[10] == DBNull.Value || row[10].ToString().Trim() == "" ? "" : row[10].ToString(),
                        };

                        jsonString = JsonConvert.SerializeObject(tributo);

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
                        Plantilla_Tributos tributo = new Plantilla_Tributos
                        {
                            Fecha = !DateTime.TryParse(row[0].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[0].ToString()),
                            Periodo = row[1] == DBNull.Value || row[1].ToString().Trim() == "" ? "" : row[1].ToString(),
                            Tributo = row[2] == DBNull.Value || row[2].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Tributo_Resultante = !double.TryParse(row[3].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[3].ToString()),
                            Intereses = !double.TryParse(row[4].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[4].ToString()),
                            Total_Pagar = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Forma_Pago = row[6] == DBNull.Value || row[6].ToString().Trim() == "" ? "" : row[6].ToString(),
                            Egreso = row[7] == DBNull.Value || row[7].ToString().Trim() == "" ? "" : row[7].ToString(),
                            Fecha_Informe = !DateTime.TryParse(row[8].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[8].ToString()),
                            Numero_Informe = row[9] == DBNull.Value || row[9].ToString().Trim() == "" ? "" : row[9].ToString(),
                            Observaciones = row[10] == DBNull.Value || row[10].ToString().Trim() == "" ? "" : row[10].ToString(),
                        };

                        campoPlantilla = ValidarRegistroTributo(tributo);

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

        public List<CampoPlantilla> ValidarRegistroTributo(Plantilla_Tributos registro)
        {
            List<CampoPlantilla> respuesta = new List<CampoPlantilla>();

            if (registro.Fecha.Year == 1900)
                respuesta.Add(new CampoPlantilla() { Campo = "Fecha", Mensaje = ErroresPlantilla.FechaNoValida });

            if (registro.Periodo.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Periodo", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Tributo.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Tributo", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Tributo_Resultante == 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Tributo_Resultante", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Total_Pagar == 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Total_Pagar", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Forma_Pago.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Forma_Pago", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Fecha_Informe.Year == 1900)
                respuesta.Add(new CampoPlantilla() { Campo = "Fecha_Informe", Mensaje = ErroresPlantilla.FechaNoValida });

            return respuesta;
        }
    }
}