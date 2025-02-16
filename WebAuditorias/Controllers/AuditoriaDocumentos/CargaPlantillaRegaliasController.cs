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
    public class CargaPlantillaRegaliasController
    {
        public string CargaPlantillaRegalias(string filename, string sheetName, Int16 empresaCodigo, int auditoriaCodigo, Int16 tareaCodigo, Int16 plantillaCodigo, string referencia)
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
                        Plantilla_Regalias regalia = new Plantilla_Regalias
                        {
                            Codigo = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Fecha = !DateTime.TryParse(row[1].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[1].ToString()),
                            Descripcion = row[2] == DBNull.Value || row[2].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Moneda = row[3] == DBNull.Value || row[3].ToString().Trim() == "" ? "" : row[3].ToString(),
                            Valor_Fijo = !double.TryParse(row[4].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[4].ToString()),
                            Ingresos_Facturados = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Ingresos_Cartera = !double.TryParse(row[6].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[6].ToString()),
                            Retencion = !double.TryParse(row[7].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[7].ToString()),
                            Total_Soles = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Tasa_Cambio = !double.TryParse(row[9].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[9].ToString()),
                            Total_Dolares = !double.TryParse(row[10].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[10].ToString()),
                            Adjuntos = row[11] == DBNull.Value || row[11].ToString().Trim() == "" ? "" : row[11].ToString(),
                            Cuenta = row[12] == DBNull.Value || row[12].ToString().Trim() == "" ? "" : row[12].ToString(),
                            Soporte = row[13] == DBNull.Value || row[13].ToString().Trim() == "" ? "" : row[13].ToString(),
                            Observaciones = row[14] == DBNull.Value || row[14].ToString().Trim() == "" ? "" : row[14].ToString()
                        };

                        jsonString = JsonConvert.SerializeObject(regalia);

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
                        Plantilla_Regalias regalia = new Plantilla_Regalias
                        {
                            Codigo = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Fecha = !DateTime.TryParse(row[1].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[1].ToString()),
                            Descripcion = row[2] == DBNull.Value || row[2].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Moneda = row[3] == DBNull.Value || row[3].ToString().Trim() == "" ? "" : row[3].ToString(),
                            Valor_Fijo = !double.TryParse(row[4].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[4].ToString()),
                            Ingresos_Facturados = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Ingresos_Cartera = !double.TryParse(row[6].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[6].ToString()),
                            Retencion = !double.TryParse(row[7].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[7].ToString()),
                            Total_Soles = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Tasa_Cambio = !double.TryParse(row[9].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[9].ToString()),
                            Total_Dolares = !double.TryParse(row[10].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[10].ToString()),
                            Adjuntos = row[11] == DBNull.Value || row[11].ToString().Trim() == "" ? "" : row[11].ToString(),
                            Cuenta = row[12] == DBNull.Value || row[12].ToString().Trim() == "" ? "" : row[12].ToString(),
                            Soporte = row[13] == DBNull.Value || row[13].ToString().Trim() == "" ? "" : row[13].ToString(),
                            Observaciones = row[14] == DBNull.Value || row[14].ToString().Trim() == "" ? "" : row[14].ToString()
                        };

                        campoPlantilla = ValidarRegistroRegalia(regalia);

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

        public List<CampoPlantilla> ValidarRegistroRegalia(Plantilla_Regalias registro)
        {
            List<CampoPlantilla> respuesta = new List<CampoPlantilla>();

            if (registro.Codigo.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Codigo", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Fecha.Year == 1900)
                respuesta.Add(new CampoPlantilla() { Campo = "Fecha", Mensaje = ErroresPlantilla.FechaNoValida });

            if (registro.Descripcion.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Descripcion", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Moneda.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Moneda", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Valor_Fijo < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Valor_Fijo", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Ingresos_Facturados < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Ingresos_Facturados", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Ingresos_Cartera < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Ingresos_Cartera", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Retencion < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Retencion", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Total_Soles < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Total_Soles", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Tasa_Cambio < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Tasa_Cambio", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Total_Dolares < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Total_Dolares", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Cuenta.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Cuenta", Mensaje = ErroresPlantilla.CampoVacio });

            return respuesta;
        }
    }
}