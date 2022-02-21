'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmInventoryList.aspx
'Created By         :
'File navigation    :
'Created Date       :November 08, 2013, 10:54:54 AM 
'Description        :This file is used to List the Inventory Details...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Public Class frmInventoryList
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim objuserrole As New Hashtable()
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
        End Sub
        'Public Sub ddlMasterPageSelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
        '    'Get the Inventory details according to the company dropdown changing
        '    RadgvInventory.DataBind()
        'End Sub
        Private Sub ObjInventory_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjInventory.Selecting
            'Pass the companyid and retrieve the Inventory details for the company
         
            e.InputParameters("EmployeeID") = objuserrole("employeeID")
                e.InputParameters("SchemaName") = objuserInfo("schemaName")

        End Sub
    End Class
End Namespace