using System.Collections.Generic;
using WebAuditorias.Interfaces.Empresas;
using WebAuditorias.Services.Empresas;

namespace WebAuditorias.Controllers.Empresas
{
    public class EmpresasController : IEmpresasController
    {
        public IEnumerable<Models.Empresas> Consulta(int codigo)
        {
            EmpresasService _empresasService = new EmpresasService();

            return _empresasService.Consulta(codigo);
        }
    }
}