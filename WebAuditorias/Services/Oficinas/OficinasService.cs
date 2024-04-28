using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using WebAuditorias.Interfaces.Oficinas;

namespace WebAuditorias.Services.Oficinas
{
    public class OficinasService : IOficinasService
    {
        public IEnumerable<Models.Oficinas> Consulta(int empresa, int codigo)
        {
            HttpClient client = new HttpClient();
            List<Models.Oficinas> listaError = new List<Models.Oficinas>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlLogin"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Oficinas/Consulta/{0}/{1}", empresa.ToString().Trim(), codigo.ToString().Trim()));

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
                listaError.Add(new Models.Oficinas
                {
                    of_empresa = 0,
                    of_codigo = 0,
                    of_nombre = errorContent,
                    of_pais = "",
                    of_provincia = "",
                    of_ciudad = "",
                    of_direccion = "",
                    of_telefono = "",
                    of_contacto = "",
                    of_email = "",
                    of_estado = "",
                    of_usuario_creacion = "",
                    of_fecha_creacion = DateTime.Now,
                    of_usuario_actualizacion = "",
                    of_fecha_actualizacion = DateTime.Now
                });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.Oficinas>>(respuesta);
            }
        }
    }
}