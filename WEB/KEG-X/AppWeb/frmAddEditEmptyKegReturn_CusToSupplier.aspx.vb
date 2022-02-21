
Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmAddEditEmptyKegReturn_CusToSupplier
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging
        Dim objuserrole As New Hashtable()
        Dim dt As New DataTable 'Datatable to store the item details
        Private mid As String = ""
        Dim objUserDetails As New Hashtable()
        Dim objUserRoleDetails As New Hashtable()
        Private CID As Int32 = 0

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session

            If objuserInfo Is Nothing Then
                Return
            End If

            If Not Request.QueryString("id") Is Nothing And Not Request.QueryString("cid") Is Nothing Then
                mid = Request.QueryString("id")
                CID = Request.QueryString("cid")
            End If
            If Not IsPostBack Then
                Me.Form.DefaultButton = btnAdd.UniqueID
                dtReturnDate.MaxDate = DateTime.Today
                dtReturnDate.SelectedDate = DateTime.Today

                If Not Request.QueryString("id") Is Nothing And Not Request.QueryString("cid") Is Nothing Then
                    EditMode(mid, CID)
                    'bind company drop down 
                Else

                    If Not objuserInfo Is Nothing Then
                        If Convert.ToString(objUserDetails("employeeNo")).ToUpper() = "PARTNER" Then

                            rcbFromCmpny.Visible = False

                        Else
                            If objuserInfo("isOrganisation") = "1" And Convert.ToString(objuserInfo("userName")).ToUpper() = "SUPERADMIN" Then
                                rcbFromCmpny.Visible = False

                            Else
                                BindCompany()
                                'rcbFromCmpny.SelectedValue = Session("SelectedCompanyID")
                                rcbFromCmpny.Visible = True

                            End If

                        End If


                    End If
                    'end binding
                End If
                'rcbCustomer.Items.Insert(0, New RadComboBoxItem("--Select One--"))
                'rcbToSupplier.Items.Insert(0, New RadComboBoxItem("--Select One--"))

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
                AutoCompleteExtender_txtSupplier.ContextKey = rcbToCmpny.SelectedValue & "," & objuserInfo("schemaName")
            Else
                AutoCompleteExtender_txtSupplier.ContextKey = ""
            End If

        End Sub
        Private Sub BindCompany()
            rcbFromCmpny.Items.Clear()
            rcbToCmpny.Items.Clear()
            Dim ds As New DataSet
            ds = clsTabMOrganization.GetAllTabMOrganizationByEmployeeID(objuserrole("employeeID"), objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                ' bind From company
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
        Private Sub EditMode(ByVal mid As String, ByVal CID As Int32)
            RadgvEmptyKegTransfer.DataBind()
        End Sub
        'Private Sub ObjDSCustomers_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjDSCustomers.Selecting
        '    Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
        '    If Not strCompanyId = "" Then
        '        e.InputParameters("CompanyID") = strCompanyId
        '        e.InputParameters("EmployeeID") = objuserrole("employeeID")
        '        e.InputParameters("SchemaName") = objuserInfo("schemaName")
        '    End If
        'End Sub

        'Private Sub ObjDSSupplier_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjDSSupplier.Selecting
        '    Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
        '    If Not strCompanyId = "" Then
        '        e.InputParameters("CompanyID") = strCompanyId
        '        e.InputParameters("EmployeeID") = objuserrole("employeeID")
        '        e.InputParameters("SchemaName") = objuserInfo("schemaName")
        '    End If
        'End Sub

        Private Sub txtItemCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItemCode.TextChanged

            ClearFields()
            If txtItemCode.Text.Length < 1 Then Return
            If rcbFromCmpny.SelectedIndex <= 0 Then
                'ClearFields()
                lblMsg.Text = "Please select the From Company"
                rcbFromCmpny.Focus()
                Return
            End If
            If rcbToCmpny.SelectedIndex < 1 Then
                'ClearFields()
                lblMsg.Text = "Please select the To Company"
                rcbToCmpny.Focus()
                Return
            End If
            Dim strItemCode As String = Nothing
            If txtItemCode.Text.IndexOf(" -") < 1 Then
                'ClearFields()
                lblMsg.Text = "Please Enter the Correct Item Details"
                txtItemCode.Focus()
                Return
            End If
            strItemCode = Left(txtItemCode.Text, txtItemCode.Text.IndexOf(" -"))
            txtItemCode.Text = strItemCode

            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)

            Dim VALITEM As Boolean = clsTabUOMMaster.ItemCodeValForTwoCompanies(Trim(txtItemCode.Text), CInt(rcbFromCmpny.SelectedValue), CInt(rcbToCmpny.SelectedValue), objuserInfo("schemaName"))
            If VALITEM = True Then
                Dim ds As DataSet = clsTabUOMMaster.GetTabUOMMasterByItemCode(Trim(txtItemCode.Text), objuserInfo("schemaName"), strCompanyId)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtItemDesc.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName1"))
                    txtItemName2.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName2"))
                    hdfItemID.Value = Convert.ToString(ds.Tables(0).Rows(0)("ItemID"))
                    txtUOM.Text = Convert.ToString(ds.Tables(0).Rows(0)("UOM"))
                    txtQuantity.Focus()
                Else
                    'ClearFields()
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
            txtItemName2.Text = ""
            hdfItemID.Value = ""
            txtUOM.Text = ""
            txtQuantity.Text = ""
        End Sub

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            lblMsg.Text = ""
            If dtReturnDate.SelectedDate Is Nothing Then
                lblMsg.Text = "Return date is required"
                dtReturnDate.Focus()
                Exit Sub
            End If
            'If rcbFromCmpny.SelectedIndex < 1 Then
            '    lblMsg.Text = "Please select the From Company"
            '    rcbFromCmpny.Focus()
            '    Return
            'End If
            'If hdfCustomerID.Value.Length < 1 Then
            '    lblMsg.Text = "Please select the Customer"
            '    txtCustomer.Focus()
            '    Return
            'End If
            'If hdfItemID.Value.Length < 1 Then
            '    lblMsg.Text = "Please select the Item Details"
            '    txtItemCode.Focus()
            '    Return
            'End If
            If NullCheck() = False Then Return

            'Validate with empty keg onhand balance
            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            Dim Count = clsTabKEmptyTransferBP.ValidateforOpeningBal(strCompanyId, hdfCustomerID.Value, 2, hdfItemID.Value, txtQuantity.Text, objuserInfo("schemaName"))
            If Count = 0 Then
                'lblMsg.Text = "Please enter Opening balance for selected customer and selected Item"
                'Exit Sub
            ElseIf Count = 1 Then
                'lblMsg.Text = "Quantity entered is greater than latest onhand quantity for slected customer and item.Enter correct quantity."
                'txtQuantity.Focus()
                'Exit Sub
            End If


            ''validate with empty keg onhand balance , check if there is balance on collectio date selected, if already exist check the onhand qantity should not be less than quantity selected

            'Dim CNT = clsTabKEmptyTransferBP.ValidateforNegativeOpeningBal(strCompanyId, rcbCustomer.SelectedValue, 2, txtItemID.Text, txtQuantity.Text, dtReturnDate.SelectedDate, objuserInfo("schemaName"))
            Dim CNT = clsTabKEmptyTransferBP.ValidateforNegativeOpeningBal(strCompanyId, hdfCustomerID.Value, 2, hdfItemID.Value, txtQuantity.Text, dtReturnDate.SelectedDate, objuserInfo("schemaName"))
            If CNT = 0 Then
                'lblMsg.Text = "Please enter Opening balance for selected Customer and selected Item for the selected collection date"
                'Exit Sub
            ElseIf Count = 1 Then
                'lblMsg.Text = "Quantity entered is greater than latest onhand quantity for slected Customer and item.Enter correct quantity."
                'txtQuantity.Focus()
                'Exit Sub
            End If

            BindData()
            
            txtItemCode.Focus()
        End Sub
        Private Sub BindData()
            dt.Columns.Add("ItemID", GetType(String))
            dt.Columns.Add("ItemCode", GetType(String))
            dt.Columns.Add("ItemName1", GetType(String))
            dt.Columns.Add("ItemName2", GetType(String))
            dt.Columns.Add("UOM", GetType(String))
            dt.Columns.Add("Quantity", GetType(String))
            dt.Columns.Add("ECSOrderID", GetType(String))
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
            row1("ECSOrderID") = "0"
            If dt.Rows.Count > 0 Then
                Dim count As Int32 = 0
                For Each row As DataRow In dt.Rows
                    If row("ItemID") = row1("ItemID") Then
                        count = count + 1
                        GoTo NextRecord
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
            rcbToCmpny.Enabled = False
            txtSupplier.Enabled = False
            'dtReturnDate.Enabled = False
        End Sub
        Private Sub BindGrid()
            RadgvEmptyKegTransfer.DataSource = dt
            RadgvEmptyKegTransfer.DataBind()
        End Sub

        Private Function NullCheck() As Boolean
            If rcbFromCmpny.SelectedIndex < 1 Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alrtCF", "alert('Please select From Company');", True)
                rcbFromCmpny.Focus()
                Return False
            End If
            If rcbToCmpny.SelectedIndex < 1 Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "alrtCT", "alert('Please select To Company');", True)
                rcbToCmpny.Focus()
                Return False
            End If
            'If hdfCustomerID.Value.Length < 1 Then
            GetCustomerID()
            If hdfCustomerID.Value.Length < 1 Then
                Return False
            End If
            'End If
            'If hdfSupplierID.Value.Length < 1 Then
            GetSupplierID()
            If hdfSupplierID.Value.Length < 1 Then
                Return False
            End If
            'End If
            Return True
        End Function

        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            lblMsg.Text = ""
            If NullCheck() = False Then Return

            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            Dim ret As Boolean
            'Validate grid has atleast one record 
            If RadgvEmptyKegTransfer.Items.Count <= 0 Then
                lblMsg.Text = "Atleast one item is required"
                txtItemCode.Focus()
                Exit Sub
            End If
            Dim strmsg As String = ""
            'Validate whether the quantity changed is more 
            For Each item As GridDataItem In RadgvEmptyKegTransfer.Items
                Dim lblItemID As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                Dim strItemCode = item("ItemCode").Text

                'Dim Count = clsTabKEmptyTransferBP.ValidateforOpeningBal(strCompanyId, rcbCustomer.SelectedValue, 2, lblItemID.Text, txtQuantity.Text, objuserInfo("schemaName"))
                Dim Count = clsTabKEmptyTransferBP.ValidateforOpeningBal(strCompanyId, hdfCustomerID.Value, 2, lblItemID.Text, txtQuantity.Text, objuserInfo("schemaName"))
                'If Count = 0 Or Count = 1 Then
                '    item.BackColor = Drawing.Color.OrangeRed
                '    If strmsg.Length <= 0 Then
                '        strmsg = strmsg + "Quantity entered is greater than latest onhand quantity for slected customer and item.Enter correct quantity for the item codes : " + strItemCode
                '    Else
                '        strmsg = strmsg + "," + strItemCode
                '    End If
                'Else
                '    item.BackColor = Drawing.Color.Transparent
                'End If
            Next
            If strmsg.Length > 0 Then
                lblMsg.Text = strmsg
                txtQuantity.Focus()
                Exit Sub
            End If
            If Not mid = "" Then
                For Each item As GridDataItem In RadgvEmptyKegTransfer.Items
                    Dim lblECSOrderID As Label = DirectCast(item.FindControl("lblECSOrderID"), Label)
                    Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                    Dim lblPrevQty As Label = DirectCast(item.FindControl("lblPrevQty"), Label)
                    'Create new instance of the class to store the aspx controls data...
                    Dim objTabKEmptyCustToSupp As New PropertyTabKEmptyCustToSupp
                    objTabKEmptyCustToSupp.ECSOrderID = lblECSOrderID.Text
                    objTabKEmptyCustToSupp.SupplierID = hdfSupplierID.Value ' rcbToSupplier.SelectedValue
                    objTabKEmptyCustToSupp.CustomerID = hdfCustomerID.Value 'rcbCustomer.SelectedValue
                    objTabKEmptyCustToSupp.CompanyID = CInt(rcbFromCmpny.SelectedValue)
                    objTabKEmptyCustToSupp.ToCompanyID = CInt(rcbToCmpny.SelectedValue)
                    objTabKEmptyCustToSupp.ItemID = itemid.Text
                    objTabKEmptyCustToSupp.Quantity = txtQuantity.Text
                    objTabKEmptyCustToSupp.Batch = True
                    objTabKEmptyCustToSupp.TransferDate = dtReturnDate.SelectedDate
                    objTabKEmptyCustToSupp.TransferBy = objuserrole("employeeID")
                    objTabKEmptyCustToSupp.ShippingLine = txtShippingLine.Text
                    objTabKEmptyCustToSupp.SerialNo = txtSerialNo.Text
                    objTabKEmptyCustToSupp.ContainerNumber = txtContainerNO.Text
                    If Not dtBOLDate.DateInput.Text = "" Then
                        objTabKEmptyCustToSupp.BOLDate = dtBOLDate.SelectedDate
                    End If
                    objTabKEmptyCustToSupp.BOLNumber = txtBOLNo.Text
                    objTabKEmptyCustToSupp.TransactionNumber = txtTransactionNumber.Text
                    objTabKEmptyCustToSupp.ModifiedBy = objuserrole("employeeID")

                    ret = clsTabKEmptyCustToSupp.UpdateTabKEmptyCustToSupp(objTabKEmptyCustToSupp, objuserInfo("schemaName"), lblPrevQty.Text)
                Next
            Else
                For Each item As GridDataItem In RadgvEmptyKegTransfer.Items
                    'Create new instance of the class to store the aspx controls data...
                    Dim objTabKEmptyCustToSupp As New PropertyTabKEmptyCustToSupp
                    objTabKEmptyCustToSupp.SupplierID = hdfSupplierID.Value ' rcbToSupplier.SelectedValue
                    objTabKEmptyCustToSupp.CustomerID = hdfCustomerID.Value ' rcbCustomer.SelectedValue
                    objTabKEmptyCustToSupp.CompanyID = CInt(rcbFromCmpny.SelectedValue)
                    objTabKEmptyCustToSupp.ToCompanyID = CInt(rcbToCmpny.SelectedValue)
                    Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                    objTabKEmptyCustToSupp.ItemID = itemid.Text
                    objTabKEmptyCustToSupp.Quantity = txtQuantity.Text
                    objTabKEmptyCustToSupp.Batch = True
                    objTabKEmptyCustToSupp.TransferDate = dtReturnDate.SelectedDate
                    objTabKEmptyCustToSupp.TransferBy = objuserrole("employeeID")
                    objTabKEmptyCustToSupp.ShippingLine = txtShippingLine.Text
                    objTabKEmptyCustToSupp.SerialNo = txtSerialNo.Text
                    objTabKEmptyCustToSupp.ContainerNumber = txtContainerNO.Text
                    If Not dtBOLDate.DateInput.Text = "" Then
                        objTabKEmptyCustToSupp.BOLDate = dtBOLDate.SelectedDate
                    End If
                    objTabKEmptyCustToSupp.BOLNumber = txtBOLNo.Text
                    If txtTransactionNumber.Text = "" Then
                        'Dim ds = clsTabKEmptyCustomer.GetMaxTransactionNumber(objuserInfo("schemaName"), 3, strCompanyId)
                        'If Not ds Is Nothing Then
                        '    If ds.Tables(0).Rows.Count > 0 Then
                        '        Dim str3 As String = ""
                        '        Dim str As String = ds.Tables(0).Rows(0)(0)
                        '        Dim str1 = str.Substring(2, str.Length - 2)
                        '        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                        '        If str2.Length = 1 Then
                        '            str3 = "CS" + "0000000" + str2
                        '        ElseIf str2.Length = 2 Then
                        '            str3 = "CS" + "000000" + str2
                        '        ElseIf str2.Length = 3 Then
                        '            str3 = "CS" + "00000" + str2
                        '        ElseIf str2.Length = 4 Then
                        '            str3 = "CS" + "0000" + str2
                        '        ElseIf str2.Length = 5 Then
                        '            str3 = "CS" + "000" + str2
                        '        ElseIf str2.Length = 6 Then
                        '            str3 = "CS" + "00" + str2
                        '        ElseIf str2.Length = 7 Then
                        '            str3 = "CS" + "0" + str2
                        '        ElseIf str2.Length >= 8 Then
                        '            str3 = "CS" + str2
                        '        End If
                        '        objTabKEmptyCustToSupp.TransactionNumber = str3
                        '        txtTransactionNumber.Text = str3
                        '    Else
                        '        objTabKEmptyCustToSupp.TransactionNumber = "CS00000001"
                        '        txtTransactionNumber.Text = "CS00000001"
                        '    End If
                        'Else
                        '    objTabKEmptyCustToSupp.TransactionNumber = "CS00000001"
                        '    txtTransactionNumber.Text = "CS00000001"
                        'End If
                        objTabKEmptyCustToSupp.TransactionNumber = Nothing
                    Else
                        objTabKEmptyCustToSupp.TransactionNumber = txtTransactionNumber.Text
                    End If

                    txtTransactionNumber.Text = clsTabKEmptyCustToSupp.Save(objTabKEmptyCustToSupp, objuserInfo("schemaName"))

                    If txtTransactionNumber.Text = "-1" Then
                        txtTransactionNumber.Text = ""
                        ret = False
                    Else
                        ret = True
                    End If
                Next
            End If

            If (ret) And mid <> "" Then
                Response.Redirect("frmEmptyKegReturn_CusToSupplier.aspx?process=Y&mode=E")
            ElseIf (ret) Then
                'Insert mode and creation is success and goto List page
                Response.Redirect("frmEmptyKegReturn_CusToSupplier.aspx?process=Y&mode=A")
            Else
                lblMsg.Text = Resources.lang.msgError
            End If

        End Sub

        Private Sub RadgvEmptyKegTransfer_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvEmptyKegTransfer.NeedDataSource
            If Not mid = "" And Not CID = 0 Then
                Dim strCompanyId = CID
                BindCompany()
                Dim ds = clsTabKEmptyCustToSupp.GetAllTabKEmptyCustToSuppByCompanyID(strCompanyId, objuserInfo("schemaName"), mid)
                If Not ds Is Nothing Then
                    rcbFromCmpny.SelectedValue = ds.Tables(0).Rows(0)("CompanyID")
                    
                    hdfCustomerID.Value = ds.Tables(0).Rows(0)("CustomerID")
                    txtCustomer.Text = ds.Tables(0).Rows(0)("CustomerName")
                    txtCustomer.Enabled = False

                    rcbToCmpny.SelectedValue = ds.Tables(0).Rows(0)("ToCompanyID")
                    
                    hdfSupplierID.Value = ds.Tables(0).Rows(0)("SupplierID")
                    txtSupplier.Text = ds.Tables(0).Rows(0)("SupplierName")
                    txtSupplier.Enabled = False

                    rcbFromCmpny.Enabled = False
                    rcbToCmpny.Enabled = False
                    dtReturnDate.Enabled = False
                    txtTransactionNumber.Text = ds.Tables(0).Rows(0)("TransactionNumber")
                    txtShippingLine.Text = ds.Tables(0).Rows(0)("ShippingLine")
                    txtSerialNo.Text = ds.Tables(0).Rows(0)("SerialNo")
                    txtBOLNo.Text = ds.Tables(0).Rows(0)("BOLNumber")
                    txtContainerNO.Text = ds.Tables(0).Rows(0)("ContainerNumber")
                    dtReturnDate.SelectedDate = Convert.ToDateTime(ds.Tables(0).Rows(0)("TransferDate"))

                    If Not IsDBNull(ds.Tables(0).Rows(0)("BOLDate")) Then
                        If Not ds.Tables(0).Rows(0)("BOLDate") = "" Then
                            dtBOLDate.SelectedDate = Convert.ToDateTime(ds.Tables(0).Rows(0)("BOLDate"))
                        End If
                    End If

                    RadgvEmptyKegTransfer.DataSource = ds
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

        Private Sub rcbToCmpny_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbToCmpny.SelectedIndexChanged
            lblMsg.Text = ""
            AddContextKey()

            hdfSupplierID.Value = ""
            txtSupplier.Text = ""
            txtItemCode.Text = ""
            ClearFields()
            If rcbToCmpny.SelectedIndex > 0 Then
                txtSupplier.Focus()
            End If
        End Sub

        Private Sub txtCustomer_TextChanged(sender As Object, e As System.EventArgs) Handles txtCustomer.TextChanged
            lblMsg.Text = ""
            hdfCustomerID.Value = ""
            txtItemCode.Text = ""
            ClearFields()
            GetCustomerID()
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
                lblMsg.Text = "Please select the From Company."
                txtCustomer.Text = ""
                hdfCustomerID.Value = ""
            End If
        End Sub

        Private Sub txtSupplier_TextChanged(sender As Object, e As System.EventArgs) Handles txtSupplier.TextChanged
            lblMsg.Text = ""
            hdfSupplierID.Value = ""
            txtItemCode.Text = ""
            ClearFields()
            GetSupplierID()
            txtBOLNo.Focus()
        End Sub
        Private Sub GetSupplierID()
           
            If rcbFromCmpny.SelectedIndex > 0 Then
                Dim SupplierID As Integer
                SupplierID = clsValidations.GetLookUpIDByCode(rcbToCmpny.SelectedValue, txtSupplier.Text, "S", objuserInfo("schemaName"))

                If SupplierID > 0 Then
                    hdfSupplierID.Value = SupplierID
                Else
                    lblMsg.Text = "Please select the Supplier."
                    txtSupplier.Focus()
                End If
            Else
                lblMsg.Text = "Please select the To Company."
                txtSupplier.Text = ""
                hdfSupplierID.Value = ""
            End If
        End Sub
    End Class
End Namespace
