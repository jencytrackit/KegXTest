Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OpenIdConnect
Imports System.Security.Claims

Namespace TrackITKTS

    Partial Class ADLogin
        Inherits System.Web.UI.Page
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            lblMsg.Text = ""
            If Not Page.IsPostBack Then
                Try
                    If CheckAzureADUser_In_KEGX() = False Then

                        'Session.Abandon()
                        'Session.Contents.Clear()
                        ''FormsAuthentication.SignOut()
                        ''FormsAuthentication.RedirectToLoginPage()
                        'Context.GetOwinContext().Authentication.SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType)


                    End If
                Catch ex As Exception
                    lblMsg.Text = ex.Message.ToString
                    Return
                End Try

                If Request.IsAuthenticated Then
                    If lblMsg.Text.Trim.Length < 1 Then
                        'Label1.Text = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("name").Value
                        Context.GetOwinContext().Authentication.Challenge(New AuthenticationProperties With {
                              .RedirectUri = "Appweb/frmHome.aspx"
                          }, OpenIdConnectAuthenticationDefaults.AuthenticationType)

                    End If
                End If
            End If

        End Sub

        Private Sub hrefLogin_ServerClick(sender As Object, e As EventArgs) Handles hrefLogin.ServerClick
            'Context.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType)

            Session.Abandon()
            Session.Contents.Clear()
            Context.GetOwinContext().Authentication.SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType)

            'FormsAuthentication.SignOut()

            'Dim idpClaim = ClaimsPrincipal.Current.Claims.FirstOrDefault(Function(claim) claim.Type = "idp")

            'If idpClaim IsNot Nothing Then
            '    Context.GetOwinContext().Authentication.SignOut(OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType)
            'End If

        End Sub

        'Private Sub SessionEnd()
        '    If HttpContext.Request.IsAuthenticated Then
        '        Dim userObjectID As String = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value
        '        Dim authContext As AuthenticationContext = New AuthenticationContext(Startup.authority, New NaiveSessionCache(userObjectID))
        '        authContext.TokenCache.Clear()
        '    End If

        '    HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType)
        'End Sub

        Private Function CheckAzureADUser_In_KEGX() As Boolean
            Dim objUser As New PropertyTabMEmployees
            Dim strRole As String = ""
            Dim ADUserName As String
            ADUserName = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value.ToUpper

            Dim objUserDetails As New Hashtable()
            Dim objUserPrivileges As New Hashtable()
            Dim objUserRoleDetails As New Hashtable()

            objUser.UserName = ADUserName
            objUserDetails = clsTabMEmployees.ValidateAzureADUser(ADUserName)

            If objUserDetails("empID") = 0 Then
                lblMsg.Text = objUserDetails("userName")
                Return False
            End If

            If objUserDetails Is Nothing Then
                lblMsg.Text = Resources.lang.valLoginFailed 'For invalid username and password entry show login failed
                Return False
            End If


            Dim userState As String = objUserDetails("disabled")

            Select Case userState
                Case "No"
                    'Get Logged in User Organization,Company,Roles
                    objUserRoleDetails = clsTabMEmployees.GetEmployeeCompanyAndRolesAzureAD(objUser, objUserDetails("schemaName"))
                    If Not objUserRoleDetails Is Nothing Then
                        strRole = objUserRoleDetails("userRoles")
                    End If
                Case "Yes" 'Checking for user disable
                    lblMsg.Text = Resources.lang.valLoginDisabled
                    Return False
                Case "2"   'Checking whether user has role or not
                    lblMsg.Text = Resources.lang.valLoginNoRole
                    Return False
                Case "3"   'Checking for system is down
                    lblMsg.Text = Resources.lang.valLoginSystemDown 'If system is down show system down try after sometime
                    Return False
                Case "4"  'Checking whether another user logged in with same credentials
                    lblMsg.Text = Resources.lang.valLoginLoggedin
                    Return False
            End Select
            If Convert.ToString(objUserDetails("employeeNo")).ToUpper() = "PARTNER" Then
                Session("UserDetails") = objUserDetails 'Store the logged in user details 
                Session("UserRoleDetails") = objUserRoleDetails
                'Response.Redirect("Appweb/frmHome.aspx") 'Transfer to home page for correct credentials
            Else
                If Len(Trim(strRole)) > 0 Then
                    ''FormsAuthentication.Authenticate(txtUserName.Text & "~" & objUserRoleDetails("userRoles"), txtPassword.Text)
                    'FormsAuthentication.Authenticate(ADUserName & "~" & objUserRoleDetails("userRoles"), "nopwd")


                    'Update User Login for logged in credentials
                    Dim objUserLog As New PropertyTabMEmployees
                    objUserLog.EmployeeID = objUserDetails("empID")
                    objUserLog.Logged = "Yes"
                    Dim str As Boolean = clsTabMEmployees.UpdateTabMEmployees_LoggedIn(objUserLog, objUserDetails("schemaName"))
                    objUserLog = Nothing


                    'get user privileges for all the pages based on the logged in user
                    If objUserDetails("isOrganisation") = "1" Then
                        Dim strOrgIds As String = objUserRoleDetails("organizationIDS")
                        Dim OrgIds() As String = Split(strOrgIds, ",")
                        objUserPrivileges = clsTabMEmployees.GetAllTABUserPrivilegesByEmployeeID(objUserRoleDetails("employeeID"), 0, OrgIds(0), objUserDetails("schemaName"))
                    Else
                        Dim strCompanyIds As String = objUserRoleDetails("companyIDS")
                        Dim CompanyIds() As String = Split(strCompanyIds, ",")
                        objUserPrivileges = clsTabMEmployees.GetAllTABUserPrivilegesByEmployeeID(objUserRoleDetails("employeeID"), CompanyIds(0), 0, objUserDetails("schemaName"))
                    End If


                    Session("UserDetails") = objUserDetails 'Store the logged in user details 
                    Session("UserPrivileges") = objUserPrivileges 'Store the logged in user privileges
                    Session("UserRoleDetails") = objUserRoleDetails
                    'Response.Redirect("Appweb/frmHome.aspx") 'Transfer to home page for correct credentials

                Else
                    lblMsg.Text = "Invalid Login..."
                    Return False
                End If
            End If

            Return True
        End Function
    End Class

End Namespace