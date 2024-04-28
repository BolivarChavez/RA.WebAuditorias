using System.Collections.Generic;
using WebAuditorias.Interfaces.Monedas;
using WebAuditorias.Services.Monedas;

namespace WebAuditorias.Controllers.Monedas
{
    public class MonedasController : IMonedasController
    {
        public string Actualizacion(Models.Monedas monedas)
        {
            MonedasService _monedasService = new MonedasService();

            return _monedasService.Actualizacion(monedas);
        }

        public IEnumerable<Models.Monedas> Consulta(int codigo)
        {
            MonedasService _monedasService = new MonedasService();

            return _monedasService.Consulta(codigo);
        }

        public string Ingreso(Models.Monedas monedas)
        {
            MonedasService _monedasService = new MonedasService();

            return _monedasService.Ingreso(monedas);
        }
    }
}