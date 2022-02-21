<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master" EnableEventValidation="true"
    MaintainScrollPositionOnPostback="true" CodeBehind="frmAddEditEmptyKegReturn_CusToSupplier.aspx.vb"
    Inherits="KEG_X.TrackITKTS.frmAddEditEmptyKegReturn_CusToSupplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">
        function OnClientClickedConfirm(button, args) {
            if (window.confirm("Are you sure? Do you want to continue?")) {
                button.set_autoPostBack(true);
            }
            else {
                button.set_autoPostBack(false);
                button.enableAfterSingleClick();
            }
        }

        function redirectme(sender, args) {
            location.href = 'frmEmptyKegReturn_CusToSupplier.aspx';
        }
        function OnClientClicked(button, args) {
            //            document.forms[0].reset();
            //            button.set_autoPostBack(false);
            location.href = "<%= Request.RawUrl %>";
        }
        function OnlyNumbers(e) {
            e = e || window.event;
            ch = e.which || e.keyCode;
            if (ch != null) {
                if ((ch >= 48 && ch <= 57) || ch == 0 || ch == 8 || ch == 13 || ch == 9 || ch == 43 || ch == 45)
                    return true;
            }

            return false;
        }
        function clientListShown(source, eventArgs) {
            var completionList = $find("AutoCompleteEx").get_completionList();
            completionList.style.width = 'auto';
        }
        function clientListShownCustomer(source, eventArgs) {
            var completionList = $find("AutoCompleteCustomer").get_completionList();
            completionList.style.width = 'auto';
        }
        function clientListShownSupplier(source, eventArgs) {
            var completionList = $find("AutoCompleteSupplier").get_completionList();
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
            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <h2>
        <asp:Label ID="addATSEmployees" Text="Empty Kegs Returns Customer to Supplier" runat="server" /></h2>
    <em>* Required Fields</em>
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate >
            <table id="TABLE100" cellspacing="0" cellpadding="0" height="100%" width="100%" border="0"
                runat="server">
                <tr>
                    <td valign="Top">
                        <div>
                            <table id="TABLE1" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                                class="table" rules='none' width="80%" border='0' runat="server">
                                <tr>
                                    <td bgcolor='white' colspan="4">
                                        <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label5" Text="From Company" runat="server" />*
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <telerik:RadComboBox ID="rcbFromCmpny" runat="server" AutoPostBack="true" Filter="Contains"
                                            Width="195px">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID='rfvrdcCmpnny' ControlToValidate='rcbFromCmpny' ErrorMessage="From Company is Required"
                                            Display='None' runat='server' ValidationGroup="Add" InitialValue="--Select One--" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label2" Text="Customer" runat="server" />*
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <div id="divCustomer" runat="server" class="autocomplete_completionListElement">
                                            <asp:TextBox ID="txtCustomer" runat="server" Width="195px" MaxLength='100' AutoPostBack="true"
                                                onFocus="this.select()" onclick="this.select()" />
                                            <asp:HiddenField ID="hdfCustomerID" runat="server" />
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCustomer" runat="server" DelimiterCharacters=""
                                                Enabled="True" TargetControlID="txtCustomer" BehaviorID="AutoCompleteCustomer"
                                                OnClientShown="clientListShownCustomer" ServicePath="~/AppWeb/LookupWebService.asmx"
                                                ServiceMethod="CustomerLookup" CompletionInterval="100" MinimumPrefixLength="1"
                                                EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem"
                                                CompletionListCssClass="AutoCompleteFlyout" CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                                                UseContextKey="true">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <asp:RequiredFieldValidator ID='RequiredFieldValidator2' ControlToValidate='txtCustomer'
                                            ErrorMessage="Customer is Required" Display='None' runat='server' ValidationGroup="Add" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label3" Text="To Company" runat="server" />*
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <telerik:RadComboBox ID="rcbToCmpny" runat="server" Filter="Contains" AutoPostBack="true"
                                            Width="195px">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID='rfvrcToCmpny' ControlToValidate='rcbToCmpny' ErrorMessage="To Company is Required"
                                            Display='None' runat='server' ValidationGroup="Add" InitialValue="--Select One--" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label1" Text="Supplier" runat="server" />*
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <div id="divSupplier" runat="server" class="autocomplete_completionListElement">
                                            <asp:TextBox ID="txtSupplier" runat="server" Width="195px" MaxLength='100' AutoPostBack="true"
                                                onFocus="this.select()" onclick="this.select()" />
                                            <asp:HiddenField ID="hdfSupplierID" runat="server" />
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtSupplier" runat="server" DelimiterCharacters=""
                                                Enabled="True" TargetControlID="txtSupplier" BehaviorID="AutoCompleteSupplier"
                                                OnClientShown="clientListShownSupplier" ServicePath="~/AppWeb/LookupWebService.asmx"
                                                ServiceMethod="SupplierLookup" CompletionInterval="100" MinimumPrefixLength="1"
                                                EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem"
                                                CompletionListCssClass="AutoCompleteFlyout" CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                                                UseContextKey="true">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <asp:RequiredFieldValidator ID='RequiredFieldValidator1' ControlToValidate='txtSupplier'
                                            ErrorMessage="Supplier is Required" Display='None' runat='server' ValidationGroup="Add" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label7" Text="BOL Number" runat="server" />
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <telerik:RadTextBox ID="txtBOLNo" runat="server" Width="195px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label8" Text="BOL Date" runat="server" />
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <telerik:RadDatePicker ID="dtBOLDate" runat="server" Width="195px" DateInput-DateFormat="MM/dd/yyyy"
                                            ShowPopupOnFocus="true">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label6" Text="Shipping Line" runat="server" />
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <telerik:RadTextBox ID="txtShippingLine" runat="server" Width="195px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label9" Text="SerialNo" runat="server" />
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <telerik:RadTextBox ID="txtSerialNo" runat="server">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label10" Text="Container Number" runat="server" />
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <telerik:RadTextBox ID="txtContainerNO" runat="server" Width="195px">
                                        </telerik:RadTextBox>
                                        <telerik:RadTextBox ID="txtTransactionNumber" runat="server" Width="195px" Visible="false">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td class='sideHead'>
                                        <asp:Label ID="Label11" Text="Return Date" runat="server" />*
                                    </td>
                                    <td class='sideHead1'>
                                        <telerik:RadDatePicker ID="dtReturnDate" runat="server" DateInput-DateFormat="MM/dd/yyyy"
                                            ShowPopupOnFocus="true">
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID='RequiredFieldValidator7' ControlToValidate='dtReturnDate'
                                            ErrorMessage="Return Date is Required" Display='None' runat='server' ValidationGroup="Save" />
                                        <asp:RequiredFieldValidator ID='RequiredFieldValidator8' ControlToValidate='dtReturnDate'
                                            ErrorMessage="Return Date is Required" Display='None' runat='server' ValidationGroup="Add" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="border-bottom: 1px solid #ccddef">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="lblItemID" Text="Item Code" runat="server" />*
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <div id="divItems" runat="server" class="autocomplete_completionListElement">
                                            <asp:TextBox ID="txtItemCode" runat="server" Width="195px" MaxLength='100' AutoPostBack="true"
                                                onFocus="this.select()" onclick="this.select()" />
                                            <asp:HiddenField ID="hdfItemID" runat="server" />
                                            <asp:AutoCompleteExtender ID="txtItemCode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                Enabled="True" TargetControlID="txtItemCode" BehaviorID="AutoCompleteEx" OnClientShown="clientListShown"
                                                ServicePath="~/AppWeb/LookupWebService.asmx" ServiceMethod="ItemsLookup" CompletionInterval="100"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true"
                                                CompletionListItemCssClass="AutoCompleteFlyoutItem" CompletionListCssClass="AutoCompleteFlyout"
                                                CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem" UseContextKey="true">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <asp:RequiredFieldValidator ID='RequiredFieldValidator3' ControlToValidate='txtItemCode'
                                            ErrorMessage="ItemCode is Required" Display='None' runat='server' ValidationGroup="Add" />
                                    </td>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="lblItemDescription" Text="Item Name1" runat="server" />*
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <telerik:RadTextBox ID="txtItemDesc" runat="server" Width="195px" Enabled="false">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID='RequiredFieldValidator4' ControlToValidate='txtItemDesc'
                                            ErrorMessage="Item Name1 is Required" Display='None' runat='server' ValidationGroup="Add" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead'>
                                        <asp:Label ID="Label12" Text="Item Name2" runat="server" />*
                                    </td>
                                    <td class='sideHead1'>
                                        <telerik:RadTextBox ID="txtItemName2" runat="server" Width="195px" Enabled="false">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID='RequiredFieldValidator9' ControlToValidate='txtItemName2'
                                            ErrorMessage="Item Name2 is Required" Display='None' runat='server' ValidationGroup="Add" />
                                    </td>
                                    <td class='sideHead'>
                                        <asp:Label ID="lblQuantity" Text="Quantity" runat="server" />*
                                    </td>
                                    <td class='sideHead1'>
                                        <telerik:RadTextBox ID="txtQuantity" runat="server" Width="195px" onkeyPress="return OnlyNumbers(event);">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID='RequiredFieldValidator5' ControlToValidate='txtQuantity'
                                            ErrorMessage="Quantity is Required" Display='None' runat='server' ValidationGroup="Add" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQuantity"
                                            ErrorMessage="Quantity should b greater than 0" ValidationExpression="^\d+$"
                                            ValidationGroup="Add" ForeColor="Red" Font-Size="Small"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead'>
                                        <asp:Label ID="lblUOM" Text="UOM" runat="server" />*
                                    </td>
                                    <td class='sideHead1'>
                                        <telerik:RadTextBox ID="txtUOM" Width="195px" MaxLength='100' runat="server" Enabled="false">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID='RequiredFieldValidator6' ControlToValidate='txtUOM'
                                            ValidationGroup="Add" ErrorMessage="UOM is Required" Display='None' runat='server' />
                                    </td>
                                    <td class='sideHead'>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btnAdd" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                            Width="100px" Text="Add" runat="server" ValidationGroup="Add"
                                            />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <telerik:RadGrid ID="RadgvEmptyKegTransfer" runat="server" CellSpacing="0" GridLines="None"
                                            Skin="WebBlue">
                                            <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
                                                NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ItemCode" FilterControlAltText="Filter ItemCode column"
                                                        HeaderText="Item Code" SortExpression="ItemCode" UniqueName="ItemCode">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ItemName1" FilterControlAltText="Filter ItemName1 column"
                                                        HeaderText="Item Name1" SortExpression="ItemName1" UniqueName="ItemName1">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ItemName2" FilterControlAltText="Filter ItemName2 column"
                                                        HeaderText="Item Name2" SortExpression="ItemName2" UniqueName="ItemName2">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="UOM" FilterControlAltText="Filter UOM column"
                                                        HeaderText="UOM" SortExpression="UOM" UniqueName="UOM">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' onkeyPress="return OnlyNumbers(event);"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQuantity"
                                                                ErrorMessage="Quantity should b greater than 0" ValidationExpression="^\d+$"
                                                                ValidationGroup="Save" ForeColor="Red" Font-Size="Small"></asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgridItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PrevQty" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrevQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="EOrderID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblECSOrderID" runat="server" Text='<%# Eval("ECSOrderID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" bgcolor="#ffffff" colspan="4">
                                        <telerik:RadButton ID="radSave" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                            Width="100px" Text="<%$ Resources:lang, btnSave %>" ValidationGroup="Save" runat="server" 
                                            SingleClick="true" SingleClickText="wait..."  DisabledButtonCssClass="btnDisable" 
                                            OnClientClicked="OnClientClickedConfirm"  />
                                        &nbsp;&nbsp;
                                        <telerik:RadButton ID="radClear" HoveredCssClass="normal-btnh" ForeColor="White"
                                            Font-Size="17px" Width="100px" Text="<%$ Resources:lang, btnClear %>" runat="server"
                                            AutoPostBack="false" OnClientClicked="OnClientClicked" CausesValidation="False" />
                                        &nbsp;&nbsp;
                                        <telerik:RadButton ID="radCancel" HoveredCssClass="normal-btnh" ForeColor="White"
                                            Font-Size="17px" runat="server" Width="100px" AutoPostBack="false" OnClientClicked="redirectme"
                                            Text="<%$ Resources:lang, btnCancel %>" CausesValidation="False" />
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <br />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:ValidationSummary ID='ValidationSummary1' runat='server' ShowSummary='False'
                                ShowMessageBox='True' HeaderText='<%$ Resources:lang, Errors %>' ValidationGroup="Add">
                            </asp:ValidationSummary>
                            <asp:ValidationSummary ID='ValidationSummary2' runat='server' ShowSummary='False'
                                ShowMessageBox='True' HeaderText='<%$ Resources:lang, Errors %>' ValidationGroup="Save">
                            </asp:ValidationSummary>
                        </div>
                    </td>
                </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjDSCustomers" runat="server" SelectMethod="GetTabMCustomersByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMCustomers" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjDSSupplier" runat="server" SelectMethod="GetTabMSuppliersByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMSuppliers" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
