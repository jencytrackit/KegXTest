'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmAddEditEmptyKegReturn_BPToSupplier.aspx
'Created By         :
'File navigation    :
'Created Date       :November 13, 2013, 5:16:46 PM
'Description        :This file is used to Add / Edit the records...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmAddEditEmptyKegReturn_BPToSupplier
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim dt As New DataTable 'Datatable to store the item details
        Dim objuserrole As New Hashtable()
        Private mid As String = ""
        Dim objUserDetails As New Hashtable()
        Dim objUserRoleDetails As New Hashtable()

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
            If Not Request.QueryString("id") Is Nothing Then
                mid = Request.QueryString("id")
            End If
            If Not IsPostBack Then
                dpReturnDate.MaxDate = DateTime.Today
                dpReturnDate.SelectedDate = DateTime.Today
                If Not Request.QueryString("id") Is Nothing Then
                    EditMode(mid)
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
                'rcbBP.Items.Insert(0, New RadComboBoxItem("--Select One--"))
                'rcbToSupplier.Items.Insert(0, New RadComboBoxItem("--Select One--"))
            End If

            AddContextKey()

        End Sub
        Private Sub AddContextKey()


            If objuserInfo.Count > 1 And rcbFromCmpny.SelectedIndex > 0 Then
                AutoCompleteExtender_txtFromBP.ContextKey = rcbFromCmpny.SelectedValue & "," & objuserInfo("schemaName")
                txtItemCode_AutoCompleteExtender.ContextKey = rcbFromCmpny.SelectedValue & "," & objuserInfo("schemaName")
            Else
                txtItemCode_AutoCompleteExtender.ContextKey = ""
                AutoCompleteExtender_txtFromBP.ContextKey = ""
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
        Private Sub EditMode(ByVal mid As String)
            RadgvEmptyKegTransfer.DataBind()
        End Sub


        'Private Sub ObjBranchPlant_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjBranchPlant.Selecting
        '    'Pass the logged in employeeid and companyid and retrieve the Branch Plants associated for that company and employee
        '    Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
        '    If Not strCompanyId = "" Then
        '        e.InputParameters("CompanyID") = strCompanyId
        '        e.InputParameters("EmployeeID") = objuserrole("employeeID")
        '        e.InputParameters("SchemaName") = objuserInfo("schemaName")
        '    End If
        'End Sub

        'Private Sub ObjDSSupplier_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjDSSupplier.Selecting
        '    'Pass the logged in employeeid and companyid and retrieve the Suppliers associated for that company and employee
        '    Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
        '    If Not strCompanyId = "" Then
        '        e.InputParameters("CompanyID") = strCompanyId
        '        e.InputParameters("EmployeeID") = objuserrole("employeeID")
        '        e.InputParameters("SchemaName") = objuserInfo("schemaName")
        '    End If
        'End Sub

        Private Sub txtItemCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItemCode.TextChanged
            lblMsg.Text = ""
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)




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


            If rcbFromCmpny.SelectedIndex <> 0 Then
                Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
                If Trim(txtItemCode.Text) <> "" Then
                    Dim VALITEM As Boolean = clsTabUOMMaster.ItemCodeValForTwoCompanies(Trim(txtItemCode.Text), CInt(rcbFromCmpny.SelectedValue), CInt(rcbToCmpny.SelectedValue), objuserInfo("schemaName"))
                    If VALITEM = True Then
                        Dim ds As DataSet = clsTabUOMMaster.GetTabUOMMasterByItemCode(Trim(txtItemCode.Text), objuserInfo("schemaName"), strCompanyId) 'Get the item details by Item code
                        If ds Is Nothing Then
                            ClearFields()
                            Exit Sub
                        End If
                        If ds.Tables(0).Rows.Count > 0 Then
                            txtItemDesc.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName1"))
                            txtItemName2.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName2"))
                            hdfItemID.Value = Convert.ToString(ds.Tables(0).Rows(0)("ItemID"))
                            txtUOM.Text = Convert.ToString(ds.Tables(0).Rows(0)("UOM"))
                        Else
                            ClearFields()
                        End If
                    Else
                        lblMsg.Text = "Item Code does not exists for the To Company"
                        Exit Sub
                    End If
                Else
                    ClearFields()
                End If
            Else
                lblMsg.Text = "Please select From Company"
                Exit Sub
            End If
        End Sub
        Private Sub ClearFields()
            'This Method is called to clear the controls related to items
            txtItemDesc.Text = ""
            hdfItemID.Value = ""
            txtItemName2.Text = ""
            txtUOM.Text = ""
            txtQuantity.Text = ""
            lblMsg.Text = ""
        End Sub

        'Private Sub rcbToSupplier_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbToSupplier.DataBound
        '    rcbToSupplier.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        'End Sub

        'Private Sub rcbBP_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbBP.DataBound
        '    rcbBP.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        'End Sub

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            lblMsg.Text = ""
            If dpReturnDate.SelectedDate Is Nothing Then
                lblMsg.Text = "Return date is required"
                Exit Sub
            End If


            If hdfFromBPID.Value.Length < 1 Then
                lblMsg.Text = "Please select the Branch"
                txtFromBP.Focus()
                Return
            End If
            If hdfSupplierID.Value.Length < 1 Then
                lblMsg.Text = "Please select the Supplier"
                txtFromBP.Focus()
                Return
            End If
            If hdfItemID.Value.Length < 1 Then
                lblMsg.Text = "Please select the Item Details"
                txtItemCode.Focus()
                Return
            End If

            'Validate with empty keg onhand balance
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            Dim Count = clsTabKEmptyTransferBP.ValidateforOpeningBal(strCompanyId, hdfFromBPID.Value, 1, hdfItemID.Value, txtQuantity.Text, objuserInfo("schemaName"))
            If Count = 0 Then
                'lblMsg.Text = "Please enter Opening balance for selected Branch Plant and selected Item"
                'Exit Sub
            ElseIf Count = 1 Then
                'lblMsg.Text = "Quantity entered is greater than latest onhand quantity for slected Branch Plant and item.Enter correct quantity."
                'Exit Sub
            End If



            ''validate with empty keg onhand balance , check if there is balance on collectio date selected, if already exist check the onhand qantity should not be less than quantity selected

            Dim CNT = clsTabKEmptyTransferBP.ValidateforNegativeOpeningBal(strCompanyId, hdfFromBPID.Value, 1, hdfItemID.Value, txtQuantity.Text, dpReturnDate.SelectedDate, objuserInfo("schemaName"))
            If CNT = 0 Then
                'lblMsg.Text = "Please enter Opening balance for selected Branch Plant and selected Item for the selected Returned date"
                'Exit Sub
            ElseIf Count = 1 Then
                'lblMsg.Text = "Quantity entered is greater than latest onhand quantity for slected Customer and item.Enter correct quantity."
                'Exit Sub
            End If
            BindData()
           
        End Sub
        Private Sub BindData()
            'This method is called to add the Item Details to grid
            dt.Columns.Add("ItemID", GetType(String))
            dt.Columns.Add("ItemCode", GetType(String))
            dt.Columns.Add("ItemName1", GetType(String))
            dt.Columns.Add("ItemName2", GetType(String))
            dt.Columns.Add("UOM", GetType(String))
            dt.Columns.Add("Quantity", GetType(String))
            dt.Columns.Add("ESOrderID", GetType(String))

            'If ViewState("table") <> Nothing Then
            If Not ViewState("table") Is Nothing Then
                dt = DirectCast(ViewState("table"), DataTable)
            End If
            Dim row1 As DataRow = dt.NewRow()
            row1("ItemID") = hdfItemID.Value
            row1("ItemCode") = txtItemCode.Text
            row1("ItemName1") = txtItemDesc.Text
            row1("ItemName2") = txtItemName2.Text
            row1("UOM") = txtUOM.Text
            row1("Quantity") = txtQuantity.Text
            row1("ESOrderID") = "0"
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
            txtFromBP.Enabled = False
            rcbToCmpny.Enabled = False
            txtSupplier.Enabled = False
            dpReturnDate.Enabled = False

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
            GetBranchID()
            If hdfFromBPID.Value.Length < 1 Then
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
        Private Sub GetBranchID()

            If rcbFromCmpny.SelectedIndex > 0 Then
                Dim CustomerID As Integer
                CustomerID = clsValidations.GetLookUpIDByCode(rcbFromCmpny.SelectedValue, txtFromBP.Text, "B", objuserInfo("schemaName"))

                If CustomerID > 0 Then
                    hdfFromBPID.Value = CustomerID
                Else
                    lblMsg.Text = "Please select the Branch."
                    txtFromBP.Focus()
                End If
            Else
                lblMsg.Text = "Please select the From Branch."
                txtFromBP.Text = ""
                hdfFromBPID.Value = ""
            End If
        End Sub
        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            lblMsg.Text = ""
            If NullCheck() = False Then Return

            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            Dim ret As Boolean
            'Validate grid has atleast one record 
            If RadgvEmptyKegTransfer.Items.Count <= 0 Then
                lblMsg.Text = "Atleast one item is required"
                Exit Sub
            End If
            Dim strmsg As String = ""
            'Validate whether the quantity changed is more 
            For Each item As GridDataItem In RadgvEmptyKegTransfer.Items
                Dim lblItemID As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                Dim strItemCode = item("ItemCode").Text

                Dim Count = clsTabKEmptyTransferBP.ValidateforOpeningBal(strCompanyId, hdfFromBPID.Value, 1, lblItemID.Text, txtQuantity.Text, objuserInfo("schemaName"))
                'If Count = 0 Or Count = 1 Then
                '    item.BackColor = Drawing.Color.OrangeRed
                '    If strmsg.Length <= 0 Then
                '        strmsg = strmsg + "Quantity entered is greater than latest onhand quantity for slected Branch plant and item.Enter correct quantity for the item codes : " + strItemCode
                '    Else
                '        strmsg = strmsg + "," + strItemCode
                '    End If
                'Else
                '    item.BackColor = Drawing.Color.Transparent
                'End If
            Next
            If strmsg.Length > 0 Then
                lblMsg.Text = strmsg
                Exit Sub
            End If
            If Not mid = "" Then
                For Each item As GridDataItem In RadgvEmptyKegTransfer.Items
                    'Looping the grid Saving the data in DB
                    Dim lblESOrderID As Label = DirectCast(item.FindControl("lblESOrderID"), Label)
                    Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                    Dim lblPrevQty As Label = DirectCast(item.FindControl("lblPrevQty"), Label)
                    Dim objTabKEmptyToSupplier As New PropertyTabKEmptyToSupplier
                    objTabKEmptyToSupplier.ESOrderID = lblESOrderID.Text
                    objTabKEmptyToSupplier.SupplierID = hdfSupplierID.Value
                    objTabKEmptyToSupplier.BranchID = hdfFromBPID.Value
                    objTabKEmptyToSupplier.CompanyID = strCompanyId
                    objTabKEmptyToSupplier.ItemID = itemid.Text
                    objTabKEmptyToSupplier.Quantity = txtQuantity.Text
                    objTabKEmptyToSupplier.Batch = True
                    objTabKEmptyToSupplier.TransferDate = dpReturnDate.SelectedDate
                    objTabKEmptyToSupplier.TransferBy = objuserrole("employeeID")
                    objTabKEmptyToSupplier.ShippingLine = txtShippingLine.Text
                    'objTabKEmptyToSupplier.SerialNo = txtSerialNo.Text
                    objTabKEmptyToSupplier.ContainerNumber = txtContainerNO.Text
                    objTabKEmptyToSupplier.PortFrom = txtPortFrom.Text
                    objTabKEmptyToSupplier.PortTo = txtPortTo.Text
                    If Not dtBOLDate.DateInput.Text = "" Then
                        objTabKEmptyToSupplier.BOLDate = dtBOLDate.SelectedDate
                    End If
                    objTabKEmptyToSupplier.BOLNumber = txtBOLNo.Text
                    objTabKEmptyToSupplier.TransactionNumber = txtTransactionNumber.Text
                    objTabKEmptyToSupplier.Status = "Done"
                    objTabKEmptyToSupplier.ModifiedBy = objuserrole("employeeID")
                    'New record is inserted
                    ret = clsTabKEmptyToSupplier.UpdateTabKEmptyToSupplier(objTabKEmptyToSupplier, objuserInfo("schemaName"), lblPrevQty.Text)
                Next
            Else
                For Each item As GridDataItem In RadgvEmptyKegTransfer.Items
                    'Looping the grid Saving the data in DB
                    Dim objTabKEmptyToSupplier As New PropertyTabKEmptyToSupplier
                    objTabKEmptyToSupplier.SupplierID = hdfSupplierID.Value
                    objTabKEmptyToSupplier.BranchID = hdfFromBPID.Value
                    ' objTabKEmptyToSupplier.CompanyID = strCompanyId
                    objTabKEmptyToSupplier.CompanyID = CInt(rcbFromCmpny.SelectedValue)
                    objTabKEmptyToSupplier.ToCompanyID = CInt(rcbToCmpny.SelectedValue)
                    Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                    objTabKEmptyToSupplier.ItemID = itemid.Text
                    objTabKEmptyToSupplier.Quantity = txtQuantity.Text
                    objTabKEmptyToSupplier.Batch = True
                    objTabKEmptyToSupplier.TransferDate = dpReturnDate.SelectedDate
                    objTabKEmptyToSupplier.TransferBy = objuserrole("employeeID")
                    objTabKEmptyToSupplier.ShippingLine = txtShippingLine.Text
                    'objTabKEmptyToSupplier.SerialNo = txtSerialNo.Text
                    objTabKEmptyToSupplier.ContainerNumber = txtContainerNO.Text
                    objTabKEmptyToSupplier.PortFrom = txtPortFrom.Text
                    objTabKEmptyToSupplier.PortTo = txtPortTo.Text
                    If Not dtBOLDate.DateInput.Text = "" Then
                        objTabKEmptyToSupplier.BOLDate = dtBOLDate.SelectedDate
                    End If
                    objTabKEmptyToSupplier.BOLNumber = txtBOLNo.Text
                    If txtTransactionNumber.Text = "" Then
                        Dim ds = clsTabKEmptyCustomer.GetMaxTransactionNumber(objuserInfo("schemaName"), 2, strCompanyId)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Dim str3 As String = ""
                                Dim str As String = ds.Tables(0).Rows(0)(0)
                                Dim str1 = str.Substring(2, str.Length - 2)
                                Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                If str2.Length = 1 Then
                                    str3 = "BS" + "0000000" + str2
                                ElseIf str2.Length = 2 Then
                                    str3 = "BS" + "000000" + str2
                                ElseIf str2.Length = 3 Then
                                    str3 = "BS" + "00000" + str2
                                ElseIf str2.Length = 4 Then
                                    str3 = "BS" + "0000" + str2
                                ElseIf str2.Length = 5 Then
                                    str3 = "BS" + "000" + str2
                                ElseIf str2.Length = 6 Then
                                    str3 = "BS" + "00" + str2
                                ElseIf str2.Length = 7 Then
                                    str3 = "BS" + "0" + str2
                                ElseIf str2.Length >= 8 Then
                                    str3 = "BS" + str2
                                End If
                                objTabKEmptyToSupplier.TransactionNumber = str3
                                txtTransactionNumber.Text = str3
                            Else
                                objTabKEmptyToSupplier.TransactionNumber = "BS00000001"
                                txtTransactionNumber.Text = "BS00000001"
                            End If
                        Else
                            objTabKEmptyToSupplier.TransactionNumber = "BS00000001"
                            txtTransactionNumber.Text = "BS00000001"
                        End If
                    Else
                        objTabKEmptyToSupplier.TransactionNumber = txtTransactionNumber.Text
                    End If

                    objTabKEmptyToSupplier.Status = "Done"
                    'New record is inserted
                    ret = clsTabKEmptyToSupplier.Save(objTabKEmptyToSupplier, objuserInfo("schemaName"))
                Next
            End If
            If (ret) And mid <> "" Then
                'redirects to list page if record is updated successfully.
                Response.Redirect("frmEmptyKegReturn_ToSupplier.aspx?process=Y&mode=E")
            ElseIf ret Then
                'redirects to list page if record is saved successfully.
                Response.Redirect("frmEmptyKegReturn_ToSupplier.aspx?process=Y&mode=A")
            Else
                lblMsg.Text = Resources.lang.msgError
            End If

        End Sub

        Private Sub RadgvEmptyKegTransfer_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvEmptyKegTransfer.NeedDataSource
            If Not mid = "" Then
                Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
                Dim ds = clsTabKEmptyToSupplier.GetAllTabKEmptyToSupplierByCompanyID(strCompanyId, objuserInfo("schemaName"), mid)
                If Not ds Is Nothing Then
                    hdfSupplierID.Value = ds.Tables(0).Rows(0)("SupplierID")
                    hdfFromBPID = ds.Tables(0).Rows(0)("BranchID")
                    txtSupplier.Enabled = False
                    txtFromBP.Enabled = False
                    dpReturnDate.Enabled = False

                    txtTransactionNumber.Text = ds.Tables(0).Rows(0)("TransactionNumber")
                    txtShippingLine.Text = ds.Tables(0).Rows(0)("ShippingLine")
                    txtBOLNo.Text = ds.Tables(0).Rows(0)("BOLNumber")
                    txtContainerNO.Text = ds.Tables(0).Rows(0)("ContainerNumber")
                    txtPortFrom.Text = ds.Tables(0).Rows(0)("PortFrom")
                    txtPortTo.Text = ds.Tables(0).Rows(0)("PortTo")
                    dpReturnDate.SelectedDate = Convert.ToDateTime(ds.Tables(0).Rows(0)("TransferDate"))
                    RadgvEmptyKegTransfer.DataSource = ds
                    dt = ds.Tables(0)
                    ViewState("table") = dt
                    AddContextKey()
                End If
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

        Private Sub rcbFromCmpny_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbFromCmpny.SelectedIndexChanged
            lblMsg.Text = ""
            AddContextKey()

            hdfFromBPID.Value = ""
            txtFromBP.Text = ""
            txtItemCode.Text = ""
            ClearFields()
            If rcbFromCmpny.SelectedIndex > 0 Then
                txtFromBP.Focus()
            End If
        End Sub

        Protected Sub txtSupplier_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSupplier.TextChanged
            lblMsg.Text = ""
            hdfSupplierID.Value = ""
            If rcbToCmpny.SelectedIndex > 0 Then
                Dim SupplierID As Integer
                SupplierID = clsValidations.GetLookUpIDByCode(rcbToCmpny.SelectedValue, txtSupplier.Text, "S", objuserInfo("schemaName"))

                If SupplierID > 0 Then
                    hdfSupplierID.Value = SupplierID
                Else
                    lblMsg.Text = "Error while getting the Supplier ID. Please try again."
                    txtSupplier.Focus()
                End If
            Else
                txtSupplier.Text = ""
                hdfSupplierID.Value = ""
            End If
        End Sub

        Protected Sub txtFromBP_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtFromBP.TextChanged
            lblMsg.Text = ""
            hdfFromBPID.Value = ""
            If rcbFromCmpny.SelectedIndex > 0 Then
                Dim branchID As Integer
                branchID = clsValidations.GetLookUpIDByCode(rcbFromCmpny.SelectedValue, txtFromBP.Text, "B", objuserInfo("schemaName"))

                If branchID > 0 Then
                    hdfFromBPID.Value = branchID
                Else
                    lblMsg.Text = "Error while getting the BranchPlant ID. Please try again."
                    txtFromBP.Focus()
                End If
            Else
                txtFromBP.Text = ""
                hdfFromBPID.Value = ""
            End If
        End Sub
    End Class
End Namespace
