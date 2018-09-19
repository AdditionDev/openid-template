OpenIDTemplate


This repository have been moved to [VSTS](https://additionglobal.visualstudio.com/DefaultCollection/ADD-Internal/_git/OpenIdTemplate)



## Setup notes

Started from scratch 

### Packages

Added Microsoft.Net.Mvc  
Added Microsoft.Owin.Security.OpenIdConnect  
Added Microsoft.Owin.Security.ActiveDirectory  
Added Microsoft.Owin.Host.SystemWeb  


### Azure AD steps

Registered app in Azure AD

### MVC steps

Added `/Global.asax` and set up routing - for ordinary routing  
Added `/Start/WebApiConfig.cs` and configured api routing in there, calling it from Global.asax

### Owin steps

Added `/Start/Startup.cs` for use as OWIN startup class  
Added OWIN startup to web.config

````xml
    <appSettings>
        <add key="owin:appStartup" value="OwinConfig" />
        <add key="owin:AutomaticAppStartup" value="true" />
    </appSettings>
````

Configured security for page routes with

````c#
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
````

- ClientId is Azure AD Apps Application ID
- Authority AD login url - usually "https://login.windows.net/" + name of primary domain name in domain name list

Configured security for api routes with

````c#
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
````

 - Tenant is name of primary domain name
 - TokenValidationParameters.ValidAudience is App ID URI found under "Properties" for the registered app.
