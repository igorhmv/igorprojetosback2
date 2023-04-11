using arquiteturaBase.api.Providers;
using arquiteturaBase.application.AutoMapper;
using arquiteturaBase.ioc;
using HMV.Core.DataAccess;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;

//[assembly: OwinStartup(typeof(arquiteturaBase.api.Startup))]

namespace arquiteturaBase.api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

#if DEBUG
            SwaggerConfig.Register(config);
#endif

            ConfigureCors(app);
            AtivandoAcessTokens(app);
            ConfiguraAmbiente();
            app.UseWebApi(config);
        }

        private void AtivandoAcessTokens(IAppBuilder app)
        {
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new TokensDeAcessoProvider()
            };

            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private void ConfigureCors(IAppBuilder app)
        {
            var corsPolicy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true,
                AllowAnyOrigin = true
            };

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(corsPolicy)
                }
            };
            app.UseCors(corsOptions);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }

        private void ConfiguraAmbiente()
        {
            var config = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SessionManager.ConfigureDataAccess(config, ConfigurationManager.AppSettings["ConfigNHibernate"]);
            IoCWorker.ConfigureWEB();
            AutoMapperConfig.RegisterMappings();
        }
    }
}