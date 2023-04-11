using arquiteturaBase.application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arquiteturaBase.application.Interfaces.Service
{
    public interface IRelatorioService
    {
        IEnumerable<ItemDto> GetItens(string data,string cd_atendimento,string sucesso);
    }
}
