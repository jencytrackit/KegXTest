Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Public Class frmTest
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim objuserrole As New Hashtable()
        Private mid As String = ""
        Private strdate As String = ""
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            strdate = Convert.ToString(System.DateTime.Now)
            lblPostback.Text = strdate
            If Not IsPostBack Then
                BindCustomer()
                BindBP()
                EditMode("CB00000001")
                strdate = strdate + " " + Convert.ToString(System.DateTime.Now)
                lblnotpostback.Text = strdate
            End If
        End Sub
        Private Sub EditMode(ByVal mid As String)
            If Not mid = "" Then
                Dim strCompanyId = 1
                Dim ds = clsTabKEmptyCustomer.GetAllTabKEmptyCustomerByCompanyID(strCompanyId, objuserInfo("schemaName"), mid)
                If Not ds Is Nothing Then
                    rcbCustomer.SelectedValue = ds.Tables(0).Rows(0)("CustomerID")
                    rcbToBP.SelectedValue = ds.Tables(0).Rows(0)("BranchID")
                    rcbCustomer.Enabled = False
                    rcbToBP.Enabled = False
                    txtTransactionNumber.Text = ds.Tables(0).Rows(0)("TransactionNumber")

                    RadgvEmptyKegTransfer.DataSource = ds
                    RadgvEmptyKegTransfer.DataBind()
                End If
            End If
        End Sub
        Private Sub BindBP()
            Dim ds = clsTabMBranchPlant.GetTabMBranchPlantByCompanyID(1, objuserrole("employeeID"), objuserInfo("schemaName"))
            rcbToBP.DataSource = ds
            rcbToBP.DataTextField = "BranchName"
            rcbToBP.DataValueField = "BranchID"
            rcbToBP.DataBind()
        End Sub
        Private Sub BindCustomer()
            Dim ds = clsTabMCustomers.GetTabMCustomersByCompanyID(1, objuserrole("employeeID"), objuserInfo("schemaName"))
            rcbCustomer.DataSource = ds
            rcbCustomer.DataTextField = "CustomerName"
            rcbCustomer.DataValueField = "CustomerID"
            rcbCustomer.DataBind()
        End Sub
        Private Sub txtItemCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtItemCode.TextChanged
            strdate = strdate + " " + Convert.ToString(System.DateTime.Now)
            lbltextchangedstart.Text = strdate
            If Trim(txtItemCode.Text) <> "" Then
                Dim ds As DataSet = clsTabUOMMaster.GetTabUOMMasterByItemCode(Trim(txtItemCode.Text), objuserInfo("schemaName"), 1) 'Get the item details by Item code
                If ds Is Nothing Then
                    ClearFields()
                    Exit Sub
                End If
                If ds.Tables(0).Rows.Count > 0 Then
                    txtItemDesc.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName1"))
                    txtItemName2.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName2"))
                    txtItemID.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemID"))
                    If ds.Tables(1).Rows.Count > 0 Then
                        ddlUOM.DataSource = ds.Tables(1)
                        ddlUOM.DataTextField = "PrimayUOM"
                        ddlUOM.DataValueField = "UOMID"
                        ddlUOM.DataBind()
                        ddlUOM.Items.Insert(0, New RadComboBoxItem("--Select One--"))
                    Else
                        ddlUOM.Items.Clear()
                        ddlUOM.Items.Insert(0, New RadComboBoxItem("--Select One--"))
                    End If

                Else
                    ClearFields()
                End If
            Else
                ClearFields()
            End If
            strdate = strdate + " " + Convert.ToString(System.DateTime.Now)
            lbltextchangedstop.Text = strdate
        End Sub
        Private Sub ClearFields()
            txtItemDesc.Text = ""
            txtItemID.Text = ""
            txtQuantity.Text = ""
            txtItemName2.Text = ""
            'txtSerialNo.Text = ""
            ddlUOM.Items.Clear()
            ddlUOM.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        End Sub

        Private Sub btnGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGet.Click
            If Trim(txtItemCode1.Text) <> "" Then
                Dim ds As DataSet = clsTabUOMMaster.GetTabUOMMasterByItemCode(Trim(txtItemCode1.Text), objuserInfo("schemaName"), 1) 'Get the item details by Item code
                If ds Is Nothing Then
                    ClearFields()
                    Exit Sub
                End If
                If ds.Tables(0).Rows.Count > 0 Then
                    txtItemDesc.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName1"))
                    txtItemName2.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemName2"))
                    txtItemID.Text = Convert.ToString(ds.Tables(0).Rows(0)("ItemID"))
                    If ds.Tables(1).Rows.Count > 0 Then
                        ddlUOM.DataSource = ds.Tables(1)
                        ddlUOM.DataTextField = "PrimayUOM"
                        ddlUOM.DataValueField = "UOMID"
                        ddlUOM.DataBind()
                        ddlUOM.Items.Insert(0, New RadComboBoxItem("--Select One--"))
                    Else
                        ddlUOM.Items.Clear()
                        ddlUOM.Items.Insert(0, New RadComboBoxItem("--Select One--"))
                    End If

                Else
                    ClearFields()
                End If
            Else
                ClearFields()
            End If
        End Sub
    End Class
End Namespace
