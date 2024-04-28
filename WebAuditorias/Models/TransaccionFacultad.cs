using System;

namespace WebAuditorias.Models
{
    public class TransaccionFacultad
    {
        public int tf_transaccion { get; set; }
        public Int16 tf_facultad { get; set; }
        public string tf_estado { get; set; }
        public string tf_usuario_creacion { get; set; }
        public DateTime tf_fecha_creacion { get; set; }
        public string tf_usuario_actualizacion { get; set; }
        public DateTime tf_fecha_actualizacion { get; set; }
    }
}