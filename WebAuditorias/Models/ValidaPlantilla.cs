using System.Collections.Generic;

namespace WebAuditorias.Models
{
    public class ValidaPlantilla
    {
        public int Linea { get; set; }
        public List<CampoPlantilla> Campos { get; set; }
    }

    public class CampoPlantilla
    {
        public string Campo { get; set; }
        public string Mensaje { get; set; }
    }
}