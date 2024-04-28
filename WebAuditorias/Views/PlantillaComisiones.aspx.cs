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
    public partial class PlantillaComisiones : System.Web.UI.Page
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
            Plantilla_Comisiones comision = new Plantilla_Comisiones();
            List<Plantilla_Comisiones_Base> listaComisiones = new List<Plantilla_Comisiones_Base>();
            string[] arrayParametros;
            arrayParametros = parametros.Split('|');

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3])).ToList();

            foreach (var lineaDoc in _documentos)
            {
                comision = JsonConvert.DeserializeObject<Plantilla_Comisiones>(lineaDoc.ad_registro);
                listaComisiones.Add(
                    new Plantilla_Comisiones_Base
                    {
                        IdRegistro = lineaDoc.ad_codigo,
                        ReferenciaLinea = lineaDoc.ad_referencia,
                        IdEstado = lineaDoc.ad_estado,
                        Mes = comision.Mes,
                        Monto_Recuperado = comision.Monto_Recuperado,
                        Monto_Planilla = comision.Monto_Planilla,
                        Monto_Honorarios = comision.Monto_Honorarios,
                        Total_Incentivos = comision.Total_Incentivos,
                        Cheque_Girado = comision.Cheque_Girado,
                        Pagado = comision.Pagado,
                        Entregado_Caja_Interna_1 = comision.Entregado_Caja_Interna_1,
                        No_Girado = comision.No_Girado,
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
    }
}