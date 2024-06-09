using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using WebAuditorias.Interfaces.UsuarioFacultad;

namespace WebAuditorias.Services.UsuarioFacultad
{
    public class UsuarioFacultadService : IUsuarioFacultadService
    {
        public IEnumerable<Models.UsuarioFacultad> Consulta(int idUsuario, int idTransaccion)
        {
            HttpClient client = new HttpClient();
            List<Models.UsuarioFacultad> listaError = new List<Models.UsuarioFacultad>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "UsuarioFacultad/Consulta/{0}/{1}", idUsuario.ToString().Trim(), idTransaccion.ToString().Trim()));

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
                listaError.Add(new Models.UsuarioFacultad
                {
                    uf_usuario = 0,
                    uf_transaccion = 0,
                    uf_facultad = 0,
                    uf_estado = errorContent,
                    uf_usuario_creacion = "",
                    uf_fecha_creacion = DateTime.Now,
                    uf_usuario_actualizacion = "",
                    uf_fecha_actualizacion = DateTime.Now
                });

                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.UsuarioFacultad>>(respuesta);
            }
        }
    }
}