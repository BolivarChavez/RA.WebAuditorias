using System.Collections.Generic;
using WebAuditorias.Interfaces.AuditoriaAsignacion;
using WebAuditorias.Services.AuditoriaAsignacion;

namespace WebAuditorias.Controllers.AuditoriaAsignacion
{
    public class AuditoriaAsignacionController : IAuditoriaAsignacionController
    {
        public string Actualizacion(Models.AuditoriaAsignacion auditoriaAsignacion)
        {
            AuditoriaAsignacionService _auditoriaAsignacionService = new AuditoriaAsignacionService();

            return _auditoriaAsignacionService.Actualizacion(auditoriaAsignacion);
        }

        public IEnumerable<Models.AuditoriaAsignacion> Consulta(int empresa, int auditoria, int tarea)
        {
            AuditoriaAsignacionService _auditoriaAsignacionService = new AuditoriaAsignacionService();

            return _auditoriaAsignacionService.Consulta(empresa, auditoria, tarea);
        }

        public string Ingreso(Models.AuditoriaAsignacion auditoriaAsignacion)
        {
            AuditoriaAsignacionService _auditoriaAsignacionService = new AuditoriaAsignacionService();

            return _auditoriaAsignacionService.Ingreso(auditoriaAsignacion);
        }
    }
}