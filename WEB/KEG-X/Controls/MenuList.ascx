<%@ Control Language="VB" AutoEventWireup="false" Inherits="KEG_X.TrackITKTS.Controls_MenuList"
    CodeBehind="MenuList.ascx.vb" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script language="vb" runat="server">
    Dim struserName As String
    Dim strPass As String
    Dim objUserinfo As New Hashtable()
    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        objUserinfo = Session("ATS_userDetails")
        If Not objUserinfo Is Nothing Then
            struserName = objUserinfo("userName")
            strPass = Session("passwd")
        End If
    End Sub
</script>
<div id="Div1"  runat="server">
    
    
    <telerik:radmenu id="RadMenu1" runat="server" Width="100%" ClickToOpen="true" Skin="WebBlue" Style="z-index: 1000;">
                  
                </telerik:radmenu>
          
</div>
