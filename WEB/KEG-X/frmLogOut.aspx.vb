Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OpenIdConnect

Public Class frmLogOut
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Context.GetOwinContext().Authentication.SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType)

        Session.Abandon()
        Session.Contents.Clear()
        FormsAuthentication.SignOut()
        FormsAuthentication.RedirectToLoginPage()
        'Session("SelectedCompanyID") = Nothing
        'Response.Redirect("frmLogin.aspx")

    End Sub

End Class