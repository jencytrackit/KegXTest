'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmEmptyKegReturnList.aspx
'Created By         :
'File navigation    :
'Created Date       :November 08, 2013, 3:14:29 PM
'Description        :This file is used to List the Empty Kegs Details that comes from the Customer to BP...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports Telerik.Web.UI

Namespace TrackITKTS
    Partial Class frmEmptyKegReturnList
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
                AutoCompleteExtender_txtBranchplant.ContextKey = objuserrole("employeeID") & "," & objuserInfo("schemaName")
            Else
                'txtItemCode_AutoCompleteExtender.ContextKey = ""
                AutoCompleteExtender_txtBranchplant.ContextKey = ""
            End If
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
        Private Sub ObjEmptyKegsReceive_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjEmptyKegsReceive.Selecting
            'Pass the companyid and retrieve the Empty Kegs Retruned from Customer to BP for that company
            ' Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            ' If Not strCompanyId = "" Then
            'e.InputParameters("CompanyID") = strCompanyId
            'e.InputParameters("SchemaName") = objuserInfo("schemaName")
            'e.InputParameters("TransactionNumber") = ""
            'Else
            e.InputParameters("CompanyID") = 0
            e.InputParameters("CompanyID") = objuserInfo("schemaName")
            e.InputParameters("TransactionNumber") = ""
            ' End If
        End Sub
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

        Private Sub RadgvEmptyKegReturns_DetailTableDataBind(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadgvEmptyKegReturns.DetailTableDataBind
            Dim dataItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
            If e.DetailTableView.Name = "ChildItems" Then
                'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
                Dim CompanyID As Int32 = dataItem("CompanyID").Text
                Dim TransactionNumber As String = dataItem.GetDataKeyValue("TransactionNumber").ToString()
                If txtItemCode.Text.Trim.Length > 0 Then
                    e.DetailTableView.DataSource = clsTabKEmptyCustomer.GetAllTabKEmptyCustomerByCompanyID_Search(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), TransactionNumber, hdfItemCode.Value)
                Else
                    e.DetailTableView.DataSource = clsTabKEmptyCustomer.GetAllTabKEmptyCustomerByCompanyID_Search(CompanyID, objuserrole("employeeID"), objuserInfo("schemaName"), TransactionNumber, "")
                End If
            End If

        End Sub

        Private Sub RadgvEmptyKegReturns_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadgvEmptyKegReturns.ItemCommand
            If e.CommandName = "edit" Then
                'Edit is selected from the grid. Call the edit aspx page by passing primary key value...
                Dim item = DirectCast(e.Item, GridDataItem)
                Dim CompanyID = item("CompanyID").Text
                Dim TransactionNumber = item.GetDataKeyValue("TransactionNumber").ToString()
                Response.Redirect("frmAddEditEmptyKegReturn_CusToBP.aspx?id=" + TransactionNumber + "&cid=" + CompanyID)
            End If
            If e.CommandName = "delete" Then
                'Delete is selected from the grid. Call the BL Layer delete method by passing primary key value
                Dim TransNumber As String = e.CommandArgument.ToString()
                Dim returnValue As String = clsTabKEmptyCustomer.DeleteTabKEmptyCustomer(0, TransNumber, objuserInfo("schemaName"))
                If Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("0") Then
                    lblMsg.Text = Resources.lang.Deleted
                    RadgvEmptyKegReturns.DataBind()
                ElseIf Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("1") Then
                    lblMsg.Text = Resources.lang.childExists
                Else
                    lblMsg.Text = Resources.lang.deleteFail
                End If
            End If
            If e.CommandName = "deletechild" Then
                'Delete is selected from the grid. Call the BL Layer delete method by passing primary key value
                Dim EOrderID As String = e.CommandArgument.ToString()
                Dim num As Int32 = Convert.ToInt32(EOrderID)
                Dim returnValue As String = clsTabKEmptyCustomer.DeleteTabKEmptyCustomer(EOrderID, "", objuserInfo("schemaName"))
                If Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("0") Then
                    lblMsg.Text = Resources.lang.Deleted
                    RadgvEmptyKegReturns.DataBind()
                ElseIf Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("1") Then
                    lblMsg.Text = Resources.lang.childExists
                Else
                    lblMsg.Text = Resources.lang.deleteFail
                End If
            End If
        End Sub

        Private Sub RadgvEmptyKegReturns_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvEmptyKegReturns.ItemDataBound
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

        Private Sub RadgvEmptyKegReturns_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvEmptyKegReturns.NeedDataSource

            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            'If Not strCompanyId = "" Then
            Dim itemCode As String = ""
            If txtItemCode.Text.Trim.Length > 0 And hdfItemCode.Value <> "" Then
                itemCode = hdfItemCode.Value
            Else
                itemCode = ""
            End If

            Dim ds As DataSet = clsTabKEmptyCustomer.GetAllTabKEmptyCustomerByCompanyID_Search(0, objuserrole("employeeID"), objuserInfo("schemaName"), "", Trim(itemCode))
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

                'If Not dpCollectionDate.DateInput.Text = "" Then
                '    strQ = strQ & " And CollectionDate= '" + dpCollectionDate.SelectedDate + "'"
                'End If
                If Not dpCollectionDate.DateInput.Text = "" Then
                    strQ = strQ & " And CollectionDate= '" + String.Format("{0:MM/dd/yyyy}", dpCollectionDate.SelectedDate) + "'"
                End If

                If txtBranchplant.Text <> "" And hdfBranchID.Value.Length > 0 Then
                    strQ = strQ & " And BranchID= " + hdfBranchID.Value + ""
                End If
                If txtCustomer.Text <> "" And hdfCustomerID.Value.Length > 0 Then
                    strQ = strQ & " And CustomerID= " + hdfCustomerID.Value + ""
                End If
                dv.RowFilter = strQ
                RadgvEmptyKegReturns.DataSource = dv
            End If
            'End If

        End Sub
        Private Sub ObjDSCustomers_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjDSCustomers.Selecting
            'Pass the logged in employeeid and companyid and retrieve the Customers associated for that company and employee
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            'If Not strCompanyId = "" Then
            e.InputParameters("EmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")
            'End If
        End Sub

        Private Sub ObjBranchPlant_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjBranchPlant.Selecting
            'Pass the logged in employeeid and companyid and retrieve the BranchPlants associated for that company and employee
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            'If Not strCompanyId = "" Then
            e.InputParameters("EmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")
            'End If
        End Sub

        'Private Sub rcbCustomer_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbCustomer.DataBound
        '    rcbCustomer.Items.Insert(0, New RadComboBoxItem("--Select One--"))

        'End Sub

        'Private Sub rcbToBP_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbToBP.DataBound
        '    rcbToBP.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        'End Sub

        Private Sub radSearch_Click(sender As Object, e As System.EventArgs) Handles radSearch.Click
            lblMsg.Text = ""
            RadgvEmptyKegReturns.Rebind()
        End Sub

        Private Sub radClear_Click(sender As Object, e As System.EventArgs) Handles radClear.Click
            'txtCompanyCode.Text = ""
            'txtOrderNo.Text = ""
            lblMsg.Text = ""
            txtItemCode.Text = ""
            txtBranchplant.Text = ""
            txtCustomer.Text = ""
            txtItemCode.Text = ""
            hdfBranchID.Value = ""
            hdfCustomerID.Value = ""
            hdfItemCode.Value = ""
            txtTransactionId.Text = ""
            dpCollectionDate.DbSelectedDate = ""
            RadgvEmptyKegReturns.Rebind()
        End Sub
        Protected Sub txtCustomer_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCustomer.TextChanged

            hdfCustomerID.Value = ""
            If txtCustomer.Text <> "" Then
                Dim CustomerID As Integer
                CustomerID = clsValidations.GetLookupIDsbyEmpID(objuserrole("employeeID"), clsValidations.GetLookUpIDByEmployeeCode(txtCustomer.Text), "C", objuserInfo("schemaName"))

                If CustomerID > 0 Then
                    hdfCustomerID.Value = CustomerID
                Else
                    lblMsg.Text = "Error while getting the Customer ID. Please try again."
                    txtCustomer.Focus()
                End If
            Else
                txtCustomer.Text = ""
                hdfCustomerID.Value = ""
            End If
        End Sub

        Protected Sub txtBranchplant_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtBranchplant.TextChanged
            hdfBranchID.Value = ""
            If txtBranchplant.Text <> "" Then
                Dim BranchID As Integer
                BranchID = clsValidations.GetLookupIDsbyEmpID(objuserrole("employeeID"), clsValidations.GetLookUpIDByEmployeeCode(txtBranchplant.Text), "B", objuserInfo("schemaName"))

                If BranchID > 0 Then
                    hdfBranchID.Value = BranchID
                Else
                    lblMsg.Text = "Error while getting the BranchPlant ID. Please try again."
                    txtBranchplant.Focus()
                End If
            Else
                txtBranchplant.Text = ""
                hdfBranchID.Value = ""
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
