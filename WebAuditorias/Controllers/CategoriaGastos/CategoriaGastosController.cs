using System.Collections.Generic;
using WebAuditorias.Interfaces.CategoriaGastos;
using WebAuditorias.Services.CategoriaGastos;

namespace WebAuditorias.Controllers.CategoriaGastos
{
    public class CategoriaGastosController : ICategoriaGastosController
    {
        public string Actualizacion(Models.CategoriaGastos categoriaGastos)
        {
            CategoriaGastosService _categoriaGastosService = new CategoriaGastosService();

            return _categoriaGastosService.Actualizacion(categoriaGastos);
        }

        public IEnumerable<Models.CategoriaGastos> Consulta(int empresa)
        {
            CategoriaGastosService _categoriaGastosService = new CategoriaGastosService();

            return _categoriaGastosService.Consulta(empresa);
        }

        public string Ingreso(Models.CategoriaGastos categoriaGastos)
        {
            CategoriaGastosService _categoriaGastosService = new CategoriaGastosService();

            return _categoriaGastosService.Ingreso(categoriaGastos);
        }
    }
}