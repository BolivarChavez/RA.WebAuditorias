using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebAuditorias.Interfaces.CatalogoGastos;

namespace WebAuditorias.Services.CatalogoGastos
{
    public class CatalogoGastosService : ICatalogoGastosService
    {
        public string Actualizacion(Models.CatalogoGastos catalogoGastos)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "CatalogoGastos/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(catalogoGastos);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.CatalogoGastos> Consulta(int empresa)
        {
            HttpClient client = new HttpClient();
            List<Models.CatalogoGastos> listaError = new List<Models.CatalogoGastos>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "CatalogoGastos/Consulta/{0}", empresa.ToString().Trim()));

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
                listaError.Add(new Models.CatalogoGastos { cg_empresa = 0, cg_codigo = 0, cg_descripion = errorContent, cg_estado = "", cg_usuario_creacion ="", cg_fecha_creacion = DateTime.Now, cg_usuario_actualizacion = "", cg_fecha_actualizacion = DateTime.Now });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.CatalogoGastos>>(respuesta);
            }
        }

        public string Ingreso(Models.CatalogoGastos catalogoGastos)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "CatalogoGastos/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(catalogoGastos);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}