using System.Collections.Generic;

namespace WebAuditorias.Interfaces.Auditorias
{
    public interface IAuditoriasService
    {
        string Ingreso(Models.Auditorias auditoria);

        string Actualizacion(Models.Auditorias auditoria);

        IEnumerable<Models.Auditorias> Consulta(int empresa, int codigo);
    }
}
