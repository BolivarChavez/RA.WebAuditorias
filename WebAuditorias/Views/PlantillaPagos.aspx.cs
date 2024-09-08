using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentos;
using WebAuditorias.Models.Bases;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Models;
using System.Runtime.Caching;

namespace WebAuditorias.Views
{
    public partial class PlantillaPagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
                CargaDatosUsuario();
            }
        }

        private void InitializedView()
        {
            string[] arrayParametros;
            arrayParametros = Request.QueryString["plantilla"].Split('|');

            Auditoria.Value = arrayParametros[0] + "-" + arrayParametros[1];
            Codigo.Value = "0";
            Tarea.Value = arrayParametros[2] + "-" + arrayParametros[3];
            Plantilla.Value = arrayParametros[4] + "-" + arrayParametros[5];
        }

        private void CargaDatosUsuario()
        {
            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            lblNombre.Text = user_cookie.Nombre;
            lblFechaConexion.Text = DateTime.Now.ToString();
        }

        protected void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            InitializedView();
        }

        protected void BtnBuscar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "document.getElementById('profile-tab').click(); LlenaGrid();", true);
        }

        protected void BtnGrabar_ServerClick(object sender, EventArgs e)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            Plantilla_Pagos pago = new Plantilla_Pagos();
            List<ValidaPlantilla> validaPlantilla = new List<ValidaPlantilla>();
            CargaPlantillaPagosController plantillaContoller = new CargaPlantillaPagosController();
            string jsonString;
            string response;
            double valorDecimal;
            DateTime fechaTabla;

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            pago.Periodo = Periodo.Value.ToUpper();
            pago.Detalle = Detalle.Value.ToUpper();
            pago.Fecha_Pago = !DateTime.TryParse(Fecha_Pago.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Pago.Value.Trim());  
            pago.Importe_Bruto = !double.TryParse(Importe_Bruto.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Importe_Bruto.Value.Trim(), CultureInfo.InvariantCulture);  
            pago.Descuentos = !double.TryParse(Descuentos.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Descuentos.Value.Trim(), CultureInfo.InvariantCulture);  
            pago.Neto_Pagar = !double.TryParse(Neto_Pagar.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Neto_Pagar.Value.Trim(), CultureInfo.InvariantCulture);  
            pago.Transferencia = !double.TryParse(Transferencia.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Transferencia.Value.Trim(), CultureInfo.InvariantCulture); 
            pago.Cheque = !double.TryParse(Cheque.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Cheque.Value.Trim(), CultureInfo.InvariantCulture);  
            pago.Diferencia = !double.TryParse(Diferencia.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Diferencia.Value.Trim(), CultureInfo.InvariantCulture);  
            pago.Numero_Cheque = Numero_Cheque.Value.ToUpper();
            pago.Numero_Informe = Numero_Informe.Value.ToUpper();
            pago.Observaciones = Observaciones.Value.ToUpper();

            var validaResponse = plantillaContoller.ValidarRegistroPago(pago);

            if (validaResponse != null && validaResponse.Count > 0)
            {
                validaPlantilla.Add(new ValidaPlantilla() { Linea = 0, Campos = validaResponse });
                ObjectCache cache = MemoryCache.Default;
                string CacheKey = user_cookie.Usuario.Trim();

                if (cache.Contains(CacheKey))
                    cache.Remove(CacheKey);

                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(CacheKey, validaPlantilla, cacheItemPolicy);

                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "mensajeGrabacion('0', 'Existen campos con errores')", true);
                return;
            }

            jsonString = JsonConvert.SerializeObject(pago);

            parametro.ad_empresa = 1;
            parametro.ad_auditoria = auditoriaId;
            parametro.ad_tarea = tareaId;
            parametro.ad_codigo = Int16.Parse(Codigo.Value);
            parametro.ad_plantilla = plantillaId;
            parametro.ad_referencia = "";
            parametro.ad_registro = jsonString;
            parametro.ad_auditoria_origen = 0;
            parametro.ad_responsable = 0;
            parametro.ad_estado = "A";
            parametro.ad_usuario_creacion = user_cookie.Usuario;
            parametro.ad_fecha_creacion = DateTime.Now;
            parametro.ad_usuario_actualizacion = user_cookie.Usuario;
            parametro.ad_fecha_actualizacion = DateTime.Now;

            if (Int16.Parse(Codigo.Value) == 0)
            {
                response = _controller.Ingreso(parametro);
            }
            else
            {
                response = _controller.Actualizacion(parametro);
            }

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "mensajeGrabacion('1', 'El registro de plantilla se grabó exitosamente')", true);
        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            Plantilla_Pagos pago = new Plantilla_Pagos();
            string jsonString;
            string response;
            double valorDecimal;
            DateTime fechaTabla;

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            pago.Periodo = Periodo.Value.ToUpper();
            pago.Detalle = Detalle.Value.ToUpper();
            pago.Fecha_Pago = !DateTime.TryParse(Fecha_Pago.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Pago.Value.Trim());
            pago.Importe_Bruto = !double.TryParse(Importe_Bruto.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Importe_Bruto.Value.Trim(), CultureInfo.InvariantCulture);
            pago.Descuentos = !double.TryParse(Descuentos.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Descuentos.Value.Trim(), CultureInfo.InvariantCulture);
            pago.Neto_Pagar = !double.TryParse(Neto_Pagar.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Neto_Pagar.Value.Trim(), CultureInfo.InvariantCulture);
            pago.Transferencia = !double.TryParse(Transferencia.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Transferencia.Value.Trim(), CultureInfo.InvariantCulture);
            pago.Cheque = !double.TryParse(Cheque.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Cheque.Value.Trim(), CultureInfo.InvariantCulture);
            pago.Diferencia = !double.TryParse(Diferencia.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Diferencia.Value.Trim(), CultureInfo.InvariantCulture);
            pago.Numero_Cheque = Numero_Cheque.Value.ToUpper();
            pago.Numero_Informe = Numero_Informe.Value.ToUpper();
            pago.Observaciones = Observaciones.Value.ToUpper();

            jsonString = JsonConvert.SerializeObject(pago);

            parametro.ad_empresa = 1;
            parametro.ad_auditoria = auditoriaId;
            parametro.ad_tarea = tareaId;
            parametro.ad_codigo = Int16.Parse(Codigo.Value);
            parametro.ad_plantilla = plantillaId;
            parametro.ad_referencia = "";
            parametro.ad_registro = jsonString;
            parametro.ad_auditoria_origen = 0;
            parametro.ad_responsable = 0;
            parametro.ad_estado = "X";
            parametro.ad_usuario_creacion = user_cookie.Usuario;
            parametro.ad_fecha_creacion = DateTime.Now;
            parametro.ad_usuario_actualizacion = user_cookie.Usuario;
            parametro.ad_fecha_actualizacion = DateTime.Now;

            response = _controller.Actualizacion(parametro);

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "mensajeGrabacion('1', 'El registro de plantilla se eliminó exitosamente')", true);
        }

        protected void BtnCargar_ServerClick(object sender, EventArgs e)
        {
            string fileName;

            if (CargaArchivo.HasFile)
            {
                fileName = CargaArchivo.FileName.Trim();
                HiddenField2.Value = fileName;
                CargaArchivo.SaveAs(Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + fileName);
                CargaHojasArchivo(Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + fileName);
            }
        }

        public void CargaHojasArchivo(string fileName)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                fileName.Trim() +
                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            objConn = new OleDbConnection(constr);
            objConn.Open();
            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            objConn.Close();

            Hoja.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                if (row["TABLE_NAME"].ToString().Contains("$"))
                {
                    string s = row["TABLE_NAME"].ToString();
                    Hoja.Items.Add(s.StartsWith("'") ? s.Substring(1, s.Length - 3) : s.Substring(0, s.Length - 1));
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillas(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Pagos pago = new Plantilla_Pagos();
            List<Plantilla_Pagos_Base> listaPagos = new List<Plantilla_Pagos_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();

            foreach (var lineaDoc in _documentos)
            {
                pago = JsonConvert.DeserializeObject<Plantilla_Pagos>(lineaDoc.ad_registro);
                listaPagos.Add(
                    new Plantilla_Pagos_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = "",
                        Periodo = pago.Periodo,
                        Detalle = pago.Detalle,
                        Fecha_Pago = pago.Fecha_Pago,
                        Importe_Bruto= pago.Importe_Bruto,
                        Descuentos = Math.Round(pago.Descuentos, 2),
                        Neto_Pagar = Math.Round(pago.Neto_Pagar, 2),
                        Transferencia = Math.Round(pago.Transferencia, 2),
                        Cheque = Math.Round(pago.Cheque, 2),
                        Diferencia = Math.Round(pago.Diferencia, 2),
                        Numero_Cheque = pago.Numero_Cheque,
                        Numero_Informe = pago.Numero_Informe,
                        Observaciones = pago.Observaciones
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaPagos);
        }

        protected void BtnCargaPlantilla_ServerClick(object sender, EventArgs e)
        {
            CargaPlantillaPagosController plantilla = new CargaPlantillaPagosController();
            string fileName = HiddenField2.Value;
            string archivoCarga = Server.MapPath("~") + ConfigurationManager.AppSettings["PathDocs"] + @"\" + fileName;
            string hojaArchivo = Hoja.Text.Trim();
            string referencia = Referencia.Value.Trim();
            string response = "";

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            response = plantilla.CargaPlantillaPagos(archivoCarga, hojaArchivo, 1, auditoriaId, tareaId, plantillaId, referencia);

            if (response == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "mensajeGrabacion('1', 'La plantilla se ha procesado correctamente')", true);
            }
            else
            {
                var listaErrores = JsonConvert.DeserializeObject<List<ValidaPlantilla>>(response);

                ObjectCache cache = MemoryCache.Default;
                string CacheKey = user_cookie.Usuario.Trim();

                if (cache.Contains(CacheKey))
                    cache.Remove(CacheKey);

                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(CacheKey, listaErrores, cacheItemPolicy);

                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "mensajeGrabacion('0', 'Existen campos con errores')", true);
            }
        }

        protected void BtnAddTarea_ServerClick(object sender, EventArgs e)
        {
            string parametros = "";

            parametros += "1|";
            parametros += Auditoria.Value.ToString().Split('-')[0] + "|";
            parametros += Auditoria.Value.ToString().Substring(Auditoria.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Tarea.Value.ToString().Split('-')[0] + "|";
            parametros += Tarea.Value.ToString().Substring(Tarea.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Plantilla.Value.ToString().Split('-')[0] + "|" + Plantilla.Value.ToString().Split('-')[1] + "|" + Codigo.Value.ToString();

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "window.open('AuditoriaDocumentoProceso.aspx?plantilla=" + parametros + "', '_blank');", true);
        }

        protected void BtnAddTareaGrupo_ServerClick(object sender, EventArgs e)
        {
            string parametros = "";

            parametros += "1|";
            parametros += Auditoria.Value.ToString().Split('-')[0] + "|";
            parametros += Auditoria.Value.ToString().Substring(Auditoria.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Tarea.Value.ToString().Split('-')[0] + "|";
            parametros += Tarea.Value.ToString().Substring(Tarea.Value.ToString().IndexOf('-') + 1) + "|";
            parametros += Plantilla.Value.ToString().Split('-')[0] + "|" + Plantilla.Value.ToString().Split('-')[1] + "|" + Codigo.Value.ToString();

            ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "window.open('AuditoriaDocumentoProcesoGrupo.aspx?plantilla=" + parametros + "', '_blank');", true);
        }
    }
}