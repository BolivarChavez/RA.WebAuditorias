using System;

namespace WebAuditorias.Models
{
    public class Empresas
    {
        public Int16 em_codigo { get; set; }
        public string em_nombre { get; set; }
        public string em_pais { get; set; }
        public string em_provincia { get; set; }
        public string em_ciudad { get; set; }
        public string em_direccion { get; set; }
        public string em_telefono { get; set; }
        public string em_contacto { get; set; }
        public string em_email { get; set; }
        public string em_estado { get; set; }
        public string em_usuario_creacion { get; set; }
        public DateTime em_fecha_creacion { get; set; }
        public string em_usuario_actualizacion { get; set; }
        public DateTime em_fecha_actualizacion { get; set; }
    }
}