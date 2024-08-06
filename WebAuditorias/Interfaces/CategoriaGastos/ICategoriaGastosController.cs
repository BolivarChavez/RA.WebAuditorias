using System.Collections.Generic;

namespace WebAuditorias.Interfaces.CategoriaGastos
{
    public interface ICategoriaGastosController
    {
        string Ingreso(WebAuditorias.Models.CategoriaGastos categoriaGastos);

        string Actualizacion(WebAuditorias.Models.CategoriaGastos categoriaGastos);

        IEnumerable<WebAuditorias.Models.CategoriaGastos> Consulta(int empresa);
    }
}
