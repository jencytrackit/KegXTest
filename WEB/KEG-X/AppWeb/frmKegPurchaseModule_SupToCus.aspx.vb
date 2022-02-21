'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmKegPurchaseModule_SuptoCus.aspx
'Created By         :
'File navigation    :
'Created Date       :November 08, 2013, 3:14:29 PM
'Description        :This file is used to List the Full Kegs Details that comes from the Supplier to Customer...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports Telerik.Web.UI
Namespace TrackITKTS
    Partial Class frmKegPurchaseModule_SupToCus
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim userPrivileges As New Hashtable
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            userPrivileges = Session("UserPrivileges") '// user Privileges maintained in the Session after logging
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
                'implementPrivilegesAdd()
            End If
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
                    'ImgBtn1.Visible = False
                    RadButton1.Visible = False
                End If
            End If
        End Sub
        Private Sub ObjFullKegReceive_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjFullKegReceive.Selecting
            'Pass the companyid and retrieve the full Kegs from Supplier to Customer for that company
            Dim objTabMCustomers = clsTabMCustomers.GetAllTabMCustomersByCustomerCode(objuserInfo("employeeName"), objuserInfo("schemaName"))
            If Not objTabMCustomers.CompanyID = 0 Then
                e.InputParameters("CompanyID") = objTabMCustomers.CompanyID
                e.InputParameters("SchemaName") = objuserInfo("schemaName")
                e.InputParameters("TransactionNumber") = ""
            End If
        End Sub

        Private Sub RadgvFullKegReceive_DetailTableDataBind(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadgvFullKegReceive.DetailTableDataBind
            Dim dataItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
            If e.DetailTableView.Name = "ChildItems" Then
                Dim objTabMCustomers = clsTabMCustomers.GetAllTabMCustomersByCustomerCode(objuserInfo("employeeName"), objuserInfo("schemaName"))
                Dim TransactionNumber As String = dataItem.GetDataKeyValue("TransactionNumber").ToString()
                e.DetailTableView.DataSource = clsTabFullKegSuppToCust.GetAllTabFullKegSuppToCust(objTabMCustomers.CompanyID, objuserInfo("schemaName"), TransactionNumber)
            End If
        End Sub

        Private Sub RadgvFullKegReceive_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadgvFullKegReceive.ItemCommand
            If e.CommandName = "edit" Then
                'Edit is selected from the grid. Call the edit aspx page by passing primary key value...
                Dim item = DirectCast(e.Item, GridDataItem)
                Dim TransactionNumber = item.GetDataKeyValue("TransactionNumber").ToString()
                Response.Redirect("frmAddEditKegPurchase_SupToCus.aspx?id=" + TransactionNumber)
            End If
            If e.CommandName = "delete" Then
                'Delete is selected from the grid. Call the BL Layer delete method by passing primary key value
                Dim TransNumber As String = e.CommandArgument.ToString()
                Dim returnValue As String = clsTabFullKegSuppToCust.DeleteTabFullKegSuppToCust(0, TransNumber, objuserInfo("schemaName"))
                If Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("0") Then
                    lblMsg.Text = Resources.lang.Deleted
                    RadgvFullKegReceive.DataBind()
                ElseIf Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("1") Then
                    lblMsg.Text = Resources.lang.childExists
                Else
                    lblMsg.Text = Resources.lang.deleteFail
                End If
            End If
            If e.CommandName = "deletechild" Then
                'Delete is selected from the grid. Call the BL Layer delete method by passing primary key value
                Dim FullKegID As String = e.CommandArgument.ToString()
                Dim num As Int32 = Convert.ToInt32(FullKegID)
                Dim returnValue As String = clsTabFullKegSuppToCust.DeleteTabFullKegSuppToCust(FullKegID, "", objuserInfo("schemaName"))
                If Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("0") Then
                    lblMsg.Text = Resources.lang.Deleted
                    RadgvFullKegReceive.DataBind()
                ElseIf Not String.IsNullOrEmpty(returnValue) And returnValue.Equals("1") Then
                    lblMsg.Text = Resources.lang.childExists
                Else
                    lblMsg.Text = Resources.lang.deleteFail
                End If
            End If
        End Sub

        Private Sub RadgvFullKegReceive_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvFullKegReceive.ItemDataBound
            If TypeOf e.Item Is GridDataItem Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                'implementPrivileges(CType(dataItem.FindControl("EditButton"), ImageButton), "E")
                'implementPrivileges(CType(dataItem.FindControl("DeleteButton"), ImageButton), "D")
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
                    'ImgBtn1.Visible = False
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
    End Class
End Namespace