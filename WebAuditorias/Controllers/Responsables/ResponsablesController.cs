using System.Collections.Generic;
using WebAuditorias.Interfaces.Responsables;
using WebAuditorias.Services.Responsables;

namespace WebAuditorias.Controllers.Responsables
{
    public class ResponsablesController : IResponsablesController
    {
        public string Actualizacion(Models.Responsables responsables)
        {
            ResponsablesService _responsablesService = new ResponsablesService();

            return _responsablesService.Actualizacion(responsables);
        }

        public IEnumerable<Models.Responsables> Consulta(int codigo)
        {
            ResponsablesService _responsablesService = new ResponsablesService();

            return _responsablesService.Consulta(codigo);
        }

        public string Ingreso(Models.Responsables responsables)
        {
            ResponsablesService _responsablesService = new ResponsablesService();

            return _responsablesService.Ingreso(responsables);
        }
    }
}