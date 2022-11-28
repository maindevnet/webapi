using AspNet.Security.OpenIdConnect.Server;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using QLKS.Data.EF;
using QLKS.Service.IService;
using QLKS.Service.Service;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QlKS.WebApi.Authentications
{
    public class AuthenticationServeProvider : OAuthAuthorizationServerProvider
    {
        private readonly DbContext _dbContext = new QLKSEntities();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //Kiem tra dang nhap //
            User user = await _dbContext.Set<User>().
                FirstOrDefaultAsync(x => x.Name == context.UserName && x.Password == context.Password);
            if (user is null)
            {
                context.SetError("invalid_grant", "Tài khoản và mật khẩu không đúng!");
                return;
            }
            else
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "admin"));

                AuthenticationProperties properties = CreateProperties(user);
                AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);

                context.Validated(ticket);
            }
            //return  base.GrantResourceOwnerCredentials(context);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
        public static AuthenticationProperties CreateProperties(User user)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", user.Name },
                { "email", user.Email },
            };
            return new AuthenticationProperties(data);
        }
    }
}