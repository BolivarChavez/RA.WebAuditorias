using System.Collections.Generic;

namespace WebAuditorias.Interfaces.Responsables
{
    public interface IResponsablesService
    {
        string Ingreso(Models.Responsables responsables);

        string Actualizacion(Models.Responsables responsables);

        IEnumerable<Models.Responsables> Consulta(int codigo);
    }
}
