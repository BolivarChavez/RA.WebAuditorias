using System;

namespace WebAuditorias.Models
{
    public class Transacciones
    {
        public int tr_codigo { get; set; }
        public string tr_descripcion { get; set; }
        public string tr_tipo { get; set; }
        public string tr_estado { get; set; }
        public string tr_usuario_ceacion { get; set; }
        public DateTime tr_fecha_creacion { get; set; }
        public string tr_usuario_actualizacion { get; set; }
        public DateTime tr_fecha_actualizacion { get; set; }
    }
}