<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="rptDiscrepancyTimeDelay.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.rptDiscrepancyTimeDelay" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="~/controls/MenuList.ascx" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">

        function clientListShownCustomerSOA(source, eventArgs) {
            var completionList = $find("AutoCompleteCustomerSOA").get_completionList();
            completionList.style.width = 'auto';
        }
    </script>
    <ATS:Session ID="chkSession" runat="Server" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/AppWeb/LookupWebService.asmx" />
        </Services>
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        ClientIDMode="AutoID">
        <AjaxSettings>
            <%--<telerik:AjaxSetting AjaxControlID="rcbCompany">
                <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divCustomer" />
                    <telerik:AjaxUpdatedControl ControlID="txtCustomer" />
                    <telerik:AjaxUpdatedControl ControlID="lblMsg" />                     
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCustomer">
                <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="divCustomer" />
                    <telerik:AjaxUpdatedControl ControlID="lblMsg" />                     
                                    </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <%-- <telerik:AjaxSetting AjaxControlID="btnShow">
                <UpdatedControls>                    
                <telerik:AjaxUpdatedControl ControlID="divCustomer" />                  
                <telerik:AjaxUpdatedControl ControlID="lblMsg" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>
    <h2>
        <asp:Label ID="addATSUsers" Text="Discrepancy Report" runat="server" /></h2>
    <em>* Required Fields</em>
    <table id="TABLE2" cellspacing="0" cellpadding="0" height="450px" width="100%" border="0"
        runat="server">
        <tr>
            <td height="450px" valign="Top">
                <div>
                    <br />
                       <asp:UpdatePanel ID="upnl" runat="server">
                        <ContentTemplate>
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
                                <telerik:RadComboBox ID="rcbCompany" runat="server" Filter="Contains" Width="195px"
                                    AutoPostBack="true">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='rfvrdcCmpnny' ControlToValidate='rcbCompany' ErrorMessage="Company is Required"
                                    Display='None' runat='server' InitialValue="--Select One--" />
                            </td>
                        </tr>
                        <tr>
                            <td class="sideHead">
                                <asp:Label ID="lblCustCode" Text="Customer" runat="server" />*
                            </td>
                            <td class="sideHead1">
                               <div id="divCustomer" runat="server" class="autocomplete_completionListElement">
                                    <asp:TextBox ID="txtCustomer" runat="server" Width="195px" MaxLength='100' AutoPostBack="true"
                                        onFocus="this.select()" onclick="this.select()" />
                                    <asp:HiddenField ID="hdfCustomerID" runat="server" />
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCustomerSOA" runat="server"
                                        DelimiterCharacters="" Enabled="True" TargetControlID="txtCustomer" BehaviorID="AutoCompleteCustomerSOA"
                                        OnClientShown="clientListShownCustomerSOA" ServicePath="~/AppWeb/LookupWebService.asmx"
                                        ServiceMethod="CustomerLookup" CompletionInterval="100" MinimumPrefixLength="1"
                                        EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem"
                                        CompletionListCssClass="AutoCompleteFlyout" CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                                        UseContextKey="true">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <%-- <asp:RequiredFieldValidator ID='RequiredFieldValidator2' ControlToValidate='ddlCustomerCode'
                                    ErrorMessage="Customer is Required" Display='None' runat='server' 
                                    InitialValue="--Select One--" />--%>
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
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                               
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
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
                                <telerik:RadButton ID="btnShow" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                    runat="server" Width="100px" Text="show" />
                            </td>
                        </tr>
                        <tr>
                            <td style="display: block; padding-top: 5px; padding-bottom: 5px; padding-right: 5px;
                                padding-left: 0px">
                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                                    HasDrilldownTabs="False" Height="800px" Width="900px" HasToggleGroupTreeButton="false"
                                    ToolPanelView="None" ReuseParameterValuesOnRefresh="True" EnableDatabaseLogonPrompt="False"
                                    EnableParameterPrompt="False" BestFitPage="false" HasCrystalLogo="False" PrintMode="Pdf" />
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID='summary' runat='server' ShowSummary='False' ShowMessageBox='True'
                        HeaderText="<%$ Resources:lang, Errors %>"></asp:ValidationSummary>
                    <%--<asp:ObjectDataSource ID="ObjDSCustomers" runat="server" SelectMethod="GetTabMCustomersByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMCustomers" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
