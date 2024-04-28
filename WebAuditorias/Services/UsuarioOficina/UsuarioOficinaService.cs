using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using WebAuditorias.Interfaces.UsuarioOficina;

namespace WebAuditorias.Services.UsuarioOficina
{
    public class UsuarioOficinaService : IUsuarioOficinaService
    {
        public IEnumerable<Models.UsuarioOficina> Consulta(int usuario, int empresa)
        {
            HttpClient client = new HttpClient();
            List<Models.UsuarioOficina> listaError = new List<Models.UsuarioOficina>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "UsuarioOficina/Consulta/{0}/{1}", usuario.ToString().Trim(), empresa.ToString().Trim()));

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
                listaError.Add(new Models.UsuarioOficina
                {
                    uo_usuario = 0,
                    uo_empresa = 0,
                    uo_oficina = 0,
                    uo_estado = errorContent,
                    uo_usuario_creacion = "",
                    uo_fecha_creacion = DateTime.Now,
                    uo_usuario_actualizacion = "",
                    uo_fecha_actualizacion = DateTime.Now
                });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.UsuarioOficina>>(respuesta);
            }
        }
    }
}