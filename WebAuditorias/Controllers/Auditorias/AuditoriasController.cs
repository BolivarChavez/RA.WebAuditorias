using System.Collections.Generic;
using WebAuditorias.Interfaces.Auditorias;
using WebAuditorias.Services.Auditorias;

namespace WebAuditorias.Controllers.Auditorias
{
    public class AuditoriasController : IAuditoriasController
    {
        public string Actualizacion(Models.Auditorias auditoria)
        {
            AuditoriasService _auditoriasService = new AuditoriasService();

            return _auditoriasService.Actualizacion(auditoria);
        }

        public IEnumerable<Models.Auditorias> Consulta(int empresa, int codigo)
        {
            AuditoriasService _auditoriasService = new AuditoriasService();

            return _auditoriasService.Consulta(empresa, codigo);
        }

        public string Ingreso(Models.Auditorias auditoria)
        {
            AuditoriasService _auditoriasService = new AuditoriasService();

            return _auditoriasService.Ingreso(auditoria);
        }
    }
}