using System.Collections.Generic;

namespace WebAuditorias.Interfaces.Empresas
{
    public interface IEmpresasService
    {
        IEnumerable<Models.Empresas> Consulta(int codigo);
    }
}
