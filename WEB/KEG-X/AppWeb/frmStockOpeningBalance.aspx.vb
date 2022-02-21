
Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports System.Data.OleDb
Imports System.IO
Imports System.Web
Imports System.Net

Namespace TrackITKTS
    Partial Class frmStockOpeningBalance
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging
        Private mid As String = ""
        Dim dt As New DataTable
        Dim objuserrole As New Hashtable()
        Dim objCommand As OleDbCommand
        Dim objXConn As OleDbConnection
        Dim ErrorsFound As Boolean

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            lblMsg.Text = ""

            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
            If Not Page.IsPostBack Then
                RadgvStockOpeningBalance.DataSource = ""
                RadgvStockOpeningBalance.DataBind()
                lblBrachPlant.Text = "Branch Plant"
                BindCompany()
                'rcbFromCmpny.SelectedValue = Session("SelectedCompanyID")
                rcbFromCmpny.Visible = True
                Me.Form.DefaultButton = btnAdd.UniqueID

            End If
            'AddContextKey()
        End Sub
        Private Sub BindCompany()
            rcbFromCmpny.Items.Clear()
            Dim ds As New DataSet
            ds = clsTabMOrganization.GetAllTabMOrganizationByEmployeeID(objuserrole("employeeID"), objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                ' bind From company
                rcbFromCmpny.DataSource = ds
                rcbFromCmpny.DataTextField = "CompanyName"
                rcbFromCmpny.DataValueField = "CompanyID"
                rcbFromCmpny.DataBind()
                rcbFromCmpny.SelectedIndex = -1
            End If

            rcbFromCmpny.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        End Sub
        'Private Sub BindBranchPlant() 'Filling Branchplant dropdown details  fore each radio button value
        '    txtItemCode.Text = ""
        '    txtItemDesc.Text = ""
        '    txtItemDesc.Text = ""
        '    txtItemDesc2.Text = ""
        '    hdfItemID.value = ""
        '    rntxtQuantity.Text = ""
        '    txtUOM.Text = ""
        '    AddContextKey()
        '    'If rcbFromCmpny.SelectedIndex <> 0 Then
        '    '    lblMsg.Text = ""
        '    '    Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
        '    '    If Not strCompanyId = 0 Then
        '    '        If rbtEntityType.SelectedValue = 1 Then
        '    '            ddlBranchPlant.DataSource = clsTabMBranchPlant.GetTabMBranchPlantByCompanyID(CInt(rcbFromCmpny.SelectedValue), objuserrole("employeeID"), objuserInfo("schemaName"))
        '    '            ddlBranchPlant.DataTextField = "BranchCodeName"
        '    '            ddlBranchPlant.DataValueField = "BranchID"
        '    '            ddlBranchPlant.DataBind()
        '    '        ElseIf rbtEntityType.SelectedValue = 2 Then
        '    '            ddlBranchPlant.DataSource = clsTabMCustomers.GetTabMCustomersByCompanyID(CInt(rcbFromCmpny.SelectedValue), objuserrole("employeeID"), objuserInfo("schemaName"))
        '    '            ddlBranchPlant.DataTextField = "CustCodeName"
        '    '            ddlBranchPlant.DataValueField = "CustomerID"
        '    '            ddlBranchPlant.DataBind()
        '    '        ElseIf rbtEntityType.SelectedValue = 3 Then
        '    '            ddlBranchPlant.DataSource = clsTabMSuppliers.GetTabMSuppliersByCompanyID(CInt(rcbFromCmpny.SelectedValue), objuserrole("employeeID"), objuserInfo("schemaName"))
        '    '            ddlBranchPlant.DataTextField = "SuppCodeName"
        '    '            ddlBranchPlant.DataValueField = "SupplierID"
        '    '            ddlBranchPlant.DataBind()
        '    '        End If
        '    '    End If

        '    '    ddlBranchPlant.Items.Insert(0, New RadComboBoxItem("--Select One--"))

        '    'End If

        'End Sub

        Private Sub BindData()
            'Creating new datatable and inserting values into it
            dt.Columns.Add("ItemID", GetType(Int32))
            dt.Columns.Add("ItemCode", GetType(String))
            dt.Columns.Add("ItemName1", GetType(String))
            dt.Columns.Add("ItemName2", GetType(String))
            dt.Columns.Add("UOM", GetType(String))
            dt.Columns.Add("OnHandQuantity", GetType(Int32))
            dt.Columns.Add("InTransitQuantity", GetType(Int32))
            dt.Columns.Add("InventoryID", GetType(Int32))
            dt.Columns.Add("CompanyID", GetType(Int32))
            dt.Columns.Add("EntityID", GetType(Int32))
            dt.Columns.Add("EntityType", GetType(Int32))
            dt.Columns.Add("TranasctionDate", GetType(Date))
            dt.Columns.Add("AdjustmentReason", GetType(String))
            If ViewState("table") IsNot Nothing Then
                dt = DirectCast(ViewState("table"), DataTable)
            End If
            Dim row1 As DataRow = dt.NewRow()
            row1("ItemID") = CInt(hdfItemID.Value)
            row1("ItemCode") = txtItemCode.Text
            row1("ItemName1") = txtItemDesc.Text
            row1("ItemName2") = txtItemDesc2.Text
            row1("UOM") = txtUOM.Text
            row1("OnHandQuantity") = CInt(rntxtQuantity.Text)
            row1("InTransitQuantity") = 0
            row1("InventoryID") = 0
            row1("CompanyID") = 0
            row1("EntityID") = 0
            row1("EntityType") = 0
            row1("TranasctionDate") = Now.Date
            row1("AdjustmentReason") = ""
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
        End Sub

        Private Sub rbtEntityType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtEntityType.SelectedIndexChanged
            lblMsg.Text = ""
            txtEntityType.Text = ""
            hdfEntityType.Value = ""
            ClearFields()
            'If radio button click change the label text is filling
            If rbtEntityType.SelectedValue = 1 Then
                'RadTextBox1.Text = ""
                'RadTextBox1.Enabled = True

                lblBrachPlant.Text = "Branch Plant"
                lblMsg.Text = ""
            ElseIf rbtEntityType.SelectedValue = 2 Then
                'RadTextBox1.Text = "0"
                'RadTextBox1.Enabled = False
                lblBrachPlant.Text = "Customer"
                lblMsg.Text = ""
            ElseIf rbtEntityType.SelectedValue = 3 Then
                'RadTextBox1.Text = "0"
                'RadTextBox1.Enabled = False
                lblBrachPlant.Text = "Supplier"
                lblMsg.Text = ""
            End If
            'If rcbCompany.SelectedIndex > 0 Then
            AddContextKey()
            'BindBranchPlant()

            RadgvStockOpeningBalance.DataSource = ""
            RadgvStockOpeningBalance.DataBind()
            ViewState("table") = Nothing
            RadgvOpeningBalanceHistory.DataSource = ""
            RadgvOpeningBalanceHistory.DataBind()
            txtEntityType.Focus()
            'End If
        End Sub

        Private Sub txtItemCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItemCode.TextChanged

            If rcbFromCmpny.SelectedIndex <= 0 Then
                txtItemCode.Text = ""
                lblMsg.Text = "Please select Company"
                Exit Sub
            End If

            If hdfEntityType.Value.Length <= 0 Then
                txtItemCode.Text = ""
                lblMsg.Text = "Please select BranchPlant"
                Exit Sub
            End If

            Dim strItemCode As String = Nothing
            If txtItemCode.Text.LastIndexOf(" -") < 1 Then
                ClearFields()
                lblMsg.Text = "Please Enter the Correct Item Details"
                Return
            End If
            strItemCode = Left(txtItemCode.Text, txtItemCode.Text.LastIndexOf(" -"))
            txtItemCode.Text = strItemCode

            lblMsg.Text = ""
            Dim ds2 As New DataSet
            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            If Trim(txtItemCode.Text) <> "" Then
                Dim ItemName As String = ""
                'Getting item master details by passing itemcode as parameter 
                Dim ds As DataSet = clsTabUOMMaster.GetTabUOMMasterByItemCode(Trim(txtItemCode.Text), objuserInfo("schemaName"), CInt(rcbFromCmpny.SelectedValue))
                If ds Is Nothing Then
                    ClearFields()
                    Exit Sub
                End If
                'Each and every item in the dataset is filling the aspx screen 
                If ds.Tables(0).Rows.Count > 0 Then
                    txtItemDesc.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName1"))
                    txtItemDesc2.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName2"))
                    hdfItemID.value = Convert.ToString(ds.Tables(0).Rows(0)("ItemID"))
                    txtUOM.Text = Convert.ToString(ds.Tables(0).Rows(0)("UOM"))

                    rntxtQuantity.Focus()
                    If rbtEntityType.SelectedValue = 1 And hdfEntityType.Value.Length <= 0 Then
                        lblMsg.Text = "Please select BranchPlant"
                        Exit Sub
                    ElseIf rbtEntityType.SelectedValue = 2 And hdfEntityType.Value.Length <= 0 Then
                        lblMsg.Text = "Please select Customer"
                        Exit Sub
                    ElseIf rbtEntityType.SelectedValue = 3 And hdfEntityType.Value.Length <= 0 Then
                        lblMsg.Text = "Please select Supplier"
                        Exit Sub
                    End If

                    getexistingopeningbalance()
                Else
                    ClearFields()
                    'RadgvStockOpeningBalance.DataSource = ""
                    'RadgvStockOpeningBalance.DataBind()
                End If

            Else
                ClearFields() 'clear the textboxes               
            End If

        End Sub


        Private Sub getexistingopeningbalance()

            Dim ds As New DataSet
            If hdfEntityType.Value.Length > 0 And hdfItemID.Value.Length > 0 And rcbFromCmpny.SelectedIndex > 0 Then
                Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
                If rbtEntityType.SelectedValue = 1 Then
                    ds = clsTabkEmptyInventory.GetAllTabKEmptyInventoryLatestOBByItemID(CInt(rcbFromCmpny.SelectedValue), hdfEntityType.Value, 1, hdfItemID.Value, objuserInfo("schemaName"))
                ElseIf rbtEntityType.SelectedValue = 2 Then
                    ds = clsTabkEmptyInventory.GetAllTabKEmptyInventoryLatestOBByItemID(CInt(rcbFromCmpny.SelectedValue), hdfEntityType.Value, 2, hdfItemID.Value, objuserInfo("schemaName"))
                ElseIf rbtEntityType.SelectedValue = 3 Then
                    ds = clsTabkEmptyInventory.GetAllTabKEmptyInventoryLatestOBByItemID(CInt(rcbFromCmpny.SelectedValue), hdfEntityType.Value, 3, hdfItemID.Value, objuserInfo("schemaName"))
                End If
                rntxtQuantity.Enabled = True
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        rntxtQuantity.Enabled = False

                        Dim dt2 As New DataTable
                        If ViewState("table") Is Nothing Then
                            dt2 = ds.Tables(0)
                            ViewState("table") = dt2
                        Else
                            dt2 = DirectCast(ViewState("table"), DataTable)
                            Dim tempdt As DataTable
                            tempdt = CType(ViewState("table"), DataTable)
                            If tempdt.Select("ItemID=" & hdfItemID.Value).Length < 1 Then
                                dt2.Merge(ds.Tables(0))
                                ViewState("table") = dt2
                            End If
                        End If

                        RadgvStockOpeningBalance.DataSource = dt2
                        RadgvStockOpeningBalance.DataBind()

                    End If

                End If
                BindHistoryGrid()
            Else
                RadgvStockOpeningBalance.DataSource = ""
                RadgvStockOpeningBalance.DataBind()
                RadgvOpeningBalanceHistory.DataSource = ""
                RadgvOpeningBalanceHistory.DataBind()
                ViewState("table") = Nothing
            End If
        End Sub

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            lblMsg.Text = ""

            BindData()
            ClearFields()
            txtItemCode.Text = ""
            rntxtQuantity.Text = ""
            If rbtEntityType.SelectedValue = 1 Then
                'RadTextBox1.Text = ""
                'RadTextBox1.Enabled = True              
            ElseIf rbtEntityType.SelectedValue = 2 Then
                'RadTextBox1.Text = "0"
                'RadTextBox1.Enabled = False              
            ElseIf rbtEntityType.SelectedValue = 3 Then
                'RadTextBox1.Text = "0"
                'RadTextBox1.Enabled = False 
            End If
        End Sub

        Private Sub BindGrid()
            lblMsg.Text = ""
            'Binding the grid with the datatable
            RadgvStockOpeningBalance.DataSource = dt
            RadgvStockOpeningBalance.DataBind()
        End Sub

        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            If rcbFromCmpny.SelectedIndex <= 0 Then
                lblMsg.Text = "Company is Required"
                Exit Sub
            End If
            lblMsg.Text = ""
            If hdfEntityType.Value.Length <= 0 Then
                If rbtEntityType.SelectedValue = "" Then
                    lblMsg.Visible = True
                    lblMsg.Text = "Entity Type is Required"

                    Exit Sub
                End If
                If rbtEntityType.SelectedValue = 1 Then
                    lblMsg.Visible = True
                    lblMsg.Text = "BranchPlant is Required"

                    Exit Sub
                ElseIf rbtEntityType.SelectedValue = 2 Then
                    lblMsg.Visible = True
                    lblMsg.Text = "Customer is Required"

                    Exit Sub
                ElseIf rbtEntityType.SelectedValue = 3 Then
                    lblMsg.Visible = True
                    lblMsg.Text = "Supplier is Required"

                    Exit Sub
                End If
            End If

            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            Dim ret As Boolean

            'Validate grid has atleast one record 
            If RadgvStockOpeningBalance.Items.Count <= 0 Then
                lblMsg.Visible = True
                lblMsg.Text = "Atleast one item is required in the grid"

                Exit Sub
            End If
            Dim ErrorFound As Boolean = False
            'Dim CNT As Integer = 0
            'For Each item As GridDataItem In RadgvStockOpeningBalance.Items
            '    'Dim objTabKEmptyInventory2 As New PropertyTabkEmptyInventory
            '    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
            '    Dim txtAdjustmentQuantity As TextBox = DirectCast(item.FindControl("txtAdjustmentQuantity"), TextBox)
            '    'objTabKEmptyInventory2.OnHandQuantity = txtQuantity.Text
            '    Dim AdjustmentQty2 As Int32 = 0

            '    If txtAdjustmentQuantity.Text.Length > 0 Then
            '        AdjustmentQty2 = txtAdjustmentQuantity.Text
            '    End If

            '    If txtAdjustmentQuantity.Text.Length > 0 Then
            '        If txtAdjustmentQuantity.Text = 0 Then
            '            lblMsg.Text = "Adjustment cannot be zero"

            '        End If
            '        CNT = AdjustmentQty2 + (txtQuantity.Text)
            '        If CNT < 0 Or AdjustmentQty2 = 0 Then
            '            ErrorFound = True
            '            item.BackColor = Drawing.Color.Red
            '        End If
            '    End If
            'Next
            'If ErrorFound = True Then
            '    lblMsg.Text = "Insertion Failed, please check the entered adjustment quantity"
            '    lblMsg.Visible = True
            '    Exit Sub
            'End If

            For Each item As GridDataItem In RadgvStockOpeningBalance.Items
                'Create new instance of the class to store the aspx controls data...
                Dim objTabKEmptyInventory As New PropertyTabkEmptyInventory
                objTabKEmptyInventory.CompanyID = CInt(rcbFromCmpny.SelectedValue)
                txtTransactionNumber.Text = ""
                Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                Dim txtInsQuantity As TextBox = DirectCast(item.FindControl("txtInsQuantity"), TextBox)
                Dim txtAdjustmentQuantity As TextBox = DirectCast(item.FindControl("txtAdjustmentQuantity"), TextBox)
                Dim txtAdjustReason As TextBox = DirectCast(item.FindControl("txtAdjustReason"), TextBox)
                Dim lblInventoryID As Label = DirectCast(item.FindControl("lblInventoryID"), Label)
                objTabKEmptyInventory.ItemID = itemid.Text
                objTabKEmptyInventory.OnHandQuantity = txtQuantity.Text
                objTabKEmptyInventory.TransitQuantity = txtInsQuantity.Text
                objTabKEmptyInventory.EntityID = hdfEntityType.Value
                objTabKEmptyInventory.EntityTypeID = rbtEntityType.SelectedValue
                objTabKEmptyInventory.DamagedQuantity = 0
                Dim AdjustmentQty As Int32 = 0

                If txtAdjustmentQuantity.Text = "" Then
                    AdjustmentQty = 0
                Else
                    AdjustmentQty = txtAdjustmentQuantity.Text
                End If


                If lblInventoryID.Text = "0" And txtAdjustmentQuantity.Text = "" Then
                    ret = clsTabkEmptyInventory.Save(objTabKEmptyInventory, objuserInfo("schemaName"), AdjustmentQty)
                ElseIf lblInventoryID.Text > "0" And txtAdjustmentQuantity.Text.Length > 0 Then
                    If AdjustmentQty = 0 Then
                        lblMsg.Text = "Adjustment cannot be zero"
                        Exit Sub
                    Else
                        ret = clsTabkEmptyInventory.Save(objTabKEmptyInventory, objuserInfo("schemaName"), AdjustmentQty)

                    End If
                Else
                    ret = False
                End If

                If ret Then
                    'Save empty keg opening balance history
                    Dim objTabKEmptyOBHistory As New PropertyTabkEmptyOpeningBalanceHistory
                    Dim ds As New DataSet

                    objTabKEmptyOBHistory.InventoryID = objTabKEmptyInventory.InventoryID
                    objTabKEmptyOBHistory.Quantity = txtQuantity.Text
                    objTabKEmptyOBHistory.AdjustmentQuantity = AdjustmentQty
                    objTabKEmptyOBHistory.CompanyID = CInt(rcbFromCmpny.SelectedValue)
                    objTabKEmptyOBHistory.EntityType = rbtEntityType.SelectedValue
                    objTabKEmptyOBHistory.EntityID = hdfEntityType.Value
                    If lblInventoryID.Text = "0" Then
                        objTabKEmptyOBHistory.CreatedDate = System.DateTime.Now
                        objTabKEmptyOBHistory.CreatedBy = objuserrole("employeeID")
                        If txtTransactionNumber.Text = "" Then
                            If rbtEntityType.SelectedValue = 1 Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 1, CInt(rcbFromCmpny.SelectedValue), 0)
                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "OBB" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "OBB" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "OBB" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "OBB" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "OBB" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "OBB00001"
                                        txtTransactionNumber.Text = "OBB00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "OBB00001"
                                    txtTransactionNumber.Text = "OBB00001"
                                End If
                            ElseIf rbtEntityType.SelectedValue = 2 Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 2, CInt(rcbFromCmpny.SelectedValue), 0)
                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "OBC" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "OBC" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "OBC" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "OBC" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "OBC" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "OBC00001"
                                        txtTransactionNumber.Text = "OBC00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "OBC00001"
                                    txtTransactionNumber.Text = "OBC00001"
                                End If
                            ElseIf rbtEntityType.SelectedValue = 3 Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 3, CInt(rcbFromCmpny.SelectedValue), 0)

                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "OBS" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "OBS" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "OBS" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "OBS" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "OBS" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "OBS00001"
                                        txtTransactionNumber.Text = "OBS00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "OBS00001"
                                    txtTransactionNumber.Text = "OBS00001"
                                End If
                            End If
                        Else
                            objTabKEmptyOBHistory.TransactionID = txtTransactionNumber.Text
                        End If

                    Else
                        objTabKEmptyOBHistory.ModifiedDate = System.DateTime.Now
                        objTabKEmptyOBHistory.ModifiedBy = objuserrole("employeeID")
                        If txtTransactionNumber.Text = "" Then
                            If rbtEntityType.SelectedValue = 1 Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 1, CInt(rcbFromCmpny.SelectedValue), 1)
                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "ADB" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "ADB" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "ADB" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "ADB" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "ADB" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "ADB00001"
                                        txtTransactionNumber.Text = "ADB00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "ADB00001"
                                    txtTransactionNumber.Text = "ADB00001"
                                End If
                            ElseIf rbtEntityType.SelectedValue = 2 Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 2, CInt(rcbFromCmpny.SelectedValue), 1)
                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "ADC" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "ADC" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "ADC" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "ADC" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "ADC" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "ADC00001"
                                        txtTransactionNumber.Text = "ADC00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "ADC00001"
                                    txtTransactionNumber.Text = "ADC00001"
                                End If
                            ElseIf rbtEntityType.SelectedValue = 3 Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 3, CInt(rcbFromCmpny.SelectedValue), 1)
                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "ADS" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "ADS" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "ADS" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "ADS" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "ADS" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "ADS00001"
                                        txtTransactionNumber.Text = "ADS00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "ADS00001"
                                    txtTransactionNumber.Text = "ADS00001"
                                End If
                            End If
                        Else
                            objTabKEmptyOBHistory.TransactionID = txtTransactionNumber.Text
                        End If
                    End If
                    Dim AdjustmentReason As String = ""
                    If Len(txtAdjustReason.Text.Trim) = 0 Then
                        txtAdjustReason.Text = ""
                    Else
                        AdjustmentReason = txtAdjustReason.Text.Trim

                    End If

                    ret = clsTabkEmptyOpeningBalanceHistory.Save(objTabKEmptyOBHistory, objuserInfo("schemaName"), AdjustmentReason)

                End If
            Next
            If (ret) Then
                lblMsg.Text = "Record Inserted Successfully"
                lblMsg.Visible = True
                'ClearFields()
                txtItemCode.Focus()
            End If
            txtTransactionNumber.Text = ""
            'ddlBranchPlant.SelectedIndex = 0
            'hdfEntityType.Value = ""
            'txtEntityType.Text = ""
            RadgvStockOpeningBalance.DataSource = ""
            RadgvStockOpeningBalance.DataBind()
            'RadgvOpeningBalanceHistory.DataSource = ""
            'RadgvOpeningBalanceHistory.DataBind()
            BindHistoryGrid()
            ViewState("table") = Nothing
            ClearFields()
        End Sub

        Private Sub ClearFields()
            lblMsg.Visible = True
            txtItemDesc.Text = ""
            txtItemDesc2.Text = ""
            txtItemCode.Text = ""
            txtUOM.Text = ""
            txtItemCode.Text = ""
            hdfItemID.Value = ""
            rntxtQuantity.Text = ""
            txtUOM.Text = ""
        End Sub

        Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
            lblMsg.Visible = True
            If lblFileName.Text <> "" Then

                ErrorsFound = False
                lblSuccess.Text = ""
                ImportBPOpeningBal()
                ImportCustomerOpeningBal()
                ImportSupplierOpeningBal()
                If ErrorsFound = True Then
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType, "alrt", "alert('Records Imported Partially! Errors Found While Importing. Check Error Log for more info.');", True)
                    'lblSuccess.Text = "Records Imported Partially! Errors Found While Importing. Check Error Log for more info."
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alrt", "alert('Imported Successfully.');", True)
                    lblSuccess.Text = "Imported Successfully"
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alrt", "alert('Imported Successfully.');", True)
                'lblSuccess.Text = "Imported Successfully"
                'lblSuccess.Text = ""
                lblFileName.Text = ""
            Else
                lblFileName.Text = "Please upload a file to import data"
                lblSuccess.Text = ""
            End If
        End Sub

        Private Sub ImportBPOpeningBal()
            Dim reader As OleDbDataReader
            'Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            Try
                WriteError("Importing BranchPlants Opening Balances started at  --" & DateTime.Now.ToString())
                WriteError("__________________________________________________________")
                ' retrieve the Select Command for the worksheet data
                Dim objCommand As New OleDbCommand()
                objCommand = ExcelConnection(ConfigurationManager.AppSettings("BPOpeningBalSheetName"))
                '  create a DataReader
                reader = objCommand.ExecuteReader()
                Dim counter As Integer = 0 ' used for testing your import in smaller increments

                While reader.Read()
                    'WriteError("here exception at 721")
                    counter = counter + 1 ' counter to exit early for testing...
                    ' set default values for loop
ContinueForAdjustments:
                    Dim CompanyCode As String = Convert.ToString(reader("CompanyCode"))
                    Dim BranchPlantCode As String = Convert.ToString(reader("BranchPlantCode"))
                    Dim ItemCode As String = Convert.ToString(reader("ItemCode"))
                    Dim OnHandQty As String = Convert.ToString(reader("OnHandQty"))
                    Dim InTransitQty As Integer = 0 '= Convert.ToString(reader("InTransitQty"))
                    Dim Adjustment As String = Convert.ToString(reader("Adjustment"))
                    'Validations
                    If CompanyCode.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "CompanyCode is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    If BranchPlantCode.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "Branch Plant Code is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    If ItemCode.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "ItemCode is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    If OnHandQty.Length <= 0 And Adjustment.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "OnHand Quantity is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If


                    'If InTransitQty.Length <= 0 Then
                    '    WriteError("Row Number:" & counter & "|" & "InTransit Quantity is required")
                    '    GoTo NextRecord
                    'End If

                    'Get CompanyId by Company Code
                    Dim objTabMOrganization As New PropertyTabMOrganization
                    objTabMOrganization = clsTabMOrganization.GetTabMOrganizationByCompanyCode(CompanyCode, objuserInfo("schemaName"))
                    If objTabMOrganization.CompanyID = 0 Then
                        WriteError("Row Number:" & counter & "|" & "CompanyCode:" & CompanyCode & "|" & "Not a valid companycode")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    ''If CInt(rcbFromCmpny.SelectedValue) <> objTabMOrganization.CompanyID Then
                    ''    WriteError("Row Number:" & counter & "|" & "CompanyCode:" & CompanyCode & "|" & "select the correct company for importing data")
                    ''    GoTo NextRecord
                    ''End If
                    'Get BranchPlantID by BranchPlant Code
                    Dim objTabMBranchPlant As New PropertyTabMBranchPlant
                    objTabMBranchPlant = clsTabMBranchPlant.GetTabMBranchPlantByBranchCode(objTabMOrganization.CompanyID, BranchPlantCode, objuserInfo("schemaName"))
                    If objTabMBranchPlant.BranchID = 0 Then
                        WriteError("Row Number:" & counter & "|" & "Branch Plant Code:" & BranchPlantCode & "|" & "Not a valid branch plant code")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    'Get ItemID by ItemCode
                    Dim dsItems = clsTabUOMMaster.GetTabUOMMasterByItemCode(ItemCode, objuserInfo("schemaName"), objTabMOrganization.CompanyID)
                    If dsItems Is Nothing Then
                        WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Not a valid Item code")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    'Check whether OB already exist for the item or nor
                    'Dim ret1 = clsTabkEmptyInventory.CheckEmptyOpeningBalExist(objTabMOrganization.CompanyID, objTabMBranchPlant.BranchID, 1, dsItems.Tables(0).Rows(0)("ItemID"), objuserInfo("schemaName"))
                    Dim GetExistingOnHandQty As String
                    GetExistingOnHandQty = clsTabkEmptyInventory.CheckAndGetEmptyOpeningBal(objTabMOrganization.CompanyID, objTabMBranchPlant.BranchID, 1, dsItems.Tables(0).Rows(0)("ItemID"), objuserInfo("schemaName"))
                    If GetExistingOnHandQty <> "" Then
                        OnHandQty = CInt(GetExistingOnHandQty)
                        If Adjustment.Length < 1 Then
                            WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Opening Balance already exists and Adjustment quantity not specified.")
                            ErrorsFound = True
                            GoTo NextRecord
                        End If
                        If Adjustment = 0 Then
                            WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Opening Balance already exists and Adjustment quantity Given as 0, So Record Skipped.")
                            ErrorsFound = True
                            GoTo NextRecord
                        End If
                    End If

                    'If ret1 And Adjustment.Length <= 0 Then
                    'WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Item already has opening balance.so,Enter adjustment quantity")
                    'GoTo NextRecord
                    'Else
                    Dim NewRecordNotExists As Boolean = False
                    Dim OnHandAndAdjQtyIsZero As Boolean = False

                    If GetExistingOnHandQty = "" And Adjustment.Length > 0 Then
                        'WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "This is the first time entry.so,remove the adjustment quantity")
                        'GoTo NextRecord
                        Try
                            If Adjustment = 0 Then 'If onhand qty=0 and adjqty=0
                                OnHandAndAdjQtyIsZero = True
                            End If
                        Catch ex As Exception

                        End Try

                        NewRecordNotExists = True
                        OnHandQty = 0
                        Adjustment = 0
                    End If

                    Dim ret1 = clsTabkEmptyInventory.CheckAndGetEmptyOpeningBalInTransit(objTabMOrganization.CompanyID, objTabMBranchPlant.BranchID, 1, dsItems.Tables(0).Rows(0)("ItemID"), objuserInfo("schemaName"))
                    If ret1 > 0 Then
                        InTransitQty = ret1
                    End If

                    'Save the record to the database from excel 
                    Dim objTabKEmptyInventory As New PropertyTabkEmptyInventory
                    objTabKEmptyInventory.CompanyID = objTabMOrganization.CompanyID 'CInt(rcbFromCmpny.SelectedValue)
                    objTabKEmptyInventory.ItemID = dsItems.Tables(0).Rows(0)("ItemID")
                    objTabKEmptyInventory.OnHandQuantity = OnHandQty
                    objTabKEmptyInventory.TransitQuantity = InTransitQty
                    objTabKEmptyInventory.EntityID = objTabMBranchPlant.BranchID
                    objTabKEmptyInventory.EntityTypeID = 1
                    objTabKEmptyInventory.DamagedQuantity = 0
                    Dim AdjustmentQty As Int32
                    If Adjustment.Length > 0 Then
                        AdjustmentQty = Adjustment
                    Else
                        AdjustmentQty = 0
                    End If
                    Dim ret = clsTabkEmptyInventory.Save(objTabKEmptyInventory, objuserInfo("schemaName"), AdjustmentQty)
                    If ret Then
                        Dim objTabKEmptyOBHistory As New PropertyTabkEmptyOpeningBalanceHistory
                        Dim ds As New DataSet
                        objTabKEmptyOBHistory.InventoryID = objTabKEmptyInventory.InventoryID
                        objTabKEmptyOBHistory.Quantity = OnHandQty
                        objTabKEmptyOBHistory.AdjustmentQuantity = AdjustmentQty
                        objTabKEmptyOBHistory.CompanyID = objTabMOrganization.CompanyID 'CInt(rcbFromCmpny.SelectedValue)
                        objTabKEmptyOBHistory.EntityType = 1
                        objTabKEmptyOBHistory.EntityID = objTabMBranchPlant.BranchID
                        'If Adjustment.Length <= 0 Then
                        'If GetExistingOnHandQty < 1 Then
                        If GetExistingOnHandQty = "" Then
                            objTabKEmptyOBHistory.CreatedDate = System.DateTime.Now
                            objTabKEmptyOBHistory.CreatedBy = objuserrole("employeeID")
                            If txtTransactionNumber.Text = "" Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 1, objTabMOrganization.CompanyID, 0)
                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "OBB" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "OBB" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "OBB" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "OBB" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "OBB" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "OBB00001"
                                        txtTransactionNumber.Text = "OBB00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "OBB00001"
                                    txtTransactionNumber.Text = "OBB00001"
                                End If
                            Else
                                objTabKEmptyOBHistory.TransactionID = txtTransactionNumber.Text
                            End If
                        Else
                            objTabKEmptyOBHistory.ModifiedDate = System.DateTime.Now
                            objTabKEmptyOBHistory.ModifiedBy = objuserrole("employeeID")
                            If txtTransactionNumber.Text = "" Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 1, objTabMOrganization.CompanyID, 1)
                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "ADB" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "ADB" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "ADB" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "ADB" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "ADB" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "ADB00001"
                                        txtTransactionNumber.Text = "ADB00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "ADB00001"
                                    txtTransactionNumber.Text = "ADB00001"
                                End If
                            Else
                                objTabKEmptyOBHistory.TransactionID = txtTransactionNumber.Text
                            End If

                        End If
                        ret = clsTabkEmptyOpeningBalanceHistory.Save(objTabKEmptyOBHistory, objuserInfo("schemaName"), "")
                    End If
                    txtTransactionNumber.Text = ""

                    If NewRecordNotExists = True And OnHandAndAdjQtyIsZero = False Then
                        GoTo ContinueForAdjustments
                    End If

NextRecord:
                End While
                reader.Close()

            Catch ex As Exception
                ErrorsFound = True
                WriteError("Errors:" & ex.Message.ToString)
                objCommand = Nothing
                objXConn = Nothing
                objCommand = Nothing
                Exit Sub
            End Try

            objCommand = Nothing
            objXConn = Nothing
            objCommand = Nothing
            txtTransactionNumber.Text = ""
        End Sub

        Private Sub ImportCustomerOpeningBal()
            Dim reader As OleDbDataReader

            Try
                WriteError("Importing Customer Opening Balances started at  --" & DateTime.Now.ToString())
                WriteError("__________________________________________________________")
                ' retrieve the Select Command for the worksheet data
                Dim objCommand As New OleDbCommand()
                objCommand = ExcelConnection(ConfigurationManager.AppSettings("CustomerOpeningBalSheetName"))

                '  create a DataReader
                reader = objCommand.ExecuteReader()
                Dim counter As Integer = 0 ' used for testing your import in smaller increments

                While reader.Read()
                    counter = counter + 1 ' counter to exit early for testing...
                    ' set default values for loop
ContinueForAdjustments:
                    Dim CompanyCode As String = Convert.ToString(reader("CompanyCode"))
                    Dim CustomerCode As String = Convert.ToString(reader("CustomerCode"))
                    Dim ItemCode As String = Convert.ToString(reader("ItemCode"))
                    Dim OnHandQty As String = Convert.ToString(reader("OnHandQty"))
                    Dim Adjustment As String = Convert.ToString(reader("Adjustment"))
                    'Validations
                    If CompanyCode.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "CompanyCode is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    If CustomerCode.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "Customer Code is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    If ItemCode.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "ItemCode is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    If OnHandQty.Length <= 0 And Adjustment.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "OnHand Quantity is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If

                    'Get CompanyId by Company Code
                    Dim objTabMOrganization As New PropertyTabMOrganization
                    objTabMOrganization = clsTabMOrganization.GetTabMOrganizationByCompanyCode(CompanyCode, objuserInfo("schemaName"))
                    If objTabMOrganization.CompanyID = 0 Then
                        WriteError("Row Number:" & counter & "|" & "CompanyCode:" & CompanyCode & "|" & "Not a valid companycode")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If

                    'Get CustomerID by Customer Code
                    Dim objTabMCustomers As New PropertyTabMCustomers
                    objTabMCustomers = clsTabMCustomers.GetAllTabMCustomersByCusCodeNDCompID(CustomerCode, objuserInfo("schemaName"), objTabMOrganization.CompanyID)
                    If objTabMCustomers.CustomerID = 0 Then
                        WriteError("Row Number:" & counter & "|" & "Customer Code:" & CustomerCode & "|" & "Not a valid Customer code")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    'Get ItemID by ItemCode
                    Dim dsItems = clsTabUOMMaster.GetTabUOMMasterByItemCode(ItemCode, objuserInfo("schemaName"), objTabMOrganization.CompanyID)
                    If dsItems Is Nothing Then
                        WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Not a valid Item code")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    'Check whether OB already exist for the item or nor

                    Dim GetExistingOnHandQty As String = ""
                    GetExistingOnHandQty = clsTabkEmptyInventory.CheckAndGetEmptyOpeningBal(objTabMOrganization.CompanyID, objTabMCustomers.CustomerID, 2, dsItems.Tables(0).Rows(0)("ItemID"), objuserInfo("schemaName"))
                    If GetExistingOnHandQty <> "" Then
                        OnHandQty = CInt(GetExistingOnHandQty)
                        If Adjustment.Length < 1 Then
                            WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Opening Balance already exists and Adjustment quantity not specified.")
                            ErrorsFound = True
                            GoTo NextRecord
                        End If
                        If Adjustment = 0 Then
                            WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Opening Balance already exists and Adjustment quantity Given as 0, So Record Skipped.")
                            ErrorsFound = True
                            GoTo NextRecord
                        End If
                    End If

                    'Dim ret1 = clsTabkEmptyInventory.CheckEmptyOpeningBalExist(objTabMOrganization.CompanyID, objTabMCustomers.CustomerID, 2, dsItems.Tables(0).Rows(0)("ItemID"), objuserInfo("schemaName"))
                    'If ret1 And Adjustment.Length <= 0 Then
                    '    WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Item already has opening balance.so,Enter adjustment quantity")
                    '    GoTo NextRecord
                    'Else
                    Dim NewRecordNotExists As Boolean = False
                    Dim OnHandAndAdjQtyIsZero As Boolean = False

                    If GetExistingOnHandQty = "" And Adjustment.Length > 0 Then
                        'WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "This is the first time entry.so,remove the adjustment quantity")
                        'GoTo NextRecord
                        Try
                            If Adjustment = 0 Then 'If onhand qty=0 and adjqty=0
                                OnHandAndAdjQtyIsZero = True
                            End If
                        Catch ex As Exception

                        End Try

                        NewRecordNotExists = True
                        OnHandQty = 0
                        Adjustment = 0
                    End If

                    'Save the record to the database from excel 
                    Dim objTabKEmptyInventory As New PropertyTabkEmptyInventory
                    objTabKEmptyInventory.CompanyID = objTabMOrganization.CompanyID
                    objTabKEmptyInventory.ItemID = dsItems.Tables(0).Rows(0)("ItemID")
                    objTabKEmptyInventory.OnHandQuantity = OnHandQty
                    objTabKEmptyInventory.TransitQuantity = 0
                    objTabKEmptyInventory.EntityID = objTabMCustomers.CustomerID
                    objTabKEmptyInventory.EntityTypeID = 2
                    objTabKEmptyInventory.DamagedQuantity = 0
                    Dim AdjustmentQty As Int32 = 0
                    If Adjustment.Length > 0 Then
                        AdjustmentQty = CInt(Adjustment)
                    Else
                        AdjustmentQty = 0
                    End If
                    Dim ret = clsTabkEmptyInventory.Save(objTabKEmptyInventory, objuserInfo("schemaName"), AdjustmentQty)
                    If ret Then
                        Dim objTabKEmptyOBHistory As New PropertyTabkEmptyOpeningBalanceHistory
                        Dim ds As New DataSet
                        objTabKEmptyOBHistory.InventoryID = objTabKEmptyInventory.InventoryID
                        objTabKEmptyOBHistory.Quantity = OnHandQty
                        objTabKEmptyOBHistory.AdjustmentQuantity = AdjustmentQty
                        objTabKEmptyOBHistory.CompanyID = objTabMOrganization.CompanyID
                        objTabKEmptyOBHistory.EntityType = 2
                        objTabKEmptyOBHistory.EntityID = objTabMCustomers.CustomerID
                        'If Adjustment.Length <= 0 Then
                        'If GetExistingOnHandQty < 1 Then
                        If GetExistingOnHandQty = "" Then
                            objTabKEmptyOBHistory.CreatedDate = System.DateTime.Now
                            objTabKEmptyOBHistory.CreatedBy = objuserrole("employeeID")
                            If txtTransactionNumber.Text = "" Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 2, objTabMOrganization.CompanyID, 0)
                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "OBC" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "OBC" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "OBC" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "OBC" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "OBC" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "OBC00001"
                                        txtTransactionNumber.Text = "OBC00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "OBC00001"
                                    txtTransactionNumber.Text = "OBC00001"
                                End If
                            Else
                                objTabKEmptyOBHistory.TransactionID = txtTransactionNumber.Text
                            End If
                        Else
                            objTabKEmptyOBHistory.ModifiedDate = System.DateTime.Now
                            objTabKEmptyOBHistory.ModifiedBy = objuserrole("employeeID")
                            If txtTransactionNumber.Text = "" Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 2, objTabMOrganization.CompanyID, 1)
                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "ADC" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "ADC" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "ADC" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "ADC" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "ADC" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "ADC00001"
                                        txtTransactionNumber.Text = "ADC00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "ADC00001"
                                    txtTransactionNumber.Text = "ADC00001"
                                End If
                            Else
                                objTabKEmptyOBHistory.TransactionID = txtTransactionNumber.Text
                            End If

                        End If
                        ret = clsTabkEmptyOpeningBalanceHistory.Save(objTabKEmptyOBHistory, objuserInfo("schemaName"), "")
                    End If
                    txtTransactionNumber.Text = ""

                    If NewRecordNotExists = True And OnHandAndAdjQtyIsZero = False Then
                        GoTo ContinueForAdjustments
                    End If

NextRecord:
                End While
                reader.Close()

            Catch ex As Exception
                ErrorsFound = True
                WriteError("Errors:" & ex.Message.ToString)
                objCommand = Nothing
                objXConn = Nothing
                objCommand = Nothing
                Exit Sub
            End Try

            objCommand = Nothing
            objXConn = Nothing
            objCommand = Nothing
            txtTransactionNumber.Text = ""
        End Sub

        Private Sub ImportSupplierOpeningBal()
            Dim reader As OleDbDataReader
            Try
                WriteError("Importing Suppliers Opening Balances started at  --" & DateTime.Now.ToString())
                WriteError("__________________________________________________________")
                ' retrieve the Select Command for the worksheet data
                Dim objCommand As New OleDbCommand()
                objCommand = ExcelConnection(ConfigurationManager.AppSettings("SupplierOpeningBalSheetName"))

                '  create a DataReader
                reader = objCommand.ExecuteReader()
                Dim counter As Integer = 0 ' used for testing your import in smaller increments

                While reader.Read()
                    counter = counter + 1 ' counter to exit early for testing...
                    ' set default values for loop
ContinueForAdjustments:
                    Dim CompanyCode As String = Convert.ToString(reader("CompanyCode"))
                    Dim SupplierCode As String = Convert.ToString(reader("SupplierCode"))
                    Dim ItemCode As String = Convert.ToString(reader("ItemCode"))
                    Dim OnHandQty As String = Convert.ToString(reader("OnHandQty"))
                    Dim Adjustment As String = Convert.ToString(reader("Adjustment"))
                    'Validations
                    If CompanyCode.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "CompanyCode is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    If SupplierCode.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "Supplier Code is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    If ItemCode.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "ItemCode is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If

                    If OnHandQty.Length <= 0 And Adjustment.Length <= 0 Then
                        WriteError("Row Number:" & counter & "|" & "OnHand Quantity is required")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If

                    'Get CompanyId by Company Code
                    Dim objTabMOrganization As New PropertyTabMOrganization
                    objTabMOrganization = clsTabMOrganization.GetTabMOrganizationByCompanyCode(CompanyCode, objuserInfo("schemaName"))
                    If objTabMOrganization.CompanyID = 0 Then
                        WriteError("Row Number:" & counter & "|" & "CompanyCode:" & CompanyCode & "|" & "Not a valid companycode")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If

                    'Get CustomerID by Customer Code
                    Dim objTabMSuppliers As New PropertyTabMSuppliers
                    objTabMSuppliers = clsTabMSuppliers.GetTabMSuppliersByCode(objTabMOrganization.CompanyID, SupplierCode, objuserInfo("schemaName"))
                    If objTabMSuppliers.SupplierID = 0 Then
                        WriteError("Row Number:" & counter & "|" & "Supplier Code:" & SupplierCode & "|" & "Not a valid Supplier Code")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    'Get ItemID by ItemCode
                    Dim dsItems = clsTabUOMMaster.GetTabUOMMasterByItemCode(ItemCode, objuserInfo("schemaName"), objTabMOrganization.CompanyID)
                    If dsItems Is Nothing Then
                        WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Not a valid Item code")
                        ErrorsFound = True
                        GoTo NextRecord
                    End If
                    'Check whether OB already exist for the item or nor

                    'Dim ret1 = clsTabkEmptyInventory.CheckEmptyOpeningBalExist(objTabMOrganization.CompanyID, objTabMBranchPlant.BranchID, 1, dsItems.Tables(0).Rows(0)("ItemID"), objuserInfo("schemaName"))
                    Dim GetExistingOnHandQty As String
                    GetExistingOnHandQty = clsTabkEmptyInventory.CheckAndGetEmptyOpeningBal(objTabMOrganization.CompanyID, objTabMSuppliers.SupplierID, 3, dsItems.Tables(0).Rows(0)("ItemID"), objuserInfo("schemaName"))
                    If GetExistingOnHandQty <> "" Then
                        OnHandQty = CInt(GetExistingOnHandQty)
                        If Adjustment.Length < 1 Then
                            WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Opening Balance already exists and Adjustment quantity not specified.")
                            ErrorsFound = True
                            GoTo NextRecord
                        End If
                        If Adjustment = 0 Then
                            WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Opening Balance already exists and Adjustment quantity Given as 0, So Record Skipped.")
                            ErrorsFound = True
                            GoTo NextRecord
                        End If
                    End If

                    'Dim ret1 = clsTabkEmptyInventory.CheckEmptyOpeningBalExist(objTabMOrganization.CompanyID, objTabMSuppliers.SupplierID, 3, dsItems.Tables(0).Rows(0)("ItemID"), objuserInfo("schemaName"))
                    'If ret1 And Adjustment.Length <= 0 Then
                    '    WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "Item already has opening balance.so,Enter adjustment quantity")
                    '    GoTo NextRecord
                    'Else
                    Dim NewRecordNotExists As Boolean = False
                    Dim OnHandAndAdjQtyIsZero As Boolean = False
                    If GetExistingOnHandQty = "" And Adjustment.Length > 0 Then
                        'WriteError("Row Number:" & counter & "|" & "Item Code:" & ItemCode & "|" & "This is the first time entry.so,remove the adjustment quantity")
                        'GoTo NextRecord
                        Try
                            If Adjustment = 0 Then 'If onhand qty=0 and adjqty=0
                                OnHandAndAdjQtyIsZero = True
                            End If
                        Catch ex As Exception

                        End Try

                        NewRecordNotExists = True
                        OnHandQty = 0
                        Adjustment = 0
                    End If
                    'Save the record to the database from excel 
                    Dim objTabKEmptyInventory As New PropertyTabkEmptyInventory
                    objTabKEmptyInventory.CompanyID = objTabMOrganization.CompanyID
                    objTabKEmptyInventory.ItemID = dsItems.Tables(0).Rows(0)("ItemID")
                    objTabKEmptyInventory.OnHandQuantity = OnHandQty
                    objTabKEmptyInventory.TransitQuantity = 0
                    objTabKEmptyInventory.EntityID = objTabMSuppliers.SupplierID
                    objTabKEmptyInventory.EntityTypeID = 3
                    objTabKEmptyInventory.DamagedQuantity = 0
                    Dim AdjustmentQty As Int32
                    If Adjustment.Length > 0 Then
                        AdjustmentQty = Adjustment
                    Else
                        AdjustmentQty = 0
                    End If
                    Dim ret = clsTabkEmptyInventory.Save(objTabKEmptyInventory, objuserInfo("schemaName"), AdjustmentQty)
                    If ret Then
                        Dim objTabKEmptyOBHistory As New PropertyTabkEmptyOpeningBalanceHistory
                        Dim ds As New DataSet
                        objTabKEmptyOBHistory.InventoryID = objTabKEmptyInventory.InventoryID
                        objTabKEmptyOBHistory.Quantity = OnHandQty
                        objTabKEmptyOBHistory.AdjustmentQuantity = AdjustmentQty
                        objTabKEmptyOBHistory.CompanyID = objTabMOrganization.CompanyID
                        objTabKEmptyOBHistory.EntityType = 3
                        objTabKEmptyOBHistory.EntityID = objTabMSuppliers.SupplierID
                        'If Adjustment.Length <= 0 Then
                        'If GetExistingOnHandQty < 1 Then
                        If GetExistingOnHandQty = "" Then

                            objTabKEmptyOBHistory.CreatedDate = System.DateTime.Now
                            objTabKEmptyOBHistory.CreatedBy = objuserrole("employeeID")
                            If txtTransactionNumber.Text = "" Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 3, objTabMOrganization.CompanyID, 0)
                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "OBS" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "OBS" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "OBS" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "OBS" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "OBS" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "OBS00001"
                                        txtTransactionNumber.Text = "OBS00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "OBS00001"
                                    txtTransactionNumber.Text = "OBS00001"
                                End If
                            Else
                                objTabKEmptyOBHistory.TransactionID = txtTransactionNumber.Text
                            End If
                        Else
                            objTabKEmptyOBHistory.ModifiedDate = System.DateTime.Now
                            objTabKEmptyOBHistory.ModifiedBy = objuserrole("employeeID")
                            If txtTransactionNumber.Text = "" Then
                                ds = clsTabkEmptyOpeningBalanceHistory.GetMaxTransactionNumber_OBHistory(objuserInfo("schemaName"), 2, objTabMOrganization.CompanyID, 1)
                                If Not ds Is Nothing Then
                                    If ds.Tables(0).Rows.Count > 0 Then
                                        Dim str3 As String = ""
                                        Dim str As String = ds.Tables(0).Rows(0)(0)
                                        Dim str1 = str.Substring(3, str.Length - 3)
                                        Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                        If str2.Length = 1 Then
                                            str3 = "ADS" + "0000" + str2
                                        ElseIf str2.Length = 2 Then
                                            str3 = "ADS" + "000" + str2
                                        ElseIf str2.Length = 3 Then
                                            str3 = "ADS" + "00" + str2
                                        ElseIf str2.Length = 4 Then
                                            str3 = "ADS" + "0" + str2
                                        ElseIf str2.Length >= 5 Then
                                            str3 = "ADS" + str2
                                        End If
                                        objTabKEmptyOBHistory.TransactionID = str3
                                        txtTransactionNumber.Text = str3
                                    Else
                                        objTabKEmptyOBHistory.TransactionID = "ADS00001"
                                        txtTransactionNumber.Text = "ADS00001"
                                    End If
                                Else
                                    objTabKEmptyOBHistory.TransactionID = "ADS00001"
                                    txtTransactionNumber.Text = "ADS00001"
                                End If
                            Else
                                objTabKEmptyOBHistory.TransactionID = txtTransactionNumber.Text
                            End If

                        End If
                        ret = clsTabkEmptyOpeningBalanceHistory.Save(objTabKEmptyOBHistory, objuserInfo("schemaName"), "")
                    End If
                    txtTransactionNumber.Text = ""
                    If NewRecordNotExists = True And OnHandAndAdjQtyIsZero = False Then
                        GoTo ContinueForAdjustments
                    End If

NextRecord:
                End While
                reader.Close()

            Catch ex As Exception
                ErrorsFound = True
                WriteError("Errors:" & ex.Message.ToString)
                objCommand = Nothing
                objXConn = Nothing
                objCommand = Nothing
                Exit Sub
            End Try

            objCommand = Nothing
            objXConn = Nothing
            objCommand = Nothing
            txtTransactionNumber.Text = ""
        End Sub

        Protected Function ExcelConnection(ByVal sheetName As String) As OleDbCommand

            ' Connect to the Excel Spreadsheet
            Dim Path As String
            Path = Server.MapPath(".")



            Dim pos As Int32 = Path.LastIndexOf("\")
            Path = Path.Substring(0, pos)
            pos = Path.LastIndexOf("\")
            Path = Path.Substring(0, pos)
            Path = Path & "\ExcelImport.xls"
            Path.Replace("\", "/")

            Dim xConnStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;" & _
                        "Data Source=" & Path & ";" & _
            "Extended Properties=" & Convert.ToChar(34).ToString() & "Excel 12.0;Imex=1; HDR=Yes; " + Convert.ToChar(34).ToString()

            ' use a SQL Select command to retrieve the data from the Excel Spreadsheet
            ' the "table name" is the name of the worksheet within the spreadsheet
            ' in this case, the worksheet name is "Members" and is coded as: [Members$]
            Try

                '"Extended Properties=Excel 8.0;IMEX=1"
                ' create your excel connection object using the connection string
                objXConn = New OleDbConnection(xConnStr)
                objXConn.Open()

                Dim objCommand = New OleDbCommand("SELECT * FROM [" & sheetName & "$]", objXConn)
                Return objCommand
            Catch ex As Exception
                'WriteError("here exception at 1462")
                objCommand = Nothing
                objXConn = Nothing
                objCommand = Nothing
                Return Nothing
            End Try
        End Function

        Private Sub AsyncUpload1_FileUploaded(ByVal sender As Object, ByVal e As Telerik.Web.UI.FileUploadedEventArgs) Handles AsyncUpload1.FileUploaded
            If AsyncUpload1.UploadedFiles.Count > 0 Then
                Dim fileExt As String = AsyncUpload1.UploadedFiles(0).FileName 'EmptyKegsOpeningBalance.xlsx
                Try
                    Dim Path As String
                    Path = Server.MapPath(".")

                    Dim pos As Int32 = Path.LastIndexOf("\")
                    Path = Path.Substring(0, pos)
                    pos = Path.LastIndexOf("\")
                    Path = Path.Substring(0, pos)
                    Path = Path & "\ExcelImport.xls"
                    Path.Replace("\", "/")

                    ' alter path for your project
                    AsyncUpload1.UploadedFiles(0).SaveAs(Path)
                    lblFileName.Text = "File Uploaded Successfully : " + fileExt
                    lblSuccess.Text = ""
                Catch ex As Exception

                End Try

            End If
        End Sub

        Private Sub WriteError(ByVal errorMessage As String)
            Try
                Dim Path As String
                Path = Server.MapPath(".")

                Dim pos As Int32 = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)
                pos = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)
                Path = Path & "\OpErrorLog.txt"
                Path.Replace("\", "/")
                If (Not File.Exists(path)) Then
                    File.Create(path).Close()

                End If
                Using w As StreamWriter = File.AppendText(path)
                    ''w.WriteLine(Constants.vbCrLf & "Log Entry : ")
                    'w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture))
                    'Dim err As String = "Error Message:" & errorMessage & " AT" & DateTime.Now.ToString(CultureInfo.InvariantCulture)
                    ' w.WriteLine(err)
                    w.WriteLine(errorMessage)
                    ''w.WriteLine("__________________________")
                    w.Flush()
                    w.Close()

                End Using
            Catch ex As Exception
                WriteError(ex.Message)
            End Try

        End Sub

        Private Sub RadgvOpeningBalanceHistory_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadgvOpeningBalanceHistory.PageIndexChanged
            BindHistoryGrid()
        End Sub

        Private Sub BindHistoryGrid()
            'check below validation
            If rcbFromCmpny.SelectedIndex < 1 Or hdfEntityType.Value.Length < 1 Then Return

            Dim dsHistory As New DataSet
            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            If rbtEntityType.SelectedValue = 1 Then
                dsHistory = clsTabkEmptyOpeningBalanceHistory.GetAllTabkEmptyOBHistoryByEntityType(CInt(rcbFromCmpny.SelectedValue), hdfEntityType.Value, 1, objuserInfo("schemaName"))
            ElseIf rbtEntityType.SelectedValue = 2 Then
                dsHistory = clsTabkEmptyOpeningBalanceHistory.GetAllTabkEmptyOBHistoryByEntityType(CInt(rcbFromCmpny.SelectedValue), hdfEntityType.Value, 2, objuserInfo("schemaName"))
            ElseIf rbtEntityType.SelectedValue = 3 Then
                dsHistory = clsTabkEmptyOpeningBalanceHistory.GetAllTabkEmptyOBHistoryByEntityType(CInt(rcbFromCmpny.SelectedValue), hdfEntityType.Value, 3, objuserInfo("schemaName"))
            End If
            If Not dsHistory Is Nothing Then
                If dsHistory.Tables(0).Rows.Count > 0 Then
                    RadgvOpeningBalanceHistory.DataSource = dsHistory
                    RadgvOpeningBalanceHistory.DataBind()
                Else
                    RadgvOpeningBalanceHistory.DataSource = dsHistory
                    RadgvOpeningBalanceHistory.DataBind()
                End If
            Else
                RadgvOpeningBalanceHistory.DataSource = dsHistory
                RadgvOpeningBalanceHistory.DataBind()
            End If
        End Sub

        Private Sub RadgvOpeningBalanceHistory_PageSizeChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageSizeChangedEventArgs) Handles RadgvOpeningBalanceHistory.PageSizeChanged
            BindHistoryGrid()
        End Sub

        Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs)
            'If TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode Then
            '    Dim item As GridEditFormItem = DirectCast(e.Item, GridEditFormItem)
            '    Dim textbox As TextBox = DirectCast(item("txtAdjustReason").Controls(0), TextBox)
            '    ' Where CustomerID is ColumnUniqueName
            '    ' Set width 
            '    '  textbox.TextMode = TextBoxMode.MultiLine;  // Set to MultiLine mode 
            '    textbox.Width = Unit.Pixel(20)
            'End If
        End Sub
        Protected Sub RadgvStockOpeningBalance_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvStockOpeningBalance.ItemCreated
            If TypeOf e.Item Is GridDataItem Then
                Try
                    Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                    Dim txt As TextBox = TryCast(item.FindControl("txtAdjustmentQuantity"), TextBox)

                    Dim invID As Label = DirectCast(item.FindControl("lblInventoryID"), Label)
                    If Len(invID.Text) = 0 Then
                        txt.Enabled = False
                    Else
                        txt.Enabled = True
                    End If

                Catch ex As Exception
                End Try
            End If
            'If (TypeOf e.Item Is GridEditableItem) AndAlso (e.Item.IsInEditMode) Then
            '    Dim edititem As GridEditableItem = DirectCast(e.Item, GridEditableItem)
            '    Dim txtbx As TextBox = DirectCast(edititem("txtAdjustReason").Controls(0), TextBox)
            '    txtbx.Width = Unit.Pixel(20)
            'End If
            'If TypeOf e.Item Is GridDataItem Then
            '    Dim edititem As GridEditableItem = DirectCast(e.Item, GridEditableItem)
            '    Dim txtbx As TextBox = DirectCast(edititem("txtAdjustReason").Controls(0), TextBox)
            '    txtbx.Width = Unit.Pixel(20)
            '    Dim editItem2 As GridDataItem = DirectCast(e.Item, GridDataItem)
            '    Dim txtbx2 As TextBox = DirectCast(editItem2.FindControl("txtAdjustReason"), TextBox)
            '    txtbx2.Width = Unit.Pixel(10)

            '    'If (e.Item.IsInEditMode) Then
            '    '    Dim editItem2 As GridEditFormItem = DirectCast(e.Item, GridEditFormItem)
            '    '    Dim txtbx2 As TextBox = DirectCast(editItem2.FindControl("txtAdjustReason").Controls(0), TextBox)
            '    '    txtbx2.Width = Unit.Pixel(10)
            '    'End If

            '    Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            '    Dim txt As RadNumericTextBox = TryCast(item.FindControl("txtAdjustmentQuantity"), RadNumericTextBox)
            '    Dim invID As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
            '    If invID.Text = hdfItemID.Value Then
            '        txt.Focus()
            '    End If



            '    Dim rntx As RadNumericTextBox = DirectCast(item.FindControl("txtAdjustmentQuantity"), RadNumericTextBox)
            '    Dim invVal As Integer = 0
            '    Try
            '        If hdfItemID.Value.Length < 1 And RadgvStockOpeningBalance.Items.Count < 1 Then
            '            rntx.Enabled = False
            '        Else
            '            rntx.Enabled = True
            '        End If
            '        'invVal = item.DataItem("InventoryID")
            '        'If invVal = "0" Then
            '        '    rntx.Enabled = False
            '        'End If
            '    Catch ex As Exception
            '        'If invVal = "0" Then
            '        '    rntx.Enabled = False
            '        'End If
            '    End Try

            'End If
        End Sub

        Protected Sub rcbFromCmpny_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbFromCmpny.SelectedIndexChanged
            txtEntityType.Text = ""
            hdfEntityType.Value = ""
            ClearFields()
            If rcbFromCmpny.SelectedIndex > 0 Then
                RadgvOpeningBalanceHistory.DataSource = ""
                RadgvOpeningBalanceHistory.DataBind()
                RadgvStockOpeningBalance.DataSource = ""
                RadgvStockOpeningBalance.DataBind()
                'BindBranchPlant()
            Else
                'ClearFields()
                RadgvOpeningBalanceHistory.DataSource = ""
                RadgvOpeningBalanceHistory.DataBind()
                RadgvStockOpeningBalance.DataSource = ""
                RadgvStockOpeningBalance.DataBind()
                'ddlBranchPlant.Items.Clear()
                'ddlBranchPlant.Items.Insert(0, New RadComboBoxItem("--Select One--"))
            End If
            AddContextKey()
            txtEntityType.Focus()
        End Sub

        Private Sub AddContextKey()
            If objuserInfo.Count > 1 And rcbFromCmpny.SelectedIndex > 0 Then
                txtItemCode_AutoCompleteExtender.ContextKey = rcbFromCmpny.SelectedValue & "," & objuserInfo("schemaName")
            Else
                txtItemCode_AutoCompleteExtender.ContextKey = ""
            End If

            Select Case rbtEntityType.SelectedValue
                Case 1  'BP
                    AutoCompleteExtender_txtEntityType.ServiceMethod = "BranchPlantLookup"
                Case 2  'Customer
                    AutoCompleteExtender_txtEntityType.ServiceMethod = "CustomerLookup"
                Case 3  'Supplier
                    AutoCompleteExtender_txtEntityType.ServiceMethod = "SupplierLookup"
            End Select

            If rcbFromCmpny.SelectedIndex > 0 Then
                AutoCompleteExtender_txtEntityType.ContextKey = rcbFromCmpny.SelectedValue & "," & objuserInfo("schemaName")
            Else
                AutoCompleteExtender_txtEntityType.ContextKey = ""
            End If
        End Sub

        Private Sub txtEntityType_TextChanged(sender As Object, e As System.EventArgs) Handles txtEntityType.TextChanged
            lblMsg.Text = ""
            ClearFields()
            rntxtQuantity.Enabled = True
            RadgvStockOpeningBalance.DataSource = ""
            RadgvStockOpeningBalance.DataBind()
            RadgvOpeningBalanceHistory.DataSource = ""
            RadgvOpeningBalanceHistory.DataBind()
            ViewState("table") = Nothing

            Dim Itemcode As String = ""
            Dim ds As New DataSet
            GetEntityTypeIDs()

            BindHistoryGrid()
            If txtItemCode.Text.Trim.Length > 0 Then
                'getexistingopeningbalance()
                If hdfEntityType.Value.Length > 0 Then
                    Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
                    If rbtEntityType.SelectedValue = 1 Then
                        ds = clsTabkEmptyInventory.GetAllTabKEmptyInventoryLatestOB(CInt(rcbFromCmpny.SelectedValue), hdfEntityType.Value, 1, objuserInfo("schemaName"))
                    ElseIf rbtEntityType.SelectedValue = 2 Then
                        ds = clsTabkEmptyInventory.GetAllTabKEmptyInventoryLatestOB(CInt(rcbFromCmpny.SelectedValue), hdfEntityType.Value, 2, objuserInfo("schemaName"))
                    ElseIf rbtEntityType.SelectedValue = 3 Then
                        ds = clsTabkEmptyInventory.GetAllTabKEmptyInventoryLatestOB(CInt(rcbFromCmpny.SelectedValue), hdfEntityType.Value, 3, objuserInfo("schemaName"))
                    End If
                    If Not ds Is Nothing Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            RadgvStockOpeningBalance.DataSource = ds
                            RadgvStockOpeningBalance.DataBind()
                            dt = ds.Tables(0)
                            ViewState("table") = dt
                        Else
                            RadgvStockOpeningBalance.DataSource = ds
                            RadgvStockOpeningBalance.DataBind()
                            ViewState("table") = Nothing
                        End If
                    Else
                        RadgvStockOpeningBalance.DataSource = ds
                        RadgvStockOpeningBalance.DataBind()
                        ViewState("table") = Nothing
                    End If
                    BindHistoryGrid()
                Else
                    ''RadgvStockOpeningBalance.DataSource = ""
                    ''RadgvStockOpeningBalance.DataBind()
                    ''RadgvOpeningBalanceHistory.DataSource = ""
                    ''RadgvOpeningBalanceHistory.DataBind()
                    ViewState("table") = Nothing
                End If
            End If

            txtItemCode.Focus()
        End Sub

        Private Sub GetEntityTypeIDs()

            If rcbFromCmpny.SelectedIndex > 0 Then
                Dim EntityID As Integer = 0
                Dim chrEntityType As Char = ""

                Select Case rbtEntityType.SelectedValue
                    Case 1  'BP
                        chrEntityType = "B"
                    Case 2  'Customer
                        chrEntityType = "C"
                    Case 3  'Supplier
                        chrEntityType = "S"
                End Select

                EntityID = clsValidations.GetLookUpIDByCode(rcbFromCmpny.SelectedValue, txtEntityType.Text, chrEntityType, objuserInfo("schemaName"))

                If EntityID > 0 Then
                    hdfEntityType.Value = EntityID
                Else
                    RadgvStockOpeningBalance.DataSource = ""
                    RadgvStockOpeningBalance.DataBind()
                    RadgvOpeningBalanceHistory.DataSource = ""
                    RadgvOpeningBalanceHistory.DataBind()
                    lblMsg.Text = "Please select the Entity Type."
                    hdfEntityType.Value = ""
                    txtEntityType.Text = ""
                    txtEntityType.Focus()
                End If

            Else
                lblMsg.Text = "Please select the Company."
                txtEntityType.Text = ""
                hdfEntityType.Value = ""
            End If

        End Sub

        Private Sub RadgvStockOpeningBalance_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvStockOpeningBalance.ItemDataBound
            If TypeOf e.Item Is GridDataItem Then
                Try
                    Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                    Dim txt As TextBox = TryCast(item.FindControl("txtAdjustmentQuantity"), TextBox)

                    Dim invID As Label = DirectCast(item.FindControl("lblInventoryID"), Label)
                    If invID.Text = "0" Then
                        txt.Enabled = False
                    Else
                        txt.Enabled = True
                    End If

                    ''Dim editItem As GridDataItem = DirectCast(e.Item, GridDataItem)
                    ''Dim txtbx As TextBox = DirectCast(editItem.FindControl("txtAdjustReason"), TextBox)
                    ''txtbx.Width = Unit.Pixel(10)

                    ''If (e.Item.IsInEditMode) Then
                    ''    Dim editItem2 As GridEditFormItem = DirectCast(e.Item, GridEditFormItem)
                    ''    Dim txtbx2 As TextBox = DirectCast(editItem2.FindControl("txtAdjustReason"), TextBox)
                    ''    txtbx2.Width = Unit.Pixel(10)
                    ''End If


                Catch ex As Exception

                End Try

            End If


        End Sub
    End Class
End Namespace
