using arquiteturaBase.application.Dto;
using arquiteturaBase.application.Interfaces.Service;
using HMV.Core.DataAccess;
using NHibernate;
using NHibernate.Transform;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arquiteturaBase.application.Services
{
    public class RelatorioService : IRelatorioService
    {
        public IEnumerable<ItemDto> GetItens(string data="", string cd_atendimento="",string sucesso="")
        {
            StringBuilder qry = new StringBuilder();

            bool temFiltro = false;

            qry.AppendFormat(@"SELECT 
                i.seq_item_enviado,
                i.cd_importacao,
                i.cd_atendimento,
                i.data,
                i.observacao,
                i.sucesso,
                i.xml
                FROM  dbahmv.hmv_drg_item_enviado i");

            if (data != null && data.Length > 0 && cd_atendimento != null && cd_atendimento.Length > 0)
            {
                qry.AppendFormat(@"
                WHERE 
                to_date(i.data,'dd.mm.yyyy') LIKE to_date('{0}','dd.mm.yyyy')
                AND i.cd_atendimento = '{1}'",
                    data,
                    cd_atendimento);
                temFiltro = true;
            }
            else if (data != null && data.Length > 0)
            {
                qry.AppendFormat(@"
                WHERE
                to_date(i.data,'dd.mm.yyyy') LIKE to_date('{0}','dd.mm.yyyy')",
                    data);
                temFiltro = true;
            }
            else if (cd_atendimento != null && cd_atendimento.Length > 0)
            {
                qry.AppendFormat(@"
                WHERE 
                i.cd_atendimento = '{0}'",
                    cd_atendimento);
                temFiltro = true;
            }

            if (temFiltro) {
                if (sucesso != null && sucesso.Length > 0 && sucesso != "SN") {
                    qry.AppendFormat(@" AND 
                i.sucesso LIKE '{0}'",
                    sucesso);
                }
            }
            else
            {
                if (sucesso != null && sucesso.Length > 0 && sucesso != "SN")
                {
                    qry.AppendFormat(@" WHERE 
                i.sucesso LIKE '{0}'",
                    sucesso);
                }
            }

            IQuery query = ObjectFactory.GetInstance<IUnitOfWork>().CurrentSession.CreateSQLQuery(qry.ToString())
                .AddScalar("seq_item_enviado",NHibernateUtil.String)
                .AddScalar("cd_importacao", NHibernateUtil.String)
                .AddScalar("cd_atendimento", NHibernateUtil.String)
                .AddScalar("data", NHibernateUtil.String)
                .AddScalar("observacao", NHibernateUtil.String)
                .AddScalar("sucesso", NHibernateUtil.String)
                .AddScalar("xml", NHibernateUtil.String);

            query.SetResultTransformer(Transformers.AliasToBean(typeof(ItemDto)));
            return query.List<ItemDto>();
        }
    }
}
