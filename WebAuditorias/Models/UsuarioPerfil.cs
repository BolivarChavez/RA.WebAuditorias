using System;

namespace WebAuditorias.Models
{
    public class UsuarioPerfil
    {
        public Int16 up_perfil { get; set; }
        public int up_usuario { get; set; }
        public string up_estado { get; set; }
        public string up_usuario_creacion { get; set; }
        public DateTime up_fecha_creacion { get; set; }
        public string up_usuario_actualizacion { get; set; }
        public DateTime up_fecha_actualizacion { get; set; }
    }
}