using System.Collections.Generic;
using WebAuditorias.Interfaces.Auditorias;
using WebAuditorias.Models;
using WebAuditorias.Services.Auditorias;
using static WebAuditorias.Models.TipoPlantilla;

namespace WebAuditorias.Controllers.Auditorias
{
    public class AuditoriasController : IAuditoriasController
    {
        public string Actualizacion(Models.Auditorias auditoria)
        {
            AuditoriasService _auditoriasService = new AuditoriasService();

            return _auditoriasService.Actualizacion(auditoria);
        }

        public IEnumerable<Models.Auditorias> Consulta(int empresa, int codigo, int anio)
        {
            AuditoriasService _auditoriasService = new AuditoriasService();

            return _auditoriasService.Consulta(empresa, codigo, anio);
        }

        public IEnumerable<Models.Auditorias> ConsultaPlantilla(int empresa, int codigo, int anio, int plantilla)
        {
            AuditoriasService _auditoriasService = new AuditoriasService();

            return _auditoriasService.ConsultaPlantilla(empresa, codigo, anio, plantilla);
        }

        public IEnumerable<AuditoriaResumen> ConsultaResumen(int empresa, int anio)
        {
            AuditoriasService _auditoriasService = new AuditoriasService();

            return _auditoriasService.ConsultaResumen(empresa, anio);
        }

        public string CopiaAuditoria(Models.Auditorias auditoria)
        {
            AuditoriasService _auditoriasService = new AuditoriasService();

            return _auditoriasService.CopiaAuditoria(auditoria);
        }

        public string Ingreso(Models.Auditorias auditoria)
        {
            AuditoriasService _auditoriasService = new AuditoriasService();

            return _auditoriasService.Ingreso(auditoria);
        }
    }
}