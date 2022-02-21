
'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmReportRequestsX.aspx
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
Namespace TrackITKTS
    Public Class frmReportRequestsX
        Inherits System.Web.UI.Page
        Dim objuserrole As New Hashtable()
        Dim objuserInfo As New Hashtable()
        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
        End Sub
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If objuserInfo Is Nothing Then
                Return
            End If
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
                Dim RequestName As String = e.Item.Cells(2).Text
                DownloadFile(RequestName & "_" & lblRequestID.Text & ".xls")
            ElseIf e.CommandName = "PDFDownload" Then
                Dim item = DirectCast(e.Item, GridDataItem)
                Dim lnkbatch As LinkButton = DirectCast(item.FindControl("lnkPDFDownload"), LinkButton)
                Dim lblRequestID As Label = DirectCast(item.FindControl("lblRequestID"), Label)
                Dim RequestName As String = e.Item.Cells(2).Text
                DownloadFile(RequestName & "_" & lblRequestID.Text & ".pdf")
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
    End Class
End Namespace
