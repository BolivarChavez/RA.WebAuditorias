using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebAuditorias.Interfaces.CatalogoPlantillas;

namespace WebAuditorias.Services.CatalogoPlantillas
{
    public class CatalogoPlantillasService : ICatalogoPlantillasService
    {
        public string Actualizacion(Models.CatalogoPlantillas plantilla)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "CatalogoPlantillas/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(plantilla);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.CatalogoPlantillas> Consulta(int empresa)
        {
            HttpClient client = new HttpClient();
            List<Models.CatalogoPlantillas> listaError = new List<Models.CatalogoPlantillas>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "CatalogoPlantillas/Consulta/{0}", empresa.ToString().Trim()));

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
                listaError.Add(new Models.CatalogoPlantillas { cp_empresa = 0, cp_codigo = 0, cp_descripcion = errorContent, cp_estado = "", cp_usuario_creacion = "", cp_fecha_creacion = DateTime.Now, cp_usuario_actualizacion = "", cp_fecha_actualizacion = DateTime.Now });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.CatalogoPlantillas>>(respuesta);
            }
        }

        public string Ingreso(Models.CatalogoPlantillas plantilla)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "CatalogoPlantillas/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(plantilla);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}