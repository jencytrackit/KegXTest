<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="rptDiscrepancy.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.rptDiscrepancy" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
  <script language="javascript" type="text/javascript">

      function clientListShownCustomerSOA(source, eventArgs) {
          var completionList = $find("AutoCompleteCustomerSOA").get_completionList();
          completionList.style.width = 'auto';
      }

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
        <asp:Label ID="addATSUsers" Text="Discrepancy Report (Dispatch & Collection)" runat="server" /></h2>
    <em>* Required Fields</em>
    <table id="TABLE2" cellspacing="0" cellpadding="0" height="450px" width="100%" border="0"
        runat="server">
        <tr>
            <td height="450px" valign="Top">
                <div>
                    <br />
                       <asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
                        <ContentTemplate>
                    <table id="TABLE4" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                        rules='none' width="50%" border='0' runat="server">
                        <tr>
                            <td bgcolor='white' valign="top" colspan="2">
                                <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td class="sideHead">
                                <asp:Label ID="Label4" Text="Company" runat="server" />*
                            </td>
                            <td class="sideHead1">
                                <telerik:RadComboBox ID="rcbCompany" runat="server" Filter="Contains" Width="195px"
                                    AutoPostBack="true">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='rfvrdcCmpnny' ControlToValidate='rcbCompany' ErrorMessage="Company is Required"
                                    Display='None' runat='server' InitialValue="--Select One--" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="sideHead">
                                <asp:Label ID="Label3" Text="Channel" runat="server" />
                            </td>
                            <td class="sideHead1">
                                <telerik:RadComboBox ID="rcbChannel" runat="server" Filter="Contains" Width="195px"
                                    AutoPostBack="true" >
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='rfvrdcChannel' ControlToValidate='rcbChannel' ErrorMessage="Channel is Required"
                                    Display='None' runat='server' InitialValue="--Select One--"  />
                                <%--   <telerik:RadComboBox ID="rcbCompany" runat="server" Filter="Contains" Width="195px">
                                </telerik:RadComboBox>
                                     <asp:RequiredFieldValidator ID='rfvrdcCmpnny' ControlToValidate='rcbCompany' ErrorMessage="Company is Required"
                                    Display='None' runat='server' InitialValue="--Select One--" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="sideHead">
                                <asp:Label ID="lblDate" Text="Date From" runat="server" />*
                            </td>
                            <td class="sideHead1">
                                <asp:Label ID="Label2" Text="From" Visible="false" runat="server" />
                                <telerik:RadDatePicker ID="FromDt" runat="server" DateInput-DateFormat="dd/MM/yy"
                                    ShowPopupOnFocus="true">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="sideHead">
                                <asp:Label ID="Label1" Text="Date To" runat="server" />*
                            </td>
                            <td class="sideHead1">
                                <telerik:RadDatePicker ID="ToDt" runat="server" DateInput-DateFormat="dd/MM/yy" ShowPopupOnFocus="true">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="sideHead">
                                <asp:Label ID="lblCustCode" Text="Customer" runat="server" />*
                            </td>
                            <td class="sideHead1">
                                <asp:HiddenField ID="hdfCustomerID" runat="server" />
                                <telerik:RadComboBox runat="server" ID="rcbCustomers" Width="500px" EmptyMessage="Select Customer(s)"
                                    CheckBoxes="true" Filter="Contains" EnableTextSelection="true" EnableCheckAllItemsCheckBox="True"
                                    EnableItemCaching="True" AutoCompleteSeparator="," Localization-AllItemsCheckedString="All Customers Selected"
                                    Localization-ItemsCheckedString="Customers Selected" Localization-CheckAllString="Select All">
                                </telerik:RadComboBox>
                            </td>
                        </tr>--%>
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
                            <td colspan="2">
                            </td>
                        </tr>
                    <%--    <tr>
                            <td colspan="2" align="center">
                                <telerik:RadButton ID="btnShow" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                    runat="server" Width="100px" Text="show" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2" style="border-bottom: 1px solid #ccddef">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                    </table>
                      </ContentTemplate>
                       
                    </asp:UpdatePanel>
                   <table id="TABLE1" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                        rules='none' width="90%" border='0' runat="server">
                        <tr>
                            <td style="border-bottom: 1px solid #ccddef; padding-left: 220px;">
                                    <div id="overlay" class="overlay"></div>
                                 <asp:Button ID="btn" Text ="Show"  Width="90px" Font-Size="16px" OnClientClick ="return showOverlay();" runat="server" CssClass="btnNew"  />

                              <%--  <telerik:RadButton ID="btnShow"  SingleClick="true" SingleClickText="wait..."  DisabledButtonCssClass="btnDisable" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                    runat="server" Width="100px" Text="show" />--%>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
                        <ContentTemplate>
                            <table id="TABLE3" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                                rules='none' width="90%" border='0' runat="server">
                                <tr>
                                    <td style="padding-top: 5px; padding-bottom: 5px; padding-right: 5px; padding-left: 0px;
                                        visibility: visible; display: block">
                                        <div id="test3">
                                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                                                HasDrilldownTabs="False" Height="800px" Width="900px" HasToggleGroupTreeButton="false"
                                                ToolPanelView="None" ReuseParameterValuesOnRefresh="True" EnableDatabaseLogonPrompt="False"
                                                EnableParameterPrompt="False" BestFitPage="false" HasCrystalLogo="False" PrintMode="Pdf" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--<asp:ValidationSummary ID='summary' runat='server' ShowSummary='False' ShowMessageBox='True'
                        HeaderText="<%$ Resources:lang, Errors %>"></asp:ValidationSummary>--%>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
