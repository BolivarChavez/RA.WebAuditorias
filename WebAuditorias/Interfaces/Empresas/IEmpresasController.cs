using System.Collections.Generic;

namespace WebAuditorias.Interfaces.Empresas
{
    public interface IEmpresasController
    {
        IEnumerable<Models.Empresas> Consulta(int codigo);
    }
}
