using PrototipoData.Models;

namespace WebAuditorias.Models.Bases
{
    public class Plantilla_Regularizaciones_Base : Plantilla_Regularizaciones
    {
        public int IdRegistro { get; set; }
        public string ReferenciaLinea { get; set; }
        public string IdEstado { get; set; }
        public string ReferenciaDocumento { get; set; }
    }
}