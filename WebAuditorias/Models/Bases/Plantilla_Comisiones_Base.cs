using PrototipoData.Models;

namespace WebAuditorias.Models.Bases
{
    public class Plantilla_Comisiones_Base : Plantilla_Comisiones
    {
        public int IdRegistro { get; set; }
        public string ReferenciaLinea { get; set; }
        public string IdEstado { get; set; }
        public string ReferenciaDocumento { get; set; }
    }
}