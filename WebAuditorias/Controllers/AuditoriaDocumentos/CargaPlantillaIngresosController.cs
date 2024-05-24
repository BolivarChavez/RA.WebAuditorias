using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Data.OleDb;
using System.Data;
using System.Globalization;

namespace WebAuditorias.Controllers.AuditoriaDocumentos
{
    public class CargaPlantillaIngresosController
    {
        public string CargaPlantillaIngresos(string filename, string sheetName, Int16 empresaCodigo, int auditoriaCodigo, Int16 tareaCodigo, Int16 plantillaCodigo, string referencia)
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
                        Plantilla_Ingresos ingreso = new Plantilla_Ingresos
                        {
                            Mes = row[0].ToString(),
                            Factura = row[1].ToString(),
                            Cuenta = row[2].ToString(),
                            Detalle = row[3].ToString(),
                            Concepto = row[4].ToString(),
                            Subtotal = row[5].ToString() == "" ? 0 : double.Parse(row[5].ToString()),
                            Porcentaje = row[6].ToString() == "" ? 0 : double.Parse(row[6].ToString()),
                            Total = row[7].ToString() == "" ? 0 : double.Parse(row[7].ToString()),
                            Fecha_Detraccion = DateTime.Parse(row[8].ToString()),
                            Detraccion_Moneda_Destino = row[9].ToString() == "" ? 0 : double.Parse(row[9].ToString()),
                            Detraccion_Moneda_Base = row[10].ToString() == "" ? 0 : double.Parse(row[10].ToString()),
                            Comprobante_Ingreso = row[11].ToString(),
                            Neto_Ingreso = row[12].ToString() == "" ? 0 : double.Parse(row[12].ToString()),
                            Flujo = row[13].ToString(),
                            Estado_Cuenta_1 = row[14].ToString(),
                            Estado_Cuenta_2 = row[15].ToString()
                        };

                        jsonString = JsonConvert.SerializeObject(ingreso);

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
