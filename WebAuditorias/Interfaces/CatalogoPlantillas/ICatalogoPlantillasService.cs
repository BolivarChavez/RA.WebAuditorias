using System.Collections.Generic;

namespace WebAuditorias.Interfaces.CatalogoPlantillas
{
    public interface ICatalogoPlantillasService
    {
        string Ingreso(WebAuditorias.Models.CatalogoPlantillas plantilla);

        string Actualizacion(WebAuditorias.Models.CatalogoPlantillas plantilla);

        IEnumerable<WebAuditorias.Models.CatalogoPlantillas> Consulta(int empresa);
    }
}
