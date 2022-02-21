
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmAddEditSuppliers
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
            Dim objSupplier As PropertyTabMSuppliers = clsTabMSuppliers.GetTabMSuppliersByID(mid, objuserInfo("schemaName"))


            txtSupplierCode.Text = objSupplier.SupplierCode
            txtSupplierName.Text = objSupplier.SupplierName
            txtAddress1.Text = objSupplier.Address1
            txtAddress2.Text = objSupplier.Address2
            txtAddress3.Text = objSupplier.Address3
            txtAddress4.Text = objSupplier.Address4
            txtCity.Text = objSupplier.City
            txtState.Text = objSupplier.State
            txtCountry.Text = objSupplier.Country
            txtPhone.Text = objSupplier.Phone
            txtEmail.Text = objSupplier.Email
            txtFax.Text = objSupplier.Fax
            txtWebsite.Text = objSupplier.Website
            txtShipmentPort.Text = objSupplier.ShipmentPort
            txtDestinationPort.Text = objSupplier.DestinationPort
            txtTermsofreturn.Text = objSupplier.TermsofReturn

            objSupplier = Nothing
        End Sub

        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click

            Dim ret As Boolean
            'Create new instance of the class to store the aspx controls data...
            Dim objTABMSuppliers As PropertyTabMSuppliers
            If mid.Length > 0 Then
                objTABMSuppliers = clsTabMSuppliers.GetTabMSuppliersByID(mid, objuserInfo("schemaName"))
            Else
                objTABMSuppliers = New PropertyTabMSuppliers
            End If


            If mid.Length > 0 Then
                objTABMSuppliers.ShipmentPort = txtShipmentPort.Text
                objTABMSuppliers.DestinationPort = txtDestinationPort.Text
                objTABMSuppliers.TermsofReturn = txtTermsofreturn.Text
                Dim i As Int32 = Integer.Parse(Request.QueryString("id"))
                objTABMSuppliers.SupplierID = i
                ret = clsTabMSuppliers.UpdateTabMSuppliers(objTABMSuppliers, objuserInfo("schemaName"))

            End If


            'Audit Trail.... 
            If (ret) And mid.Length > 0 Then
                Dim objAudit As New PropertyTABAuditTrail

                objAudit.UserID = objuserrole("employeeID")
                objAudit.ActionDate = Now
                objAudit.ActionName = "Edit"
                objAudit.FunctionName = "Modified " & "Suppliers Record" & " : " & txtSupplierName.Text
                objAudit.TableName = "TabMSuppliers"
                objAudit.PrimaryID = objTABMSuppliers.SupplierID
                Dim suc As String = clsTABAuditTrail.Save(objAudit, objuserInfo("schemaName"))
            ElseIf (ret) Then
                Dim objAudit As New PropertyTABAuditTrail
                objAudit.UserID = objuserrole("employeeID")
                objAudit.ActionDate = Now
                objAudit.ActionName = "Add"
                objAudit.FunctionName = "Created " & "Suppliers Record" & " : " & txtSupplierName.Text
                objAudit.TableName = "TabMSuppliers"
                objAudit.PrimaryID = objTABMSuppliers.SupplierID
                Dim suc As String = clsTABAuditTrail.Save(objAudit, objuserInfo("schemaName"))
            End If

            If (ret) And mid.Length > 0 Then
                'Insert mode and creation is success and goto List page
                Response.Redirect("frmSuppliersList.aspx?process=Y&mode=E")
            ElseIf ret Then
                Response.Redirect("frmSuppliersList.aspx?process=Y&mode=A")
            Else
                lblMsg.Text = Resources.lang.msgError
            End If
        End Sub
    End Class
End Namespace
