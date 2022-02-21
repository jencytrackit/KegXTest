Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports Telerik.Web.UI
Namespace TrackITKTS
    Public Class frmFullKegReturnCustomer_Bulk
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim userPrivileges As New Hashtable
        Dim objuserrole As New Hashtable()

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            userPrivileges = Session("UserPrivileges") '// user Privileges maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session

            lblMsg.Text = ""

            If objuserInfo Is Nothing Then
                Return
            End If
            If Not Page.IsPostBack Then
                'if the query string values for process or mode exists then the control is coming from the add or edit page...
                Dim strVal As String = Request.QueryString("process")
                Dim strMOde As String = Request.QueryString("mode")
                Select Case strMOde
                    Case "D"
                        If Trim(strVal) = "Y" Then  '// chekcing for the success or failure of the process
                            lblMsg.Text = Resources.lang.Deleted
                        End If
                    Case "E"
                        If Trim(strVal) = "Y" Then  '// chekcing for the success or failure of the process
                            lblMsg.Text = Resources.lang.Updated
                        End If
                    Case "A"
                        If Trim(strVal) = "Y" Then  '// chekcing for the success or failure of the process
                            lblMsg.Text = Resources.lang.Inserted
                        End If
                    Case "F"
                        lblMsg.Text = Resources.lang.NotFound
                    Case "IA"  '//Checking the invalid access page from master page load....

                        lblMsg.Text = "Invalid Access..."
                End Select

                'RadgvFullCust.Visible = False

                BindToCompany()

                'implementPrivilegesAdd()
            End If

            AddContextKey()

        End Sub
        Private Sub AddContextKey()
            If objuserInfo.Count > 1 And objuserrole("employeeID") > 0 Then
                AutoCompleteExtender_txtCustomer.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
                AutoCompleteExtender_txtItemCode.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
                'AutoCompleteExtender_txtBranchplant.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
            Else
                AutoCompleteExtender_txtCustomer.ContextKey = ""
                AutoCompleteExtender_txtItemCode.ContextKey = ""
                'AutoCompleteExtender_txtBranchplant.ContextKey = ""
            End If
            If objuserInfo.Count > 1 And rcbToCmpny.SelectedIndex > 0 Then
                AutoCompleteExtender_txtBranchplant.ContextKey = rcbToCmpny.SelectedValue & "," & objuserInfo("schemaName")
            Else
                AutoCompleteExtender_txtBranchplant.ContextKey = ""
            End If

        End Sub

        Private Sub BindToCompany()

            Dim ds As New DataSet
            ds = clsTabMOrganization.GetAllTabMOrganizationByEmployeeID(objuserrole("employeeID"), objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                ' bind to company
                rcbToCmpny.DataSource = ds
                rcbToCmpny.DataTextField = "CompanyName"
                rcbToCmpny.DataValueField = "CompanyID"
                rcbToCmpny.DataBind()
            End If


            rcbToCmpny.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        End Sub

        Private Sub RadgvFullCust_DetailTableDataBind(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadgvFullCust.DetailTableDataBind
            Dim dataItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
            If e.DetailTableView.Name = "ChildItems" Then
                'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
                Dim CompanyID As Int32 = dataItem("CompanyID").Text
                Dim SOrderID As String = dataItem.GetDataKeyValue("SOrderID").ToString()

                e.DetailTableView.DataSource = clsTabKFullKegReturnsCustomer.GetAllTabKFullKegReturnCustomerByCompanyID_Search_Bulk(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), SOrderID, txtOrderNo.Text.Trim, hdfCustomerID.Value, hdfItemCode.Value)

            End If
        End Sub

        Private Sub RadgvFullCust_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvFullCust.NeedDataSource

            Dim EmployeeID As Integer = objuserrole("employeeID")

            Dim ds As DataSet = clsTabKFullKegReturnsCustomer.GetAllTabKFullKegReturnCustomerByCompanyID_Search_Bulk(0, objuserrole("employeeID"), objuserInfo("schemaName"), "", txtOrderNo.Text.Trim, hdfCustomerID.Value, hdfItemCode.Value)

            If ds.Tables.Count > 0 Then
                RadgvFullCust.DataSource = ds
            Else
                'RadgvFullCust.Visible = False
                'trSRC.Visible = False
            End If

        End Sub

        Private Sub radSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSearch.Click

            Clear()
            lblMsg.Text = ""
            'RadgvFullCust.Visible = True
            RadgvFullCust.Rebind()

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
                    txtItemCode1.Focus()
                End If
            Else
                txtItemCode1.Text = ""
                hdfItemCode.Value = ""
            End If
        End Sub

        Protected Sub txtCustomer_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCustomer.TextChanged

            'RadgvFullCust.Visible = False
            'trSRC.Visible = False

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
        Protected Sub txtToBP_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtToBP.TextChanged

            lblMsg.Text = ""
            hdfToBPID.Value = ""
            GetBPID()

        End Sub

        Private Sub radSave_Click(sender As Object, e As EventArgs) Handles radSave.Click
            lblMsg.Text = ""

            'If NullCheck() = False Then Return

            Dim ret As Boolean
            Dim strmsg As String = ""
            Dim count As Integer = 0

            For Each item As GridDataItem In RadgvFullCust.MasterTableView.Items
                Dim tableView As GridTableView = CType(item.ChildItem.NestedTableViews(0), GridTableView)

                For Each childItem As GridDataItem In tableView.Items
                    Dim chkselect As CheckBox = CType(childItem.FindControl("chkselect"), CheckBox)
                    If chkselect.Checked = True Then
                        count = count + 1
                        'Dim txtQty As TextBox = DirectCast(childItem.FindControl("txtQuantity"), TextBox)
                        'If txtQty.Text.Trim = "" Then
                        '    lblMsg.Text = "Please enter quantity for the selected item(s)"
                        '    Exit Sub
                        'End If
                        Exit For
                    End If
                Next
            Next

            If count <= 0 Then
                lblMsg.Text = "Please select Item(s)"
                Exit Sub
            End If

            Try
                For Each item As GridDataItem In RadgvFullCust.MasterTableView.Items
                    Dim tableView As GridTableView = CType(item.ChildItem.NestedTableViews(0), GridTableView)

                    'Dim CustomerID As Integer = item.GetDataKeyValue("CustomerID").ToString()
                    Dim CustomerID As Integer = item("CustomerID").Text
                    'Dim BranchID As Integer = item("BranchID").Text      'item.GetDataKeyValue("BranchID").ToString()
                    Dim CompanyID As Integer = item("CompanyID").Text
                    'Dim ToCompanyID As Integer = item("ToCompanyID").Text
                    Dim strTransactionNumber As String = ""

                    For Each childItem As GridDataItem In tableView.Items
                        Dim chkselect As CheckBox = CType(childItem.FindControl("chkselect"), CheckBox)

                        If chkselect.Checked = True Then
                            Dim ItemID As Integer = childItem.GetDataKeyValue("ItemID").ToString()
                            'Dim txtQuantity As TextBox = DirectCast(childItem.FindControl("txtQuantity"), TextBox)
                            Dim Barcode As String = childItem("Barcode").Text

                            Dim Qty As Integer = 1

                            'Create new instance of the class to store the aspx controls data...

                            Dim objTabKFullCustomer As New PropertyTabKFullKegReturnsCustomer
                            objTabKFullCustomer.CustomerID = CustomerID 'hdfCustomerID.Value
                            objTabKFullCustomer.BranchID = hdfToBPID.Value
                            'Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                            'Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                            'Dim txtBarcode As TextBox = DirectCast(item.FindControl("txtBarcode"), TextBox)
                            objTabKFullCustomer.ItemID = ItemID
                            objTabKFullCustomer.CompanyID = CompanyID 'CInt(rcbFromCmpny.SelectedValue)
                            objTabKFullCustomer.ToCompanyID = CInt(rcbToCmpny.SelectedValue)
                            objTabKFullCustomer.Quantity = Qty
                            objTabKFullCustomer.ReturnDate = dpReturnDate.SelectedDate
                            objTabKFullCustomer.ReturnBy = objuserrole("employeeID")
                            objTabKFullCustomer.Barcode = Barcode
                            objTabKFullCustomer.Batch = True

                            If strTransactionNumber = "" Then
                                objTabKFullCustomer.TransactionNumber = Nothing
                            Else
                                objTabKFullCustomer.TransactionNumber = strTransactionNumber
                            End If

                            strTransactionNumber = clsTabKFullKegReturnsCustomer.SaveBulk(objTabKFullCustomer, objuserInfo("schemaName"))
                            If strTransactionNumber = "-1" Then
                                strTransactionNumber = ""
                                ret = False
                            Else
                                ret = True
                            End If

                        End If
                    Next
                Next
            Catch ex As Exception
                lblMsg.Text = "Error: " & ex.Message.ToString
                ret = False
                Exit Sub
            End Try

            'RadgvFullCust.Rebind()

            If ret = True Then
                'redirects to list page if record is saved successfully.
                'Response.Redirect("FrmFullKegReturnCustomer.aspx?process=Y&mode=A")
                RadgvFullCust.Rebind()
                Clear()
                lblMsg.Text = "Record(s) saved successfully"
            Else
                lblMsg.Text = Resources.lang.msgError
            End If

        End Sub

        Protected Sub RadgvFullCust_HeaderCheckChanged(sender As Object, e As EventArgs)
            CheckUncheckAll()
        End Sub

        Private Sub Clear()
            rcbToCmpny.SelectedIndex = 0
            txtToBP.Text = ""
            'hdfToBPID.Value = ""
            dpReturnDate.Clear()

            'txtOrderNo.Text = ""
            'hdfCustomerID.Value = ""
            'hdfItemCode.Value = ""
            'txtItemCode1.Text = ""
            'txtCustomer.Text = ""
        End Sub

        Private Sub CheckUncheckAll()

            For Each item As GridDataItem In RadgvFullCust.MasterTableView.Items
                Dim tableView As GridTableView = CType(item.ChildItem.NestedTableViews(0), GridTableView)
                Dim chkselectAll As CheckBox = CType(item.FindControl("chkselectALL"), CheckBox)

                For Each childItem As GridDataItem In tableView.Items

                    Dim chkselect As CheckBox = CType(childItem.FindControl("chkselect"), CheckBox)

                    If chkselectAll.Checked = True Then
                        chkselect.Checked = True
                    Else
                        chkselect.Checked = False
                    End If
                Next
            Next

        End Sub

        Private Function NullCheck() As Boolean

            If rcbToCmpny.SelectedIndex < 1 Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alrtCT", "alert('Please select To Company');", True)
                rcbToCmpny.Focus()
                Return False
            End If

            GetBPID()
            If hdfToBPID.Value.Length < 1 Then
                Return False
            End If

            Return True

        End Function

        Private Sub GetBPID()

            If rcbToCmpny.SelectedIndex > 0 Then
                Dim BPID As Integer
                BPID = clsValidations.GetLookUpIDByCode(rcbToCmpny.SelectedValue, txtToBP.Text, "B", objuserInfo("schemaName"))

                If BPID > 0 Then
                    hdfToBPID.Value = BPID
                Else
                    lblMsg.Text = "Please select the Branch Plant."
                    txtToBP.Focus()
                End If
            Else
                lblMsg.Text = "Please select the To Company."
                txtToBP.Text = ""
                hdfToBPID.Value = ""
            End If

        End Sub

        Private Sub rcbToCmpny_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbToCmpny.SelectedIndexChanged
            lblMsg.Text = ""
            AddContextKey()

            txtToBP.Text = ""
            hdfToBPID.Value = ""

            If rcbToCmpny.SelectedIndex > 0 Then
                txtToBP.Focus()
            End If

        End Sub

    End Class

End Namespace