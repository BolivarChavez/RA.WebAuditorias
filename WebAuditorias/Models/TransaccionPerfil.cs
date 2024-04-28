using System;

namespace WebAuditorias.Models
{
    public class TransaccionPerfil
    {
        public Int16 tp_perfil { get; set; }
        public int tp_transaccion { get; set; }
        public Int16 tp_grupo_opcion { get; set; }
        public string tp_estado { get; set; }
        public string tp_usuario_creacion { get; set; }
        public DateTime tp_fecha_creacion { get; set; }
        public string tp_usuario_actualizacion { get; set; }
        public DateTime tr_fecha_actualizacion { get; set; }
    }
}