using System;

namespace PrototipoData.Models
{
    public class Plantilla_Mutuos
    {
        public string Codigo { get; set; }
        public DateTime Fecha_Documento { get; set; }
        public DateTime Fecha_Inicio_Pago { get; set; }
        public double Monto_Prestamo { get; set; }
        public double Valor_Cuota { get; set; }
        public double Total_Cancelado { get; set; }
        public double Saldo_Pendiente { get; set; }
        public string Cuotas_Pendientes { get; set; }
        public string Contrato_Adjunto { get; set; }
        public string Comprobante_Pago { get; set; }
        public string Cuenta { get; set; }
    }
}
