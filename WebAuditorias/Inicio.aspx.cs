using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using WebAuditorias.Controllers.Login;
using WebAuditorias.Models;

namespace WebAuditorias
{
    public partial class Inicio : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitializedView();
        }

        private void InitializedView()
        {
            UserId.Value = "";
            UserPassword.Value = "";

            if (HttpContext.Current.Request.Cookies["userInfo"] != null)
            {
                HttpContext.Current.Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
            }
        }

        protected void BtnLogin_ServerClick(object sender, EventArgs e)
        {
            LoginController _LoginController = new LoginController();
            string[] respuesta;

            LoginUsuario login = new LoginUsuario
            {
                usuario = UserId.Value.ToString().Trim(),
                password = UserPassword.Value.ToString().Trim()
            };

            System.Threading.Thread.Sleep(1000);
            respuesta = _LoginController.ProcesaLogin(login).Split('|');

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "validaLogin('" + respuesta[0].Trim() + "','" + respuesta[3].Trim() + "','" + respuesta[4].Trim() + "')", true);
        }
    }
}