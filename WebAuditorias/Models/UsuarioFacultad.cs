using System;

namespace WebAuditorias.Models
{
    public class UsuarioFacultad
    {
        public int uf_usuario { get; set; }
        public int uf_transaccion { get; set; }
        public Int16 uf_facultad { get; set; }
        public string uf_estado { get; set; }
        public string uf_usuario_creacion { get; set; }
        public DateTime uf_fecha_creacion { get; set; }
        public string uf_usuario_actualizacion { get; set; }
        public DateTime uf_fecha_actualizacion { get; set; }
    }
}