﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WebAuditorias.Controllers.CatalogoPlantillas;

namespace WebAuditorias.Views
{
    public partial class CatalogoPlantillas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitializedView();
        }

        private void InitializedView()
        {
            Codigo.Value = "0";
            Descripcion.Value = "";
            chkEstado.Checked = false;
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "LlenaGrid();", true);
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillas()
        {
            CatalogoPlantillasController _controller = new CatalogoPlantillasController();
            List<Models.CatalogoPlantillas> _plantillas = new List<Models.CatalogoPlantillas>();

            _plantillas = _controller.Consulta(1).ToList();
            return JsonConvert.SerializeObject(_plantillas);
        }

        protected void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            InitializedView();
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarPlantilla();", true);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarPlantilla(string parametros)
        {
            CatalogoPlantillasController _controller = new CatalogoPlantillasController();
            Models.CatalogoPlantillas parametro = new Models.CatalogoPlantillas();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.cp_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.cp_codigo = Int16.Parse(arrayParametros[1].ToString());
            parametro.cp_descripcion = arrayParametros[2].ToString().Trim().ToUpper();
            parametro.cp_estado = arrayParametros[3].ToString();
            parametro.cp_usuario_creacion = "usuario";
            parametro.cp_fecha_creacion = DateTime.Now;
            parametro.cp_usuario_actualizacion = "usuario";
            parametro.cp_fecha_actualizacion = DateTime.Now;

            if (parametro.cp_codigo == 0)
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
        public static string EliminarPlantilla(string parametros)
        {
            CatalogoPlantillasController _controller = new CatalogoPlantillasController();
            Models.CatalogoPlantillas parametro = new Models.CatalogoPlantillas();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.cp_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.cp_codigo = Int16.Parse(arrayParametros[1].ToString());
            parametro.cp_descripcion = arrayParametros[2].ToString().Trim().ToUpper();
            parametro.cp_estado = arrayParametros[3].ToString();
            parametro.cp_usuario_creacion = "usuario";
            parametro.cp_fecha_creacion = DateTime.Now;
            parametro.cp_usuario_actualizacion = "usuario";
            parametro.cp_fecha_actualizacion = DateTime.Now;

            response = _controller.Actualizacion(parametro);
            return response;
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarPlantilla();", true);
        }
    }
}