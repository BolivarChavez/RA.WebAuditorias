using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebAuditorias.Interfaces.AuditoriaGastos;

namespace WebAuditorias.Services.AuditoriaGastos
{
    public class AuditoriaGastosService : IAuditoriaGastosService
    {
        public string Actualizacion(Models.AuditoriaGastos auditoriaGastos)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "AuditoriaGastos/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(auditoriaGastos);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.AuditoriaGastos> Consulta(int empresa, int auditoria)
        {
            HttpClient client = new HttpClient();
            List<Models.AuditoriaGastos> listaError = new List<Models.AuditoriaGastos>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "AuditoriaGastos/Consulta/{0}/{1}", empresa.ToString().Trim(), auditoria.ToString().Trim()));

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
                listaError.Add(new Models.AuditoriaGastos
                {
                    ag_empresa = 0,
                    ag_auditoria = 0,
                    ag_secuencia = 0,
                    ag_tipo = 0,
                    ag_fecha_inicio = DateTime.Now,
                    ag_fecha_fin = DateTime.Now,
                    ag_valor = 0,
                    ag_estado = errorContent,
                    ag_usuario_creacion = "",
                    ag_fecha_creacion = DateTime.Now,
                    ag_usuario_actualizacion = "",
                    ag_fecha_actualizacion = DateTime.Now
                });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.AuditoriaGastos>>(respuesta);
            }
        }

        public string Ingreso(Models.AuditoriaGastos auditoriaGastos)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "AuditoriaGastos/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(auditoriaGastos);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}