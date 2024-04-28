using System.Collections.Generic;
using WebAuditorias.Models;

namespace WebAuditorias.Interfaces.Operador
{
    public interface IOperadorService
    {
        Models.Usuarios ConsultaUsuario(int codigo, string usuario);

        IEnumerable<UsuarioGrupoOpciones> ConsultaGrupoOpciones (int usuario);

        IEnumerable<TransaccionesUsuario> ConsultaTransacciones(int usuario, int grupo);
    }
}
