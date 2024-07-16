using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.CatalogoProcesos;
using WebAuditorias.Controllers.CatalogoTareas;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class CatalogoTareas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
                CargaProcesos();
            }
        }

        private void InitializedView()
        {
            Codigo.Value = "0";
            Descripcion.Value = "";
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

        private void CargaProcesos()
        {
            CatalogoProcesosController _controller = new CatalogoProcesosController();
            List<Models.CatalogoProcesos> _procesos = new List<Models.CatalogoProcesos>();

            _procesos = _controller.Consulta(1).ToList();

            Proceso.DataSource = _procesos;
            Proceso.DataValueField = "cp_codigo"; 
            Proceso.DataTextField = "cp_descripcion"; 
            Proceso.DataBind();
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

        [System.Web.Services.WebMethod]
        public static string ConsultaTareas()
        {
            CatalogoTareasController _controller = new CatalogoTareasController();
            List<Models.CatalogoTareas> _tareas = new List<Models.CatalogoTareas>();
            CatalogoProcesosController _controllerProcesos = new CatalogoProcesosController();
            List<Models.CatalogoProcesos> _procesos = new List<Models.CatalogoProcesos>();

            _tareas = _controller.Consulta(1).ToList();
            _procesos = _controllerProcesos.Consulta(1).ToList();

            var listaTareas = from tarea in _tareas
                             join proceso in _procesos on tarea.ct_proceso equals proceso.cp_codigo
                             orderby proceso.cp_descripcion, tarea.ct_descripcion
                             select new { tarea.ct_codigo, tarea.ct_proceso, proceso.cp_descripcion, tarea.ct_descripcion, tarea.ct_estado };

            return JsonConvert.SerializeObject(listaTareas);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarTarea(string parametros)
        {
            CatalogoTareasController _controller = new CatalogoTareasController();
            Models.CatalogoTareas parametro = new Models.CatalogoTareas();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.ct_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.ct_proceso = Int16.Parse(arrayParametros[1].ToString());
            parametro.ct_codigo = Int16.Parse(arrayParametros[2].ToString());
            parametro.ct_descripcion = arrayParametros[3].ToString().Trim().ToUpper();
            parametro.ct_estado = arrayParametros[4].ToString();
            parametro.ct_usuario_creacion = user_cookie.Usuario;
            parametro.ct_fecha_creacion = DateTime.Now;
            parametro.ct_usuario_actualizacion = user_cookie.Usuario;
            parametro.ct_fecha_actualizacion = DateTime.Now;

            if (parametro.ct_codigo == 0)
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
        public static string EliminarTarea(string parametros)
        {
            CatalogoTareasController _controller = new CatalogoTareasController();
            Models.CatalogoTareas parametro = new Models.CatalogoTareas();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.ct_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.ct_proceso = Int16.Parse(arrayParametros[1].ToString());
            parametro.ct_codigo = Int16.Parse(arrayParametros[2].ToString());
            parametro.ct_descripcion = arrayParametros[3].ToString().Trim().ToUpper();
            parametro.ct_estado = arrayParametros[4].ToString();
            parametro.ct_usuario_creacion = user_cookie.Usuario;
            parametro.ct_fecha_creacion = DateTime.Now;
            parametro.ct_usuario_actualizacion = user_cookie.Usuario;
            parametro.ct_fecha_actualizacion = DateTime.Now;

            response = _controller.Actualizacion(parametro);
            return response;
        }
    }
}