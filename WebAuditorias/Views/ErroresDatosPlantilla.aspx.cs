using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class ErroresDatosPlantilla : System.Web.UI.Page
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
            CargaListaErrores(user_cookie.Usuario);
        }

        private void CargaListaErrores(string usuario)
        {
            string result;
            string titulo = Request.QueryString["plantilla"].Trim();

            Errores.InnerHtml = "";

            ObjectCache cache = MemoryCache.Default;
            var contenido = (List<ValidaPlantilla>)cache.Get(usuario);

            result = "";
            result += @"<div class=""card text-left border-primary rounded-2 shadow"" style=""width: 100%;"">";
            result += @"<div class=""card-header text-primary"">";
            result += $"Errores en registros : <b>{titulo}</b>";
            result += @"</div>";
            result += @"<div class=""card-body"">";
            result += @"<table class=""table"" style=""font-size: 12px"">";
            result += @"<thead><tr>";
            result += @"<th>Fila</th>";
            result += @"<th>Campo</th>";
            result += @"<th>Error</th>";
            result += @"</tr></thead>";
            result += @"<tbody>";

            foreach (var item in contenido) 
            {
                foreach (var error in item.Campos)
                {
                    result += @"<tr>";
                    result += $"<td>{item.Linea}</td>";
                    result += $"<td>{error.Campo}</td>";
                    result += $"<td>{error.Mensaje}</td>";
                    result += @"</tr>";
                }
            }

            result += @"</tbody>";
            result += @"</table>";
            result += @"</div>";
            result += @"</div>";

            Errores.InnerHtml = result;
        }
    }
}