using PrototipoData.Models;

namespace WebAuditorias.Models.Bases
{
    public class Plantilla_Tributos_Base : Plantilla_Tributos
    {
        public int IdRegistro { get; set; }
        public string ReferenciaLinea { get; set; }
        public string IdEstado { get; set; }
        public string ReferenciaDocumento { get; set; }
    }
}