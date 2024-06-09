using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Oficinas;
using WebAuditorias.Controllers.Responsables;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class Responsables : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
                CargaOficinas();
            }
        }

        private void InitializedView()
        {
            HiddenField1.Value = "I";
            Codigo.Value = "0";
            Nombre.Value = "";
            Cargo.Value = "";
            Correo.Value = "";
            Usuario.Value = "";
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

        private void CargaOficinas()
        {
            OficinasController _controller = new OficinasController();
            List<Models.Oficinas> _oficinas = new List<Models.Oficinas>();

            _oficinas = _controller.Consulta(1, 0).ToList();

            Oficina.DataSource = _oficinas;
            Oficina.DataValueField = "of_codigo";
            Oficina.DataTextField = "of_nombre";
            Oficina.DataBind();
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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarResponsables();", true);
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaResponsables()
        {
            ResponsablesController _controller = new ResponsablesController();
            List<Models.Responsables> _responsables = new List<Models.Responsables>();

            _responsables = _controller.Consulta(1).ToList();

            return JsonConvert.SerializeObject(_responsables);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarResponsables(string parametros)
        {
            ResponsablesController _controller = new ResponsablesController();
            Models.Responsables parametro = new Models.Responsables();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.re_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.re_codigo = Int16.Parse(arrayParametros[1].ToString());
            parametro.re_nombre = arrayParametros[2].ToString().Trim().ToUpper();
            parametro.re_cargo = arrayParametros[3].ToString().Trim().ToUpper();
            parametro.re_oficina = Int16.Parse(arrayParametros[4].ToString());
            parametro.re_tipo = arrayParametros[5].ToString().Trim().ToUpper();
            parametro.re_correo = arrayParametros[6].ToString().Trim();
            parametro.re_usuario = arrayParametros[7].ToString().Trim();
            parametro.re_estado = arrayParametros[8].ToString().Trim().ToUpper();
            parametro.re_usuario_creacion = "usuario";
            parametro.re_fecha_creacion = DateTime.Now;
            parametro.re_usuario_actualizacion = "usuario";
            parametro.re_fecha_actualizacion = DateTime.Now;

            if (arrayParametros[9].ToString().Trim() == "I")
            {
                response = _controller.Ingreso(parametro);
            }
            else
            {
                response = _controller.Actualizacion(parametro);
            }

            return response;
        }
    }
}