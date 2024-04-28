using System.Collections.Generic;

namespace WebAuditorias.Interfaces.Oficinas
{
    public interface IOficinasController
    {
        IEnumerable<Models.Oficinas> Consulta(int empresa, int codigo);
    }
}
