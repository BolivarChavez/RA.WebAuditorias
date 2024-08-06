using PrototipoData.Models;

namespace WebAuditorias.Models.Bases
{
    public class Plantilla_Reembolsos_Base : Plantilla_Reembolsos
    {
        public int IdRegistro { get; set; }
        public string ReferenciaLinea { get; set; }
        public string IdEstado { get; set; }
        public string ReferenciaDocumento { get; set; }
    }
}