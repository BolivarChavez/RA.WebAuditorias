using System;

namespace PrototipoData.Models
{
    public class Plantilla_Reembolsos
    {
        public string Documento { get; set; }
        public DateTime Fecha_Documento { get; set; }
        public string Soporte { get; set; }
        public double Valor_Total { get; set; }
        public string Moneda { get; set; }
        public string Estado { get; set; }
        public string Numero_Cheque { get; set; }
        public string Adjuntos { get; set; }
        public string Observaciones { get; set; }
    }
}
