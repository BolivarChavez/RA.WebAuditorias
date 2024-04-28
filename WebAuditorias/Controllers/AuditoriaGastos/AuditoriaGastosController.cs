using System.Collections.Generic;
using WebAuditorias.Interfaces.AuditoriaGastos;
using WebAuditorias.Services.AuditoriaGastos;

namespace WebAuditorias.Controllers.AuditoriaGastos
{
    public class AuditoriaGastosController : IAuditoriaGastosController
    {
        public string Actualizacion(Models.AuditoriaGastos auditoriaGastos)
        {
            AuditoriaGastosService _auditoriaGastosService = new AuditoriaGastosService();

            return _auditoriaGastosService.Actualizacion(auditoriaGastos);
        }

        public IEnumerable<Models.AuditoriaGastos> Consulta(int empresa, int auditoria)
        {
            AuditoriaGastosService _auditoriaGastosService = new AuditoriaGastosService();

            return _auditoriaGastosService.Consulta(empresa, auditoria);
        }

        public string Ingreso(Models.AuditoriaGastos auditoriaGastos)
        {
            AuditoriaGastosService _auditoriaGastosService = new AuditoriaGastosService();

            return _auditoriaGastosService.Ingreso(auditoriaGastos);
        }
    }
}