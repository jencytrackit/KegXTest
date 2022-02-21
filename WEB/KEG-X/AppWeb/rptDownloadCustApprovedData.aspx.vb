
'***************************************************************
'Application :Kegs Tracking System
'File Name : frmStockOpeningBalance.aspx
'Created By : 
'File navigation :
'Created Date :  
'Description : This add/edit the records in empty keg
'Modified By :
'Modified Date :
'***************************************************************


Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports System.Data.OleDb
Imports System.IO
Imports System.Web
Imports System.Net
Imports Microsoft.Office
Imports ClosedXML
Imports ClosedXML.Excel
Imports System.Windows.Forms
Namespace TrackITKTS
    Partial Class rptDownloadCustApprovedData
        Inherits System.Web.UI.Page
        Dim dt As New DataTable
        Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging

        Dim ds As New DataSet
        Dim objuserrole As New Hashtable()
        Dim objCommand As OleDbCommand
        Dim objXConn As OleDbConnection


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            Session("rReportsDownloadCustData") = Nothing
            If objuserInfo Is Nothing Then
                Return
            End If
            If Not Page.IsPostBack Then 'filling company dropdown values and setting customerName,CustomerCode dropdowns intial value 
                FromDt.SelectedDate = Now.Date
                hdfItemIDs.Value = ""
                lblMsg.Text = ""
                BindItems()
            End If

        End Sub
        Private Sub BindItems()
            rcbItems.Items.Clear()
            Dim ds As New DataSet
            ds = clsTabItems.GetTabItemsByCompanyID(3, objuserInfo("schemaName"))
            If ds.Tables(0).Rows.Count > 0 Then
                rcbItems.DataSource = ds
                rcbItems.DataTextField = "ItemName"
                rcbItems.DataValueField = "ItemCode"
                rcbItems.DataBind()
            End If

        End Sub

        Private Sub Export2()
            If Session("rReportsDownloadCustData") Is Nothing Then Return
            Dim strItemID As String = Session("rReportsDownloadCustData")("rItem")
            lblMsg.Text = ""
            Try
                If FromDt.SelectedDate.ToString = Nothing Then
                    lblMsg.Text = "Please Select Date"
                    Exit Sub
                End If
            Catch ex As Exception
                Exit Sub
            End Try
            If FromDt.SelectedDate.ToString = Nothing Then
                lblMsg.Text = "Invalid Date Formatt"

                '  MsgBox("Invalid Date")
                Exit Sub
            End If

            Try
                Cursor.Current = Cursors.WaitCursor
                ds = clsTabKEmptyCustomer.GetTabKCustomerDumpApprovedQty(objuserInfo("schemaName"), FromDt.SelectedDate, strItemID, objuserrole("employeeID"))
                If ds.Tables(0).Rows.Count = 0 Then
                    Cursor.Current = Cursors.Default
                    lblMsg.Text = "No Records Found"
                    Exit Sub
                End If
                If Not ds Is Nothing Then
                    dt = ds.Tables(0)

                    Using wb As New XLWorkbook()
                        wb.Worksheets.Add(dt, "Customers")

                        Response.Clear()
                        Response.Buffer = True
                        Response.Charset = ""
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                        Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx")
                        Using MyMemoryStream As New MemoryStream()
                            wb.SaveAs(MyMemoryStream)
                            MyMemoryStream.WriteTo(Response.OutputStream)
                            Response.Flush()
                            Response.End()
                        End Using
                    End Using
                End If
            Catch ex As Exception
                Cursor.Current = Cursors.Default
            End Try

            Cursor.Current = Cursors.Default
        End Sub

        'Private Sub export1()

        '    Dim excel As New Microsoft.Office.Interop.Excel.Application
        '    '  Dim excel As New Microsoft.Office.Interop.Excel.ApplicationClass
        '    Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        '    Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

        '    wBook = excel.Workbooks.Add()
        '    wSheet = wBook.ActiveSheet()

        '    Dim dt As System.Data.DataTable = ds.Tables(0)
        '    Dim dc As System.Data.DataColumn
        '    Dim dr As System.Data.DataRow
        '    Dim colIndex As Integer = 0
        '    Dim rowIndex As Integer = 0

        '    For Each dc In dt.Columns
        '        colIndex = colIndex + 1
        '        excel.Cells(1, colIndex) = dc.ColumnName
        '    Next

        '    For Each dr In dt.Rows
        '        rowIndex = rowIndex + 1
        '        colIndex = 0
        '        For Each dc In dt.Columns
        '            colIndex = colIndex + 1
        '            excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)

        '        Next
        '    Next

        '    wSheet.Columns.AutoFit()
        '    Dim strFileName As String = "D:\ss.xls"
        '    Dim blnFileOpen As Boolean = False
        '    Try
        '        Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
        '        fileTemp.Close()
        '    Catch ex As Exception
        '        blnFileOpen = False
        '    End Try

        '    If System.IO.File.Exists(strFileName) Then
        '        System.IO.File.Delete(strFileName)
        '    End If

        '    wBook.SaveAs(strFileName)
        '    excel.Workbooks.Open(strFileName)
        '    excel.Visible = True
        'End Sub
        Private Sub radSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radSave.Click

            Dim htbl As New Hashtable
            hdfItemIDs.Value = LoopItems()
            htbl.Add("rItem", hdfItemIDs.Value)
            Session("rReportsDownloadCustData") = htbl
            Export2()

        End Sub
        Public Function LoopItems() As String
            Dim strSelectedItem As String = ""
            If rcbItems.CheckedItems.Count = 0 Then
                Return "ALL"
            End If

                For Each radcbiSource In rcbItems.CheckedItems
                    If rcbItems.CheckedItems.Count = rcbItems.Items.Count Then
                        Return "ALL"
                    End If
                    If rcbItems.CheckedItems.Count > 0 Then
                        strSelectedItem += radcbiSource.Value
                        strSelectedItem += ", "
                    End If
                Next

            Return strSelectedItem
        End Function



    End Class
End Namespace
