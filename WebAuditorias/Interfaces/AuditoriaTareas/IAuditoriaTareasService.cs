using System.Collections.Generic;

namespace WebAuditorias.Interfaces.AuditoriaTareas
{
    public interface IAuditoriaTareasService
    {
        string Ingreso(Models.AuditoriaTareas auditoriaTareas);

        string Actualizacion(Models.AuditoriaTareas auditoriaTareas);

        IEnumerable<Models.AuditoriaTareas> Consulta(int empresa, int auditoria, int tarea);
    }
}
