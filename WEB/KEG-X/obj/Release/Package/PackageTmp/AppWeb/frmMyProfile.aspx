<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmMyProfile.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmMyProfile" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">
        function OnlyNumbers(e) {
            e = e || window.event;
            ch = e.which || e.keyCode;
            if (ch != null) {

                if ((ch >= 48 && ch <= 57) || ch == 0 || ch == 8 || ch == 13 || ch == 9 || ch == 43 || ch == 45)
                    return true;
            }

            return false;
        }
        function OnClientClicked(button, args) {
            document.forms[0].reset();
            button.set_autoPostBack(false);

        }
    </script>
    <%--<ATS:Session ID="chkSession" runat="Server" />--%>
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <h2><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:lang, addATSEmployees %>" /></h2>
    <em>* Required Fields</em>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Textbox"
        EnableRoundedCorners="true" />
    <table id="TABLE21" cellspacing="0" cellpadding="0" width="100%" border="0"  runat="server">
        <tr>
            <td style="border: 0px solid #9BAFC1;">
                <div class="tabledata">
                    <table id="TABLE1" bordercolor='#ccddef' height="1%" cellspacing='0' class="table" rules='none' width="70%" border='0' 
                        runat="server">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblEmployeeName" Text="<%$ Resources:lang, lblEmployeeName %>" runat="server" />*&nbsp;&nbsp;
                            </td>
                            <td class='sideHead1'>
                                <asp:TextBox ID='txtEmployeeName' Width="195px" MaxLength='50' runat='server' />
                                <asp:RequiredFieldValidator ID='rfvEmployeeName' ControlToValidate='txtEmployeeName'
                                    ErrorMessage="<%$ Resources:lang, rfvEmployeeName %>" Display='None' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblEmployeeNo" Text="<%$ Resources:lang, lblEmployeeNo %>" runat="server" />
                            </td>
                            <td class='sideHead1'>
                                <asp:TextBox ID='txtEmployeeNo' Width="195px" MaxLength='50' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblPosition" Text="<%$ Resources:lang, lblPosition %>" runat="server" />
                            </td>
                            <td class='sideHead1'>
                                <asp:TextBox ID='txtPosition' Width="195px" MaxLength='50' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblPhone" Text="<%$ Resources:lang, lblPhone %>" runat="server" />
                            </td>
                            <td class='sideHead1'>
                                <asp:TextBox ID='txtPhone' Width="195px" MaxLength='50' runat='server' onkeyPress="return OnlyNumbers(event);" />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblEmail" Text="<%$ Resources:lang, lblEmail %>" runat="server" />*&nbsp;&nbsp;
                            </td>
                            <td class='sideHead1'>
                                <asp:TextBox ID='txtEmail' Width="195px" MaxLength='50' runat='server' />
                                <asp:RequiredFieldValidator ID='rfvEmail' ControlToValidate='txtEmail' ErrorMessage="<%$ Resources:lang, rfvEmail %>"
                                    Display='None' runat='server' />
                                <asp:RegularExpressionValidator ID='valEmailAddress' ControlToValidate='txtEmail'
                                    ValidationExpression=".*@.*\..*" ErrorMessage="<%$ Resources:lang, valEmailAddress %>"
                                    Display='None' EnableClientScript='true' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblComments" Text="<%$ Resources:lang, lblComments %>" runat="server" />
                            </td>
                            <td class='sideHead1'>
                                <asp:TextBox ID='txtComments' Width="195px" MaxLength='50' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblRoles" Text="<%$ Resources:lang, lblRole %>" runat="server" />
                            </td>
                            <td class='sideHead1'>
                                <asp:Label ID="lblLabel1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblCompanyAss" Text="Companies Associated" runat="server" />
                            </td>
                            <td class='sideHead1'>
                                <telerik:RadListBox ID="lstBox" Width="250px" runat="server" CheckBoxes="true" Enabled="false">
                                </telerik:RadListBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" bgcolor="#ffffff" colspan="2"><br />
                                <telerik:RadButton ID="radSave" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Width="100px" Text="<%$ Resources:lang, btnSave %>"
                                    runat="server" />
                                &nbsp;&nbsp;
                                <telerik:RadButton ID="radClear" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Width="100px" Text="<%$ Resources:lang, btnClear %>"
                                    runat="server" OnClientClicked="OnClientClicked" CausesValidation="False" />
                                     &nbsp;&nbsp;
                                <telerik:RadButton ID="btnCancel" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Width="100px" Text="<%$ Resources:lang, btnCancel %>"
                                    runat="server"  CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID='summary' runat='server' ShowSummary='False' ShowMessageBox='True'
                        HeaderText="<%$ Resources:lang, Errors %>"></asp:ValidationSummary>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
