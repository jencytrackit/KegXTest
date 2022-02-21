Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Public Class frmEmptyKegReceipt_Batch
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            BindGrid()
        End Sub
        Private Sub BindGrid()
            Dim table As New DataTable
            table.Columns.Add("CompanyName", GetType(String))
            table.Columns.Add("BranchPlantCode", GetType(String))
            table.Columns.Add("BranchPlant", GetType(String))
            table.Columns.Add("CustomerName", GetType(String))
            table.Columns.Add("ItemName", GetType(String))
            table.Columns.Add("Barcode", GetType(String))
            table.Columns.Add("Quantity", GetType(String))
            table.Columns.Add("UOM", GetType(String))
            table.Columns.Add("Date", GetType(String))

            table.Rows.Add("African+Eastern", "BP-003", "Branch Plant 3", "Customer 1", "Item1", "10000001", "1", "25ltrs", "11/08/2013")
            table.Rows.Add("African+Eastern", "BP-001", "Branch Plant 1", "Customer 1", "Item2", "10000002", "2", "25ltrs", "11/08/2013")
            table.Rows.Add("African+Eastern", "BP-002", "Branch Plant 2", "Customer 2", "Item3", "10000003", "1", "25ltrs", "11/05/2013")
            table.Rows.Add("African+Eastern", "BP-002", "Branch Plant 2", "Customer 1", "Item4", "10000004", "1", "50ltrs", "11/07/2013")
            RadgvEmptyKeg.DataSource = table
        End Sub
    End Class
End Namespace