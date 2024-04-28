using System.Collections.Generic;
using WebAuditorias.Interfaces.AuditoriaDocumentoProcesos;
using WebAuditorias.Services.AuditoriaDocumentoProcesos;

namespace WebAuditorias.Controllers.AuditoriaDocumentoProcesos
{
    public class AuditoriaDocumentoProcesosController : IAuditoriaDocumentoProcesosController
    {
        public string Actualizacion(Models.AuditoriaDocumentoProcesos auditoriaDocumento)
        {
            AuditoriaDocumentoProcesosService _auditoriaDocumentoProcesosService = new AuditoriaDocumentoProcesosService();

            return _auditoriaDocumentoProcesosService.Actualizacion(auditoriaDocumento);
        }

        public IEnumerable<Models.AuditoriaDocumentoProcesos> Consulta(int empresa, int auditoria, int tarea, int codigo)
        {
            AuditoriaDocumentoProcesosService _auditoriaDocumentoProcesosService = new AuditoriaDocumentoProcesosService();

            return _auditoriaDocumentoProcesosService.Consulta(empresa, auditoria, tarea, codigo);
        }

        public string Ingreso(Models.AuditoriaDocumentoProcesos auditoriaDocumento)
        {
            AuditoriaDocumentoProcesosService _auditoriaDocumentoProcesosService = new AuditoriaDocumentoProcesosService();

            return _auditoriaDocumentoProcesosService.Ingreso(auditoriaDocumento);
        }
    }
}