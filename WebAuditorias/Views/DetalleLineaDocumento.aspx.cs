using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebAuditorias.Controllers.AuditoriaDocumentos;
using WebAuditorias.Models;

namespace WebAuditorias.Views
{
    public partial class DetalleLineaDocumento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MuestraLineaDocumento();
            }
        }

        private void MuestraLineaDocumento()
        {
            string[] arrayParametros;
            arrayParametros = Request.QueryString["plantilla"].Split('|');
            string lineaPlantilla;
            string contenido = "";
            DetalleLineaDocumentoController _controller = new DetalleLineaDocumentoController();

            lineaPlantilla = CargaLineaPlantilla();

            switch (int.Parse(arrayParametros[3])) 
            {
                case (int)TipoPlantilla.Plantillas.Plantilla_Cheques:
                    contenido = _controller.DetallePlantillaCheques(lineaPlantilla);
                    break;

                case (int)TipoPlantilla.Plantillas.Plantilla_Comisiones:
                    contenido = _controller.DetallePlantillaComisiones(lineaPlantilla);
                    break;

                case (int)TipoPlantilla.Plantillas.Plantilla_Ingresos:
                    contenido = _controller.DetallePlantillaIngresos(lineaPlantilla);
                    break;

                case (int)TipoPlantilla.Plantillas.Plantilla_Mutuos:
                    contenido = _controller.DetallePlantillaMutuos(lineaPlantilla);
                    break;

                case (int)TipoPlantilla.Plantillas.Plantilla_Pagos:
                    contenido = _controller.DetallePlantillaPagos(lineaPlantilla);
                    break;

                case (int)TipoPlantilla.Plantillas.Plantilla_Planillas:
                    contenido = _controller.DetallePlantillaPlanillas(lineaPlantilla);
                    break;

                case (int)TipoPlantilla.Plantillas.Plantilla_Reembolsos:
                    contenido = _controller.DetallePlantillaReembolsos(lineaPlantilla);
                    break;

                case (int)TipoPlantilla.Plantillas.Plantilla_Regalias:
                    contenido = _controller.DetallePlantillaRegalias(lineaPlantilla);
                    break;

                case (int)TipoPlantilla.Plantillas.Plantilla_Regularizaciones:
                    contenido = _controller.DetallePlantillaRegularizaciones(lineaPlantilla);
                    break;

                case (int)TipoPlantilla.Plantillas.Plantilla_Transferencias:
                    contenido = _controller.DetallePlantillaTransferencias(lineaPlantilla);
                    break;

                case (int)TipoPlantilla.Plantillas.Plantilla_Tributos:
                    contenido = _controller.DetallePlantillaTributos(lineaPlantilla);
                    break;
            }

            DivDetalle.InnerHtml = contenido;
        }

        private string CargaLineaPlantilla()
        {
            string[] arrayParametros;
            arrayParametros = Request.QueryString["plantilla"].Split('|');

            AuditoriaDocumentosController _controller = new AuditoriaDocumentosController();
            List<Models.AuditoriaDocumentos> _documentos = new List<Models.AuditoriaDocumentos>();

            _documentos = _controller.Consulta(int.Parse(arrayParametros[0]), int.Parse(arrayParametros[1]), int.Parse(arrayParametros[2]), int.Parse(arrayParametros[3]), 0).ToList();
            return _documentos.Where(li => li.ad_codigo == int.Parse(arrayParametros[4])).FirstOrDefault().ad_registro;
        }
    }
}