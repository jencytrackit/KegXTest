'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmEmptyKegReturn_CusToSupplier.aspx
'Created By         :
'File navigation    :
'Created Date       :November 11, 2013, 10:41:28 AM
'Description        :This file is used to List the Empty Kegs Details that are returned from Customer to Supplier...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports Telerik.Web.UI

Namespace TrackITKTS
    Partial Class frmEmptyKegReturn_CusToSupplier
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
                AddContextKey()
            End If
        End Sub
        Private Sub AddContextKey()

            If objuserInfo.Count > 1 And objuserrole("employeeID") > 0 Then
                AutoCompleteExtender_txtCustomer.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
                AutoCompleteExtender_txtSupplier.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
                AutoCompleteExtender_txtItemCode.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
            Else
                'txtItemCode11_AutoCompleteExtender.ContextKey = ""
                AutoCompleteExtender_txtCustomer.ContextKey = ""
                AutoCompleteExtender_txtSupplier.ContextKey = ""
                AutoCompleteExtender_txtItemCode.ContextKey = ""
            End If
        End Sub
        'Private Sub Ob

        'Private Sub ObjEmptyKegsReturn_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjEmptyKegsReturn.Selecting
        '    'Pass the companyid and retrieve the Empty Kegs Retruned from Customer to Supplier for that company
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
                    'ImgBtn1.Visible = False
                    RadButton1.Visible = False
                End If
            End If
        End Sub

        Private Sub RadgvCusttoSupplier_DetailTableDataBind(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadgvCusttoSupplier.DetailTableDataBind
            Dim dataItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
            If e.DetailTableView.Name = "ChildItems" Then
                'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
                Dim CompanyID As Int32 = dataItem("CompanyID").Text
                Dim TransactionNumber As String = dataItem.GetDataKeyValue("TransactionNumber").ToString()
                If txtItemCode1.Text.Trim.Length > 0 Then
                    e.DetailTableView.DataSource = clsTabKEmptyCustToSupp.GetAllTabKEmptyCustToSuppByCompanyID_Search(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), TransactionNumber, hdfItemCode.Value)
                Else
                    e.DetailTableView.DataSource = clsTabKEmptyCustToSupp.GetAllTabKEmptyCustToSuppByCompanyID_Search(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), TransactionNumber, "")
                End If

            End If
        End Sub

        Private Sub RadgvCusttoSupplier_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadgvCusttoSupplier.ItemCommand
            If e.CommandName = "edit" Then
                'Edit is selected from the grid. Call the edit aspx page by passing primary key value...
                Dim item = DirectCast(e.Item, GridDataItem)
                Dim CompanyID = item("CompanyID").Text
                Dim TransactionNumber = item.GetDataKeyValue("TransactionNumber").ToString()
                Response.Redirect("frmAddEditEmptyKegReturn_CusToSupplier.aspx?id=" + TransactionNumber + "&cid=" + CompanyID)
            End If
            If e.CommandName = "delete" Then
                'Delete is selected from the grid. Call the BL Layer delete method by passing primary key value
                Dim TransNumber As String = e.CommandArgument.ToString()
                Dim returnValue As String = clsTabKEmptyCustToSupp.DeleteTabKEmptyCustToSupp(0, TransNumber, objuserInfo("schemaName"))
                If Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("0") Then
                    lblMsg.Text = Resources.lang.Deleted
                    RadgvCusttoSupplier.DataBind()
                ElseIf Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("1") Then
                    lblMsg.Text = Resources.lang.childExists
                Else
                    lblMsg.Text = Resources.lang.deleteFail
                End If

            End If
            If e.CommandName = "deletechild" Then
                'Delete is selected from the grid. Call the BL Layer delete method by passing primary key value
                Dim ECSOrderID As String = e.CommandArgument.ToString()
                Dim num As Int32 = Convert.ToInt32(ECSOrderID)
                Dim returnValue As String = clsTabKEmptyCustToSupp.DeleteTabKEmptyCustToSupp(ECSOrderID, "", objuserInfo("schemaName"))
                If Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("0") Then
                    lblMsg.Text = Resources.lang.Deleted
                    RadgvCusttoSupplier.DataBind()
                ElseIf Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("1") Then
                    lblMsg.Text = Resources.lang.childExists
                Else
                    lblMsg.Text = Resources.lang.deleteFail
                End If

            End If
        End Sub

        Private Sub RadgvCusttoSupplier_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvCusttoSupplier.ItemDataBound
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

       

      
        Private Sub RadgvCusttoSupplier_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvCusttoSupplier.NeedDataSource
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            'If Not strCompanyId = "" Then
            Dim EmployeeID As Integer = objuserrole("employeeID")
            Dim suppliercode As String = ""
            Dim itemCode As String = ""
            If txtItemCode1.Text.Trim.Length > 0 Then

                itemCode = clsValidations.GetLookUpIDByEmployeeCode(txtItemCode1.Text)
                hdfItemCode.Value = itemCode
            Else
                itemCode = ""
            End If
            If txtSupplier.Text.Trim.Length > 0 Then

                suppliercode = clsValidations.GetLookUpIDByEmployeeCode(txtSupplier.Text)
                hdfSupplierID.Value = suppliercode
            Else
                'itemCode = ""
            End If
            Dim ds As DataSet = clsTabKEmptyCustToSupp.GetAllTabKEmptyCustToSuppByCompanyID_Search(0, objuserrole("employeeID"), objuserInfo("schemaName"), "", Trim(itemCode))
            If Not ds Is Nothing Then
                ds.CaseSensitive = False
                Dim dv As DataView = ds.Tables(0).DefaultView
                Dim strQ As String
                strQ = "1=1" '"CompanyID=" & strCompanyId
                'If txtCompanyCode.Text.Trim.Length > 0 Then
                '    strQ = strQ & " And CompanyCode= '" + txtCompanyCode.Text.ToUpper + "'"
                'End If
                If txtTransactionId.Text.Trim.Length > 0 Then
                    strQ = strQ & " And TransactionNumber= '" + txtTransactionId.Text.ToUpper + "'"
                End If

                If Not dpReturnDate.DateInput.Text = "" Then
                    strQ = strQ & " And TransferDate= '" + String.Format("{0:MM/dd/yyyy}", dpReturnDate.SelectedDate) + "'"
                End If
                If hdfCustomerID.Value.Length > 0 Then
                    strQ = strQ & " And CustomerID= " + hdfCustomerID.Value + ""
                End If
                If hdfSupplierID.Value.Length > 0 Then
                    strQ = strQ & " And SupplierCode= " + hdfSupplierID.Value + ""
                End If
                dv.RowFilter = strQ
                RadgvCusttoSupplier.DataSource = dv
            End If
            ' End If
        End Sub

        Private Sub radSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSearch.Click
            lblMsg.Text = ""
            RadgvCusttoSupplier.Rebind()
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

        Protected Sub txtSupplier_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSupplier.TextChanged
            hdfSupplierID.Value = ""
            If Len(txtSupplier.Text) > 1 Then
                Dim SupplierCode As String

                SupplierCode = clsValidations.GetLookUpIDByEmployeeCode(txtSupplier.Text)


                If SupplierCode.Length > 0 Then
                    hdfSupplierID.Value = SupplierCode
                Else
                    '  lblMsg.Text = "Error while getting the Customer ID. Please try again."
                    txtSupplier.Focus()
                End If
            Else
                txtSupplier.Text = ""
                hdfSupplierID.Value = ""
            End If
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
        Private Sub radClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radClear.Click
            'txtCompanyCode.Text = ""
            'txtOrderNo.Text = ""
            'txtOrderType.Text = ""
            lblMsg.Text = ""
            txtCustomer.Text = ""
            txtItemCode1.Text = ""
            txtSupplier.Text = ""
            hdfSupplierID.Value = ""
            hdfCustomerID.Value = ""
            hdfItemCode.Value = ""
            hdfSupplierID.Value = ""
            txtTransactionId.Text = ""
            dpReturnDate.DbSelectedDate = ""
            RadgvCusttoSupplier.Rebind()

        End Sub
    End Class
End Namespace