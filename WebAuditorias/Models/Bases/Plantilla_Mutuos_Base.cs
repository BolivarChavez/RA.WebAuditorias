using PrototipoData.Models;

namespace WebAuditorias.Models.Bases
{
    public class Plantilla_Mutuos_Base : Plantilla_Mutuos
    {
        public int IdRegistro { get; set; }
        public string ReferenciaLinea { get; set; }
        public string IdEstado { get; set; }
    }
}