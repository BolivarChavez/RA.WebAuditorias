using System.Collections.Generic;

namespace WebAuditorias.Interfaces.UsuarioOficina
{
    public interface IUsuarioOficinaService
    {
        IEnumerable<Models.UsuarioOficina> Consulta(int usuario, int empresa);
    }
}
