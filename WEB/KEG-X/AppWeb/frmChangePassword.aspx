<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmChangePassword.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmChangePassword" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">
        function OnClientClicked(button, args) {
            document.forms[0].reset();
            button.set_autoPostBack(false);
        }

    </script>
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <h2><asp:Label ID="addATSUsers" Text="<%$ Resources:lang, lblChangePassword %>" runat="server" /></h2>
    <em>* Required Fields</em>
                    <table id="Table1" cellspacing="0" cellpadding="0" width="70%" align="left" border="0"
                        bordercolor='#ccddef'  rules='none'
                        class="table" runat="server" height="1%">
                        <tr>
                            <td bgcolor='white' colspan="2">
                                <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblUserName" Text="<%$ Resources:lang, lblOldPassword %>" runat="server" />*
                            </td>
                            <td class='sideHead1'>
                                <asp:TextBox ID="txtOldPassWord" MaxLength="50" size="20" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblPassword" Text="<%$ Resources:lang, lblNewPassword %>" runat="server" />*
                            </td>
                            <td class='sideHead1'>
                                <asp:TextBox ID='txtNewPass' TextMode='Password' MaxLength='20' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="Label1" Text="<%$ Resources:lang, lblCPassword %>" runat="server" />*
                            </td>
                            <td class='sideHead1'>
                                <asp:TextBox ID='txtConfPass' TextMode='Password' MaxLength='20' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" bgcolor="#ffffff" colspan="2"><br />
                                <telerik:RadButton ID="radSave"  HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Width="100px" Text="<%$ Resources:lang, btnSave %>"
                                    runat="server" >       </telerik:RadButton>
                                &nbsp;&nbsp;
                                <telerik:RadButton ID="radClear" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Width="100px" Text="<%$ Resources:lang, btnClear %>"
                                    runat="server" AutoPostBack="true" OnClientClicked="OnClientClicked" CausesValidation="False" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="oldpass" runat="server" Display="None" ControlToValidate="txtOldPassWord"
                        ErrorMessage="<%$ Resources:lang, rfvOPassword %>"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="oldExp" runat="server" Display="None" ControlToValidate="txtOldPassWord"
                        ErrorMessage="<%$ Resources:lang, valPasswordLength %>" ValidationExpression="^.{6,50}$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="newpass" runat="server" Display="None" ControlToValidate="txtNewPass"
                        ErrorMessage="<%$ Resources:lang, rfvNPassword %>"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="newExp" runat="server" Display="None" ControlToValidate="txtNewPass"
                        ErrorMessage="<%$ Resources:lang, valPasswordLength %>" ValidationExpression="^.{6,50}$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="confPass" runat="server" Display="None" ControlToValidate="txtConfPass"
                        ErrorMessage="<%$ Resources:lang, rfvCPassword %>"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="confExp" runat="server" Display="None" ControlToValidate="txtConfPass"
                        ErrorMessage="<%$ Resources:lang, valPasswordLength %>" ValidationExpression="^.{6,50}$"></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="newconf" runat="server" Display="None" ControlToValidate="txtNewPass"
                        ControlToCompare="txtConfPass" ErrorMessage="<%$ Resources:lang, valComparePassword %>"
                        Type="String"></asp:CompareValidator>
                    <asp:ValidationSummary ID="Validationsummary1" runat="server" ShowSummary="False"
                        ShowMessageBox="True" HeaderText="<%$ Resources:lang, Errors %>"></asp:ValidationSummary>
           
</asp:Content>
