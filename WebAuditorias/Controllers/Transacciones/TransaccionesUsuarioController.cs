using System.Collections.Generic;
using System.Linq;
using WebAuditorias.Interfaces.Transacciones;
using WebAuditorias.Models;
using WebAuditorias.Services.Operador;

namespace WebAuditorias.Controllers.Transacciones
{
    public class TransaccionesUsuarioController : ITransaccionesUsuarioController
    {
        public string CreaOpcionesUsuario(int usuario)
        {
            string contenido = string.Empty;
            string linea = string.Empty;
            string linea_rep1 = string.Empty;
            string linea_rep2 = string.Empty;
            OperadorService _operadorService = new OperadorService();
            List<TransaccionesUsuario> listaTransacciones;

            var opciones = _operadorService.ConsultaGrupoOpciones(usuario);

            if (opciones.Count() > 0)
            {
                contenido += @"<div class=""contenerdor-menu"">";
                contenido += @"<table>";
                contenido += @"<thead>";
                contenido += @"<tr>";
                contenido += @"<th style=""width:10%;"">Opcion</th>";
                contenido += @"<th style=""width:90%;"" colspan=""2"">Descripcion</th>";
                contenido += @"</tr>";
                contenido += @"</thead>";
                contenido += @"<tbody>";

                using (var sequenceEnum = opciones.GetEnumerator())
                {
                    while (sequenceEnum.MoveNext())
                    {
                        contenido += @"<tbody class=""labels"">";
                        contenido += @"<tr>";
                        contenido += @"<td colspan=""3"">";
                        linea = @"<label for=""codigo"">descripcion</label>";
                        linea_rep1 = linea.Replace("codigo", "grp-" + sequenceEnum.Current.go_codigo.ToString());
                        linea_rep2 = linea_rep1.Replace("descripcion", sequenceEnum.Current.go_descripcion.Trim());
                        contenido += linea_rep2;
                        linea = @"<input type=""checkbox"" name=""codigo"" id=""codigo"" data-toggle=""toggle"">";
                        linea_rep1 = linea.Replace("codigo", "grp-" + sequenceEnum.Current.go_codigo.ToString());
                        contenido += linea_rep1;
                        contenido += @"</td>";
                        contenido += @"</tr>";
                        contenido += @"</tbody>";

                        listaTransacciones = _operadorService.ConsultaTransacciones(usuario, sequenceEnum.Current.go_codigo).ToList();

                        foreach (var transaccion in listaTransacciones)
                        {
                            contenido += @"<tbody class=""hide"">";
                            contenido += @"<tr onmouseover=""setBackground(this, '#E5E8E8');"" onmouseout=""restoreBackground(this);"">";
                            linea = @"<td id=""codigo"" style=""width:10%; cursor:pointer; font-family:Open Sans; font-weight:normal;"" onClick=""EjecutaOpcion(this.id)"">" + transaccion.tr_codigo.ToString() + "</td>";
                            linea_rep1 = linea.Replace("codigo", transaccion.tr_codigo.ToString());
                            contenido += linea_rep1;
                            linea = @"<td id=""codigo"" style=""width:80%; cursor:pointer; font-family:Open Sans; font-weight:normal;"" onClick=""EjecutaOpcion(this.id)"">" + transaccion.tr_descripcion.Trim() + "</td>";
                            linea_rep1 = linea.Replace("codigo", "opt-" + transaccion.tr_codigo.ToString());
                            contenido += linea_rep1;
                            linea = @"<td id=""codigo"" style=""width:10%; display:none; font-family:Open Sans; font-weight:normal;"">" + transaccion.tr_programa.Trim() + "</td>";
                            linea_rep1 = linea.Replace("codigo", "pag-" + transaccion.tr_codigo.ToString());
                            contenido += linea_rep1;
                            contenido += @"</tr>";
                            contenido += @"</tbody>";
                        }
                    }
                }

                contenido += @"</tbody>";
                contenido += @"</table>";
                contenido += @"</div>";
            }

            return contenido;
        }
    }
}