using System;

namespace PrototipoData.Models
{
    public class Plantilla_Regalias
    {
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public double Valor_Fijo { get; set; }
        public double Valor_Proporcional { get; set; }
        public double Porcentaje { get; set; }
        public double Subtotal { get; set; }
        public double Tasa_Cambio { get; set; }
        public double Total { get; set; }
        public string Adjuntos { get; set; }
        public string Cuenta { get; set; }
    }
}
