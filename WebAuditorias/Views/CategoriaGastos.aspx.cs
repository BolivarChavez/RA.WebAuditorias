using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.CategoriaGastos;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class CategoriaGastos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                InitializedView();
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

        [System.Web.Services.WebMethod]
        public static string ConsultaGastos()
        {
            CategoriaGastosController _controller = new CategoriaGastosController();
            List<Models.CategoriaGastos> _gastos = new List<Models.CategoriaGastos>();

            _gastos = _controller.Consulta(1).ToList();
            return JsonConvert.SerializeObject(_gastos);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarGasto(string parametros)
        {
            CategoriaGastosController _controller = new CategoriaGastosController();
            Models.CategoriaGastos parametro = new Models.CategoriaGastos();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.cg_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.cg_codigo = Int16.Parse(arrayParametros[1].ToString());
            parametro.cg_descripcion = arrayParametros[2].ToString().Trim().ToUpper();
            parametro.cg_estado = arrayParametros[3].ToString();
            parametro.cg_usuario_creacion = user_cookie.Usuario;
            parametro.cg_fecha_creacion = DateTime.Now;
            parametro.cg_usuario_actualizacion = user_cookie.Usuario;
            parametro.cg_fecha_actualizacion = DateTime.Now;

            if (parametro.cg_codigo == 0)
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
        public static string EliminarGasto(string parametros)
        {
            CategoriaGastosController _controller = new CategoriaGastosController();
            Models.CategoriaGastos parametro = new Models.CategoriaGastos();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.cg_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.cg_codigo = Int16.Parse(arrayParametros[1].ToString());
            parametro.cg_descripcion = arrayParametros[2].ToString().Trim().ToUpper();
            parametro.cg_estado = arrayParametros[3].ToString();
            parametro.cg_usuario_creacion = user_cookie.Usuario;
            parametro.cg_fecha_creacion = DateTime.Now;
            parametro.cg_usuario_actualizacion = user_cookie.Usuario;
            parametro.cg_fecha_actualizacion = DateTime.Now;

            response = _controller.Actualizacion(parametro);
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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarGategoria();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarCategoria();", true);
        }
    }
}