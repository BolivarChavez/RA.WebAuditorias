using System.Collections.Generic;

namespace WebAuditorias.Interfaces.Oficinas
{
    public interface IOficinasService
    {
        IEnumerable<Models.Oficinas> Consulta(int empresa, int codigo);
    }
}
