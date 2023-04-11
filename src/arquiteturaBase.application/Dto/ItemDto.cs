using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arquiteturaBase.application.Dto
{
   public class ItemDto
    {

       public string seq_item_enviado { get; set; }
       public string cd_importacao { get; set; }
        public string cd_atendimento { get; set; }
        public string data { get; set; }
        public string observacao { get; set; }
        public string sucesso { get; set; }
        public string xml { get; set; }
    }
}
