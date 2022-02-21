Public Class GenericErrorPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request.QueryString("ermsg") Is Nothing Then
            ltrMsg.Text = Request.QueryString("ermsg")
        End If
    End Sub

End Class