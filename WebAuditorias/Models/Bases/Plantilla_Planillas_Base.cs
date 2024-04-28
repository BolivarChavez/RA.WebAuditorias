using PrototipoData.Models;

namespace WebAuditorias.Models.Bases
{
    public class Plantilla_Planillas_Base : Plantilla_Planillas
    {
        public int IdRegistro { get; set; }
        public string ReferenciaLinea { get; set; }
        public string IdEstado { get; set; }
    }
}