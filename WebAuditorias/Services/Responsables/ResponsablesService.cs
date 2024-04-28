using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebAuditorias.Interfaces.Responsables;

namespace WebAuditorias.Services.Responsables
{
    public class ResponsablesService : IResponsablesService
    {
        public string Actualizacion(Models.Responsables responsables)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "Responsables/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(responsables);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.Responsables> Consulta(int codigo)
        {
            HttpClient client = new HttpClient();
            List<Models.Responsables> listaError = new List<Models.Responsables>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Responsables/Consulta/{0}", codigo.ToString().Trim()));

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
                listaError.Add(new Models.Responsables
                {
                    re_empresa = 0,
                    re_codigo = 0,
                    re_nombre = errorContent,
                    re_cargo = "",
                    re_oficina = 0,
                    re_tipo = "",
                    re_correo = "",
                    re_usuario = "",
                    re_estado = "",
                    re_usuario_creacion = "",
                    re_fecha_creacion = DateTime.Now,
                    re_usuario_actualizacion = "",
                    re_fecha_actualizacion = DateTime.Now
                });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.Responsables>>(respuesta);
            }
        }

        public string Ingreso(Models.Responsables responsables)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "Responsables/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(responsables);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}