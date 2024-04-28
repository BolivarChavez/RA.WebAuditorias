using System;

namespace WebAuditorias.Models
{
    public class UsuarioOficina
    {
        public int uo_usuario { get; set; }
        public Int16 uo_empresa { get; set; }
        public Int16 uo_oficina { get; set; }
        public string uo_estado { get; set; }
        public string uo_usuario_creacion { get; set; }
        public DateTime uo_fecha_creacion { get; set; }
        public string uo_usuario_actualizacion { get; set; }
        public DateTime uo_fecha_actualizacion { get; set; }
    }
}