'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmAddEditRoles.aspx
'Created By         :
'File navigation    :
'Created Date       :November 07, 2013, 5:33:45 PM 
'Description        :This file is used to Add / Edit the records...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmAddEditRoles
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Private mid As String = ""
        Dim objuserrole As New Hashtable()
        
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
            rcbFromCmpny.Enabled = True

            If Not Request.QueryString("id") Is Nothing Then
                mid = Request.QueryString("id")
            End If
            If Not Page.IsPostBack Then
                BindCompany()
                If Not Request.QueryString("id") Is Nothing Then
                    'Edit Mode
                    mid = Request.QueryString("id") 'Get the RoleID and store it in a variable
                    Dim objRole As PropertyTabRole = clsTabRole.GetTabRoleByID(mid, objuserInfo("schemaName")) 'Get the role details by RoleID



                    rcbFromCmpny.SelectedValue = objRole.CompanyID
                    radRoleName.Text = objRole.RoleName

                    rcbFromCmpny.Enabled = False
                    objRole = Nothing
                End If
            End If

            lblMsg.Text = ""
        End Sub
        Private Sub BindCompany()
            rcbFromCmpny.Items.Clear()

            Dim ds As New DataSet
            ds = clsTabMOrganization.GetAllTabMOrganizationByEmployeeID(objuserrole("employeeID"), objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                ' bind From company
                rcbFromCmpny.DataSource = ds
                rcbFromCmpny.DataTextField = "CompanyName"
                rcbFromCmpny.DataValueField = "CompanyID"
                rcbFromCmpny.DataBind()
                ' bind to company

               
            End If

            rcbFromCmpny.Items.Insert(0, New RadComboBoxItem("--Select One--"))


        End Sub
        Private Sub ObjDSTABPrevileges_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjDSTABPrevileges.Selecting
            If Len(radRoleName.Text.Trim) > 0 Then
                e.InputParameters("RoleID") = mid
                e.InputParameters("SchemaName") = objuserInfo("schemaName")
            Else
                e.InputParameters("RoleID") = 0
                e.InputParameters("SchemaName") = objuserInfo("schemaName")
            End If
        End Sub

        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            'Create new instance of the class to store the aspx controls data in DB...
            Dim objTABUserPrivileges As PropertyTABRolePrivileges
            Dim ret As Boolean
            Dim Count As Integer = 0


            
            'Validating the grid if at least one record is selected or not.
            For Each item As GridDataItem In RadgridTABPrevileges.Items
                If Count = 0 Then
                    Dim chkView As CheckBox = DirectCast(item.FindControl("chkView"), CheckBox)
                    Dim chkAdd As CheckBox = DirectCast(item.FindControl("chkAdd"), CheckBox)
                    Dim chkEdit As CheckBox = DirectCast(item.FindControl("chkEdit"), CheckBox)
                    Dim chkDelete As CheckBox = DirectCast(item.FindControl("chkDelete"), CheckBox)
                    If chkView.Checked = True Or chkAdd.Checked = True Or chkEdit.Checked = True Or chkDelete.Checked = True Then
                        Count = Count + 1
                    End If
                Else
                    GoTo SkipGridCheck
                End If
            Next
            If Count = 0 Then
                lblMsg.Text = Resources.lang.rfvRoleselect
                Exit Sub
            End If
SkipGridCheck:

            'validating role name if exists for add or update
            Dim objValidate As New PropertyValidations
            objValidate.Name = "RoleName"
            objValidate.Table = objuserInfo("schemaName") + ".TabRole"
            objValidate.NameVal = radRoleName.Text
            If mid.Length > 0 Then
                objValidate.ID = "RoleID"
                objValidate.IDVal = mid
            End If
            objValidate.FName = "CompanyID"
            objValidate.FNameVal = rcbFromCmpny.SelectedValue

            Dim exists As Boolean = clsValidations.ValidateTable(objValidate, objuserInfo("schemaName"))
            If exists Then
                lblMsg.Text = Resources.lang.rfvRoleNameExist
                Exit Sub
            End If

            'Create new instance of the class to store the aspx controls data in DB...
            Dim objTABRole As PropertyTabRole
            If mid.Length > 0 Then
                objTABRole = clsTabRole.GetTabRoleByID(mid, objuserInfo("schemaName"))
            Else
                objTABRole = New PropertyTabRole
            End If
            objTABRole.RoleName = radRoleName.Text
            objTABRole.EnteredBy = objuserrole("employeeID")
            Dim dt2 As DateTime
            dt2 = DateTime.Parse(DateTime.Today)
            objTABRole.EnteredOn = dt2
            objTABRole.CompanyID = rcbFromCmpny.SelectedValue
            objTABRole.IsOrganization = False
            If mid.Length > 0 Then
                'Edit mode record will be updated based on roleid
                Dim i As Int32 = Integer.Parse(Request.QueryString("id"))
                objTABRole.RoleID = i
                ret = clsTabRole.UpdateTabRole(objTABRole, objuserInfo("schemaName"))
            Else
                'New Role record will be inserted...
                ret = clsTabRole.Save(objTABRole, objuserInfo("schemaName"))
            End If
            'Delete the previoulsy existing roles for roleid
            Dim str As String = clsTABRolePrivileges.DeleteTABRolePrivileges(objTABRole.RoleID, objuserInfo("schemaName"))
            For Each item As GridDataItem In RadgridTABPrevileges.Items
                objTABUserPrivileges = New PropertyTABRolePrivileges
                Dim chkView As CheckBox = DirectCast(item.FindControl("chkView"), CheckBox)
                Dim chkAdd As CheckBox = DirectCast(item.FindControl("chkAdd"), CheckBox)
                Dim chkEdit As CheckBox = DirectCast(item.FindControl("chkEdit"), CheckBox)
                Dim chkDelete As CheckBox = DirectCast(item.FindControl("chkDelete"), CheckBox)
                Dim lblMenuID As Label = DirectCast(item.FindControl("lblgridMenuID"), Label)

                objTABUserPrivileges.AllowView = chkView.Checked
                objTABUserPrivileges.AllowAdd = chkAdd.Checked
                objTABUserPrivileges.AllowEdit = chkEdit.Checked
                objTABUserPrivileges.AllowDelete = chkDelete.Checked
                objTABUserPrivileges.RoleID = objTABRole.RoleID
                objTABUserPrivileges.MenuID = Convert.ToInt32(lblMenuID.Text)
                'New user privilege record will be inserted based on the roleid
                ret = clsTABRolePrivileges.Save(objTABUserPrivileges, objuserInfo("schemaName"))
            Next

            'Audit Trail.
            If (ret) And mid.Length > 0 Then
                Dim objAudit As New PropertyTABAuditTrail

                objAudit.UserID = objuserrole("employeeID")
                objAudit.ActionDate = Now
                objAudit.ActionName = "Edit"
                objAudit.FunctionName = "Modified " & "Role Record" & " : " & radRoleName.Text
                objAudit.TableName = "TabRole"
                objAudit.PrimaryID = objTABRole.RoleID
                Dim suc As String = clsTABAuditTrail.Save(objAudit, objuserInfo("schemaName"))
            ElseIf (ret) Then
                Dim objAudit As New PropertyTABAuditTrail
                objAudit.UserID = objuserrole("employeeID")
                objAudit.ActionDate = Now
                objAudit.ActionName = "Add"
                objAudit.FunctionName = "Created " & "Role Record" & " : " & radRoleName.Text
                objAudit.TableName = "TabRole"
                objAudit.PrimaryID = objTABRole.RoleID
                Dim suc As String = clsTABAuditTrail.Save(objAudit, objuserInfo("schemaName"))
            End If

            If (ret) Then
                'Insert mode and creation is success then redirecting it to List page
                Response.Redirect("frmRoles.aspx?process=Y&mode=A")
            Else
                lblMsg.Text = Resources.lang.msgError
            End If
        End Sub
    End Class
End Namespace