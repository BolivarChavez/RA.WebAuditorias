using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.CatalogoGastos;

namespace WebAuditorias.Views
{
    public partial class CatalogoGastos : System.Web.UI.Page
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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarGasto();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarGasto();", true);
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaGastos()
        {
            CatalogoGastosController _controller = new CatalogoGastosController();
            List<Models.CatalogoGastos> _gastos = new List<Models.CatalogoGastos>();

            _gastos = _controller.Consulta(1).ToList();
            return JsonConvert.SerializeObject(_gastos);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarGasto(string parametros)
        {
            CatalogoGastosController _controller = new CatalogoGastosController();
            Models.CatalogoGastos parametro = new Models.CatalogoGastos();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.cg_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.cg_codigo = Int16.Parse(arrayParametros[1].ToString());
            parametro.cg_descripion = arrayParametros[2].ToString().Trim().ToUpper();
            parametro.cg_estado = arrayParametros[3].ToString();
            parametro.cg_usuario_creacion = "usuario";
            parametro.cg_fecha_creacion = DateTime.Now;
            parametro.cg_usuario_actualizacion = "usuario";
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
            CatalogoGastosController _controller = new CatalogoGastosController();
            Models.CatalogoGastos parametro = new Models.CatalogoGastos();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.cg_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.cg_codigo = Int16.Parse(arrayParametros[1].ToString());
            parametro.cg_descripion = arrayParametros[2].ToString().Trim().ToUpper();
            parametro.cg_estado = arrayParametros[3].ToString();
            parametro.cg_usuario_creacion = "usuario";
            parametro.cg_fecha_creacion = DateTime.Now;
            parametro.cg_usuario_actualizacion = "usuario";
            parametro.cg_fecha_actualizacion = DateTime.Now;

            response = _controller.Actualizacion(parametro);
            return response;
        }
    }
}