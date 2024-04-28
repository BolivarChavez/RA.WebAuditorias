using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using WebAuditorias.Interfaces.Login;
using WebAuditorias.Models;

namespace WebAuditorias.Services.Login
{
    public class LoginService : ILoginService
    {
        public SPRetorno LoginUsuario(LoginUsuario login)
        {
            string url = string.Empty;
            string _key = string.Empty;
            LoginUsuario NewLogin = new LoginUsuario();

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString() + "Usuarios/ProcesaLogin";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            NewLogin.usuario = login.usuario;
            NewLogin.password = login.password;
             
            var json = JsonConvert.SerializeObject(NewLogin);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<IEnumerable<SPRetorno>>(result).FirstOrDefault();
        }

        public string ValidaLogin(LoginUsuario login)
        {
            string respuesta = string.Empty;

            if (login.usuario.Trim().Length <= 1)
            {
                respuesta = "Se debe ingresar un nombre de usuario";
            }

            if (login.password.Trim().Length <= 1)
            {
                respuesta = "Se debe ingresar una contraseña";
            }

            if (!login.usuario.Trim().All(char.IsLetter))
            {
                respuesta = "El nombre de usuario contiene caracteres no validos";
            }

            return respuesta;
        }
    }
}