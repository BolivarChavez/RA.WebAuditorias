using PrototipoData.Models;

namespace WebAuditorias.Models.Bases
{
    public class Plantilla_Regalias_Base : Plantilla_Regalias
    {
        public int IdRegistro { get; set; }
        public string ReferenciaLinea { get; set; }
        public string IdEstado { get; set; }
        public string ReferenciaDocumento { get; set; }
    }
}