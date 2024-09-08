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
    public class CargaPlantillaComisionesController
    {
        public string CargaPlantillaComisiones(string filename, string sheetName, Int16 empresaCodigo, int auditoriaCodigo, Int16 tareaCodigo, Int16 plantillaCodigo, string referencia)
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
                        Plantilla_Comisiones comision = new Plantilla_Comisiones
                        {
                            Mes = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Monto_Recuperado = !double.TryParse(row[1].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[1].ToString()),
                            Monto_Planilla = !double.TryParse(row[2].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[2].ToString()),
                            Monto_Honorarios = !double.TryParse(row[3].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[3].ToString()),
                            Total_Incentivos = !double.TryParse(row[4].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[4].ToString()),
                            Cheque_Girado = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Pagado = !double.TryParse(row[6].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[6].ToString()),
                            Entregado_Caja_Interna_1 = row[7] == DBNull.Value || row[7].ToString().Trim() == "" ? "" : row[7].ToString(),
                            No_Girado = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Fecha_Informe = !DateTime.TryParse(row[9].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[9].ToString()),
                            Fecha_Contabilidad = !DateTime.TryParse(row[10].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[10].ToString()),
                            Informe_Comisiones = row[11] == DBNull.Value || row[11].ToString().Trim() == "" ? "" : row[11].ToString(),
                            Entregado_Caja_Interna_2 = row[12] == DBNull.Value || row[12].ToString().Trim() == "" ? "" : row[12].ToString(),
                            Observaciones = row[13] == DBNull.Value || row[13].ToString().Trim() == "" ? "" : row[13].ToString()
                        };

                        jsonString = JsonConvert.SerializeObject(comision);

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
                        Plantilla_Comisiones comision = new Plantilla_Comisiones
                        {
                            Mes = row[0] == DBNull.Value || row[0].ToString().Trim() == "" ? "" : row[0].ToString(),
                            Monto_Recuperado = !double.TryParse(row[1].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[1].ToString()),
                            Monto_Planilla = !double.TryParse(row[2].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[2].ToString()),
                            Monto_Honorarios = !double.TryParse(row[3].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[3].ToString()),
                            Total_Incentivos = !double.TryParse(row[4].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[4].ToString()),
                            Cheque_Girado = !double.TryParse(row[5].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[5].ToString()),
                            Pagado = !double.TryParse(row[6].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[6].ToString()),
                            Entregado_Caja_Interna_1 = row[7] == DBNull.Value || row[7].ToString().Trim() == "" ? "" : row[7].ToString(),
                            No_Girado = !double.TryParse(row[8].ToString().Trim(), out valorDecimal) ? 0 : double.Parse(row[8].ToString()),
                            Fecha_Informe = !DateTime.TryParse(row[9].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[9].ToString()),
                            Fecha_Contabilidad = !DateTime.TryParse(row[10].ToString().Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(row[10].ToString()),
                            Informe_Comisiones = row[11] == DBNull.Value || row[11].ToString().Trim() == "" ? "" : row[11].ToString(),
                            Entregado_Caja_Interna_2 = row[12] == DBNull.Value || row[12].ToString().Trim() == "" ? "" : row[12].ToString(),
                            Observaciones = row[13] == DBNull.Value || row[13].ToString().Trim() == "" ? "" : row[13].ToString()
                        };

                        campoPlantilla = ValidarRegistroComision(comision);

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

        public List<CampoPlantilla> ValidarRegistroComision(Plantilla_Comisiones registro)
        {
            List<CampoPlantilla> respuesta = new List<CampoPlantilla>();

            if (registro.Mes.Trim() == "")
                respuesta.Add(new CampoPlantilla() { Campo = "Mes", Mensaje = ErroresPlantilla.CampoVacio });

            if (registro.Monto_Recuperado == 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Monto_Recuperado", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Monto_Planilla == 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Monto_Planilla", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Monto_Honorarios == 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Monto_Planilla", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Cheque_Girado == 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Cheque_Girado", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Pagado == 0)
                respuesta.Add(new CampoPlantilla() { Campo = "Pagado", Mensaje = ErroresPlantilla.CantidadNoValida });

            if (registro.Fecha_Informe.Year == 1900)
                respuesta.Add(new CampoPlantilla() { Campo = "Fecha_Pago", Mensaje = ErroresPlantilla.FechaNoValida });

            return respuesta;
        }
    }
}