using System;

namespace PrototipoData.Models
{
    public class Plantilla_Cheques
    {
        public string Item { get; set; }
        public string Talonario { get; set; }
        public string Req { get; set; }
        public string Beneficiario { get; set; }
        public string Comprobante { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public string Comprobante_Egreso { get; set; }
        public string Banco { get; set; }
        public string Numero_Cheque { get; set; }
        public double Tipo_Cambio { get; set; }
        public string Observacion_Preliminar { get; set; }
        public string Observacion_Final { get; set; }
        public string Estado { get; set; }
        public string Tipo_Plantilla { get; set; }
        public string Cuentas { get; set; }
    }
}
