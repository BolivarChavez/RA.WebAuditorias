using System.Collections.Generic;
using System.Linq;
using WebAuditorias.Interfaces.Usuarios;
using WebAuditorias.Services.UsuarioFacultad;

namespace WebAuditorias.Controllers.Usuarios
{
    public class UsuariosFacultadController : IUsuariosFacultadController
    {
        public bool ValidaFacultad(int idUsuario, int idTransaccion, short idFacultad)
        {
            UsuarioFacultadService _usuarioFacultadService = new UsuarioFacultadService();
            List<Models.UsuarioFacultad> _usuarioFacultades = new List<Models.UsuarioFacultad>();
            int facultades;

            _usuarioFacultades = _usuarioFacultadService.Consulta(idUsuario, idTransaccion).Where(x => x.uf_estado == "A").ToList();
            var _facultadesAutorizadas = _usuarioFacultades.Where(x => x.uf_facultad == idFacultad || x.uf_facultad == 0).ToList();

            if (_facultadesAutorizadas == null)
            {
                facultades = 0;
            }
            else
            {
                facultades = _facultadesAutorizadas.Count();
            }

            return facultades > 0;
        }
    }
}