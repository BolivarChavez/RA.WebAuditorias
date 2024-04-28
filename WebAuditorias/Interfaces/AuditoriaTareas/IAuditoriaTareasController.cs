using System.Collections.Generic;

namespace WebAuditorias.Interfaces.AuditoriaTareas
{
    public interface IAuditoriaTareasController
    {
        string Ingreso(Models.AuditoriaTareas auditoriaTareas);

        string Actualizacion(Models.AuditoriaTareas auditoriaTareas);

        IEnumerable<Models.AuditoriaTareas> Consulta(int empresa, int auditoria, int tarea);
    }
}
