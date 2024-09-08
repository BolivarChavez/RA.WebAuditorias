using System;

namespace PrototipoData.Models
{
    public class Plantilla_Mutuos
    {
        public string Codigo { get; set; }
        public string Banco { get; set; }
        public string Moneda { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha_Documento { get; set; }
        public double Monto_Prestamo { get; set; }
        public DateTime Fecha_Pago_Cuota { get; set; }
        public string Numero_Cuota { get; set; }
        public double Valor_Cuota { get; set; }
        public string Comprobante_Pago { get; set; }
        public double Saldo_Pendiente { get; set; }
        public string Cuotas_Pendientes { get; set; }
        public string Documento_Legal { get; set; }
        public string Observacion { get; set; }
    }
}
