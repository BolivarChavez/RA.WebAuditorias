using System;

namespace PrototipoData.Models
{
    public class Plantilla_Pagos
    {
        public string Periodo { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public double Importe_Bruto { get; set; }
        public double Descuentos { get; set; }
        public double Neto_Pagar { get; set; }
        public double Transferencia { get; set; }
        public double Cheque { get; set; }
        public double Diferencia { get; set; }
        public string Numero_Cheque { get; set; }
        public string Numero_Informe { get; set; }
        public string Observaciones { get; set; }
    }
}
