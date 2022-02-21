<%@ Page Language="vb" MasterPageFile="~/Templates/MasterPage.Master" AutoEventWireup="false"
    Inherits="KEG_X.TrackITKTS.frmAddEditATSUsers" Title="Add Users" CodeBehind="frmAddEditATSUsers.aspx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <%--  <ATS:Session ID="chkSession" runat="Server" />--%>
    <form id="frmATS" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <script language="javascript" type="text/javascript">
        function redirectme(sender, args) {
            location.href = 'frmUserList.aspx';
        }
        function OnClientClicked(button, args) {
            document.forms[0].reset();
            button.set_autoPostBack(false);
        }
    </script>
    <table id="TABLE1" cellspacing="0" cellpadding="0" height="450px" width="100%" border="0"
         runat="server">
        <tr>
            <td height="450px" valign="Top">
                <ATSMenu:Menu ID="MENULIST" runat="Server" />
                <div>
                    <br />
                    <br />
                    <table id="TABLE2" align='center' bordercolor='#ccddef' height="1%" cellspacing='0'
                        class="table" rules='none' width="80%" border='1' 
                        runat="server">
                        <tr valign='top' height="5%">
                            <td class='callOutStyle' colspan="6">
                                <asp:Label ID="addATSUsers" Text="<%$ Resources:lang, addATSUsers %>" runat="server" />
                            </td>
                        </tr>
                       <tr>
                            <td bgcolor='white' valign="top" colspan="6">
                                <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label><asp:Label ID='txtMsg'
                                    runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblUserName" Text="<%$ Resources:lang, lblUserName %>" runat="server" />
                            </td>
                            <td class='MandatoryField'>
                                *
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID='radUserName' Width="195px" MaxLength='20' runat='server' />
                                <telerik:RadTextBox ID='radOldUserName' MaxLength='20' runat='server' Visible="false" />
                                <asp:RequiredFieldValidator ID='rfvUserName' ControlToValidate='radUserName' ErrorMessage="<%$ Resources:lang, rfvUserName %>"
                                    Display='None' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblCompanyName" Text="<%$ Resources:lang, gridCompanyName %>" runat="server" />
                            </td>
                            <td class='MandatoryField'>
                                *
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadComboBox ID="ddlCompanyName" runat="server" Filter="Contains" Width="195px">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator3' ControlToValidate='ddlCompanyName'
                                    InitialValue="<%$ Resources:lang, ddlSelect %>" ErrorMessage="<%$ Resources:lang, rfvCompanyName %>"
                                    Display='None' runat='server' />
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblEmployeeName" Text="<%$ Resources:lang, lblEmployeeName %>" runat="server" />
                            </td>
                            <td class='MandatoryField'>
                                *
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadComboBox ID="rcbEmployeeName" runat="server" Filter="Contains" Width="195px">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='rfvEmployee' ControlToValidate='rcbEmployeeName'
                                    InitialValue="<%$ Resources:lang, ddlSelect %>" ErrorMessage="<%$ Resources:lang, rfvEmployee %>"
                                    Display='None' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblPassword" Text="<%$ Resources:lang, lblPassword %>" runat="server" />
                            </td>
                            <td class='MandatoryField'>
                                *
                            </td>
                            <td class='sideHead1'>
                                <telerik:RadTextBox ID='radPassword' Width="195px" TextMode='Password' MaxLength='20'
                                    runat='server' />
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator1' ControlToValidate='radPassword'
                                    ErrorMessage="<%$ Resources:lang, rfvPassword %>" Display='None' runat='server' />
                            </td>
                            <td class='sideHead'>
                                <asp:Label ID="Label1" Text="<%$ Resources:lang, lblCPassword %>" runat="server" />
                            </td>
                            <td class='MandatoryField'>
                                *
                            </td>
                            <td class='sideHead1'>
                                <telerik:RadTextBox ID='radCPassword' Width="195px" TextMode='Password' MaxLength='20'
                                    runat='server' />
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator2' ControlToValidate='radCPassword'
                                    ErrorMessage="<%$ Resources:lang, rfvCPassword %>" Display='None' runat='server' />
                                <asp:CompareValidator ID='valComparePassword' ControlToValidate='radCPassword' ErrorMessage="<%$ Resources:lang, valComparePassword %>"
                                    ControlToCompare='radPassword' Display='None' EnableClientScript='true' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblRole" Text="<%$ Resources:lang, lblRoleName %>" runat="server" />
                            </td>
                            <td class='MandatoryField'>
                                *
                            </td>
                            <td class='sideHead1'>
                                <%--<telerik:RadComboBox ID="rcbRoles" runat="server" Filter="Contains" Width="195px"></telerik:RadComboBox>--%>
                                <telerik:RadListBox ID="rlbRoles" runat="server" CheckBoxes="True" Width="195px">
                                    <Items>
                                        <telerik:RadListBoxItem Text="Administrator" Value="1" />
                                        <telerik:RadListBoxItem Text="Section Head" Value="2" />
                                        <telerik:RadListBoxItem Text="Test" Value="3" />
                                        <telerik:RadListBoxItem Text="Test1" Value="4" />
                                    </Items>
                                </telerik:RadListBox>
                            </td>
                            <td class='sideHead'>
                                <asp:Label ID="lblActive" Text="<%$ Resources:lang, lblActive %>" runat="server" />
                            </td>
                            <td class='MandatoryField'>
                                *
                            </td>
                            <td class='sideHead1'>
                                <asp:RadioButtonList ID="rbtActive" runat="server" Width="100px" RepeatDirection="Horizontal"
                                    CssClass="nopadding">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" bgcolor="#9BAFC1" colspan="6">
                                <telerik:RadButton ID='radSave' Width="100px" Text="<%$ Resources:lang, btnSave %>"
                                    runat='server' />
                                &nbsp;&nbsp;
                                <telerik:RadButton ID='radSaveAdd' Width="125px" Text="<%$ Resources:lang, btnSaveAdd %>"
                                    runat='server' />
                                &nbsp;&nbsp;
                                <telerik:RadButton ID="radClear" runat="server" Width="100px" Text="<%$ Resources:lang, btnClear %>"
                                    OnClientClicked="OnClientClicked" CausesValidation="False" />
                                &nbsp;&nbsp;
                                <telerik:RadButton ID="radCancel" runat="server" Width="100px" AutoPostBack="false"
                                    OnClientClicked="redirectme" Text="<%$ Resources:lang, btnCancel %>" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                    <asp:RegularExpressionValidator ID="oldExp" runat="server" Display="None" ControlToValidate="radPassword"
                        ErrorMessage="<%$ Resources:lang, valPasswordLength %>" ValidationExpression="^.{6,50}$"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID='summary' runat='server' ShowSummary='False' ShowMessageBox='True'
                        HeaderText="<%$ Resources:lang, Errors %>"></asp:ValidationSummary>
                </div>
                <%-- <asp:ObjectDataSource ID="odsEmployeeNameEdit" runat="server" SelectMethod="GetAllATSEmployeesDrop"
                    TypeName="TrackIT.TrackITATS.clsATSEmployees" OldValuesParameterFormatString="{0}">
                    <SelectParameters>
                        <asp:Parameter Name="valUserName" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="odsEmployeeNameNew" runat="server" SelectMethod="GetAllATSEmployeesDropUser"
                    TypeName="TrackIT.TrackITATS.clsATSEmployees" OldValuesParameterFormatString="{0}">
                    <SelectParameters>
                        <asp:Parameter Name="valUserName" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>--%>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
