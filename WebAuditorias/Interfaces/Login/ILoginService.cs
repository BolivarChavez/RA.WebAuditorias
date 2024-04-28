using WebAuditorias.Models;

namespace WebAuditorias.Interfaces.Login
{
    public interface ILoginService
    {
        SPRetorno LoginUsuario(LoginUsuario login);

        string ValidaLogin(LoginUsuario login);
    }
}
