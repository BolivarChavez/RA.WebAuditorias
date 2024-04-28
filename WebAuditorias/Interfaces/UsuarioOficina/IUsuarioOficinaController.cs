using System.Collections.Generic;

namespace WebAuditorias.Interfaces.UsuarioOficina
{
    public interface IUsuarioOficinaController
    {
        IEnumerable<Models.UsuarioOficina> Consulta(int usuario, int empresa);
    }
}
