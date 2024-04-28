using System.Collections.Generic;
using WebAuditorias.Interfaces.CatalogoPlantillas;
using WebAuditorias.Services.CatalogoPlantillas;

namespace WebAuditorias.Controllers.CatalogoPlantillas
{
    public class CatalogoPlantillasController : ICatalogoPlantillasController
    {
        public string Actualizacion(Models.CatalogoPlantillas plantilla)
        {
            CatalogoPlantillasService _catalogoPlantillasService = new CatalogoPlantillasService();

            return _catalogoPlantillasService.Actualizacion(plantilla);
        }

        public IEnumerable<Models.CatalogoPlantillas> Consulta(int empresa)
        {
            CatalogoPlantillasService _catalogoPlantillasService = new CatalogoPlantillasService();

            return _catalogoPlantillasService.Consulta(empresa);
        }

        public string Ingreso(Models.CatalogoPlantillas plantilla)
        {
            CatalogoPlantillasService _catalogoPlantillasService = new CatalogoPlantillasService();

            return _catalogoPlantillasService.Ingreso(plantilla);
        }
    }
}