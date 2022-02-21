Imports Microsoft.Owin
Imports Owin
Imports Microsoft.IdentityModel.Protocols.OpenIdConnect
Imports Microsoft.IdentityModel.Tokens
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OpenIdConnect
Imports Microsoft.Owin.Security.Notifications
Imports System.Threading.Tasks
Imports Microsoft.Owin.Extensions


<Assembly: OwinStartupAttribute(GetType(KEG_X.Startup))>
Public Class Startup
    Private clientId As String = System.Configuration.ConfigurationManager.AppSettings("ClientId")
    Private redirectUri As String = System.Configuration.ConfigurationManager.AppSettings("RedirectUri")
    Shared tenant As String = System.Configuration.ConfigurationManager.AppSettings("Tenant")
    Private authority As String = String.Format(System.Globalization.CultureInfo.InvariantCulture, System.Configuration.ConfigurationManager.AppSettings("Authority"), tenant)

    Public Sub Configuration(ByVal app As IAppBuilder)
        app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType)
        app.UseCookieAuthentication(New CookieAuthenticationOptions())
        app.UseOpenIdConnectAuthentication(New OpenIdConnectAuthenticationOptions With {
            .ClientId = clientId,
            .Authority = authority,
            .RedirectUri = redirectUri,
            .PostLogoutRedirectUri = redirectUri,
            .Scope = OpenIdConnectScope.OpenIdProfile,
            .ResponseType = OpenIdConnectResponseType.IdToken,
            .TokenValidationParameters = New TokenValidationParameters() With {
                .ValidateIssuer = False
            },
            .Notifications = New OpenIdConnectAuthenticationNotifications With {
                .AuthenticationFailed = AddressOf OnAuthenticationFailed
            }
        })
        app.UseStageMarker(PipelineStage.Authenticate)

    End Sub

    Private Function OnAuthenticationFailed(ByVal context As AuthenticationFailedNotification(Of OpenIdConnectMessage, OpenIdConnectAuthenticationOptions)) As Task
        context.HandleResponse()
        'context.Response.Redirect("/?errormessage=" & context.Exception.Message)
        context.Response.Redirect("AppWeb/GenericErrorPage.aspx?ermsg=" & context.Exception.Message)
        'AppWeb/GenericErrorPage.aspx
        Return Task.FromResult(0)
    End Function

    'Public Sub ConfigureAuth(ByVal app As IAppBuilder)
    '    app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType)
    '    app.UseCookieAuthentication(New CookieAuthenticationOptions())
    '    app.UseOpenIdConnectAuthentication(New OpenIdConnectAuthenticationOptions With {
    '        .ClientId = clientId,
    '        .Authority = authority,
    '        .PostLogoutRedirectUri = postLogoutRedirectUri,
    '        .Notifications = New OpenIdConnectAuthenticationNotifications() With {
    '            .AuthenticationFailed = Function(context) System.Threading.Tasks.Task.FromResult(0)
    '        }
    '    })
    '    app.UseStageMarker(PipelineStage.Authenticate)
    'End Sub
End Class

