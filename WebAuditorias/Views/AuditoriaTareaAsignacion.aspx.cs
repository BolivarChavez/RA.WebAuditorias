using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaAsignacion;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Responsables;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class AuditoriaTareaAsignacion : System.Web.UI.Page
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

            Responsable.DataSource = _responsables;
            Responsable.DataValueField = "re_codigo";
            Responsable.DataTextField = "re_nombre";
            Responsable.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaTareaAsignacion(string parametros)
        {
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaAsignacionController _controller = new AuditoriaAsignacionController();
            List<Models.AuditoriaAsignacion> _auditoriaAsignacion = new List<Models.AuditoriaAsignacion>();

            ResponsablesController _controllerResponsable = new ResponsablesController();
            List<Models.Responsables> _responsables = new List<Models.Responsables>();

            _responsables = _controllerResponsable.Consulta(1).ToList();
            _auditoriaAsignacion = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2])).Where(at => at.aa_estado != "X").ToList();

            var listaAuditoriaAsignacion = from asignacion in _auditoriaAsignacion
                                           join responsable in _responsables on asignacion.aa_responsable equals responsable.re_codigo
                                           orderby asignacion.aa_secuencia ascending
                                           select new { asignacion.aa_secuencia, asignacion.aa_responsable, responsable.re_nombre, asignacion.aa_tipo, asignacion.aa_rol, asignacion.aa_estado };

            return JsonConvert.SerializeObject(listaAuditoriaAsignacion);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarAuditoriaAsignacion(string parametros)
        {
            AuditoriaAsignacionController _controller = new AuditoriaAsignacionController();
            Models.AuditoriaAsignacion parametro = new Models.AuditoriaAsignacion();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.aa_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.aa_auditoria = int.Parse(arrayParametros[1].ToString());
            parametro.aa_tarea = Int16.Parse(arrayParametros[2].ToString());
            parametro.aa_secuencia = Int16.Parse(arrayParametros[3].ToString());
            parametro.aa_responsable = Int16.Parse(arrayParametros[4].ToString());
            parametro.aa_tipo = arrayParametros[5].ToString();
            parametro.aa_rol = arrayParametros[6].ToString();
            parametro.aa_estado = arrayParametros[7].ToString();
            parametro.aa_usuario_creacion = user_cookie.Usuario;
            parametro.aa_fecha_creacion = DateTime.Now;
            parametro.aa_usuario_actualizacion = user_cookie.Usuario;
            parametro.aa_fecha_actualizacion = DateTime.Now;

            if (parametro.aa_secuencia == 0)
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
        }
    }
}