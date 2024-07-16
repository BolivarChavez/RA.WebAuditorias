using System;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Transacciones;
using WebAuditorias.Models;

namespace WebAuditorias
{
    public partial class Consola : System.Web.UI.Page
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
            TransaccionesUsuarioController _TransaccionesUsuarioController = new TransaccionesUsuarioController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();

            DivOpciones.InnerHtml = _TransaccionesUsuarioController.CreaOpcionesUsuario(int.Parse(user_cookie.CodigoUsuario));           
        }

        protected void Salir_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }
    }
}