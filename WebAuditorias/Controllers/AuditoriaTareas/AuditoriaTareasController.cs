using System.Collections.Generic;
using WebAuditorias.Interfaces.AuditoriaTareas;
using WebAuditorias.Services.AuditoriaTareas;

namespace WebAuditorias.Controllers.AuditoriaTareas
{
    public class AuditoriaTareasController : IAuditoriaTareasController
    {
        public string Actualizacion(Models.AuditoriaTareas auditoriaTareas)
        {
            AuditoriaTareasService _auditoriaTareasService = new AuditoriaTareasService();

            return _auditoriaTareasService.Actualizacion(auditoriaTareas);
        }

        public IEnumerable<Models.AuditoriaTareas> Consulta(int empresa, int auditoria, int tarea)
        {
            AuditoriaTareasService _auditoriaTareasService = new AuditoriaTareasService();

            return _auditoriaTareasService.Consulta(empresa, auditoria, tarea);
        }

        public string Ingreso(Models.AuditoriaTareas auditoriaTareas)
        {
            AuditoriaTareasService _auditoriaTareasService = new AuditoriaTareasService();

            return _auditoriaTareasService.Ingreso(auditoriaTareas);
        }
    }
}