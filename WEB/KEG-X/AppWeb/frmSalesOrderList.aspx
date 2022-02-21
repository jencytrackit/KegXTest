<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmSalesOrderList.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmSalesOrderList" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
    <script language="javascript" type="text/javascript">

        function OnClientClickedConfirm(button, args) {
            if (window.confirm("Are you sure? Do you want to Re-Print the Barcodes for selected Items?")) {
                button.set_autoPostBack(true);
            }
            else {
                button.set_autoPostBack(false);
                button.enableAfterSingleClick();
            }
        }
        function EnableSingleClick(b, args) {
            var button = $find("<%= radSearch.ClientID %>");

            //button.set_autoPostBack(false);
            button.enableAfterSingleClick();
            var bclear = $find("<%=  radClear.ClientID %>");
            bclear.set_enabled(true);
        }
        function DisableCtrl(b, args) {
            var bsearch = $find("<%= radSearch.ClientID %>");
            var bclear = $find("<%=  radClear.ClientID %>");
            bsearch.set_enabled(false);
            bclear.set_enabled(false);
        }

        function redirectme(sender, args) {
            location.href = 'frmEmptyKegReturn_CusToSupplier.aspx';
        }
        function OnClientClicked(button, args) {
            document.forms[0].reset();
            button.set_autoPostBack(false);

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
        function clientListShownBPEmp(source, eventArgs) {
            var completionList = $find("AutoCompleteBPEmp").get_completionList();
            completionList.style.width = 'auto';
        }
        function clientListShownItemEmp(source, eventArgs) {
            var completionList = $find("AutoCompleteItemEmp").get_completionList();
            completionList.style.width = 'auto';
        }


                function SetHeaderCheckBox(checkbox, parentIndex) {
                    var masterTable = $find('<%= RadgvSalesOrder.ClientID %>').get_masterTableView();
                    var nestedView = masterTable.get_dataItems()[parentIndex].get_nestedViews()[0];
                    for (var i = 0; i < nestedView.get_dataItems().length; i++) {
                        if (!nestedView.get_dataItems()[i].findElement("chkAll").checked) {

                            nestedView.HeaderRow.getElementsByTagName('input')[0].checked = false;
                            return;

                        }
                    }

                    nestedView.HeaderRow.getElementsByTagName('input')[0].checked = true;

                }

                function SelectAllCheckBox(checkbox, parentIndex) {
                    var masterTable = $find('<%= RadgvSalesOrder.ClientID %>').get_masterTableView();
                    var nestedView = masterTable.get_dataItems()[parentIndex].get_nestedViews()[0];
                    for (var i = 0; i < nestedView.get_dataItems().length; i++) {

                        nestedView.get_dataItems()[i].findElement("chkSelectAll").checked = checkbox.checked;

                    }
                }

                function CheckAll(id) {
                    var masterTable = $find("<%= RadgvSalesOrder.ClientID %>").get_masterTableView();
                    var row = masterTable.get_dataItems();
                    if (id.checked == true) {
                        for (var i = 0; i < row.length; i++) {
                            masterTable.get_dataItems()[i].findElement("chkSelectAllMain").checked = true;
                        }
                    }
                    else {
                        for (var i = 0; i < row.length; i++) {
                            masterTable.get_dataItems()[i].findElement("chkSelectAllMain").checked = false;
                        }
                    }
                }

               
    </script>
<%--    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>--%>

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/AppWeb/LookupWebService.asmx" />
        </Services>
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadgvSalesOrder">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="RadgvSalesOrder" />
                    <telerik:AjaxUpdatedControl ControlID="lblMsg"  />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radSearch">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="RadgvSalesOrder" />
                    <telerik:AjaxUpdatedControl ControlID="lblMsg"  />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <ClientEvents OnResponseEnd="EnableSingleClick" OnRequestStart="DisableCtrl" />
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <h2>
                                    <asp:Literal ID="Literal1" runat="server" Text="Manage Sales Order" /></h2>

<%--<asp:UpdatePanel ID="upnl1" runat ="server">

<ContentTemplate >--%>  

<table id="TABLE1" cellspacing="0" cellpadding="0" align="center" border="0" runat="server">
        <tr>
            <td style="padding: 10px 0 0 0;">
                <ATSMenu:Menu ID="MENU1" runat="Server" />
                <asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />
                <table id="Table2" cellspacing="1" cellpadding="1" align="center" border="0" runat="server">
                    <tr>
                        <td style="border: 0px solid #9BAFC1;">
                            <div class="half-panel">
                                <table id="Table7" cellspacing="5" cellpadding="5" align="center" border="0" runat="server">
                                    <tr>
                                      <%--  <td class='sideHead' width="20%">
                                            <asp:Label ID="lblItemID" Text="Company Code" Enabled ="false" Width="100px" runat="server" />
                                        </td>
                                        <td class='sideHead1 MandatoryField' style="width: 10px">
                                            &nbsp;
                                        </td>
                                        <td class='sideHead1' width="30%">
                                            <telerik:RadTextBox ID="txtCompanyCode" Width="195px" Enabled ="false" MaxLength='100' runat="server">
                                            </telerik:RadTextBox>
                                        </td>--%>
                                        <td class='sideHead' width="20%">
                                            <asp:Label ID="Label2" Text="Customer" Width="100px" runat="server" />
                                        </td>
                                        <td class='sideHead1 MandatoryField' style="width: 10px">
                                            &nbsp;
                                        </td>
                                        <td class='sideHead1' width="30%">

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


                                   <%--         <telerik:RadComboBox ID="rcbCustomer" runat="server" Filter="Contains" Width="195px"
                                                DataSourceID="ObjDSCustomers" DataTextField="CustomerName" DataValueField="CustomerID">
                                            </telerik:RadComboBox>--%>
                                        </td>
                                        <td class='sideHead' width="20%">
                                            <asp:Label ID="Label1" Text="Branch Plant" Width="100px" runat="server" />
                                        </td>
                                        <td class='sideHead1 MandatoryField' style="width: 10px">
                                            &nbsp;
                                        </td>
                                                <td class='sideHead1' width="30%">
                                <div id="divBranchplant" runat="server" class="autocomplete_completionListElement">
                                    <asp:TextBox ID="txtBranchplant" runat="server" Width="195px" MaxLength='100' AutoPostBack="true"
                                        onFocus="this.select()" onclick="this.select()" />
                                    <asp:HiddenField ID="hdfBranchID" runat="server" />
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtBranchplant" runat="server" DelimiterCharacters=""
                                        Enabled="True" TargetControlID="txtBranchplant" BehaviorID="AutoCompleteBPEmp"
                                        OnClientShown="clientListShownBPEmp" ServicePath="~/AppWeb/LookupWebService.asmx"
                                        ServiceMethod="BranchPlantLookupbyEmpID" CompletionInterval="100" MinimumPrefixLength="1"
                                        EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem"
                                        CompletionListCssClass="AutoCompleteFlyout" CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                                        UseContextKey="true">
                                    </asp:AutoCompleteExtender>                                    
                                </div>
                         
                            </td>
                                        <%--<td class='sideHead1' width="30%">
                                            <telerik:RadComboBox ID="rcbToBP" runat="server" Filter="Contains" Width="195px"
                                                DataSourceID="ObjBranchPlant" DataTextField="BranchName" DataValueField="BranchID">
                                            </telerik:RadComboBox>
                                        </td>--%>
                                             <td class='sideHead' width="20%">
                                            <asp:Label ID="Label6" Text="Item Barcode" Width="100px" runat="server" />
                                        </td>
                                        <td class='sideHead1 MandatoryField' style="width: 10px">
                                            &nbsp;
                                        </td>
                                        <td class='sideHead1' width="30%">
                                            <telerik:RadTextBox ID="txtItemBarcode" Width="195px" MaxLength='100' runat="server">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td style="border: 0px solid #9BAFC1;">
                            <div class="half-panel2">
                                <table id="Table3" cellspacing="5" cellpadding="5" align="center" border="0" runat="server">
                                    <tr>
                                        <td class='sideHead' width="20%">
                                            <asp:Label ID="Label3" Text="Item Code" runat="server" />
                                        </td>
                                        <td class='sideHead1 MandatoryField' style="width: 10px">
                                            &nbsp;
                                        </td>
                                        <td class='sideHead1' width="30%">

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
                                        
                                        </td>
                                        <td class='sideHead' width="20%">
                                            <asp:Label ID="Label4" Text="Order Number" runat="server" />
                                        </td>
                                        <td class='sideHead1 MandatoryField' style="width: 10px">
                                            &nbsp;
                                        </td>
                                        <td class='sideHead1' width="30%">
                                            <telerik:RadTextBox ID="txtOrderNo" Width="195px" MaxLength='100' runat="server">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td class='sideHead' width="20%">
                                            <asp:Label ID="Label5" Text="Order Type" runat="server" />
                                        </td>
                                        <td class='sideHead1 MandatoryField' style="width: 10px">
                                            &nbsp;
                                        </td>
                                        <td class='sideHead1' width="30%">
                                            <telerik:RadTextBox ID="txtOrderType" Width="195px" MaxLength='100' runat="server">
                                            </telerik:RadTextBox>
                                        </td>
                                             <td class='sideHead' width="20%">
                                              &nbsp;
                                        </td>
                                        <td class='sideHead1 MandatoryField' style="width: 10px">
                                            &nbsp;
                                        </td>
                                        <td class='sideHead1' width="30%">
                                         &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#ffffff" colspan="9">
                            <telerik:RadButton ID="radSearch" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Width="100px" Text="Search" runat="server" />
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="radClear" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Width="100px" Text="Clear" runat="server" CausesValidation="False" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#ffffff" style="padding: 10px 0 0 0;" colspan="2">
                            <telerik:RadGrid ID="RadgvSalesOrder" runat="server" CellSpacing="0" GridLines="None" CssClass="rgNoScrollImage"
                                Skin="WebBlue" AllowPaging="True" AllowSorting="true" AllowFilteringByColumn="false">
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>" Name="ParentGrid"
                                    NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>" DataKeyNames="OrderNumber,BranchID">
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="ItemID" Name="OrderItems" Width="100%" AutoGenerateColumns="false">
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
                                                <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter Quantity column"
                                                    HeaderText="Quantity" SortExpression="Quantity" UniqueName="Quantity">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ItemBarcode" FilterControlAltText="Filter ItemBarcode column"
                                                    HeaderText="Item Barcode" SortExpression="ItemBarcode" UniqueName="ItemBarcode">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Verified" FilterControlAltText="Filter Verified column" HeaderStyle-Width="80px"
                                                    HeaderText="Verified" SortExpression="Verified" UniqueName="Verified" AllowFiltering="false" />                                                
                                             <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="50px"
                                                UniqueName="gridSelectAll"> 
                                                <HeaderTemplate>                                                   
                                                   <asp:CheckBox ID="chkAll" runat="server" Checked="false" />
                                                </HeaderTemplate>                              
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" Checked="false" />
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            </Columns>

                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="CompanyCode" FilterControlAltText="Filter CompanyCode column"
                                            HeaderText="Company Code" SortExpression="CompanyCode" Visible ="false" UniqueName="CompanyCode">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CompanyName" FilterControlAltText="Filter CompanyName column"
                                            HeaderText="Company Name" SortExpression="CompanyName" Visible ="false" UniqueName="CompanyName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BranchPlantCode" FilterControlAltText="Filter BranchPlantCode column"
                                            HeaderText="Branch Plant Code" SortExpression="BranchPlantCode" UniqueName="BranchPlantCode">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BranchPlantName" FilterControlAltText="Filter BranchPlant column"
                                            HeaderText="Branch Plant" SortExpression="BranchPlantName" UniqueName="BranchPlantName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CustomerCode" FilterControlAltText="Filter CustomerCode column"
                                            HeaderText="Customer Code" SortExpression="CustomerCode" UniqueName="CustomerCode">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CustomerName" FilterControlAltText="Filter CustomerName column"
                                            HeaderText="Customer Name" SortExpression="CustomerName" UniqueName="CustomerName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OrderNumber" FilterControlAltText="Filter OrderNumber column"
                                            HeaderText="Order Number" SortExpression="OrderNumber" UniqueName="OrderNumber">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OrderType" FilterControlAltText="Filter OrderType column"
                                            HeaderText="Order Type" SortExpression="OrderType" UniqueName="OrderType">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OrderDate" FilterControlAltText="Filter OrderDate column"
                                            HeaderText="Order Date" SortExpression="OrderDate" UniqueName="OrderDate">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SaleOrderBarcode" FilterControlAltText="Filter SaleOrderBarcode column"
                                            HeaderText="SaleOrder Barcode" ItemStyle-Width="75px" SortExpression="SaleOrderBarcode" UniqueName="SaleOrderBarcode">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Status" FilterControlAltText="Filter Status column" 
                                            HeaderText="Status" SortExpression="Status" UniqueName="Status" AllowFiltering="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <telerik:RadButton ID="btnReprint" HoveredCssClass="normal-btnh"  CommandName="Reprint" Font-Size="10px"
                                                 Height="20px" SingleClick="true" SingleClickText="Processing..."  DisabledButtonCssClass="btnDisable"  Width="150px"
                                                 Text="Re-print Selected" runat="server"
                                                OnClientClicked="OnClientClickedConfirm" />
                                                <%--<asp:Label ID="lblAllApproved" runat="server" Text="Approved" Visible='<%# IIf(Eval("ApprovalPending") = "0", "1", "0")%>' />--%>
                                            </ItemTemplate>                                        
                                    </telerik:GridTemplateColumn>
                                         <%--<telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Reprint" HeaderStyle-Width="80px"
                                                UniqueName="gridSelectAllMain"> 
                                                <HeaderTemplate>                                                   
                                                   <asp:CheckBox ID="chkAllMain" runat="server" Checked="false" onclick="CheckAll(this);" />
                                                   Reprint?
                                                </HeaderTemplate>                              
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectAllMain" runat="server" Checked="false" />                                                    
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>--%>
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
            </td>
        </tr>
    </table>
     <asp:ObjectDataSource ID="ObjSaleOrder" runat="server" SelectMethod="GetAllTabKSaleOrdersByEmployeeIDSearch"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabKSaleOrders" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
            <asp:Parameter Name="SaleOrderNumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjDSCustomers" runat="server" SelectMethod="GetTabMCustomersByEmployeeID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMCustomers" OldValuesParameterFormatString="{0}">
        <SelectParameters>

            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjBranchPlant" runat="server" SelectMethod="GetTabMBranchPlantByEmployeeID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMBranchPlant" OldValuesParameterFormatString="{0}">
        <SelectParameters>
      
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <%-- </ContentTemplate>
     </asp:UpdatePanel>
  --%>


    
 
</asp:Content>
