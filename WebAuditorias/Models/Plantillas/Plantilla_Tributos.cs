using System;

namespace PrototipoData.Models
{
    public class Plantilla_Tributos
    {
        public DateTime Fecha { get; set; }
        public string Periodo { get; set; }
        public string Tributo { get; set; }
        public double Tributo_Resultante { get; set; }
        public double Intereses { get; set; }
        public double Total_Pagar { get; set; }
        public string Forma_Pago { get; set; }
        public string Egreso { get; set; }
        public DateTime Fecha_Informe { get; set; }
        public string Numero_Informe { get; set; }
        public string Observaciones { get; set; }
    }
}
