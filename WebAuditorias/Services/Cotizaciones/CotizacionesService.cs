using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebAuditorias.Interfaces.Cotizaciones;

namespace WebAuditorias.Services.Cotizaciones
{
    public class CotizacionesService : ICotizacionesService
    {
        public string Actualizacion(Models.Cotizaciones cotizaciones)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "Cotizaciones/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(cotizaciones);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.Cotizaciones> Consulta(int empresa, int monedaBase, int monedaDestino, int anio)
        {
            HttpClient client = new HttpClient();
            List<Models.Cotizaciones> listaError = new List<Models.Cotizaciones>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "Cotizaciones/Consulta/{0}/{1}/{2}/{3}", empresa.ToString().Trim(), monedaBase.ToString().Trim(), monedaDestino.ToString().Trim(), anio.ToString().Trim()));

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
                listaError.Add(new Models.Cotizaciones
                {
                    co_empresa = 0,
                    co_moneda_base = 0,
                    co_moneda_destino = 0,
                    co_cotizacion = 0,
                    co_fecha_vigencia = DateTime.Now,
                    co_estado = errorContent,
                    co_usuario_creacion = "",
                    co_fecha_creacion = DateTime.Now,
                    co_usurio_actualizacion = "",
                    co_fecha_actualizacion = DateTime.Now
                });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.Cotizaciones>>(respuesta);
            }
        }

        public string Ingreso(Models.Cotizaciones cotizaciones)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "Cotizaciones/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(cotizaciones);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}