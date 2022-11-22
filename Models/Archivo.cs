using System;
using System.Collections.Generic;

namespace ApiProject2.Models
{
    public partial class Archivo
    {
        public int IdAr { get; set; }
        public string Nombre { get; set; } = null!;
        public string ExtencionAr { get; set; } = null!;
        public double Tamanio { get; set; }
        public string Ubicacion { get; set; } = null!;
    }
}
