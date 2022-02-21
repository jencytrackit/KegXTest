

Imports CrystalDecisions.CrystalReports.Engine
Imports Telerik.Web.UI
Imports CrystalDecisions.Shared
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports System.Globalization
Namespace TrackITKTS
    Public Class rptCustomerEmptyKegCollection
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
            If objuserInfo Is Nothing Then
                Return
            End If
            If Not Page.IsPostBack Then 'filling company dropdown values and setting customerName,CustomerCode dropdowns intial value 
                BindItems()
                BindUsername()
                Session("rReportsCColl") = Nothing
                FromDt.SelectedDate = Now.Date
                ToDt.SelectedDate = Now.Date
            Else
               
                    If rcbUserName.SelectedIndex <= 0 Then
                        lblMsg.Text = "UserName is required"
                        Me.CrystalReportViewer1.ReportSource = Nothing
                        CrystalReportViewer1.RefreshReport()
                        Exit Sub
                    Else
                        lblMsg.Text = ""
                    End If

            End If

        End Sub
      
        Private Sub BindUsername()
            rcbUserName.Items.Clear()
            Dim dsuser As New DataSet
            dsuser = clsTabKEmptyCustomer.GetAllUsers(objuserInfo("schemaName"))
            If dsuser.Tables(0).Rows.Count > 0 Then
                rcbUserName.DataSource = dsuser
                rcbUserName.DataTextField = "UserName"
                rcbUserName.DataValueField = "UserID"
                rcbUserName.DataBind()
            End If
            rcbUserName.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        End Sub
        Private Sub BindItems()
            rcbItems.Items.Clear()
            Dim ds As New DataSet


            'ds = clsTabMCustomers.GetRptCustomersDrop(rcbCompany.SelectedValue, objuserrole("employeeID"), objuserInfo("schemaName"))
            ds = clsTabItems.GetTabItemsByCompanyID(3, objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                rcbItems.DataSource = ds
                rcbItems.DataTextField = "ItemName"
                rcbItems.DataValueField = "ItemCode"
                rcbItems.DataBind()
            End If

        End Sub


        Private Sub BindReport()
            Dim intUserID As Integer
            If Session("rReportsCColl") Is Nothing Then Return
            Dim strItemID As String = Session("rReportsCColl")("rItem")
            Dim intCompanyID As Integer = Session("rReportsCColl")("rCompanyID")
            Dim dtFromDate As DateTime = Session("rReportsCColl")("rFromDate")
            Dim dttoDate As DateTime = Session("rReportsCColl")("rToDate")
            intUserID = Session("rReportsCColl")("rUserID")
         
            Dim rUserName As String = Session("rReportsCColl")("rUserName")

            Dim repPath As String = ""
            Dim Filter As String = ""


            If FromDt.SelectedDate > ToDt.SelectedDate Then
                lblMsg.Text = "From Date shoud be always less than To Date"
                Exit Sub
            Else
                lblMsg.Text = ""
            End If

            repPath = System.Configuration.ConfigurationManager.AppSettings("ReportPath") & "CustomerEmptyKegCollection.rpt" 'Intializing repPath variable to the actual report path

            Dim ConnInfo As New ConnectionInfo
            With ConnInfo 'getting database connection details from web.config
                .ServerName = System.Configuration.ConfigurationManager.AppSettings("server")
                .DatabaseName = System.Configuration.ConfigurationManager.AppSettings("database")
                .UserID = System.Configuration.ConfigurationManager.AppSettings("userid")
                .Password = System.Configuration.ConfigurationManager.AppSettings("password")
            End With

            'Dim paramField As New ParameterField()
            Dim paramFields As New ParameterFields()

            Dim path As String = Server.MapPath(repPath)
            rep.Load(path) 'Loading the report by setting report path


            ds = clsReports.REPCustomerEmptyKegCollection(dtFromDate, dttoDate, intUserID, strItemID, objuserInfo("schemaName"))
            rep.SetDataSource(ds.Tables(0))


            Dim RepTbls As Tables = rep.Database.Tables
            For Each RepTbl As Table In RepTbls
                Dim RepTblLogonInfo As TableLogOnInfo = RepTbl.LogOnInfo
                RepTblLogonInfo.ConnectionInfo = ConnInfo
                RepTbl.ApplyLogOnInfo(RepTblLogonInfo)
            Next

            rep.DataDefinition.RecordSelectionFormula = Filter 'Filtering the report 


            'rep.SummaryInfo.ReportTitle = Session("rReportsCColl")("rCompanyName")

            rep.SummaryInfo.ReportComments = Session("rReportsCColl")("rUserName")
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

        Private Sub CrystalReportViewer1_ViewZoom(ByVal source As Object, ByVal e As CrystalDecisions.Web.ZoomEventArgs) Handles CrystalReportViewer1.ViewZoom
            ViewState("zoomLevel") = e.NewZoomFactor
        End Sub

        Private Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.Click

            Dim htbl As New Hashtable
            hdfItemIDs.Value = LoopItems()
            'htbl.Add("rCompanyID", rcbCompany.SelectedValue)
            htbl.Add("rFromDate", FromDt.SelectedDate)
            htbl.Add("rToDate", ToDt.SelectedDate)
            htbl.Add("rItem", hdfItemIDs.Value)

            'htbl.Add("rCompanyName", rcbCompany.SelectedItem.Text)
            If rcbUserName.SelectedIndex > 0 Then
                htbl.Add("rUserID", rcbUserName.SelectedItem.Value)
                htbl.Add("rUserName", rcbUserName.SelectedItem.Text)
            Else
            End If
            Session("rReportsCColl") = htbl

            BindReport() 'Button click binding the report

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
        Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
            rep.Close()
            rep.Dispose()
            GC.Collect()
        End Sub
    End Class
End Namespace