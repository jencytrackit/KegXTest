Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmAddEditEmployees
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
            empimgpos.Attributes.Add("style", "position: relative; display: inline-block; padding-left: 212px; margin-top: -48px;")
            lblMsg.Text = ""
            If Not Request.QueryString("id") Is Nothing Then
                mid = Request.QueryString("id")
            End If
            If Not IsPostBack Then
                If Convert.ToString(objuserInfo("userName")).ToUpper() = "SUPERADMIN" Then
                    BindOrganization()
                    trOrganization.Visible = True
                    trCompanyAssociation.Visible = False
                Else
                    trOrganization.Visible = False
                    trCompanyAssociation.Visible = True
                    BindCompanyAssociation()
                    BindRoles()
                End If
                If Not Request.QueryString("id") Is Nothing Then
                    EditMode(mid)
                    rfvPassword.Enabled = False
                    rfvConfirmPassword.Enabled = False
                Else
                    rfvPassword.Enabled = True
                    rfvConfirmPassword.Enabled = True
                End If

            End If
        End Sub
        Private Sub EditMode(ByVal mid As String)
            Dim objTabMEmployees As PropertyTabMEmployees = clsTabMEmployees.GetAllTabMEmployeesByUserName(mid, objuserInfo("schemaName"))
            radEmployeeName.Text = objTabMEmployees.EmployeeName
            radEmployeeNo.Text = objTabMEmployees.EmployeeNo
            radPosition.Text = objTabMEmployees.Position
            radPhone.Text = objTabMEmployees.Phone
            radEmail.Text = objTabMEmployees.Email
            RadComments.Text = objTabMEmployees.Comments
            radUserName.Text = objTabMEmployees.UserName
            radUserName.Enabled = False
            radPassword.Text = objTabMEmployees.Password
            If objTabMEmployees.Disable.ToUpper() = "YES" Then
                rbtActive.SelectedValue = 1
            Else
                rbtActive.SelectedValue = 0
            End If

            radMobile.Text = objTabMEmployees.Mobile
            If Not objTabMEmployees.EmpPhoto Is Nothing Then
                RadBinaryImage1.Attributes.Add("Style", "visibility: visible")
                RadBinaryImage1.Visible = True
                RadBinaryImage1.DataValue = objTabMEmployees.EmpPhoto
                lblImageValue.Text = Convert.ToBase64String(objTabMEmployees.EmpPhoto)
            End If
            objTabMEmployees = Nothing
            Dim ds = clsTabMCompEmp.GetAllTabMCompEmpOrgByEmployeeID(mid, objuserInfo("schemaName"))
            If Convert.ToString(objuserInfo("userName")).ToUpper() = "SUPERADMIN" Then
                rcbOrganization.SelectedValue = ds.Tables(0).Rows(0)("OrganizationID")
                rcbOrganization.Enabled = False
            Else
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    For Each item As RadListBoxItem In radlstCompanyAssociation.Items
                        If ds.Tables(0).Rows(i)("CompanyID") = item.Value And item.Checked = False Then
                            item.Checked = True
                            item.Enabled = False
                        End If
                    Next
                    For Each item1 As RadListBoxItem In rlbRoles.Items
                        Dim strRoleIDs1 = Split(item1.Value, "-")
                        If ds.Tables(0).Rows(i)("roleid") = Trim(strRoleIDs1(0)) And item1.Checked = False Then
                            item1.Checked = True
                            item1.Enabled = False
                        End If
                    Next
                Next

            End If
        End Sub
        Protected Sub AsyncUpload1_FileUploaded(ByVal sender As Object, ByVal e As FileUploadedEventArgs)

            If AsyncUpload1.UploadedFiles.Count > 0 Then
                Try
                    Dim buffer1 As Byte() = New Byte(AsyncUpload1.UploadedFiles(0).ContentLength - 1) {}
                    AsyncUpload1.UploadedFiles(0).InputStream.Read(buffer1, 0, Int32.Parse(AsyncUpload1.UploadedFiles(0).InputStream.Length.ToString()))
                    Dim fileExt As String = AsyncUpload1.UploadedFiles(0).GetExtension
                    RadBinaryImage1.Attributes.Add("Style", "visibility: visible")
                    RadBinaryImage1.Visible = True
                    RadBinaryImage1.DataValue = buffer1
                    lblImageValue.Text = Convert.ToBase64String(buffer1)
                Catch ex As Exception

                End Try
            Else

            End If
        End Sub
        Private Sub BindOrganization()
            'Bind the Organizations for Superadmin
            rcbOrganization.DataSource = clsTabMOrganisationMaster.GetAllTabMOrganizationMasterByEmployeeID(0, objuserInfo("schemaName"))
            rcbOrganization.DataTextField = "CodeName"
            rcbOrganization.DataValueField = "OrganizationID"
            rcbOrganization.DataBind()
        End Sub
        Private Sub BindCompanyAssociation()
            'Bind the companies list box based on logged in user comapny association
            radlstCompanyAssociation.DataSource = clsTabMOrganization.GetAllTabMOrganizationByEmployeeID(objuserrole("employeeID"), objuserInfo("schemaName"))
            radlstCompanyAssociation.DataTextField = "CodeName"
            radlstCompanyAssociation.DataValueField = "CompanyID"
            radlstCompanyAssociation.DataBind()
        End Sub

        Private Sub BindRoles()
            'Bind the companies list box based on logged in user comapny association
            rlbRoles.DataSource = clsTabRole.GetAllTabRoleByEmployeeID(objuserrole("employeeID"), objuserInfo("schemaName"))
            rlbRoles.DataTextField = "RoleCompany"
            rlbRoles.DataValueField = "RoleIDCompanyID"
            rlbRoles.DataBind()
        End Sub

        Private Sub rcbOrganization_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbOrganization.DataBound
            rcbOrganization.Items.Insert(0, New RadComboBoxItem("--Select--", String.Empty))
        End Sub

        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            Dim str As str
            Dim str1 As str
            If radUserName.Text.Contains(" ") Then
                lblMsg.Text = "UserName should not contain blank spaces"
                Exit Sub
            End If
            If objuserInfo("schemaName") = "dbo" Then
                'Save/Update New Employee Record in default Schema

                str = CreateNewEmployee(objuserInfo("schemaName"))
                If str.m_ret Then
                    'Save/Update New Employee Record in New Schema
                    Dim strOrg As String = rcbOrganization.SelectedItem.Text
                    Dim strOrganization() As String = Split(strOrg, "-")
                    str1 = CreateNewEmployee(strOrganization(0).Trim())
                End If
            Else
                'Save/Update New Employee Record in default Schema
                str = CreateNewEmployee("dbo")
                If str.m_ret Then
                    'Save/Update New Employee Record in New Schema
                    str1 = CreateNewEmployee(objuserInfo("schemaName"))
                End If
            End If

            If str.m_ret And str1.m_ret And mid.Length > 0 Then
                'Update mode and update is success and goto List page
                Response.Redirect("frmEmployeeList.aspx?process=Y&mode=E")
            ElseIf str.m_ret And str1.m_ret Then
                'Insert mode and creation is success and goto List page
                Response.Redirect("frmEmployeeList.aspx?process=Y&mode=A")
            ElseIf str.m_ret = False Or str1.m_ret = False Then
                lblMsg.Text = str.m_errormsg
            End If
        End Sub
        Private Function CreateNewEmployee(ByVal SchemaName As String) As str
            Dim ret As Boolean
            Dim Count As Integer = 0
            Dim Count1 As Integer = 0
            Dim Count2 As Integer = 0
            Dim exists As Boolean
            Dim strRoleIDs As String = ""
            Dim objTabMEmployee As PropertyTabMEmployees
            If Convert.ToString(objuserInfo("userName")).ToUpper() = "SUPERADMIN" Then
                'Validate if organization is selected or not
                If rcbOrganization.SelectedIndex <= 0 Then
                    lblMsg.Text = "Organization is required."
                    CreateNewEmployee.m_ret = False
                    CreateNewEmployee.m_errormsg = lblMsg.Text
                    'Return False
                    Exit Function
                End If
            Else

                'Validate if role selected or not
                If rlbRoles.Items.Count <= 0 Then
                    lblMsg.Text = "Select atleast one role."
                    CreateNewEmployee.m_ret = False
                    CreateNewEmployee.m_errormsg = lblMsg.Text
                    'Return False
                    Exit Function
                Else
                    For Each item1 As RadListBoxItem In rlbRoles.Items
                        If item1.Checked = True Then
                            Dim strRoleIDs1 = Split(item1.Value, "-")
                            strRoleIDs = strRoleIDs + strRoleIDs1(1) + ","
                            Count1 = Count1 + 1
                        End If
                    Next
                    If Count1 = 0 Then
                        lblMsg.Text = "Select atleast one role for each company selected."
                        CreateNewEmployee.m_ret = False
                        CreateNewEmployee.m_errormsg = lblMsg.Text
                        'Return False
                        Exit Function
                    End If
                    If strRoleIDs.Length > 0 Then
                        strRoleIDs = strRoleIDs.Remove(strRoleIDs.Length - 1)
                    End If
                End If
                'Validate if company selected or not
                If radlstCompanyAssociation.Items.Count <= 0 Then
                    lblMsg.Text = "Select atleast one company association."
                    CreateNewEmployee.m_ret = False
                    CreateNewEmployee.m_errormsg = lblMsg.Text
                    'Return False
                    Exit Function
                Else
                    For Each item As RadListBoxItem In radlstCompanyAssociation.Items
                        If item.Checked = True Then
                            If strRoleIDs.Contains(item.Value) Then
                                Count = Count + 1
                            Else
                                Count = 0
                            End If
                            Count2 = Count1 + 1
                        Else
                            If strRoleIDs.Contains(item.Value) Then
                                Count = 0
                            End If
                        End If
                    Next
                    If Count2 = 0 Then
                        lblMsg.Text = "Select atleast one company Association."
                        CreateNewEmployee.m_ret = False
                        CreateNewEmployee.m_errormsg = lblMsg.Text
                        'Return False
                        Exit Function
                    End If
                    If Count = 0 Then
                        lblMsg.Text = "Select atleast one role for each company selected.."
                        CreateNewEmployee.m_ret = False
                        CreateNewEmployee.m_errormsg = lblMsg.Text
                        'Return False
                        Exit Function
                    End If
                End If
            End If

            'validating EmployeeNo(Unique) if exists for add or update
            Dim objValidate As New PropertyValidations
            objValidate.Name = "EmployeeNo"
            objValidate.Table = "TabMEmployees"
            objValidate.NameVal = radEmployeeNo.Text
            If mid.Length > 0 Then
                objValidate.ID = "UserName"
                objValidate.IDVal = mid
            End If
            exists = clsValidations.ValidateTableByUserName(objValidate, SchemaName)
            If exists Then
                lblMsg.Text = "Employee No already exist"
                CreateNewEmployee.m_ret = False
                CreateNewEmployee.m_errormsg = lblMsg.Text
                'Return False
                Exit Function
            End If

            'validating Email(Unique) if exists for add or update
            objValidate.Name = "Email"
            objValidate.Table = "TabMEmployees"
            objValidate.NameVal = radEmail.Text
            If mid.Length > 0 Then
                objValidate.ID = "UserName"
                objValidate.IDVal = mid
            End If
            exists = clsValidations.ValidateTableByUserName(objValidate, SchemaName)
            If exists Then
                lblMsg.Text = "Email already exist"
                CreateNewEmployee.m_ret = False
                CreateNewEmployee.m_errormsg = lblMsg.Text
                'Return False
                Exit Function
            End If

            'validating UserName(Unique) if exists for add or update
            objValidate.Name = "UserName"
            objValidate.Table = "TabMEmployees"
            objValidate.NameVal = radUserName.Text
            If mid.Length > 0 Then
                objValidate.ID = "UserName"
                objValidate.IDVal = mid
            End If
            exists = clsValidations.ValidateTableByUserName(objValidate, SchemaName)
            If exists Then
                lblMsg.Text = "UserName already exist"
                CreateNewEmployee.m_ret = False
                CreateNewEmployee.m_errormsg = lblMsg.Text
                'Return False
                Exit Function
            End If

            'Create new instance of the class to store the aspx controls data in DB...

            If mid.Length > 0 Then
                objTabMEmployee = clsTabMEmployees.GetAllTabMEmployeesByUserName(mid, SchemaName)
            Else
                objTabMEmployee = New PropertyTabMEmployees
            End If
            objTabMEmployee.EmployeeName = radEmployeeName.Text
            objTabMEmployee.EmployeeNo = radEmployeeNo.Text
            objTabMEmployee.Position = radPosition.Text
            objTabMEmployee.Phone = radPhone.Text
            objTabMEmployee.Email = radEmail.Text
            objTabMEmployee.Comments = RadComments.Text
            objTabMEmployee.UserName = radUserName.Text
            If radPassword.Text.Length > 0 Then
                objTabMEmployee.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Trim(radPassword.Text), "SHA1")
            End If

            If rbtActive.SelectedItem.Text = "Yes" Then
                objTabMEmployee.Disable = "No"
            Else
                objTabMEmployee.Disable = "Yes"
            End If

            objTabMEmployee.Mobile = radMobile.Text

            'Employee Photo
            If lblImageValue.Text.Trim() <> "" Then
                objTabMEmployee.EmpPhoto = Convert.FromBase64String(lblImageValue.Text)
            End If

            'Save/Update the record based on mid
            If mid.Length > 0 Then
                'Edit mode record will be updated based on roleid
                objTabMEmployee.EmployeeID = objTabMEmployee.EmployeeID
                ret = clsTabMEmployees.UpdateTabMEmployees(objTabMEmployee, SchemaName)
                If Convert.ToString(objuserInfo("userName")).ToUpper() <> "SUPERADMIN" And SchemaName <> "dbo" Then
                    'Create CompanyEmployeeRole Association
                    For Each item1 As RadListBoxItem In rlbRoles.Items
                        If item1.Checked = True And item1.Enabled = True Then
                            Dim strRoleIDs1 = Split(item1.Value, "-")
                            Dim OrgIds() As String = Split(objuserrole("organizationIDS"), ",")
                            Dim objCompEmp As New PropertyTabMCompEmp
                            objCompEmp.EmployeeID = objTabMEmployee.EmployeeID
                            objCompEmp.CompanyID = Trim(strRoleIDs1(1))
                            objCompEmp.RoleID = Trim(strRoleIDs1(0))
                            objCompEmp.OrganizationID = OrgIds(0)
                            ret = clsTabMCompEmp.Save(objCompEmp, SchemaName)
                        End If
                    Next
                End If
            Else
                objTabMEmployee.Logged = "No"

                If Convert.ToString(objuserInfo("userName")).ToUpper() = "SUPERADMIN" Then
                    objTabMEmployee.IsOrganization = "True"
                    Dim strOrg As String = rcbOrganization.SelectedItem.Text
                    Dim strOrganization() As String = Split(strOrg, "-")
                    objTabMEmployee.SchemaName = strOrganization(0).Trim()
                Else
                    objTabMEmployee.IsOrganization = "False"
                    objTabMEmployee.SchemaName = objuserInfo("schemaName")
                End If


                'New Organization record will be inserted...
                ret = clsTabMEmployees.Save(objTabMEmployee, SchemaName)


                If SchemaName <> "dbo" Then
                    If Convert.ToString(objuserInfo("userName")).ToUpper() = "SUPERADMIN" Then
                        'Create a default Role for assigning to newly created employee
                        Dim objorg = clsTabMOrganisationMaster.GetTabMOrganizationMasterByOrgCode(SchemaName, SchemaName)
                        Dim objTabRole As New PropertyTabRole
                        objTabRole.RoleName = "Default Role - " + SchemaName
                        objTabRole.EnteredBy = 0
                        objTabRole.EnteredOn = DateTime.Now
                        objTabRole.CompanyID = objorg.OrganizationID
                        objTabRole.IsOrganization = True
                        ret = clsTabRole.Save(objTabRole, SchemaName)
                        If ret Then
                            'Create role privileges for logged in user with new role
                            ret = clsTABRolePrivileges.CreateNewTABRolePrivileges_ForDefaultRole(objTabRole.RoleID, SchemaName)
                        End If
                        'Create CompanyEmployee Association
                        Dim objCompEmp As New PropertyTabMCompEmp
                        objCompEmp.EmployeeID = objTabMEmployee.EmployeeID
                        objCompEmp.OrganizationID = objorg.OrganizationID
                        objCompEmp.RoleID = objTabRole.RoleID
                        ret = clsTabMCompEmp.Save(objCompEmp, SchemaName)
                    Else
                        'Create CompanyEmployeeRole Association
                        For Each item1 As RadListBoxItem In rlbRoles.Items
                            If item1.Checked = True Then
                                Dim strRoleIDs1 = Split(item1.Value, "-")
                                Dim OrgIds() As String = Split(objuserrole("organizationIDS"), ",")
                                Dim objCompEmp As New PropertyTabMCompEmp
                                objCompEmp.EmployeeID = objTabMEmployee.EmployeeID
                                objCompEmp.CompanyID = Trim(strRoleIDs1(1))
                                objCompEmp.RoleID = Trim(strRoleIDs1(0))
                                objCompEmp.OrganizationID = OrgIds(0)
                                ret = clsTabMCompEmp.Save(objCompEmp, SchemaName)
                            End If
                        Next
                    End If
                Else
                    If Convert.ToString(objuserInfo("userName")).ToUpper() = "SUPERADMIN" Then
                        'Create CompanyEmployee Association
                        Dim objCompEmp As New PropertyTabMCompEmp
                        objCompEmp.EmployeeID = objTabMEmployee.EmployeeID
                        objCompEmp.OrganizationID = rcbOrganization.SelectedValue
                        ret = clsTabMCompEmp.Save(objCompEmp, SchemaName)
                    End If

                End If
            End If

            'Audit Trail.... 
            If (SchemaName <> "dbo" And Convert.ToString(objuserInfo("userName")).ToUpper() <> "SUPERADMIN") Or (SchemaName = "dbo" And Convert.ToString(objuserInfo("userName")).ToUpper() = "SUPERADMIN") Then
                If (ret) And mid.Length > 0 Then
                    Dim objAudit As New PropertyTABAuditTrail

                    objAudit.UserID = objuserrole("employeeID")
                    objAudit.ActionDate = Now
                    objAudit.ActionName = "Edit"
                    objAudit.FunctionName = "Modified " & "Employee Record" & " : " & radEmployeeName.Text
                    objAudit.TableName = "TabMEmployees"
                    objAudit.PrimaryID = objTabMEmployee.EmployeeID
                    Dim suc As String = clsTABAuditTrail.Save(objAudit, SchemaName)
                ElseIf (ret) Then
                    Dim objAudit As New PropertyTABAuditTrail
                    objAudit.UserID = objuserrole("employeeID")
                    objAudit.ActionDate = Now
                    objAudit.ActionName = "Add"
                    objAudit.FunctionName = "Created " & "Employee Record" & " : " & radEmployeeName.Text
                    objAudit.TableName = "TabMEmployees"
                    objAudit.PrimaryID = objTabMEmployee.EmployeeID
                    Dim suc As String = clsTABAuditTrail.Save(objAudit, SchemaName)
                End If
            End If
            CreateNewEmployee.m_ret = ret
            CreateNewEmployee.m_errormsg = lblMsg.Text
            'Return ret
        End Function
        Public Structure str

            Public m_ret As Boolean

            Public m_errormsg As String

        End Structure
    End Class
End Namespace

