'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmSalesOrderList.aspx
'Created By         :
'File navigation    :
'Created Date       :November 08, 2013, 2:17:45 PM
'Description        :This file is used to List the Sale Order Details...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports Telerik.Web.UI

Namespace TrackITKTS
    Partial Class frmSalesOrderList
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim objuserrole As New Hashtable()
        'Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        '    Dim cbo As Telerik.Web.UI.RadComboBox = CType(Me.Master.FindControl("rcbCompany"), Telerik.Web.UI.RadComboBox)
        '    AddHandler cbo.SelectedIndexChanged, AddressOf ddlMasterPageSelectedIndexChanged
        'End Sub
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            lblMsg.Text = ""
            If objuserInfo Is Nothing Then
                Return
            End If
            AddContextKey()
            If Not Page.IsPostBack Then
                ViewState("SearchStatus") = "Pending"
            End If
        End Sub

        Private Sub ObjDSCustomers_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjDSCustomers.Selecting
            'Pass the logged in employeeid and companyid and retrieve the Customers associated for that company and employee

            e.InputParameters("EmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")

        End Sub

        Private Sub ObjBranchPlant_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjBranchPlant.Selecting
            'Pass the logged in employeeid and companyid and retrieve the BranchPlants associated for that company and employee

            e.InputParameters("EmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")

        End Sub
        Private Sub AddContextKey()
       
            If objuserInfo.Count > 1 And objuserrole("employeeID") > 0 Then
                AutoCompleteExtender_txtBranchplant.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
                AutoCompleteExtender_txtCustomer.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
                AutoCompleteExtender_txtItemCode.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
            Else
                AutoCompleteExtender_txtBranchplant.ContextKey = ""
                AutoCompleteExtender_txtCustomer.ContextKey = ""
                AutoCompleteExtender_txtItemCode.ContextKey = ""
            End If
        End Sub
    
        Private Sub ObjSaleOrder_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjSaleOrder.Selecting
            'Pass the companyid and retrieve the Sale Order details for the company
            
            e.InputParameters("EmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")
            e.InputParameters("SaleOrderNumber") = ""
        End Sub

        Private Sub RadgvSalesOrder_DetailTableDataBind(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadgvSalesOrder.DetailTableDataBind
            Dim dataItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
            If e.DetailTableView.Name = "OrderItems" Then
                'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
                Dim EmployeeID As Integer = objuserrole("employeeID")

                Dim OrderNumber As String = dataItem.GetDataKeyValue("OrderNumber").ToString()
                Dim BranchID As Integer = dataItem.GetDataKeyValue("BranchID")

                If txtItemCode1.Text.Trim.Length > 0 And txtItemBarcode.Text.Trim.Length <= 0 Then
                    e.DetailTableView.DataSource = clsTabKSaleOrders.GetAllTabKSaleOrdersByEmployeeIDSearch(EmployeeID, objuserInfo("schemaName"), OrderNumber, hdfItemCode.Value, "", BranchID)
                ElseIf txtItemCode1.Text.Trim.Length > 0 And txtItemBarcode.Text.Trim.Length > 0 Then
                    e.DetailTableView.DataSource = clsTabKSaleOrders.GetAllTabKSaleOrdersByEmployeeIDSearch(EmployeeID, objuserInfo("schemaName"), OrderNumber, hdfItemCode.Value, txtItemBarcode.Text, BranchID)
                ElseIf txtItemCode1.Text.Trim.Length <= 0 And txtItemBarcode.Text.Trim.Length > 0 Then
                    e.DetailTableView.DataSource = clsTabKSaleOrders.GetAllTabKSaleOrdersByEmployeeIDSearch(EmployeeID, objuserInfo("schemaName"), OrderNumber, "", hdfItemCode.Value, BranchID)
                Else
                    e.DetailTableView.DataSource = clsTabKSaleOrders.GetAllTabKSaleOrdersByEmployeeIDSearch(EmployeeID, objuserInfo("schemaName"), OrderNumber, "", "", BranchID)
                End If

            End If

        End Sub

        Private Sub RadgvSalesOrder_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvSalesOrder.NeedDataSource

            ' Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            Dim EmployeeID As Integer = objuserrole("employeeID")
            'If Not strCompanyId = "" Then
            Dim itemCode As String = ""
            Dim itemBarcode As String = ""
            If txtItemCode1.Text.Trim.Length > 0 Then

                itemCode = clsValidations.GetLookUpIDByEmployeeCode(txtItemCode1.Text)
                hdfItemCode.Value = itemCode
            Else
                itemCode = ""
            End If
            If txtItemBarcode.Text.Trim.Length > 0 Then
                itemBarcode = txtItemBarcode.Text
            Else
                itemBarcode = ""
            End If

            Dim ds As DataSet = clsTabKSaleOrders.GetAllTabKSaleOrdersByEmployeeIDSearch(EmployeeID, objuserInfo("schemaName"), "", itemCode, itemBarcode, 0)
            If Not ds Is Nothing Then
                ds.CaseSensitive = False
                Dim dv As DataView = ds.Tables(0).DefaultView
                Dim strQ As String

                If ViewState("SearchStatus") = "Pending" Then
                    strQ = "EmployeeID=" & EmployeeID & " AND Status='" & ViewState("SearchStatus") & "'"
                Else
                    strQ = "EmployeeID=" & EmployeeID
                End If

                'If txtCompanyCode.Text.Trim.Length > 0 Then
                '    strQ = "CompanyCode= '" + txtCompanyCode.Text.ToUpper + "'"
                'End If

                If txtOrderNo.Text.Trim.Length > 0 Then
                    strQ = strQ & " And OrderNumber= '" + txtOrderNo.Text.ToUpper + "'"
                End If
                If txtOrderType.Text.Trim.Length > 0 Then
                    strQ = strQ & " And OrderType= '" + txtOrderType.Text.ToUpper + "'"
                End If
                If hdfBranchID.Value.Length > 0 Then
                    strQ = strQ & " And BranchID= " + hdfBranchID.Value + ""

                End If
                If hdfCustomerID.Value.Length > 0 Then
                    strQ = strQ & " And CustomerID= " + hdfCustomerID.Value + ""
                End If
          
                'If rcbCustomer.SelectedIndex > 0 Then
                '    strQ = strQ & " And CustomerID= " + rcbCustomer.SelectedValue + ""
                'End If
                dv.RowFilter = strQ
                RadgvSalesOrder.DataSource = dv
            End If
            '  End If

        End Sub

        Private Sub radSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSearch.Click
            ViewState("SearchStatus") = ""
            RadgvSalesOrder.Rebind()
        End Sub

        Private Sub radClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radClear.Click
            txtOrderNo.Text = ""
            txtOrderType.Text = ""
            txtBranchplant.Text = ""
            hdfBranchID.Value = ""
            hdfCustomerID.Value = ""
            hdfItemCode.Value = ""
            txtCustomer.Text = ""
            txtItemCode1.Text = ""
            txtItemBarcode.Text = ""
            ViewState("SearchStatus") = "Pending"
            RadgvSalesOrder.Rebind()
        End Sub

        Protected Sub txtCustomer_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCustomer.TextChanged

            hdfCustomerID.Value = ""
            If Len(txtCustomer.Text) > 1 Then
                Dim CustomerID As Integer
                CustomerID = clsValidations.GetLookupIDsbyEmpID(objuserrole("employeeID"), clsValidations.GetLookUpIDByEmployeeCode(txtCustomer.Text), "C", objuserInfo("schemaName"))

                If CustomerID > 0 Then
                    hdfCustomerID.Value = CustomerID
                Else
                    '  lblMsg.Text = "Error while getting the Customer ID. Please try again."
                    txtCustomer.Focus()
                End If
            Else
                txtCustomer.Text = ""
                hdfCustomerID.Value = ""
            End If
        End Sub

        Protected Sub txtBranchplant_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtBranchplant.TextChanged
            hdfBranchID.Value = ""
            If Len(txtBranchplant.Text) > 1 Then
                Dim BranchID As Integer
                BranchID = clsValidations.GetLookupIDsbyEmpID(objuserrole("employeeID"), clsValidations.GetLookUpIDByEmployeeCode(txtBranchplant.Text), "B", objuserInfo("schemaName"))

                If BranchID > 0 Then
                    hdfBranchID.Value = BranchID
                Else
                    '  lblMsg.Text = "Error while getting the Customer ID. Please try again."
                    txtBranchplant.Focus()
                End If
            Else
                txtBranchplant.Text = ""
                hdfBranchID.Value = ""
            End If
        End Sub

        Protected Sub txtItemCode1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtItemCode1.TextChanged
            hdfItemCode.Value = ""
            If Len(txtItemCode1.Text) > 1 Then
                Dim ItemCode As Integer

                ItemCode = clsValidations.GetLookUpIDByEmployeeCode(txtItemCode1.Text)

                If Len(ItemCode) > 0 Then
                    hdfItemCode.Value = ItemCode
                Else
                    '  lblMsg.Text = "Error while getting the Customer ID. Please try again."
                    txtBranchplant.Focus()
                End If
            Else
                txtItemCode1.Text = ""
                hdfItemCode.Value = ""
            End If
        End Sub

        Private Sub RadgvSalesOrder_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvSalesOrder.ItemDataBound
            If TypeOf e.Item Is GridDataItem AndAlso e.Item.OwnerTableView.Name = "OrderItems" Then
                'Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                'TryCast(item("gridSelectAll").FindControl("chkSelectAll"), CheckBox).Attributes.Add("onclick", "return SetHeaderCheckBox(this, " + item.OwnerTableView.ParentItem.ItemIndex.ToString & ");")
            ElseIf TypeOf e.Item Is GridHeaderItem AndAlso e.Item.OwnerTableView.Name = "OrderItems" Then
                Dim header As GridHeaderItem = TryCast(e.Item, GridHeaderItem)
                TryCast(header("gridSelectAll").FindControl("chkAll"), CheckBox).Attributes.Add("onclick", "return SelectAllCheckBox(this, " + header.OwnerTableView.ParentItem.ItemIndex.ToString & ");")
                'TryCast(header("grdSubGrid").FindControl("chkSelectAllSub"), CheckBox).Attributes.Add("onclick", "return SelectAllCheckBox(this, " + header.OwnerTableView.ParentItem.ItemIndex.ToString & ");")
            End If
        End Sub

        Private Sub RadgvSalesOrder_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadgvSalesOrder.ItemCommand

            If e.CommandName = "Reprint" Then
                lblMsg.Text = ""

                Dim ParentGridDataItem As GridDataItem = CType(e.Item, GridDataItem)
                Dim txtOrderNumber As String = String.Empty
                Dim SelectedRecSel As Byte = 0

                If e.Item.OwnerTableView.Name = "ParentGrid" Then
                    txtOrderNumber = ParentGridDataItem.GetDataKeyValue("OrderNumber")

                    For Each childItem As GridDataItem In ParentGridDataItem.ChildItem.NestedTableViews(0).Items
                       
                        Dim OrderItemID As Integer = childItem.GetDataKeyValue("ItemID")
                        Dim ItemBarcode As String = childItem.Item("ItemBarcode").Text
                        Dim chkChecked As System.Web.UI.WebControls.CheckBox

                        Dim retVal As Boolean
                        chkChecked = DirectCast(childItem.FindControl("chkSelectAll"), System.Web.UI.WebControls.CheckBox)
                        If chkChecked.Checked = True And chkChecked.Enabled = True Then
                            'retVal = clsTabKEmptyCustomer.UpdateTabKEmptyKegCustomerReturnsHHT(trnxNumber, FromCompID, ToCompID, FromItmID, ToItmID, intApprovedQuantity, strComments, objuserInfo("schemaName"))
                            'lblMsg.Text = lblMsg.Text & "Order No: " & txtOrderNumber & " - Order Item ID:" & OrderItemID & " - ItemBarcode:" & ItemBarcode & "<br/>"
                            retVal = clsTabKSaleOrders.UpdateRePrintStatusSO(txtOrderNumber, OrderItemID, ItemBarcode, objuserInfo("schemaName"))
                            lblMsg.Text = "Re-Print Record Status Updated Successfully!"
                            SelectedRecSel = 1
                        End If
                    Next
                    If SelectedRecSel = 0 Then
                        lblMsg.Text = "Please select The Items To Re-Print The Barcode."
                    End If
                End If

            End If
        End Sub


    End Class
End Namespace

