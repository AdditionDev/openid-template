using System.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.ActiveDirectory;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using OpenIdPoc.Web.Start;
using Owin;

[assembly: OwinStartup("OwinConfig", typeof(Startup))]

namespace OpenIdPoc.Web.Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = "2cb886e3-0931-4fcf-a271-8ca0e52417a0",
                    Authority = "https://login.windows.net/addition.dk",
                    RedirectUri = "http://localhost:57515/"
                }
            );

            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    Tenant = "addition.dk",
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = "https://addition.dk/3c28f7b3-8d16-4fcd-bd3f-cc9037ce11ea"
                    },
                    AuthenticationType = "OAuth2Bearer"
                });
        }
    }
}