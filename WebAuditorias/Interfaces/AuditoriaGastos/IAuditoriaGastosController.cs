using System.Collections.Generic;

namespace WebAuditorias.Interfaces.AuditoriaGastos
{
    public interface IAuditoriaGastosController
    {
        string Ingreso(Models.AuditoriaGastos auditoriaGastos);

        string Actualizacion(Models.AuditoriaGastos auditoriaGastos);

        IEnumerable<Models.AuditoriaGastos> Consulta(int empresa, int auditoria);
    }
}
