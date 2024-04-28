using System.Collections.Generic;
using WebAuditorias.Interfaces.CatalogoTareas;
using WebAuditorias.Services.CatalogoTareas;

namespace WebAuditorias.Controllers.CatalogoTareas
{
    public class CatalogoTareasController : ICatalogoTareasController
    {
        public string Actualizacion(Models.CatalogoTareas tarea)
        {
            CatalogoTareasService _catalogoTareasService = new CatalogoTareasService();

            return _catalogoTareasService.Actualizacion(tarea);
        }

        public IEnumerable<Models.CatalogoTareas> Consulta(int empresa)
        {
            CatalogoTareasService _catalogoTareasService = new CatalogoTareasService();

            return _catalogoTareasService.Consulta(empresa);
        }

        public string Ingreso(Models.CatalogoTareas tarea)
        {
            CatalogoTareasService _catalogoTareasService = new CatalogoTareasService();

            return _catalogoTareasService.Ingreso(tarea);
        }
    }
}