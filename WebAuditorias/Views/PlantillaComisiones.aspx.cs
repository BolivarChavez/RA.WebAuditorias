using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentos;
using WebAuditorias.Models.Bases;
using System.Globalization;

namespace WebAuditorias.Views
{
    public partial class PlantillaComisiones : System.Web.UI.Page
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

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            Plantilla_Comisiones comision = new Plantilla_Comisiones();
            string jsonString;
            string response;

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            comision.Mes = Mes.Value.ToUpper();
            comision.Monto_Recuperado = double.Parse(Monto_Recuperado.Value, CultureInfo.InvariantCulture);
            comision.Monto_Planilla = double.Parse(Monto_Planilla.Value, CultureInfo.InvariantCulture);
            comision.Monto_Honorarios = double.Parse(Monto_Honorarios.Value, CultureInfo.InvariantCulture);
            comision.Total_Incentivos = double.Parse(Total_Incentivos.Value, CultureInfo.InvariantCulture);
            comision.Cheque_Girado = double.Parse(Cheque_Girado.Value, CultureInfo.InvariantCulture);
            comision.Pagado = double.Parse(Pagado.Value, CultureInfo.InvariantCulture);
            comision.Entregado_Caja_Interna_1 = Entregado_Caja_Interna_1.Value.ToUpper();
            comision.No_Girado = double.Parse(No_Girado.Value, CultureInfo.InvariantCulture);
            comision.Fecha_Informe = DateTime.Parse(Fecha_Informe.Value);
            comision.Fecha_Contabilidad = DateTime.Parse(Fecha_Contabilidad.Value);
            comision.Informe_Comisiones = Informe_Comisiones.Value.ToUpper();
            comision.Entregado_Caja_Interna_2 = Entregado_Caja_Interna_2.Value.ToUpper();
            comision.Observaciones = Observaciones.Value.ToUpper();

            jsonString = JsonConvert.SerializeObject(comision);

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

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillas(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Comisiones comision = new Plantilla_Comisiones();
            List<Plantilla_Comisiones_Base> listaComisiones = new List<Plantilla_Comisiones_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3])).ToList();

            foreach (var lineaDoc in _documentos)
            {
                comision = JsonConvert.DeserializeObject<Plantilla_Comisiones>(lineaDoc.ad_registro);
                listaComisiones.Add(
                    new Plantilla_Comisiones_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        Mes = comision.Mes,
                        Monto_Recuperado = comision.Monto_Recuperado,
                        Monto_Planilla = comision.Monto_Planilla,
                        Monto_Honorarios = comision.Monto_Honorarios,
                        Total_Incentivos = comision.Total_Incentivos,
                        Cheque_Girado = comision.Cheque_Girado,
                        Pagado = comision.Pagado,
                        Entregado_Caja_Interna_1 = comision.Entregado_Caja_Interna_1,
                        No_Girado = comision.No_Girado,
                        Fecha_Informe = comision.Fecha_Informe,
                        Fecha_Contabilidad = comision.Fecha_Contabilidad,
                        Informe_Comisiones = comision.Informe_Comisiones,
                        Entregado_Caja_Interna_2 = comision.Entregado_Caja_Interna_2,
                        Observaciones = comision.Observaciones
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaComisiones);
        }

        protected void BtnCargaPlantilla_ServerClick(object sender, EventArgs e)
        {
            CargaPlantillaComisionesController plantilla = new CargaPlantillaComisionesController();
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

            response = plantilla.CargaPlantillaComisiones(archivoCarga, hojaArchivo, 1, auditoriaId, tareaId, plantillaId, referencia);

            if (response == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "alert('La plantilla se ha procesado correctamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "alert('Error : " + response + "');", true);
            }
        }

        protected void BtnAddTarea_ServerClick(object sender, EventArgs e)
        {
            string parametros = "";

            parametros += "1|";
            parametros += Auditoria.Value.ToString().Split('-')[0] + "|";
            parametros += Auditoria.Value.ToString().Substring(Auditoria.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Tarea.Value.ToString().Split('-')[0] + "|";
            parametros += Tarea.Value.ToString().Substring(Tarea.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Plantilla.Value.ToString().Split('-')[0] + "|" + Plantilla.Value.ToString().Split('-')[1] + "|" + Codigo.Value.ToString();

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "window.open('AuditoriaDocumentoProceso.aspx?plantilla=" + parametros + "', '_blank');", true);
        }
    }
}