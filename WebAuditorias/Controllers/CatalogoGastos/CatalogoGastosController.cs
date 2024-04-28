using System.Collections.Generic;
using WebAuditorias.Interfaces.CatalogoGastos;
using WebAuditorias.Services.CatalogoGastos;

namespace WebAuditorias.Controllers.CatalogoGastos
{
    public class CatalogoGastosController : ICatalogoGastosController
    {
        public string Actualizacion(Models.CatalogoGastos catalogoGastos)
        {
            CatalogoGastosService _catalogoGastosService = new CatalogoGastosService();

            return  _catalogoGastosService.Actualizacion(catalogoGastos);
        }

        public IEnumerable<Models.CatalogoGastos> Consulta(int empresa)
        {
            CatalogoGastosService _catalogoGastosService = new CatalogoGastosService();

            return _catalogoGastosService.Consulta(empresa);
        }

        public string Ingreso(Models.CatalogoGastos catalogoGastos)
        {
            CatalogoGastosService _catalogoGastosService = new CatalogoGastosService();

            return _catalogoGastosService.Ingreso(catalogoGastos);
        }
    }
}