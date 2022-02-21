
'***************************************************************
'Application :Kegs Tracking System
'File Name : rptSupplierReconciliation.aspx
'Created By : 
'File navigation :
'Created Date : November 15, 2013, 11:00:44 AM
'Description : This is used to show Supplier Reconciliation details Report
'Modified By :
'Modified Date :
'***************************************************************

Imports CrystalDecisions.CrystalReports.Engine
Imports Telerik.Web.UI
Imports CrystalDecisions.Shared
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class rptSupplierReconciliation
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

            If Not Page.IsPostBack Then 'filling company dropdown values and setting customerName,CustomerCode dropdowns intial value 
                BindCompany()
                FromDt.SelectedDate = Now.Date
                ToDt.SelectedDate = Now.Date
                Session("rReportsSupRecon") = Nothing
                'ddlSupplierCode.Items.Insert(0, New RadComboBoxItem("--Select One--"))
            Else
                If rcbCompany.SelectedIndex > 0 And hdfSupplierID.Value <> "" Then
                    'BindReport()
                Else
                    If rcbSuppliers.CheckedItems.Count = 0 Then
                        lblMsg.Text = "Supplier is required"
                        Me.CrystalReportViewer1.ReportSource = Nothing
                        CrystalReportViewer1.RefreshReport()
                        Exit Sub
                    Else
                        lblMsg.Text = ""
                    End If
                End If
            End If
            'AddContextKey()
        End Sub
        'Private Sub AddContextKey()

        '    If objuserInfo.Count > 1 And rcbCompany.SelectedIndex > 0 Then
        '        AutoCompleteExtender_txtSupplier.ContextKey = rcbCompany.SelectedValue & "," & objuserInfo("schemaName")
        '    Else
        '        AutoCompleteExtender_txtSupplier.ContextKey = ""
        '    End If

        'End Sub
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
        Private Sub BindSuppliers()
            rcbSuppliers.Items.Clear()
            Dim ds As New DataSet

            If rcbCompany.SelectedIndex < 1 Then
                lblMsg.Text = "Please select the company"
                Return
            End If

            ds = clsTabMSuppliers.GetRptSuppliersDrop(rcbCompany.SelectedValue, objuserrole("employeeID"), objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                rcbSuppliers.DataSource = ds
                rcbSuppliers.DataTextField = "SupplierName"
                rcbSuppliers.DataValueField = "SupplierID"
                rcbSuppliers.DataBind()
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

        Private Sub BindReport()

            If Session("rReportsSupRecon") Is Nothing Then Return

            Dim intCompanyID As Integer = Session("rReportsSupRecon")("rCompanyID")
            Dim intSupplierID As String = Session("rReportsSupRecon")("rSupplier")
            Dim strItemID As String = Session("rReportsSupRecon")("rItem")
            Dim dtFromDate As DateTime = Session("rReportsSupRecon")("rFromDate")
            Dim dttoDate As DateTime = Session("rReportsSupRecon")("rToDate")

            'CrystalReportViewer1.ParameterFieldInfo.Clear()
            Dim repPath As String = ""
            Dim Filter As String = ""
            lblMsg.Text = ""
            If FromDt.SelectedDate > ToDt.SelectedDate Then
                lblMsg.Text = "From Date shoud be always less than To Date"
                Exit Sub
            End If

            repPath = System.Configuration.ConfigurationManager.AppSettings("ReportPath") & "SupplierReconciliation.rpt" 'Intializing repPath variable to the actual report path

            Dim ConnInfo As New ConnectionInfo
            With ConnInfo 'getting database connection details from web.config
                .ServerName = System.Configuration.ConfigurationManager.AppSettings("server")
                .DatabaseName = System.Configuration.ConfigurationManager.AppSettings("database")
                .UserID = System.Configuration.ConfigurationManager.AppSettings("userid")
                .Password = System.Configuration.ConfigurationManager.AppSettings("password")
            End With

            'Dim paramField As New ParameterField()
            Dim paramFields As New ParameterFields()
            'Dim paramDiscreteValue As New ParameterDiscreteValue()

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


            'paramField = New ParameterField()
            'paramField.Name = "@SupplierCode"
            'paramDiscreteValue = New ParameterDiscreteValue()
            'If txtSupplier.Text <> "" And hdfSupplierID.Value <> "" Then
            '    paramDiscreteValue.Value = hdfSupplierID.Value
            'Else
            '    paramDiscreteValue.Value = ""
            'End If
            'paramField.CurrentValues.Add(paramDiscreteValue)
            'paramFields.Add(paramField)


            Dim path As String = Server.MapPath(repPath)
            rep.Load(path) 'Loading the report by setting report path



            'ds = clsReports.REPSupplierReconciliation(rcbCompany.SelectedItem.Value, FromDt.SelectedDate, ToDt.SelectedDate, hdfSupplierID.Value, objuserInfo("schemaName"))
            ds = clsReports.REPSupplierReconciliation(intCompanyID, dtFromDate, dttoDate, intSupplierID, strItemID, objuserInfo("schemaName"))
            rep.SetDataSource(ds.Tables(0))


            Dim RepTbls As Tables = rep.Database.Tables
            For Each RepTbl As Table In RepTbls
                Dim RepTblLogonInfo As TableLogOnInfo = RepTbl.LogOnInfo
                RepTblLogonInfo.ConnectionInfo = ConnInfo
                RepTbl.ApplyLogOnInfo(RepTblLogonInfo)
            Next

            'rep.DataDefinition.RecordSelectionFormula = Filter 'Filtering the report 
            rep.SummaryInfo.ReportTitle = Session("rReportsSupRecon")("rCompanyName")


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
                htbl.Add("rCompanyID", rcbCompany.SelectedValue)
                If rcbSuppliers.CheckedItems.Count = 0 Then
                    lblMsg.Text = "Supplier is required"
                    Me.CrystalReportViewer1.ReportSource = Nothing
                    CrystalReportViewer1.RefreshReport()
                    ClientScript.RegisterStartupScript(Me.GetType(), "hideOverlay", "hideOverlay();", True)
                    Exit Sub
                Else
                    lblMsg.Text = ""
                End If
                hdfSupplierID.Value = LoopSuppliers()
                hdfItemIDs.Value = LoopItems()

                htbl.Add("rSupplier", hdfSupplierID.Value)
                htbl.Add("rItem", hdfItemIDs.Value)

                htbl.Add("rFromDate", FromDt.SelectedDate)
                htbl.Add("rToDate", ToDt.SelectedDate)
                htbl.Add("rCompanyName", rcbCompany.SelectedItem.Text)
                Session("rReportsSupRecon") = htbl

                BindReport() 'Button click binding the report
             

            End If
            ClientScript.RegisterStartupScript(Me.GetType(), "hideOverlay", "hideOverlay();", True)
        End Sub
        Public Function LoopSuppliers() As String
            Dim strSelectedItem As String = ""
            For Each radcbiSource In rcbSuppliers.CheckedItems
                If rcbSuppliers.CheckedItems.Count = rcbSuppliers.Items.Count Then
                    Return "ALL"
                End If
                If rcbSuppliers.CheckedItems.Count > 0 Then
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

        'Private Sub ddlSupplierCode_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSupplierCode.DataBound
        '    ddlSupplierCode.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        'End Sub

        'Private Sub ObjDSSupplier_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjDSSupplier.Selecting
        '    Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
        '    If Not strCompanyId = "" Then
        '        e.InputParameters("CompanyID") = strCompanyId
        '        e.InputParameters("EmployeeID") = objuserInfo("empID")
        '        e.InputParameters("SchemaName") = objuserInfo("schemaName")
        '    End If
        'End Sub
        Private Sub rcbCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCompany.SelectedIndexChanged
           lblMsg.Text = ""
            'AddContextKey()
            hdfSupplierID.Value = ""
            hdfItemIDs.Value = ""
            'txtSupplier.Text = ""
            If rcbCompany.SelectedIndex > 0 Then
                BindSuppliers()
                BindItems()
                CrystalReportViewer1.ReportSource = Nothing
                CrystalReportViewer1.RefreshReport()
            Else
                hdfSupplierID.Value = ""
                hdfItemIDs.Value = ""
                'txtSupplier.Text = ""
            End If
        End Sub
        'Private Sub txtSupplier_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSupplier.TextChanged
        '    lblMsg.Text = ""
        '    hdfSupplierID.Value = ""
        '    GetSupplierID()
        'End Sub
        'Private Sub GetSupplierID()

        '    If rcbCompany.SelectedIndex > 0 Then
        '        Dim SupplierID As Integer
        '        SupplierID = clsValidations.GetLookUpIDByCode(rcbCompany.SelectedValue, txtSupplier.Text, "S", objuserInfo("schemaName"))

        '        If SupplierID > 0 Then
        '            hdfSupplierID.Value = SupplierID
        '        Else
        '            lblMsg.Text = "Please select the Supplier."
        '            txtSupplier.Focus()
        '        End If
        '    Else
        '        lblMsg.Text = "Please select the  Company."
        '        txtSupplier.Text = ""
        '        hdfSupplierID.Value = ""
        '    End If
        'End Sub
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