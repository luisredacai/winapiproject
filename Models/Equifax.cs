using System;
using System.Collections.Generic;

namespace ApiProject2.Models
{
    public partial class Equifax
    {
        public int Id { get; set; }
        public string Ruc { get; set; }= null!;
        public string RUC20 { get; set; }= null!;
        public string dni { get; set; }= null!;
       // public string ce { get; set; }= null!;
        public DateTime FechaCreacionRuc { get; set; }
        public string Nombre { get; set; } = null!;
        
        public int ScoreCrediticio { get; set; }
        //public string Deuda { get; set; } = null!;
    }
}
