using System.Collections.Generic;
using System.Linq;
using WebAuditorias.Controllers.AuditoriaTareaProcesos;
using WebAuditorias.Controllers.AuditoriaTareas;
using WebAuditorias.Controllers.CatalogoProcesos;
using WebAuditorias.Controllers.CatalogoTareas;
using WebAuditorias.Controllers.Responsables;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;

namespace WebAuditorias.Controllers.Auditorias
{
    public class AuditoriaInformeController
    {
        public void GeneraBorradorInforme(int auditoriaId, string filePath)
        {
            try
            {
                AuditoriasController _controller = new AuditoriasController();
                List<Models.Auditorias> _auditorias = new List<Models.Auditorias>();

                CatalogoProcesosController _controllerProcesos = new CatalogoProcesosController();
                List<Models.CatalogoProcesos> _procesos = new List<Models.CatalogoProcesos>();

                AuditoriaTareasController _controllerAudTareas = new AuditoriaTareasController();
                List<Models.AuditoriaTareas> _auditoriaTareas = new List<Models.AuditoriaTareas>();

                CatalogoTareasController _controllerTareas = new CatalogoTareasController();
                List<Models.CatalogoTareas> _tareas = new List<Models.CatalogoTareas>();

                AuditoriaTareaProcesosController _controllerTareaProceso = new AuditoriaTareaProcesosController();
                List<Models.AuditoriaTareaProcesos> _auditoriaTareasProcesos = new List<Models.AuditoriaTareaProcesos>();

                ResponsablesController _controllerResponsable = new ResponsablesController();
                List<Models.Responsables> _responsables = new List<Models.Responsables>();

                string tituloSeccion = "";

                _responsables = _controllerResponsable.Consulta(1).ToList();

                _auditorias = _controller.Consulta(1, auditoriaId, 0).ToList();
                _procesos = _controllerProcesos.Consulta(1).Where(x => x.cp_codigo == _auditorias.FirstOrDefault().au_tipo_proceso).ToList();

                _tareas = _controllerTareas.Consulta(1).Where(ta => ta.ct_proceso == _auditorias.FirstOrDefault().au_tipo_proceso).ToList();
                _auditoriaTareas = _controllerAudTareas.Consulta(1, auditoriaId, 0).Where(lt => lt.at_estado != "X").ToList();

                var listaAuditoriaTareas = from tareaAudit in _auditoriaTareas
                                           join tarea in _tareas on tareaAudit.at_tarea equals tarea.ct_codigo
                                           orderby tarea.ct_descripcion ascending
                                           select new { tareaAudit.at_tarea, tarea.ct_descripcion, tareaAudit.at_oficina, tareaAudit.at_asignacion, tareaAudit.at_estado };

                if (listaAuditoriaTareas.Any())
                {
                    using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
                    {
                        MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                        mainPart.Document = new Document(new Body());
                        Body body = mainPart.Document.Body;

                        var heading = CreateParagraph(_auditorias.FirstOrDefault().au_observaciones, true, 16, JustificationValues.Center);
                        body.Append(heading);

                        foreach (var tareaActual in listaAuditoriaTareas)
                        {
                            var titulo = CreateParagraph(tareaActual.ct_descripcion + " - " + tareaActual.at_asignacion, true, 14, JustificationValues.Left);
                            body.Append(titulo);

                            _auditoriaTareasProcesos = _controllerTareaProceso.Consulta(1, auditoriaId, tareaActual.at_tarea).Where(lt => lt.at_estado != "X").ToList();

                            if (_auditoriaTareasProcesos.Any())
                            {
                                foreach (var tareaProceso in _auditoriaTareasProcesos)
                                {
                                    if (tareaProceso.at_estado.Trim() != "X")
                                    {
                                        tituloSeccion = "Fecha de registro.";
                                        var titulo1 = CreateParagraph(tituloSeccion, true, 12, JustificationValues.Left);
                                        body.Append(titulo1);

                                        var para1 = CreateParagraph(tareaProceso.at_fecha.ToString("dd/MM/yyyy"), false, 12, JustificationValues.Both);
                                        body.Append(para1);

                                        tituloSeccion = "Detalle de proceso.";
                                        var titulo2 = CreateParagraph(tituloSeccion, true, 12, JustificationValues.Left);
                                        body.Append(titulo2);

                                        var para2 = CreateParagraph(tareaProceso.at_observaciones, false, 12, JustificationValues.Both);
                                        body.Append(para2);

                                        tituloSeccion = "Responsable.";
                                        var titulo3 = CreateParagraph(tituloSeccion, true, 12, JustificationValues.Left);
                                        body.Append(titulo3);

                                        var responsableNombre = _responsables.Where(r => r.re_codigo == tareaProceso.at_responsable).FirstOrDefault().re_nombre;
                                        var para3 = CreateParagraph(responsableNombre, false, 12, JustificationValues.Both);
                                        body.Append(para3);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private Paragraph CreateParagraph(string text, bool bold, int fontSize, JustificationValues alignment)
        {
            RunProperties runProps = new RunProperties();
            runProps.Append(new FontSize() { Val = (fontSize * 2).ToString() }); // OpenXML uses half-points

            if (bold)
            {
                runProps.Append(new Bold());
            }

            Run run = new Run();
            run.Append(runProps);
            run.Append(new Text(text));

            Paragraph paragraph = new Paragraph();
            ParagraphProperties paraProps = new ParagraphProperties();
            paraProps.Justification = new Justification() { Val = alignment };
            paragraph.Append(paraProps);
            paragraph.Append(run);

            return paragraph;
        }
    }
}