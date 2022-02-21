Imports System.Web.Security
Imports System.Security.Principal
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OpenIdConnect

Namespace TrackITKTS
    Partial Class chkUserpresent
        Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ''Put user code to initialize the page here
            'If Session.SessionID = "" Then
            '    Session.Abandon() '// end the session 
            '    Session.Contents.Clear() '// clears all the session contents
            '    FormsAuthentication.SignOut() '// Clears all the authenticated values if present.
            '    Response.Redirect("../frmLogin.aspx")
            'Else
            '    Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging
            '    objuserInfo = Session("UserDetails")
            '    Dim userPrivileges As New Hashtable()
            '    userPrivileges = Session("UserPrivileges")
            '    If Not objuserInfo Is Nothing Or Not userPrivileges Is Nothing Then
            '        'commented for access privileges verification...
            '        'If Len(Trim(objuserInfo("changedby"))) < 1 Then
            '        '    Response.Redirect("frmChangePassword.aspx")
            '        'End If
            '    Else
            '        Session.Abandon() '// end the session 
            '        Session.Contents.Clear() '// clears all the session contents
            '        FormsAuthentication.SignOut() '// Clears all the authenticated values if present.
            '        Response.Redirect("../frmLogin.aspx")
            '    End If
            'End If
            If Request.IsAuthenticated Then
                Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging
                objuserInfo = Session("UserDetails")
                Dim userPrivileges As New Hashtable()
                userPrivileges = Session("UserPrivileges")
                If Not objuserInfo Is Nothing Or Not userPrivileges Is Nothing Then

                Else
                    Session.Abandon() '// end the session 
                    Session.Contents.Clear() '// clears all the session contents
                    Context.GetOwinContext().Authentication.SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType)
                    FormsAuthentication.SignOut() '// Clears all the authenticated values if present.
                    Response.Redirect("../ADLogin.aspx")
                End If
            Else
                Session.Abandon() '// end the session 
                Session.Contents.Clear() '// clears all the session contents
                Context.GetOwinContext().Authentication.SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType)
                FormsAuthentication.SignOut() '// Clears all the authenticated values if present.
                Response.Redirect("../ADLogin.aspx")
            End If

        End Sub
    End Class

End Namespace
