using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentos;
using WebAuditorias.Controllers.Auditorias;
using WebAuditorias.Models.Bases;

namespace WebAuditorias.Views
{
    public partial class PlantillaCheques : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
            }
        }

        private void InitializedView()
        {
            string[] arrayParametros;
            arrayParametros = Request.QueryString["plantilla"].Split('|');

            Auditoria.Value = arrayParametros[0] + "-" + arrayParametros[1];
            Codigo.Value = "0";
            Tarea.Value = arrayParametros[2] + "-" + arrayParametros[3];
            Plantilla.Value = arrayParametros[4] + "-" + arrayParametros[5];
        }

        protected void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            InitializedView();
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "document.getElementById('profile-tab').click(); LlenaGrid();", true);
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillas(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Cheques cheque = new Plantilla_Cheques();
            List<Plantilla_Cheques_Base> listaCheques = new List<Plantilla_Cheques_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3])).ToList();

            foreach (var lineaDoc in _documentos)
            {
                cheque = JsonConvert.DeserializeObject<Plantilla_Cheques>(lineaDoc.ad_registro);
                listaCheques.Add(
                    new Plantilla_Cheques_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        Item = cheque.Item,
                        Talonario = cheque.Talonario,
                        Req = cheque.Req,
                        Beneficiario = cheque.Beneficiario,
                        Comprobante = cheque.Comprobante,
                        Monto = cheque.Monto,
                        Fecha_Pago = cheque.Fecha_Pago,
                        Comprobante_Egreso = cheque.Comprobante_Egreso,
                        Banco = cheque.Banco,
                        Numero_Cheque = cheque.Numero_Cheque,
                        Tipo_Cambio = cheque.Tipo_Cambio,
                        Observacion_Preliminar = cheque.Observacion_Preliminar,
                        Observacion_Final = cheque.Observacion_Final,
                        Estado = cheque.Estado,
                        Tipo_Plantilla = cheque.Tipo_Plantilla,
                        Cuentas = cheque.Cuentas
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaCheques);
        }

        public void CargaHojasArchivo(string fileName)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                fileName.Trim() +
                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            objConn = new OleDbConnection(constr);
            objConn.Open();
            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            objConn.Close();

            Hoja.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                if (row["TABLE_NAME"].ToString().Contains("$"))
                {
                    string s = row["TABLE_NAME"].ToString();
                    Hoja.Items.Add(s.StartsWith("'") ? s.Substring(1, s.Length - 3) : s.Substring(0, s.Length - 1));
                }
            }
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            Plantilla_Cheques cheque = new Plantilla_Cheques();
            string jsonString;
            string response;

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            cheque.Item = Item.Value.ToUpper();
            cheque.Talonario = Talonario.Value.ToUpper();
            cheque.Req = Req.Value.ToUpper();
            cheque.Beneficiario = Beneficiario.Value.ToUpper();
            cheque.Comprobante = Comprobante.Value.ToUpper();
            cheque.Monto = double.Parse(Monto.Value, CultureInfo.InvariantCulture);
            cheque.Fecha_Pago = DateTime.Parse(FechaPago.Value);
            cheque.Comprobante_Egreso = ComprobanteEgreso.Value.ToUpper();
            cheque.Banco = Banco.Value.ToUpper();
            cheque.Numero_Cheque = NumeroCheque.Value.ToUpper();
            cheque.Tipo_Cambio = double.Parse(TipoCambio.Value, CultureInfo.InvariantCulture);
            cheque.Observacion_Preliminar = ObservacionPreliminar.Value.ToUpper();
            cheque.Observacion_Final = Observacion_Final.Value.ToUpper();
            cheque.Estado = Estado.Value.ToUpper();
            cheque.Tipo_Plantilla = Tipo_Plantilla.Value.ToUpper();
            cheque.Cuentas = Cuentas.Value.ToUpper();

            jsonString = JsonConvert.SerializeObject(cheque);

            parametro.ad_empresa = 1;
            parametro.ad_auditoria = auditoriaId;
            parametro.ad_tarea = tareaId;
            parametro.ad_codigo = Int16.Parse(Codigo.Value);
            parametro.ad_plantilla = plantillaId;
            parametro.ad_referencia = "";
            parametro.ad_registro = jsonString;
            parametro.ad_auditoria_origen = 0;
            parametro.ad_responsable = 0;
            parametro.ad_estado = "A";
            parametro.ad_usuario_creacion = "usuario";
            parametro.ad_fecha_creacion = DateTime.Now;
            parametro.ad_usuario_actualizacion = "usuario";
            parametro.ad_fecha_actualizacion = DateTime.Now;

            if (Int16.Parse(Codigo.Value) == 0)
            {
                response = _controller.Ingreso(parametro);
            }
            else
            {
                response = _controller.Actualizacion(parametro);
            }

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "document.getElementById('profile-tab').click(); LlenaGrid();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {

        }

        protected void BtnCargar_ServerClick(object sender, EventArgs e)
        {
            string fileName;

            if (CargaArchivo.HasFile)
            {
                fileName = CargaArchivo.FileName.Trim();
                HiddenField2.Value = fileName;
                CargaArchivo.SaveAs(Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + fileName);
                CargaHojasArchivo(Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + fileName);
            }
        }

        protected void BtnCargaPlantilla_ServerClick(object sender, EventArgs e)
        {
            CargaPlantillaChequesController plantilla = new CargaPlantillaChequesController();
            string fileName = HiddenField2.Value;
            string archivoCarga = Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + fileName;
            string hojaArchivo = Hoja.Text.Trim();
            string referencia = Referencia.Value.Trim();
            string response = "";

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            response = plantilla.CargaPlantillaChqeues(archivoCarga, hojaArchivo, 1, auditoriaId, tareaId, plantillaId, referencia);

            if (response == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "alert('La plantilla se ha procesado correctamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "alert('Error : " + response  +"');", true);
            }
        }
    }
}