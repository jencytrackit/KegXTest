
'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmAddEditOrganization.aspx
'Created By         :
'File navigation    :
'Created Date       : January 17, 2014, 2:08:31 PM
'Description        : This file is used to List the Organizations Records...
'Modified By        :
'Modified Date      :
'***************************************************************

Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmAddEditOrganization
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Private mid As String = ""
        Private m_lImageFileLength As Long = 0
        Dim vavalue As Byte = 1

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
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
            empimgpos.Attributes.Add("style", "position: relative; display: inline-block; padding-left: 212px; margin-top: -48px;")
            lblMsg.Text = ""
        End Sub
        Private Sub EditMode(ByVal mid As Integer)
            Dim objOrganization As PropertyTabMOrganisationMaster = clsTabMOrganisationMaster.GetTabMOrganizationMasterByID(mid, objuserInfo("schemaName"))

            txtOrganizationCode.Text = objOrganization.OrganizationCode
            txtOrganizationCode.Enabled = False
            txtOrganizationName.Text = objOrganization.OrganizationName
            txtAddress.Text = objOrganization.Address1
            txtAddress2.Text = objOrganization.Address2
            txtCity.Text = objOrganization.City
            txtState.Text = objOrganization.State
            txtCountry.Text = objOrganization.Country
            txtPhone.Text = objOrganization.Phone
            txtEmail.Text = objOrganization.Email
            If Not objOrganization.OrganizationImage Is Nothing Then
                RadBinaryImage1.Attributes.Add("Style", "visibility: visible")
                RadBinaryImage1.Visible = True
                RadBinaryImage1.DataValue = objOrganization.OrganizationImage
                lblImageValue.Text = Convert.ToBase64String(objOrganization.OrganizationImage)
            End If

            objOrganization = Nothing
        End Sub

        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            Dim ret As Boolean
            Dim ret1 As Boolean
            'Save/Update New Organization Record in default Schema
            ret = CreateNewOrganization(objuserInfo("schemaName"))
            If ret Then
                'Create Schema 

                'Save/Update New Organization Record in New Schema
                ret1 = CreateNewOrganization(txtOrganizationCode.Text)
            End If

            If ret And mid.Length > 0 Then
                'Update mode and update is success and goto List page
                Response.Redirect("frmOrganisationMasterList.aspx?process=Y&mode=E")
            ElseIf ret And ret1 Then
                'Insert mode and creation is success and goto List page
                Response.Redirect("frmOrganisationMasterList.aspx?process=Y&mode=A")
            ElseIf vavalue = 3 Then
                lblMsg.Text = "Organization code already exist"
            Else
                lblMsg.Text = Resources.lang.msgError
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

        Private Function CreateNewOrganization(ByVal SchemaName As String) As Boolean
            Dim ret As Boolean
            Dim Count As Integer = 0
            Dim objTabMOrganisationMaster As PropertyTabMOrganisationMaster
            'validating Organization code if exists for add or update
            Dim objValidate As New PropertyValidations
            objValidate.Name = "OrganizationCode"
            objValidate.Table = "TabMOrganisationMaster"
            objValidate.NameVal = txtOrganizationCode.Text
            If mid.Length > 0 Then
                objValidate.ID = "OrganizationID"
                objValidate.IDVal = mid
            End If

            Dim exists As Boolean = clsValidations.ValidateTable(objValidate, SchemaName)
            If exists Then
                lblMsg.Text = "Organization code already exist"
                vavalue = 3
                Return ret
                Exit Function
            End If

            'Create new instance of the class to store the aspx controls data in DB...

            If mid.Length > 0 Then
                objTabMOrganisationMaster = clsTabMOrganisationMaster.GetTabMOrganizationMasterByID(mid, SchemaName)
            Else

                objTabMOrganisationMaster = New PropertyTabMOrganisationMaster
            End If
            objTabMOrganisationMaster.OrganizationCode = txtOrganizationCode.Text
            objTabMOrganisationMaster.OrganizationName = txtOrganizationName.Text
            objTabMOrganisationMaster.Address1 = txtAddress.Text
            objTabMOrganisationMaster.Address2 = txtAddress2.Text
            objTabMOrganisationMaster.City = txtCity.Text
            objTabMOrganisationMaster.State = txtState.Text
            objTabMOrganisationMaster.Phone = txtPhone.Text
            objTabMOrganisationMaster.Country = txtCountry.Text
            objTabMOrganisationMaster.Email = txtEmail.Text

            'Organization Photo
            If lblImageValue.Text.Trim() <> "" Then
                objTabMOrganisationMaster.OrganizationImage = Convert.FromBase64String(lblImageValue.Text)
            End If

            'Save/Update the record based on mid
            If mid.Length > 0 Then
                'Edit mode record will be updated based on roleid
                Dim i As Int32 = Integer.Parse(Request.QueryString("id"))
                objTabMOrganisationMaster.OrganizationID = i
                ret = clsTabMOrganisationMaster.UpdateTabMOrganization(objTabMOrganisationMaster, SchemaName)
            Else
                'New Organization record will be inserted...
                ret = clsTabMOrganisationMaster.Save(objTabMOrganisationMaster, SchemaName)
                If ret Then
                    If SchemaName.Trim.Equals("dbo") Then
                        Dim str = clsTabMOrganisationMaster.GenerateSchema(txtOrganizationCode.Text.Trim())
                    End If
                End If
            End If
            'Audit Trail.... 
            If SchemaName = "dbo" Then
                If (ret) And mid.Length > 0 Then
                    Dim objAudit As New PropertyTABAuditTrail
                    objAudit.UserID = objuserInfo("empID")
                    objAudit.ActionDate = Now
                    objAudit.ActionName = "Edit"
                    objAudit.FunctionName = "Modified" & "Organization Record" & " : " & txtOrganizationName.Text
                    objAudit.TableName = "TabMOrganisationMaster"
                    objAudit.PrimaryID = objTabMOrganisationMaster.OrganizationID
                    Dim suc As String = clsTABAuditTrail.Save(objAudit, SchemaName)
                ElseIf (ret) Then
                    Dim objAudit As New PropertyTABAuditTrail
                    objAudit.UserID = objuserInfo("empID")
                    objAudit.ActionDate = Now
                    objAudit.ActionName = "Add"
                    objAudit.FunctionName = "Created " & "Organization Record" & " : " & txtOrganizationName.Text
                    objAudit.TableName = "TabMOrganisationMaster"
                    objAudit.PrimaryID = objTabMOrganisationMaster.OrganizationID
                    Dim suc As String = clsTABAuditTrail.Save(objAudit, SchemaName)
                End If
            End If

            Return ret
        End Function

    End Class
End Namespace
