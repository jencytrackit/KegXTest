'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmItemMasterList.aspx
'Created By         :
'File navigation    :
'Created Date       :February 06, 2014, 11:09:14 AM
'Description        :This file is used to List the Item Details...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Public Class frmItemMasterList
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

        Private Sub ObjItemMaster_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjItemMaster.Selecting
            'Pass the companyid and retrieve the Inventory details for the company
       
            e.InputParameters("EmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")

        End Sub
    End Class
End Namespace
