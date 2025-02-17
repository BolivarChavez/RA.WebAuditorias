using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.Auditorias;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class ResumenAuditoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
            }
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
            Anio.Value = DateTime.Now.Year.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaResumen(string parametros)
        {
            AuditoriasController _controller = new AuditoriasController();
            List<Models.AuditoriaResumen> _resumen = new List<Models.AuditoriaResumen>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _resumen = _controller.ConsultaResumen(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1])).ToList();

            return JsonConvert.SerializeObject(_resumen);
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "LlenaGrid();", true);
        }
    }
}