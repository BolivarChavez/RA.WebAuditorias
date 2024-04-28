using System.Collections.Generic;

namespace WebAuditorias.Interfaces.CatalogoGastos
{
    public interface ICatalogoGastosController
    {
        string Ingreso(WebAuditorias.Models.CatalogoGastos catalogoGastos);

        string Actualizacion(WebAuditorias.Models.CatalogoGastos catalogoGastos);

        IEnumerable<WebAuditorias.Models.CatalogoGastos> Consulta(int empresa);
    }
}
