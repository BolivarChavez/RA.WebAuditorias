using System.Collections.Generic;

namespace WebAuditorias.Interfaces.UsuarioFacultad
{
    public interface IUsuarioFacultadService
    {
        IEnumerable<Models.UsuarioFacultad> Consulta(int idUsuario, int idTransaccion);
    }
}
