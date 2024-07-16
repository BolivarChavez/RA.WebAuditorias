﻿using System;

namespace PrototipoData.Models
{
    public class Plantilla_Ingresos
    {
        public string Mes { get; set; }
        public string Factura { get; set; }
        public string Cuenta { get; set; }
        public string Detalle { get; set; }
        public string Concepto { get; set; }
        public double Subtotal { get; set; }
        public double Porcentaje { get; set; }
        public double Total { get; set; }
        public DateTime Fecha_Detraccion { get; set; }
        public double Detraccion_Moneda_Destino { get; set; }
        public double Detraccion_Moneda_Base { get; set; }
        public string Comprobante_Ingreso { get; set; }
        public double Neto_Ingreso { get; set; }
        public string Flujo { get; set; }
        public string Estado_Cuenta_1 { get; set; }
        public string Estado_Cuenta_2 { get; set; }
        public string Soporte { get; set; }
    }
}
