﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Monedas;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class Monedas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
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

            if (user_cookie.Usuario == null || user_cookie.Usuario.Trim() == "")
            {
                Response.Redirect("ErrorAccesoOpcion.aspx", true);
            }

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarMoneda();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarMoneda();", true);
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaModedas()
        {
            MonedasController _controller = new MonedasController();
            List<Models.Monedas> _monedas = new List<Models.Monedas>();

            _monedas = _controller.Consulta(0).ToList();

            return JsonConvert.SerializeObject(_monedas);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarMoneda(string parametros)
        {
            MonedasController _controller = new MonedasController();
            Models.Monedas parametro = new Models.Monedas();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.mo_codigo = Int16.Parse(arrayParametros[1].ToString());
            parametro.mo_descripcion = arrayParametros[2].ToString().Trim().ToUpper();
            parametro.mo_estado = arrayParametros[3].ToString();
            parametro.mo_usuario_creacion = user_cookie.Usuario;
            parametro.mo_fecha_creacion = DateTime.Now;
            parametro.mo_usuario_actualizacion = user_cookie.Usuario;
            parametro.mo_fecha_actualizacion = DateTime.Now;

            if (parametro.mo_codigo == 0)
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
        public static string EliminarMoneda(string parametros)
        {
            MonedasController _controller = new MonedasController();
            Models.Monedas parametro = new Models.Monedas();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.mo_codigo = Int16.Parse(arrayParametros[1].ToString());
            parametro.mo_descripcion = arrayParametros[2].ToString().Trim().ToUpper();
            parametro.mo_estado = arrayParametros[3].ToString();
            parametro.mo_usuario_creacion = user_cookie.Usuario;
            parametro.mo_fecha_creacion = DateTime.Now;
            parametro.mo_usuario_actualizacion = user_cookie.Usuario;
            parametro.mo_fecha_actualizacion = DateTime.Now;

            response = _controller.Actualizacion(parametro);
            return response;
        }
    }
}