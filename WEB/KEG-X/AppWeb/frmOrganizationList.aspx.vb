'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmOrganizationList.aspx
'Created By         :
'File navigation    :
'Created Date       : November 06, 2013, 4:46:38 PM 
'Description        :This file is used to List the Company Records...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Partial Class frmOrganizationList
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

        Private Sub ObjOrganization_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjOrganization.Selecting
            ''Pass the logged in username and retrieve the companys associated for that user
            'Dim strCompanyId = clsTabMOrganization.GetSelectedCompanyID(Me.Master)
            'If Not strCompanyId = "" Then
            'e.InputParameters("CompanyID") = strCompanyId
            'e.InputParameters("SchemaName") = objuserInfo("schemaName")
            'Else
            e.InputParameters("EmployeeID") = objuserrole("employeeID")
            e.InputParameters("SchemaName") = objuserInfo("schemaName")
            'End If

        End Sub
    End Class
End Namespace

