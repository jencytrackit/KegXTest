Imports Telerik.Web.UI
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Namespace TrackITKTS
    Public Class MasterPage
        Inherits System.Web.UI.MasterPage
        Dim objUserDetails As New Hashtable()
        Dim objUserRoleDetails As New Hashtable()
        'Dim strMenuImg() As String = {"dash", "tran", "process", "repoprts", "profile", "sign-out"}
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            objUserDetails = Session("UserDetails")
            objUserRoleDetails = Session("UserRoleDetails")
            If Not IsPostBack Then
                If Not objUserDetails Is Nothing Then
                    If Convert.ToString(objUserDetails("employeeNo")).ToUpper() = "PARTNER" Then
                        BindMenuNewPurchase()
                        'rcbCompany.Visible = False
                        'lblCompany.Visible = False
                    Else
                        If objUserDetails("isOrganisation") = "1" And Convert.ToString(objUserDetails("userName")).ToUpper() = "SUPERADMIN" Then
                            'rcbCompany.Visible = False
                            'lblCompany.Visible = False
                        Else
                            'BindCompany()
                            'rcbCompany.SelectedValue = Session("SelectedCompanyID")
                            'rcbCompany.Visible = True
                            'lblCompany.Visible = True
                        End If
                        BindMenuNew()
                    End If
                   
                End If

            End If
        End Sub
        'Private Sub BindCompany()           
        '    rcbCompany.DataSource = clsTabMOrganization.GetAllTabMOrganizationByEmployeeID(objUserRoleDetails("employeeID"), objUserDetails("schemaName"))
        '    rcbCompany.DataTextField = "CompanyName"
        '    rcbCompany.DataValueField = "CompanyID"
        '    rcbCompany.DataBind()
        '    'rcbCompany.Items.Insert(0, New RadComboBoxItem("--Select One--"))
        'End Sub

        'Private Sub rcbCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCompany.SelectedIndexChanged
        '    Session("SelectedCompanyID") = rcbCompany.SelectedValue
        '    If objUserDetails("isOrganisation") <> "1" Then
        '        Dim objUserPrivileges As New Hashtable()
        '        objUserPrivileges = clsTabMEmployees.GetAllTABUserPrivilegesByEmployeeID(objUserRoleDetails("employeeID"), rcbCompany.SelectedValue, 0, objUserDetails("schemaName"))
        '        Session("UserPrivileges") = objUserPrivileges 'Store the logged in user privileges
        '    End If

        '    Response.Redirect("frmHome.aspx")
        'End Sub

        Private Sub BindMenuNewPurchase()
            
            'Dim idx As Integer = 0
            Dim strMenu As String = ""
            strMenu &= "<li><a href='../AppWeb/frmHome.aspx' class='dash'><span class='dashboard'>&nbsp;</span>Home</a></li>" & ControlChars.CrLf
            strMenu &= "<li><a href='#' class='tran'><span>&nbsp;</span>Transactions</a>" & ControlChars.CrLf & "<ul class=""sub-menu"">" & ControlChars.CrLf
            strMenu &= "<li><a href='../AppWeb/frmKegPurchaseModule_SupToCus.aspx'>Keg Purchase Module</a></li>" & ControlChars.CrLf
            strMenu &= "</ul>" & ControlChars.CrLf & "</li>" & ControlChars.CrLf
            strMenu &= "<li><a href='../frmLogout.aspx' class='sign-out'><span>&nbsp;</span>Logout</a></li>" & ControlChars.CrLf
            'strMenu &= "<li><a href='../frmLogOut.aspx' class='sign-out'><span>&nbsp;</span>Logout</a></li>" & ControlChars.CrLf

            ltMenu.Text = strMenu

        End Sub
        Private Sub BindMenuNew()
            Dim ds As New DataSet
            Dim ds1 As New DataSet
            If Not objUserDetails Is Nothing Then
                If objUserDetails("isOrganisation") = "1" Then
                    Dim strOrgIds As String = objUserRoleDetails("organizationIDS")
                    Dim OrgIds() As String = Split(strOrgIds, ",")
                    ds = clsTabMEmployees.GetAllTABUserPrivilegesByEmployeeID_Menu(objUserRoleDetails("employeeID"), 0, OrgIds(0), objUserDetails("schemaName"))
                Else
                    ' ds = clsTabMEmployees.GetAllTABUserPrivilegesByEmployeeID_Menu(objUserRoleDetails("employeeID"), rcbCompany.SelectedValue, 0, objUserDetails("schemaName"))
                    ds = clsTabMEmployees.GetAllTABUserPrivilegesByEmployeeID_Menu(objUserRoleDetails("employeeID"), 0, 0, objUserDetails("schemaName"))
                End If
            End If
            ds1 = ds
            For Each rw As DataRow In ds.Tables(0).Rows
                If IsDBNull(rw.Item("ParentID")) Then
                    'Add to list
                    Dim m As New Menu
                    m.ID = rw.Item("MenuID")
                    m.Text = rw.Item("Feature_en")
                    If Not IsDBNull(rw.Item("Description")) Then
                        m.cssDesc = rw.Item("Description")
                    Else
                        m.cssDesc = ""
                    End If
                    If Not IsDBNull(rw.Item("NavigateUrl")) Then
                        m.URL = rw.Item("NavigateUrl")
                    End If
                    arrMenu.Add(m)
                Else
                    For Each p As Menu In arrMenu
                        'sub items
                        If p.ID = rw.Item("ParentID") Then
                            'Add to list
                            Dim m As New Menu
                            m.ID = rw.Item("MenuID")
                            m.Text = rw.Item("Feature_en")
                            If Not IsDBNull(rw.Item("Description")) Then
                                m.cssDesc = rw.Item("Description")
                            Else
                                m.cssDesc = ""
                            End If
                            If Not IsDBNull(rw.Item("NavigateUrl")) Then
                                m.URL = rw.Item("NavigateUrl")
                            End If
                            'sub sub items
                            For Each rw1 As DataRow In ds1.Tables(0).Rows
                                If Not IsDBNull(rw1.Item("ParentID")) Then
                                    If m.ID = rw1.Item("ParentID") Then
                                        Dim m1 As New Menu
                                        m1.ID = rw1.Item("MenuID")
                                        m1.Text = rw1.Item("Feature_en")
                                        If Not IsDBNull(rw1.Item("Description")) Then
                                            m1.cssDesc = rw1.Item("Description")
                                        Else
                                            m1.cssDesc = ""
                                        End If
                                        If Not IsDBNull(rw1.Item("NavigateUrl")) Then
                                            m1.URL = rw1.Item("NavigateUrl")
                                        End If
                                        m.SubItems.Add(m1)
                                    End If
                                End If
                            Next
                            p.SubItems.Add(m)
                        End If
                    Next
                End If
            Next
            'Dim idx As Integer = 0
            Dim strMenu As String = ""
            For Each p As Menu In arrMenu
                'If idx > strMenuImg.Length - 1 Then
                '    idx = strMenuImg.Length - 1
                'End If
                If p.SubItems.Count = 0 Then
                    If p.Text.Trim.Equals("Home") Then
                        strMenu &= "<li><a href=""" & p.URL.Replace("~/AppWeb/", "") & """ class=""" & p.cssDesc & """><span class='dashboard'>&nbsp;</span>" & p.Text & "</a></li>" & ControlChars.CrLf
                    ElseIf p.cssDesc.Trim.Equals("sign-out") Then
                        strMenu &= "<li><a href=""" & p.URL.Replace("~/", "../") & """ class=""" & p.cssDesc & """><span>&nbsp;</span>" & p.Text & "</a></li>" & ControlChars.CrLf
                    ElseIf p.URL.Trim.Length > 0 Then
                        strMenu &= "<li><a href=""" & p.URL.Replace("~/AppWeb/", "") & """ class=""" & p.cssDesc & """><span>&nbsp;</span>" & p.Text & "</a></li>" & ControlChars.CrLf
                    Else
                        strMenu &= "<li><a href=""#"" class=""" & p.cssDesc & """><span>&nbsp;</span>" & p.Text & "</a></li>" & ControlChars.CrLf
                    End If
                Else
                    strMenu &= "<li><a href=""" & p.URL.Replace("~/AppWeb/", "") & """ class=""" & p.cssDesc & """><span>&nbsp;</span>" & p.Text & "</a>" & ControlChars.CrLf
                    strMenu &= "<ul class=""sub-menu"">" & ControlChars.CrLf
                    For Each m As Menu In p.SubItems
                        If m.SubItems.Count > 0 Then
                            strMenu &= "<li><a href=""" & m.URL.Replace("~/AppWeb/", "") & """ class=""" & m.cssDesc & """><span>&nbsp;</span>" & m.Text & "</a>" & ControlChars.CrLf
                            strMenu &= "<ul class=""sub-sub-menu"">" & ControlChars.CrLf
                            For Each m1 As Menu In m.SubItems
                                strMenu &= "<li><a href=""" & m1.URL.Replace("~/AppWeb/", "") & """>" & m1.Text & "</a></li>" & ControlChars.CrLf
                            Next
                            strMenu &= "</ul>" & ControlChars.CrLf & "</li>" & ControlChars.CrLf
                        Else
                            strMenu &= "<li><a href=""" & m.URL.Replace("~/AppWeb/", "") & """>" & m.Text & "</a></li>" & ControlChars.CrLf
                        End If
                    Next
                    strMenu &= "</ul>" & ControlChars.CrLf & "</li>" & ControlChars.CrLf
                End If
                'idx += 1
            Next
            ltMenu.Text = strMenu
        End Sub
        Dim arrMenu As New List(Of Menu)
        Dim arrMenuSub As New List(Of Menu)
    End Class
    Public Class Menu
        Public ID As Integer = 0
        Public Text As String = ""
        Public cssDesc As String = ""
        Public URL As String = "#"
        Public SubItems As New List(Of Menu)
    End Class
End Namespace

