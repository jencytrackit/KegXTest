'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmTABAuditTrailList.aspx
'Created By         :
'File navigation    :
'Created Date       :November 11, 2013, 4:17:49 PM
'Description        :This file is used to List the Audit Trail Details...
'Modified By        :
'Modified Date      :
'***************************************************************

Imports System
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI

Namespace TrackITKTS
    Partial Class frmTABAuditTrailList
        Inherits System.Web.UI.Page
        Dim objuserrole As New Hashtable()
        Dim objuserInfo As New Hashtable()
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
        End Sub
        
       
        Private Sub ObjDSTABAuditTrail_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjDSTABAuditTrail.Selecting
            e.InputParameters("valEmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")
        End Sub
    End Class
End Namespace
