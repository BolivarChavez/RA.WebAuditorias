using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using WebAuditorias.Interfaces.AuditoriaAsignacion;

namespace WebAuditorias.Services.AuditoriaAsignacion
{
    public class AuditoriaAsignacionService : IAuditoriaAsignacionService
    {
        public string Actualizacion(Models.AuditoriaAsignacion auditoriaAsignacion)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "AuditoriaAsignacion/Actualizacion";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(auditoriaAsignacion);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }

        public IEnumerable<Models.AuditoriaAsignacion> Consulta(int empresa, int auditoria, int tarea)
        {
            HttpClient client = new HttpClient();
            List<Models.AuditoriaAsignacion> listaError = new List<Models.AuditoriaAsignacion>();
            string url = string.Empty;
            string _key = string.Empty;
            string respuesta = string.Empty;
            string errorContent = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString();
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var uri = new Uri(string.Format(url + "AuditoriaAsignacion/Consulta/{0}/{1}/{2}", empresa.ToString().Trim(), auditoria.ToString().Trim(), tarea.ToString().Trim()));

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
                listaError.Add(new Models.AuditoriaAsignacion 
                {
                    aa_empresa = 0,
                    aa_auditoria = 0,
                    aa_tarea = 0,
                    aa_secuencia = 0,
                    aa_responsable = 0,
                    aa_tipo = errorContent,
                    aa_rol = "",
                    aa_estado = "",
                    aa_usuario_creacion = "",
                    aa_fecha_creacion = DateTime.Now,
                    aa_usuario_actualizacion = "",
                    aa_fecha_actualizacion = DateTime.Now
                });
                return listaError;
            }
            else
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.AuditoriaAsignacion>>(respuesta);
            }
        }

        public string Ingreso(Models.AuditoriaAsignacion auditoriaAsignacion)
        {
            string url = string.Empty;
            string _key = string.Empty;

            url = ConfigurationManager.AppSettings["UrlOpciones"].ToString() + "AuditoriaAsignacion/Ingreso";
            _key = ConfigurationManager.AppSettings["Llave_cifrado"].ToString();

            var json = JsonConvert.SerializeObject(auditoriaAsignacion);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}