﻿using PrototipoData.Models;

namespace WebAuditorias.Models.Bases
{
    public class Plantilla_Pagos_Base : Plantilla_Pagos
    {
        public int IdRegistro { get; set; }
        public string ReferenciaLinea { get; set; }
        public string IdEstado { get; set; }
    }
}