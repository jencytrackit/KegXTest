'***************************************************************
'Application :Kegs Tracking System
'File Name : rptSupplierSummary.aspx
'Created By : 
'File navigation :
'Created Date : November 15, 2013, 3:28:14 PM
'Description : This is used to show Supplier Summary Details Report
'Modified By :
'Modified Date :
'***************************************************************

Imports CrystalDecisions.CrystalReports.Engine
Imports Telerik.Web.UI
Imports CrystalDecisions.Shared
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class rptSupplierSummary
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging
        Dim objuserrole As New Hashtable()
        Dim rep As New ReportDocument
        Dim ds As New DataSet
        Dim zoomLevel As Integer = clsReports.ReportZoom.ZoomDefaultSize
        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            objuserInfo = Session("UserDetails") '// UserDetails values assigned to objuserInfo
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
            If Page.IsPostBack Then
                BindReport()
            End If
        End Sub
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'objuserInfo = Session("UserDetails") '// UserDetails values assigned to objuserInfo
            'objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
            If Not Page.IsPostBack Then 'filling company dropdown values and setting branch,supplier dropdown intial value 
                'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
                BindCompany()
                Session("rReportsSupSumm") = Nothing
                FromDt.SelectedDate = Now.Date
                ToDt.SelectedDate = Now.Date
            Else
                'BindReport()
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

        Private Sub BindReport()
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            If Session("rReportsSupSumm") Is Nothing Then Return

            Dim intCompanyID As Integer = Session("rReportsSupSumm")("rCompanyID")
            Dim strItemID As String = Session("rReportsSupSumm")("rItem")
            Dim dtFromDate As DateTime = Session("rReportsSupSumm")("rFromDate")
            Dim dttoDate As DateTime = Session("rReportsSupSumm")("rToDate")
            Dim repPath As String = ""
            Dim Filter As String = ""

            'Intializing repPath variable to the actual report path
            repPath = System.Configuration.ConfigurationManager.AppSettings("ReportPath") & "SupplierSummary.rpt"

            ' Dim paramField As New ParameterField()
            Dim paramFields As New ParameterFields()
            ' Dim paramDiscreteValue As New ParameterDiscreteValue()

            'paramField = New ParameterField()
            'paramField.Name = "@CompanyID"
            'paramDiscreteValue = New ParameterDiscreteValue()
            'If rcbCompany.SelectedIndex > 0 Then
            '    paramDiscreteValue.Value = rcbCompany.SelectedItem.Value
            'Else
            '    paramDiscreteValue.Value = 0
            'End If
            'paramField.CurrentValues.Add(paramDiscreteValue)
            'paramFields.Add(paramField)


            'paramField = New ParameterField()
            'paramField.Name = "@FromDate"
            'paramDiscreteValue = New ParameterDiscreteValue()

            'paramDiscreteValue.Value = FromDt.SelectedDate

            'paramField.CurrentValues.Add(paramDiscreteValue)
            'paramFields.Add(paramField)

            'paramField = New ParameterField()
            'paramField.Name = "@ToDate"
            'paramDiscreteValue = New ParameterDiscreteValue()

            'paramDiscreteValue.Value = ToDt.SelectedDate

            'paramField.CurrentValues.Add(paramDiscreteValue)
            'paramFields.Add(paramField)


            Dim path As String = Server.MapPath(repPath)
            rep.Load(path) 'Loading the report by setting report path

            Dim ConnInfo As New ConnectionInfo
            With ConnInfo 'getting database connection details from web.config
                .ServerName = System.Configuration.ConfigurationManager.AppSettings("server")
                .DatabaseName = System.Configuration.ConfigurationManager.AppSettings("database")
                .UserID = System.Configuration.ConfigurationManager.AppSettings("userid")
                .Password = System.Configuration.ConfigurationManager.AppSettings("password")
            End With

            ' ds = clsReports.REPSupplierSummary(rcbCompany.SelectedItem.Value, FromDt.SelectedDate, ToDt.SelectedDate, objuserInfo("schemaName"))
            ds = clsReports.REPSupplierSummary(intCompanyID, dtFromDate, dttoDate, strItemID, objuserInfo("schemaName"))
            rep.SetDataSource(ds.Tables(0))


            Dim RepTbls As Tables = rep.Database.Tables
            For Each RepTbl As Table In RepTbls
                Dim RepTblLogonInfo As TableLogOnInfo = RepTbl.LogOnInfo
                RepTblLogonInfo.ConnectionInfo = ConnInfo
                RepTbl.ApplyLogOnInfo(RepTblLogonInfo)
            Next

            'rep.DataDefinition.RecordSelectionFormula = Filter 'Filtering the report 
            rep.SummaryInfo.ReportTitle = Session("rReportsSupSumm")("rCompanyName")

            If Not ViewState("zoomLevel") Is Nothing Then
                zoomLevel = ViewState("zoomLevel")
            End If

            CrystalReportViewer1.Zoom(zoomLevel)

            CrystalReportViewer1.ParameterFieldInfo = paramFields
            CrystalReportViewer1.HasSearchButton = False 'hide the search buttons 
            CrystalReportViewer1.HasDrillUpButton = False 'hide the drillup button 
            Me.CrystalReportViewer1.ReportSource = rep 'Report is binding to crystalreportviewer
            CrystalReportViewer1.RefreshReport() 'Refreshing the report viewer
        End Sub


        Private Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.Click
            If rcbCompany.SelectedIndex > 0 Then
             
                Dim htbl As New Hashtable
                hdfItemIDs.Value = LoopItems()
                htbl.Add("rCompanyID", rcbCompany.SelectedValue)
                htbl.Add("rItem", hdfItemIDs.Value)
                htbl.Add("rFromDate", FromDt.SelectedDate)
                htbl.Add("rToDate", ToDt.SelectedDate)
                htbl.Add("rCompanyName", rcbCompany.SelectedItem.Text)
                Session("rReportsSupSumm") = htbl

                BindReport() 'Button click binding the report
             
            End If
            ClientScript.RegisterStartupScript(Me.GetType(), "hideOverlay", "hideOverlay();", True)
        End Sub
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

            hdfItemIDs.Value = ""
            'txtSupplier.Text = ""
            If rcbCompany.SelectedIndex > 0 Then

                BindItems()
                CrystalReportViewer1.ReportSource = Nothing
                CrystalReportViewer1.RefreshReport()
            Else

                hdfItemIDs.Value = ""
                'txtSupplier.Text = ""
            End If
        End Sub
        Private Sub CrystalReportViewer1_ViewZoom(ByVal source As Object, ByVal e As CrystalDecisions.Web.ZoomEventArgs) Handles CrystalReportViewer1.ViewZoom
            ViewState("zoomLevel") = e.NewZoomFactor
        End Sub

        Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
            rep.Close()
            rep.Dispose()
            GC.Collect()
        End Sub
    End Class
End Namespace