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
    public class CargaPlantillaMutuosController
    {
        public string CargaPlantillaMutuos(string filename, string sheetName, Int16 empresaCodigo, int auditoriaCodigo, Int16 tareaCodigo, Int16 plantillaCodigo, string referencia)
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
                        Plantilla_Mutuos mutuo = new Plantilla_Mutuos
                        {
                            Codigo = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Banco = row[1] == DBNull.Value || row[1].ToString().Trim() == "" ? "" : row[1].ToString(),
                            Moneda = row[2] == DBNull.Value || row[2].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Detalle = row[3] == DBNull.Value || row[3].ToString().Trim() == "" ? "" : row[3].ToString(),
                            Fecha_Documento = !DateTime.TryParse(row[4].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[4].ToString()),
                            Monto_Prestamo = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Fecha_Pago_Cuota = !DateTime.TryParse(row[6].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[6].ToString()),
                            Numero_Cuota = row[7] == DBNull.Value || row[7].ToString().Trim() == "" ? "" : row[7].ToString(),
                            Valor_Cuota = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Comprobante_Pago = row[9] == DBNull.Value || row[9].ToString().Trim() == "" ? "" : row[9].ToString(),
                            Saldo_Pendiente = !double.TryParse(row[10].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[10].ToString()),
                            Cuotas_Pendientes = row[11] == DBNull.Value || row[11].ToString().Trim() == "" ? "" : row[11].ToString(),
                            Documento_Legal = row[12] == DBNull.Value || row[12].ToString().Trim() == "" ? "" : row[12].ToString(),
                            Observacion = row[13] == DBNull.Value || row[13].ToString().Trim() == "" ? "" : row[13].ToString()
                        };

                        jsonString = JsonConvert.SerializeObject(mutuo);

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
                        Plantilla_Mutuos mutuo = new Plantilla_Mutuos
                        {
                            Codigo = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Banco = row[1] == DBNull.Value || row[1].ToString().Trim() == "" ? "" : row[1].ToString(),
                            Moneda = row[2] == DBNull.Value || row[2].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Detalle = row[3] == DBNull.Value || row[3].ToString().Trim() == "" ? "" : row[3].ToString(),
                            Fecha_Documento = !DateTime.TryParse(row[4].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[4].ToString()),
                            Monto_Prestamo = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Fecha_Pago_Cuota = !DateTime.TryParse(row[6].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[6].ToString()),
                            Numero_Cuota = row[7] == DBNull.Value || row[7].ToString().Trim() == "" ? "" : row[7].ToString(),
                            Valor_Cuota = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Comprobante_Pago = row[9] == DBNull.Value || row[9].ToString().Trim() == "" ? "" : row[9].ToString(),
                            Saldo_Pendiente = !double.TryParse(row[10].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[10].ToString()),
                            Cuotas_Pendientes = row[11] == DBNull.Value || row[11].ToString().Trim() == "" ? "" : row[11].ToString(),
                            Documento_Legal = row[12] == DBNull.Value || row[12].ToString().Trim() == "" ? "" : row[12].ToString(),
                            Observacion = row[13] == DBNull.Value || row[13].ToString().Trim() == "" ? "" : row[13].ToString()
                        };

                        campoPlantilla = ValidarRegistroMutuo(mutuo);

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

        public List<CampoPlantilla> ValidarRegistroMutuo(Plantilla_Mutuos registro)
        {
            List<CampoPlantilla> respuesta = new List<CampoPlantilla>();

            if (registro.Codigo.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Codigo", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Banco.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Banco", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Moneda.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Moneda", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Detalle.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Detalle", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Fecha_Documento.Year == 1900)
                respuesta.Add(new CampoPlantilla() { Campo = "Fecha_Documento", Mensaje = ErroresPlantilla.FechaNoValida });

            if (registro.Monto_Prestamo < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Monto_Prestamo", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Fecha_Pago_Cuota.Year == 1900)
                respuesta.Add(new CampoPlantilla() { Campo = "Fecha_Pago_Cuota", Mensaje = ErroresPlantilla.FechaNoValida });

            if (registro.Numero_Cuota.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Numero_Cuota", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Valor_Cuota < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Valor_Cuota", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Comprobante_Pago.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Comprobante_Pago", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Saldo_Pendiente < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Saldo_Pendiente", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Cuotas_Pendientes.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Cuotas_Pendientes", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Observacion.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Observacion", Mensaje = ErroresPlantilla.CampoVacio });

            return respuesta;
        }
    }
}