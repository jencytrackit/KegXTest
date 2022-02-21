Imports Telerik.Web.UI

'***************************************************************
'Application        :Kegs Tracking System
'File Name          :frmOrganisationMasterList.aspx
'Created By         :
'File navigation    :
'Created Date       : January 17, 2014, 2:08:31 PM
'Description        : This file is used to List the Organizations Records...
'Modified By        :
'Modified Date      :
'***************************************************************
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Public Class frmOrganisationMasterList
        Inherits System.Web.UI.Page
        Dim objuserInfo As New Hashtable()
        Dim userPrivileges As New Hashtable
        Dim objuserrole As New Hashtable()
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objuserInfo = Session("UserDetails") '// user info maintained in the Session after logging
            userPrivileges = Session("UserPrivileges") '// user Privileges maintained in the Session after logging
            objuserrole = Session("UserRoleDetails") '//Logged in schema user details maintained in the session
            If objuserInfo Is Nothing Then
                Return
            End If
            If Not Page.IsPostBack Then
                'if the query string values for process or mode exists then the control is coming from the add or edit page...
                Dim strVal As String = Request.QueryString("process")
                Dim strMOde As String = Request.QueryString("mode")
                Select Case strMOde
                    Case "D"
                        If Trim(strVal) = "Y" Then  '// chekcing for the success or failure of the process
                            lblMsg.Text = Resources.lang.Deleted
                        End If
                    Case "E"
                        If Trim(strVal) = "Y" Then  '// chekcing for the success or failure of the process
                            lblMsg.Text = Resources.lang.Updated
                        End If
                    Case "A"
                        If Trim(strVal) = "Y" Then  '// chekcing for the success or failure of the process
                            lblMsg.Text = Resources.lang.Inserted
                        End If
                    Case "F"
                        lblMsg.Text = Resources.lang.NotFound
                    Case "IA"  '//Checking the invalid access page from master page load....

                        lblMsg.Text = "Invalid Access..."
                End Select
                implementPrivilegesAdd()
            End If
        End Sub

        Private Sub ObjOrganization_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjOrganization.Selecting
            'Pass the logged in username and retrieve the companys associated for that user
            If Not objuserInfo Is Nothing Then
                If Convert.ToString(objuserInfo("userName")).ToUpper() = "SUPERADMIN" Then
                    e.InputParameters("EmployeeID") = 0
                    e.InputParameters("SchemaName") = objuserInfo("schemaName")
                Else
                    e.InputParameters("EmployeeID") = objuserrole("employeeID")
                    e.InputParameters("SchemaName") = objuserInfo("schemaName")
                End If

            End If
        End Sub
        Private Sub implementPrivilegesAdd()
            'This method is called to display the add new record button based on the logged in user privileges
            Dim urlOrg As String = Request.Url.ToString()
            Dim pos As Int32 = urlOrg.LastIndexOf("/")
            urlOrg = urlOrg.Substring(pos, urlOrg.Length - pos)
            pos = urlOrg.IndexOf("?")
            If pos > 0 Then
                urlOrg = urlOrg.Substring(0, pos)
            End If
            urlOrg = System.Configuration.ConfigurationManager.AppSettings("ApplicationURL") & urlOrg

            If userPrivileges.Contains(urlOrg) Then
                Dim priv As String = userPrivileges(urlOrg)
                Dim privileges() As String = priv.Split(",")
                If privileges(1).Equals("0") Then
                    'ImgBtn1.Visible = False
                    RadButton1.Visible = False
                End If
            End If
        End Sub

        Private Sub RadgvOrganisation_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadgvOrganisation.ItemCommand
            If e.CommandName = "edit" Then
                'Edit is selected from the grid. Call the edit aspx page by passing primary key value...
                Dim item = DirectCast(e.Item, GridDataItem)
                Dim OrganizationID As Integer = Int32.Parse(item.GetDataKeyValue("OrganizationID").ToString())
                Response.Redirect("frmAddEditOrganization.aspx?id=" + OrganizationID.ToString)
            End If
        End Sub
        Private Sub implementPrivileges(ByVal showCell As ImageButton, ByVal type As String)
            'This method is called to display the edit button in grid based on the user privileges
            Dim urlOrg As String = Request.Url.ToString()
            Dim pos As Int32 = urlOrg.LastIndexOf("/")
            urlOrg = urlOrg.Substring(pos, urlOrg.Length - pos)
            pos = urlOrg.IndexOf("?")
            If pos > 0 Then
                urlOrg = urlOrg.Substring(0, pos)
            End If
            urlOrg = System.Configuration.ConfigurationManager.AppSettings("ApplicationURL") & urlOrg

            If userPrivileges.Contains(urlOrg) Then
                Dim priv As String = userPrivileges(urlOrg)
                Dim privileges() As String = priv.Split(",")
                If privileges(2).Equals("0") And type.Equals("E") Then
                    showCell.Visible = False
                End If
            End If
        End Sub
        Private Sub RadgvOrganisation_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadgvOrganisation.ItemDataBound
            If TypeOf e.Item Is GridDataItem Then
                'Find the edit button from grid and check for user privileges to display or not...
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                implementPrivileges(CType(dataItem.FindControl("EditButton"), ImageButton), "E")
            End If
        End Sub
    End Class
End Namespace
