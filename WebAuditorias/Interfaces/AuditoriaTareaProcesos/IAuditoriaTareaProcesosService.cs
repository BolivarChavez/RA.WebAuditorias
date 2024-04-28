using System.Collections.Generic;

namespace WebAuditorias.Interfaces.AuditoriaTareaProcesos
{
    public interface IAuditoriaTareaProcesosService
    {
        string Ingreso(Models.AuditoriaTareaProcesos auditoriaTareas);

        string Actualizacion(Models.AuditoriaTareaProcesos auditoriaTareas);

        IEnumerable<Models.AuditoriaTareaProcesos> Consulta(int empresa, int auditoria, int tarea);
    }
}
