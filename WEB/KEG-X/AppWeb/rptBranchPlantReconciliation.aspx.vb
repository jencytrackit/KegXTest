'***************************************************************
'Application :Kegs Tracking System
'File Name : rptBranchPlantReconciliation.aspx
'Created By : 
'File navigation :
'Created Date : November 15, 2013, 3:28:13 PM
'Description : This is used to show BranchPlant Reconciliation Details Report
'Modified By :
'Modified Date :
'***************************************************************
Imports CrystalDecisions.CrystalReports.Engine
Imports Telerik.Web.UI
Imports CrystalDecisions.Shared
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class rptBranchPlantReconciliation
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging
        Dim objuserrole As New Hashtable()
        Dim rep As New ReportDocument

        Dim zoomLevel As Integer = clsReports.ReportZoom.ZoomDefaultSize

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// UserDetails values assigned to objuserInfo
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
            If Not Page.IsPostBack Then 'filling company dropdown values and setting SupplierCode,SupplierName dropdown intial value 
                BindCompany()
                ddlBranchPlantCode.Items.Insert(0, New RadComboBoxItem("--Select One--"))
            ElseIf ddlCompanyName.SelectedIndex > 0 Then 'Company dropdown value Greater than zero binding the report
                BindReport()
            End If
        End Sub

        Private Sub BindReport()
            Dim repPath As String = ""
            Dim Filter As String = ""
            Dim BranchCode As String = ""
            repPath = System.Configuration.ConfigurationManager.AppSettings("ReportPath") & "BranchPlantReconciliation.rpt"  'Intializing repPath variable to the actual report path


            Dim ConnInfo As New ConnectionInfo
            With ConnInfo 'getting database connection details from web.config
                .ServerName = System.Configuration.ConfigurationManager.AppSettings("server")
                .DatabaseName = System.Configuration.ConfigurationManager.AppSettings("database")
                .UserID = System.Configuration.ConfigurationManager.AppSettings("userid")
                .Password = System.Configuration.ConfigurationManager.AppSettings("password")
            End With

            Dim paramField As New ParameterField()
            Dim paramFields As New ParameterFields()
            Dim paramDiscreteValue As New ParameterDiscreteValue()

            paramField = New ParameterField()
            paramField.Name = "@CompanyID"
            paramDiscreteValue = New ParameterDiscreteValue()
            If ddlCompanyName.SelectedIndex > 0 Then
                paramDiscreteValue.Value = ddlCompanyName.SelectedItem.Value
            Else
                paramDiscreteValue.Value = 0
            End If
            paramField.CurrentValues.Add(paramDiscreteValue)
            paramFields.Add(paramField)


            paramField = New ParameterField()
            paramField.Name = "@BPCode"
            paramDiscreteValue = New ParameterDiscreteValue()
            If ddlBranchPlantCode.SelectedIndex > 0 Then
                Dim wordArr As String() = ddlBranchPlantCode.SelectedItem.Text.Split(" -")
                BranchCode = wordArr(0)
                paramDiscreteValue.Value = BranchCode
            Else
                paramDiscreteValue.Value = ""
            End If
            paramField.CurrentValues.Add(paramDiscreteValue)
            paramFields.Add(paramField)

            Dim path As String = Server.MapPath(repPath)
            rep.Load(path) 'Loading the report by setting report path


            Dim ds As DataSet = clsReports.REPBPReconciliation(ddlCompanyName.SelectedItem.Value, BranchCode, objuserInfo("schemaName"))
            rep.SetDataSource(ds.Tables(0))


            Dim RepTbls As Tables = rep.Database.Tables
            For Each RepTbl As Table In RepTbls
                Dim RepTblLogonInfo As TableLogOnInfo = RepTbl.LogOnInfo
                RepTblLogonInfo.ConnectionInfo = ConnInfo
                RepTbl.ApplyLogOnInfo(RepTblLogonInfo)
            Next

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
        Private Sub BindCompany()
            ddlCompanyName.Items.Clear()
            Dim ds As New DataSet
            ds = clsTabMOrganization.GetAllTabMOrganizationByEmployeeID(objuserrole("employeeID"), objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                ' bind  company
                ddlCompanyName.DataSource = ds
                ddlCompanyName.DataTextField = "CompanyName"
                ddlCompanyName.DataValueField = "CompanyID"
                ddlCompanyName.DataBind()
            End If
        End Sub

        Private Sub BindBranchPlantCode() 'Filling BranchPlantCode dropdown values
            ddlBranchPlantCode.DataSource = clsTabMBranchPlant.GetTabMBranchPlantByCompanyID(ddlCompanyName.SelectedItem.Value, objuserInfo("empID"), objuserInfo("schemaName"))
            ddlBranchPlantCode.DataTextField = "BranchCodeName"
            ddlBranchPlantCode.DataValueField = "BranchID"
            ddlBranchPlantCode.DataBind()
        End Sub
       
        Private Sub ddlCompanyName_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompanyName.DataBound
            ddlCompanyName.Items.Insert(0, New RadComboBoxItem("--Select One--"))  'Inserting Company Dropdown intial value 
        End Sub

        Private Sub ddlBranchPlantCode_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBranchPlantCode.DataBound
            ddlBranchPlantCode.Items.Insert(0, New RadComboBoxItem("--Select One--"))  'Inserting BranchPlantCode Dropdown intial value 
        End Sub

        Private Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.Click
          
            BindReport() 'Button click binding the report
           ClientScript.RegisterStartupScript(Me.GetType(), "hideOverlay", "hideOverlay();", True)
        End Sub

        Private Sub ddlCompanyName_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlCompanyName.SelectedIndexChanged
            If ddlCompanyName.SelectedIndex > 0 Then  'filling BranchPlantCode,BranchPlantName details if dropdown company value is selected
                BindBranchPlantCode()
            Else
                ddlBranchPlantCode.Items.Clear()
                ddlBranchPlantCode.Items.Insert(0, New RadComboBoxItem("--Select One--"))
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