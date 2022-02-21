
'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmReportRequests.aspx
'Created By         :
'File navigation    :
'Created Date       :November 06, 2013, 4:46:57 PM
'Description        :This file is used to List the Branch Plant Records...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports System.IO

Namespace TrackITKTS
    Public Class frmReportRequests
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging
        Dim objuserrole As New Hashtable()
        Dim zoomLevel As Integer = clsReports.ReportZoom.ZoomDefaultSize
        Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
            objuserInfo = Session("UserDetails") '// UserDetails values assigned to objuserInfo
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
            'If Page.IsPostBack Then
            '    BindGrid()
            'End If
        End Sub
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'objuserInfo = Session("UserDetails") '// UserDetails values assigned to objuserInfo
            'objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
            If Not Page.IsPostBack Then 'filling company dropdown values and setting customerName,CustomerCode dropdowns intial value 
                BindRequests()
            Else
                If rcbRequests.SelectedIndex > 0 Then
                    If mvRequests.ActiveViewIndex = 1 And rcbCustomers.CheckedItems.Count = 0 Then
                        lblMsg.Text = "Customer is required"
                        Exit Sub
                    Else
                        lblMsg.Text = ""
                    End If
                End If
            End If
        End Sub
        Private Sub BindRequests()
            rcbRequests.Items.Clear()
            Dim ds As New DataSet
            ds = clsReports.GetAllTabMRequestsByCompanyID(0, objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                ' bind  Requests
                rcbRequests.DataSource = ds
                rcbRequests.DataTextField = "Description"
                rcbRequests.DataValueField = "RequestName"
                rcbRequests.DataBind()
            End If
            rcbRequests.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        End Sub
        Private Sub rcbRequests_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbRequests.SelectedIndexChanged
            lblMsg.Text = ""
            hdfCustomerID.Value = ""
            hdfItemIDs.Value = ""
            If rcbRequests.SelectedIndex > 0 Then
                mvRequests.ActiveViewIndex = rcbRequests.SelectedIndex
                If (mvRequests.ActiveViewIndex = 1) Then
                    BindCompany()
                    FromDt.SelectedDate = Now.Date
                    ToDt.SelectedDate = Now.Date
                End If
            Else
                mvRequests.ActiveViewIndex = 0
            End If
        End Sub
        Private Sub BindCompany()
            rcbCompany.Items.Clear()
            Dim ds As New DataSet
            ds = clsTabMOrganization.GetAllTabMOrganizationByEmployeeID(objuserrole("employeeID"), objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                ' bind  company
                rcbCompany.DataSource = ds
                rcbCompany.DataTextField = "CompanyName"
                rcbCompany.DataValueField = "CompanyID"
                rcbCompany.DataBind()
            End If
            rcbCompany.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        End Sub

        Private Sub BindCustomers()
            rcbCustomers.Items.Clear()
            Dim ds As New DataSet

            If rcbCompany.SelectedIndex < 1 Then
                lblMsg.Text = "Please select the company"
                Return
            End If

            ds = clsTabMCustomers.GetRptCustomersDrop(rcbCompany.SelectedValue, objuserrole("employeeID"), objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                rcbCustomers.DataSource = ds
                rcbCustomers.DataTextField = "CustomerName"
                rcbCustomers.DataValueField = "CustomerID"
                rcbCustomers.DataBind()
            End If

        End Sub
        Private Sub BindItems()
            rcbItems.Items.Clear()
            Dim ds As New DataSet

            If rcbCompany.SelectedIndex < 1 Then
                lblMsg.Text = "Please select the company"
                Return
            End If

            'ds = clsTabMCustomers.GetRptCustomersDrop(rcbCompany.SelectedValue, objuserrole("employeeID"), objuserInfo("schemaName"))
            ds = clsTabItems.GetTabItemsByCompanyID(rcbCompany.SelectedValue, objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                rcbItems.DataSource = ds
                rcbItems.DataTextField = "ItemName"
                rcbItems.DataValueField = "ItemID"
                rcbItems.DataBind()
            End If

        End Sub


        Private Sub BindGrid()
            RadgvReportRequests.DataBind()
        End Sub


        Private Sub btnReq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReq.Click
            If rcbRequests.SelectedIndex <= 0 Then
                lblMsg.Text = "Please select Report Name"
                ClientScript.RegisterStartupScript(Me.GetType(), "hideOverlay", "hideOverlay();", True)
                Exit Sub
            End If

            Dim RequestNo As Integer = 0

            If (mvRequests.ActiveViewIndex = 1) Then
                RequestNo = SubmitCustomerSOA()
            End If

            If RequestNo > 0 Then
                lblMsg.Text = "Request has been submitted with Process ID : " + RequestNo.ToString()
                BindGrid()
            Else
                Exit Sub
            End If

            ClientScript.RegisterStartupScript(Me.GetType(), "hideOverlay", "hideOverlay();", True)

        End Sub

        Private Function SubmitCustomerSOA() As Integer
            If rcbCompany.SelectedIndex > 0 Then
                If rcbCustomers.CheckedItems.Count = 0 Then
                    lblMsg.Text = "Customer is required"
                    ClientScript.RegisterStartupScript(Me.GetType(), "hideOverlay", "hideOverlay();", True)
                    Return 0
                Else
                    lblMsg.Text = ""
                End If

                Dim strRequestName As String = rcbRequests.SelectedValue
                Dim intCompanyID As Integer = rcbCompany.SelectedValue
                Dim intCustomerID As String = LoopCustomer()
                Dim strItemID As String = LoopItems()
                Dim dtFromDate As DateTime = FromDt.SelectedDate
                Dim dttoDate As DateTime = ToDt.SelectedDate
                Dim IsMultiFileDownload As Boolean
                If (rbtFileType.SelectedValue = "1") Then
                    IsMultiFileDownload = True
                End If

                If FromDt.SelectedDate > ToDt.SelectedDate Then
                    lblMsg.Text = "From Date shoud be always less than To Date"
                    Return 0
                Else
                    lblMsg.Text = ""
                End If

                Return clsReports.SubmitRequest(strRequestName, intCompanyID.ToString(), dtFromDate.ToString("dd MMM yyyy"), dttoDate.ToString("dd MMM yyyy"), intCustomerID, strItemID, Nothing, Nothing, objuserrole("employeeID"), IsMultiFileDownload, objuserInfo("schemaName"))

            End If
        End Function

        Public Function LoopCustomer() As String
            Dim strSelectedItem As String = ""
            For Each radcbiSource In rcbCustomers.CheckedItems
                If rcbCustomers.CheckedItems.Count = rcbCustomers.Items.Count Then
                    Return "ALL"
                End If
                If rcbCustomers.CheckedItems.Count > 0 Then
                    strSelectedItem += radcbiSource.Value
                    strSelectedItem += ", "
                End If
            Next
            Return strSelectedItem
        End Function
        Public Function LoopItems() As String
            Dim strSelectedItem As String = ""

            If rcbItems.CheckedItems.Count = 0 Then
                Return "ALL"
            Else
                For Each radcbiSource In rcbItems.CheckedItems
                    If rcbItems.CheckedItems.Count = rcbItems.Items.Count Then
                        Return "ALL"
                    End If
                    If rcbItems.CheckedItems.Count > 0 Then
                        strSelectedItem += radcbiSource.Value
                        strSelectedItem += ", "
                    End If
                Next
            End If
            Return strSelectedItem
        End Function
        Private Sub rcbCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCompany.SelectedIndexChanged
            lblMsg.Text = ""
            'AddContextKey()
            hdfCustomerID.Value = ""
            hdfItemIDs.Value = ""
            If rcbCompany.SelectedIndex > 0 Then
                BindCustomers()
                BindItems()
            Else
                hdfCustomerID.Value = ""
                hdfItemIDs.Value = ""
            End If
        End Sub

        Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
            GC.Collect()
        End Sub
        Private Sub ObjReportRequests_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjReportRequests.Selecting
            'Pass the logged in employeeid and retrieve the branch plants details associated to the logged in user and company

            e.InputParameters("CompanyID") = 0
            e.InputParameters("EmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")

        End Sub

        Private Sub RadgvReportRequests_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadgvReportRequests.ItemCommand
            If e.CommandName = "ExcelDownload" Then
                Dim item = DirectCast(e.Item, GridDataItem)
                Dim lnkbatch As LinkButton = DirectCast(item.FindControl("lnkExcelDownload"), LinkButton)
                Dim lblRequestID As Label = DirectCast(item.FindControl("lblRequestID"), Label)
                Dim IsMultiFileDownload As Label = DirectCast(item.FindControl("IsMultiFileDownload"), Label)
                Dim lblRequestName As Label = DirectCast(item.FindControl("lblRequestName"), Label)
                Dim RequestName As String = lblRequestName.Text
                Dim fileName As String
                If (IsMultiFileDownload.Text = "True") Then
                    fileName = RequestName & "_" & lblRequestID.Text & "E.zip"
                Else
                    fileName = RequestName & "_" & lblRequestID.Text & ".xls"
                End If

                Dim RepDump As String = System.Configuration.ConfigurationManager.AppSettings("RepDump")
                Dim path As String = RepDump & fileName
                If File.Exists(path) Then
                    DownloadFile(fileName)
                Else
                    lblMsg.Text = "File Not Found"
                End If
            ElseIf e.CommandName = "PDFDownload" Then
                Dim item = DirectCast(e.Item, GridDataItem)
                Dim lnkbatch As LinkButton = DirectCast(item.FindControl("lnkPDFDownload"), LinkButton)
                Dim lblRequestID As Label = DirectCast(item.FindControl("lblRequestID"), Label)
                Dim IsMultiFileDownload As Label = DirectCast(item.FindControl("IsMultiFileDownload"), Label)
                Dim lblRequestName As Label = DirectCast(item.FindControl("lblRequestName"), Label)
                Dim RequestName As String = lblRequestName.Text
                Dim fileName As String
                If (IsMultiFileDownload.Text = "True") Then
                    fileName = RequestName & "_" & lblRequestID.Text & "P.zip"
                Else
                    fileName = RequestName & "_" & lblRequestID.Text & ".pdf"
                End If

                Dim RepDump As String = System.Configuration.ConfigurationManager.AppSettings("RepDump")
                Dim path As String = RepDump & fileName
                If File.Exists(path) Then
                    DownloadFile(fileName)
                Else
                    lblMsg.Text = "File Not Found"
                End If
            End If
        End Sub
        Private Sub DownloadFile(ByVal fileName As String)
            Dim RepDump As String = System.Configuration.ConfigurationManager.AppSettings("RepDump")
            Dim path As String = RepDump & fileName 'Server.MapPath("/sample/" & fileName)
            Dim bts As Byte() = System.IO.File.ReadAllBytes(path)
            Response.Clear()
            Response.ClearHeaders()
            Response.AddHeader("Content-Type", "Application/octet-stream")
            Response.AddHeader("Content-Length", bts.Length.ToString())
            Response.AddHeader("Content-Disposition", "attachment; filename=" & fileName)
            Response.BinaryWrite(bts)
            Response.Flush()
            Response.End()
        End Sub

        Protected Sub RadgvRequestsTimer_Tick(sender As Object, e As EventArgs)
            RadgvReportRequests.Rebind()
        End Sub
    End Class
End Namespace
