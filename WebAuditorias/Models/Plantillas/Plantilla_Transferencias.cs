namespace PrototipoData.Models
{
    public class Plantilla_Transferencias
    {
        public string Item { get; set; }
        public string Proveedor { get; set; }
        public string Concepto { get; set; }
        public string Referencia { get; set; }
        public string Mes { get; set; }
        public double Importe_Monto { get; set; }
        public double Monto { get; set; }
        public double Tipo_Cambio { get; set; }
        public string Comprobante_Pago { get; set; }
        public string Observacion_Preliminar { get; set; }
        public string Observacion_Final { get; set; }
        public string Estado { get; set; }
        public string Tipo_Plantilla { get; set; }
        public string Cuenta { get; set; }
        public string Soporte { get; set; }
    }
}
