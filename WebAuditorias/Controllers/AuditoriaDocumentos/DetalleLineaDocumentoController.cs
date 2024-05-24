using Newtonsoft.Json;
using PrototipoData.Models;
using System;
using System.Reflection;

namespace WebAuditorias.Controllers.AuditoriaDocumentos
{
    public class DetalleLineaDocumentoController
    {
        public string DetallePlantillaCheques(string lineaDocumento)
        {
            string contenido = string.Empty;
            Plantilla_Cheques cheque = new Plantilla_Cheques();

            cheque = JsonConvert.DeserializeObject<Plantilla_Cheques>(lineaDocumento);
            contenido = ConstruyeTabla(cheque);

            return contenido;
        }

        public string DetallePlantillaComisiones(string lineaDocumento)
        {
            string contenido = string.Empty;
            Plantilla_Comisiones comision = new Plantilla_Comisiones();

            comision = JsonConvert.DeserializeObject<Plantilla_Comisiones>(lineaDocumento);
            contenido = ConstruyeTabla(comision);

            return contenido;
        }

        public string DetallePlantillaIngresos(string lineaDocumento)
        {
            string contenido = string.Empty;
            Plantilla_Ingresos ingreso = new Plantilla_Ingresos();

            ingreso = JsonConvert.DeserializeObject<Plantilla_Ingresos>(lineaDocumento);
            contenido = ConstruyeTabla(ingreso);

            return contenido;
        }

        public string DetallePlantillaMutuos(string lineaDocumento)
        {
            string contenido = string.Empty;
            Plantilla_Mutuos mutuo = new Plantilla_Mutuos();

            mutuo = JsonConvert.DeserializeObject<Plantilla_Mutuos>(lineaDocumento);
            contenido = ConstruyeTabla(mutuo);

            return contenido;
        }

        public string DetallePlantillaPagos(string lineaDocumento)
        {
            string contenido = string.Empty;
            Plantilla_Pagos pago = new Plantilla_Pagos();

            pago = JsonConvert.DeserializeObject<Plantilla_Pagos>(lineaDocumento);
            contenido = ConstruyeTabla(pago);

            return contenido;
        }

        public string DetallePlantillaPlanillas(string lineaDocumento)
        {
            string contenido = string.Empty;
            Plantilla_Planillas planilla = new Plantilla_Planillas();

            planilla = JsonConvert.DeserializeObject<Plantilla_Planillas>(lineaDocumento);
            contenido = ConstruyeTabla(planilla);

            return contenido;
        }

        public string DetallePlantillaReembolsos(string lineaDocumento)
        {
            string contenido = string.Empty;
            Plantilla_Reembolsos reembolso = new Plantilla_Reembolsos();

            reembolso = JsonConvert.DeserializeObject<Plantilla_Reembolsos>(lineaDocumento);
            contenido = ConstruyeTabla(reembolso);

            return contenido;
        }

        public string DetallePlantillaRegalias(string lineaDocumento)
        {
            string contenido = string.Empty;
            Plantilla_Regalias regalia = new Plantilla_Regalias();

            regalia = JsonConvert.DeserializeObject<Plantilla_Regalias>(lineaDocumento);
            contenido = ConstruyeTabla(regalia);

            return contenido;
        }

        public string DetallePlantillaRegularizaciones(string lineaDocumento)
        {
            string contenido = string.Empty;
            Plantilla_Regularizaciones regularizacion = new Plantilla_Regularizaciones();

            regularizacion = JsonConvert.DeserializeObject<Plantilla_Regularizaciones>(lineaDocumento);
            contenido = ConstruyeTabla(regularizacion);

            return contenido;
        }

        public string DetallePlantillaTransferencias(string lineaDocumento)
        {
            string contenido = string.Empty;
            Plantilla_Transferencias transferencia = new Plantilla_Transferencias();

            transferencia = JsonConvert.DeserializeObject<Plantilla_Transferencias>(lineaDocumento);
            contenido = ConstruyeTabla(transferencia);

            return contenido;
        }

        public string DetallePlantillaTributos(string lineaDocumento)
        {
            string contenido = string.Empty;
            Plantilla_Tributos tributo = new Plantilla_Tributos();

            tributo = JsonConvert.DeserializeObject<Plantilla_Tributos>(lineaDocumento);
            contenido = ConstruyeTabla(tributo);

            return contenido;
        }

        private string ConstruyeTabla(Object obj)
        {
            Type type;
            string contenido = string.Empty;

            type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();

            contenido += @"<table class=""table table-hover"">";
            contenido += @"<thead>";
            contenido += @"<tr>";
            contenido += @"<th style=""width:30%;"">Campo</th>";
            contenido += @"<th style=""width:70%;"">Valor</th>";
            contenido += @"</tr>";
            contenido += @"</thead>";
            contenido += @"<tbody>";

            foreach (PropertyInfo prop in props)
            {
                contenido += @"<tr>";
                contenido += @"<td style=""word-wrap: break-word;"">" + prop.Name + "</td>";
                contenido += @"<td style=""word-wrap: break-word;"">" + prop.GetValue(obj) + "</td>";
                contenido += @"</tr>";
            }

            contenido += @"</tbody>";
            contenido += @"</table>";

            return contenido;
        }
    }
}