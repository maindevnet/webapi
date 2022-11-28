using Microsoft.Owin;
using Owin;
using QlKS.WebApi.Authentications;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(QlKS.WebApi.Startup))]

namespace QlKS.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var provider = new AuthenticationServeProvider();

            OAuthAuthorizationServerOptions serverOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = provider,
            };
            app.UseOAuthAuthorizationServer(serverOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
        }
    }
}
