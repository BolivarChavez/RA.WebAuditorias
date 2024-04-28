using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.Auditorias;
using WebAuditorias.Controllers.CatalogoPlantillas;
using WebAuditorias.Controllers.CatalogoProcesos;
using WebAuditorias.Controllers.CatalogoTareas;
using WebAuditorias.Controllers.Oficinas;
using WebAuditorias.Controllers.Responsables;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class Auditorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaOficinas();
                CargaProcesos();
                CargaTareas(Int16.Parse(TipoProceso.SelectedValue));
                CargaPlantillas();
            }
        }

        private void InitializedView()
        {
            HiddenField1.Value = "I";
            Codigo.Value = "0";
            FechaInicio.Value = DateTime.Today.ToString("yyyy-MM-dd");
            FechaCierre.Value = DateTime.Today.ToString("yyyy-MM-dd");
            Observaciones.Value = "";
        }

        private void CargaOficinas()
        {
            OficinasController _controller = new OficinasController();
            List<Models.Oficinas> _oficinas = new List<Models.Oficinas>();

            _oficinas = _controller.Consulta(1, 0).ToList();

            OficinaOrigen.DataSource = _oficinas;
            OficinaOrigen.DataValueField = "of_codigo";
            OficinaOrigen.DataTextField = "of_nombre";
            OficinaOrigen.DataBind();

            OficinaDestino.DataSource = _oficinas;
            OficinaDestino.DataValueField = "of_codigo";
            OficinaDestino.DataTextField = "of_nombre";
            OficinaDestino.DataBind();
        }

        private void CargaProcesos()
        {
            CatalogoProcesosController _controller = new CatalogoProcesosController();
            List<Models.CatalogoProcesos> _procesos = new List<Models.CatalogoProcesos>();

            _procesos = _controller.Consulta(1).ToList();

            TipoProceso.DataSource = _procesos;
            TipoProceso.DataValueField = "cp_codigo";
            TipoProceso.DataTextField = "cp_descripcion";
            TipoProceso.DataBind();
        }

        private void CargaTareas(Int16 codigoProceso)
        {
            CatalogoTareasController _controller = new CatalogoTareasController();
            List<Models.CatalogoTareas> _tareas = new List<Models.CatalogoTareas>();

            _tareas = _controller.Consulta(1).ToList();

            var listaTareas = from tarea in _tareas
                              where tarea.ct_proceso == codigoProceso
                              select new { tarea.ct_codigo, tarea.ct_descripcion };

            cboTareas.DataSource = listaTareas;
            cboTareas.DataValueField = "ct_codigo";
            cboTareas.DataTextField = "ct_descripcion";
            cboTareas.DataBind();
        }

        private void CargaPlantillas()
        {
            CatalogoPlantillasController _controller = new CatalogoPlantillasController();
            List<Models.CatalogoPlantillas> _plantillas = new List<Models.CatalogoPlantillas>();

            _plantillas = _controller.Consulta(1).ToList();

            cboPlantillas.DataSource = _plantillas;
            cboPlantillas.DataValueField = "cp_codigo";
            cboPlantillas.DataTextField = "cp_descripcion";
            cboPlantillas.DataBind();
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


        protected void BtnAddTarea_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "CargaPlantilla();", true);
        }

        protected void BtnAddGasto_ServerClick(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string ConsultaAuditorias()
        {
            AuditoriasController _controller = new AuditoriasController();
            List<Models.Auditorias> _auditorias = new List<Models.Auditorias>();

            _auditorias = _controller.Consulta(1, 0).ToList();

            return JsonConvert.SerializeObject(_auditorias);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarAuditoria(string parametros)
        {
            AuditoriasController _controller = new AuditoriasController();
            Models.Auditorias parametro = new Models.Auditorias();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            parametro.au_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.au_codigo = int.Parse(arrayParametros[1].ToString());
            parametro.au_oficina_origen = Int16.Parse(arrayParametros[2].ToString());
            parametro.au_oficina_destino = Int16.Parse(arrayParametros[3].ToString());
            parametro.au_tipo_proceso = Int16.Parse(arrayParametros[4].ToString());
            parametro.au_fecha_inicio = DateTime.Parse(arrayParametros[5].ToString());
            parametro.au_fecha_cierre = DateTime.Parse(arrayParametros[6].ToString());
            parametro.au_tipo = arrayParametros[7].ToString().Trim().ToUpper();
            parametro.au_observaciones = arrayParametros[8].ToString().Trim().ToUpper();
            parametro.au_estado = arrayParametros[9].ToString().Trim().ToUpper();
            parametro.au_usuario_creacion = "usuario";
            parametro.au_fecha_creacion = DateTime.Now;
            parametro.au_usuario_actualizacion = "usuario";
            parametro.au_fecha_actualizacion = DateTime.Now;

            if (parametro.au_codigo == 0)
            {
                response = _controller.Ingreso(parametro);
            }
            else
            {
                response = _controller.Actualizacion(parametro);
            }

            return response;
        }

        protected void TipoProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaTareas(Int16.Parse(TipoProceso.SelectedValue));
        }
    }
}