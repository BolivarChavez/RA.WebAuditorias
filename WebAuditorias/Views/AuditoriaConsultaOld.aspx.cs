using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentoProcesos;
using WebAuditorias.Controllers.AuditoriaDocumentos;
using WebAuditorias.Controllers.Auditorias;
using WebAuditorias.Controllers.CatalogoProcesos;
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
                CargaProcesos();
                DetalleInfo.Visible = false;
            }
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
            Anio.Value = DateTime.Now.Year.ToString();
        }

        private void CargaProcesos()
        {
            CatalogoProcesosController _controller = new CatalogoProcesosController();
            List<Models.CatalogoProcesos> _procesos = new List<Models.CatalogoProcesos>();

            _procesos = _controller.Consulta(1).ToList();
            _procesos.Add(new Models.CatalogoProcesos { cp_codigo = 0, cp_descripcion = "Seleccionar Todos" });

            Proceso.DataSource = _procesos.OrderBy(x => x.cp_codigo);
            Proceso.DataValueField = "cp_codigo";
            Proceso.DataTextField = "cp_descripcion";
            Proceso.DataBind();
            Proceso.SelectedIndex = 0;
        }

        private void CargaProcesosAuditoria()
        {
            AuditoriasController _controller = new AuditoriasController();
            List<Models.Auditorias> _auditorias = new List<Models.Auditorias>();

            if (Proceso.SelectedValue.Trim() == "0")
                _auditorias = _controller.Consulta(1, 0, int.Parse(Anio.Value)).OrderByDescending(au => au.au_codigo).ToList();
            else
                _auditorias = _controller.Consulta(1, 0, int.Parse(Anio.Value)).Where(au => au.au_tipo_proceso == int.Parse(Proceso.SelectedValue.Trim())).OrderByDescending(au => au.au_codigo).ToList();

            numPA.InnerHtml = _auditorias.Where(p => p.au_estado == "A").Count().ToString();
            numPC.InnerHtml = _auditorias.Where(p => p.au_estado == "C").Count().ToString();
            numPP.InnerHtml = _auditorias.Where(p => p.au_estado == "P").Count().ToString();

            if (HttpContext.Current.Request.Cookies["AnioConsulta"] != null)
            {
                HttpCookie myCookie = new HttpCookie("AnioConsulta");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }

            HttpCookie anioConsulta = new HttpCookie("AnioConsulta");
            anioConsulta.Value = Anio.Value;
            HttpContext.Current.Response.Cookies.Add(anioConsulta);
        }

        private static string ConsultaPlantillas(int tipoPlantilla, int anio)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();

            AuditoriaDocumentoProcesosController _controllerDocumento = new AuditoriaDocumentoProcesosController();
            List<Models.AuditoriaDocumentoProcesos> _auditoriaDocumentosProcesos = new List<Models.AuditoriaDocumentoProcesos>();

            int plantillasIngresadas;
            int plantillasRevisadas;

            _auditoriaDocumentosProcesos = _controllerDocumento.Consulta(1, 0, 0, 0, anio).Where(lt => lt.ad_estado != "X").ToList();

            _documentos = _controller.Consulta(1, 0, 0, tipoPlantilla, anio).Where(x => x.ad_estado == "A").ToList();

            var chequesProcesados = from documento in _documentos
                                    join proceso in _auditoriaDocumentosProcesos
                                    on new { empresa = documento.ad_empresa, auditoria = documento.ad_auditoria, tarea = documento.ad_tarea, codigo = documento.ad_codigo }
                                    equals new { empresa = proceso.ad_empresa, auditoria = proceso.ad_auditoria, tarea = proceso.ad_tarea, codigo = proceso.ad_codigo }
                                    select new { proceso.ad_empresa, proceso.ad_auditoria, proceso.ad_tarea, proceso.ad_codigo };

            plantillasIngresadas = _documentos.Count();
            plantillasRevisadas = chequesProcesados.Distinct().Count();

            return plantillasIngresadas.ToString() + "|" + (plantillasIngresadas - plantillasRevisadas).ToString();
        }

        [System.Web.Services.WebMethod]
        public static string CargaPlantillasRegistradas(string anio)
        {
            string[] arrayRespuesta;
            List<ResumenPlantillas> listaPlantillas = new List<ResumenPlantillas>();

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Cheques, int.Parse(anio)).Split('|');
            listaPlantillas.Add(new ResumenPlantillas
            {
                IdPlantilla = (int)TipoPlantilla.Plantillas.Plantilla_Cheques,
                NombrePlantilla = "Plantilla de Cheques",
                Registradas = arrayRespuesta[0].Trim(),
                Pendientes = arrayRespuesta[1].Trim()
            });

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Comisiones, int.Parse(anio)).Split('|');
            listaPlantillas.Add(new ResumenPlantillas
            {
                IdPlantilla = (int)TipoPlantilla.Plantillas.Plantilla_Comisiones,
                NombrePlantilla = "Plantilla de Comisiones",
                Registradas = arrayRespuesta[0].Trim(),
                Pendientes = arrayRespuesta[1].Trim()
            });

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Ingresos, int.Parse(anio)).Split('|');
            listaPlantillas.Add(new ResumenPlantillas
            {
                IdPlantilla = (int)TipoPlantilla.Plantillas.Plantilla_Ingresos,
                NombrePlantilla = "Plantilla de Ingresos",
                Registradas = arrayRespuesta[0].Trim(),
                Pendientes = arrayRespuesta[1].Trim()
            });

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Mutuos, int.Parse(anio)).Split('|');
            listaPlantillas.Add(new ResumenPlantillas
            {
                IdPlantilla = (int)TipoPlantilla.Plantillas.Plantilla_Mutuos,
                NombrePlantilla = "Plantilla de Mutuos",
                Registradas = arrayRespuesta[0].Trim(),
                Pendientes = arrayRespuesta[1].Trim()
            });

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Pagos, int.Parse(anio)).Split('|');
            listaPlantillas.Add(new ResumenPlantillas
            {
                IdPlantilla = (int)TipoPlantilla.Plantillas.Plantilla_Pagos,
                NombrePlantilla = "Plantilla de Pagos",
                Registradas = arrayRespuesta[0].Trim(),
                Pendientes = arrayRespuesta[1].Trim()
            });

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Planillas, int.Parse(anio)).Split('|');
            listaPlantillas.Add(new ResumenPlantillas
            {
                IdPlantilla = (int)TipoPlantilla.Plantillas.Plantilla_Planillas,
                NombrePlantilla = "Plantilla de Planillas",
                Registradas = arrayRespuesta[0].Trim(),
                Pendientes = arrayRespuesta[1].Trim()
            });

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Reembolsos, int.Parse(anio)).Split('|');
            listaPlantillas.Add(new ResumenPlantillas
            {
                IdPlantilla = (int)TipoPlantilla.Plantillas.Plantilla_Reembolsos,
                NombrePlantilla = "Plantilla de Reembolsos",
                Registradas = arrayRespuesta[0].Trim(),
                Pendientes = arrayRespuesta[1].Trim()
            });

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Regalias, int.Parse(anio)).Split('|');
            listaPlantillas.Add(new ResumenPlantillas
            {
                IdPlantilla = (int)TipoPlantilla.Plantillas.Plantilla_Regalias,
                NombrePlantilla = "Plantilla de Regalias",
                Registradas = arrayRespuesta[0].Trim(),
                Pendientes = arrayRespuesta[1].Trim()
            });

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Regularizaciones, int.Parse(anio)).Split('|');
            listaPlantillas.Add(new ResumenPlantillas
            {
                IdPlantilla = (int)TipoPlantilla.Plantillas.Plantilla_Regularizaciones,
                NombrePlantilla = "Plantilla de Regularizaciones",
                Registradas = arrayRespuesta[0].Trim(),
                Pendientes = arrayRespuesta[1].Trim()
            });

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Transferencias, int.Parse(anio)).Split('|');
            listaPlantillas.Add(new ResumenPlantillas
            {
                IdPlantilla = (int)TipoPlantilla.Plantillas.Plantilla_Transferencias,
                NombrePlantilla = "Plantilla de Transferencias",
                Registradas = arrayRespuesta[0].Trim(),
                Pendientes = arrayRespuesta[1].Trim()
            });

            arrayRespuesta = ConsultaPlantillas((int)TipoPlantilla.Plantillas.Plantilla_Tributos, int.Parse(anio)).Split('|');
            listaPlantillas.Add(new ResumenPlantillas
            {
                IdPlantilla = (int)TipoPlantilla.Plantillas.Plantilla_Tributos,
                NombrePlantilla = "Plantilla de Tributos",
                Registradas = arrayRespuesta[0].Trim(),
                Pendientes = arrayRespuesta[1].Trim()
            });

            return JsonConvert.SerializeObject(listaPlantillas);
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            DetalleInfo.Visible = false;

            CargaProcesosAuditoria();
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "LlenaGridPlantilla();", true);

            DetalleInfo.Visible = true;
        }
    }
}