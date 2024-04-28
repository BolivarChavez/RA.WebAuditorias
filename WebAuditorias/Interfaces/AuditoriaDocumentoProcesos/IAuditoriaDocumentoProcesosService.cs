﻿using System.Collections.Generic;

namespace WebAuditorias.Interfaces.AuditoriaDocumentoProcesos
{
    public interface IAuditoriaDocumentoProcesosService
    {
        string Ingreso(Models.AuditoriaDocumentoProcesos auditoriaDocumento);

        string Actualizacion(Models.AuditoriaDocumentoProcesos auditoriaDocumento);

        IEnumerable<Models.AuditoriaDocumentoProcesos> Consulta(int empresa, int auditoria, int tarea, int codigo);
    }
}
