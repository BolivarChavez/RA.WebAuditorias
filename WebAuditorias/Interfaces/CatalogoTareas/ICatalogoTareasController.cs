using System.Collections.Generic;

namespace WebAuditorias.Interfaces.CatalogoTareas
{
    public interface ICatalogoTareasController
    {
        string Ingreso(WebAuditorias.Models.CatalogoTareas tarea);

        string Actualizacion(WebAuditorias.Models.CatalogoTareas tarea);

        IEnumerable<WebAuditorias.Models.CatalogoTareas> Consulta(int empresa);
    }
}
