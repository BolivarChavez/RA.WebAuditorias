using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaTareaProcesos;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Responsables;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class AuditoriaTareaProceso : System.Web.UI.Page
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
            arrayParametros = Request.QueryString["auditoria"].Split('|');

            Auditoria.Value = arrayParametros[0];
            Tarea.Value = arrayParametros[1];
            Codigo.Value = "0";
            Fecha.Value = DateTime.Today.ToString("yyyy-MM-dd");
            Observaciones.Value = "";
            Documento.Value = "";
            chkEstado.Checked = false;
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
        public static string ConsultaTareasProcesos(string parametros)
        {
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaTareaProcesosController _controller = new AuditoriaTareaProcesosController();
            List<Models.AuditoriaTareaProcesos> _auditoriaTareasProcesos = new List<Models.AuditoriaTareaProcesos>();

            ResponsablesController _controllerResponsable = new ResponsablesController();
            List<Models.Responsables> _responsables = new List<Models.Responsables>();

            _responsables = _controllerResponsable.Consulta(1).ToList();
            _auditoriaTareasProcesos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2])).Where(lt => lt.at_estado != "X").ToList();

            var listaAuditoriaTareasProcesos = from proceso in _auditoriaTareasProcesos
                                       join auditor in _responsables on proceso.at_auditor equals auditor.re_codigo
                                       join responsable in _responsables on proceso.at_responsable equals responsable.re_codigo
                                       let responsableNombre = responsable.re_nombre
                                       orderby proceso.at_secuencia ascending
                                       select new
                                       {
                                           proceso.at_secuencia,
                                           proceso.at_fecha,
                                           proceso.at_auditor,
                                           auditor.re_nombre,
                                           proceso.at_responsable,
                                           responsableNombre,
                                           proceso.at_observaciones,
                                           proceso.at_documento,
                                           proceso.at_estado
                                       };

            return JsonConvert.SerializeObject(listaAuditoriaTareasProcesos);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarAuditoriaTareasProcesos(string parametros)
        {
            AuditoriaTareaProcesosController _controller = new AuditoriaTareaProcesosController();
            Models.AuditoriaTareaProcesos parametro = new Models.AuditoriaTareaProcesos();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.at_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.at_auditoria = int.Parse(arrayParametros[1].ToString());
            parametro.at_tarea = Int16.Parse(arrayParametros[2].ToString());
            parametro.at_secuencia = Int16.Parse(arrayParametros[3].ToString());
            parametro.at_auditor = Int16.Parse(arrayParametros[4].ToString());
            parametro.at_responsable = Int16.Parse(arrayParametros[5].ToString());
            parametro.at_fecha = DateTime.Parse(arrayParametros[6].ToString());
            parametro.at_observaciones = arrayParametros[7].ToString().ToUpper();
            parametro.at_documento = arrayParametros[8].ToString();
            parametro.at_estado = arrayParametros[9].ToString();
            parametro.at_usuario_creacion = user_cookie.Usuario;
            parametro.at_fecha_creacion = DateTime.Now;
            parametro.at_usuario_actualizacion = user_cookie.Usuario;
            parametro.at_fecha_actualizacion = DateTime.Now;

            if (parametro.at_secuencia == 0)
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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "LlenaGrid();", true);
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarProceso();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarProceso();", true);
            InitializedView();
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

        protected void BtnVerArchivo_ServerClick(object sender, EventArgs e)
        {
            if (Documento.Value.ToString().Trim() != "")
            {
                string url = string.Format("VistaArchivo.aspx?archivo={0}", Documento.Value.ToString().Trim());
                string script = "window.open('" + url + "')";
                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", script, true);
            }
        }
    }
}