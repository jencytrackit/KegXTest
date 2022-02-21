Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Public Class frmAddEditKegPurchase_SupToCus
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging
        Dim objuserrole As New Hashtable()
        Dim dt As New DataTable
        Private mid As String = ""
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

                If Not Request.QueryString("id") Is Nothing Then
                    EditMode(mid)
                End If
            End If
        End Sub
        Private Sub EditMode(ByVal mid As String)
            RadgvFullKegReceive.DataBind()
        End Sub
        Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
            If Not IsPostBack Then
                Dim objTabMCustomers = clsTabMCustomers.GetAllTabMCustomersByCustomerCode(objuserInfo("employeeName"), objuserInfo("schemaName"))
                BindSuppliers(objTabMCustomers.CompanyID)
                BindCustomers(objTabMCustomers.CompanyID, objTabMCustomers.CustomerID)
            End If
        End Sub
        Private Sub BindSuppliers(ByVal CompanyId As Integer) 'Filling Branchplant dropdown details  fore each radio button value
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            rcbSupplier.DataSource = clsTabMSuppliers.GetTabMSuppliersByCompanyID(CompanyId, 0, objuserInfo("schemaName"))
            rcbSupplier.DataTextField = "SuppCodeName"
            rcbSupplier.DataValueField = "SupplierID"
            rcbSupplier.DataBind()
            rcbSupplier.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        End Sub
        Private Sub BindCustomers(ByVal CompanyId As Integer, ByVal CustomerId As Integer) 'Filling Branchplant dropdown details  fore each radio button value
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            rcbToCustomer.DataSource = clsTabMCustomers.GetTabMCustomersByCompanyID(CompanyId, 0, objuserInfo("schemaName"))
            rcbToCustomer.DataTextField = "CustCodeName"
            rcbToCustomer.DataValueField = "CustomerID"
            rcbToCustomer.DataBind()
            rcbToCustomer.Items.Insert(0, New RadComboBoxItem("--Select One--"))
            rcbToCustomer.SelectedValue = CustomerId
            rcbToCustomer.Enabled = False
        End Sub

        Private Sub txtItemCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItemCode.TextChanged
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            Dim objTabMCustomers = clsTabMCustomers.GetAllTabMCustomersByCustomerCode(objuserInfo("employeeName"), objuserInfo("schemaName"))
            If Trim(txtItemCode.Text) <> "" Then
                Dim ds As DataSet = clsTabUOMMaster.GetTabUOMMasterByItemCode(Trim(txtItemCode.Text), objuserInfo("schemaName"), objTabMCustomers.CompanyID) 'Get the item details by Item code
                If ds Is Nothing Then
                    ClearFields()
                    Exit Sub
                End If
                If ds.Tables(0).Rows.Count > 0 Then
                    txtItemDesc.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName1"))
                    txtItemName2.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName2"))
                    txtItemID.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemID"))
                    txtUOM.Text = Convert.ToString(ds.Tables(0).Rows(0)("UOM"))

                Else
                    ClearFields()
                End If
            Else
                ClearFields()
            End If
        End Sub
        Private Sub ClearFields()
            'This Method is called to clear the controls related to items
            txtItemDesc.Text = ""
            txtItemName2.Text = ""
            txtItemID.Text = ""
            txtUOM.Text = ""
            txtQuantity.Text = ""
           
        End Sub

        Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            BindData()
            ClearFields()
            txtItemCode.Text = ""
        End Sub

        Private Sub BindData()
            'Creating new datatable and inserting values into it
            dt.Columns.Add("ItemID", GetType(String))
            dt.Columns.Add("ItemCode", GetType(String))
            dt.Columns.Add("ItemName1", GetType(String))
            dt.Columns.Add("ItemName2", GetType(String))
            dt.Columns.Add("UOM", GetType(String))
            dt.Columns.Add("Quantity", GetType(String))

            dt.Columns.Add("FullKegID", GetType(String))

            If ViewState("table") IsNot Nothing Then
                dt = DirectCast(ViewState("table"), DataTable)
            End If
            Dim row1 As DataRow = dt.NewRow()
            row1("ItemID") = txtItemID.Text
            row1("ItemCode") = txtItemCode.Text
            row1("ItemName1") = txtItemDesc.Text
            row1("ItemName2") = txtItemName2.Text
            row1("UOM") = txtUOM.Text

            row1("Quantity") = txtQuantity.Text
            row1("FullKegID") = "0"
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
            RadgvFullKegReceive.DataSource = dt
            RadgvFullKegReceive.DataBind()
        End Sub

        
        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click
            
            Dim ret As Boolean
            Dim objTabMCustomers = clsTabMCustomers.GetAllTabMCustomersByCustomerCode(objuserInfo("employeeName"), objuserInfo("schemaName"))

            'Validate grid has atleast one record 
            If RadgvFullKegReceive.Items.Count <= 0 Then
                lblMsg.Text = "Add atleast one item"
                lblMsg.Visible = True
                Exit Sub
            End If
            If Not mid = "" Then
                For Each item As GridDataItem In RadgvFullKegReceive.Items
                    'Create new instance of the class to store the aspx controls data...
                    Dim lblFullKegID As Label = DirectCast(item.FindControl("lblFullKegID"), Label)
                    Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)

                    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                    Dim lblPrevQty As Label = DirectCast(item.FindControl("lblPrevQty"), Label)
                    Dim objTabFullKegSuppToCust As New PropertyTabFullKegSuppToCust
                    objTabFullKegSuppToCust.FullKegID = lblFullKegID.Text
                    objTabFullKegSuppToCust.CompanyID = objTabMCustomers.CompanyID
                    objTabFullKegSuppToCust.SupplierID = rcbSupplier.SelectedValue
                    objTabFullKegSuppToCust.CustomerID = rcbToCustomer.SelectedValue
                    objTabFullKegSuppToCust.ItemID = itemid.Text

                    objTabFullKegSuppToCust.Quantity = txtQuantity.Text
                    objTabFullKegSuppToCust.ReceiptDate = dtReceiptDate.SelectedDate
                    If Not dtBOLDate.DateInput.Text = "" Then
                        objTabFullKegSuppToCust.BOLDate = dtBOLDate.SelectedDate
                    Else
                        objTabFullKegSuppToCust.BOLDate = Nothing
                    End If
                    objTabFullKegSuppToCust.BOLNumber = txtBOLNo.Text
                    objTabFullKegSuppToCust.POType = txtPOType.Text
                    objTabFullKegSuppToCust.PONumber = txtPONumber.Text
                    objTabFullKegSuppToCust.ContainerNumber = txtContainerNO.Text
                    objTabFullKegSuppToCust.TrasactionDoneBy = objTabMCustomers.EmployeeID
                    objTabFullKegSuppToCust.TransactionNumber = txtTransactionNumber.Text
                    objTabFullKegSuppToCust.ModifiedBy = objuserrole("employeeID")
                    ret = clsTabFullKegSuppToCust.UpdateTabFullKegSuppToCust(objTabFullKegSuppToCust, objuserInfo("schemaName"))
                Next
            Else
                For Each item As GridDataItem In RadgvFullKegReceive.Items
                    'Create new instance of the class to store the aspx controls data...
                    Dim objTabFullKegSuppToCust As New PropertyTabFullKegSuppToCust
                    objTabFullKegSuppToCust.CompanyID = objTabMCustomers.CompanyID
                    objTabFullKegSuppToCust.SupplierID = rcbSupplier.SelectedValue
                    objTabFullKegSuppToCust.CustomerID = rcbToCustomer.SelectedValue
                    Dim itemid As Label = DirectCast(item.FindControl("lblgridItemID"), Label)
                    objTabFullKegSuppToCust.ItemID = itemid.Text
                    
                    Dim txtQuantity As TextBox = DirectCast(item.FindControl("txtQuantity"), TextBox)
                    objTabFullKegSuppToCust.Quantity = txtQuantity.Text
                    objTabFullKegSuppToCust.ReceiptDate = dtReceiptDate.SelectedDate
                    If Not dtBOLDate.DateInput.Text = "" Then
                        objTabFullKegSuppToCust.BOLDate = dtBOLDate.SelectedDate
                    Else
                        objTabFullKegSuppToCust.BOLDate = Nothing
                    End If
                    objTabFullKegSuppToCust.BOLNumber = txtBOLNo.Text
                    objTabFullKegSuppToCust.POType = txtPOType.Text
                    objTabFullKegSuppToCust.PONumber = txtPONumber.Text
                    objTabFullKegSuppToCust.ContainerNumber = txtContainerNO.Text
                    objTabFullKegSuppToCust.TrasactionDoneBy = objTabMCustomers.EmployeeID
                    If txtTransactionNumber.Text = "" Then
                        Dim ds = clsTabKEmptyCustomer.GetMaxTransactionNumber(objuserInfo("schemaName"), 4, objTabMCustomers.CompanyID)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Dim str3 As String = ""
                                Dim str As String = ds.Tables(0).Rows(0)(0)
                                Dim str1 = str.Substring(2, str.Length - 2)
                                Dim str2 = Convert.ToString(Convert.ToInt32(str1) + 1)
                                If str2.Length = 1 Then
                                    str3 = "SC" + "0000000" + str2
                                ElseIf str2.Length = 2 Then
                                    str3 = "SC" + "000000" + str2
                                ElseIf str2.Length = 3 Then
                                    str3 = "SC" + "00000" + str2
                                ElseIf str2.Length = 4 Then
                                    str3 = "SC" + "0000" + str2
                                ElseIf str2.Length = 5 Then
                                    str3 = "SC" + "000" + str2
                                ElseIf str2.Length = 6 Then
                                    str3 = "SC" + "00" + str2
                                ElseIf str2.Length = 7 Then
                                    str3 = "SC" + "0" + str2
                                ElseIf str2.Length >= 8 Then
                                    str3 = "SC" + str2
                                End If
                                objTabFullKegSuppToCust.TransactionNumber = str3
                                txtTransactionNumber.Text = str3
                            Else
                                objTabFullKegSuppToCust.TransactionNumber = "SC00000001"
                                txtTransactionNumber.Text = "SC00000001"
                            End If
                        Else
                            objTabFullKegSuppToCust.TransactionNumber = "SC00000001"
                            txtTransactionNumber.Text = "SC00000001"
                        End If
                    Else
                        objTabFullKegSuppToCust.TransactionNumber = txtTransactionNumber.Text
                    End If
                    ret = clsTabFullKegSuppToCust.Save(objTabFullKegSuppToCust, objuserInfo("schemaName"))
                Next
            End If
            If (ret) And mid <> "" Then
                Response.Redirect("frmKegPurchaseModule_SupToCus.aspx?process=Y&mode=E")
            ElseIf (ret) Then
                Response.Redirect("frmKegPurchaseModule_SupToCus.aspx?process=Y&mode=A")
            Else
                lblMsg.Text = Resources.lang.msgError
            End If
           
        End Sub

        Private Sub RadgvFullKegReceive_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadgvFullKegReceive.NeedDataSource
            If Not mid = "" Then
                Dim objTabMCustomers = clsTabMCustomers.GetAllTabMCustomersByCustomerCode(objuserInfo("employeeName"), objuserInfo("schemaName"))
                Dim ds = clsTabFullKegSuppToCust.GetAllTabFullKegSuppToCust(objTabMCustomers.CompanyID, objuserInfo("schemaName"), mid)
                If Not ds Is Nothing Then
                    rcbSupplier.SelectedValue = ds.Tables(0).Rows(0)("SupplierID")
                    rcbToCustomer.SelectedValue = ds.Tables(0).Rows(0)("CustomerID")
                    rcbSupplier.Enabled = False
                    rcbToCustomer.Enabled = False
                    txtTransactionNumber.Text = ds.Tables(0).Rows(0)("TransactionNumber")
                    If Not IsDBNull(ds.Tables(0).Rows(0)("BOLDate")) Then
                        If Not ds.Tables(0).Rows(0)("BOLDate") = "" Then
                            dtBOLDate.SelectedDate = Convert.ToDateTime(ds.Tables(0).Rows(0)("BOLDate"))
                        End If
                    End If
                    txtBOLNo.Text = ds.Tables(0).Rows(0)("BOLNumber")
                    txtPOType.Text = ds.Tables(0).Rows(0)("POType")
                    txtPONumber.Text = ds.Tables(0).Rows(0)("PONumber")
                    txtContainerNO.Text = ds.Tables(0).Rows(0)("ContainerNumber")
                    dtReceiptDate.SelectedDate = Convert.ToDateTime(ds.Tables(0).Rows(0)("ReceiptDate"))
                    RadgvFullKegReceive.DataSource = ds
                    dt = ds.Tables(0)
                    ViewState("table") = dt
                End If
            End If
        End Sub
    End Class
End Namespace
