using System;
using System.Collections.Generic;
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

            if (user_cookie.Usuario == null || user_cookie.Usuario.Trim() == "")
            {
                Response.Redirect("ErrorAccesoOpcion.aspx", true);
            }

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
            string handle;
            int contadorTarea;
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
            handle = "";
            contadorTarea = 1;

            foreach (var tarea in listaAuditoriaTareas)
            {
                handle = "handle" + contadorTarea.ToString();

                result += @"<section class=""accordion"">";
                result += $"<input type = \"checkbox\" name = \"collapse\" id = \"{handle}\" />";
                result += @"<h2 class=""handle"">";
                result += $"<label for=\"{handle}\">Tarea : <b>{tarea.ct_descripcion}</b></label>";
                result += @"</h2>";
                result += @"<div class=""content"">";
                result += @"<div class=""card text-left border-primary rounded-2 shadow"" style=""width: 100%;"">";
                result += @"<div class=""card-header text-primary"">";
                result += $"Detalle de Tarea : <b>{tarea.ct_descripcion}</b>";
                result += @"</div>";
                result += @"<div class=""card-body"">";
                result += @"<table class=""table table-borderless w-auto""><tbody>";
                result += @"<tr>";
                result += @"<td><b>Asignacion :</b></td>";
                result += $"<td>{tarea.at_asignacion}</td>";
                result += @"</tr>";
                result += @"<tr>";
                result += @"<td><b>Estado :</b></td>";
                result += $"<td>{EstadoTarea(tarea.at_estado)}</td>";
                result += @"</tr>";
                result += @"</tbody></table>";
                result += ConsultaTareasProcesos(int.Parse(auditoria), tarea.at_tarea);
                result += @"</div>";
                result += @"</div>";
                result += @"</div>";
                result += @"</section>";

                contadorTarea++;
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
                result += @"<section class=""accordion"">";
                result += @"<input type = ""checkbox"" name = ""collapse"" id = ""handleact1"" />";
                result += @"<h2 class=""handle"">";
                result += @"<label for=""handleact1""><b>Actividades Pendientes</b></label>";
                result += @"</h2>";
                result += @"<div class=""content"">";

                result += @"<div class=""card text-left border-warning rounded-2 shadow"" style=""width: 100%;"">";
                result += @"<div class=""card-header text-warning"">";
                result += $"<b>Detalle de Actividades Pendientes</b>";
                result += @"</div>";
                result += @"<div class=""card-body"">";
                result += @"<table class=""table tabel-responsive table-borderless w-auto"" style=""width:100%; font-size:14px""><tbody>";

                foreach (var actividad in listaAuditoriaTareasProcesos.Where(x => x.at_estado == "I"))
                {
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><b>Fecha de compromiso :</b></td>";
                    result += $"<td style=\"width:85%\">{actividad.at_fecha.ToString("dd/MM/yyyy")}</td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><b>Auditor :</b></td>";
                    result += $"<td style=\"width:85%\">{actividad.re_nombre}</td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><b>Responsable :</b></td>";
                    result += $"<td style=\"width:85%\">{actividad.responsableNombre}</td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><b>Descripción de actividad :</b></td>";
                    result += $"<td style=\"width:85%\">{actividad.at_observaciones}</td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><b>Documento de soporte :</b></td>";

                    if (actividad.at_documento == null)
                        result += $"<td style=\"width:85%\"></td>";
                    else
                    {
                        if (actividad.at_documento.Trim().Length > 1)
                            result += $"<td style=\"width:85%\"><a href=\"VistaArchivo.aspx?archivo={actividad.at_documento}\" target=\"_blank\">{actividad.at_documento}</a></td>";
                        else
                            result += $"<td style=\"width:85%\"></td>";
                    }

                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td></td>";
                    result += @"<td><hr/></td>";
                    result += @"</tr>";
                }

                result += @"</tbody></table>";
                result += @"</div>";
                result += @"</div>";

                result += @"</div>";
                result += @"</section>";
                result += @"<br />";
            }

            if (listaAuditoriaTareasProcesos.Where(x => x.at_estado == "A").Count() > 0)
            {
                result += @"<section class=""accordion"">";
                result += @"<input type = ""checkbox"" name = ""collapse"" id = ""handleact2"" />";
                result += @"<h2 class=""handle"">";
                result += @"<label for=""handleact2""><b>Actividades Terminadas</b></label>";
                result += @"</h2>";
                result += @"<div class=""content"">";

                result += @"<div class=""card text-left border-success rounded-2 shadow"" style=""width: 100%;"">";
                result += @"<div class=""card-header text-success"">";
                result += $"<b>Detalle de Actividades Terminadas</b>";
                result += @"</div>";
                result += @"<div class=""card-body"">";
                result += @"<table class=""table tabel-responsive table-borderless w-auto"" style=""width:100%; font-size:14px""><tbody>";

                foreach (var actividad in listaAuditoriaTareasProcesos.Where(x => x.at_estado == "A"))
                {
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><b>Fecha de compromiso :</b></td>";
                    result += $"<td style=\"width:85%\">{actividad.at_fecha.ToString("dd/MM/yyyy")}</td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><b>Auditor :</b></td>";
                    result += $"<td style=\"width:85%\">{actividad.re_nombre}</td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><b>Responsable :</b></td>";
                    result += $"<td style=\"width:85%\">{actividad.responsableNombre}</td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><b>Descripción de actividad :</b></td>";
                    result += $"<td style=\"width:85%\">{actividad.at_observaciones}</td>";
                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td style=""width:15%""><b>Documento de soporte :</b></td>";

                    if (actividad.at_documento == null)
                        result += $"<td style=\"width:85%\"></td>";
                    else
                    {
                        if (actividad.at_documento.Trim().Length > 1)
                            result += $"<td style=\"width:85%\"><a href=\"VistaArchivo.aspx?archivo={actividad.at_documento}\" target=\"_blank\">{actividad.at_documento}</a></td>";
                        else
                            result += $"<td style=\"width:85%\"></td>";
                    }

                    result += @"</tr>";
                    result += @"<tr>";
                    result += @"<td></td>";
                    result += @"<td><hr/></td>";
                    result += @"</tr>";
                }

                result += @"</tbody></table>";
                result += @"</div>";
                result += @"</div>";

                result += @"</div>";
                result += @"</section>";
                result += @"<br />";
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