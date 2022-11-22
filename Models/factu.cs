using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiProject2.Models
{
    public partial class factu
    {   
        public int ide_sco  { get; set; }

        [Key]
        public  string dni   { get; set; } = null!;
        public string id_pedido   { get; set; } = null!;
        public string nrorecibos { get; set; } = null!;
        public string pagos_al_dia { get; set; } = null!;
        // public int dias_retiro { get; set; }
        public int score { get; set; }
        public string estado   { get; set; } = null!;
        public int ca { get; set; }
        public string nrorecibos_emi  { get; set; } = null!;
        public char codigocliente { get; set; }
    }
}
