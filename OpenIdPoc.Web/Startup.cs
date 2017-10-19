using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup("OwinConfig", typeof(OpenIdPoc.Web.Startup))]

namespace OpenIdPoc.Web
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
        }
    }
}