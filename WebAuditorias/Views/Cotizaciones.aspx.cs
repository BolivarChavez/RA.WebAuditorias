using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Cotizaciones;
using WebAuditorias.Controllers.Monedas;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class Cotizaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
                CargaMonedas();
            }
        }

        private void InitializedView()
        {
            Cotizacion.Value = "0";
            chkEstado.Checked = false;
            Fecha.Value = DateTime.Today.ToString("yyyy-MM-dd");
            HiddenField1.Value = "I";
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            if (user_cookie.Usuario == null || user_cookie.Usuario.Trim() == "")
            {
                Response.Redirect("ErrorAccesoOpcion.aspx", true);
            }

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        private void CargaMonedas()
        {
            MonedasController _controller = new MonedasController();
            List<Models.Monedas> _monedas = new List<Models.Monedas>();

            _monedas = _controller.Consulta(0).ToList();

            MonedaBase.DataSource = _monedas;
            MonedaBase.DataValueField = "mo_codigo";
            MonedaBase.DataTextField = "mo_descripcion";
            MonedaBase.DataBind();

            MonedaDestino.DataSource = _monedas;
            MonedaDestino.DataValueField = "mo_codigo";
            MonedaDestino.DataTextField = "mo_descripcion";
            MonedaDestino.DataBind();
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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarCotizacion();", true);
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaCotizaciones(string parametros)
        {
            CotizacionesController _controller = new CotizacionesController();
            List<Models.Cotizaciones> _monedas = new List<Models.Cotizaciones>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _monedas = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3])).ToList();

            return JsonConvert.SerializeObject(_monedas);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarCotizacion(string parametros)
        {
            CotizacionesController _controller = new CotizacionesController();
            Models.Cotizaciones parametro = new Models.Cotizaciones();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.co_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.co_moneda_base = Int16.Parse(arrayParametros[1].ToString());
            parametro.co_moneda_destino = Int16.Parse(arrayParametros[2].ToString());
            parametro.co_cotizacion = double.Parse(arrayParametros[3].ToString().Trim(), CultureInfo.InvariantCulture);
            parametro.co_fecha_vigencia = DateTime.Parse(arrayParametros[4].ToString());
            parametro.co_estado = arrayParametros[5].ToString();
            parametro.co_usuario_creacion = user_cookie.Usuario;
            parametro.co_fecha_creacion = DateTime.Now;
            parametro.co_usurio_actualizacion = user_cookie.Usuario;
            parametro.co_fecha_actualizacion = DateTime.Now;

            if (arrayParametros[6].ToString().Trim() == "I")
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