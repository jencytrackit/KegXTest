Imports System
Imports Telerik.Web.UI


Namespace TrackITKTS
    Partial Class frmAddEditATSUsers
        Inherits System.Web.UI.Page

        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                BindActive()
                rcbEmployeeName.Items.Insert(0, New RadComboBoxItem(Resources.lang.ddlSelect, 0))
                ddlCompanyName.Items.Insert(0, New RadComboBoxItem(Resources.lang.ddlSelect, 0))
                BindEmployee()
                BindCompany()
            End If
        End Sub
        Private Sub BindActive()
            rbtActive.Items.Add(Resources.lang.lblYes)
            rbtActive.Items.Add(Resources.lang.lblNo)
            rbtActive.SelectedIndex = 0
        End Sub

        Private Sub rcbEmployeeName_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles rcbEmployeeName.DataBound
            rcbEmployeeName.Items.Insert(0, New RadComboBoxItem(Resources.lang.ddlSelect, 0))
        End Sub
        Private Sub radCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles radCancel.Click
            Response.Redirect("frmUserList.aspx")
        End Sub
        Private Sub BindEmployee()
            rcbEmployeeName.Items.Insert(1, New RadComboBoxItem("Bharani", 1))
            rcbEmployeeName.Items.Insert(2, New RadComboBoxItem("Suresh", 2))
            rcbEmployeeName.Items.Insert(3, New RadComboBoxItem("Vamsee", 3))
            rcbEmployeeName.Items.Insert(4, New RadComboBoxItem("Yasaswini", 4))
            rcbEmployeeName.Items.Insert(5, New RadComboBoxItem("Jagan", 5))
        End Sub

        Private Sub ddlCompanyName_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompanyName.DataBound
            ddlCompanyName.Items.Insert(0, New RadComboBoxItem(Resources.lang.ddlSelect, 0))
        End Sub
        Private Sub BindCompany()
            ddlCompanyName.Items.Insert(1, New RadComboBoxItem("TrackIT", 1))
            ddlCompanyName.Items.Insert(2, New RadComboBoxItem("African+eastern", 2))
            
        End Sub
    End Class
End Namespace
