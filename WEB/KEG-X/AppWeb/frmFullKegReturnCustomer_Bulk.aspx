<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="frmFullKegReturnCustomer_Bulk.aspx.vb" Inherits="KEG_X.TrackITKTS.frmFullKegReturnCustomer_Bulk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">

        function OnlyNumbers(e) {
            e = e || window.event;
            ch = e.which || e.keyCode;
            if (ch != null) {
                if ((ch >= 48 && ch <= 57) || ch == 0 || ch == 8 || ch == 13 || ch == 9 || ch == 43 || ch == 45)
                    return true;
            }

            return false;
        }
             

        function clientListShownItemEmp(source, eventArgs) {
            var completionList = $find("AutoCompleteItemCodeEmp").get_completionList();
            completionList.style.width = 'auto';
        }
        function clientListShownBPEmp(source, eventArgs) {
            var completionList = $find("AutoCompleteBPEmp").get_completionList();
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
    
    <div class="f-left">
        <%--<asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />--%>
    </div>
    <div id="first-div" class="f-left">        
    
    </div>
    <div id="second-div" class="div-left">

        <h2>
            <asp:Literal ID="Literal1" runat="server" Text="Full Keg Bulk Return - Customer" /></h2>
    </div>
    <div style="clear: both;">
    </div>

    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>

            <table id="TABLE1" cellspacing="0" cellpadding="0" width="100%" align="center" border="0"
                runat="server">

                <tr>
                    <td>
                        <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="TABLE4" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" runat="server" visible="false">
                            <tr>
                                <td>
                                    <asp:Label ID='Label2' runat='server' Text="Search Criteria" Font-Bold="false" Font-Size="Small"></asp:Label>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>                
                <tr>
                    <td style="padding: 10px 0 0 0;">
                        <hr />
                        <table id="Table2" cellspacing="1" cellpadding="1" align="center" border="0" runat="server">
                            <tr>
                                <td style="border: 0px solid #9BAFC1;">
                                    
                                    <div class="half-panel">
                                        <table id="Table6" cellspacing="5" cellpadding="5" width="100%" align="center" border="0"
                                            runat="server">
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
                                                </td>

                                                <td class='sideHead'>
                                                    <asp:Label ID="lblItemCode" runat="server" Text="Item Code"></asp:Label>
                                                </td>
                                                <td class='sideHead1'>
                                                    <div id="divItemCode" runat="server" class="autocomplete_completionListElement">
                                                        <asp:TextBox ID="txtItemCode1" runat="server" onFocus="this.select()" onclick="this.select()"
                                                            Width="195px" MaxLength='100' AutoPostBack="true"></asp:TextBox>
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
                                                    <asp:Label ID="lblOrderNo" Text="Order Number" runat="server" />
                                                </td>
                                                <td class='sideHead1'>
                                                    <telerik:RadTextBox ID="txtOrderNo" runat="server" Width="195px" Enabled="true">
                                                    </telerik:RadTextBox>                                                    
                                                </td>
                                                
                                                <td class='sideHead'>
                                                    <telerik:RadButton ID="radSearch" HoveredCssClass="normal-btnh" ForeColor="White"
                                                        Font-Size="15px" Width="60px" Text="Search" runat="server" SingleClick="true" SingleClickText="wait..." />
                                                </td>                                                
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>                      

                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                        <table id="TABLE5" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" runat="server">
                            <tr>                              
                                <td width="250px">
                                    <asp:Label ID="Label7" Text="TO COMPANY *" runat="server" Font-Size="12px" color="#6b6b6b" />
                                    <telerik:RadComboBox ID="rcbToCmpny" runat="server" Filter="Contains" AutoPostBack="true"
                                        Width="160px">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID='rfvrcToCmpny' ControlToValidate='rcbToCmpny' ErrorMessage="ToCompany is Required"
                                        Display="Dynamic" runat='server' ValidationGroup="Save" InitialValue="--Select One--" ForeColor="Red" Font-Size="12px" />
                                </td>
                                <td width="250px">                                    
                                    <asp:Label ID="Label1" Text="TO BRANCH PLANT *" Width="127px" runat="server" Font-Size="12px" color="#6b6b6b" />
                                    <div id="divBranchplant" runat="server" class="autocomplete_completionListElement">                                        
                                        <asp:TextBox ID="txtToBP" runat="server" AutoPostBack="true" MaxLength="100" 
                                            onclick="this.select()" onFocus="this.select()" Width="175px" />
                                        <asp:HiddenField ID="hdfToBPID" runat="server" />
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtBranchplant" runat="server"
                                            DelimiterCharacters="" Enabled="True" TargetControlID="txtToBP" BehaviorID="AutoCompleteBPEmp"
                                            OnClientShown="clientListShownBPEmp" ServicePath="~/AppWeb/LookupWebService.asmx"
                                            ServiceMethod="BranchPlantLookup" CompletionInterval="100" MinimumPrefixLength="1"
                                            EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem"
                                            CompletionListCssClass="AutoCompleteFlyout" CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                                            UseContextKey="true">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <asp:RequiredFieldValidator ID='RequiredFieldValidator1' ControlToValidate='txtToBP'
                                        ErrorMessage="Branch Plant is Required" Display="Dynamic" runat='server' ValidationGroup="Save" ForeColor="Red" Font-Size="12px" />
                                </td>                             
                                <td width="250px">
                                    <asp:Label ID="lblReturnDate" runat="server" Text="RETURN DATE *" Font-Size="12px" color="#6b6b6b"></asp:Label>
                                    <telerik:RadDatePicker ID="dpReturnDate" runat="server" Width="100px" DateInput-DateFormat="MM/dd/yyyy">
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID='RequiredFieldValidator7' ControlToValidate='dpReturnDate' Style="margin-left: 20px"
                                        ErrorMessage="Return Date is Required" ForeColor="Red" Display="Dynamic" runat='server' ValidationGroup="Save" Font-Size="12px" />                                    
                                </td>
                                <td style="padding-left:100px;">
                                    <telerik:RadButton ID="radSave" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="15px"
                                        Width="60px" Text="<%$ Resources:lang, btnSave %>" runat="server" ValidationGroup="Save" SingleClick="true" SingleClickText="wait..." />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <hr />
                        <telerik:RadGrid ID="RadgvFullCust" runat="server" CellSpacing="0" GridLines="None"
                            AllowPaging="True" AllowFilteringByColumn="false" Skin="WebBlue" AllowSorting="true">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>" HierarchyDefaultExpanded="true"
                                NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>" DataKeyNames="SOrderID">
                                <DetailTables>
                                    <telerik:GridTableView DataKeyNames="ItemID" Name="ChildItems" Width="100%" AutoGenerateColumns="false" 
                                        HierarchyDefaultExpanded="true" AllowPaging="True">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ItemID" FilterControlAltText="Filter ItemID column"
                                                Display="false" HeaderText="ItemID" SortExpression="ItemID" UniqueName="ItemID">
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
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <div style="padding-top: 10px;">
                                                    </div>
                                                    <asp:Literal ID="ltrQty" Text="1" runat="server" ></asp:Literal>
                                                   <%-- <asp:TextBox ID="txtQuantity" runat="server" onkeyPress="return OnlyNumbers(event);" Text="1" ></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQuantity"
                                                        ErrorMessage="Quantity should b greater than 0" ValidationExpression="^\d+$"
                                                        ValidationGroup="Save" ForeColor="Red" Font-Size="Small" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Barcode" FilterControlAltText="Filter Barcode column"
                                                HeaderText="Barcode" SortExpression="Barcode" UniqueName="Barcode">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false">                                               
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkselect" runat="server" />
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
                                    <telerik:GridBoundColumn DataField="CompanyCode" Display="false" FilterControlAltText="Filter CompanyCode column"
                                        HeaderText="Company Code" SortExpression="CompanyCode" UniqueName="CompanyCode">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CompanyName" Display="false" FilterControlAltText="Filter CompanyName column"
                                        HeaderText="Company Name" SortExpression="CompanyName" UniqueName="CompanyName">
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn DataField="ToCompanyID" Display="false" FilterControlAltText="Filter ToCompanyID column"
                                        HeaderText="ToCompanyID" SortExpression="ToCompanyID" UniqueName="ToCompanyID">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="SOrderID" FilterControlAltText="Filter SOrderID column"
                                        HeaderText="Sale Order ID" SortExpression="SOrderID" UniqueName="SOrderID" Display="false">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="OrderNumber" FilterControlAltText="Filter OrderNumber column"
                                        HeaderText="Order Number" SortExpression="OrderNumber" UniqueName="OrderNumber">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="CustomerID" FilterControlAltText="Filter CustomerID column"
                                        HeaderText="Customer ID" SortExpression="CustomerID" UniqueName="CustomerID" Display="false">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="CustomerCode" FilterControlAltText="Filter CustomerCode column"
                                        HeaderText="Customer Code" SortExpression="CustomerCode" UniqueName="CustomerCode">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CustomerName" FilterControlAltText="Filter CustomerName column"
                                        HeaderText="Customer Name" SortExpression="CustomerName" UniqueName="CustomerName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BranchCode" FilterControlAltText="Filter BranchCode column"
                                        HeaderText="Branch Plant Code" SortExpression="BranchCode" UniqueName="BranchCode">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BranchName" FilterControlAltText="Filter BranchName column"
                                        HeaderText="Branch Plant Name" SortExpression="BranchName" UniqueName="BranchName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BranchID" FilterControlAltText="Filter BranchID column"
                                        HeaderText="Branch Plant ID" SortExpression="BranchID" UniqueName="BranchID" Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkselectALL" runat="server" Text="Select All" AutoPostBack="true" OnCheckedChanged="RadgvFullCust_HeaderCheckChanged" />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
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

