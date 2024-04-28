using System.Collections.Generic;
using WebAuditorias.Interfaces.CatalogoProcesos;
using WebAuditorias.Services.CatalogoProcesos;

namespace WebAuditorias.Controllers.CatalogoProcesos
{
    public class CatalogoProcesosController : ICatalogoProcesosController
    {
        public string Actualizacion(Models.CatalogoProcesos proceso)
        {
            CatalogoProcesosService _catalogoProcesosService = new CatalogoProcesosService();

            return _catalogoProcesosService.Actualizacion(proceso);
        }

        public IEnumerable<Models.CatalogoProcesos> Consulta(int empresa)
        {
            CatalogoProcesosService _catalogoProcesosService = new CatalogoProcesosService();

            return _catalogoProcesosService.Consulta(empresa);
        }

        public string Ingreso(Models.CatalogoProcesos proceso)
        {
            CatalogoProcesosService _catalogoProcesosService = new CatalogoProcesosService();

            return _catalogoProcesosService.Ingreso(proceso);
        }
    }
}