'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmFullKegTransferList_BptoBP.aspx
'Created By         :
'File navigation    :
'Created Date       :November 08, 2013, 2:46:43 PM
'Description        :This file is used to List the Full Kegs transfer details that comes from BP to BP...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmFullKegTransferList_BPtoBP
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

        Private Sub ObjFullKegTransferBP_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjFullKegTransferBP.Selecting
            'Pass the companyid and retrieve the Full Kegs Details for that company
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            'If Not strCompanyId = "" Then
            e.InputParameters("EmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")
            'Else
            'e.InputParameters("EmployeeID") = 0
            'e.InputParameters("SchemaName") = objuserInfo("schemaName")
            'End If
        End Sub
    End Class
End Namespace