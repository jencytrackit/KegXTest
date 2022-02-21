Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Public Class frmAddEditFullKegReturn_Customer
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim dt As New DataTable 'Datatable to store the item details
        Dim objuserrole As New Hashtable()
        Private mid As String = ""
        Dim objUserDetails As New Hashtable()
        Dim objUserRoleDetails As New Hashtable()
        Private CID As Int32 = 0
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            lblMsg.Text = ""
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If

            If Not Request.QueryString("id") Is Nothing And Not Request.QueryString("cid") Is Nothing Then
                mid = Request.QueryString("id")
                CID = Request.QueryString("cid")
            End If

            If objuserInfo Is Nothing Then
                Return
            End If
            If Not IsPostBack Then
                dpReturnDate.MaxDate = DateTime.Today
                dpReturnDate.SelectedDate = DateTime.Today
                If Not Request.QueryString("id") Is Nothing And Not Request.QueryString("cid") Is Nothing Then
                    EditMode(mid, CID)
                Else

                    'bind company drop down 

                    If Not objuserInfo Is Nothing Then
                        If Convert.ToString(objUserDetails("employeeNo")).ToUpper() = "PARTNER" Then
                            rcbFromCmpny.Visible = False

                        Else
                            If objuserInfo("isOrganisation") = "1" And Convert.ToString(objuserInfo("userName")).ToUpper() = "SUPERADMIN" Then
                                rcbFromCmpny.Visible = False
                            Else
                                BindCompany()
                                rcbFromCmpny.Visible = True
                            End If

                        End If


                    End If
                    'end binding

                End If

            End If
            AddContextKey()
        End Sub
        Private Sub AddContextKey()
            If objuserInfo.Count > 1 And rcbFromCmpny.SelectedIndex > 0 Then
                txtItemCode_AutoCompleteExtender.ContextKey = rcbFromCmpny.SelectedValue & "," & objuserInfo("schemaName")
                AutoCompleteExtender_txtCustomer.ContextKey = rcbFromCmpny.SelectedValue & "," & objuserInfo("schemaName")
            Else
                txtItemCode_AutoCompleteExtender.ContextKey = ""
                AutoCompleteExtender_txtCustomer.ContextKey = ""
            End If
            If objuserInfo.Count > 1 And rcbToCmpny.SelectedIndex > 0 Then
                AutoCompleteExtender_txtToBP.ContextKey = rcbToCmpny.SelectedValue & "," & objuserInfo("schemaName")
            Else
                AutoCompleteExtender_txtToBP.ContextKey = ""
            End If
        End Sub
        Private Sub BindCompany()
            rcbFromCmpny.Items.Clear()
            Dim ds As New DataSet
            ds = clsTabMOrganization.GetAllTabMOrganizationByEmployeeID(objuserrole("employeeID"), objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                ' bind FKom company
                rcbFromCmpny.DataSource = ds
                rcbFromCmpny.DataTextField = "CompanyName"
                rcbFromCmpny.DataValueField = "CompanyID"
                rcbFromCmpny.DataBind()

                ' bind to company
                rcbToCmpny.DataSource = ds
                rcbToCmpny.DataTextField = "CompanyName"
                rcbToCmpny.DataValueField = "CompanyID"
                rcbToCmpny.DataBind()
            End If

            rcbFromCmpny.Items.Insert(0, New RadComboBoxItem("--Select One--"))
            rcbToCmpny.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        End Sub

        Private Sub txtItemCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItemCode.TextChanged

            ClearFields()
            If txtItemCode.Text.Length < 1 Then Return
            If rcbFromCmpny.SelectedIndex <= 0 Then

                lblMsg.Text = "Please select the Company"
                rcbFromCmpny.Focus()
                Return
            End If
            If rcbToCmpny.SelectedIndex < 1 Then
                lblMsg.Text = "Please select the To Company"
                rcbToCmpny.Focus()
                Return
            End If
            Dim strItemCode As String = Nothing
            If txtItemCode.Text.IndexOf(" -") < 1 Then
                lblMsg.Text = "Please Enter the Correct Item Details"
                txtItemCode.Focus()
                Return
            End If

            strItemCode = Left(txtItemCode.Text, txtItemCode.Text.IndexOf(" -"))
            txtItemCode.Text = strItemCode

            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)

            Dim VALITEM As Boolean = clsTabUOMMaster.ItemCodeValForTwoCompanies(Trim(txtItemCode.Text), CInt(rcbFromCmpny.SelectedValue), CInt(rcbToCmpny.SelectedValue), objuserInfo("schemaName"))
            If VALITEM = True Then
                Dim ds As DataSet = clsTabUOMMaster.GetTabUOMMasterByItemCode(Trim(txtItemCode.Text), objuserInfo("schemaName"), strCompanyId) 'Get the item details by Item code
                If ds.Tables(0).Rows.Count > 0 Then
                    txtItemDesc.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName1"))
                    txtItemName2.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName2"))
                    hdfItemID.Value = Convert.ToString(ds.Tables(0).Rows(0)("ItemID"))
                    txtUOM.Text = Convert.ToString(ds.Tables(0).Rows(0)("UOM"))
                    txtBarcode.Focus()
                Else

                End If
            Else
                lblMsg.Text = "Item Code does not exists for the ToCompany"
                txtItemCode.Focus()
                Exit Sub
            End If

        End Sub

        Private Sub ClearFields()
            lblMsg.Text = ""
            txtItemDesc.Text = ""
            txtItemID.Text = ""
            txtQuantity.Text = ""
            txtItemName2.Text = ""
            txtBarcode.Text = ""
            txtUOM.Text = ""
        End Sub
        Private Function NullCheck() As Boolean
            If rcbFromCmpny.SelectedIndex < 1 Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alrtCF", "alert('Please select Company');", True)
                rcbFromCmpny.Focus()
                Return False
            End If

            If rcbToCmpny.SelectedIndex < 1 Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alrtCT", "alert('Please select To Company');", True)
                rcbToCmpny.Focus()
                Return False
            End If
            GetCustomerID()
            If hdfCustomerID.Value.Length < 1 Then
                Return False
            End If

            GetBPID()
            If hdfToBPID.Value.Length < 1 Then
                Return False
            End If


            Return True
        End Function
        Private Sub EditMode(ByVal mid As String, ByVal CID As Int32)
            RadgvFullKegTransfer.DataBind()
        End Sub
        Private Sub RadgvFullKegTransfer_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvFullKegTransfer.NeedDataSource
            If Not mid = "" And Not CID = 0 Then
                Dim strCompanyId = CID
                BindCompany()
                Dim ds = clsTabKFullKegReturnsCustomer.GetAllTabKFullKegReturnCustomerByCompanyID(strCompanyId, objuserInfo("schemaName"), mid)
                If Not ds Is Nothing Then
                    rcbFromCmpny.SelectedValue = ds.Tables(0).Rows(0)("CompanyID")
                    hdfCustomerID.Value = ds.Tables(0).Rows(0)("CustomerID")
                    txtCustomer.Text = ds.Tables(0).Rows(0)("CustomerName")
                    txtCustomer.Enabled = False
                    rcbFromCmpny.Enabled = False
                    dpReturnDate.Enabled = False
                    txtTransactionNumber.Text = ds.Tables(0).Rows(0)("TransactionNumber")
                    dpReturnDate.SelectedDate = Convert.ToDateTime(ds.Tables(0).Rows(0)("ReturnDate"))
                    RadgvFullKegTransfer.DataSource = ds
                    dt = ds.Tables(0)
                    ViewState("table") = dt
                    AddContextKey()
                End If
            End If
        End Sub
        Protected Sub rcbFromCmpny_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbFromCmpny.SelectedIndexChanged
            lblMsg.Text = ""
            AddContextKey()

            hdfCustomerID.Value = ""
            txtCustomer.Text = ""
            txtItemCode.Text = ""
            ClearFields()
            If rcbFromCmpny.SelectedIndex > 0 Then
                txtCustomer.Focus()
            End If

        End Sub
        Protected Sub rcbToCmpny_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbToCmpny.SelectedIndexChanged
            lblMsg.Text = ""
            AddContextKey()

            hdfToBPID.Value = ""
            txtToBP.Text = ""
            txtItemCode.Text = ""
            ClearFields()
            If rcbToCmpny.SelectedIndex > 0 Then
                txtToBP.Focus()
            End If
        End Sub
        Private Sub txtCustomer_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustomer.TextChanged
            lblMsg.Text = ""
            hdfCustomerID.Value = ""
            txtItemCode.Text = ""
            ClearFields()
            GetCustomerID()
        End Sub

        Private Sub txtToBP_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtToBP.TextChanged
            lblMsg.Text = ""
            hdfToBPID.Value = ""
            txtItemCode.Text = ""
            ClearFields()
            GetBPID()

        End Sub
        Private Sub GetCustomerID()

            If rcbFromCmpny.SelectedIndex > 0 Then
                Dim CustomerID As Integer
                CustomerID = clsValidations.GetLookUpIDByCode(rcbFromCmpny.SelectedValue, txtCustomer.Text, "C", objuserInfo("schemaName"))

                If CustomerID > 0 Then
                    hdfCustomerID.Value = CustomerID
                Else
                    lblMsg.Text = "Please select the Customer."
                    txtCustomer.Focus()
                End If
            Else
                lblMsg.Text = "Please select Company."
                txtCustomer.Text = ""
                hdfCustomerID.Value = ""
            End If
        End Sub
        Private Sub GetBPID()

            If rcbFromCmpny.SelectedIndex > 0 Then
                Dim BPID As Integer
                BPID = clsValidations.GetLookUpIDByCode(rcbToCmpny.SelectedValue, txtToBP.Text, "B", objuserInfo("schemaName"))

                If BPID > 0 Then
                    hdfToBPID.Value = BPID
                Else
                    lblMsg.Text = "Please select the BranchPlant."
                    txtToBP.Focus()
                End If
            Else
                lblMsg.Text = "Please select the To Company."
                txtToBP.Text = ""
                hdfToBPID.Value = ""
            End If
        End Sub

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

            lblMsg.Text = ""
            If dpReturnDate.SelectedDate Is Nothing Then
                lblMsg.Text = "Return Date is required"
                Exit Sub
            End If

            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)

            BindData()
            txtItemCode.Focus()
        End Sub
        Private Sub BindData()
            'This method is called to add the Item Details to grid
            dt.Columns.Add("ItemID", GetType(String))
            dt.Columns.Add("ItemCode", GetType(String))
            dt.Columns.Add("ItemName1", GetType(String))
            dt.Columns.Add("ItemName2", GetType(String))
            dt.Columns.Add("UOM", GetType(String))
            dt.Columns.Add("Quantity", GetType(String))
            dt.Columns.Add("FCOrderID", GetType(String))
            dt.Columns.Add("Barcode", GetType(String))
            If ViewState("table") IsNot Nothing Then
                dt = DirectCast(ViewState("table"), DataTable)
            End If
            Dim row1 As DataRow = dt.NewRow()
            row1("ItemID") = hdfItemID.Value
            row1("ItemCode") = txtItemCode.Text
            row1("ItemName1") = txtItemDesc.Text
            row1("ItemName2") = txtItemName2.Text
            row1("UOM") = txtUOM.Text
            row1("Quantity") = txtQuantity.Text
            row1("FCOrderID") = "0"
            row1("Barcode") = txtBarcode.Text

            If dt.Rows.Count > 0 Then
                Dim count As Int32 = 0
                For Each row As DataRow In dt.Rows
                    If row("ItemID") = row1("ItemID") Then
                        If row("Barcode") = row1("Barcode") Then
                            count = count + 1
                            GoTo NextRecord
                        End If
                    End If
                Next row
NextRecord:
                If count > 0 Then
                    lblMsg.Text = "Item already exist"
                    txtItemCode.Focus()
                    Exit Sub
                Else
                    lblMsg.Text = ""
                End If
            End If
            dt.Rows.Add(row1)
            ViewState("table") = dt
            BindGrid()
            ClearFields()
            txtItemCode.Text = ""
            rcbFromCmpny.Enabled = False
            txtCustomer.Enabled = False
            dpReturnDate.Enabled = False
            'rcbToCmpny.Enabled = False
            'txtToBP.Enabled = False
            For Each item As GridDataItem In RadgvFullKegTransfer.Items
                Dim txtBar As TextBox = DirectCast(item.FindControl("txtBarcode"), TextBox)
                If txtBarcode.Text = "" Then
                    txtBar.Enabled = False
                Else
                    txtBar.Enabled = True
                End If

            Next
        End Sub
        Private Sub BindGrid()
            RadgvFullKegTransfer.DataSource = dt
            RadgvFullKegTransfer.DataBind()
        End Sub

        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            lblMsg.Text = ""

            If NullCheck() = False Then Return

            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            Dim ret As Boolean
            'Validate grid has atleast one record 
            If RadgvFullKegTransfer.Items.Count <= 0 Then
                lblMsg.Text = "Atleast one item is required"
                txtItemCode.Focus()
                Exit Sub
            End If
            Dim strmsg As String = ""
            'Validate whether the quantity changed is more 
            For Each item As GridDataItem In RadgvFullKegTransfer.Items
                Dim lblItemID As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                Dim strItemCode = item("ItemCode").Text

            Next
            If strmsg.Length > 0 Then
                lblMsg.Text = strmsg
                txtQuantity.Focus()
                Exit Sub
            End If
            If Not mid = "" Then
                For Each item As GridDataItem In RadgvFullKegTransfer.Items
                    'Create new instance of the class to store the aspx controls data...
                    Dim objTabKFullCustomer As New PropertyTabKFullKegReturnsCustomer
                    Dim lblFCOrderID As Label = DirectCast(item.FindControl("lblFCOrderID"), Label)
                    Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                    Dim lblPrevQty As Label = DirectCast(item.FindControl("lblPrevQty"), Label)
                    Dim txtBarcode As TextBox = DirectCast(item.FindControl("txtBarcode"), TextBox)

                    objTabKFullCustomer.FCOrderID = lblFCOrderID.Text
                    objTabKFullCustomer.CustomerID = hdfCustomerID.Value
                    objTabKFullCustomer.CompanyID = CInt(rcbFromCmpny.SelectedValue)
                    objTabKFullCustomer.BranchID = hdfToBPID.Value
                    objTabKFullCustomer.ToCompanyID = CInt(rcbToCmpny.SelectedValue)
                    objTabKFullCustomer.ItemID = itemid.Text
                    objTabKFullCustomer.Quantity = txtQuantity.Text
                    objTabKFullCustomer.ReturnDate = dpReturnDate.SelectedDate
                    objTabKFullCustomer.ReturnBy = objuserrole("employeeID")
                    objTabKFullCustomer.TransactionNumber = txtTransactionNumber.Text
                    objTabKFullCustomer.ModifiedBy = objuserrole("employeeID")
                    objTabKFullCustomer.Barcode = txtBarcode.Text
                    objTabKFullCustomer.Batch = True
                    'record is updated
                    ret = clsTabKFullKegReturnsCustomer.UpdateTabKFullKegReturnCustomer(objTabKFullCustomer, objuserInfo("schemaName"), lblPrevQty.Text)

                Next
            Else
                For Each item As GridDataItem In RadgvFullKegTransfer.Items
                    'Create new instance of the class to store the aspx controls data...
                    Dim objTabKFullCustomer As New PropertyTabKFullKegReturnsCustomer
                    objTabKFullCustomer.CustomerID = hdfCustomerID.Value
                    objTabKFullCustomer.BranchID = hdfToBPID.Value
                    Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                    Dim txtBarcode As TextBox = DirectCast(item.FindControl("txtBarcode"), TextBox)
                    objTabKFullCustomer.ItemID = itemid.Text
                    objTabKFullCustomer.CompanyID = CInt(rcbFromCmpny.SelectedValue)
                    objTabKFullCustomer.ToCompanyID = CInt(rcbToCmpny.SelectedValue)
                    objTabKFullCustomer.Quantity = txtQuantity.Text
                    objTabKFullCustomer.ReturnDate = dpReturnDate.SelectedDate
                    objTabKFullCustomer.ReturnBy = objuserrole("employeeID")
                    objTabKFullCustomer.Barcode = txtBarcode.Text
                    objTabKFullCustomer.Batch = True
                    If txtTransactionNumber.Text = "" Then
                        'Dim ds = clsTabKEmptyCustomer.GetMaxTransactionNumber(objuserInfo("schemaName"), 6, strCompanyId)
                        'If Not ds Is Nothing Then
                        '    If ds.Tables(0).Rows.Count > 0 Then
                        '        Dim str3 As String = ""
                        '        Dim str As String = ds.Tables(0).Rows(0)(0)
                        '        str3 = str
                        '        'Dim str1 = str.Substring(2, str.Length - 2)
                        '        'Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                        '        'If str2.Length = 1 Then
                        '        '    str3 = "FK" + "0000000" + str2
                        '        'ElseIf str2.Length = 2 Then
                        '        '    str3 = "FK" + "000000" + str2
                        '        'ElseIf str2.Length = 3 Then
                        '        '    str3 = "FK" + "00000" + str2
                        '        'ElseIf str2.Length = 4 Then
                        '        '    str3 = "FK" + "0000" + str2
                        '        'ElseIf str2.Length = 5 Then
                        '        '    str3 = "FK" + "000" + str2
                        '        'ElseIf str2.Length = 6 Then
                        '        '    str3 = "FK" + "00" + str2
                        '        'ElseIf str2.Length = 7 Then
                        '        '    str3 = "FK" + "0" + str2
                        '        'ElseIf str2.Length >= 8 Then
                        '        '    str3 = "FK" + str2
                        '        'End If
                        '        objTabKFullCustomer.TransactionNumber = str3
                        '        txtTransactionNumber.Text = str3
                        '    Else
                        '        objTabKFullCustomer.TransactionNumber = "FK00000001"
                        '        txtTransactionNumber.Text = "FK00000001"
                        '    End If

                        'Else
                        '    objTabKFullCustomer.TransactionNumber = "FK00000001"
                        '    txtTransactionNumber.Text = "FK00000001"
                        'End If
                        objTabKFullCustomer.TransactionNumber = Nothing
                    Else
                        objTabKFullCustomer.TransactionNumber = txtTransactionNumber.Text
                    End If


                    'New record is inserted
                    txtTransactionNumber.Text = clsTabKFullKegReturnsCustomer.Save(objTabKFullCustomer, objuserInfo("schemaName"))
                    If txtTransactionNumber.Text = "-1" Then
                        txtTransactionNumber.Text = ""
                        ret = False
                    Else
                        ret = True
                    End If

                Next
            End If

            If (ret) And mid <> "" Then
                'redirects to list page if record is saved successfully.
                Response.Redirect("FrmFullKegReturnCustomer.aspx?process=Y&mode=E")
            ElseIf ret Then
                'redirects to list page if record is saved successfully.
                Response.Redirect("FrmFullKegReturnCustomer.aspx?process=Y&mode=A")
            Else
                lblMsg.Text = Resources.lang.msgError
            End If
        End Sub

        Private Sub txtBarcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBarcode.TextChanged
            If rcbFromCmpny.SelectedIndex > 0 Then
                Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
                If hdfCustomerID.Value <> "" And hdfItemID.Value <> "" Then
                    If txtBarcode.Text.Length > 0 Then
                        Dim VALBARCODE As Int32 = clsTabKFullKegReturnsCustomer.BarcodeValidationFullKegReturn(hdfItemID.Value, strCompanyId, hdfCustomerID.Value, Trim(txtBarcode.Text), objuserInfo("schemaName"))
                        If VALBARCODE = 0 Then
                            txtQuantity.Text = ""
                            txtQuantity.Enabled = True
                            lblMsg.Text = "Entered Barcode is not correct for selected Customer and Item"
                            Exit Sub
                        ElseIf VALBARCODE = 2 Then
                            lblMsg.Text = "Barcode Already exists"
                            Exit Sub
                        Else
                            lblMsg.Text = ""
                            txtQuantity.Text = VALBARCODE
                            txtQuantity.Enabled = False
                        End If

                        btnAdd.Focus()
                    Else
                        txtQuantity.Text = ""
                        txtQuantity.Enabled = True
                        txtQuantity.Focus()
                    End If

                Else
                    lblMsg.Text = "Please select the Customer and Item details."
                    txtCustomer.Focus()
                    Return
                End If
            Else
                lblMsg.Text = "Please select the Company"
                rcbFromCmpny.Focus()
                Return
            End If

        End Sub
    End Class
End Namespace