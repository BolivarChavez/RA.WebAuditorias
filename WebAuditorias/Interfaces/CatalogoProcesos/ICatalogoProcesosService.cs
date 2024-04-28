using System.Collections.Generic;

namespace WebAuditorias.Interfaces.CatalogoProcesos
{
    public interface ICatalogoProcesosService
    {
        string Ingreso(WebAuditorias.Models.CatalogoProcesos proceso);

        string Actualizacion(WebAuditorias.Models.CatalogoProcesos proceso);

        IEnumerable<WebAuditorias.Models.CatalogoProcesos> Consulta(int empresa);
    }
}
