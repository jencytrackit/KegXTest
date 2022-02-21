'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmAddEditEmptyKegReturn_CusToBP.aspx
'Created By         :
'File navigation    :
'Created Date       :November 18, 2013, 3:22:33 PM 
'Description        :This file is used to Add / Edit the records...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmAddEditEmptyKegReturn_CusToBP
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim dt As New DataTable 'Datatable to store the item details
        Dim objuserrole As New Hashtable()
        Private mid As String = ""
        Dim objUserDetails As New Hashtable()
        Dim objUserRoleDetails As New Hashtable()

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            lblMsg.Text = ""
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
            If Not Request.QueryString("id") Is Nothing Then
                mid = Request.QueryString("id")
            End If
            If objuserInfo Is Nothing Then
                Return
            End If
            If Not IsPostBack Then
                dpCollectionDate.MaxDate = DateTime.Today
                dpCollectionDate.SelectedDate = DateTime.Today
                If Not Request.QueryString("id") Is Nothing Then
                    EditMode(mid)
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
                                'rcbFromCmpny.SelectedValue = Session("SelectedCompanyID")
                                rcbFromCmpny.Visible = True
                            End If

                        End If


                    End If
                    'end binding

                End If
                'rcbCustomer.Items.Insert(0, New RadComboBoxItem("--Select One--"))
                'rcbToBP.Items.Insert(0, New RadComboBoxItem("--Select One--"))

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
            'rcbCompany.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        End Sub

        'Private Sub ObjDSCustomers_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjDSCustomers.Selecting
        '    lblMsg.Text = ""
        '    'Pass the logged in employeeid and companyid and retrieve the Customers associated for that company and employee
        '    Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
        '    If Not strCompanyId = "" Then
        '        e.InputParameters("CompanyID") = strCompanyId
        '        e.InputParameters("EmployeeID") = objuserrole("employeeID")
        '        e.InputParameters("SchemaName") = objuserInfo("schemaName")
        '    End If
        'End Sub

        'Private Sub ObjBranchPlant_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjBranchPlant.Selecting
        '    lblMsg.Text = ""
        '    'Pass the logged in employeeid and companyid and retrieve the BranchPlants associated for that company and employee
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
                Dim ds As DataSet = clsTabUOMMaster.GetTabUOMMasterByItemCode(Trim(txtItemCode.Text), objuserInfo("schemaName"), strCompanyId) 'Get the item details by Item code
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

        'Private Sub rcbCustomer_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbCustomer.DataBound
        '    rcbCustomer.Items.Insert(0, New RadComboBoxItem("--Select One--"))

        'End Sub

        'Private Sub rcbToBP_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbToBP.DataBound
        '    rcbToBP.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        'End Sub

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            'Validate with empty keg onhand balance
            lblMsg.Text = ""
            If dpCollectionDate.SelectedDate Is Nothing Then
                lblMsg.Text = "Collection date is required"
                Exit Sub
            End If

            If NullCheck() = False Then Return

            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            If txtSerialNo.Text.Trim.Length > 0 Then
                Dim VALBARCODE As Int32 = clsTabKEmptyCustomer.ItemBarcodeValidation(hdfItemID.Value, strCompanyId, Trim(txtSerialNo.Text), objuserInfo("schemaName"))
                If VALBARCODE = 1 Then
                    lblMsg.Text = "Barcode is assigned for different item"
                    Exit Sub
                Else
                    lblMsg.Text = ""
                End If
            End If
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            Dim Count = clsTabKEmptyTransferBP.ValidateforOpeningBal(strCompanyId, hdfCustomerID.Value, 2, hdfItemID.Value, txtQuantity.Text, objuserInfo("schemaName"))
            If Count = 0 Then
                'lblMsg.Text = "Please enter Opening balance for selected Customer and selected Item"
                'Exit Sub
            ElseIf Count = 1 Then
                'lblMsg.Text = "Quantity entered is greater than latest onhand quantity for slected Customer and item.Enter correct quantity."
                'txtQuantity.Focus()
                'Exit Sub
            End If
            ''validate with empty keg onhand balance , check if there is balance on collectio date selected, if already exist check the onhand qantity should not be less than quantity selected

            Dim CNT = clsTabKEmptyTransferBP.ValidateforNegativeOpeningBal(strCompanyId, hdfCustomerID.Value, 2, hdfItemID.Value, txtQuantity.Text, dpCollectionDate.SelectedDate, objuserInfo("schemaName"))
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
            'This method is called to add the Item Details to grid
            dt.Columns.Add("ItemID", GetType(String))
            dt.Columns.Add("ItemCode", GetType(String))
            dt.Columns.Add("ItemName1", GetType(String))
            dt.Columns.Add("ItemName2", GetType(String))
            dt.Columns.Add("UOM", GetType(String))
            dt.Columns.Add("Quantity", GetType(String))
            dt.Columns.Add("EOrderID", GetType(String))
            dt.Columns.Add("SerialNumber", GetType(String))
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
            row1("EOrderID") = "0"
            row1("SerialNumber") = txtSerialNo.Text

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
            txtToBP.Enabled = False
            dpCollectionDate.Enabled = False
        End Sub
        Private Sub BindGrid()
            RadgvEmptyKegTransfer.DataSource = dt
            RadgvEmptyKegTransfer.DataBind()
        End Sub

        Private Sub ClearFields()
            lblMsg.Text = ""
            txtItemDesc.Text = ""
            txtItemID.Text = ""
            txtQuantity.Text = ""
            txtItemName2.Text = ""
            txtSerialNo.Text = ""
            txtUOM.Text = ""
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

        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            lblMsg.Text = ""

            If NullCheck() = False Then Return

            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
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

                Dim Count = clsTabKEmptyTransferBP.ValidateforOpeningBal(strCompanyId, hdfCustomerID.Value, 2, lblItemID.Text, txtQuantity.Text, objuserInfo("schemaName"))
                'If Count = 0 Or Count = 1 Then
                '    item.BackColor = Drawing.Color.OrangeRed
                '    If strmsg.Length <= 0 Then
                '        strmsg = strmsg + "Quantity entered is greater than latest onhand quantity for slected Customer and item.Enter correct quantity for the item codes : " + strItemCode
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
                    'Create new instance of the class to store the aspx controls data...
                    Dim objTabKEmptyCustomer As New PropertyTabKEmptyCustomer
                    Dim lblEOrderID As Label = DirectCast(item.FindControl("lblEOrderID"), Label)
                    Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                    Dim lblPrevQty As Label = DirectCast(item.FindControl("lblPrevQty"), Label)
                    Dim txtSerialNumber As TextBox = DirectCast(item.FindControl("txtSerialNumber"), TextBox)

                    objTabKEmptyCustomer.EOrderID = lblEOrderID.Text
                    objTabKEmptyCustomer.CustomerID = hdfCustomerID.Value
                    objTabKEmptyCustomer.CompanyID = strCompanyId
                    objTabKEmptyCustomer.BranchID = hdfToBPID.Value
                    objTabKEmptyCustomer.ItemID = itemid.Text
                    objTabKEmptyCustomer.Quantity = txtQuantity.Text
                    objTabKEmptyCustomer.Batch = True
                    objTabKEmptyCustomer.CollectionDate = dpCollectionDate.SelectedDate
                    objTabKEmptyCustomer.ReceiveBy = objuserrole("employeeID")
                    objTabKEmptyCustomer.SerialNumber = txtSerialNumber.Text
                    objTabKEmptyCustomer.TransactionNumber = txtTransactionNumber.Text
                    objTabKEmptyCustomer.ModifiedBy = objuserrole("employeeID")
                    objTabKEmptyCustomer.Status = "Done"
                    objTabKEmptyCustomer.Barcode = txtSerialNumber.Text
                    'record is updated
                    ret = clsTabKEmptyCustomer.UpdateTabKEmptyCustomer(objTabKEmptyCustomer, objuserInfo("schemaName"), lblPrevQty.Text)

                Next
            Else
                For Each item As GridDataItem In RadgvEmptyKegTransfer.Items
                    'Create new instance of the class to store the aspx controls data...
                    Dim objTabKEmptyCustomer As New PropertyTabKEmptyCustomer
                    objTabKEmptyCustomer.CustomerID = hdfCustomerID.Value 'rcbCustomer.SelectedValue
                    objTabKEmptyCustomer.BranchID = hdfToBPID.Value 'rcbToBP.SelectedValue
                    ' objTabKEmptyCustomer.CompanyID = strCompanyId
                    Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                    Dim txtSerialNumber As TextBox = DirectCast(item.FindControl("txtSerialNumber"), TextBox)
                    objTabKEmptyCustomer.ItemID = itemid.Text
                    objTabKEmptyCustomer.FromCompanyID = CInt(rcbFromCmpny.SelectedValue)
                    objTabKEmptyCustomer.ToCompanyID = CInt(rcbToCmpny.SelectedValue)
                    objTabKEmptyCustomer.Quantity = txtQuantity.Text
                    objTabKEmptyCustomer.Batch = True
                    objTabKEmptyCustomer.CollectionDate = dpCollectionDate.SelectedDate
                    objTabKEmptyCustomer.ReceiveBy = objuserrole("employeeID")
                    objTabKEmptyCustomer.SerialNumber = txtSerialNumber.Text
                    objTabKEmptyCustomer.Barcode = txtSerialNumber.Text
                    If txtTransactionNumber.Text = "" Then
                        'Dim ds = clsTabKEmptyCustomer.GetMaxTransactionNumber(objuserInfo("schemaName"), 1, strCompanyId)
                        'If Not ds Is Nothing Then
                        '    If ds.Tables(0).Rows.Count > 0 Then
                        '        Dim str3 As String = ""
                        '        Dim str As String = ds.Tables(0).Rows(0)(0)
                        '        Dim str1 = str.Substring(2, str.Length - 2)
                        '        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                        '        If str2.Length = 1 Then
                        '            str3 = "CB" + "0000000" + str2
                        '        ElseIf str2.Length = 2 Then
                        '            str3 = "CB" + "000000" + str2
                        '        ElseIf str2.Length = 3 Then
                        '            str3 = "CB" + "00000" + str2
                        '        ElseIf str2.Length = 4 Then
                        '            str3 = "CB" + "0000" + str2
                        '        ElseIf str2.Length = 5 Then
                        '            str3 = "CB" + "000" + str2
                        '        ElseIf str2.Length = 6 Then
                        '            str3 = "CB" + "00" + str2
                        '        ElseIf str2.Length = 7 Then
                        '            str3 = "CB" + "0" + str2
                        '        ElseIf str2.Length >= 8 Then
                        '            str3 = "CB" + str2
                        '        End If
                        '        objTabKEmptyCustomer.TransactionNumber = str3
                        '        txtTransactionNumber.Text = str3
                        '    Else
                        '        objTabKEmptyCustomer.TransactionNumber = "CB00000001"
                        '        txtTransactionNumber.Text = "CB00000001"
                        '    End If

                        'Else
                        '    objTabKEmptyCustomer.TransactionNumber = "CB00000001"
                        '    txtTransactionNumber.Text = "CB00000001"
                        'End If
                        objTabKEmptyCustomer.TransactionNumber = Nothing

                    Else
                        objTabKEmptyCustomer.TransactionNumber = txtTransactionNumber.Text
                    End If


                    objTabKEmptyCustomer.Status = "Done"
                    'New record is inserted
                    txtTransactionNumber.Text = clsTabKEmptyCustomer.SaveCR(objTabKEmptyCustomer, objuserInfo("schemaName"))
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
                Response.Redirect("frmEmptyKegReturnList.aspx?process=Y&mode=E")
            ElseIf ret Then
                'redirects to list page if record is saved successfully.
                Response.Redirect("frmEmptyKegReturnList.aspx?process=Y&mode=A")
            Else
                lblMsg.Text = Resources.lang.msgError
            End If
        End Sub

        Private Sub EditMode(ByVal mid As String)
            lblMsg.Text = ""
            RadgvEmptyKegTransfer.DataBind()
        End Sub

        Private Sub RadgvEmptyKegTransfer_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvEmptyKegTransfer.NeedDataSource
            If Not mid = "" Then
                'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
                Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
                Dim ds = clsTabKEmptyCustomer.GetAllTabKEmptyCustomerByCompanyID(strCompanyId, objuserInfo("schemaName"), mid)
                If Not ds Is Nothing Then
                    'rcbCustomer.SelectedValue = ds.Tables(0).Rows(0)("CustomerID")
                    'rcbToBP.SelectedValue = ds.Tables(0).Rows(0)("BranchID")
                    'rcbCustomer.Enabled = False
                    'rcbToBP.Enabled = False
                    dpCollectionDate.Enabled = False
                    txtTransactionNumber.Text = ds.Tables(0).Rows(0)("TransactionNumber")
                    dpCollectionDate.SelectedDate = Convert.ToDateTime(ds.Tables(0).Rows(0)("CollectionDate"))
                    RadgvEmptyKegTransfer.DataSource = ds
                    dt = ds.Tables(0)
                    ViewState("table") = dt
                End If
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
                lblMsg.Text = "Please select the From Company."
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
    End Class
End Namespace
