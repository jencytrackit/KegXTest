
'***************************************************************
'Application :Kegs Tracking System
'File Name : frmMyProfile.aspx
'Created By : 
'File navigation :
'Created Date :  November 06, 2013, 4:50:35 PM
'Description : This is used to show loggedIn User details 
'Modified By :
'Modified Date :
'***************************************************************

Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmMyProfile
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging
        Private mid As String = ""
        Dim objuserrole As New Hashtable()
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// UserDetails values assigned to objuserInfo
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If

            mid = Integer.Parse(objuserrole("employeeID"))

            If Not Page.IsPostBack Then
                '
                Dim i As Int32 = Integer.Parse(objuserrole("employeeID"))
                EditMode(i)

                'if the query string values for process or mode exists then the control is coming from the add or edit page...
                Dim strVal As String = Request.QueryString("process")
                Dim strMOde As String = Request.QueryString("mode")
                Select Case strMOde
                    Case "E"
                        If Trim(strVal) = "Y" Then  '// chekcing for the success or failure of the process
                            lblMsg.Text = Resources.lang.Updated
                        End If
                    Case "A"
                        If Trim(strVal) = "Y" Then  '// chekcing for the success or failure of the process
                            lblMsg.Text = Resources.lang.Inserted
                        End If
                End Select
            End If
        End Sub

        Private Sub EditMode(ByVal EmployeeID As Int32)
            'Get the record from database by calling BL Layer based on the primary key value passed

            Dim objTabMEmployees As PropertyTabMEmployees = clsTabMEmployees.GetTabMEmployeesByID(EmployeeID, objuserInfo("schemaName"))

            'assign the values to controls from class by calling getter methods...
            If Not (objTabMEmployees Is Nothing) Then
                txtEmployeeName.Text = objTabMEmployees.EmployeeName
                txtEmployeeNo.Text = objTabMEmployees.EmployeeNo
                txtPosition.Text = objTabMEmployees.Position
                txtPhone.Text = objTabMEmployees.Phone
                txtEmail.Text = objTabMEmployees.Email
                txtComments.Text = objTabMEmployees.Comments
                lblLabel1.Text = ""

                'Dim ObjRoles As PropertyTabRole = clsTabRole.GetTabRoleByID(objTabMEmployees.RoleID, objuserInfo("schemaName"))
                'lblLabel1.Text = ObjRoles.RoleName

                Dim CompDet As DataSet = clsTabMOrganization.GetAllTabMOrganization(objuserInfo("userName"), objuserInfo("schemaName"))
                lstBox.DataSource = CompDet
                lstBox.DataTextField = "CompanyName"
                lstBox.DataValueField = "CompanyID"
                lstBox.DataBind()
                For Each item As RadListBoxItem In lstBox.Items
                    item.Checked = True
                Next
            End If
        End Sub
        Private Sub Save(ByVal mode As String)
            If Page.IsValid = False Then
                Return
            End If

            ''validating Employee name if exists for add or update
            'Dim objValidate As New cls
            'objValidate.Name = "EmployeeName"
            'objValidate.Table = "ATSEmployees"
            'objValidate.NameVal = txtEmployeeName.Text
            'If mid.Length > 0 Then
            '    objValidate.ID = "EmployeeID"
            '    objValidate.IDVal = mid
            'End If


            'Create new instance of the class to store the aspx controls data...
            Dim objTabMEmployees As PropertyTabMEmployees
            If mid.Length > 0 Then
                objTabMEmployees = clsTabMEmployees.GetTabMEmployeesByID(mid, objuserInfo("schemaName"))
            Else
                objTabMEmployees = New PropertyTabMEmployees
            End If

            objTabMEmployees.EmployeeName = Trim(txtEmployeeName.Text)
            objTabMEmployees.EmployeeNo = Trim(txtEmployeeNo.Text)
            objTabMEmployees.Position = Trim(txtPosition.Text)
            objTabMEmployees.Phone = Trim(txtPhone.Text)
            objTabMEmployees.Email = Trim(txtEmail.Text)
            objTabMEmployees.Comments = Trim(txtComments.Text)

            Dim ret As Boolean

            'mid contains the primary key value, if it has a value then record should be updated...
            If mid.Length > 0 Then
                objTabMEmployees.EmployeeID = mid
                ret = clsTabMEmployees.UpdateTabMEmployees(objTabMEmployees, objuserInfo("schemaName"))
            Else
                'New record will be inserted...
                ret = clsTabMEmployees.Save(objTabMEmployees, objuserInfo("schemaName"))
            End If


            If (ret) And mid.Length > 0 And mode = "Save" Then
                'Update mode and updation is success and goto List page
                Response.Redirect("frmMyProfile.aspx?process=Y&mode=E")
            Else
                lblMsg.Text = Resources.lang.msgError
            End If

        End Sub
        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            Save("Save")
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Response.Redirect("frmEmployeeList.aspx")
        End Sub
    End Class
End Namespace
