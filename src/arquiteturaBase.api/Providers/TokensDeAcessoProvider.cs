using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Threading.Tasks;

namespace arquiteturaBase.api.Providers
{
    public class TokensDeAcessoProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                string user = context.UserName.ToUpper();
                string password = context.Password;

                //IGenericSecurityService gerericSecurity = ObjectFactory.GetInstance<IGenericSecurityService>();
                //gerericSecurity.ValidaSenhaDescriptografada(user, password);
                //Usuarios usuario = ObjectFactory.GetInstance<IUsuariosService>().FiltraPorID(user);

                //if (usuario == null)
                //{
                //    context.SetError("invalid_grant", "Usuário e/ou Senha Incorretos.");
                //    return;
                //}

                //var props = new AuthenticationProperties(new Dictionary<string, string>
                //{
                //    {
                //        "username", usuario.cd_usuario
                //    },
                //    {
                //        "name", usuario.Nome
                //    }
                //});

                //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                //var identidadeUsuario = new AuthenticationTicket(identity, props);
                //identidadeUsuario.Identity.AddClaim(new Claim("usuario_id", usuario.ID));
                //context.Validated(identidadeUsuario);
            }
            catch
            {
                context.SetError("Dados inválidos", "Usuário ou Senha estão incorretos.");
                return;
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var item in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(item.Key, item.Value);
            }

            var clains = context.Identity.Claims
                .GroupBy(x => x.Type)
                .Select(y => new { Clain = y.Key, Value = y.Select(z => z.Value).ToArray() });

            return base.TokenEndpoint(context);
        }
    }
}