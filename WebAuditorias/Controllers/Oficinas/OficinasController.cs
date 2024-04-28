using System.Collections.Generic;
using WebAuditorias.Interfaces.Oficinas;
using WebAuditorias.Services.Oficinas;

namespace WebAuditorias.Controllers.Oficinas
{
    public class OficinasController : IOficinasController
    {
        public IEnumerable<Models.Oficinas> Consulta(int empresa, int codigo)
        {
            OficinasService _oficinasService = new OficinasService();

            return _oficinasService.Consulta(empresa, codigo);
        }
    }
}