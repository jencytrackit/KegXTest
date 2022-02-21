<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmEmptyCustomerApproval.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmEmptyCustomerApproval" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">

        function OnClientClickedConfirmApproval(button, args) {
            if (window.confirm("Are you sure? Do you want to continue?")) {
                button.set_autoPostBack(true);
            }
            else {
                button.set_autoPostBack(false);
                button.enableAfterSingleClick();
            }
        } function OnClientClickedConfirmUpdate(button, args) {
            if (window.confirm("Are you sure to update the transaction details?")) {
                button.set_autoPostBack(true);
            }
            else {
                button.set_autoPostBack(false);
                button.enableAfterSingleClick();
            }
        }
        var mycombo;
        var txtID;
        var txtName;
        function CustomerChanged(sender, args) {
            mycombo = sender;

            txtID = $("#" + mycombo.get_id()).parent().parent().parent().parent().parent().parent().find('input')[0].id;
            txtName = $("#" + mycombo.get_id()).parent().parent().parent().parent().parent().parent().find('input')[1].id;
            $("#" + txtID).get(0).value = "0";
            $("#" + txtName).get(0).value = "NewName";


            var txtName = sender.get_attributes().getAttribute("Value");
            $("#" + txtName).get(0).value = "NewName";

        }

        function EnableSingleClick(b, args) {
            var button =  $find("<%= radSearch.ClientID %>");
            
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

        function clientListShownCustomer(source, eventArgs) {
            var completionList = $find("AutoCompleteCustomer").get_completionList();
            completionList.style.width = 'auto';
        }
        function clientListShownItemCodeEmp(source, eventArgs) {
            var completionList = $find("AutoCompleteItemCodeEmp").get_completionList();
            completionList.style.width = 'auto';
        }


        function OnlyNumbers(e) {
            e = e || window.event;
            ch = e.which || e.keyCode;

            if (ch != null) {

                if ((ch >= 48 && ch <= 57) || ch == 0 || ch == 8 || ch == 13 || ch == 9 || ch == 43)
                    return true;
            }

            return false;
        }




        //        function SetHeaderCheckBox(checkbox, parentIndex) {
        //            var masterTable = $find('<%= RadgvEmptyCustomerApproval.ClientID %>').get_masterTableView();
        //            var nestedView = masterTable.get_dataItems()[parentIndex].get_nestedViews()[0];
        //            for (var i = 0; i < nestedView.get_dataItems().length; i++) {
        //                if (!nestedView.get_dataItems()[i].findElement("chkselect").checked) {

        //                    nestedView.HeaderRow.getElementsByTagName('input')[0].checked = false;
        //                    return;

        //                }
        //            }

        //            nestedView.HeaderRow.getElementsByTagName('input')[0].checked = true;

        //        }

        //        function SelectAllCheckBox(checkbox, parentIndex) {
        //            var masterTable = $find('<%= RadgvEmptyCustomerApproval.ClientID %>').get_masterTableView();
        //            var nestedView = masterTable.get_dataItems()[parentIndex].get_nestedViews()[0];
        //            for (var i = 0; i < nestedView.get_dataItems().length; i++) {

        //                nestedView.get_dataItems()[i].findElement("chkselect").checked = checkbox.checked;

        //            }
        //        }
        //       
    </script>
    <ATS:Session ID="chkSession" runat="Server" />
    <div class="f-left">
    </div>
    <div id="first-div" class="div-right">
    </div>
    <div id="second-div" class="div-left">
        <h2>
            <asp:Literal ID="Literal1" runat="server" Text="Empty Keg Customer Returns Approval" /></h2>
    </div>
    <div style="clear: both;">
    </div>
    <ATS:Session ID="Session1" runat="Server" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="36000">
        <Services>
            <asp:ServiceReference Path="~/AppWeb/LookupWebService.asmx" />
        </Services>
    </telerik:RadScriptManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
             <telerik:AjaxSetting AjaxControlID="RadgvEmptyCustomerApproval">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="RadgvEmptyCustomerApproval" />
                    <telerik:AjaxUpdatedControl ControlID="lblMsg"  />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radSearch">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="RadgvEmptyCustomerApproval" />
                    <telerik:AjaxUpdatedControl ControlID="lblMsg"  />
                </UpdatedControls>
            </telerik:AjaxSetting>

          <%--  <telerik:AjaxSetting AjaxControlID="btnApproveSelected">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadgvEmptyCustomerApproval" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
        <ClientEvents OnResponseEnd="EnableSingleClick" OnRequestStart="DisableCtrl" />
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

<%--
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"> 
         <ProgressTemplate>     
            <div style="position:absolute; font-size:large; display :none ; color : Green">loading.....</div> 
        </ProgressTemplate> 
    </asp:UpdateProgress> --%>
   
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table id="TABLE100" cellspacing="0" cellpadding="0" height="100%" width="100%" border="0"
                runat="server">
                <tr>
                    <td style="padding: 10px 0 0 0;">
                        <asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />
                        <table id="Table2" cellspacing="1" cellpadding="1" align="center" border="0" runat="server">
                            <tr>
                                <td style="border: 0px solid #9BAFC1;">
                                    <div class="half-panel">
                                        <table id="tablemainnew" cellspacing="5" cellpadding="5" width="100%" align="center"
                                            border="0" runat="server">
                                            <tr>
                                                <td class='sideHead' width="20%">
                                                    <asp:Label ID="Label2" Text="Customer" runat="server" />
                                                </td>
                                                <td class='sideHead1' width="50%">
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
                                                    <asp:RequiredFieldValidator ID='RequiredFieldValidator2' ControlToValidate='txtCustomer'
                                                        ErrorMessage="Customer is Required" Display='None' runat='server' ValidationGroup="Add" />
                                                </td>
                                                <td class='sideHead'>
                                                    <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
                                                </td>
                                                <td class='sideHead1'>
                                                    <telerik:RadDatePicker ID="dpTransactionDate" runat="server" Width="195px" ShowPopupOnFocus="true">
                                                        <DateInput ID="DateInput1" DateFormat="MM/dd/yyyy" DisplayDateFormat="MM/dd/yyyy"
                                                            runat="server">
                                                        </DateInput>
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td class='sideHead' width="10%">
                                                    <%--<asp:Label ID="lblCompanyCode" runat="server" Width="100px" Text="Company Code"></asp:Label>--%>
                                                </td>
                                                <td class='sideHead1' colspan="2">
                                                    <asp:RadioButtonList ID="rbnApp" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                                        Width="300px">
                                                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Not Approved" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="All" Value="2" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td style="border: 0px solid #9BAFC1;">
                                    <div class="half-panel2">
                                        <table id="Table4" cellspacing="5" cellpadding="5" align="center" border="0" runat="server">
                                            <tr>
                                                <td class='sideHead' width="20%">
                                                    <asp:Label ID="lblItemCode" runat="server" Text="Item Code"></asp:Label>
                                                </td>
                                                <td class='sideHead1' width="30%">
                                                    <div id="divItemCode" runat="server" class="autocomplete_completionListElement">
                                                        <asp:TextBox ID="txtItemCode" runat="server" onFocus="this.select()" onclick="this.select()"
                                                            Width="195px" MaxLength='100' AutoPostBack="true"></asp:TextBox>
                                                        <asp:HiddenField ID="hdfItemCode" runat="server" />
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtItemCode" runat="server" DelimiterCharacters=""
                                                            Enabled="True" TargetControlID="txtItemCode" BehaviorID="AutoCompleteItemCodeEmp"
                                                            OnClientShown="clientListShownItemCodeEmp" ServicePath="~/AppWeb/LookupWebService.asmx"
                                                            ServiceMethod="ItemsLookupByEmpID" CompletionInterval="100" MinimumPrefixLength="1"
                                                            EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem"
                                                            CompletionListCssClass="AutoCompleteFlyout" CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                                                            UseContextKey="true">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </td>
                                                <td class='sideHead'>
                                                    <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
                                                </td>
                                                <td class='sideHead1'>
                                                    <telerik:RadComboBox ID="rcbUserName" runat="server" Filter="Contains" Width="195px">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td class='sideHead'>
                                                    &nbsp;
                                                </td>
                                                <td class='sideHead1'>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" bgcolor="#ffffff" colspan="6">
                                    <telerik:RadButton ID="radSearch" HoveredCssClass="normal-btnh" ForeColor="White"
                                        Font-Size="17px" Width="100px"  SingleClick="true" SingleClickText="wait..."  DisabledButtonCssClass="btnDisable"  Text="Search" runat="server" />
                                    &nbsp;&nbsp;
                                    <telerik:RadButton ID="radClear" HoveredCssClass="normal-btnh" ForeColor="White"
                                        Font-Size="17px" Width="100px" Text="Clear" runat="server" CausesValidation="False" />
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                        <br />
                        <telerik:RadGrid ID="RadgvEmptyCustomerApproval" runat="server" CellSpacing="0" GridLines="None"
                            Skin="WebBlue" AllowPaging="True" AllowSorting="true" AllowFilteringByColumn="false"
                            Width="100%">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
                                TableLayout="Auto" Name="ParentGrid" NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>"
                                DataKeyNames="TransactionNum">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="FromCompanyID" Display="false" FilterControlAltText="Filter CompanyID column"
                                        HeaderText="FromCompanyID" SortExpression="FromCompanyID" UniqueName="FromCompanyID">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ApprovalID" FilterControlAltText="Filter ApprovalID column"
                                        Visible="false" HeaderText="ApprovalID" SortExpression="ApprovalID" UniqueName="ApprovalID">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TransactionNum" FilterControlAltText="Filter TransactionNumber column"
                                        HeaderStyle-Width="80px" HeaderText="Transaction Number" SortExpression="TransactionNum"
                                        UniqueName="TransactionNum">
                                    </telerik:GridBoundColumn>
                                     <%--<telerik:GridBoundColumn DataField="CustomerCode" Display="false" FilterControlAltText="Filter CustomerCode column"
                                        HeaderText="Customer Code" SortExpression="CustomerCode" UniqueName="CustomerCode">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CustomerName" FilterControlAltText="Filter CustomerName column"
                                        HeaderStyle-Width="200px" HeaderText="Customer Name" SortExpression="CustomerName"
                                        UniqueName="CustomerName">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn DataField="FromCustomerID" Display="false" UniqueName="FromCustomerID">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Customer" DataField="CustomerName" AllowFiltering="false">
                                        <ItemTemplate>
                                            <telerik:RadComboBox ID="cbxCustomer" runat="server" Filter="Contains" Width="250px"
                                                Enabled='<%# Eval("ApprovalPending") %>'>
                                            </telerik:RadComboBox>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <%--        
                                    <telerik:GridBoundColumn DataField="BranchCode" Display="false" FilterControlAltText="Filter BranchCode column"
                                        HeaderText="Branch Plant Code" SortExpression="BranchCode"
                                        UniqueName="BranchCode">
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="BranchName" FilterControlAltText="Filter BranchName column"
                                        HeaderStyle-Width="100px" HeaderText="Branch Plant Name" SortExpression="BranchName"
                                        UniqueName="BranchName">>
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn DataField="ToCompanyID" Display="false" UniqueName="ToCompanyID">
                                    </telerik:GridBoundColumn>  
                                    <telerik:GridBoundColumn DataField="BranchID" Display="false" UniqueName="BranchID">
                                    </telerik:GridBoundColumn>                                  
                                    <telerik:GridTemplateColumn HeaderText="Branch" DataField="BranchName" AllowFiltering="false">
                                        <ItemTemplate>
                                            <telerik:RadComboBox ID="cbxBranch" runat="server" Filter="Contains" Width="250px"
                                                Enabled='<%# Eval("ApprovalPending") %>'>
                                            </telerik:RadComboBox>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ReceiveDate" FilterControlAltText="Filter ReceiveDate column"
                                        HeaderStyle-Width="80px" HeaderText="Transaction Date" SortExpression="ReceiveDate"
                                        UniqueName="ReceiveDate">
                                    </telerik:GridBoundColumn>
                                    <%--       <telerik:GridBoundColumn DataField="ApprovedOn" FilterControlAltText="Filter ApprovedOn column"
                                        HeaderStyle-Width="70px" HeaderText="Collection Date" SortExpression="ApprovedOn"
                                        UniqueName="ApprovedOn">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn DataField="UserName" FilterControlAltText="Filter UserName column"
                                        HeaderStyle-Width="70px" HeaderText="User Name" SortExpression="UserName" UniqueName="UserName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalQuantity" FilterControlAltText="Filter TotalQuantity column"
                                        HeaderStyle-Width="70px" HeaderText="Actual Quantity" SortExpression="TotalQuantity"
                                        HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" UniqueName="TotalQuantity">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ApprovedQuantity" FilterControlAltText="Filter ApprovedQuantity column"
                                        HeaderStyle-Width="70px" HeaderText="Approved Quantity" SortExpression="ApprovedQuantity"
                                        HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" UniqueName="ApprovedQuantity">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Batch" FilterControlAltText="Filter Batch column"
                                        HeaderStyle-Width="50px" HeaderText="Batch" SortExpression="Batch" UniqueName="Batch"
                                        AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn>
                                        <ItemTemplate>
<%--                                            <asp:Button ID="btnApproveSelected" runat="server" Text="Approve Selected" CommandName="ApproveSelected"
                                                Visible='<%# Eval("ApprovalPending") %>' OnClientClick="return confirm('Are you sure? Do you want to continue?');" />
--%>
                                        <telerik:RadButton ID="btnApproveSelected" HoveredCssClass="normal-btnh"  CommandName="ApproveSelected" Font-Size="10px"
                                         Height="20px" SingleClick="true" SingleClickText="Processing..."  DisabledButtonCssClass="btnDisable"  Width="150px"
                                        Visible='<%# Eval("ApprovalPending") %>' Text="Approve Selected" runat="server"
                                        OnClientClicked="OnClientClickedConfirmApproval" />

                                            <asp:Label ID="lblAllApproved" runat="server" Text="Approved" Visible='<%# IIf(Eval("ApprovalPending") = "0", "1", "0")%>' />
                                        </ItemTemplate>
                                        
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <ItemTemplate>
                                        <telerik:RadButton ID="btnUpdate" HoveredCssClass="normal-btnh"  CommandName="Update" Font-Size="10px"
                                         Height="20px" SingleClick="true" SingleClickText="Updating..."  DisabledButtonCssClass="btnDisable"  Width="50px"
                                        Visible='<%# Eval("ApprovalPending") %>' Text="Update" runat="server"
                                        OnClientClicked="OnClientClickedConfirmUpdate" />
                                        </ItemTemplate>
                                        
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <DetailTables>
                                    <telerik:GridTableView Name="NestedChildItems" Width="100%" AutoGenerateColumns="false"
                                        ShowFooter="false">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ItemID" FilterControlAltText="Filter ItemID column"
                                                HeaderText="FromItemID" SortExpression="ItemID" UniqueName="ItemID" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ItemCode" FilterControlAltText="Filter ItemCode column"
                                                HeaderText="Item Code" SortExpression="ItemCode" UniqueName="ItemCode" HeaderStyle-Width="100px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ItemName1" FilterControlAltText="Filter ItemName1 column"
                                                HeaderText="Item Name1" SortExpression="ItemName1" UniqueName="ItemName1" HeaderStyle-Width="150px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ItemName2" FilterControlAltText="Filter ItemName2 column"
                                                HeaderText="Item Name2" SortExpression="ItemName2" UniqueName="ItemName2" HeaderStyle-Width="100px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UOM" FilterControlAltText="Filter UOM column"
                                                HeaderStyle-Width="50px" HeaderText="UOM" SortExpression="UOM" UniqueName="UOM">
                                            </telerik:GridBoundColumn>
                                            <%-- <telerik:GridBoundColumn DataField="Barcode" FilterControlAltText="Filter Barcode column"
                                                HeaderStyle-Width="100px" HeaderText="Barcode" SortExpression="Barcode" UniqueName="Barcode">
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn DataField="ActualQuantity" FilterControlAltText="Filter ActualQuantity column"
                                                HeaderStyle-Width="100px" HeaderText="ActualQuantity" SortExpression="ActualQuantity"
                                                UniqueName="ActualQuantity">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Actal Qty" AllowFiltering="false" Visible="false">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txtActQty" Text='<%#Eval("ActualQuantity")%>' runat="server">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="ApprovedQuantity" DataField="ApprovedQuantity"
                                                AllowFiltering="false">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txtQuantity" runat="server" Text='<%#Eval("ApprovedQuantity")%>'
                                                        onkeyPress="return OnlyNumbers(event);" Width="50px" Height="25px" TextMode="MultiLine"
                                                        Enabled='<%# IIf(Eval("ApprovalStatus") = "0", "1", "0")%>' MaxLength="5">
                                                    </telerik:RadTextBox>
                                                    <%--   <asp:CompareValidator ID="cmptxtValue" runat="server" errormessage="Approved quantity should not be greater than Actual quantity"
                                                  ControlToValidate="txtQuantity" ControlToCompare="txtActQty" Operator="LessThanEqual"></asp:CompareValidator>--%>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="ApprovedOn" FilterControlAltText="Filter ApprovedOn column"
                                                HeaderStyle-Width="80px" HeaderText="Collection Date" SortExpression="ApprovedOn"
                                                DataFormatString="{0:MM/dd/yyyy}" UniqueName="ApprovedOn">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Comments" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txtComments" Text='<%#Eval("Comments")%>' runat="server"
                                                        TextMode="MultiLine" Enabled='<%# IIf(Eval("ApprovalStatus") = "0", "1", "0")%>'>
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Approve" HeaderStyle-Width="130px"
                                                UniqueName="chkApprove">
                                                <%--  <HeaderTemplate>
                                                    <asp:CheckBox ID="checkAll" runat="server" />
                                                </HeaderTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkselect" runat="server" Enabled='<%# IIf(Eval("ApprovalStatus") = "0", "1", "0")%>'
                                                        Checked="true" />
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="From CompID" AllowFiltering="false" Visible="false">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txtFromComp" Text='<%#Eval("FromCompanyID")%>' runat="server">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="To CompID" AllowFiltering="false" Visible="false">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txtToComp" Text='<%#Eval("ToCompanyID")%>' runat="server">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="From ItmID" AllowFiltering="false" Visible="false">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txtFromItm" Text='<%#Eval("FromItemID")%>' runat="server">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="From CompID" AllowFiltering="false" Visible="false">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txtToItm" Text='<%#Eval("ToItemID")%>' runat="server">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </telerik:GridTableView>
                                </DetailTables>
                            </MasterTableView>
                            <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="false">
                                <Scrolling AllowScroll="True" SaveScrollPosition="true" UseStaticHeaders="true">
                                </Scrolling>
                                <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>
