using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaGastos;
using WebAuditorias.Controllers.CatalogoGastos;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class AuditoriaGasto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
                CargaGastos();
            }
        }

        private void InitializedView()
        {
            Auditoria.Value = Request.QueryString["auditoria"];
            Codigo.Value = "0";
            Valor.Value = "0";
            FechaInicio.Value = DateTime.Today.ToString("yyyy-MM-dd");
            FechaFin.Value = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        private void CargaGastos()
        {
            CatalogoGastosController _controller = new CatalogoGastosController();
            List<Models.CatalogoGastos> _gastos = new List<Models.CatalogoGastos>();

            _gastos = _controller.Consulta(1).Where(ga => ga.cg_estado == "A").ToList();

            TipoGasto.DataSource = _gastos;
            TipoGasto.DataValueField = "cg_codigo";
            TipoGasto.DataTextField = "cg_descripion";
            TipoGasto.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaGastosAuditoria(string parametros)
        {
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaGastosController _controller = new AuditoriaGastosController();
            List<Models.AuditoriaGastos> _auditoriaGastos = new List<Models.AuditoriaGastos>();

            CatalogoGastosController _controllerGastos = new CatalogoGastosController();
            List<Models.CatalogoGastos> _gastos = new List<Models.CatalogoGastos>();

            _gastos = _controllerGastos.Consulta(1).ToList();
            _auditoriaGastos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1])).Where(ga => ga.ag_estado != "X").ToList();

            var listaAuditoriaGastos = from gastoAudit in _auditoriaGastos
                                       join gasto in _gastos on gastoAudit.ag_tipo  equals gasto.cg_codigo
                                       orderby gasto.cg_descripion ascending
                                       select new { gastoAudit.ag_secuencia, gastoAudit.ag_tipo, gasto.cg_descripion, gastoAudit.ag_fecha_inicio, gastoAudit.ag_fecha_fin, gastoAudit.ag_valor, gastoAudit.ag_estado };

            return JsonConvert.SerializeObject(listaAuditoriaGastos);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarAuditoriaGasto(string parametros)
        {
            AuditoriaGastosController _controller = new AuditoriaGastosController();
            Models.AuditoriaGastos parametro = new Models.AuditoriaGastos();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.ag_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.ag_auditoria = int.Parse(arrayParametros[1].ToString());
            parametro.ag_secuencia = Int16.Parse(arrayParametros[2].ToString());
            parametro.ag_tipo = Int16.Parse(arrayParametros[3].ToString());
            parametro.ag_fecha_inicio = DateTime.Parse(arrayParametros[4].ToString());
            parametro.ag_fecha_fin = DateTime.Parse(arrayParametros[5].ToString());
            parametro.ag_valor = double.Parse(arrayParametros[6].ToString(), CultureInfo.InvariantCulture);
            parametro.ag_estado = arrayParametros[7].ToString().Trim().ToUpper();
            parametro.ag_usuario_creacion = user_cookie.Usuario;
            parametro.ag_fecha_creacion = DateTime.Now;
            parametro.ag_usuario_actualizacion = user_cookie.Usuario;
            parametro.ag_fecha_actualizacion = DateTime.Now;

            if (arrayParametros[2].ToString().Trim().ToUpper() == "0")
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