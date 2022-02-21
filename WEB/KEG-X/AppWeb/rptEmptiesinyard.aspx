<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="rptEmptiesinyard.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.rptEmptiesinyard" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="~/controls/MenuList.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
  <style type="text/css">
    .overlay {
        background-color: #000;
        cursor: wait;
        display: none;
        height: 100%;
        left: 0;
        opacity: 0.4;
        position: fixed;
        top: 0;
        width: 100%;
        z-index: 9999998;
    }
</style>
    <script language="javascript" type="text/javascript">

        function showOverlay() {
            document.getElementById("overlay").style.display = "block";
        }

        function hideOverlay() {
            document.getElementById("overlay").style.display = "none";
        }

   
    </script>
    <ATS:Session ID="chkSession" runat="Server" />
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <h2>
        <asp:Label ID="addATSUsers" Text="Empties in yard Report" runat="server" /></h2>
    <table id="TABLE2" cellspacing="0" cellpadding="0" height="450px" width="100%" border="0"
        runat="server">
        <tr>
            <td height="450px" valign="Top">
                <ATSMenu:Menu ID="MENULIST" runat="Server" />
                <div>
                    <br />
                    <br />
                    <table id="TABLE4" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                        rules='none' width="50%" border='0' runat="server">
                           <tr>
                            <td bgcolor='white' valign="top" colspan="2">
                                <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                         <tr>
                           
                            <td class="sideHead">
                                <asp:Label ID="Label3" Text="Company" runat="server" />*
                            </td>
                            <td class="sideHead1">
                                <telerik:RadComboBox ID="rcbCompany" runat="server" Filter="Contains" Width="195px"  AutoPostBack="true">
                                </telerik:RadComboBox>
                                     <asp:RequiredFieldValidator ID='rfvrdcCmpnny' ControlToValidate='rcbCompany' ErrorMessage="Company is Required"
                                    Display='None' runat='server' InitialValue="--Select One--" />
                            </td>
                       
                        </tr>
                        <tr>
                            <td class="sideHead">
                                <asp:Label ID="Label4" Text="Items" runat="server" />*
                            </td>
                            <td class="sideHead1">
                                <asp:HiddenField ID="hdfItemIDs" runat="server" />
                                <telerik:RadComboBox runat="server" ID="rcbItems" Width="500px" EmptyMessage="Select Items(s)"
                                    CheckBoxes="true" Filter="Contains" EnableTextSelection="true" EnableCheckAllItemsCheckBox="True"
                                    EnableItemCaching="True" AutoCompleteSeparator="," Localization-AllItemsCheckedString="All Items Selected"
                                    Localization-ItemsCheckedString="Items Selected" Localization-CheckAllString="Select All">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                       <tr>
                            <td colspan="2" align="center">
                                     <div id="overlay" class="overlay"></div>
                             <asp:Button ID="btn" Text ="Show"  Width="90px" Font-Size="16px" OnClientClick ="return showOverlay();" runat="server" CssClass="btnNew"  />

<%--
                                <telerik:RadButton ID="btnShow" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                    runat="server" Width="100px" Text="show" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="border-bottom: 1px solid #ccddef">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                            </td>
                        </tr>
                     
                    </table>
                         <table id="TABLE1" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                        rules='none' width="90%" border='0' runat="server">
                         <tr>
                            <td colspan="2" style="display: block; padding-top: 5px; padding-bottom: 5px; padding-right: 5px;
                                padding-left: 0px">
                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                                    HasDrilldownTabs="False" Height="800px" Width="900px" HasToggleGroupTreeButton="false"
                                    ToolPanelView="None" ReuseParameterValuesOnRefresh="True" EnableDatabaseLogonPrompt="False"
                                    EnableParameterPrompt="False" BestFitPage="false" HasCrystalLogo="False" />
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
