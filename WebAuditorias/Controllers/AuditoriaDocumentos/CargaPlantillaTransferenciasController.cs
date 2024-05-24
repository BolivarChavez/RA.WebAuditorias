using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Data.OleDb;
using System.Data;
using System.Globalization;

namespace WebAuditorias.Controllers.AuditoriaDocumentos
{
    public class CargaPlantillaTransferenciasController
    {
        public string CargaPlantillaTransferencias(string filename, string sheetName, Int16 empresaCodigo, int auditoriaCodigo, Int16 tareaCodigo, Int16 plantillaCodigo, string referencia)
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
                        Plantilla_Transferencias transferencia = new Plantilla_Transferencias
                        {
                            Item = row[0].ToString(),
                            Proveedor = row[1].ToString(),
                            Concepto = row[2].ToString(),
                            Referencia = row[3].ToString(),
                            Mes = row[4].ToString(),
                            Importe_Monto = row[5].ToString() == "" ? 0 : double.Parse(row[5].ToString()),
                            Monto = row[6].ToString() == "" ? 0 : double.Parse(row[6].ToString()),
                            Tipo_Cambio = row[7].ToString() == "" ? 0 : double.Parse(row[7].ToString()),
                            Comprobante_Pago = row[8].ToString(),
                            Observacion_Preliminar = row[9].ToString(),
                            Observacion_Final = row[10].ToString(),
                            Estado = row[11].ToString(),
                            Banco = row[12].ToString(),
                            Empresa = row[13].ToString(),
                            Sede = row[14].ToString(),
                            Cuenta = row[15].ToString(),
                            Sub_Cuenta = row[16].ToString(),
                            Soporte = row[17].ToString()
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
    }
}