'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmLogin.aspx
'Created By         :
'File navigation    :
'Created Date       :November 06, 2013, 2:37:41 PM
'Description        :This file is used to login into kegx web application
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmLogin
        Inherits System.Web.UI.Page
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            lblMsg.Text = ""
            txtUserName.Focus()
        
        End Sub

        Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click


            lblMsg.Text = ""

            If Page.IsValid Then
                Dim objUser As New PropertyTabMEmployees
                Dim strRole As String = ""
                Dim objUserDetails As New Hashtable()
                Dim objUserPrivileges As New Hashtable()
                Dim objUserRoleDetails As New Hashtable()

                objUser.UserName = txtUserName.Text
                objUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Trim(txtPassword.Text), "SHA1") 'Encript the password entered for security purpose
                objUserDetails = clsTabMEmployees.chkValidUser(objUser) 'Get The logged in user details based on usernname and password entered
                If objUserDetails Is Nothing Then
                    lblMsg.Text = Resources.lang.valLoginFailed 'For invalid username and password entry show login failed
                    Exit Sub
                End If

                Dim userState As String = objUserDetails("disabled")

                Select Case userState
                    Case "No"
                        'Get Logged in User Organization,Company,Roles
                        objUserRoleDetails = clsTabMEmployees.GetEmployeeCompanyAndRoles(objUser, objUserDetails("schemaName"))
                        If Not objUserRoleDetails Is Nothing Then
                            strRole = objUserRoleDetails("userRoles")
                        End If
                    Case "Yes" 'Checking for user disable
                        lblMsg.Text = Resources.lang.valLoginDisabled
                        Exit Sub
                    Case "2"   'Checking whether user has role or not
                        lblMsg.Text = Resources.lang.valLoginNoRole
                        Exit Sub
                    Case "3"   'Checking for system is down
                        lblMsg.Text = Resources.lang.valLoginSystemDown 'If system is down show system down try after sometime
                        Exit Sub
                    Case "4"  'Checking whether another user logged in with same credentials
                        lblMsg.Text = Resources.lang.valLoginLoggedin
                        Exit Sub
                End Select
                If Convert.ToString(objUserDetails("employeeNo")).ToUpper() = "PARTNER" Then
                    Session("UserDetails") = objUserDetails 'Store the logged in user details 
                    Session("UserRoleDetails") = objUserRoleDetails
                    Response.Redirect("Appweb/frmHome.aspx") 'Transfer to home page for correct credentials
                Else
                    If Len(Trim(strRole)) > 0 Then
                        FormsAuthentication.Authenticate(txtUserName.Text & "~" & objUserRoleDetails("userRoles"), txtPassword.Text)
                        FormsAuthentication.RedirectFromLoginPage(txtUserName.Text & "~" & objUserRoleDetails("userRoles"), False)

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
                        Response.Redirect("Appweb/frmHome.aspx") 'Transfer to home page for correct credentials

                    Else
                        lblMsg.Text = "Invalid Login..."
                    End If
                End If

            End If


        End Sub
    End Class
End Namespace