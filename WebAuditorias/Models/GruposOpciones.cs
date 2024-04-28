using System;

namespace WebAuditorias.Models
{
    public class GruposOpciones
    {
        public Int16 go_codigo { get; set; }
        public Int16 go_orden { get; set; }
        public string go_descripcion { get; set; }
        public string go_estado { get; set; }
        public string go_usuario_creacion { get; set; }
        public DateTime go_fecha_greacion { get; set; }
        public string go_usuario_actualizacion { get; set; }
        public DateTime go_fecha_actualizacion { get; set; }
    }
}