'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmFullKegReceiveList_FromSupplier.aspx
'Created By         :
'File navigation    :
'Created Date       :November 08, 2013, 5:33:36 PM
'Description        :This file is used to List the Full Kegs Details that comes from the Supplier...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports Telerik.Web.UI
Namespace TrackITKTS
    Partial Class frmEmptyKegReceive_BPtoBP
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim objuserrole As New Hashtable()
        Dim userPrivileges As New Hashtable

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            userPrivileges = Session("UserPrivileges") '// user Privileges maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
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

                implementPrivilegesAdd()
            End If
            AddContextKey()
        End Sub
        Private Sub AddContextKey()
            If objuserInfo.Count > 1 And objuserrole("employeeID") > 0 Then
                AutoCompleteExtender_txtFromBP.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
            Else
                'txtItemCode_AutoCompleteExtender.ContextKey = ""
                AutoCompleteExtender_txtFromBP.ContextKey = ""
            End If
            If objuserInfo.Count > 1 And objuserrole("employeeID") > 0 Then
                AutoCompleteExtender_txtToBP.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
            Else
                AutoCompleteExtender_txtToBP.ContextKey = ""
            End If
            If objuserInfo.Count > 1 And objuserrole("employeeID") > 0 Then
                AutoCompleteExtender_txtItemCode.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
            Else
                AutoCompleteExtender_txtItemCode.ContextKey = ""
            End If
        End Sub

        'Private Sub ObjEmptyKegsReceiveBP_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjEmptyKegsReceiveBP.Selecting
        '    'Pass the companyid and retrieve the Empty Kegs Received from BP for that company
        '    Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
        '    If Not strCompanyId = "" Then
        '        e.InputParameters("CompanyID") = strCompanyId
        '        e.InputParameters("SchemaName") = objuserInfo("schemaName")
        '        e.InputParameters("TransactionNumber") = ""

        '    Else
        '        e.InputParameters("CompanyID") = 0
        '        e.InputParameters("SchemaName") = objuserInfo("schemaName")
        '        e.InputParameters("TransactionNumber") = ""
        '    End If
        'End Sub
        Private Sub implementPrivilegesAdd()
            'This method is called to display the add new record button based on the logged in user privileges
            Dim urlOrg As String = Request.Url.ToString()
            Dim pos As Int32 = urlOrg.LastIndexOf("/")
            urlOrg = urlOrg.Substring(pos, urlOrg.Length - pos)
            pos = urlOrg.IndexOf("?")
            If pos > 0 Then
                urlOrg = urlOrg.Substring(0, pos)
            End If
            urlOrg = System.Configuration.ConfigurationManager.AppSettings("ApplicationURL") & urlOrg

            If userPrivileges.Contains(urlOrg) Then
                Dim priv As String = userPrivileges(urlOrg)
                Dim privileges() As String = priv.Split(",")
                If privileges(1).Equals("0") Then
                    '  ImgBtn1.Visible = False
                    RadButton1.Visible = False
                End If
            End If
        End Sub

        Private Sub RadgvEmptyKegTransfer_DetailTableDataBind(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadgvEmptyKegTransfer.DetailTableDataBind
            Dim dataItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
            If e.DetailTableView.Name = "ChildItems" Then
                Dim CompanyID As Int32 = dataItem("CompanyID").Text
                Dim TransactionNumber As String = dataItem.GetDataKeyValue("TransactionNumber").ToString()
                e.DetailTableView.DataSource = clsTabKEmptyTransferBP.GetAllEmptyKegReceiveBPtoBP_Search(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), TransactionNumber, hdfItemCode.Value, rtbStatus.Text)
                'If rtbItemCode.Text.Trim.Length > 0 And rtbStatus.Text.Trim.Length > 0 Then
                '    e.DetailTableView.DataSource = clsTabKEmptyTransferBP.GetAllEmptyKegReceiveBPtoBP_Search(strCompanyId, objuserInfo("schemaName"), TransactionNumber, rtbItemCode.Text, rtbStatus.Text)
                'ElseIf rtbItemCode.Text.Trim.Length = 0 And rtbStatus.Text.Trim.Length = 0 Then
                '    e.DetailTableView.DataSource = clsTabKEmptyTransferBP.GetAllEmptyKegReceiveBPtoBP_Search(strCompanyId, objuserInfo("schemaName"), TransactionNumber, "", "")
                'ElseIf rtbItemCode.Text.Trim.Length  0 And rtbStatus.Text.Trim.Length = 0 Then

            End If

        End Sub


        Private Sub RadgvEmptyKegTransfer_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadgvEmptyKegTransfer.ItemCommand
            If e.CommandName = "edit" Then
                'Edit is selected from the grid. Call the edit aspx page by passing primary key value...
                Dim item = DirectCast(e.Item, GridDataItem)
                Dim CompanyID = item("CompanyID").Text
                Dim TransactionNumber = item.GetDataKeyValue("TransactionNumber").ToString()
                Response.Redirect("frmAddEditReceiveEmptyKeg_BPtoBP.aspx?id=" + TransactionNumber + "&cid=" + CompanyID)
            End If
        End Sub

        Private Sub ObjBranchPlant_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjBranchPlant.Selecting
            'Pass the logged in employeeid and companyid and retrieve the BranchPlants associated for that company and employee
            ' Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            'If Not strCompanyId = "" Then
            e.InputParameters("EmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")
            'End If
        End Sub

        'Private Sub rcbFromBP_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbFromBP.DataBound
        '    rcbFromBP.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        'End Sub

        'Private Sub rcbToBP_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbToBP.DataBound
        '    rcbToBP.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        'End Sub

        Private Sub RadgvEmptyKegTransfer_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvEmptyKegTransfer.NeedDataSource
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            'If Not strCompanyId = "" Then
            Dim itemCode As String = ""
            If txtItemCode.Text.Trim.Length > 0 And hdfItemCode.Value <> "" Then
                itemCode = hdfItemCode.Value
            Else
                itemCode = ""
            End If

            Dim Status As String = ""
            If rtbStatus.Text.Trim.Length > 0 Then
                Status = rtbStatus.Text
            Else
                Status = ""
            End If
            Dim ds As DataSet = clsTabKEmptyTransferBP.GetAllEmptyKegReceiveBPtoBP_Search(0, objuserrole("employeeID"), objuserInfo("schemaName"), "", Trim(itemCode), Status)
            If Not ds Is Nothing Then
                ds.CaseSensitive = False
                Dim dv As DataView = ds.Tables(0).DefaultView
                Dim strQ As String
                strQ = "1=1" '& strCompanyId
                'If rtbCompanyCode.Text.Trim.Length > 0 Then
                '    strQ = strQ & " And CompanyCode= '" + rtbCompanyCode.Text.ToUpper + "'"
                'End If
                If txtFromBP.Text <> "" And hdfFromBPID.Value.Length > 0 Then
                    strQ = strQ & " And FromBranchID= " + hdfFromBPID.Value + ""
                End If
                If txtToBP.Text <> "" And hdfToBPID.Value.Length > 0 Then
                    strQ = strQ & " And ToBranchID= " + hdfToBPID.Value + ""
                End If
                If rtbTransactionId.Text.Trim.Length > 0 Then
                    strQ = strQ & " And TransactionNumber= '" + rtbTransactionId.Text.ToUpper + "'"
                End If
                'If rtbItemCode.Text.Trim.Length > 0 Then
                '    strQ = strQ & " And ItemCode= '" + rtbItemCode.Text.ToUpper + "'"
                'End If
                'If rtbStatus.Text.Trim.Length > 0 Then
                '    strQ = strQ & " And Status= '" + rtbStatus.Text.ToUpper + "'"
                'End If

                dv.RowFilter = strQ
                RadgvEmptyKegTransfer.DataSource = dv


            End If
            'End If
        End Sub

        Private Sub radSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSearch.Click
            lblMsg.Text = ""
            RadgvEmptyKegTransfer.Rebind()
        End Sub

        Private Sub radClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radClear.Click
            ' rtbCompanyCode.Text = ""
            'rcbFromBP.SelectedIndex = 0
            'rcbToBP.SelectedIndex = 0
            'rtbItemCode.Text = ""
            lblMsg.Text = ""
            rtbStatus.Text = ""
            rtbTransactionId.Text = ""
            txtItemCode.Text = ""
            txtFromBP.Text = ""
            txtToBP.Text = ""
            hdfFromBPID.Value = ""
            hdfItemCode.Value = ""
            hdfToBPID.Value = ""
            RadgvEmptyKegTransfer.Rebind()
        End Sub

        Private Sub txtFromBP_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFromBP.TextChanged
            lblMsg.Text = ""
            hdfFromBPID.Value = ""
            Dim FromBPID As Integer
            If txtFromBP.Text <> "" Then
                FromBPID = clsValidations.GetLookupIDsbyEmpID(objuserrole("employeeID"), clsValidations.GetLookUpIDByEmployeeCode(txtFromBP.Text), "B", objuserInfo("schemaName"))

                If FromBPID > 0 Then
                    hdfFromBPID.Value = FromBPID
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
            Dim ToBPID As Integer
            If txtToBP.Text <> "" Then
                ToBPID = clsValidations.GetLookupIDsbyEmpID(objuserrole("employeeID"), clsValidations.GetLookUpIDByEmployeeCode(txtToBP.Text), "B", objuserInfo("schemaName"))

                If ToBPID > 0 Then
                    hdfToBPID.Value = ToBPID
                Else
                    lblMsg.Text = "Error while getting the ToBranchPlant ID. Please try again."
                    txtToBP.Focus()
                End If
            Else
                txtToBP.Text = ""
                hdfToBPID.Value = ""
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
    End Class
End Namespace
