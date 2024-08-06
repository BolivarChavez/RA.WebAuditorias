using System.Collections.Generic;
using WebAuditorias.Models;

namespace WebAuditorias.Interfaces.Auditorias
{
    public interface IAuditoriasService
    {
        string Ingreso(Models.Auditorias auditoria);

        string Actualizacion(Models.Auditorias auditoria);

        IEnumerable<Models.Auditorias> Consulta(int empresa, int codigo, int anio);

        IEnumerable<Models.Auditorias> ConsultaPlantilla(int empresa, int codigo, int anio, int plantilla);

        IEnumerable<AuditoriaResumen> ConsultaResumen(int empresa, int anio);

        string CopiaAuditoria(Models.Auditorias auditoria);
    }
}
