using System;

namespace PrototipoData.Models
{
    public class Plantilla_Planillas
    {
        public string Mes { get; set; }
        public DateTime Fecha_Pago_Cash { get; set; }
        public string Lote { get; set; }
        public double Remuneracion_Cash { get; set; }
        public double Remuneracion_Cheque { get; set; }
        public double Remuneracion_Total { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public double Honorarios_Planilla { get; set; }
        public double Honorarios_Incentivos { get; set; }
        public double Honorarios_Total { get; set; }
        public double Pagado { get; set; }
        public double Honorarios_Cesantes { get; set; }
        public double Diferencia { get; set; }
        public DateTime Fecha_Pago_Gratificacion { get; set; }
        public double Gratificaciones { get; set; }
        public string Numero_Informe { get; set; }
        public string Observaciones { get; set; }
    }
}
