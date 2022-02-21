<%@ Control Language="VB" AutoEventWireup="false" Inherits="KEG_X.MyMessageBox" Codebehind="MyMessageBox.ascx.vb" %>

<div class="container">
    <asp:Panel ID="MessageBox" runat="server">
        <asp:HyperLink runat="server" id="CloseButton" >
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/close.png" AlternateText="Click here to close this message" />
        </asp:HyperLink>
        <p>
            <asp:Literal ID="litMessage" runat="server"></asp:Literal></p>
    </asp:Panel>
</div>