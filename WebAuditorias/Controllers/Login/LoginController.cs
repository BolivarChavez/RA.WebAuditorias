using System;
using System.Web;
using WebAuditorias.Interfaces.Login;
using WebAuditorias.Models;
using WebAuditorias.Services.Login;
using WebAuditorias.Services.Operador;
using WebAuditorias.Services.Utils;

namespace WebAuditorias.Controllers.Login
{
    public class LoginController : ILoginController
    {
        public string ProcesaLogin(LoginUsuario login)
        {
            LoginService _LoginService = new LoginService();
            OperadorService _OperadorService = new OperadorService();
            CifradoService _cifrado = new CifradoService();
            string respvalida;
            string passwordCifrado;

            respvalida = _LoginService.ValidaLogin(login);

            if (respvalida == "")
            {
                passwordCifrado = _cifrado.Encriptar(login.password.ToLower());
                login.password = passwordCifrado;

                var retorno = _LoginService.LoginUsuario(login);

                if (retorno.retorno == 0)
                {
                    var operador = _OperadorService.ConsultaUsuario(0, login.usuario);

                    if (HttpContext.Current.Request.Cookies["userInfo"] != null)
                    {
                        HttpCookie myCookie = new HttpCookie("userInfo");
                        myCookie.Expires = DateTime.Now.AddDays(-1d);
                        HttpContext.Current.Response.Cookies.Add(myCookie);
                    }

                    HttpCookie userInfo = new HttpCookie("userInfo");
                    userInfo["Usuario"] = operador.us_login.Trim();
                    userInfo["Nombre"] = operador.us_nombre;
                    userInfo["Token"] = retorno.descripcion;
                    userInfo["CodigoUsuario"] = operador.us_codigo.ToString().Trim();
                    userInfo.Expires.Add(new TimeSpan(8, 0, 0));
                    HttpContext.Current.Response.Cookies.Add(userInfo);

                    return "1|success|Bienvenido!|Sus credenciales se validaron correctamente|Consola.aspx|200";
                }
                else
                {
                    return "0|error|Error en credenciales de ingreso|" + retorno.mensaje + "|Inicio.aspx|401";
                }
            }
            else
            {
                return "0|error|Ingreso no correcto|" + respvalida + "|Inicio.aspx|401";
            }
        }
    }
}