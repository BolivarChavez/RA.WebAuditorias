using System.Collections.Generic;

namespace WebAuditorias.Interfaces.Usuarios
{
    public interface IUsuariosController
    {
        IEnumerable<Models.Usuarios> Consulta(int codigo, string usuario);
    }
}
