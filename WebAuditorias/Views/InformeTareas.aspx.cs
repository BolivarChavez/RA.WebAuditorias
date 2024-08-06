using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.Auditorias;
using WebAuditorias.Controllers.AuditoriaTareaProcesos;
using WebAuditorias.Controllers.AuditoriaTareas;
using WebAuditorias.Controllers.CatalogoProcesos;
using WebAuditorias.Controllers.CatalogoTareas;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Controllers.Responsables;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class InformeTareas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaDatosUsuario();
                CargaInformacionAuditoria();
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

        private void CargaInformacionAuditoria()
        {
            AuditoriasController _controller = new AuditoriasController();
            List<Models.Auditorias> _auditorias = new List<Models.Auditorias>();

            CatalogoProcesosController _controllerProcesos = new CatalogoProcesosController();
            List<Models.CatalogoProcesos> _procesos = new List<Models.CatalogoProcesos>();

            string auditoria = Request.QueryString["auditoria"];

            _auditorias = _controller.Consulta(1, int.Parse(auditoria), 0).ToList();
            _procesos = _controllerProcesos.Consulta(1).Where(x => x.cp_codigo == _auditorias.FirstOrDefault().au_tipo_proceso).ToList();

            Auditoria.InnerText = _auditorias.FirstOrDefault().au_observaciones;
            Proceso.InnerText = _procesos.FirstOrDefault().cp_descripcion;
            HiddenField1.Value = _auditorias.FirstOrDefault().au_tipo_proceso.ToString();

            DetalleConsulta.InnerHtml = ConsultaTareasAuditoria();
        }

        private string ConsultaTareasAuditoria()
        {
            string result;
            AuditoriaTareasController _controller = new AuditoriaTareasController();
            List<Models.AuditoriaTareas> _auditoriaTareas = new List<Models.AuditoriaTareas>();

            CatalogoTareasController _controllerTareas = new CatalogoTareasController();
            List<Models.CatalogoTareas> _tareas = new List<Models.CatalogoTareas>();
            string auditoria = Request.QueryString["auditoria"];

            _tareas = _controllerTareas.Consulta(1).Where(ta => ta.ct_proceso == int.Parse(HiddenField1.Value)).ToList();
            _auditoriaTareas = _controller.Consulta(1, int.Parse(auditoria), 0).Where(lt => lt.at_estado != "X").ToList();

            var listaAuditoriaTareas = from tareaAudit in _auditoriaTareas
                                       join tarea in _tareas on tareaAudit.at_tarea equals tarea.ct_codigo
                                       orderby tarea.ct_descripcion ascending
                                       select new { tareaAudit.at_tarea, tarea.ct_descripcion, tareaAudit.at_oficina, tareaAudit.at_asignacion, tareaAudit.at_estado };

            result = "";

            foreach (var tarea in listaAuditoriaTareas)
            {
                result += @"<table class=""table table-borderless""><tbody>";
                result += @"<tr>";
                result += @"<td style=""width:15%""><h5>Tarea :</h5></td>";
                result += $"<td style=\"width:85%\"><h5>{tarea.ct_descripcion}</h5></td>";
                result += @"</tr>";
                result += @"<tr>";
                result += @"<td style=""width:15%""><h5>Asignacion :</h5></td>";
                result += $"<td style=\"width:85%\"><h5>{tarea.at_asignacion}</h5></td>";
                result += @"</tr>";
                result += @"<tr>";
                result += @"<td style=""width:15%""><h5>Estado :</h5></td>";
                result += $"<td style=\"width:85%\"><h5>{EstadoTarea(tarea.at_estado)}</h5></td>";
                result += @"</tr>";
                result += @"</tbody></table>";
                result += ConsultaTareasProcesos(int.Parse(auditoria), tarea.at_tarea);
            }

            return result;
        }

        private string EstadoTarea(string estado)
        {
            string result = "";

            switch (estado)
            {
                case "A":
                    result = "ABIERTO";
                    break;

                case "P":
                    result = "EN PROCESO";
                    break;

                case "C":
                    result = "CERRADO";
                    break;
            }

            return result;
        }

        private string ConsultaTareasProcesos(int auditoria, int tarea)
        {
            string result;
            AuditoriaTareaProcesosController _controller = new AuditoriaTareaProcesosController();
            List<Models.AuditoriaTareaProcesos> _auditoriaTareasProcesos = new List<Models.AuditoriaTareaProcesos>();

            ResponsablesController _controllerResponsable = new ResponsablesController();
            List<Models.Responsables> _responsables = new List<Models.Responsables>();

            _responsables = _controllerResponsable.Consulta(1).ToList();
            _auditoriaTareasProcesos = _controller.Consulta(1, auditoria, tarea).Where(lt => lt.at_estado != "X").ToList();

            var listaAuditoriaTareasProcesos = from proceso in _auditoriaTareasProcesos
                                               join auditor in _responsables on proceso.at_auditor equals auditor.re_codigo
                                               join responsable in _responsables on proceso.at_responsable equals responsable.re_codigo
                                               let responsableNombre = responsable.re_nombre
                                               orderby proceso.at_secuencia ascending
                                               select new
                                               {
                                                   proceso.at_secuencia,
                                                   proceso.at_fecha,
                                                   proceso.at_auditor,
                                                   auditor.re_nombre,
                                                   proceso.at_responsable,
                                                   responsableNombre,
                                                   proceso.at_observaciones,
                                                   proceso.at_documento,
                                                   proceso.at_estado
                                               };

            result = "";

            if (listaAuditoriaTareasProcesos.Count() > 0 )
            {
                result += @"<br />";
            }
                
            if (listaAuditoriaTareasProcesos.Where(x => x.at_estado == "I").Count() > 0 )
            {
                result += @"<h5>Actividades Pendientes</h5>";

                foreach (var actividad in listaAuditoriaTareasProcesos.Where(x => x.at_estado == "I"))
                {
                    result += @"<div class=""d -flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-1 border-bottom""></div>";
                    result += @"<table class=""table table-borderless""><tbody>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><h6>Fecha de compromiso :</h6></td>";
                    result += $"<td style=\"width:85%\"><h6>{actividad.at_fecha.ToString("dd/MM/yyyy")}</h6></td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><h6>Auditor :</h6></td>";
                    result += $"<td style=\"width:85%\"><h6>{actividad.re_nombre}</h6></td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><h6>Responsable :</h6></td>";
                    result += $"<td style=\"width:85%\"><h6>{actividad.responsableNombre}</h6></td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><h6>Descripción de actividad :</h6></td>";
                    result += $"<td style=\"width:85%\"><h6>{actividad.at_observaciones}</h6></td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><h6>Documento de soporte :</h6></td>";

                    if (actividad.at_documento == null)
                        result += $"<td style=\"width:85%\"><h6></h6></td>";
                    else
                    {
                        if (actividad.at_documento.Trim().Length > 1)
                            result += $"<td style=\"width:85%\"><a href=\"VistaArchivo.aspx?archivo={actividad.at_documento}\" target=\"_blank\"><h6>{actividad.at_documento}</h6></a></td>";
                        else
                            result += $"<td style=\"width:85%\"><h6></h6></td>";
                    }

                    result += @"</tr>";
                    result += @"</tbody></table>";
                }
            }

            if (listaAuditoriaTareasProcesos.Where(x => x.at_estado == "A").Count() > 0)
            {
                result += @"<h5>Actividades Terminadas</h5>";

                foreach (var actividad in listaAuditoriaTareasProcesos.Where(x => x.at_estado == "A"))
                {
                    result += @"<div class=""d -flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-1 border-bottom""></div>";
                    result += @"<table class=""table table-borderless""><tbody>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><h6>Fecha de compromiso :</h6></td>";
                    result += $"<td style=\"width:85%\"><h6>{actividad.at_fecha.ToString("dd/MM/yyyy")}</h6></td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><h6>Auditor :</h6></td>";
                    result += $"<td style=\"width:85%\"><h6>{actividad.re_nombre}</h6></td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><h6>Responsable :</h6></td>";
                    result += $"<td style=\"width:85%\"><h6>{actividad.responsableNombre}</h6></td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><h6>Descripción de actividad :</h6></td>";
                    result += $"<td style=\"width:85%\"><h6>{actividad.at_observaciones}</h6></td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><h6>Documento de soporte :</h6></td>";

                    if (actividad.at_documento == null)
                        result += $"<td style=\"width:85%\"><h6></h6></td>";
                    else
                    {
                        if (actividad.at_documento.Trim().Length > 1)
                            result += $"<td style=\"width:85%\"><a href=\"VistaArchivo.aspx?archivo={actividad.at_documento}\" target=\"_blank\"><h6>{actividad.at_documento}</h6></a></td>";
                        else
                            result += $"<td style=\"width:85%\"><h6></h6></td>";
                    }

                    result += @"</tr>";
                    result += @"</tbody></table>";
                }
            }

            return result;
        }

        private string EstadoActividad(string estado)
        {
            string result = "";

            switch (estado)
            {
                case "A":
                    result = "TERMINADO";
                    break;

                case "I":
                    result = "PENDIENTE";
                    break;
            }

            return result;
        }
    }
}