﻿using System;

namespace WebAuditorias.Models
{
    public class AuditoriaGastos
    {
        public Int16 ag_empresa { get; set; }
        public int ag_auditoria { get; set; }
        public Int16 ag_secuencia { get; set; }
        public Int16 ag_tipo { get; set; }
        public DateTime ag_fecha_inicio { get; set; }
        public DateTime ag_fecha_fin { get; set; }
        public double ag_valor { get; set; }
        public string ag_estado { get; set; }
        public string ag_usuario_creacion { get; set; }
        public DateTime ag_fecha_creacion { get; set; }
        public string ag_usuario_actualizacion { get; set; }
        public DateTime ag_fecha_actualizacion { get; set; }
    }
}