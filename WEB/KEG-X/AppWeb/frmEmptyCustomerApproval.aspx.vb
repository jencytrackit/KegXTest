'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmFullKegReceiveList_FromSupplier.aspx
'Created By         :
'File navigation    :
'Created Date       :November 08, 2013, 2:46:43 PM
'Description        :This file is used to List the Full Kegs Details that comes from the Supplier...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports Telerik.Web.UI
'Imports System.Windows.Forms

Namespace TrackITKTS
    Partial Class frmEmptyCustomerApproval
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
                BindUsername()
            End If
            AddContextKey()
        End Sub
        Private Sub AddContextKey()
            If objuserInfo.Count > 1 And objuserrole("employeeID") > 0 Then
                AutoCompleteExtender_txtCustomer.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
            Else
                AutoCompleteExtender_txtCustomer.ContextKey = ""
            End If
            If objuserInfo.Count > 1 And objuserrole("employeeID") > 0 Then
                AutoCompleteExtender_txtItemCode.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
            Else
                AutoCompleteExtender_txtItemCode.ContextKey = ""
            End If
        End Sub
        Private Sub BindUsername()
            rcbUserName.Items.Clear()
            Dim dsuser As New DataSet
            dsuser = clsTabKEmptyCustomer.GetAllUsers(objuserInfo("schemaName"))
            If dsuser.Tables(0).Rows.Count > 0 Then
                rcbUserName.DataSource = dsuser
                rcbUserName.DataTextField = "UserName"
                rcbUserName.DataValueField = "UserID"
                rcbUserName.DataBind()
            End If
            rcbUserName.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        End Sub


        Private Sub RadgvEmptyCustomerApproval_DetailTableDataBind(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadgvEmptyCustomerApproval.DetailTableDataBind
            Dim dataItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
            If e.DetailTableView.Name = "NestedChildItems" Then

                Dim CompanyID As Int32 = dataItem("FromCompanyID").Text
                Dim TransactionNumber As String = dataItem.GetDataKeyValue("TransactionNum").ToString()

                Dim rbnapprovedvalue As Int32
                If rbnApp.SelectedItem.Value = 1 Then
                    rbnapprovedvalue = 1
                ElseIf rbnApp.SelectedItem.Value = 0 Then
                    rbnapprovedvalue = 0
                ElseIf rbnApp.SelectedItem.Value = 2 Then
                    rbnapprovedvalue = 2
                End If


                If txtItemCode.Text.Trim.Length > 0 Then
                    e.DetailTableView.DataSource = clsTabKEmptyCustomer.GetAllTabKEmptyCustomerApprovalHHT(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), hdfItemCode.Value, TransactionNumber, rbnapprovedvalue)
                Else
                    e.DetailTableView.DataSource = clsTabKEmptyCustomer.GetAllTabKEmptyCustomerApprovalHHT(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), "", TransactionNumber, rbnapprovedvalue)
                End If

            End If

        End Sub

        Private Sub RadgvEmptyCustomerApproval_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadgvEmptyCustomerApproval.ItemCommand

            If e.CommandName = "ApproveSelected" Then
                lblMsg.Text = ""
                Dim ParentGridDataItem As GridDataItem = CType(e.Item, GridDataItem)
                Dim trnxNumber As String = String.Empty
                Dim SelectedManually As Byte = 0

                If e.Item.OwnerTableView.Name = "ParentGrid" Then
                    trnxNumber = ParentGridDataItem.GetDataKeyValue("TransactionNum")

                    For Each childItem As GridDataItem In ParentGridDataItem.ChildItem.NestedTableViews(0).Items
                        If SelectedManually = 0 Then
                            SelectedManually = 1
                        End If
                        'Dim intApprovedID As Integer = childItem.GetDataKeyValue("ApprovalID")
                        Dim FromCompID As Integer = Convert.ToInt32(DirectCast(childItem.FindControl("txtFromComp"), RadTextBox).Text)
                        Dim ToCompID As Integer = Convert.ToInt32(DirectCast(childItem.FindControl("txtToComp"), RadTextBox).Text)
                        Dim FromItmID As Integer = Convert.ToInt32(DirectCast(childItem.FindControl("txtFromItm"), RadTextBox).Text)
                        Dim ToItmID As Integer = Convert.ToInt32(DirectCast(childItem.FindControl("txtToItm"), RadTextBox).Text)
                        Dim intApprovedQuantity As Integer = Convert.ToInt32(DirectCast(childItem.FindControl("txtQuantity"), RadTextBox).Text)
                        Dim strComments As String = DirectCast(childItem.FindControl("txtComments"), RadTextBox).Text
                        Dim ActualQty As Integer = Convert.ToInt32(DirectCast(childItem.FindControl("txtActQty"), RadTextBox).Text)
                        Dim chkChecked As System.Web.UI.WebControls.CheckBox

                        Dim retVal As Boolean

                        If intApprovedQuantity > ActualQty Then
                            lblMsg.Text = "Approved quantity should not be greater than actual quantity"
                            Exit Sub
                        End If


                        If e.CommandName = "ApproveSelected" Then
                            chkChecked = DirectCast(childItem.FindControl("chkselect"), System.Web.UI.WebControls.CheckBox)
                            If chkChecked.Checked = True And chkChecked.Enabled = True Then
                                retVal = clsTabKEmptyCustomer.UpdateTabKEmptyKegCustomerReturnsHHT(trnxNumber, FromCompID, ToCompID, FromItmID, ToItmID, intApprovedQuantity, strComments, objuserInfo("schemaName"))
                                lblMsg.Text = "Customer Returns Approved Successfully!"
                                SelectedManually = 2
                            End If
                            'Else
                            '    retVal = clsTabKEmptyCustomer.UpdateTabKEmptyKegCustomerReturnsHHT(intApprovedID, intApprovedQuantity, strComments, objuserInfo("schemaName"))
                        End If
                    Next
                    'ElseIf lblMsg.Text.Length > 1 Then
                    '    RadgvEmptyCustomerApproval.Rebind()

                    If SelectedManually = 1 Then
                        lblMsg.Text = "Please check atleast one item from the list for Approval"
                        Return
                    ElseIf SelectedManually = 0 Then
                        'trnxNumber = ParentGridDataItem.GetDataKeyValue("TransactionNum")

                        Dim CompanyID As Int32 = e.Item.Cells(2).Text 'm("FromCompanyID")
                        Dim rbnapprovedvalue As Int32

                        rbnapprovedvalue = 0 'Not Approved Records Only

                        Dim dsAll As DataSet
                        If txtItemCode.Text.Trim.Length > 0 Then
                            dsAll = clsTabKEmptyCustomer.GetAllTabKEmptyCustomerApprovalHHT(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), hdfItemCode.Value, trnxNumber, rbnapprovedvalue)
                        Else
                            dsAll = clsTabKEmptyCustomer.GetAllTabKEmptyCustomerApprovalHHT(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), "", trnxNumber, rbnapprovedvalue)
                        End If

                        For Each dr As DataRow In dsAll.Tables(0).Rows
                            Dim FromCompnyID As Integer = Convert.ToInt32(dr(0).ToString)
                            Dim ToCompnyID As Integer = Convert.ToInt32(dr(1).ToString)
                            Dim FromItemmID As Integer = Convert.ToInt32(dr(2).ToString)
                            Dim ToItemmID As Integer = Convert.ToInt32(dr(3).ToString)
                            Dim intApprovedQuantity As Integer = Convert.ToInt32(dr(4).ToString)
                            Dim strComments As String = String.Empty
                            'Dim chkChecked As CheckBox

                            Dim retVal As Boolean
                            retVal = clsTabKEmptyCustomer.UpdateTabKEmptyKegCustomerReturnsHHT(trnxNumber, FromCompnyID, ToCompnyID, FromItemmID, ToItemmID, intApprovedQuantity, strComments, objuserInfo("schemaName"))
                            lblMsg.Text = "Customer Returns Approved Successfully!"
                            SelectedManually = 2
                        Next

                    End If
                    If SelectedManually = 2 Then
                        RadgvEmptyCustomerApproval.Rebind()
                        radSearch_Click(Nothing, Nothing)
                    End If

                End If

            ElseIf e.CommandName = "Update" Then
                lblMsg.Text = ""
                Dim ParentGridDataItem As GridDataItem = CType(e.Item, GridDataItem)
                Dim trnxNumber As String = String.Empty
                Dim SelectedManually As Byte = 0

                If e.Item.OwnerTableView.Name = "ParentGrid" Then
                    trnxNumber = ParentGridDataItem.GetDataKeyValue("TransactionNum")
                    Dim FromCompanyID As Int32 = e.Item.Cells(2).Text
                    'Dim CustomerCode As String = e.Item.Cells(5).Text
                    Dim cbxCustomer As RadComboBox = CType(e.Item.Cells(6).FindControl("cbxCustomer"), RadComboBox)
                    Dim FromCustomerID As Int64 = Convert.ToInt64(cbxCustomer.SelectedValue.Split("|")(0))
                    Dim ToCompanyID As Int32 = e.Item.Cells(7).Text
                    'Dim BranchCode As String = e.Item.Cells(8).Text
                    Dim cbxBranch As RadComboBox = CType(e.Item.Cells(9).FindControl("cbxBranch"), RadComboBox)
                    Dim ToBranchID As Int64 = Convert.ToInt64(cbxBranch.SelectedValue.Split("|")(0))

                    Dim retVal As Boolean = clsTabKEmptyCustomer.UpdateTabKEmptyKegCustomerReturns(trnxNumber, FromCompanyID, FromCustomerID, ToBranchID, ToCompanyID, objuserInfo("schemaName"))
                    lblMsg.Text = "Return Details Updated Successfully!"


                    RadgvEmptyCustomerApproval.Rebind()
                    radSearch_Click(Nothing, Nothing)

                End If

            End If



        End Sub

        Private Sub RadgvEmptyCustomerApproval_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvEmptyCustomerApproval.ItemDataBound
            If e.Item.ItemType = Telerik.Web.UI.GridItemType.AlternatingItem Or e.Item.ItemType = Telerik.Web.UI.GridItemType.Item Then
                Dim CompCustID As String = e.Item.Cells(2).Text + "|" + e.Item.Cells(5).Text
                'Dim ds As DataSet = clsTabMCustomers.GetRptCustomersDrop(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"))
                'Dim cbxCustomer As RadComboBox = CType(e.Item.Cells(6).FindControl("cbxCustomer"), RadComboBox)
                'cbxCustomer.DataSource = ds
                'cbxCustomer.DataTextField = "CustomerName"
                'cbxCustomer.DataValueField = "CustomerCode"
                'cbxCustomer.SelectedValue = e.Item.Cells(5).Text
                Dim cbxCustomer As RadComboBox = CType(e.Item.Cells(6).FindControl("cbxCustomer"), RadComboBox)
                For Each ComboItem As RadComboBoxItem In cbxCustomer.Items
                    If (ComboItem.Value.Split("|")(0).ToString() <> e.Item.Cells(2).Text) Then
                        ComboItem.Visible = False
                    Else
                        ComboItem.Visible = True
                    End If
                Next
                cbxCustomer.SelectedValue = CompCustID
                cbxCustomer.ToolTip = cbxCustomer.SelectedItem.Text

                Dim CompBranchID As String = e.Item.Cells(7).Text + "|" + e.Item.Cells(8).Text
                Dim cbxBranch As RadComboBox = CType(e.Item.Cells(9).FindControl("cbxBranch"), RadComboBox)
                For Each ComboItem As RadComboBoxItem In cbxBranch.Items
                    If (ComboItem.Value.Split("|")(0).ToString() <> e.Item.Cells(7).Text) Then
                        ComboItem.Visible = False
                    Else
                        ComboItem.Visible = True
                    End If
                Next
                cbxBranch.SelectedValue = CompBranchID
                cbxBranch.ToolTip = cbxBranch.SelectedItem.Text

                If rbnApp.SelectedItem.Value = 1 Then
                    Try
                        CType(e.Item.Cells(14).FindControl("btnApproveSelected"), RadButton).Visible = False
                        CType(e.Item.Cells(14).FindControl("lblAllApproved"), System.Web.UI.WebControls.Label).Visible = True
                        CType(e.Item.Cells(15).FindControl("btnUpdate"), RadButton).Visible = False
                    Catch ex As Exception

                    End Try
                End If
            End If
        End Sub
        Private Sub RadgvEmptyCustomerApproval_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvEmptyCustomerApproval.ItemCreated
            If e.Item.ItemType = Telerik.Web.UI.GridItemType.AlternatingItem Or e.Item.ItemType = Telerik.Web.UI.GridItemType.Item Then
                Dim dsCustomer As DataSet = clsTabMCustomers.GetTabMCustomersByCompanyID(0, objuserrole("employeeID"), objuserInfo("schemaName"))
                Dim cbxCustomer As RadComboBox = CType(e.Item.Cells(6).FindControl("cbxCustomer"), RadComboBox)
                cbxCustomer.DataSource = dsCustomer
                cbxCustomer.DataTextField = "CustCodeName"
                cbxCustomer.DataValueField = "CompCustID"

                Dim dsBranch As DataSet = clsTabMBranchPlant.GetTabMBranchPlantByCompanyID(0, objuserrole("employeeID"), objuserInfo("schemaName"))
                Dim cbxBranch As RadComboBox = CType(e.Item.Cells(9).FindControl("cbxBranch"), RadComboBox)
                cbxBranch.DataSource = dsBranch
                cbxBranch.DataTextField = "BranchCodeName"
                cbxBranch.DataValueField = "CompBranchID"
            End If
        End Sub

        'Private Sub RadgvEmptyCustomerApproval_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvEmptyCustomerApproval.ItemDataBound
        '    If TypeOf e.Item Is GridDataItem AndAlso e.Item.OwnerTableView.Name = "NestedChildItems" Then
        '        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        '        TryCast(item("chkApprove").FindControl("chkselect"), CheckBox).Attributes.Add("onclick", "return SetHeaderCheckBox(this, " + item.OwnerTableView.ParentItem.ItemIndex.ToString & ");")
        '    ElseIf TypeOf e.Item Is GridHeaderItem AndAlso e.Item.OwnerTableView.Name = "NestedChildItems" Then
        '        Dim header As GridHeaderItem = TryCast(e.Item, GridHeaderItem)
        '        TryCast(header("chkApprove").FindControl("checkAll"), CheckBox).Attributes.Add("onclick", "return SelectAllCheckBox(this, " + header.OwnerTableView.ParentItem.ItemIndex.ToString & ");")
        '    End If
        'End Sub

        Private Sub RadgvEmptyCustomerApproval_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvEmptyCustomerApproval.NeedDataSource
            radSearch.Enabled = False

            Dim itemCode As String = ""
            If txtItemCode.Text.Trim.Length > 0 And hdfItemCode.Value <> "" Then
                itemCode = hdfItemCode.Value
            Else
                itemCode = ""
            End If

            Dim Username As String = ""
            If rcbUserName.SelectedIndex > 0 Then
                Username = rcbUserName.SelectedItem.Text
            Else
                Username = ""
            End If

            Dim rbnapprovedvalue As Int32
            If rbnApp.SelectedItem.Value = 1 Then 'Approved
                rbnapprovedvalue = 1
            ElseIf rbnApp.SelectedItem.Value = 0 Then ' Not approved
                rbnapprovedvalue = 0
            ElseIf rbnApp.SelectedItem.Value = 2 Then 'All
                rbnapprovedvalue = 2
            End If
            Dim ds As DataSet
            'changed bby suresh issue no 2-on 19th april
            If rbnapprovedvalue <> 2 Then
                ds = clsTabKEmptyCustomer.GetAllTabKEmptyCustomerApprovalCRHHT(0, objuserrole("employeeID"), objuserInfo("schemaName"), itemCode, rbnapprovedvalue)
            Else
                ds = clsTabKEmptyCustomer.GetAllTabKEmptyCustomerApprovalHHT(0, objuserrole("employeeID"), objuserInfo("schemaName"), itemCode, "", rbnapprovedvalue)

            End If
            'end

            If Not ds Is Nothing Then
                ds.CaseSensitive = False
                Dim dv As DataView = ds.Tables(0).DefaultView
                Dim strQ As String = ""
                strQ = "1=1"
                If txtCustomer.Text <> "" And hdfCustomerID.Value.Length > 0 Then
                    strQ = strQ & " And FromCustomerID= " + hdfCustomerID.Value + ""
                End If

                If Not dpTransactionDate.DateInput.Text = "" Then
                    strQ = strQ & " And ReceiveDate= '" + String.Format("{0:MM/dd/yyyy}", dpTransactionDate.SelectedDate) + "'"
                End If
                If rcbUserName.SelectedIndex > 0 Then
                    strQ = strQ & " And UserName= '" + Username + "'"
                End If

                dv.RowFilter = strQ
                RadgvEmptyCustomerApproval.DataSource = dv
                'If rbnApp.SelectedItem.Value = 1 Then
                '    RadgvEmptyCustomerApproval.Columns(11).Visible = False
                'Else
                '    RadgvEmptyCustomerApproval.Columns(11).Visible = True
                'End If

            End If
            radSearch.Enabled = True
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
        Private Sub txtItemCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItemCode.TextChanged
            lblMsg.Text = ""
            hdfItemCode.Value = ""
            Dim itemcode As String
            If txtItemCode.Text <> "" Then
                itemcode = clsValidations.GetLookUpIDByEmployeeCode(txtItemCode.Text)
                If itemcode <> "" Then
                    hdfItemCode.Value = itemcode
                Else
                    lblMsg.Text = "Error while getting the Item Code. Please try again."
                    txtItemCode.Focus()
                End If
            Else
                txtItemCode.Text = ""
                hdfItemCode.Value = ""
            End If

        End Sub



        Private Sub radSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSearch.Click

            lblMsg.Text = ""
            RadgvEmptyCustomerApproval.Rebind()

        End Sub

        Private Sub radClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radClear.Click
            lblMsg.Text = ""
            txtItemCode.Text = ""
            txtCustomer.Text = ""
            txtItemCode.Text = ""
            hdfCustomerID.Value = ""
            hdfItemCode.Value = ""
            rcbUserName.SelectedIndex = 0
            dpTransactionDate.DbSelectedDate = ""
            RadgvEmptyCustomerApproval.Rebind()
        End Sub

    End Class
End Namespace
