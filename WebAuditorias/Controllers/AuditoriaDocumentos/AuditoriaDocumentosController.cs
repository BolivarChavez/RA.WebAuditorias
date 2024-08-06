using System.Collections.Generic;
using WebAuditorias.Interfaces.AuditoriaDocumentos;
using WebAuditorias.Services.AuditoriaDocumentos;

namespace WebAuditorias.Controllers.AuditoriaDocumentos
{
    public class AuditoriaDocumentosController : IAuditoriaDocumentosController
    {
        public string Actualizacion(Models.AuditoriaDocumentos auditoriaDocumento)
        {
            AuditoriaDocumentosService _auditoriaDocumentosService = new AuditoriaDocumentosService();

            return _auditoriaDocumentosService.Actualizacion(auditoriaDocumento);
        }

        public IEnumerable<Models.AuditoriaDocumentos> Consulta(int empresa, int auditoria, int tarea, int plantilla, int anio)
        {
            AuditoriaDocumentosService _auditoriaDocumentosService = new AuditoriaDocumentosService();

            return _auditoriaDocumentosService.Consulta(empresa, auditoria, tarea, plantilla, anio);
        }

        public string Ingreso(Models.AuditoriaDocumentos auditoriaDocumento)
        {
            AuditoriaDocumentosService _auditoriaDocumentosService = new AuditoriaDocumentosService();

            return _auditoriaDocumentosService.Ingreso(auditoriaDocumento);
        }
    }
}