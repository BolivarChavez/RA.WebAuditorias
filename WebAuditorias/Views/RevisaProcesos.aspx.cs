using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaGastos;
using WebAuditorias.Controllers.Auditorias;
using WebAuditorias.Controllers.CatalogoGastos;
using WebAuditorias.Controllers.CatalogoProcesos;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Oficinas;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class RevisaProcesos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                CargaAuditorias();
                CargaOficinas();
                CargaProcesos();
                ConsultaDatosAuditoria(int.Parse(ProcesoAuditoria.SelectedValue));
            }
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();

            switch (Request.QueryString["estado"].Trim())
            {
                case "A":
                    TituloOpcion.InnerHtml = "Revisión de Procesos de Auditoría <b>Abiertos</b>";
                    break;

                case "C":
                    TituloOpcion.InnerHtml = "Revisión de Procesos de Auditoría <b>Cerrados</b>";
                    break;

                case "P":
                    TituloOpcion.InnerHtml = "Revisión de Procesos de Auditoría <b>En Proceso</b>";
                    break;
            }
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

        private void CargaAuditorias()
        {
            AuditoriasController _controller = new AuditoriasController();
            List<Models.Auditorias> _auditorias = new List<Models.Auditorias>();
            string estado = Request.QueryString["estado"].Trim();
            string anioConsulta = Request.Cookies["AnioConsulta"].Value;

            _auditorias = _controller.Consulta(1, 0, int.Parse(anioConsulta)).Where(au => au.au_estado == estado).OrderByDescending(au => au.au_codigo).ToList();

            ProcesoAuditoria.DataSource = _auditorias;
            ProcesoAuditoria.DataValueField = "au_codigo";
            ProcesoAuditoria.DataTextField = "au_observaciones";
            ProcesoAuditoria.DataBind();

            ProcesoAuditoria.SelectedIndex = 0;
        }

        private void ConsultaDatosAuditoria(int codigoAuditoria)
        {
            AuditoriasController _controller = new AuditoriasController();
            List<Models.Auditorias> _auditorias = new List<Models.Auditorias>();

            _auditorias = _controller.Consulta(1, codigoAuditoria, 0).ToList();
            HiddenField1.Value = _auditorias.FirstOrDefault().au_tipo_proceso.ToString();

            Codigo.Value = _auditorias.FirstOrDefault().au_codigo.ToString();
            TipoProceso.SelectedValue = _auditorias.FirstOrDefault().au_tipo_proceso.ToString();
            OficinaOrigen.SelectedValue = _auditorias.FirstOrDefault().au_oficina_origen.ToString();
            OficinaDestino.SelectedValue = _auditorias.FirstOrDefault().au_oficina_destino.ToString();
            FechaInicio.Value = _auditorias.FirstOrDefault().au_fecha_inicio.ToString("yyyy-MM-dd");
            FechaCierre.Value = _auditorias.FirstOrDefault().au_fecha_cierre.ToString("yyyy-MM-dd");
            Tipo.SelectedValue = _auditorias.FirstOrDefault().au_tipo.ToString();
            Observaciones.Value = _auditorias.FirstOrDefault().au_observaciones.ToString();
            Estado.SelectedValue = _auditorias.FirstOrDefault().au_estado.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaGastosAuditoria(string parametros)
        {
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaGastosController _controller = new AuditoriaGastosController();
            List<Models.AuditoriaGastos> _auditoriaGastos = new List<Models.AuditoriaGastos>();

            CatalogoGastosController _controllerGastos = new CatalogoGastosController();
            List<Models.CatalogoGastos> _gastos = new List<Models.CatalogoGastos>();

            _gastos = _controllerGastos.Consulta(1).ToList();
            _auditoriaGastos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1])).Where(ga => ga.ag_estado == "A").ToList();

            var listaAuditoriaGastos = from gastoAudit in _auditoriaGastos
                                       join gasto in _gastos on gastoAudit.ag_tipo equals gasto.cg_codigo
                                       orderby gasto.cg_descripion ascending
                                       select new { gastoAudit.ag_secuencia, gastoAudit.ag_tipo, gasto.cg_descripion, gastoAudit.ag_fecha_inicio, gastoAudit.ag_fecha_fin, gastoAudit.ag_valor, gastoAudit.ag_estado };

            return JsonConvert.SerializeObject(listaAuditoriaGastos);
        }

        [System.Web.Services.WebMethod]
        public static string CerrarAuditoria(string parametros)
        {
            AuditoriasController _controller = new AuditoriasController();
            Models.Auditorias parametro = new Models.Auditorias();
            string response;
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            parametro.au_empresa = Int16.Parse(arrayParametros[0].ToString());
            parametro.au_codigo = int.Parse(arrayParametros[1].ToString());
            parametro.au_oficina_origen = 0;
            parametro.au_oficina_destino = 0;
            parametro.au_tipo_proceso = 0;
            parametro.au_fecha_inicio = DateTime.Now;
            parametro.au_fecha_cierre = DateTime.Now;
            parametro.au_tipo = "";
            parametro.au_observaciones = "";
            parametro.au_estado = "C";
            parametro.au_usuario_creacion = user_cookie.Usuario;
            parametro.au_fecha_creacion = DateTime.Now;
            parametro.au_usuario_actualizacion = user_cookie.Usuario;
            parametro.au_fecha_actualizacion = DateTime.Now;

            response = _controller.Actualizacion(parametro);

            return response;
        }

        protected void ProcesoAuditoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsultaDatosAuditoria(int.Parse(ProcesoAuditoria.SelectedValue));
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "ConsultaInicial();", true);
        }

        protected void BtnCerrar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "CerrarAuditoria();", true);
        }

        protected void BtnInforme_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "window.open('InformeTareas.aspx?auditoria=" + ProcesoAuditoria.SelectedValue + "', '_blank');", true);
        }
    }
}