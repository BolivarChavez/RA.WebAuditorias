using PrototipoData.Models;

namespace WebAuditorias.Models.Bases
{
    public class Plantilla_Ingresos_Base : Plantilla_Ingresos
    {
        public int IdRegistro { get; set; }
        public string ReferenciaLinea { get; set; }
        public string IdEstado { get; set; }
    }
}