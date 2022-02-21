
'***************************************************************
'Application :Kegs Tracking System
'File Name : frmChangePassword.aspx
'Created By : 
'File navigation :
'Created Date :  November 06, 2013, 4:50:50 PM
'Description : This form is used to change password details and setting new password
'Modified By :
'Modified Date :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmChangePassword
        Inherits System.Web.UI.Page
        Dim userPrivileges As New Hashtable
        Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            'txtOldPassWord.Enabled = False
            'txtNewPass.Enabled = False
            'txtConfPass.Enabled = False
            'radSave.Enabled = False
            'radClear.Enabled = False
            'Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("ChangeAzureUpwd"))

            objuserInfo = Session("UserDetails")

            lblMsg.Text = ""

            userPrivileges = Session("UserPrivileges")
            implementPrivileges()
            If Not Page.IsPostBack Then

                'if the query string values for process or mode exists then the control is coming from the add or edit page...
                Dim strVal As String = Request.QueryString("process")
                Dim strMOde As String = Request.QueryString("mode")
                Select Case strMOde
                    Case "E"
                        If Trim(strVal) = "Y" Then  '// chekcing for the success or failure of the process
                            lblMsg.Text = Resources.lang.changePassSucc
                        End If
                End Select
            End If
        End Sub

        Private Sub implementPrivileges()
            Dim urlOrg As String = Request.Url.ToString()
            Dim pos As Int32 = urlOrg.LastIndexOf("/")
            urlOrg = urlOrg.Substring(pos, urlOrg.Length - pos)
            pos = urlOrg.IndexOf("?")
            If pos > 0 Then
                urlOrg = urlOrg.Substring(0, pos)
            End If
            urlOrg = System.Configuration.ConfigurationManager.AppSettings("ApplicationURL") & urlOrg

            If userPrivileges.Contains(urlOrg) Then
                Dim priv As String = userPrivileges(urlOrg)
                Dim privileges() As String = priv.Split(",")
                If privileges(2).Equals("0") Then
                    radSave.Visible = False
                End If
            End If
        End Sub

        Private Sub Save(ByVal mode As String)

            If Page.IsValid = False Then
                Return
            End If

            'Create new instance of the class to store the aspx controls data in DB...
            Dim objATSUsers As PropertyTabMEmployees

            objATSUsers = New PropertyTabMEmployees
            objATSUsers.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPass.Text, "SHA1") '// encrypting the password to
            Dim oldPass As String = FormsAuthentication.HashPasswordForStoringInConfigFile(txtOldPassWord.Text, "SHA1") '// encrypting the password to

            objATSUsers.UserName = objuserInfo("userName")

            Dim ret As Boolean

            'mid contains the primary key value, if it has a value then record should be updated...
            ret = clsTabMEmployees.UpdateATSUserPassword(objATSUsers, oldPass, objuserInfo("schemaName"))

            If (ret) Then
                'Insert mode and creation is success then add new record
                objuserInfo("changedby") = objuserInfo("userName")
                Session("UserDetails") = objuserInfo
                Response.Redirect("frmChangePassword.aspx?process=Y&mode=E")
            Else
                lblMsg.Text = Resources.lang.changePassFail
            End If

        End Sub

        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            Save("Save")
        End Sub
    End Class
End Namespace
