using System;

namespace WebAuditorias.Models
{
    public class Oficinas
    {
        public Int16 of_empresa { get; set; }
        public Int16 of_codigo { get; set; }
        public string of_nombre { get; set; }
        public string of_pais { get; set; }
        public string of_provincia { get; set; }
        public string of_ciudad { get; set; }
        public string of_direccion { get; set; }
        public string of_telefono { get; set; }
        public string of_contacto { get; set; }
        public string of_email { get; set; }
        public string of_estado { get; set; }
        public string of_usuario_creacion { get; set; }
        public DateTime of_fecha_creacion { get; set; }
        public string of_usuario_actualizacion { get; set; }
        public DateTime of_fecha_actualizacion { get; set; }
    }
}