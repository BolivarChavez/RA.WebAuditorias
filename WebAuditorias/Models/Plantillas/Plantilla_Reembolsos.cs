using System;

namespace PrototipoData.Models
{
    public class Plantilla_Reembolsos
    {
        public string Codigo { get; set; }
        public DateTime Fecha_Documento { get; set; }
        public string Referencia { get; set; }
        public double Valor_Moneda_Destino { get; set; }
        public double Valor_Tasa_Cambio { get; set; }
        public double Valor_Moneda_Base { get; set; }
        public string Estado { get; set; }
        public string Numero_Cheque { get; set; }
        public string Adjuntos { get; set; }
    }
}
