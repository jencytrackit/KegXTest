
'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmBranchPlantList.aspx
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
    Public Class frmBranchPlantList
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

        Private Sub ObjBranchPlant_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjBranchPlant.Selecting
            'Pass the logged in employeeid and retrieve the branch plants details associated to the logged in user and company
  
            e.InputParameters("CompanyID") = 0
            e.InputParameters("EmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")

        End Sub

        Private Sub RadgvBranchPlant_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadgvBranchPlant.ItemCommand
            If e.CommandName = "batch" Then
                Dim item = DirectCast(e.Item, GridDataItem)
                Dim lnkbatch As LinkButton = DirectCast(item.FindControl("lnkbatch"), LinkButton)
                Dim lblBranchID As Label = DirectCast(item.FindControl("lblBranchID"), Label)
                Dim Strbatch As Boolean
                If lnkbatch.Text = "True" Then
                    Strbatch = False
                Else
                    Strbatch = True
                End If
                Dim ret = clsTabMBranchPlant.UpdateTabMBranchPlant_Batch(lblBranchID.Text, Strbatch, objuserInfo("schemaName"))
                If ret Then
                    RadgvBranchPlant.DataBind()
                End If
            End If
        End Sub
    End Class
End Namespace
