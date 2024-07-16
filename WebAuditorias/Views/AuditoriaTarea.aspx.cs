using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaTareas;
using WebAuditorias.Controllers.CatalogoPlantillas;
using WebAuditorias.Controllers.CatalogoTareas;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Oficinas;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class AuditoriaTarea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
                CargaOficinas();
                CargaPlantillas();
                CargaTareas(Int16.Parse(HiddenField1.Value.Trim()));
                Estado.SelectedIndex = 1;
            }
        }

        private void InitializedView()
        {
            string[] arrayParametros;
            arrayParametros = Request.QueryString["auditoria"].Split('|');

            Auditoria.Value = arrayParametros[0] + "-" + arrayParametros[1];
            HiddenField1.Value = arrayParametros[2].Trim();
            Codigo.Value = "0";
            Asignacion.Value = "";
            Tarea.Enabled = true;
            Estado.Enabled = false;
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        private void CargaOficinas()
        {
            OficinasController _controller = new OficinasController();
            List<Models.Oficinas> _oficinas = new List<Models.Oficinas>();

            _oficinas = _controller.Consulta(1, 0).ToList();

            Oficina.DataSource = _oficinas;
            Oficina.DataValueField = "of_codigo";
            Oficina.DataTextField = "of_nombre";
            Oficina.DataBind();

            Oficina.DataSource = _oficinas;
            Oficina.DataValueField = "of_codigo";
            Oficina.DataTextField = "of_nombre";
            Oficina.DataBind();
        }

        private void CargaTareas(Int16 codigoProceso)
        {
            CatalogoTareasController _controller = new CatalogoTareasController();
            List<Models.CatalogoTareas> _tareas = new List<Models.CatalogoTareas>();

            _tareas = _controller.Consulta(1).ToList();

            var listaTareas = from tarea in _tareas
                              where (tarea.ct_proceso == codigoProceso && tarea.ct_estado == "A")
                              select new { tarea.ct_codigo, tarea.ct_descripcion };

            Tarea.DataSource = listaTareas;
            Tarea.DataValueField = "ct_codigo";
            Tarea.DataTextField = "ct_descripcion";
            Tarea.DataBind();
        }

        private void CargaPlantillas()
        {
            CatalogoPlantillasController _controller = new CatalogoPlantillasController();
            List<Models.CatalogoPlantillas> _plantillas = new List<Models.CatalogoPlantillas>();

            _plantillas = _controller.Consulta(1).Where(pl => pl.cp_estado == "A").ToList();

            cboPlantillas.DataSource = _plantillas;
            cboPlantillas.DataValueField = "cp_codigo";
            cboPlantillas.DataTextField = "cp_descripcion";
            cboPlantillas.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaTareasAuditoria(string parametros)
        {
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaTareasController _controller = new AuditoriaTareasController();
            List<Models.AuditoriaTareas> _auditoriaTareas = new List<Models.AuditoriaTareas>();

            CatalogoTareasController _controllerTareas = new CatalogoTareasController();
            List<Models.CatalogoTareas> _tareas = new List<Models.CatalogoTareas>();

            _tareas = _controllerTareas.Consulta(1).Where(ta => ta.ct_proceso == int.Parse(arrayParametros[2])).ToList();
            _auditoriaTareas = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), 0).Where(lt => lt.at_estado != "X").ToList();

            var listaAuditoriaTareas = from tareaAudit in _auditoriaTareas
                                       join tarea in _tareas on tareaAudit.at_tarea equals tarea.ct_codigo
                                       orderby tarea.ct_descripcion ascending
                                       select new { tareaAudit.at_tarea, tarea.ct_descripcion, tareaAudit.at_oficina, tareaAudit.at_asignacion, tareaAudit.at_estado };

            return JsonConvert.SerializeObject(listaAuditoriaTareas);
        }

        [System.Web.Services.WebMethod]
        public static string GrabarAuditoriaTarea(string parametros)
        {
            AuditoriaTareasController _controller = new AuditoriaTareasController();
            Models.AuditoriaTareas parametro = new Models.AuditoriaTareas();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.at_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.at_auditoria = int.Parse(arrayParametros[1].ToString());
            parametro.at_tarea = Int16.Parse(arrayParametros[2].ToString());
            parametro.at_oficina = Int16.Parse(arrayParametros[3].ToString());
            parametro.at_asignacion = arrayParametros[4].ToString().Trim().ToUpper();
            parametro.at_estado = arrayParametros[5].ToString().Trim().ToUpper();
            parametro.at_usuario_creacion = user_cookie.Usuario;
            parametro.at_fecha_creacion = DateTime.Now;
            parametro.at_usuario_actualizacion = user_cookie.Usuario;
            parametro.at_fecha_actualizacion = DateTime.Now;

            if (arrayParametros[6].ToString().Trim().ToUpper() == "0")
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

        protected void BtnAddProceso_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "CargaActividades();", true);
        }

        protected void BtnAddPlantilla_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "CargaPlantilla();", true);
        }

        protected void BtnCerrar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "CerrarProceso();", true);
        }

        protected void BtnAsignar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "CargaAsignaciones();", true);
        }
    }
}