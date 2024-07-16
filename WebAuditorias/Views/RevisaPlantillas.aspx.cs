using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentoProcesos;
using WebAuditorias.Controllers.Auditorias;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Responsables;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class RevisaPlantillas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                CargaAuditorias();
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
            HiddenField2.Value = Request.QueryString["plantilla"].Trim();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillaProcesos(string parametros)
        {
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            AuditoriaDocumentoProcesosController _controller = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            ResponsablesController _controllerResponsable = new ResponsablesController();
            List<Models.Responsables> _responsables = new List<Models.Responsables>();

            _responsables = _controllerResponsable.Consulta(1).ToList();
            _auditoriaDocumentosProcesos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3])).Where(lt => lt.ad_estado != "X").ToList();

            var listaAuditoriaDocumentosProcesos = from proceso in _auditoriaDocumentosProcesos
                                                   join auditor in _responsables on proceso.ad_auditor equals auditor.re_codigo
                                                   join responsable in _responsables on proceso.ad_responsable equals responsable.re_codigo
                                                   let responsableNombre = responsable.re_nombre
                                                   orderby proceso.ad_secuencia ascending
                                                   select new
                                                   {
                                                       proceso.ad_secuencia,
                                                       proceso.ad_fecha,
                                                       proceso.ad_auditor,
                                                       auditor.re_nombre,
                                                       proceso.ad_responsable,
                                                       responsableNombre,
                                                       proceso.ad_observaciones,
                                                       proceso.ad_documento,
                                                       proceso.ad_estado
                                                   };

            return JsonConvert.SerializeObject(listaAuditoriaDocumentosProcesos);
        }

        private void CargaAuditorias()
        {
            AuditoriasController _controller = new AuditoriasController();
            List<Models.Auditorias> _auditorias = new List<Models.Auditorias>();
            string estado = Request.QueryString["estado"].Trim();

            _auditorias = _controller.Consulta(1, 0).Where(au => au.au_estado == estado).OrderByDescending(au => au.au_codigo).ToList();

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

            _auditorias = _controller.Consulta(1, codigoAuditoria).ToList();
            HiddenField1.Value = _auditorias.FirstOrDefault().au_tipo_proceso.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillaCheques(string parametros)
        {
            return "";
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillaComisiones(string parametros)
        {
            return "";
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillaIngresos(string parametros)
        {
            return "";
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillaMutuos(string parametros)
        {
            return "";
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillaPagos(string parametros)
        {
            return "";
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "ConsultaInicial();", true);
        }

        protected void ProcesoAuditoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsultaDatosAuditoria(int.Parse(ProcesoAuditoria.SelectedValue));
        }
    }
}