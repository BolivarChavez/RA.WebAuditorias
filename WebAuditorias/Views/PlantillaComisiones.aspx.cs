using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentos;
using WebAuditorias.Models.Bases;
using System.Globalization;
using WebAuditorias.Controllers.Cookies;
using WebAuditorias.Models;
using System.Runtime.Caching;

namespace WebAuditorias.Views
{
    public partial class PlantillaComisiones : System.Web.UI.Page
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

            if (user_cookie.Usuario == null || user_cookie.Usuario.Trim() == "")
            {
                Response.Redirect("ErrorAccesoOpcion.aspx", true);
            }

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
            Plantilla_Comisiones comision = new Plantilla_Comisiones();
            List<ValidaPlantilla> validaPlantilla = new List<ValidaPlantilla>();
            CargaPlantillaComisionesController plantillaContoller = new CargaPlantillaComisionesController();
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

            comision.Mes = Mes.Value.ToUpper();
            comision.Monto_Recuperado = !double.TryParse(Monto_Recuperado.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Monto_Recuperado.Value.Trim(), CultureInfo.InvariantCulture); 
            comision.Monto_Planilla = !double.TryParse(Monto_Planilla.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Monto_Planilla.Value.Trim(), CultureInfo.InvariantCulture);  
            comision.Monto_Honorarios = !double.TryParse(Monto_Honorarios.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Monto_Honorarios.Value.Trim(), CultureInfo.InvariantCulture); 
            comision.Total_Incentivos = !double.TryParse(Total_Incentivos.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Total_Incentivos.Value.Trim(), CultureInfo.InvariantCulture);  
            comision.Cheque_Girado = !double.TryParse(Cheque_Girado.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Cheque_Girado.Value.Trim(), CultureInfo.InvariantCulture);  
            comision.Pagado = !double.TryParse(Pagado.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Pagado.Value.Trim(), CultureInfo.InvariantCulture);  
            comision.Entregado_Caja_Interna_1 = Entregado_Caja_Interna_1.Value.ToUpper();
            comision.No_Girado = !double.TryParse(No_Girado.Value.Trim(), out valorDecimal) ? 0 : double.Parse(No_Girado.Value.Trim(), CultureInfo.InvariantCulture);  
            comision.Fecha_Informe = !DateTime.TryParse(Fecha_Informe.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Informe.Value.Trim());  
            comision.Fecha_Contabilidad = !DateTime.TryParse(Fecha_Contabilidad.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Contabilidad.Value.Trim());  
            comision.Informe_Comisiones = Informe_Comisiones.Value.ToUpper();
            comision.Entregado_Caja_Interna_2 = Entregado_Caja_Interna_2.Value.ToUpper();
            comision.Observaciones = Observaciones.Value.ToUpper();

            var validaResponse = plantillaContoller.ValidarRegistroComision(comision);

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

            jsonString = JsonConvert.SerializeObject(comision);

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

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            Plantilla_Comisiones comision = new Plantilla_Comisiones();
            string jsonString;
            string response;
            double valorDecimal;
            DateTime fechaTabla;

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            bool isEliminaChecked = chkEliminaTodos.Checked;

            if (isEliminaChecked)
            {
                EliminaPlantillas();
            }
            else
            {
                string[] arrayParametros;
                arrayParametros = Auditoria.Value.Split('-');
                int auditoriaId = int.Parse(arrayParametros[0]);
                arrayParametros = Tarea.Value.Split('-');
                Int16 tareaId = Int16.Parse(arrayParametros[0]);
                arrayParametros = Plantilla.Value.Split('-');
                Int16 plantillaId = Int16.Parse(arrayParametros[0]);

                comision.Mes = Mes.Value.ToUpper();
                comision.Monto_Recuperado = !double.TryParse(Monto_Recuperado.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Monto_Recuperado.Value.Trim(), CultureInfo.InvariantCulture);
                comision.Monto_Planilla = !double.TryParse(Monto_Planilla.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Monto_Planilla.Value.Trim(), CultureInfo.InvariantCulture);
                comision.Monto_Honorarios = !double.TryParse(Monto_Honorarios.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Monto_Honorarios.Value.Trim(), CultureInfo.InvariantCulture);
                comision.Total_Incentivos = !double.TryParse(Total_Incentivos.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Total_Incentivos.Value.Trim(), CultureInfo.InvariantCulture);
                comision.Cheque_Girado = !double.TryParse(Cheque_Girado.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Cheque_Girado.Value.Trim(), CultureInfo.InvariantCulture);
                comision.Pagado = !double.TryParse(Pagado.Value.Trim(), out valorDecimal) ? 0 : double.Parse(Pagado.Value.Trim(), CultureInfo.InvariantCulture);
                comision.Entregado_Caja_Interna_1 = Entregado_Caja_Interna_1.Value.ToUpper();
                comision.No_Girado = !double.TryParse(No_Girado.Value.Trim(), out valorDecimal) ? 0 : double.Parse(No_Girado.Value.Trim(), CultureInfo.InvariantCulture);
                comision.Fecha_Informe = !DateTime.TryParse(Fecha_Informe.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Informe.Value.Trim());
                comision.Fecha_Contabilidad = !DateTime.TryParse(Fecha_Contabilidad.Value.Trim(), out fechaTabla) ? DateTime.Parse("1900-01-01") : DateTime.Parse(Fecha_Contabilidad.Value.Trim());
                comision.Informe_Comisiones = Informe_Comisiones.Value.ToUpper();
                comision.Entregado_Caja_Interna_2 = Entregado_Caja_Interna_2.Value.ToUpper();
                comision.Observaciones = Observaciones.Value.ToUpper();

                jsonString = JsonConvert.SerializeObject(comision);

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
            }

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

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillas(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Comisiones comision = new Plantilla_Comisiones();
            List<Plantilla_Comisiones_Base> listaComisiones = new List<Plantilla_Comisiones_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');


            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).Where(x => x.ad_estado == "A").ToList();

            foreach (var lineaDoc in _documentos)
            {
                comision = JsonConvert.DeserializeObject<Plantilla_Comisiones>(lineaDoc.ad_registro);
                listaComisiones.Add(
                    new Plantilla_Comisiones_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        ReferenciaDocumento = "",
                        Mes = comision.Mes,
                        Monto_Recuperado = Math.Round(comision.Monto_Recuperado, 2),
                        Monto_Planilla = Math.Round(comision.Monto_Planilla, 2),    
                        Monto_Honorarios = Math.Round(comision.Monto_Honorarios, 2),
                        Total_Incentivos = Math.Round(comision.Total_Incentivos, 2),
                        Cheque_Girado = Math.Round(comision.Cheque_Girado, 2),
                        Pagado = Math.Round(comision.Pagado, 2),
                        Entregado_Caja_Interna_1 = comision.Entregado_Caja_Interna_1,
                        No_Girado = Math.Round(comision.No_Girado, 2),
                        Fecha_Informe = comision.Fecha_Informe,
                        Fecha_Contabilidad = comision.Fecha_Contabilidad,
                        Informe_Comisiones = comision.Informe_Comisiones,
                        Entregado_Caja_Interna_2 = comision.Entregado_Caja_Interna_2,
                        Observaciones = comision.Observaciones
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaComisiones);
        }

        private void EliminaPlantillas()
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            Models.AuditoriaDocumentos parametro = new Models.AuditoriaDocumentos();
            string response;

            string[] arrayParametros;
            arrayParametros = Auditoria.Value.Split('-');
            int auditoriaId = int.Parse(arrayParametros[0]);
            arrayParametros = Tarea.Value.Split('-');
            Int16 tareaId = Int16.Parse(arrayParametros[0]);
            arrayParametros = Plantilla.Value.Split('-');
            Int16 plantillaId = Int16.Parse(arrayParametros[0]);

            string selectedRecords = HiddenField3.Value;
            string[] arrayIds = Array.Empty<string>();

            if (!string.IsNullOrEmpty(selectedRecords))
            {
                arrayIds = selectedRecords.Split(',');
            }

            UserInfoCookie user_cookie = new UserInfoCookie();
            UserInfoCookieController _UserInfoCookieController = new UserInfoCookieController();
            user_cookie = _UserInfoCookieController.ObtieneInfoCookie();

            if (arrayIds.Count() > 0)
            {
                foreach (var id in arrayIds)
                {
                    parametro.ad_empresa = 1;
                    parametro.ad_auditoria = auditoriaId;
                    parametro.ad_tarea = tareaId;
                    parametro.ad_codigo = Int16.Parse(id.Trim());
                    parametro.ad_plantilla = plantillaId;
                    parametro.ad_referencia = "";
                    parametro.ad_registro = "";
                    parametro.ad_auditoria_origen = 0;
                    parametro.ad_responsable = 0;
                    parametro.ad_estado = "X";
                    parametro.ad_usuario_creacion = user_cookie.Usuario;
                    parametro.ad_fecha_creacion = DateTime.Now;
                    parametro.ad_usuario_actualizacion = user_cookie.Usuario;
                    parametro.ad_fecha_actualizacion = DateTime.Now;

                    response = _controller.Eliminacion(parametro);
                }
            }
        }


        protected void BtnCargaPlantilla_ServerClick(object sender, EventArgs e)
        {
            CargaPlantillaComisionesController plantilla = new CargaPlantillaComisionesController();
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

            response = plantilla.CargaPlantillaComisiones(archivoCarga, hojaArchivo, 1, auditoriaId, tareaId, plantillaId, referencia);

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