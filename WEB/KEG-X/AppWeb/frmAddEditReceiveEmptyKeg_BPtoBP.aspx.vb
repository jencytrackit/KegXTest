'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmAddEditReceiveEmptyKeg_BPtoBP.aspx
'Created By         :
'File navigation    :
'Created Date       :November 08, 2013, 5:53:09 PM 
'Description        :This file is used to Add / Edit the records...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmAddEditReceiveEmptyKeg_BPtoBP1
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim objuserrole As New Hashtable()
        Private mid As String = ""
        Private CID As Int32 = 0
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If Not Request.QueryString("id") Is Nothing And Not Request.QueryString("cid") Is Nothing Then
                mid = Request.QueryString("id")
                CID = Request.QueryString("cid")
            End If
            If objuserInfo Is Nothing Then
                Return
            End If
            If Not IsPostBack Then
                If Not Request.QueryString("id") Is Nothing And Not Request.QueryString("cid") Is Nothing Then
                    btnShow.Enabled = False
                    EditMode(mid, CID)
                Else
                    BindCompany()
                    btnShow.Enabled = True
                End If
                'rcbFromBP.Items.Insert(0, New RadComboBoxItem("--Select One--"))
                'rcbToBP.Items.Insert(0, New RadComboBoxItem("--Select One--"))
            End If
            AddContextKey()
        End Sub
        Private Sub AddContextKey()
            If objuserInfo.Count > 1 And rcbFromCmpny.SelectedIndex > 0 Then
                AutoCompleteExtender_txtFromBP.ContextKey = rcbFromCmpny.SelectedValue & "," & objuserInfo("schemaName")
            Else
                'txtItemCode_AutoCompleteExtender.ContextKey = ""
                AutoCompleteExtender_txtFromBP.ContextKey = ""
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

        End Sub
        Private Sub EditMode(ByVal mid As String, ByVal CID As Int32)
            BindGrid()
        End Sub
        'Private Sub ObjBranchPlant_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjBranchPlant.Selecting
        '    'Pass the logged in employeeid and companyid and retrieve the branch plants associated for that company and employee
        '    Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
        '    If Not strCompanyId = "" Then
        '        e.InputParameters("CompanyID") = strCompanyId
        '        e.InputParameters("EmployeeID") = objuserrole("employeeID")
        '        e.InputParameters("SchemaName") = objuserInfo("schemaName")
        '    End If
        'End Sub
        'Private Sub rcbToBP_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbToBP.DataBound
        '    rcbToBP.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        'End Sub

        Private Sub rcbTransactionNumber_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbTransactionNumber.DataBound
            rcbTransactionNumber.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        End Sub
        'Private Sub ObjEmptyKegsReceiveBP_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjEmptyKegsReceiveBP.Selecting
        '    'Pass the from branchplantid and to branchplantid and retrieve the empty kegs that are transferred to to-branchplant 

        '    If rcbFromBP.SelectedIndex > 0 And rcbToBP.SelectedIndex > 0 And rcbTransactionNumber.SelectedIndex > 0 Then
        '        e.InputParameters("FromBranchID") = rcbFromBP.SelectedValue
        '        e.InputParameters("ToBranchID") = rcbToBP.SelectedValue
        '        e.InputParameters("SchemaName") = objuserInfo("schemaName")
        '    End If

        'End Sub

        Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
            'Validate wheteher both From BP and To BP dropdowns are selected or not...
            If hdfFromBPID.Value.Length < 1 Then
                lblMsg.Text = "Please select the From BranchPlant"
                Exit Sub
            End If
           
            If hdfToBPID.Value.Length < 1 Then
                lblMsg.Text = "To Branch Plant Is Required"
                Exit Sub
            End If
            'Get empty kegs that are transferred to to-branchplant 
            BindGrid()
            RadgvEmptyKegReceive.DataBind()
        End Sub

        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            'save the records
            Dim ret As Boolean
            If RadgvEmptyKegReceive.Items.Count <= 0 Then
                lblMsg.Text = "Atleast one item is required"
                Exit Sub
            End If
            Dim str As String = ""
            'Validate whether the quantity changed is more 
            For Each item As GridDataItem In RadgvEmptyKegReceive.Items
                Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtgridqty"), TextBox)
                Dim lblPrevQty As Label = DirectCast(item.FindControl("lblPrevQty"), Label)
                Dim lblPrevQtyFirst As Label = DirectCast(item.FindControl("lblPrevQtyFirst"), Label)
                Dim lblTransferQty As Label = DirectCast(item.FindControl("lblTransferredQty"), Label)
                Dim ReceivedDate As RadDatePicker = DirectCast(item.FindControl("dpReceivedDate"), RadDatePicker)
                Dim strItemCode = item("ItemCode").Text
                If txtQuantity.Enabled = True Then
                    If Convert.ToInt32(txtQuantity.Text) + Convert.ToInt32(lblPrevQtyFirst.Text) > Convert.ToInt32(lblTransferQty.Text) Then
                        If str.Length <= 0 Then
                            str = str + "Quantity entered is greater than the transffered qty for the item codes : " + strItemCode
                            txtQuantity.Text = ""
                            ReceivedDate.DateInput.Text = ""
                            ReceivedDate.SelectedDate = Nothing

                        Else
                            str = str + "," + strItemCode
                        End If
                    End If
                End If
                
            Next
            If str.Length > 0 Then
                lblMsg.Text = str
                Exit Sub
            End If
            Dim cnt As Integer = 0
            'Validate whether the received date is entered or not 
            For Each item As GridDataItem In RadgvEmptyKegReceive.Items
                Dim ReceivedDate As RadDatePicker = DirectCast(item.FindControl("dpReceivedDate"), RadDatePicker)
                Dim chkselect As CheckBox = DirectCast(item.FindControl("chkselect"), CheckBox)
                Dim strItemCode = item("ItemCode").Text
                If chkselect.Enabled = True And chkselect.Checked = True Then

                    cnt = cnt + 1
                    If ReceivedDate.DateInput.Text = "" Then
                        If str.Length <= 0 Then
                            str = str + "Enter received date for the items : " + strItemCode
                            Exit For
                        Else
                            str = str + "," + strItemCode
                        End If
                    End If
                    'Else
                    ' str = str + "Please check atleast one item in the grid"
                End If

            Next
            If cnt = 0 Then
                lblMsg.Text = "Please check atleast one item in the grid"
                Exit Sub
            End If
            If str.Length > 0 Then
                lblMsg.Text = str
                Exit Sub
            End If

            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            Dim strCompanyId = CInt(rcbFromCmpny.SelectedValue)
            Dim Count As Integer = 0
            For Each item As GridDataItem In RadgvEmptyKegReceive.Items
                'Create new instance of the class to store the aspx controls data in DB...
                Dim chkselect As CheckBox = DirectCast(item.FindControl("chkselect"), CheckBox)
                If chkselect.Enabled = True And chkselect.Checked = True Then
                    Dim EBPOrderID As Label = DirectCast(item.FindControl("lblEBPOrderID"), Label)
                    Dim ReceivedDate As RadDatePicker = DirectCast(item.FindControl("dpReceivedDate"), RadDatePicker)
                    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtgridqty"), TextBox)
                    Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                    Dim lblPrevQty As Label = DirectCast(item.FindControl("lblPrevQty"), Label)
                    Dim lblPrevQtyFirst As Label = DirectCast(item.FindControl("lblPrevQtyFirst"), Label)
                    Dim lblTransferQty As Label = DirectCast(item.FindControl("lblTransferredQty"), Label)
                    Dim objTabKEmptyTransferBP As New PropertyTabKEmptyTransferBP
                    objTabKEmptyTransferBP.CompanyID = strCompanyId
                    objTabKEmptyTransferBP.EBPOrderID = EBPOrderID.Text
                    objTabKEmptyTransferBP.FromBranchID = hdfFromBPID.Value 'rcbFromBP.SelectedValue
                    objTabKEmptyTransferBP.ToBranchID = hdfToBPID.Value 'rcbToBP.SelectedValue
                    objTabKEmptyTransferBP.ItemID = itemid.Text
                    objTabKEmptyTransferBP.Quantity = Convert.ToInt32(txtQuantity.Text)
                    objTabKEmptyTransferBP.ReceiveDate = ReceivedDate.SelectedDate
                    objTabKEmptyTransferBP.ReceiveQty = txtQuantity.Text
                    objTabKEmptyTransferBP.ReceiveBy = objuserrole("employeeID")
                    objTabKEmptyTransferBP.ModifiedBy = objuserrole("employeeID")
                    If Convert.ToInt32(txtQuantity.Text) + Convert.ToInt32(lblPrevQtyFirst.Text) = Convert.ToInt32(lblTransferQty.Text) Then
                        objTabKEmptyTransferBP.Status = "Completed"
                    Else
                        objTabKEmptyTransferBP.Status = "Pending"
                    End If

                    'Record will be updated based on EBPOrderID
                    ret = clsTabKEmptyTransferBP.UpdateTabKEmptyReceiveBPtoBP_Edit(objTabKEmptyTransferBP, objuserInfo("schemaName"), Convert.ToInt32(lblPrevQty.Text))
                Else
                    ret = True
                End If
            Next
            'If Not mid = "" Then
            '    For Each item As GridDataItem In RadgvEmptyKegReceive.Items
            '        'Create new instance of the class to store the aspx controls data in DB...
            '        Dim chkselect As CheckBox = DirectCast(item.FindControl("chkselect"), CheckBox)
            '        If chkselect.Enabled = True And chkselect.Checked = True Then
            '            Dim EBPOrderID As Label = DirectCast(item.FindControl("lblEBPOrderID"), Label)
            '            Dim ReceivedDate As RadDatePicker = DirectCast(item.FindControl("dpReceivedDate"), RadDatePicker)
            '            Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtgridqty"), TextBox)
            '            Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
            '            Dim lblPrevQty As Label = DirectCast(item.FindControl("lblPrevQty"), Label)
            '            Dim objTabKEmptyTransferBP As New PropertyTabKEmptyTransferBP
            '            objTabKEmptyTransferBP.CompanyID = strCompanyId
            '            objTabKEmptyTransferBP.EBPOrderID = EBPOrderID.Text
            '            objTabKEmptyTransferBP.FromBranchID = rcbFromBP.SelectedValue
            '            objTabKEmptyTransferBP.ToBranchID = rcbToBP.SelectedValue
            '            objTabKEmptyTransferBP.ItemID = itemid.Text
            '            objTabKEmptyTransferBP.Quantity = Convert.ToInt32(txtQuantity.Text)
            '            objTabKEmptyTransferBP.ReceiveDate = ReceivedDate.SelectedDate
            '            objTabKEmptyTransferBP.ReceiveQty = txtQuantity.Text
            '            objTabKEmptyTransferBP.ReceiveBy = objuserrole("employeeID")
            '            objTabKEmptyTransferBP.ModifiedBy = objuserrole("employeeID")
            '            If chkselect.Checked = True Then
            '                objTabKEmptyTransferBP.Status = "Completed"
            '            Else
            '                objTabKEmptyTransferBP.Status = "Pending"
            '            End If

            '            'Record will be updated based on EBPOrderID
            '            ret = clsTabKEmptyTransferBP.UpdateTabKEmptyReceiveBPtoBP_Edit(objTabKEmptyTransferBP, objuserInfo("schemaName"), Convert.ToInt32(lblPrevQty.Text))
            '            Count = Count + 1
            '        End If
            '    Next
            '    If Count = 0 Then
            '        ret = True
            '    End If
            'Else

            'End If

            If (ret) And mid <> " " Then
                'Update mode and updation is success then redirecting it to List page
                lblMsg.Text = ""
                Response.Redirect("frmEmptyKegReceive_BPtoBP.aspx?process=Y&mode=E")
            ElseIf ret Then
                lblMsg.Text = ""
                'Update mode and creation is success then redirecting it to List page
                Response.Redirect("frmEmptyKegReceive_BPtoBP.aspx?process=Y&mode=A")
            Else
                lblMsg.Text = Resources.lang.msgError
            End If
        End Sub

        Private Sub RadgvEmptyKegReceive_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvEmptyKegReceive.ItemDataBound
            If TypeOf e.Item Is GridDataItem Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                Dim chkselect As CheckBox = DirectCast(dataItem.FindControl("chkselect"), CheckBox)
                Dim txtgridqty = DirectCast(dataItem.FindControl("txtgridqty"), TextBox)
                Dim dpReceivedDate As RadDatePicker = dataItem.FindControl("dpReceivedDate")
                Dim lblStatus = DirectCast(dataItem.FindControl("lblStatus"), Label)

                If lblStatus.Text.ToUpper() = "COMPLETED" Then
                    chkselect.Checked = True
                    chkselect.Enabled = False
                    txtgridqty.Enabled = False
                    dpReceivedDate.Enabled = False
                Else
                    chkselect.Checked = False
                    chkselect.Enabled = True
                    txtgridqty.Enabled = True
                    dpReceivedDate.Enabled = True
                End If
            End If
        End Sub

        'Private Sub RadgvEmptyKegReceive_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvEmptyKegReceive.NeedDataSource
        '    BindGrid()
        'End Sub

        Private Sub objTransactionNumber_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles objTransactionNumber.Selecting
            If rcbToCmpny.SelectedIndex <> 0 Then
                Dim strCompanyId = rcbFromCmpny.SelectedValue.ToString
                If strCompanyId <> "" And txtFromBP.Text <> "" And txtToBP.Text <> "" Then
                    e.InputParameters("valCompanyID") = strCompanyId
                    e.InputParameters("FromBranchID") = hdfFromBPID.Value
                    e.InputParameters("ToBranchID") = hdfToBPID.Value
                    e.InputParameters("SchemaName") = objuserInfo("schemaName")
                End If
            End If
        End Sub

        Private Sub rcbTransactionNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbTransactionNumber.SelectedIndexChanged
            If rcbTransactionNumber.SelectedIndex > 0 Then
                BindGrid()
            Else
                RadgvEmptyKegReceive.DataSource = ""
                RadgvEmptyKegReceive.DataBind()
            End If
        End Sub

        Private Sub BindGrid()
            If Not mid = "" And Not CID = 0 Then
                'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
                Dim strCompanyId = CID
                Dim ds = clsTabKEmptyTransferBP.GetAllTabKEmptyTransferBPByCompanyID(strCompanyId, objuserInfo("schemaName"), mid)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        BindCompany()
                        rcbFromCmpny.SelectedValue = ds.Tables(0).Rows(0)("CompanyID")
                        'rcbFromCmpny_SelectedIndexChanged(Nothing, Nothing)
                        hdfFromBPID.Value = ds.Tables(0).Rows(0)("FromBranchID")
                        txtFromBP.Text = ds.Tables(0).Rows(0)("FromBranchName")
                        txtFromBP.Enabled = False
                        'rcbFromBP.SelectedValue = ds.Tables(0).Rows(0)("FromBranchID")
                        rcbToCmpny.SelectedValue = ds.Tables(0).Rows(0)("ToCompanyID")
                        ' rcbToCmpny_SelectedIndexChanged(Nothing, Nothing)
                        hdfToBPID.Value = ds.Tables(0).Rows(0)("ToBranchID")
                        txtToBP.Text = ds.Tables(0).Rows(0)("ToBranchName")
                        txtToBP.Enabled = False
                        rcbTransactionNumber.SelectedValue = ds.Tables(0).Rows(0)("TransactionNumber")
                        'rcbFromBP.Enabled = False
                        'rcbToBP.Enabled = False
                        rcbFromCmpny.Enabled = False
                        rcbToCmpny.Enabled = False
                        rcbTransactionNumber.Enabled = False
                        RadgvEmptyKegReceive.DataSource = ds
                        RadgvEmptyKegReceive.DataBind()
                    Else
                        RadgvEmptyKegReceive.DataSource = ""
                        RadgvEmptyKegReceive.DataBind()
                    End If
                Else
                    RadgvEmptyKegReceive.DataSource = ""
                    RadgvEmptyKegReceive.DataBind()
                End If
            Else
                If txtFromBP.Text <> "" And txtToBP.Text <> "" And rcbTransactionNumber.SelectedIndex > 0 Then
                    Dim ds = clsTabKEmptyTransferBP.GetTabKEmptyTransferBPByFromToBranchID(hdfFromBPID.Value, hdfToBPID.Value, objuserInfo("schemaName"), rcbTransactionNumber.SelectedItem.Text)
                    If Not ds Is Nothing Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            RadgvEmptyKegReceive.DataSource = ds
                            RadgvEmptyKegReceive.DataBind()
                        Else
                            RadgvEmptyKegReceive.DataSource = ""
                            RadgvEmptyKegReceive.DataBind()
                        End If
                    Else
                        RadgvEmptyKegReceive.DataSource = ""
                        RadgvEmptyKegReceive.DataBind()
                    End If
                Else
                    RadgvEmptyKegReceive.DataSource = ""
                    RadgvEmptyKegReceive.DataBind()
                End If

            End If
        End Sub
        Private Sub rcbFromCmpny_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbFromCmpny.SelectedIndexChanged
            lblMsg.Text = ""
            AddContextKey()
            hdfFromBPID.Value = ""
            txtFromBP.Text = ""
            rcbTransactionNumber.Items.Clear()
            rcbTransactionNumber.Items.Insert(0, New RadComboBoxItem("--Select One--"))
            RadgvEmptyKegReceive.DataSource = ""
            RadgvEmptyKegReceive.DataBind()
            If rcbFromCmpny.SelectedIndex > 0 Then
                txtFromBP.Focus()
            End If
        End Sub

        Private Sub rcbToCmpny_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbToCmpny.SelectedIndexChanged
            lblMsg.Text = ""
            AddContextKey()
            hdfToBPID.Value = ""
            txtToBP.Text = ""
            rcbTransactionNumber.Items.Clear()
            rcbTransactionNumber.Items.Insert(0, New RadComboBoxItem("--Select One--"))
            RadgvEmptyKegReceive.DataSource = ""
            RadgvEmptyKegReceive.DataBind()
            If rcbToCmpny.SelectedIndex > 0 Then
                txtToBP.Focus()
            End If
        End Sub

        Private Sub txtFromBP_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFromBP.TextChanged
            lblMsg.Text = ""
            hdfFromBPID.Value = ""
            If rcbFromCmpny.SelectedIndex > 0 Then
                Dim FromBPID As Integer
                FromBPID = clsValidations.GetLookUpIDByCode(rcbFromCmpny.SelectedValue, txtFromBP.Text, "B", objuserInfo("schemaName"))

                If FromBPID > 0 Then
                    hdfFromBPID.Value = FromBPID
                    rcbTransactionNumber.DataBind()
                Else
                    lblMsg.Text = "Error while getting the FromBranchPlant ID. Please try again."
                    txtFromBP.Focus()
                End If
            Else
                txtFromBP.Text = ""
                hdfFromBPID.Value = ""
            End If
        End Sub

        Private Sub txtToBP_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtToBP.TextChanged
            lblMsg.Text = ""
            hdfToBPID.Value = ""
            If rcbToCmpny.SelectedIndex > 0 Then
                Dim ToBPID As Integer
                ToBPID = clsValidations.GetLookUpIDByCode(rcbToCmpny.SelectedValue, txtToBP.Text, "B", objuserInfo("schemaName"))

                If ToBPID > 0 Then
                    hdfToBPID.Value = ToBPID
                    rcbTransactionNumber.DataBind()
                    RadgvEmptyKegReceive.DataSource = ""
                    RadgvEmptyKegReceive.DataBind()
                Else
                    lblMsg.Text = "Error while getting the ToBranchPlant ID. Please try again."
                    txtToBP.Focus()
                End If
            Else
                txtToBP.Text = ""
                hdfToBPID.Value = ""
            End If
        End Sub
    End Class
End Namespace
