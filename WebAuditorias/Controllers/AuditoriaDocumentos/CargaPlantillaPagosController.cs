using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Data.OleDb;
using System.Data;
using System.Globalization;

namespace WebAuditorias.Controllers.AuditoriaDocumentos
{
    public class CargaPlantillaPagosController
    {
        public string CargaPlantillaPagos(string filename, string sheetName, Int16 empresaCodigo, int auditoriaCodigo, Int16 tareaCodigo, Int16 plantillaCodigo, string referencia)
        {
            string constr;
            string jsonString;
            string response = "";

            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            CultureInfo cultures = new CultureInfo("es-EC");

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
                            Periodo = row[0].ToString(),
                            Detalle = row[1].ToString(),
                            Fecha_Pago = DateTime.Parse(row[2].ToString()),
                            Importe_Bruto = row[3].ToString() == "" ? 0 : double.Parse(row[3].ToString()),
                            Descuentos = row[4].ToString() == "" ? 0 : double.Parse(row[4].ToString()),
                            Neto_Pagar = row[5].ToString() == "" ? 0 : double.Parse(row[5].ToString()),
                            Transferencia = row[6].ToString() == "" ? 0 : double.Parse(row[6].ToString()),
                            Cheque = row[7].ToString() == "" ? 0 : double.Parse(row[7].ToString()),
                            Diferencia = row[8].ToString() == "" ? 0 : double.Parse(row[8].ToString()),
                            Numero_Cheque = row[9].ToString(),
                            Numero_Informe = row[10].ToString(),
                            Observaciones = row[11].ToString()
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
    }
}