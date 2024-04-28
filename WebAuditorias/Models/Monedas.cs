﻿using System;

namespace WebAuditorias.Models
{
    public class Monedas
    {
        public Int16 mo_codigo { get; set; }
        public string mo_descripcion { get; set; }
        public string mo_estado { get; set; }
        public string mo_usuario_creacion { get; set; }
        public DateTime mo_fecha_creacion { get; set; }
        public string mo_usuario_actualizacion { get; set; }
        public DateTime mo_fecha_actualizacion { get; set; }
    }
}