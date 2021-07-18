using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParquedeDiversoes.Models
{
    public class Venda
    {
        [Key]
        private int ID { get; set; }
        public string descricao { get; set; }
        public float preco { get; set; }
        public string tipoVenda { get; set; }
        public DateTime validade { get; set; }
   //     public List<Brinquedo> brinquedos { get; set; }
    }
}
