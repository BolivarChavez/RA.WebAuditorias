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
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class PlantillaIngresos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
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

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
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
            Plantilla_Ingresos ingreso = new Plantilla_Ingresos();
            string jsonString;
            string response;

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            ingreso.Mes = Mes.Value.ToUpper();
            ingreso.Factura = Factura.Value.ToUpper();
            ingreso.Cuenta = Cuenta.Value.ToUpper();
            ingreso.Detalle = Detalle.Value.ToUpper();
            ingreso.Concepto = Concepto.Value.ToUpper();
            ingreso.Subtotal = double.Parse(Subtotal.Value, CultureInfo.InvariantCulture);
            ingreso.Porcentaje = double.Parse(Porcentaje.Value, CultureInfo.InvariantCulture);
            ingreso.Total = double.Parse(Total.Value, CultureInfo.InvariantCulture);
            ingreso.Fecha_Detraccion = DateTime.Parse(Fecha_Detraccion.Value);
            ingreso.Detraccion_Moneda_Destino = double.Parse(Detraccion_Moneda_Destino.Value, CultureInfo.InvariantCulture);
            ingreso.Detraccion_Moneda_Base = double.Parse(Detraccion_Moneda_Base.Value, CultureInfo.InvariantCulture);
            ingreso.Comprobante_Ingreso = Comprobante_Ingreso.Value.ToUpper();
            ingreso.Neto_Ingreso = double.Parse(Neto_Ingreso.Value, CultureInfo.InvariantCulture);
            ingreso.Flujo = Flujo.Value.ToUpper();
            ingreso.Estado_Cuenta_1 = Estado_Cuenta_1.Value.ToUpper();
            ingreso.Estado_Cuenta_2 = Estado_Cuenta_2.Value.ToUpper();
            ingreso.Soporte = Soporte.Value.ToUpper();

            jsonString = JsonConvert.SerializeObject(ingreso);

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
            parametro.ad_usuario_creacion = user_cookie.Usuario;
            parametro.ad_fecha_creacion = DateTime.Now;
            parametro.ad_usuario_actualizacion = user_cookie.Usuario;
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
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            Plantilla_Ingresos ingreso = new Plantilla_Ingresos();
            string jsonString;
            string response;

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            ingreso.Mes = Mes.Value.ToUpper();
            ingreso.Factura = Factura.Value.ToUpper();
            ingreso.Cuenta = Cuenta.Value.ToUpper();
            ingreso.Detalle = Detalle.Value.ToUpper();
            ingreso.Concepto = Concepto.Value.ToUpper();
            ingreso.Subtotal = double.Parse(Subtotal.Value, CultureInfo.InvariantCulture);
            ingreso.Porcentaje = double.Parse(Porcentaje.Value, CultureInfo.InvariantCulture);
            ingreso.Total = double.Parse(Total.Value, CultureInfo.InvariantCulture);
            ingreso.Fecha_Detraccion = DateTime.Parse(Fecha_Detraccion.Value);
            ingreso.Detraccion_Moneda_Destino = double.Parse(Detraccion_Moneda_Destino.Value, CultureInfo.InvariantCulture);
            ingreso.Detraccion_Moneda_Base = double.Parse(Detraccion_Moneda_Base.Value, CultureInfo.InvariantCulture);
            ingreso.Comprobante_Ingreso = Comprobante_Ingreso.Value.ToUpper();
            ingreso.Neto_Ingreso = double.Parse(Neto_Ingreso.Value, CultureInfo.InvariantCulture);
            ingreso.Flujo = Flujo.Value.ToUpper();
            ingreso.Estado_Cuenta_1 = Estado_Cuenta_1.Value.ToUpper();
            ingreso.Estado_Cuenta_2 = Estado_Cuenta_2.Value.ToUpper();
            ingreso.Soporte = Soporte.Value.ToUpper();

            jsonString = JsonConvert.SerializeObject(ingreso);

            parametro.ad_empresa = 1;
            parametro.ad_auditoria = auditoriaId;
            parametro.ad_tarea = tareaId;
            parametro.ad_codigo = Int16.Parse(Codigo.Value);
            parametro.ad_plantilla = plantillaId;
            parametro.ad_referencia = "";
            parametro.ad_registro = jsonString;
            parametro.ad_auditoria_origen = 0;
            parametro.ad_responsable = 0;
            parametro.ad_estado = "X";
            parametro.ad_usuario_creacion = user_cookie.Usuario;
            parametro.ad_fecha_creacion = DateTime.Now;
            parametro.ad_usuario_actualizacion = user_cookie.Usuario;
            parametro.ad_fecha_actualizacion = DateTime.Now;

            response = _controller.Actualizacion(parametro);

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "document.getElementById('profile-tab').click(); LlenaGrid();", true);
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

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillas(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Ingresos ingreso = new Plantilla_Ingresos();
            List<Plantilla_Ingresos_Base> listaIngresos = new List<Plantilla_Ingresos_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();

            foreach (var lineaDoc in _documentos)
            {
                ingreso = JsonConvert.DeserializeObject<Plantilla_Ingresos>(lineaDoc.ad_registro);
                listaIngresos.Add(
                    new Plantilla_Ingresos_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = "",
                        Mes = ingreso.Mes,
                        Factura = ingreso.Factura,
                        Cuenta = ingreso.Cuenta,
                        Detalle = ingreso.Detalle,
                        Concepto = ingreso.Concepto,
                        Subtotal = ingreso.Subtotal,
                        Porcentaje = ingreso.Porcentaje,
                        Total = ingreso.Total,
                        Fecha_Detraccion = ingreso.Fecha_Detraccion,
                        Detraccion_Moneda_Destino = ingreso.Detraccion_Moneda_Destino,
                        Detraccion_Moneda_Base = ingreso.Detraccion_Moneda_Base,
                        Comprobante_Ingreso = ingreso.Comprobante_Ingreso,
                        Neto_Ingreso = ingreso.Neto_Ingreso,
                        Flujo = ingreso.Flujo,
                        Estado_Cuenta_1 = ingreso.Estado_Cuenta_1,
                        Estado_Cuenta_2 = ingreso.Estado_Cuenta_2,
                        Soporte = ingreso.Soporte
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaIngresos);
        }

        protected void BtnCargaPlantilla_ServerClick(object sender, EventArgs e)
        {
            CargaPlantillaIngresosController plantilla = new CargaPlantillaIngresosController();
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

            response = plantilla.CargaPlantillaIngresos(archivoCarga, hojaArchivo, 1, auditoriaId, tareaId, plantillaId, referencia);

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

        protected void BtnAddTareaGrupo_ServerClick(object sender, EventArgs e)
        {
            string parametros = "";

            parametros += "1|";
            parametros += Auditoria.Value.ToString().Split('-')[0] + "|";
            parametros += Auditoria.Value.ToString().Substring(Auditoria.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Tarea.Value.ToString().Split('-')[0] + "|";
            parametros += Tarea.Value.ToString().Substring(Tarea.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Plantilla.Value.ToString().Split('-')[0] + "|" + Plantilla.Value.ToString().Split('-')[1] + "|" + Codigo.Value.ToString();

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "window.open('AuditoriaDocumentoProcesoGrupo.aspx?plantilla=" + parametros + "', '_blank');", true);
        }
    }
}