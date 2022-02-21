<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmEmptyKegReturn_CusToSupplier.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmEmptyKegReturn_CusToSupplier" %>
        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">

        function DeleteConfirm() {
            if (confirm("Are you sure you want to delete this record?")) {
                return true;
            }
            return false;
        }
        function clientListShownSupplier(source, eventArgs) {
            var completionList = $find("AutoCompleteSupplier").get_completionList();
            completionList.style.width = 'auto';
        }
       
        function clientListShownItemEmp(source, eventArgs) {
            var completionList = $find("AutoCompleteItemEmp").get_completionList();
            completionList.style.width = 'auto';
        }
        function clientListShownCustomer(source, eventArgs) {
            var completionList = $find("AutoCompleteCustomer").get_completionList();
            completionList.style.width = 'auto';
        }
    </script>
    <ATS:Session ID="chkSession" runat="Server" />
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
     <div class="f-left"><asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />
     </div>
    <div id="first-div" class="div-right">
        <telerik:RadButton ID="RadButton1" OnClientClicking="OnClientClicking" NavigateUrl="frmAddEditEmptyKegReturn_CusToSupplier.aspx"
            HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Skin="WebBlue"
            Width="100px" Text="Add" runat="server" />
    </div>
    <div id="second-div" class="div-left">
        <h2>
            <asp:Literal ID="Literal1" runat="server" Text="Empty Keg Return - Customer To Supplier" /></h2>
    </div>
    <div style="clear: both;">
    </div>
    <table id="TABLE1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
        runat="server">
        <tr>
            <td style="padding: 10px 0 0 0;">
                <table id="Table2" cellspacing="1" cellpadding="1" align="center" border="0" runat="server">
                    <tr>
                        <td style="border: 0px solid #9BAFC1;">
                            <div class="half-panel">
                                <table id="Table6" cellspacing="5" cellpadding="5" width="100%" align="center" border="0"
                                    runat="server">
                                    <tr>
                                        <td class='sideHead' width="10%">
                                            <asp:Label ID="lblTransactionId" runat="server" Width="110px" Text="Transaction Id"></asp:Label>
                                        </td>
                                        <td class='sideHead1' width="15%">
                                            <telerik:RadTextBox ID="txtTransactionId" Width="195px" runat="server">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td class='sideHead' width="25%">
                                            <asp:Label ID="lblSupplierCode" runat="server" Width="200px" Text="Supplier"></asp:Label>
                                        </td>
                                        <td class='sideHead1' width="25%">
                                            <div id="divSupplier" runat="server" class="autocomplete_completionListElement">
                                    <asp:TextBox ID="txtSupplier" runat="server" Width="195px" MaxLength='100' AutoPostBack="true"
                                        onFocus="this.select()" onclick="this.select()" />
                                    <asp:HiddenField ID="hdfSupplierID" runat="server" />
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtSupplier" runat="server" DelimiterCharacters=""
                                        Enabled="True" TargetControlID="txtSupplier" BehaviorID="AutoCompleteSupplier"
                                        OnClientShown="clientListShownSupplier" ServicePath="~/AppWeb/LookupWebService.asmx"
                                        ServiceMethod="SupplierLookupEmp" CompletionInterval="100" MinimumPrefixLength="1"
                                        EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem"
                                        CompletionListCssClass="AutoCompleteFlyout" CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                                        UseContextKey="true">
                                    </asp:AutoCompleteExtender>                                    
                                </div>

                                            <%--<telerik:RadComboBox ID="rcbToSupplier" runat="server" Filter="Contains" Width="195px"
                                                DataSourceID="ObjDSSupplier" DataTextField="SuppCodeName" DataValueField="SupplierID">
                                            </telerik:RadComboBox>--%>
                                        </td>
                                         <td class='sideHead' width="10%">
                                            <%--<asp:Label ID="lblCompanyCode" runat="server" Width="100px" Text="Company Code"></asp:Label>--%>
                                        </td>
                                        <td class='sideHead1' width="15%">
                                            <%--<telerik:RadTextBox ID="txtCompanyCode" Width="195px" runat="server">
                                            </telerik:RadTextBox>--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td style="border: 0px solid #9BAFC1;">
                            <div class="half-panel2">
                                <table id="Table3" cellspacing="5" cellpadding="5" align="center" border="0" runat="server">
                                    <tr>
                                        <td class='sideHead'>
                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>
                                        </td>
                                        <td class='sideHead1'>
                                        
                                         <div id="divCustomer" runat="server" class="autocomplete_completionListElement">
                                    <asp:TextBox ID="txtCustomer" runat="server" Width="195px" MaxLength='100' AutoPostBack="true"
                                        onFocus="this.select()" onclick="this.select()" />
                                    <asp:HiddenField ID="hdfCustomerID" runat="server" />
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtCustomer" runat="server" DelimiterCharacters=""
                                        Enabled="True" TargetControlID="txtCustomer" BehaviorID="AutoCompleteCustomer"
                                        OnClientShown="clientListShownCustomer" ServicePath="~/AppWeb/LookupWebService.asmx"
                                        ServiceMethod="CustomerLookupByEmpID" CompletionInterval="100" MinimumPrefixLength="1"
                                        EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem"
                                        CompletionListCssClass="AutoCompleteFlyout" CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                                        UseContextKey="true">
                                    </asp:AutoCompleteExtender>
                                    
                                </div>

                                            <%--<telerik:RadComboBox ID="rcbCustomer" runat="server" Filter="Contains" Width="195px"
                                                DataSourceID="ObjDSCustomers" DataTextField="CustCodeName" DataValueField="CustomerID">
                                            </telerik:RadComboBox>--%>
                                        </td>
                                        <td class='sideHead'>
                                            <asp:Label ID="lblReturnDate" runat="server" Text="Return Date"></asp:Label>
                                        </td>
                                        <td class='sideHead1'>
                                            <telerik:RadDatePicker ID="dpReturnDate" runat="server" Width="195px">
                                                <DateInput DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy">
                                                </DateInput>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td class='sideHead'>
                                            <asp:Label ID="lblItemCode" runat="server" Text="Item Code"></asp:Label>
                                        </td>
                                        <td class='sideHead1'>

                                         <div id="divItemCode" runat="server" class="autocomplete_completionListElement">
                                          <asp:TextBox ID="txtItemCode1" runat="server"  onFocus="this.select()" onclick="this.select()" Width="195px" MaxLength='100' AutoPostBack="true"></asp:TextBox>
                                 <asp:HiddenField ID="hdfItemCode" runat="server" />
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtItemCode" runat="server" DelimiterCharacters=""
                                        Enabled="True" TargetControlID="txtItemCode1" BehaviorID="AutoCompleteItemCodeEmp"
                                        OnClientShown="clientListShownItemEmp" ServicePath="~/AppWeb/LookupWebService.asmx"
                                        ServiceMethod="ItemsLookupByEmpID" CompletionInterval="100" MinimumPrefixLength="1"
                                        EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem"
                                        CompletionListCssClass="AutoCompleteFlyout" CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                                        UseContextKey="true">
                                    </asp:AutoCompleteExtender>                                    
                                </div>
                                            <%--<telerik:RadTextBox ID="txtItemCode" runat="server" Width="195px">
                                            </telerik:RadTextBox>--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#ffffff" colspan="6">
                            <telerik:RadButton ID="radSearch" HoveredCssClass="normal-btnh" ForeColor="White"
                                Font-Size="17px" Width="100px" Text="Search" runat="server" />
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="radClear" HoveredCssClass="normal-btnh" ForeColor="White"
                                Font-Size="17px" Width="100px" Text="Clear" runat="server" CausesValidation="False" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <telerik:RadGrid ID="RadgvCusttoSupplier" runat="server" CellSpacing="0" GridLines="None" AllowPaging="True" AllowFilteringByColumn="false"
                    Skin="WebBlue" AllowSorting="true">
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
                        NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>" DataKeyNames="TransactionNumber">
                        <DetailTables>
                            <telerik:GridTableView DataKeyNames="ECSOrderID" Name="ChildItems" Width="100%" AutoGenerateColumns="false">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ECSOrderID" FilterControlAltText="Filter ECSOrderID column"
                                        Visible="false" HeaderText="ECSOrderID" SortExpression="ECSOrderID" UniqueName="ECSOrderID">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ItemID" FilterControlAltText="Filter ItemID column"
                                        HeaderText="ItemID" SortExpression="ItemID" UniqueName="ItemID" Visible="false">
                                    </telerik:GridBoundColumn>
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
                                    <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter Quantity column"
                                        HeaderText="Quantity" SortExpression="Quantity" UniqueName="Quantity">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="DeleteButton" CommandName="deletechild" CommandArgument='<%# Eval("ECSOrderID") %>'
                                                ImageUrl="../Images/Delete.gif" OnClientClick="return DeleteConfirm()" />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                        <Columns>
                          <telerik:GridBoundColumn DataField="CompanyID" Display="false" FilterControlAltText="Filter CompanyID column"
                    HeaderText="CompanyID" SortExpression="CompanyID" UniqueName="CompanyID">
                </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CompanyCode" Visible="false" FilterControlAltText="Filter CompanyCode column"
                                HeaderText="Company Code" SortExpression="CompanyCode" UniqueName="CompanyCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CompanyName" Visible="false" FilterControlAltText="Filter CompanyName column"
                                HeaderText="Company Name" SortExpression="CompanyName" UniqueName="CompanyName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransactionNumber" FilterControlAltText="Filter TransactionNumber column"
                                HeaderText="Transaction Number" SortExpression="TransactionNumber" UniqueName="TransactionNumber">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerCode" FilterControlAltText="Filter CustomerCode column"
                                HeaderText="Customer Code" SortExpression="CustomerCode" UniqueName="CustomerCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CustomerName" FilterControlAltText="Filter CustomerName column"
                                HeaderText="Customer Name" SortExpression="CustomerName" UniqueName="CustomerName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SupplierCode" FilterControlAltText="Filter SupplierCode column"
                                HeaderText="Supplier Code" SortExpression="SupplierCode" UniqueName="SupplierCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SupplierName" FilterControlAltText="Filter SupplierName column"
                                HeaderText="Supplier Name" SortExpression="SupplierName" UniqueName="SupplierName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ShippingLine" FilterControlAltText="Filter ShippingLine column"
                                HeaderText="Shipping Line" SortExpression="ShippingLine" UniqueName="ShippingLine">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SerialNo" FilterControlAltText="Filter SerialNo column"
                                Visible="false" HeaderText="SerialNo" SortExpression="SerialNo" UniqueName="SerialNo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ContainerNumber" FilterControlAltText="Filter ContainerNumber column"
                                Visible="false" HeaderText="Container Number" SortExpression="ContainerNumber"
                                UniqueName="ContainerNumber">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="BOLDate" FilterControlAltText="Filter BOLDate column"
                                Visible="false" HeaderText="BOL Date" SortExpression="BOLDate" UniqueName="BOLDate">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="BOLNumber" FilterControlAltText="Filter BOLNumber column"
                                Visible="false" HeaderText="BOL Number" SortExpression="BOLNumber" UniqueName="BOLNumber">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TransferDate" FilterControlAltText="Filter TransferDate column"
                                HeaderText="Return Date" SortExpression="Date" UniqueName="TransferDate">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Batch" FilterControlAltText="Filter Batch column"
                                AllowFiltering="false" Visible="false" HeaderText="Batch" SortExpression="Batch"
                                UniqueName="Batch">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="View/Edit" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="EditButton" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container,"RowIndex") %>'
                                        ImageUrl="../Images/Edit.gif" />
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="DeleteButton" CommandName="delete" CommandArgument='<%# Eval("TransactionNumber") %>'
                                        ImageUrl="../Images/Delete.gif" OnClientClick="return DeleteConfirm()" />
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
                        </Scrolling>
                        <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ObjEmptyKegsReturn" runat="server" SelectMethod="GetAllTabKEmptyCustToSuppByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabKEmptyCustToSupp" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
            <asp:Parameter Name="TransactionNumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
      <asp:ObjectDataSource ID="ObjDSCustomers" runat="server" SelectMethod="GetTabMCustomersByEmployeeID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMCustomers" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjDSSupplier" runat="server" SelectMethod="GetTabMSuppliersByEmployeeID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMSuppliers" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
