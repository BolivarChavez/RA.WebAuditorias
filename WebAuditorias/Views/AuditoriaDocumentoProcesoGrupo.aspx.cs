using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentoProcesos;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Responsables;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class AuditoriaDocumentoProcesoGrupo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
                CargaResponsables();
            }
        }

        private void InitializedView()
        {
            string[] arrayParametros;
            arrayParametros = Request.QueryString["plantilla"].Split('|');

            Auditoria.Value = arrayParametros[1] + "-" + arrayParametros[2];
            Tarea.Value = arrayParametros[3] + "-" + arrayParametros[4];
            Plantilla.Value = arrayParametros[5] + "-" + arrayParametros[6];
            HiddenField1.Value = arrayParametros[7];
            Fecha.Value = DateTime.Today.ToString("yyyy-MM-dd");
            Observaciones.Value = "";
            Documento.Value = "";
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        private void CargaResponsables()
        {
            ResponsablesController _controller = new ResponsablesController();
            List<Models.Responsables> _responsables = new List<Models.Responsables>();

            _responsables = _controller.Consulta(1).Where(re => re.re_estado == "A").ToList();

            Auditor.DataSource = _responsables;
            Auditor.DataValueField = "re_codigo";
            Auditor.DataTextField = "re_nombre";
            Auditor.DataBind();

            Responsable.DataSource = _responsables;
            Responsable.DataValueField = "re_codigo";
            Responsable.DataTextField = "re_nombre";
            Responsable.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string GrabarDocumentosProcesos(string parametros)
        {
            AuditoriaDocumentoProcesosController _controller = new AuditoriaDocumentoProcesosController();
            Models.AuditoriaDocumentoProcesos parametro = new Models.AuditoriaDocumentoProcesos();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.ad_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.ad_auditoria = int.Parse(arrayParametros[1].ToString());
            parametro.ad_tarea = Int16.Parse(arrayParametros[2].ToString());
            parametro.ad_codigo = Int16.Parse(arrayParametros[3].ToString());
            parametro.ad_secuencia = Int16.Parse(arrayParametros[4].ToString());
            parametro.ad_fecha = DateTime.Parse(arrayParametros[5].ToString());
            parametro.ad_auditor = Int16.Parse(arrayParametros[6].ToString());
            parametro.ad_responsable = Int16.Parse(arrayParametros[7].ToString());
            parametro.ad_observaciones = arrayParametros[8].ToString().ToUpper();
            parametro.ad_documento = arrayParametros[9].ToString();
            parametro.ad_estado = arrayParametros[10].ToString();
            parametro.ad_usuario_creacion = user_cookie.Usuario;
            parametro.ad_fecha_creacion = DateTime.Now;
            parametro.ad_usuario_actualizacion = user_cookie.Usuario;
            parametro.ad_fecha_actualizacion = DateTime.Now;

            if (parametro.ad_secuencia == 0)
            {
                response = _controller.Ingreso(parametro);
            }
            else
            {
                response = _controller.Actualizacion(parametro);
            }

            return response;
        }

        protected void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            InitializedView();
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "seleccionTarea();", true);
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarProceso();", true);
        }

        protected void BtnCargar_ServerClick(object sender, EventArgs e)
        {
            string fileName;

            if (Archivo.HasFile)
            {
                fileName = Archivo.FileName.Trim();
                Documento.Value = fileName;
                Archivo.SaveAs(Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + fileName);
            }
        }
    }
}