using System.Collections.Generic;
using WebAuditorias.Interfaces.AuditoriaTareaProcesos;
using WebAuditorias.Services.AuditoriaTareaProcesos;

namespace WebAuditorias.Controllers.AuditoriaTareaProcesos
{
    public class AuditoriaTareaProcesosController : IAuditoriaTareaProcesosController
    {
        public string Actualizacion(Models.AuditoriaTareaProcesos auditoriaTareas)
        {
            AuditoriaTareaProcesosService _auditoriaTareaProcesosService = new AuditoriaTareaProcesosService();

            return _auditoriaTareaProcesosService.Actualizacion(auditoriaTareas);
        }

        public IEnumerable<Models.AuditoriaTareaProcesos> Consulta(int empresa, int auditoria, int tarea)
        {
            AuditoriaTareaProcesosService _auditoriaTareaProcesosService = new AuditoriaTareaProcesosService();

            return _auditoriaTareaProcesosService.Consulta(empresa, auditoria, tarea);
        }

        public string Ingreso(Models.AuditoriaTareaProcesos auditoriaTareas)
        {
            AuditoriaTareaProcesosService _auditoriaTareaProcesosService = new AuditoriaTareaProcesosService();

            return _auditoriaTareaProcesosService.Ingreso(auditoriaTareas);
        }
    }
}