using System;

namespace PrototipoData.Models
{
    public class Plantilla_Comisiones
    {
        public string Mes { get; set; }
        public double Monto_Recuperado { get; set; }
        public double Monto_Planilla { get; set; }
        public double Monto_Honorarios { get; set; }
        public double Total_Incentivos { get; set; }
        public double Cheque_Girado { get; set; }
        public double Pagado { get; set; }
        public string Entregado_Caja_Interna_1 { get; set; }
        public double No_Girado { get; set; }
        public DateTime Fecha_Informe { get; set; }
        public DateTime Fecha_Contabilidad { get; set; }
        public string Informe_Comisiones { get; set; }
        public string Entregado_Caja_Interna_2 { get; set; }
        public string Observaciones { get; set; }
    }
}
