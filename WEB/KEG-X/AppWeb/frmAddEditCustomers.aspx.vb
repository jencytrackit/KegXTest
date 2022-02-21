Imports [Property].TrackITKTS
Imports BLL.TrackITKTS

Namespace TrackITKTS
    Partial Class frmAddEditCustomers
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
            If Not Request.QueryString("id") Is Nothing Then
                mid = Request.QueryString("id")
            End If
            If Not Page.IsPostBack Then
                If Not Request.QueryString("id") Is Nothing Then
                    mid = Request.QueryString("id")
                    EditMode(mid)
                End If
            End If

            lblMsg.Text = ""
        End Sub
        Private Sub EditMode(ByVal mid As Integer)
            Dim objCustomers As PropertyTabMCustomers = clsTabMCustomers.GetTabMCustomersByID(mid, objuserInfo("schemaName"))


            txtCustomerCode.Text = objCustomers.CustomerCode
            txtCustomerName.Text = objCustomers.CustomerName
            txtAddress1.Text = objCustomers.Address1
            txtAddress2.Text = objCustomers.Address2
            txtAddress3.Text = objCustomers.Address3
            txtAddress4.Text = objCustomers.Address4
            txtCity.Text = objCustomers.City
            txtState.Text = objCustomers.State
            txtCountry.Text = objCustomers.Country
            txtPhone.Text = objCustomers.Phone
            txtEmail.Text = objCustomers.Email
            txtFax.Text = objCustomers.Fax
            txtWebsite.Text = objCustomers.Website
            txtLOB.Text = objCustomers.LOB
            txtChannel.Text = objCustomers.Channel
            txtSoldtoParty.Text = objCustomers.SoldtoParty
            txtShiptoParty.Text = objCustomers.ShiptoParty
            txtCollectionFrequency.Text = objCustomers.CollectionFrequency
            txtRegion.Text = objCustomers.Region
            If objCustomers.IsPartner = True Then
                chkIsPartner.Checked = True
                chkIsPartner.Enabled = False
                trCustomerUser.Visible = True
                txtUserName.Enabled = False
            Else
                chkIsPartner.Checked = False
                chkIsPartner.Enabled = True
                txtUserName.Enabled = True
                trCustomerUser.Visible = False
            End If
            txtUserName.Text = objCustomers.UserName
            txtPassword.Text = objCustomers.Password

            objCustomers = Nothing
        End Sub

        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            Dim ret As Boolean
            'Create new instance of the class to store the aspx controls data...

            Dim objTABMCustomers As PropertyTabMCustomers
            If mid.Length > 0 Then
                objTABMCustomers = clsTabMCustomers.GetTabMCustomersByID(mid, objuserInfo("schemaName"))
            Else
                objTABMCustomers = New PropertyTabMCustomers
            End If

            If mid.Length > 0 Then
                If chkIsPartner.Checked = True Then
                    If txtUserName.Text.Trim() = "" Then
                        lblMsg.Text = "UserName is Required"
                        Exit Sub
                    End If
                    If objTABMCustomers.Password = "" And txtPassword.Text.Trim() = "" Then
                        lblMsg.Text = "Password is Required"
                        Exit Sub
                    End If
                End If
                'Save the customer as partner login details in default schema and logged in user schema for login purpose
                'validating UserName(Unique) if exists for add or update
                If txtUserName.Text.Contains(" ") Then
                    lblMsg.Text = "UserName should not contain blank spaces"
                    Exit Sub
                End If

                If chkIsPartner.Checked = True Then
                    ret = SaveCustomerUser("dbo", objTABMCustomers.Password)
                    If ret Then
                        ret = SaveCustomerUser(objuserInfo("schemaName"), objTABMCustomers.Password)
                    End If
                End If

                objTABMCustomers.LOB = txtLOB.Text
                objTABMCustomers.SoldtoParty = txtSoldtoParty.Text
                objTABMCustomers.ShiptoParty = txtShiptoParty.Text
                objTABMCustomers.CollectionFrequency = txtCollectionFrequency.Text
                objTABMCustomers.Channel = txtChannel.Text
                objTABMCustomers.Region = txtRegion.Text
                If chkIsPartner.Checked = True Then
                    objTABMCustomers.IsPartner = True
                    objTABMCustomers.UserName = txtUserName.Text
                    If txtPassword.Text.Length > 0 Then
                        objTABMCustomers.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Trim(txtPassword.Text), "SHA1")
                    End If
                Else
                    objTABMCustomers.IsPartner = False
                End If
                Dim i As Int32 = Integer.Parse(Request.QueryString("id"))
                objTABMCustomers.CustomerID = i
                ret = clsTabMCustomers.UpdateTabMCustomers(objTABMCustomers, objuserInfo("schemaName"))

            End If

            'Audit Trail.... 
            If (ret) And mid.Length > 0 Then
                Dim objAudit As New PropertyTABAuditTrail

                objAudit.UserID = objuserrole("employeeID")
                objAudit.ActionDate = Now
                objAudit.ActionName = "Edit"
                objAudit.FunctionName = "Modified " & "Customers Record" & " : " & txtCustomerName.Text
                objAudit.TableName = "TabMCustomers"
                objAudit.PrimaryID = objTABMCustomers.CustomerID
                Dim suc As String = clsTABAuditTrail.Save(objAudit, objuserInfo("schemaName"))
            ElseIf (ret) Then
                Dim objAudit As New PropertyTABAuditTrail
                objAudit.UserID = objuserrole("employeeID")
                objAudit.ActionDate = Now
                objAudit.ActionName = "Add"
                objAudit.FunctionName = "Created " & "Customers Record" & " : " & txtCustomerName.Text
                objAudit.TableName = "TabMCustomers"
                objAudit.PrimaryID = objTABMCustomers.CustomerID
                Dim suc As String = clsTABAuditTrail.Save(objAudit, objuserInfo("schemaName"))
            End If

            If (ret) And mid.Length > 0 Then
                'Insert mode and creation is success and goto List page
                Response.Redirect("frmCustomersList.aspx?process=Y&mode=E")
            ElseIf ret Then
                Response.Redirect("frmCustomersList.aspx?process=Y&mode=A")
            End If
        End Sub

        Private Sub chkIsPartner_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsPartner.CheckedChanged
            If chkIsPartner.Checked = True Then
                trCustomerUser.Visible = True
            Else
                trCustomerUser.Visible = False
            End If
        End Sub

        Private Function SaveCustomerUser(ByVal SchemaName As String, ByVal Password As String) As Boolean
            Dim ret As Boolean
            Dim objValidate As New PropertyValidations
            Dim objTabMEmployee As PropertyTabMEmployees
            objTabMEmployee = clsTabMEmployees.GetAllTabMEmployeesByEmployeeName(Trim(txtCustomerCode.Text), SchemaName)
            If objTabMEmployee.EmployeeID > 0 Then
                objValidate.Name = "UserName"
                objValidate.Table = "TabMEmployees"
                objValidate.NameVal = txtUserName.Text
                If mid.Length > 0 Then
                    objValidate.ID = "UserName"
                    objValidate.IDVal = objTabMEmployee.UserName
                End If
                Dim exists = clsValidations.ValidateTableByUserName(objValidate, SchemaName)
                If exists Then

                    lblMsg.Text = "UserName already exist"
                    Return False
                    Exit Function
                End If
                objTabMEmployee.UserName = txtUserName.Text
                If txtPassword.Text.Length > 0 Then
                    objTabMEmployee.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Trim(txtPassword.Text), "SHA1")
                Else
                    objTabMEmployee.Password = Password
                End If
                ret = clsTabMEmployees.UpdateTabMEmployees(objTabMEmployee, SchemaName)
            Else
                objValidate.Name = "UserName"
                objValidate.Table = "TabMEmployees"
                objValidate.NameVal = txtUserName.Text
                Dim exists = clsValidations.ValidateTable(objValidate, SchemaName)
                If exists Then
                    lblMsg.Text = "UserName already exist"
                    Return False
                    Exit Function
                End If
                objTabMEmployee = New PropertyTabMEmployees
                objTabMEmployee.EmployeeName = txtCustomerCode.Text
                objTabMEmployee.EmployeeNo = "Partner"
                objTabMEmployee.Email = txtEmail.Text
                objTabMEmployee.UserName = txtUserName.Text
                If txtPassword.Text.Length > 0 Then
                    objTabMEmployee.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Trim(txtPassword.Text), "SHA1")
                Else
                    objTabMEmployee.Password = Password
                End If
                objTabMEmployee.Disable = "No"
                objTabMEmployee.Logged = "No"
                objTabMEmployee.IsOrganization = "False"
                objTabMEmployee.SchemaName = objuserInfo("schemaName")
                ret = clsTabMEmployees.Save(objTabMEmployee, SchemaName)
            End If
            Return ret
        End Function
    End Class
End Namespace
