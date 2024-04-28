using System.Collections.Generic;
using WebAuditorias.Interfaces.Usuarios;
using WebAuditorias.Services.Usuarios;

namespace WebAuditorias.Controllers.Usuarios
{
    public class UsuariosController : IUsuariosController
    {
        public IEnumerable<Models.Usuarios> Consulta(int codigo, string usuario)
        {
            UsuariosService _usuariosService = new UsuariosService();

            return _usuariosService.Consulta(codigo, usuario);
        }
    }
}