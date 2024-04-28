using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentos;
using WebAuditorias.Models.Bases;

namespace WebAuditorias.Views
{
    public partial class PlantillaIngresos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializedView();
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

        }

        protected void BtnEliminar_ServerClick(object sender, EventArgs e)
        {

        }

        protected void BtnCargar_ServerClick(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string ConsultaPlantillas(string parametros)
        {
            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();
            Plantilla_Ingresos ingreso = new Plantilla_Ingresos();
            List<Plantilla_Ingresos_Base> listaIngresos = new List<Plantilla_Ingresos_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3])).ToList();

            foreach (var lineaDoc in _documentos)
            {
                ingreso = JsonConvert.DeserializeObject<Plantilla_Ingresos>(lineaDoc.ad_registro);
                listaIngresos.Add(
                    new Plantilla_Ingresos_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        Mes = ingreso.Mes,
                        Factura = ingreso.Factura,
                        Cuenta = ingreso.Cuenta,
                        Detalle = ingreso.Detalle,
                        Concepto = ingreso.Concepto,
                        Subtotal = ingreso.Subtotal,
                        Porcentaje = ingreso.Porcentaje,
                        Total = ingreso.Total,
                        Fecha_Detraccion = ingreso.Fecha_Detraccion,
                        Detraccion_Moneda_Destino = ingreso.Detraccion_Moneda_Destino,
                        Detraccion_Moneda_Base = ingreso.Detraccion_Moneda_Base,
                        Comprobante_Ingreso = ingreso.Comprobante_Ingreso,
                        Neto_Ingreso = ingreso.Neto_Ingreso,
                        Flujo = ingreso.Flujo,
                        Estado_Cuenta_1 = ingreso.Estado_Cuenta_1,
                        Estado_Cuenta_2 = ingreso.Estado_Cuenta_2
                    }
                    );
            }

            return JsonConvert.SerializeObject(listaIngresos);
        }
    }
}