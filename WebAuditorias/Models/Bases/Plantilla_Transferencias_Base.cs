using PrototipoData.Models;

namespace WebAuditorias.Models.Bases
{
    public class Plantilla_Transferencias_Base : Plantilla_Transferencias
    {
        public int IdRegistro { get; set; }
        public string ReferenciaLinea { get; set; }
        public string IdEstado { get; set; }
    }
}