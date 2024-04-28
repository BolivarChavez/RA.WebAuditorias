using System;

namespace WebAuditorias.Models
{
    public class Facultades
    {
        public Int16 fa_codigo { get; set; }
        public string fa_descripcion { get; set; }
        public string fa_estado { get; set; }
        public string fa_usuario_creacion { get; set; }
        public DateTime fa_fecha_creacion { get; set; }
        public string fa_usuario_actualizacion { get; set; }
        public DateTime fa_fecha_actualizacion { get; set; }
    }
}