using System.Collections.Generic;
using WebAuditorias.Interfaces.UsuarioOficina;
using WebAuditorias.Services.UsuarioOficina;

namespace WebAuditorias.Controllers.UsuarioOficina
{
    public class UsuarioOficinaController : IUsuarioOficinaController
    {
        public IEnumerable<Models.UsuarioOficina> Consulta(int usuario, int empresa)
        {
            UsuarioOficinaService _usuarioOficinaService = new UsuarioOficinaService();

            return _usuarioOficinaService.Consulta(usuario, empresa);
        }
    }
}