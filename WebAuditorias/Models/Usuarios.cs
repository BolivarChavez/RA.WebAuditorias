using System;

namespace WebAuditorias.Models
{
    public class Usuarios
    {
        public int us_codigo { get; set; }
        public string us_nombre { get; set; }
        public string us_login { get; set; }
        public string us_password { get; set; }
        public string us_email { get; set; }
        public int us_ingresos { get; set; }
        public DateTime us_ultimo_ingreso { get; set; }
        public string us_estado { get; set; }
        public string us_usuario_creacion { get; set; }
        public DateTime us_fecha_creacion { get; set; }
        public string us_usuario_actualizacion { get; set; }
        public DateTime us_fecha_actualizacion { get; set; }
    }
}