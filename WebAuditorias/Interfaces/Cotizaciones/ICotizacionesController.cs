using System.Collections.Generic;

namespace WebAuditorias.Interfaces.Cotizaciones
{
    public interface ICotizacionesController
    {
        string Ingreso(Models.Cotizaciones cotizaciones);

        string Actualizacion(Models.Cotizaciones cotizaciones);

        IEnumerable<Models.Cotizaciones> Consulta(int empresa, int monedaBase, int monedaDestino, int anio);

    }
}
