using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.CatalogoProcesos;

namespace WebAuditorias.Views
{
    public partial class CatalogoProcesos : System.Web.UI.Page
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
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "GrabarProceso();", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "EliminarProceso();", true);
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaProcesos()
        {
            CatalogoProcesosController _controller = new CatalogoProcesosController();
            List<Models.CatalogoProcesos> _procesos = new List<Models.CatalogoProcesos>();

            _procesos = _controller.Consulta(1).ToList();
            return JsonConvert.SerializeObject(_procesos);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarProceso(string parametros)
        {
            CatalogoProcesosController _controller = new CatalogoProcesosController();
            Models.CatalogoProcesos parametro = new Models.CatalogoProcesos();
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
        public static string EliminarProceso(string parametros)
        {
            CatalogoProcesosController _controller = new CatalogoProcesosController();
            Models.CatalogoProcesos parametro = new Models.CatalogoProcesos();
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
    }
}