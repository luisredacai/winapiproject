using System;
using System.Collections.Generic;

namespace ApiProject2.Models
{
    public partial class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Puesto { get; set; } = null!;
        public double Sueldo { get; set; }
    }
}
