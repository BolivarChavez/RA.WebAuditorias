using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using WebAuditorias.Interfaces.Operador;
using WebAuditorias.Models;

namespace WebAuditorias.Services.Operador
{
    public class OperadorService : IOperadorService
    {
        public IEnumerable<UsuarioGrupoOpciones> ConsultaGrupoOpciones(int usuario)
        {
            HttpClient client = new HttpClient();
            List<UsuarioGrupoOpciones> listaError = new List<UsuarioGrupoOpciones>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Usuarios/ConsultaGrupoOpcion/{0}", usuario.ToString().Trim()));

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", stoken);
            var response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                respuesta = responseContent.ReadAsStringAsync().Result;
            }
            else
            {
                errorContent = response.StatusCode.ToString() + " - " + response.ReasonPhrase;
                respuesta = "";
            }

            if (respuesta == "")
            {
                listaError.Add(new UsuarioGrupoOpciones { go_codigo = 0, go_orden = 0, go_descripcion = errorContent });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<UsuarioGrupoOpciones>>(respuesta);
            }
        }

        public IEnumerable<TransaccionesUsuario> ConsultaTransacciones(int usuario, int grupo)
        {
            HttpClient client = new HttpClient();
            List<TransaccionesUsuario> listaError = new List<TransaccionesUsuario>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Usuarios/ConsultaTransacciones/{0}/{1}", usuario.ToString().Trim(), grupo.ToString().Trim()));

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", stoken);
            var response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                respuesta = responseContent.ReadAsStringAsync().Result;
            }
            else
            {
                errorContent = response.StatusCode.ToString() + " - " + response.ReasonPhrase;
                respuesta = "";
            }

            if (respuesta == "")
            {
                listaError.Add(new TransaccionesUsuario { tr_codigo = 0, tr_descripcion = errorContent, tr_programa = "", tr_tipo = ""});
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<TransaccionesUsuario>>(respuesta);
            }
        }

        public Models.Usuarios ConsultaUsuario(int codigo, string usuario)
        {
            HttpClient client = new HttpClient();
            Models.Usuarios usuarioError = new Models.Usuarios();
            List<Models.Usuarios> listaError = new List<Models.Usuarios>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Usuarios/Consulta/{0}/{1}", codigo.ToString().Trim(), usuario.Trim()));

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", stoken);
            var response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                respuesta = responseContent.ReadAsStringAsync().Result;
            }
            else
            {
                errorContent = response.StatusCode.ToString() + " - " + response.ReasonPhrase;
                respuesta = "";
            }

            if (respuesta == "")
            {
                listaError.Add(new Models.Usuarios { 
                    us_codigo = 0, 
                    us_nombre = errorContent, 
                    us_login = "", 
                    us_password = "", 
                    us_email = "", 
                    us_ingresos = 0, 
                    us_ultimo_ingreso = DateTime.Now, 
                    us_estado = "", 
                    us_usuario_creacion = "", 
                    us_fecha_creacion = DateTime.Now, 
                    us_usuario_actualizacion = "", 
                    us_fecha_actualizacion = DateTime.Now });

                return listaError.First();
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.Usuarios>>(respuesta).First();
            }
        }
    }
}