using System;

namespace PrototipoData.Models
{
    public class Plantilla_Regalias
    {
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Moneda { get; set; }
        public double Valor_Fijo { get; set; }
        public double Ingresos_Facturados { get; set; }
        public double Ingresos_Cartera { get; set; }
        public double Retencion { get; set; }
        public double Total_Soles { get; set; }
        public double Tasa_Cambio { get; set; }
        public double Total_Dolares { get; set; }
        public string Adjuntos { get; set; }
        public string Cuenta { get; set; }
        public string Soporte { get; set; }
        public string Observaciones { get; set; }
    }
}
