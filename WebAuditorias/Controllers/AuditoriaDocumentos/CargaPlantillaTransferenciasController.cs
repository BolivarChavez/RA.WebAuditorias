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
    public class CargaPlantillaTransferenciasController
    {
        public string CargaPlantillaTransferencias(string filename, string sheetName, Int16 empresaCodigo, int auditoriaCodigo, Int16 tareaCodigo, Int16 plantillaCodigo, string referencia)
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
                        Plantilla_Transferencias transferencia = new Plantilla_Transferencias
                        {
                            Item = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Req = row[1] == DBNull.Value || row[1].ToString().Trim() == "" ? "" : row[1].ToString(),
                            Proveedor = row[2] == DBNull.Value || row[2].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Concepto = row[3] == DBNull.Value || row[3].ToString().Trim() == "" ? "" : row[3].ToString(),
                            Documento = row[4] == DBNull.Value || row[4].ToString().Trim() == "" ? "" : row[4].ToString(),
                            Mes = row[5] == DBNull.Value || row[5].ToString().Trim() == "" ? "" : row[5].ToString(),
                            Moneda = row[6] == DBNull.Value || row[6].ToString().Trim() == "" ? "" : row[6].ToString(),
                            Importe_Monto = !double.TryParse(row[7].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[7].ToString()),
                            Monto = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Tipo_Cambio = !double.TryParse(row[9].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[9].ToString()),
                            Comprobante_Pago = row[10] == DBNull.Value || row[10].ToString().Trim() == "" ? "" : row[10].ToString(),
                            Fecha_Pago = !DateTime.TryParse(row[11].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[11].ToString()),
                            Observacion_Preliminar = row[12] == DBNull.Value || row[12].ToString().Trim() == "" ? "" : row[12].ToString(),
                            Observacion_Final = row[13] == DBNull.Value || row[13].ToString().Trim() == "" ? "" : row[13].ToString(),
                            Estado = row[14] == DBNull.Value || row[14].ToString().Trim() == "" ? "" : row[14].ToString(),
                            Banco = row[15] == DBNull.Value || row[15].ToString().Trim() == "" ? "" : row[15].ToString(),
                            Empresa = row[16] == DBNull.Value || row[16].ToString().Trim() == "" ? "" : row[16].ToString(),
                            Sede = row[17] == DBNull.Value || row[17].ToString().Trim() == "" ? "" : row[17].ToString(),
                            Cuenta = row[18] == DBNull.Value || row[18].ToString().Trim() == "" ? "" : row[18].ToString(),
                            Sub_Cuenta = row[19] == DBNull.Value || row[19].ToString().Trim() == "" ? "" : row[19].ToString(),
                            Soporte = row[20] == DBNull.Value || row[20].ToString().Trim() == "" ? "" : row[20].ToString()
                        };

                        jsonString = JsonConvert.SerializeObject(transferencia);

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
                        Plantilla_Transferencias transferencia = new Plantilla_Transferencias
                        {
                            Item = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Req = row[1] == DBNull.Value || row[1].ToString().Trim() == "" ? "" : row[1].ToString(),
                            Proveedor = row[2] == DBNull.Value || row[2].ToString().Trim() == "" ? "" : row[2].ToString(),
                            Concepto = row[3] == DBNull.Value || row[3].ToString().Trim() == "" ? "" : row[3].ToString(),
                            Documento = row[4] == DBNull.Value || row[4].ToString().Trim() == "" ? "" : row[4].ToString(),
                            Mes = row[5] == DBNull.Value || row[5].ToString().Trim() == "" ? "" : row[5].ToString(),
                            Moneda = row[6] == DBNull.Value || row[6].ToString().Trim() == "" ? "" : row[6].ToString(),
                            Importe_Monto = !double.TryParse(row[7].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[7].ToString()),
                            Monto = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Tipo_Cambio = !double.TryParse(row[9].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[9].ToString()),
                            Comprobante_Pago = row[10] == DBNull.Value || row[10].ToString().Trim() == "" ? "" : row[10].ToString(),
                            Fecha_Pago = !DateTime.TryParse(row[11].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[11].ToString()),
                            Observacion_Preliminar = row[12] == DBNull.Value || row[12].ToString().Trim() == "" ? "" : row[12].ToString(),
                            Observacion_Final = row[13] == DBNull.Value || row[13].ToString().Trim() == "" ? "" : row[13].ToString(),
                            Estado = row[14] == DBNull.Value || row[14].ToString().Trim() == "" ? "" : row[14].ToString(),
                            Banco = row[15] == DBNull.Value || row[15].ToString().Trim() == "" ? "" : row[15].ToString(),
                            Empresa = row[16] == DBNull.Value || row[16].ToString().Trim() == "" ? "" : row[16].ToString(),
                            Sede = row[17] == DBNull.Value || row[17].ToString().Trim() == "" ? "" : row[17].ToString(),
                            Cuenta = row[18] == DBNull.Value || row[18].ToString().Trim() == "" ? "" : row[18].ToString(),
                            Sub_Cuenta = row[19] == DBNull.Value || row[19].ToString().Trim() == "" ? "" : row[19].ToString(),
                            Soporte = row[20] == DBNull.Value || row[20].ToString().Trim() == "" ? "" : row[20].ToString()
                        };

                        campoPlantilla = ValidarRegistroTransferencia(transferencia);

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

        public List<CampoPlantilla> ValidarRegistroTransferencia(Plantilla_Transferencias registro)
        {
            List<CampoPlantilla> respuesta = new List<CampoPlantilla>();

            if (registro.Moneda.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Moneda", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Importe_Monto < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Importe_Monto", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Monto < 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Monto", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Tipo_Cambio == 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Tipo_Cambio", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Comprobante_Pago.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Comprobante_Pago", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Fecha_Pago.Year == 1900)
                respuesta.Add(new CampoPlantilla() { Campo = "Fecha_Pago", Mensaje = ErroresPlantilla.FechaNoValida });

            if (registro.Observacion_Preliminar.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Observacion_Preliminar", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Observacion_Final.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Observacion_Final", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Estado.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Estado", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Banco.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Banco", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Empresa.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Empresa", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Sede.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Sede", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Cuenta.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Cuenta", Mensaje = ErroresPlantilla.CampoVacio });

            return respuesta;
        }
    }
}