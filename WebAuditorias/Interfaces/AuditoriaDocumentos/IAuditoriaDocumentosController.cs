using System.Collections.Generic;

namespace WebAuditorias.Interfaces.AuditoriaDocumentos
{
    public interface IAuditoriaDocumentosController
    {
        string Ingreso(Models.AuditoriaDocumentos auditoriaDocumento);

        string Actualizacion(Models.AuditoriaDocumentos auditoriaDocumento);

        IEnumerable<Models.AuditoriaDocumentos> Consulta(int empresa, int auditoria, int tarea, int plantilla);
    }
}
