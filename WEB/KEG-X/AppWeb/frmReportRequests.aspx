<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmReportRequests.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmReportRequests" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">   
<%--   <style type="text/css">
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
</style>--%>
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

        function ConfirmExcelDownload() {
            if (confirm("Are you sure you want to download the data in Excel format?")) {
                return true;
            }
            return false;
        }
        function ConfirmPDFDownload() {
            if (confirm("Are you sure you want to download the data in PDF format?")) {
                return true;
            }
            return false;
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
            <%-- <telerik:AjaxSetting AjaxControlID="rcbCompany">
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
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnShow">
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
        <asp:Literal ID="Literal1" runat="server" Text="REPORT REQUESTS - QUEUED" /></h2>
    <em>* Required Fields</em>
    
    <table id="TABLE2" cellspacing="0" cellpadding="0" width="100%" border="0"
        runat="server">
        <tr><td>
            <%--<td height="450px" valign="Top">
                <div>--%>
                    <asp:Label ID="lblMsg" runat="server" CssClass="noteMsg"></asp:Label>
                    </td></tr><tr><td width="50%" valign="Top">
                    <asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
                        <ContentTemplate>
                             <table id="TABLE5" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                                rules='none' width="60%" border='0' runat="server">
                                <tr>
                                    <td class="sideHead">
                                        <asp:Label ID="lblRequests" Text="Report Name" runat="server" />*
                                    </td>
                                    <td class="sideHead1">
                                        <telerik:RadComboBox ID="rcbRequests" runat="server" Filter="Contains" Width="195px"
                                            AutoPostBack="true">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID='rfvrdcRequests' ControlToValidate='rcbRequests' ErrorMessage="Request Name is Required" ValidationGroup="Submit"
                                            Display='None' runat='server' InitialValue="--Select One--" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="sideHead">
                                        <asp:Label ID="lblFileType" Text="Download As" runat="server" />*
                                    </td>
                                    <td class="sideHead1">
                                        <asp:RadioButtonList ID="rbtFileType" runat="server" RepeatDirection="Horizontal"  style="width:200px;"
                                            CssClass="nopadding">
                                            <asp:ListItem Text="Single File" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Multiple Files" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                 <tr></tr>
                             </table>
                     
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td><td width="50%" valign="Top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
                        <ContentTemplate>   
                            <asp:MultiView ID="mvRequests" runat="server" ActiveViewIndex="0">     
                                 <asp:View ID="vwBlank" runat="server">  </asp:View> 
                                 <asp:View ID="vwCustomerSOA" runat="server">  
                                    <table id="TABLE4" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                                rules='none' width="50%" border='0' runat="server">
                                        <tr> <th class="sideHead1" style="width:200px;">Customer Keg Statement</th></tr>
                                <tr>
                                    <td class="sideHead">
                                        <asp:Label ID="Label3" Text="Company" runat="server" />*
                                    </td>
                                    <td class="sideHead1">
                                        <telerik:RadComboBox ID="rcbCompany" runat="server" Filter="Contains" Width="195px"
                                            AutoPostBack="true">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID='rfvrdcCmpnny' ControlToValidate='rcbCompany' ErrorMessage="Company is Required" ValidationGroup="Submit"
                                            Display='None' runat='server' InitialValue="--Select One--" />
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
                                <tr>
                                    <td class="sideHead">
                                        <asp:Label ID="lblCustCode" Text="Customer" runat="server" />*
                                    </td>
                                    <td class="sideHead1">
                                        <%--   <div id="divCustomer" runat="server" class="autocomplete_completionListElement">
                                    <asp:TextBox ID="txtCustomer" runat="server" Width="195px" MaxLength='100' AutoPostBack="true"
                                        onFocus="this.select()" onclick="this.select()" />
                                   
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCustomerSOA" runat="server"
                                        DelimiterCharacters="" Enabled="True" TargetControlID="txtCustomer" BehaviorID="AutoCompleteCustomerSOA"
                                        OnClientShown="clientListShownCustomerSOA" ServicePath="~/AppWeb/LookupWebService.asmx"
                                        ServiceMethod="CustomerLookup" CompletionInterval="100" MinimumPrefixLength="1"
                                        EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem"
                                        CompletionListCssClass="AutoCompleteFlyout" CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                                        UseContextKey="true">
                                    </asp:AutoCompleteExtender>
                                </div>--%>
                                        <asp:HiddenField ID="hdfCustomerID" runat="server" />
                                        <%--<asp:CascadingDropDown ID="cddCustomer" runat="server" TargetControlID="rcbCustomers" Category="Customer Names" LoadingText="<%$ Resources:lang, cdrpLoadDepartment %>" PromptText="<%$ Resources:lang, cdrpSelectDepartment %>" ServicePath="../WebServices/CascadeDropDown.asmx" ServiceMethod="GetCustomer" ParentControlID="rcbCompany" />--%>
                                        <telerik:RadComboBox runat="server" ID="rcbCustomers" Width="500px" EmptyMessage="Select Customer(s)"
                                            CheckBoxes="true" Filter="Contains" EnableTextSelection="true" EnableCheckAllItemsCheckBox="True"
                                            EnableItemCaching="True" AutoCompleteSeparator="," Localization-AllItemsCheckedString="All Customers Selected"
                                            Localization-ItemsCheckedString="Customers Selected" Localization-CheckAllString="Select All">
                                        </telerik:RadComboBox>
                                        <%--   <asp:RequiredFieldValidator ID='RequiredFieldValidator2' ControlToValidate='rcbCustomers'
                                            ErrorMessage="Customer is Required" Display='None' runat='server'/>--%>
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
                            </table>
                                 </asp:View>    
                            </asp:MultiView>  
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td></tr>
                    <tr><td>
                    <table id="TABLE1" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                        rules='none' width="90%" border='0' runat="server">
                        <tr>
                            <td style="border-bottom: 1px solid #ccddef; padding-left: 600px;">
                                    <div id="overlay" class="overlay"></div>
                                 <asp:Button ID="btnReq" Text ="Submit Request"  Width="300px" Font-Size="16px" OnClientClick ="return showOverlay();" runat="server" CssClass="btnNewLarge" ValidationGroup="Submit" />

                              <%--  <telerik:RadButton ID="btnShow"  SingleClick="true" SingleClickText="wait..."  DisabledButtonCssClass="btnDisable" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                    runat="server" Width="100px" Text="show" />--%>
                            </td>
                        </tr>
                    </table>
                    </td></tr>
                </table><br />
                <asp:UpdatePanel ID="upnlGrid" runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <%--<table id="TABLE3" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                            rules='none' border='0' runat="server">
                            <tr>
                                <td style="padding-top: 5px; padding-bottom: 5px; padding-right: 5px; padding-left: 0px;
                                    visibility: visible; display: block">
                                    <div id="test3"> --%>                                           
                                        <telerik:RadGrid ID="RadgvReportRequests" runat="server" CellSpacing="0" DataSourceID="ObjReportRequests"
                                            GridLines="None" AllowPaging="True" AllowFilteringByColumn="true" Skin="WebBlue" Width="100%"
                                            AllowSorting="true">
                                            <GroupingSettings CaseSensitive="false" />
                                            <MasterTableView AutoGenerateColumns="false" DataSourceID="ObjReportRequests" NoMasterRecordsText="No Records to display"
                                                NoDetailRecordsText="No Records to display">
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="RequestID" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestID" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>                                                        
                                                    <telerik:GridTemplateColumn HeaderText="" AllowFiltering="false" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="IsMultiFileDownload" runat="server" Text='<%#Eval("IsMultiFileDownload")%>'></asp:Label>
                                                            <asp:Label ID="lblRequestName" runat="server" Text='<%#Eval("RequestName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="Description" FilterControlAltText="Filter Description column"
                                                        HeaderText="Report Name" SortExpression="Description" UniqueName="Description">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Parameters" FilterControlAltText="Filter Parameters column"
                                                        HeaderText="Parameters" SortExpression="Parameters" UniqueName="Parameters">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="EmployeeName" FilterControlAltText="Filter EmployeeName column" 
                                                        HeaderText="Requested By" SortExpression="EmployeeName" UniqueName="EmployeeName">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="RequestedDate" FilterControlAltText="Filter RequestedDate column"
                                                        HeaderText="Requested Date" SortExpression="RequestedDate"
                                                        UniqueName="RequestedDate"  HtmlEncode="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Status" FilterControlAltText="Filter Status column"
                                                        HeaderText="Status" SortExpression="Status" UniqueName="Status">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Excel" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkExcelDownload" CommandName="ExcelDownload" runat="server" Text='<%# IIf(Eval("Download") = "0", "", "Download")%>'
                                                                Font-Underline="true" ForeColor="Blue" OnClientClick="return ConfirmExcelDownload()" Visible='<%# Eval("Download") %>' ></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="PDF" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkPDFDownload" CommandName="PDFDownload" runat="server" Text='<%# IIf(Eval("Download") = "0", "", "Download")%>'
                                                                Font-Underline="true" ForeColor="Blue" OnClientClick="return ConfirmPDFDownload()" Visible='<%# Eval("Download") %>' ></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="ProcessedDate" FilterControlAltText="Filter ProcessedDate column"
                                                        HeaderText="Processed Date" SortExpression="ProcessedDate"
                                                        UniqueName="ProcessedDate"  HtmlEncode="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Comments" FilterControlAltText="Filter Comments column"
                                                        HeaderText="Comments" SortExpression="Comments" UniqueName="Comments">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                                <ClientSettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
                                                </Scrolling>
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                        <asp:ObjectDataSource ID="ObjReportRequests" runat="server" SelectMethod="GetTabReportRequestsByCompanyID"
                                            DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsReports" OldValuesParameterFormatString="{0}">
                                            <SelectParameters>
                                                <asp:Parameter Name="CompanyID" Type="Int32" />
                                                <asp:Parameter Name="EmployeeID" Type="Int32" />
                                                <asp:Parameter Name="SchemaName" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                   <%-- </div>
                                </td>
                            </tr>
                        </table>--%>
                    </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="RadgvReportRequests" />
                        </Triggers>
                </asp:UpdatePanel>
                <asp:ValidationSummary ID='summary' runat='server' ShowSummary='False' ShowMessageBox='True'
                    HeaderText="<%$ Resources:lang, Errors %>"></asp:ValidationSummary>
               <%-- </div>--%>
            
                <asp:UpdatePanel ID="upnlTimer" runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
                    <ContentTemplate>
                <asp:Timer ID="RadgvRequestsTimer" runat="server" Interval="5000" OnTick="RadgvRequestsTimer_Tick">
                </asp:Timer>
                    </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="RadgvReportRequests"/>
                        </Triggers>
                </asp:UpdatePanel>
            
    <asp:ValidationSummary ID='ValidationSummary1' runat='server' ShowSummary='False'
        ShowMessageBox='True' HeaderText='<%$ Resources:lang, Errors %>'></asp:ValidationSummary>
</asp:Content>
