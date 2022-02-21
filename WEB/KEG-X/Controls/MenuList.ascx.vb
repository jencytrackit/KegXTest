
Namespace TrackITKTS
    Partial Class Controls_MenuList
        Inherits System.Web.UI.UserControl
        Dim objuserInfo As New Hashtable() '// user info maintained in the Session after logging

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'objuserInfo = Session("UserDetails")
            'Dim ds As DataSet = clsTabMEmployees.GetAllTABUserPrivilegesByEmployeeID_Menu(objuserInfo("empID"))
            'RadMenu1.DataSource = ds
            'RadMenu1.DataFieldID = "MenuID"
            'RadMenu1.DataFieldParentID = "ParentID"
            'RadMenu1.DataTextField = "Feature_en"
            'RadMenu1.DataNavigateUrlField = "NavigateUrl"
            'RadMenu1.DataBind()

        End Sub

        
    End Class
End Namespace