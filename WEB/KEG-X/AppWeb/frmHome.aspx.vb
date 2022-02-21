Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmHome
        Inherits System.Web.UI.Page

        Dim objuserInfo As New Hashtable()
        Dim objuserrole As New Hashtable()

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If

            If Not Page.IsPostBack Then
                BindCustomerRegions()
            End If

            BindSupplierInOut()
            BindTop10Suppliers()
            BindBPEmptiesInYard()

        End Sub

        Private Sub BindCustomerRegions()
            rcbRegions.DataSource = clsValidations.GetCustomerRegions(0, objuserrole("employeeID"), objuserInfo("schemaName"))
            rcbRegions.DataBind()
            rcbRegions.SelectedIndex = rcbRegions.Items.IndexOf(rcbRegions.Items.FindItemByText("Abudhabi"))
            rcbRegions_SelectedIndexChanged(Nothing, Nothing)
        End Sub

        Private Sub BindCustomerInOut()
            If rcbRegions.SelectedIndex < 0 Then Return
            rCustomerInOut.DataSource = clsValidations.GetCustomerDashBoard1(1, objuserrole("employeeID"), rcbRegions.SelectedItem.Text, objuserInfo("schemaName"))
            rCustomerInOut.DataBind()
        End Sub
        Private Sub BindTop10Customer()
            If rcbRegions.SelectedIndex < 0 Then Return
            rCustomerTop10.DataSource = clsValidations.GetTop10CustomerDashBoard(1, objuserrole("employeeID"), rcbRegions.SelectedItem.Text, objuserInfo("schemaName"))
            rCustomerTop10.DataBind()
        End Sub

        Private Sub BindSupplierInOut()
            rSupplierInOut.DataSource = clsValidations.GetSupplierDashBoardOrg(objuserInfo("schemaName"))
            rSupplierInOut.DataBind()
        End Sub

        Private Sub BindTop10Suppliers()
            If rcbRegions.SelectedIndex < 0 Then Return
            rSupplierTop10.DataSource = clsValidations.GetSupplierDashBoardOrgTop10(objuserInfo("schemaName"))
            rSupplierTop10.DataBind()
        End Sub

        Private Sub BindBPEmptiesInYard()
            rBPEmptiesInYard.DataSource = clsValidations.GetBPEmptyKegsStockDashBoard(objuserrole("employeeID"), "1000", objuserInfo("schemaName"))
            rBPEmptiesInYard.DataBind()
        End Sub

        Private Sub rcbRegions_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbRegions.SelectedIndexChanged
            BindCustomerInOut()
            BindTop10Customer()
        End Sub

    End Class
End Namespace
