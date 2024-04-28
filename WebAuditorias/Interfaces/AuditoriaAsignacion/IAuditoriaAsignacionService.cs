using System.Collections.Generic;

namespace WebAuditorias.Interfaces.AuditoriaAsignacion
{
    public interface IAuditoriaAsignacionService
    {
        string Ingreso(Models.AuditoriaAsignacion auditoriaAsignacion);

        string Actualizacion(Models.AuditoriaAsignacion auditoriaAsignacion);

        IEnumerable<Models.AuditoriaAsignacion> Consulta(int empresa, int auditoria, int tarea);
    }
}
