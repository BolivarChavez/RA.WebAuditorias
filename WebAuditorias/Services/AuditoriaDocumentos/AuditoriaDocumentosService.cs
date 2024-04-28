using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebAuditorias.Interfaces.AuditoriaDocumentos;

namespace WebAuditorias.Services.AuditoriaDocumentos
{
    public class AuditoriaDocumentosService : IAuditoriaDocumentosService
    {
        public string Actualizacion(Models.AuditoriaDocumentos auditoriaDocumento)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "AuditoriaDocumentos/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(auditoriaDocumento);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.AuditoriaDocumentos> Consulta(int empresa, int auditoria, int tarea, int plantilla)
        {
            HttpClient client = new HttpClient();
            List<Models.AuditoriaDocumentos> listaError = new List<Models.AuditoriaDocumentos>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "AuditoriaDocumentos/Consulta/{0}/{1}/{2}/{3}", empresa.ToString().Trim(), auditoria.ToString().Trim(), tarea.ToString().Trim(), plantilla.ToString().Trim()));

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
                listaError.Add(new Models.AuditoriaDocumentos
                {
                    ad_empresa = 0,
                    ad_auditoria = 0,
                    ad_tarea = 0,
                    ad_codigo = 0,
                    ad_plantilla = 0,
                    ad_referencia = errorContent,
                    ad_registro = "",
                    ad_auditoria_origen = 0,
                    ad_responsable = 0,
                    ad_estado = "",
                    ad_usuario_creacion = "",
                    ad_fecha_creacion = DateTime.Now,
                    ad_usuario_actualizacion = "",
                    ad_fecha_actualizacion = DateTime.Now
                });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.AuditoriaDocumentos>>(respuesta);
            }
        }

        public string Ingreso(Models.AuditoriaDocumentos auditoriaDocumento)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "AuditoriaDocumentos/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(auditoriaDocumento);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}