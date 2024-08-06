using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.Auditorias;
using WebAuditorias.Controllers.CatalogoProcesos;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Oficinas;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class Auditorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
                CargaOficinas();
                CargaProcesos();
            }
        }

        private void InitializedView()
        {
            HiddenField1.Value = "I";
            Codigo.Value = "0";
            FechaInicio.Value = DateTime.Today.ToString("yyyy-MM-dd");
            FechaCierre.Value = DateTime.Today.ToString("yyyy-MM-dd");
            Observaciones.Value = "";
            Estado.SelectedIndex = 0;
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        private void CargaOficinas()
        {
            OficinasController _controller = new OficinasController();
            List<Models.Oficinas> _oficinas = new List<Models.Oficinas>();

            _oficinas = _controller.Consulta(1, 0).ToList();

            OficinaOrigen.DataSource = _oficinas;
            OficinaOrigen.DataValueField = "of_codigo";
            OficinaOrigen.DataTextField = "of_nombre";
            OficinaOrigen.DataBind();

            OficinaDestino.DataSource = _oficinas;
            OficinaDestino.DataValueField = "of_codigo";
            OficinaDestino.DataTextField = "of_nombre";
            OficinaDestino.DataBind();
        }

        private void CargaProcesos()
        {
            CatalogoProcesosController _controller = new CatalogoProcesosController();
            List<Models.CatalogoProcesos> _procesos = new List<Models.CatalogoProcesos>();

            _procesos = _controller.Consulta(1).ToList();

            TipoProceso.DataSource = _procesos;
            TipoProceso.DataValueField = "cp_codigo";
            TipoProceso.DataTextField = "cp_descripcion";
            TipoProceso.DataBind();
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


        protected void BtnAddTarea_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "CargaTareas();", true);
        }

        protected void BtnAddGasto_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "CargaGastos();", true);
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaAuditorias()
        {
            AuditoriasController _controller = new AuditoriasController();
            List<Models.Auditorias> _auditorias = new List<Models.Auditorias>();

            _auditorias = _controller.Consulta(1, 0, 0).OrderByDescending(au => au.au_codigo).ToList();

            return JsonConvert.SerializeObject(_auditorias);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarAuditoria(string parametros)
        {
            AuditoriasController _controller = new AuditoriasController();
            Models.Auditorias parametro = new Models.Auditorias();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.au_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.au_codigo = int.Parse(arrayParametros[1].ToString());
            parametro.au_oficina_origen = Int16.Parse(arrayParametros[2].ToString());
            parametro.au_oficina_destino = Int16.Parse(arrayParametros[3].ToString());
            parametro.au_tipo_proceso = Int16.Parse(arrayParametros[4].ToString());
            parametro.au_fecha_inicio = DateTime.Parse(arrayParametros[5].ToString());
            parametro.au_fecha_cierre = DateTime.Parse(arrayParametros[6].ToString());
            parametro.au_tipo = arrayParametros[7].ToString().Trim().ToUpper();
            parametro.au_observaciones = arrayParametros[8].ToString().Trim().ToUpper();
            parametro.au_estado = arrayParametros[9].ToString().Trim().ToUpper();
            parametro.au_usuario_creacion = user_cookie.Usuario;
            parametro.au_fecha_creacion = DateTime.Now;
            parametro.au_usuario_actualizacion = user_cookie.Usuario;
            parametro.au_fecha_actualizacion = DateTime.Now;

            if (parametro.au_codigo == 0)
            {
                response = _controller.Ingreso(parametro);
            }
            else
            {
                response = _controller.Actualizacion(parametro);
            }

            return response;
        }

        [System.Web.Services.WebMethod]
        public static string CopiarAuditoria(string parametros)
        {
            AuditoriasController _controller = new AuditoriasController();
            Models.Auditorias parametro = new Models.Auditorias();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.au_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.au_codigo = int.Parse(arrayParametros[1].ToString());
            parametro.au_oficina_origen = Int16.Parse(arrayParametros[2].ToString());
            parametro.au_oficina_destino = Int16.Parse(arrayParametros[3].ToString());
            parametro.au_tipo_proceso = Int16.Parse(arrayParametros[4].ToString());
            parametro.au_fecha_inicio = DateTime.Parse(arrayParametros[5].ToString());
            parametro.au_fecha_cierre = DateTime.Parse(arrayParametros[6].ToString());
            parametro.au_tipo = arrayParametros[7].ToString().Trim().ToUpper();
            parametro.au_observaciones = arrayParametros[8].ToString().Trim().ToUpper();
            parametro.au_estado = arrayParametros[9].ToString().Trim().ToUpper();
            parametro.au_usuario_creacion = user_cookie.Usuario;
            parametro.au_fecha_creacion = DateTime.Now;
            parametro.au_usuario_actualizacion = user_cookie.Usuario;
            parametro.au_fecha_actualizacion = DateTime.Now;

            response = _controller.CopiaAuditoria(parametro);

            return response;
        }


        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarProceso();", true);
        }

        protected void BtnIniciar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "IniciarProceso();", true);
        }

        protected void BtnCerrar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "CerrarProceso();", true);
        }

        protected void BtnCopiar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "CopiarProceso();", true);
        }
    }
}