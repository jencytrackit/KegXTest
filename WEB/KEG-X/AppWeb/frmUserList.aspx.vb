Namespace TrackITKTS
    Partial Class frmUserList
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            BindGrid()
        End Sub
        Private Sub BindGrid()
            Dim table As New DataTable
            table.Columns.Add("UserName", GetType(String))
            table.Columns.Add("CompanyName", GetType(String))
            table.Columns.Add("EmployeeName", GetType(String))
            table.Columns.Add("Role", GetType(String))
            table.Columns.Add("Logged", GetType(String))
            table.Columns.Add("Disable")
            table.Columns.Add("Edit")
            table.Columns.Add("Delete")

            table.Rows.Add("Admin", "African+eastern", "Employee1", "Administrator", "yes", "True", "", "")
            table.Rows.Add("User2", "African+eastern", "Employee2", "User", "no", "False", "", "")
            table.Rows.Add("User3", "African+eastern", "Employee3", "UserHHT", "no", "False", "", "")

            RadUserList.DataSource = table
        End Sub

    End Class
End Namespace