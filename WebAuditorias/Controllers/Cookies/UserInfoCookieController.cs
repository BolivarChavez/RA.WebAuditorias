using System.Web;
using WebAuditorias.Interfaces.Cookies;
using WebAuditorias.Models;

namespace WebAuditorias.Controllers.Cookies
{
    public class UserInfoCookieController : IUserInfoCookieController
    {
        public UserInfoCookie ObtieneInfoCookie()
        {
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["userInfo"];
            UserInfoCookie resp_cookie = new UserInfoCookie();

            if (reqCookies != null)
            {
                resp_cookie.Usuario = reqCookies["Usuario"];
                resp_cookie.Nombre = reqCookies["Nombre"];
                resp_cookie.Token = reqCookies["Token"];
                resp_cookie.CodigoUsuario = reqCookies["CodigoUsuario"];
            }
            else
            {
                resp_cookie.Usuario = "";
                resp_cookie.Nombre = "";
                resp_cookie.Token = "";
                resp_cookie.CodigoUsuario = "";
            }

            return resp_cookie;
        }
    }
}