﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBotica.SolPedido.Entidades.Core
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string CodUsuario { get; set; }
        public byte[] Clave { get; set; }
        public string Nombres { get; set; }

        // Para entrada desde formularios (no se guarda tal cual en BD)
        public string ClaveTexto { get; set; }
    }
}
