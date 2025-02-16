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
    public class CargaPlantillaPagosController
    {
        public string CargaPlantillaPagos(string filename, string sheetName, Int16 empresaCodigo, int auditoriaCodigo, Int16 tareaCodigo, Int16 plantillaCodigo, string referencia)
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
                        Plantilla_Pagos pago = new Plantilla_Pagos
                        {
                            Periodo = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Detalle = row[1] == DBNull.Value || row[1].ToString().Trim() == "" ? "" : row[1].ToString(),
                            Fecha_Pago = !DateTime.TryParse(row[2].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[2].ToString()),
                            Importe_Bruto = !double.TryParse(row[3].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[3].ToString()),
                            Descuentos = !double.TryParse(row[4].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[4].ToString()),
                            Neto_Pagar = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Transferencia = !double.TryParse(row[6].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[6].ToString()),
                            Cheque = !double.TryParse(row[7].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[7].ToString()),
                            Diferencia = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Numero_Cheque = row[9] == DBNull.Value || row[9].ToString().Trim() == "" ? "" : row[9].ToString(),
                            Numero_Informe = row[10] == DBNull.Value || row[10].ToString().Trim() == "" ? "" : row[10].ToString(),
                            Observaciones = row[11] == DBNull.Value || row[11].ToString().Trim() == "" ? "" : row[11].ToString()
                        };

                        jsonString = JsonConvert.SerializeObject(pago);

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
                        Plantilla_Pagos pago = new Plantilla_Pagos
                        {
                            Periodo = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Detalle = row[1] == DBNull.Value || row[1].ToString().Trim() == "" ? "" : row[1].ToString(),
                            Fecha_Pago = !DateTime.TryParse(row[2].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[2].ToString()),
                            Importe_Bruto = !double.TryParse(row[3].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[3].ToString()),
                            Descuentos = !double.TryParse(row[4].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[4].ToString()),
                            Neto_Pagar = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Transferencia = !double.TryParse(row[6].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[6].ToString()),
                            Cheque = !double.TryParse(row[7].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[7].ToString()),
                            Diferencia = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Numero_Cheque = row[9] == DBNull.Value || row[9].ToString().Trim() == "" ? "" : row[9].ToString(),
                            Numero_Informe = row[10] == DBNull.Value || row[10].ToString().Trim() == "" ? "" : row[10].ToString(),
                            Observaciones = row[11] == DBNull.Value || row[11].ToString().Trim() == "" ? "" : row[11].ToString()
                        };

                        campoPlantilla = ValidarRegistroPago(pago);

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

        public List<CampoPlantilla> ValidarRegistroPago(Plantilla_Pagos registro)
        {
            List<CampoPlantilla> respuesta = new List<CampoPlantilla>();

            if (registro.Periodo.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Periodo", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Detalle.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Detalle", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Fecha_Pago.Year == 1900)
                respuesta.Add(new CampoPlantilla() { Campo = "Fecha_Pago", Mensaje = ErroresPlantilla.FechaNoValida });

            if (registro.Importe_Bruto < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Importe_Bruto", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Descuentos < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Descuentos", Mensaje = ErroresPlantilla.CantidadNoValida });

            return respuesta;
        }
    }
}