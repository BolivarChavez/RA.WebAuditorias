using System.Collections.Generic;

namespace WebAuditorias.Interfaces.Usuarios
{
    public interface IUsuariosService
    {
        IEnumerable<Models.Usuarios> Consulta(int codigo, string usuario);
    }
}
