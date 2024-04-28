using WebAuditorias.Models;

namespace WebAuditorias.Interfaces.Login
{
    public interface ILoginController
    {
        string ProcesaLogin(LoginUsuario login);
    }
}
