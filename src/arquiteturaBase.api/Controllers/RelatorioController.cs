using arquiteturaBase.application.Dto;
using arquiteturaBase.application.Interfaces.Service;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace arquiteturaBase.api.Controllers
{
    public class RelatorioController : ApiController
    {
        

        [HttpGet,Route("api/relatorio/item")]
        public Task<HttpResponseMessage> GetRelatorio(string data="", string cd_atendimento="",string sucesso="")
        {
            try
            {
                IRelatorioService service = ObjectFactory.GetInstance<IRelatorioService>();
                IEnumerable<ItemDto> itens = service.GetItens(data,cd_atendimento,sucesso);
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.OK,itens));
            }
            catch (Exception error)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.InternalServerError, error.Message));
            }

        }
    }
}
