using System;

namespace WebAuditorias.Models
{
    public class Perfiles
    {
        public Int16 pe_codigo { get; set; }
        public string pe_descripcion { get; set; }
        public string pe_estado { get; set; }
        public string pe_usuario_creacion { get; set; }
        public DateTime pe_fecha_creacion { get; set; }
        public string pe_usuario_actualizacion { get; set; }
        public DateTime pe_fecha_actualizacion { get; set; }
    }
}