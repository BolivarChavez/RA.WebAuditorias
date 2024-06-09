using System;

namespace WebAuditorias.Interfaces.Usuarios
{
    public interface IUsuariosFacultadController
    {
        bool ValidaFacultad(int idUsuario, int idTransaccion, Int16 idFacultad);
    }
}
