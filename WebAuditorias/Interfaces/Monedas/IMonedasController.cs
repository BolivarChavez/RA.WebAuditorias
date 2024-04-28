using System.Collections.Generic;

namespace WebAuditorias.Interfaces.Monedas
{
    public interface IMonedasController
    {
        string Ingreso(Models.Monedas monedas);

        string Actualizacion(Models.Monedas monedas);

        IEnumerable<Models.Monedas> Consulta(int codigo);
    }
}