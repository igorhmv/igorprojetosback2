using System.Web.Http;
using WebActivatorEx;
using arquiteturaBase.api;
using Swashbuckle.Application;
using arquiteturaBase.api.Filters;

//[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace arquiteturaBase.api
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.DocumentFilter<SwaggerAuthTokenOperationFilter>();
                c.SingleApiVersion("V1", "IGOR API 2");
                c.PrettyPrint();
                //c.IncludeXmlComments(GetXmlCommentsPath());
                c.DescribeAllEnumsAsStrings();
                c.OperationFilter<SwaggerAuthorizationHeaderFilter>();
            }).EnableSwaggerUi(c =>
            {
                c.DocumentTitle("IGOR API 2");
                c.DisableValidator();
            });

            SwaggerConfig.MapRoutes(config);
        }

        private static string GetXmlCommentsPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + @"\bin\gmex.Api.XML";
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "swagger_root",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger")
            );
        }
    }
}
