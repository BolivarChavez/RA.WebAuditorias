using System;

namespace PrototipoData.Models
{
    public class Plantilla_Regularizaciones
    {
        public string Mes { get; set; }
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; }
        public double Monto { get; set; }
        public string Motivo { get; set; }
        public string Banco_Ingreso { get; set; }
        public string Banco_Regularizar { get; set; }
        public string Cuenta { get; set; }
        public string Estado { get; set; }
        public string Soporte { get; set; } 
    }
}
