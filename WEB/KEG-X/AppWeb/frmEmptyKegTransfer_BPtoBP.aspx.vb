'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmEmptyKegTransfer_BPtoBP.aspx
'Created By         :
'File navigation    :
'Created Date       :November 08, 2013, 5:33:15 PM
'Description        :This file is used to List the Empty Kegs Details that are transferred from BP to BP...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports Telerik.Web.UI

Namespace TrackITKTS
    Partial Class frmEmptyKegTransfer_BPtoBP
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim userPrivileges As New Hashtable
        Dim objuserrole As New Hashtable()

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
                AutoCompleteExtender_txtfromBranchplant.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
                AutoCompleteExtender_txtToBranchplant.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
                AutoCompleteExtender_txtItemCode.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
            Else
                'txtItemCode11_AutoCompleteExtender.ContextKey = ""
                AutoCompleteExtender_txtfromBranchplant.ContextKey = ""
                AutoCompleteExtender_txtToBranchplant.ContextKey = ""
                AutoCompleteExtender_txtItemCode.ContextKey = ""
            End If
        End Sub

        'Private Sub ObjEmptyKegsTransferBP_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjEmptyKegsTransferBP.Selecting
        '    'Pass the companyid and retrieve the Empty Kegs Transferred from BP to BP for that company
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
                    ' ImgBtn1.Visible = False
                    RadButton1.Visible = False
                End If
            End If
        End Sub

        Private Sub RadgvEmptyKegTransfer_DetailTableDataBind(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadgvEmptyKegTransfer.DetailTableDataBind
            Dim dataItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
            If e.DetailTableView.Name = "ChildItems" Then
                'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
                Dim CompanyID As Int32 = dataItem("CompanyID").Text
                Dim TransactionNumber As String = dataItem.GetDataKeyValue("TransactionNumber").ToString()
                If txtItemCode1.Text.Trim.Length > 0 Then
                    e.DetailTableView.DataSource = clsTabKEmptyTransferBP.GetAllEmptyKegReceiveBPtoBP_Search(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), TransactionNumber, hdfItemCode.Value, rtbStatus.Text)
                Else
                    e.DetailTableView.DataSource = clsTabKEmptyTransferBP.GetAllEmptyKegReceiveBPtoBP_Search(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), TransactionNumber, "", rtbStatus.Text)
                End If


                'e.DetailTableView.DataSource = clsTabKEmptyTransferBP.GetAllEmptyKegReceiveBPtoBP_Search(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), TransactionNumber, hdfItemCode.Value, rtbStatus.Text)
              
            End If

        End Sub

        Private Sub RadgvEmptyKegTransfer_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadgvEmptyKegTransfer.ItemCommand

            If e.CommandName = "edit" Then
                'Edit is selected from the grid. Call the edit aspx page by passing primary key value...
                Dim item = DirectCast(e.Item, GridDataItem)
                Dim CompanyID = item("CompanyID").Text
                Dim TransactionNumber = item.GetDataKeyValue("TransactionNumber").ToString()
                Response.Redirect("frmAddEditTransferEmptyKeg_BPtoBP.aspx?id=" + TransactionNumber + "&cid=" + CompanyID)
            End If
            If e.CommandName = "delete" Then
                'Delete is selected from the grid. Call the BL Layer delete method by passing primary key value
                'Dim TransNumber As String = e.CommandArgument.ToString()
                Dim pid As String = e.CommandArgument.ToString()
                'Dim returnValue As String = clsTabKEmptyTransferBP.DeleteTabKEmptyTransferBP(pid, "", objuserInfo("schemaName"))
                Dim returnValue As String = clsTabKEmptyTransferBP.DeleteTabKEmptyTransferBP(0, pid, objuserInfo("schemaName"))
                If Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("0") Then
                    lblMsg.Text = Resources.lang.Deleted
                ElseIf Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("1") Then
                    lblMsg.Text = Resources.lang.childExists
                Else
                    lblMsg.Text = Resources.lang.deleteFail
                End If
            End If
            If e.CommandName = "deletechild" Then
                'Delete is selected from the grid. Call the BL Layer delete method by passing primary key value
                Dim ESOrderID As String = e.CommandArgument.ToString()
                Dim num As Int32 = Convert.ToInt32(ESOrderID)
                Dim returnValue As String = clsTabKEmptyTransferBP.DeleteTabKEmptyTransferBP(ESOrderID, "", objuserInfo("schemaName"))
                If Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("0") Then
                    lblMsg.Text = Resources.lang.Deleted
                    RadgvEmptyKegTransfer.DataBind()
                ElseIf Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("1") Then
                    lblMsg.Text = Resources.lang.childExists
                Else
                    lblMsg.Text = Resources.lang.deleteFail
                End If
            End If
        End Sub

        Private Sub RadgvEmptyKegTransfer_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvEmptyKegTransfer.ItemDataBound
            If TypeOf e.Item Is GridDataItem Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                implementPrivileges(CType(dataItem.FindControl("EditButton"), ImageButton), "E")
                implementPrivileges(CType(dataItem.FindControl("DeleteButton"), ImageButton), "D")
            End If
        End Sub
        Private Sub implementPrivileges(ByVal showCell As ImageButton, ByVal type As String)
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
                    ' ImgBtn1.Visible = False
                    RadButton1.Visible = False
                End If
                If Not showCell Is Nothing Then
                    If privileges(2).Equals("0") And type.Equals("E") Then
                        showCell.Visible = False
                    ElseIf privileges(3).Equals("0") And type.Equals("D") Then
                        showCell.Visible = False
                    End If
                End If
            End If
        End Sub

     

        Private Sub RadgvEmptyKegTransfer_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvEmptyKegTransfer.NeedDataSource
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            'If Not strCompanyId = "" Then
            Dim EmployeeID As Integer = objuserrole("employeeID")
            Dim itemCode As String = ""
            If txtItemCode1.Text.Trim.Length > 0 Then

                itemCode = clsValidations.GetLookUpIDByEmployeeCode(txtItemCode1.Text)
                hdfItemCode.Value = itemCode
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
                strQ = "EmployeeID=" & EmployeeID
                'If txtCompanyCode.Text.Trim.Length > 0 Then
                '    strQ = strQ & " And CompanyCode= '" + txtCompanyCode.Text.ToUpper + "'"
                'End If
                If hdffromBranchID.Value.Length > 0 Then
                    strQ = strQ & " And FromBranchID= " + hdffromBranchID.Value + ""
                End If
                If hdfToBranchID.Value.Length > 0 Then
                    strQ = strQ & " And ToBranchID= " + hdfToBranchID.Value + ""
                End If
                If txtTransactionId.Text.Trim.Length > 0 Then
                    strQ = strQ & " And TransactionNumber= '" + txtTransactionId.Text.ToUpper + "'"
                End If
                'If txtItemCode.Text.Trim.Length > 0 Then
                '    strQ = strQ & " And ItemCode= '" + txtItemCode.Text.ToUpper + "'"
                'End If
                If Not dpTransferDate.DateInput.Text = "" Then
                    strQ = strQ & " And TransferDate= '" + String.Format("{0:MM/dd/yyyy}", dpTransferDate.SelectedDate) + "'"
                End If

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
            'txtCompanyCode.Text = ""
            lblMsg.Text = ""
            txtfromBranchplant.Text = ""
            txtItemCode1.Text = ""
            txtToBranchplant.Text = ""
            hdffromBranchID.Value = ""
            hdfItemCode.Value = ""
            hdfToBranchID.Value = ""
            dpTransferDate.DbSelectedDate = ""
            txtTransactionId.Text = ""
            rtbStatus.Text = ""
            RadgvEmptyKegTransfer.Rebind()
        End Sub
        
        Protected Sub txtToBranchplant_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtToBranchplant.TextChanged
            hdfToBranchID.Value = ""
            If Len(txtToBranchplant.Text) > 1 Then
                Dim BranchID As Integer
                BranchID = clsValidations.GetLookupIDsbyEmpID(objuserrole("employeeID"), clsValidations.GetLookUpIDByEmployeeCode(txtToBranchplant.Text), "B", objuserInfo("schemaName"))

                If BranchID > 0 Then
                    hdfToBranchID.Value = BranchID
                Else
                    '  lblMsg.Text = "Error while getting the Customer ID. Please try again."
                    txtToBranchplant.Focus()
                End If
            Else
                txtToBranchplant.Text = ""
                hdfToBranchID.Value = ""
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
                    txtfromBranchplant.Focus()
                End If
            Else
                txtItemCode1.Text = ""
                hdfItemCode.Value = ""
            End If
        End Sub

        Protected Sub txtfromBranchplant_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtfromBranchplant.TextChanged
            hdffromBranchID.Value = ""
            If Len(txtfromBranchplant.Text) > 1 Then
                Dim BranchID As Integer
                BranchID = clsValidations.GetLookupIDsbyEmpID(objuserrole("employeeID"), clsValidations.GetLookUpIDByEmployeeCode(txtfromBranchplant.Text), "B", objuserInfo("schemaName"))

                If BranchID > 0 Then
                    hdffromBranchID.Value = BranchID
                Else
                    '  lblMsg.Text = "Error while getting the Customer ID. Please try again."
                    txtfromBranchplant.Focus()
                End If
            Else
                txtfromBranchplant.Text = ""
                hdffromBranchID.Value = ""
            End If
        End Sub
    End Class
End Namespace
