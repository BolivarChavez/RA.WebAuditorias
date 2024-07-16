using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentoProcesos;
using WebAuditorias.Controllers.AuditoriaDocumentos;
using WebAuditorias.Controllers.Auditorias;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class AuditoriaConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                CargaProcesosAuditoria();
                CargaPlantillasRegistradas();
            }
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        private void CargaProcesosAuditoria()
        {
            AuditoriasController _controller = new AuditoriasController();
            List<Models.Auditorias> _auditorias = new List<Models.Auditorias>();

            _auditorias = _controller.Consulta(1, 0).OrderByDescending(au => au.au_codigo).ToList();

            numPA.InnerHtml = _auditorias.Where(p => p.au_estado == "A").Count().ToString();
            numPC.InnerHtml = _auditorias.Where(p => p.au_estado == "C").Count().ToString();
            numPP.InnerHtml = _auditorias.Where(p => p.au_estado == "P").Count().ToString();
        }

        private string ConsultaPlantillas(int tipoPlantilla)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();

            AuditoriaDocumentoProcesosController _controllerDocumento = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            int plantillasIngresadas;
            int plantillasRevisadas;

            _auditoriaDocumentosProcesos = _controllerDocumento.Consulta(1, 0, 0, 0).Where(lt => lt.ad_estado != "X").ToList();

            _documentos = _controller.Consulta(1, 0, 0, tipoPlantilla).Where(x => x.ad_estado == "A").ToList();

            var chequesProcesados = from documento in _documentos
                                    join proceso in _auditoriaDocumentosProcesos
                                    on new { empresa = documento.ad_empresa, auditoria = documento.ad_auditoria, tarea = documento.ad_tarea, codigo = documento.ad_codigo }
                                    equals new { empresa = proceso.ad_empresa, auditoria = proceso.ad_auditoria, tarea = proceso.ad_tarea, codigo = proceso.ad_codigo }
                                    select new { proceso.ad_empresa, proceso.ad_auditoria, proceso.ad_tarea, proceso.ad_codigo };

            plantillasIngresadas = _documentos.Count();
            plantillasRevisadas = chequesProcesados.Distinct().Count();

            return plantillasIngresadas.ToString() + "|" + (plantillasIngresadas - plantillasRevisadas).ToString();
        }

        private void CargaPlantillasRegistradas()
        {
            string[] arrayRespuesta;

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Cheques).Split('|');

            p1t.InnerHtml = arrayRespuesta[0].Trim();
            p1r.InnerHtml = arrayRespuesta[1].Trim();

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Comisiones).Split('|');

            p2t.InnerHtml = arrayRespuesta[0].Trim();
            p2r.InnerHtml = arrayRespuesta[1].Trim();

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Ingresos).Split('|');

            p3t.InnerHtml = arrayRespuesta[0].Trim();
            p3r.InnerHtml = arrayRespuesta[1].Trim();

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Mutuos).Split('|');

            p4t.InnerHtml = arrayRespuesta[0].Trim();
            p4r.InnerHtml = arrayRespuesta[1].Trim();

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Pagos).Split('|');

            p5t.InnerHtml = arrayRespuesta[0].Trim();
            p5r.InnerHtml = arrayRespuesta[1].Trim();

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Planillas).Split('|');

            p6t.InnerHtml = arrayRespuesta[0].Trim();
            p6r.InnerHtml = arrayRespuesta[1].Trim();

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Reembolsos).Split('|');

            p7t.InnerHtml = arrayRespuesta[0].Trim();
            p7r.InnerHtml = arrayRespuesta[1].Trim();

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Regalias).Split('|');

            p8t.InnerHtml = arrayRespuesta[0].Trim();
            p8r.InnerHtml = arrayRespuesta[1].Trim();

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Regularizaciones).Split('|');

            p9t.InnerHtml = arrayRespuesta[0].Trim();
            p9r.InnerHtml = arrayRespuesta[1].Trim();

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Transferencias).Split('|');

            p10t.InnerHtml = arrayRespuesta[0].Trim();
            p10r.InnerHtml = arrayRespuesta[1].Trim();

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Tributos).Split('|');

            p11t.InnerHtml = arrayRespuesta[0].Trim();
            p11r.InnerHtml = arrayRespuesta[1].Trim();
        }
    }
}