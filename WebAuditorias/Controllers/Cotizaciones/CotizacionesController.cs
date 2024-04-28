using System.Collections.Generic;
using WebAuditorias.Interfaces.Cotizaciones;
using WebAuditorias.Services.Cotizaciones;

namespace WebAuditorias.Controllers.Cotizaciones
{
    public class CotizacionesController : ICotizacionesController
    {
        public string Actualizacion(Models.Cotizaciones cotizaciones)
        {
            CotizacionesService _cotizacionesService = new CotizacionesService();

            return _cotizacionesService.Actualizacion(cotizaciones);
        }

        public IEnumerable<Models.Cotizaciones> Consulta(int empresa, int monedaBase, int monedaDestino, int anio)
        {
            CotizacionesService _cotizacionesService = new CotizacionesService();

            return _cotizacionesService.Consulta(empresa, monedaBase, monedaDestino, anio);
        }

        public string Ingreso(Models.Cotizaciones cotizaciones)
        {
            CotizacionesService _cotizacionesService = new CotizacionesService();

            return _cotizacionesService.Ingreso(cotizaciones);
        }
    }
}