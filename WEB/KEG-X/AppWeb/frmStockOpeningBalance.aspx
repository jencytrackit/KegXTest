<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="frmStockOpeningBalance.aspx.vb" Inherits="KEG_X.TrackITKTS.frmStockOpeningBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">


    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/AppWeb/LookupWebService.asmx" />
        </Services>
    </telerik:RadScriptManager>
    <style type="text/css" >
        .MyClass
        {
 
            width : 20px;
        }
</style>
    <script language="javascript" type="text/javascript">
        function redirectme(sender, args) {
            location.href = 'frmEmptyKegReceive_BPtoBP.aspx';
        }
        

        function OnClientClicked(button, args) {
//            document.forms[0].reset();
              button.set_autoPostBack(false);
              location.href = 'frmStockOpeningBalance.aspx';
        }
        function clientListShown(source, eventArgs) {
            var completionList = $find("AutoCompleteEx").get_completionList();
            completionList.style.width = 'auto';
        }
        function clientListShownBP(source, eventArgs) {
            var completionList = $find("AutoCompleteBP").get_completionList();
            completionList.style.width = 'auto';
        }
        function clientListShownCustomer(source, eventArgs) {
            var completionList = $find("AutoCompleteCustomer").get_completionList();
            completionList.style.width = 'auto';
        }
    </script>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function OnlyNumbers(e) {
                e = e || window.event;
                ch = e.which || e.keyCode;

                if (ch != null) {

                    if ((ch >= 48 && ch <= 57) || ch == 0 || ch == 8 || ch == 13 || ch == 9 || ch == 43 || ch == 45)
                        return true;
                }

                return false;
            }

            function OnClientFilesUploaded(sender, args) {
                $find('<%= RadAjaxManager1.ClientID %>').ajaxRequest();
            }


            function validationFailed(sender, args) {
                $telerik.$(".ruCancel").parent().remove();
                sender.addFileInput();
                var fileExtention = args.get_fileName().substring(args.get_fileName().lastIndexOf('.') + 1, args.get_fileName().length);
                if (args.get_fileName().lastIndexOf('.') != -1) {//this checks if the extension is correct
                    if (sender.get_allowedFileExtensions().indexOf(fileExtention)) {
                        alert(document.getElementById('<%= employExt.ClientID %>').value);
                    }

                }
                else {
                    alert(document.getElementById('<%= employExt.ClientID %>').value);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <ATS:Session ID="chkSession" runat="Server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbFromCmpny" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbFromCmpny">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="divBP" />
                    <telerik:AjaxUpdatedControl ControlID="divItems" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadgvStockOpeningBalance" />
                    <telerik:AjaxUpdatedControl ControlID="RadgvOpeningBalanceHistory" />
                    <telerik:AjaxUpdatedControl ControlID="lblMsg" />
                </UpdatedControls>
            </telerik:AjaxSetting>
                                  
            <telerik:AjaxSetting AjaxControlID="btnAdd">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divItems" />
                    <telerik:AjaxUpdatedControl ControlID="divBP" />
                    <telerik:AjaxUpdatedControl ControlID="lblMsg" />
                    <telerik:AjaxUpdatedControl ControlID="RadgvStockOpeningBalance" />
                    <telerik:AjaxUpdatedControl ControlID="RadgvOpeningBalanceHistory" />                        
                    <telerik:AjaxUpdatedControl ControlID="txtEntityType" />
                    <telerik:AjaxUpdatedControl ControlID="rbtEntityType" />
                    
                    <telerik:AjaxUpdatedControl ControlID="txtItemCode" />                    
                    <telerik:AjaxUpdatedControl ControlID="hdfItemID"  />
                    <telerik:AjaxUpdatedControl ControlID="txtItemDesc" />                        
                    <telerik:AjaxUpdatedControl ControlID="txtItemDesc2" />
                    <telerik:AjaxUpdatedControl ControlID="rntxtQuantity" />
                    <telerik:AjaxUpdatedControl ControlID="txtUOM" /> 
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="radSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMsg" />
                    <telerik:AjaxUpdatedControl ControlID="divBP" />
                    <telerik:AjaxUpdatedControl ControlID="divItems" />
                    <telerik:AjaxUpdatedControl ControlID="RadgvStockOpeningBalance" />
                    <telerik:AjaxUpdatedControl ControlID="RadgvOpeningBalanceHistory" />

                    <telerik:AjaxUpdatedControl ControlID="txtEntityType" />
                    <telerik:AjaxUpdatedControl ControlID="txtItemCode" />                    
                    <telerik:AjaxUpdatedControl ControlID="hdfItemID"  />
                    <telerik:AjaxUpdatedControl ControlID="txtItemCode"  />
                    <telerik:AjaxUpdatedControl ControlID="txtItemDesc" />                        
                    <telerik:AjaxUpdatedControl ControlID="txtItemDesc2" />
                    <telerik:AjaxUpdatedControl ControlID="rntxtQuantity" />
                    <telerik:AjaxUpdatedControl ControlID="txtUOM" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="rbtEntityType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblBrachPlant" />
                    <telerik:AjaxUpdatedControl ControlID="divBP" />
                    <telerik:AjaxUpdatedControl ControlID="hdfItemID"  />
                    <telerik:AjaxUpdatedControl ControlID="txtItemCode"  />
                    <telerik:AjaxUpdatedControl ControlID="txtItemDesc" />                        
                    <telerik:AjaxUpdatedControl ControlID="txtItemDesc2" />
                    <telerik:AjaxUpdatedControl ControlID="rntxtQuantity" />
                    <telerik:AjaxUpdatedControl ControlID="txtUOM" />                   
                    <telerik:AjaxUpdatedControl ControlID="lblMsg" />
                    <telerik:AjaxUpdatedControl ControlID="RadgvStockOpeningBalance" />
                    <telerik:AjaxUpdatedControl ControlID="RadgvOpeningBalanceHistory" />

                </UpdatedControls>
            </telerik:AjaxSetting> 
            
             <telerik:AjaxSetting AjaxControlID="txtEntityType">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="divBP" />
                    <telerik:AjaxUpdatedControl ControlID="divItems" />
                    <telerik:AjaxUpdatedControl ControlID="hdfItemID"  />                    
                    <telerik:AjaxUpdatedControl ControlID="txtItemDesc" />                        
                    <telerik:AjaxUpdatedControl ControlID="txtItemDesc2" />
                    <telerik:AjaxUpdatedControl ControlID="rntxtQuantity" />
                    <telerik:AjaxUpdatedControl ControlID="txtUOM" />                   
                    <telerik:AjaxUpdatedControl ControlID="lblMsg" />
                    <telerik:AjaxUpdatedControl ControlID="RadgvStockOpeningBalance" />
                    <telerik:AjaxUpdatedControl ControlID="RadgvOpeningBalanceHistory" />

                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="txtItemCode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divItems" />
                    <telerik:AjaxUpdatedControl ControlID="hdfItemID"  />                    
                    <telerik:AjaxUpdatedControl ControlID="txtItemDesc" />                        
                    <telerik:AjaxUpdatedControl ControlID="txtItemDesc2" />
                    <telerik:AjaxUpdatedControl ControlID="rntxtQuantity" />
                    <telerik:AjaxUpdatedControl ControlID="txtUOM" />                   
                    <telerik:AjaxUpdatedControl ControlID="lblMsg" />
                    <telerik:AjaxUpdatedControl ControlID="RadgvStockOpeningBalance" />
                    <telerik:AjaxUpdatedControl ControlID="RadgvOpeningBalanceHistory" />
                </UpdatedControls>
            </telerik:AjaxSetting> 
             
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" >        
    </telerik:RadAjaxLoadingPanel>
    <asp:HiddenField ID="employExt" Value="Please select excel sheet, types accepted are .xls,.xlsx"
        runat="server" />
    <h2>
        <asp:Label ID="addATSEmployees" Text="Empty Kegs Opening Balance" runat="server" /></h2>

<%--<asp:UpdatePanel ID="upnl" runat="server">
    <ContentTemplate>    --%>
        <table id="TABLE100" cellspacing="0" cellpadding="0" height="100%" width="100%" border="0"
        runat="server">
        <tr>
            <td height="450px"  width="100%"  valign="Top">
                <div>
                    <table id="TABLE1" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                        class="table" rules='none' width="100%" border='0' runat="server">
                        <tr>
                            <td bgcolor='white' colspan="4">
                                <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label5" Text="Company" runat="server" />*
                            </td>
                            <td class='sideHead1' width="35%">
                                <telerik:RadComboBox ID="rcbFromCmpny" runat="server" AutoPostBack="true" Filter="Contains"
                                    Width="195px">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='rfvrdcCmpnny' ControlToValidate='rcbFromCmpny' ErrorMessage="Company is Required"
                                    Display='None' runat='server' ValidationGroup="Add" InitialValue="--Select One--" />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="lblActive" Text="Entity Type" runat="server" />*
                            </td>
                            <td class='sideHead1'>
                                <asp:RadioButtonList ID="rbtEntityType" runat="server" Width="350px" RepeatDirection="Horizontal"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="1" Text="Branch Plant" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Customer"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Supplier"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblBrachPlant" Text="" runat="server" />*
                            </td>
                            <td class='sideHead1'>
                                <div id="divBP" runat="server" class="autocomplete_completionListElement">
                                    <asp:TextBox ID="txtEntityType" runat="server" Width="195px" MaxLength='100' AutoPostBack="true"
                                        onFocus="this.select()" onclick="this.select()" />
                                    <asp:HiddenField ID="hdfEntityType" runat="server" />
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtEntityType" runat="server"
                                        DelimiterCharacters="" Enabled="True" TargetControlID="txtEntityType" BehaviorID="AutoCompleteBP"
                                        OnClientShown="clientListShownBP" ServicePath="~/AppWeb/LookupWebService.asmx"
                                        ServiceMethod="BranchPlantLookup" CompletionInterval="100" MinimumPrefixLength="1"
                                        EnableCaching="true" CompletionSetCount="8" FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem"
                                        CompletionListCssClass="AutoCompleteFlyout" CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                                        UseContextKey="true">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator4' ControlToValidate='txtEntityType'
                                    ValidationGroup="Add" ErrorMessage="Entity Type is Required" Display='None' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
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
                            <td class='sideHead1'>
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
                                <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ValidationGroup="Add"
                                    ErrorMessage="Item Code is Required" ControlToValidate="txtItemCode" Display="None"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblItemDescription" Text="Item Name1" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtItemDesc" Width="195px" MaxLength='100' runat="server"
                                    Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvItemDesc" runat="server" ValidationGroup="Add"
                                    ErrorMessage="Item Name is Required" ControlToValidate="txtItemDesc" Display="None"></asp:RequiredFieldValidator>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label2" Text="Item Name2" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtItemDesc2" Width="195px" MaxLength='100' runat="server"
                                    Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Add"
                                    ErrorMessage="Item Name2 is Required" ControlToValidate="txtItemDesc2" Display="None"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblQuantity" Text="Qty(On Hand)" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                
                                <telerik:RadNumericTextBox ID="rntxtQuantity" runat="server" DataType="System.Int32" NumberFormat-DecimalDigits="0" MinValue="0">                                    
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvQty" runat="server" ValidationGroup="Add" ErrorMessage="Quantity(On-Hand) is Required"
                                    ControlToValidate="rntxtQuantity" Display="None"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rfqty" runat="server" ControlToValidate="rntxtQuantity"
                                    ErrorMessage="Quantity should b greater than 0" ValidationExpression="^\d+$"
                                    ValidationGroup="Add" ForeColor="Red" Font-Size="Small"></asp:RegularExpressionValidator>
                            </td>
                            <%--        <td class='sideHead' width="20%">
                                <asp:Label ID="Label1" Text="Qty(In Transit)" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="RadTextBox1" Width="195px" MaxLength='100' runat="server"
                                    onkeyPress="return OnlyNumbers(event);">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Add"
                                    ErrorMessage="Quantity(In-Transit) is Required" ControlToValidate="RadTextBox1"
                                    Display="None"></asp:RequiredFieldValidator>
                                      <asp:RegularExpressionValidator ID="rfIqty" runat="server" ControlToValidate ="RadTextBox1" ErrorMessage="Quantity should b greater than 0" ValidationExpression ="^\d+$" ValidationGroup ="Add"
                                                     ForeColor ="Red" Font-Size="Small"></asp:RegularExpressionValidator>
                            </td>--%>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblUOM" Text="UOM" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtUOM" Width="195px" MaxLength='100' runat="server" Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator3' ControlToValidate='txtUOM'
                                    ValidationGroup="Add" ErrorMessage="UOM is Required" Display='None' runat='server' />
                            </td>
                            <td class='sideHead' width="20%">
                                &nbsp;
                            </td>
                            <td width="30%">
                                <telerik:RadButton ID="btnAdd" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                    Width="100px" Text="Add" runat="server" ValidationGroup="Add" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
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
                                <asp:Label ID="lblBrowseFile" Text="Browse File" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <telerik:RadAsyncUpload ID="AsyncUpload1" runat="server" OnClientFilesUploaded="OnClientFilesUploaded"
                                            Localization-Clear="" OnClientValidationFailed="validationFailed" Skin="Default"
                                            AutoAddFileInputs="false">
                                            <FileFilters>
                                                <telerik:FileFilter Description="Pictures(xls;xlsx)" Extensions="xls,xlsx" />
                                            </FileFilters>
                                        </telerik:RadAsyncUpload>
                                        <asp:Label ID="lblFileName" runat="server" ForeColor="Red" />
                                        <asp:Label ID="lblSuccess" runat="server" ForeColor="Red" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnImport" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td width="20%">
                                <telerik:RadButton ID="btnImport" HoveredCssClass="normal-btnh" ForeColor="White"
                                    Font-Size="17px" Width="100px" Text="Import" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
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
                            <td colspan="4">
                          
                             <telerik:RadGrid ID="RadgvStockOpeningBalance" runat="server" GridLines ="Both" MasterTableView-EditFormSettings-EditColumn-Reorderable="true"   CellSpacing="0"  
                                    AllowPaging="false" Skin="WebBlue" ShowHeadersWhenNoRecords="true" >
                                    <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
                                         NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>" ShowHeadersWhenNoRecords="true" GridLines="Both"   EditFormSettings-EditColumn-Resizable="true">
                                        
                                     
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
                                            <telerik:GridTemplateColumn AllowFiltering="false"  HeaderText="Quantity (On-Hand)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity"  runat="server" Width="100px" Text='<%# Eval("OnHandQuantity") %>'
                                                        onkeyPress="return OnlyNumbers(event);" Enabled="false"></asp:TextBox>
                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQuantity"
                                                        ErrorMessage="Quantity should b greater than 0" ValidationExpression="^\d+$"
                                                        ValidationGroup="Save" ForeColor="Red" Font-Size="Small"></asp:RegularExpressionValidator>--%>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />

                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Quantity (In-Transit)"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInsQuantity" runat="server" Width="100px"  Text='<%# Eval("InTransitQuantity") %>'
                                                        onkeyPress="return OnlyNumbers(event);" Enabled="false"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtInsQuantity"
                                                        ErrorMessage="Quantity should b greater than 0" ValidationExpression="^\d+$"
                                                        ValidationGroup="Save" ForeColor="Red" Font-Size="Small"></asp:RegularExpressionValidator>
                                                </ItemTemplate>

                                            </telerik:GridTemplateColumn>
                                                 
                                            <telerik:GridTemplateColumn AllowFiltering="false"   HeaderText="Adjustment">
                                                <ItemTemplate>
                                               <%--    <telerik:RadNumericTextBox ID="txtAdjustmentQuantity" runat="server" Width="100px" DataType="System.Int32" NumberFormat-DecimalDigits="0" Enabled='<%# IIF(Eval("InventoryID")=0,false,true) %>' >                                    
                                                   </telerik:RadNumericTextBox> --%> 
                                                          <asp:TextBox ID="txtAdjustmentQuantity" Width="100px" runat="server"></asp:TextBox>                                       
                                                </ItemTemplate>
                                                                                             
                                            </telerik:GridTemplateColumn>                                                                               


                                            <telerik:GridTemplateColumn AllowFiltering="false" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgridItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" Visible="false" UniqueName="InventoryID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInventoryID" runat="server" Text='<%# Eval("InventoryID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                     
                                           <telerik:GridTemplateColumn HeaderText="Reason">
                                           <ItemTemplate>
                                            <asp:TextBox ID="txtAdjustReason" runat="server" Width="205px"  MaxLength ="25" Enabled='<%# IIF(Eval("InventoryID")=0,false,true) %>'>
                                            
                                            </asp:TextBox>
                                           
                                           </ItemTemplate>
                                           
                                           </telerik:GridTemplateColumn>


                                        </Columns>
                                        
                                    </MasterTableView>
                                   
                                </telerik:RadGrid>
                          
                             
                            </td>
                        </tr>
                        <tr>
                            <td align="center" bgcolor="#ffffff" colspan="4">
                                <telerik:RadButton ID="radSave" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" ValidationGroup="Save"
                                    Width="100px" Text="<%$ Resources:lang, btnSave %>" runat="server" CausesValidation="true"
                                    AutoPostBack="true" />
                                &nbsp;&nbsp;
                                <telerik:RadButton ID="radClear" HoveredCssClass="normal-btnh" ForeColor="White"
                                    Font-Size="17px" Width="100px" Text="<%$ Resources:lang, btnClear %>" runat="server"
                                    OnClientClicked="OnClientClicked" CausesValidation="False" />
                                &nbsp;&nbsp;
                                <telerik:RadButton ID="radCancel" HoveredCssClass="normal-btnh" ForeColor="White"
                                    Font-Size="17px" runat="server" Width="100px" AutoPostBack="false" Text="<%$ Resources:lang, btnCancel %>"
                                    CausesValidation="False" Visible="false" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <telerik:RadGrid ID="RadgvOpeningBalanceHistory" runat="server" CellSpacing="0" GridLines="None"
                                    AllowPaging="true" Skin="WebBlue" ShowHeadersWhenNoRecords="true">
                                    <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
                                        NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>" ShowHeadersWhenNoRecords="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="TransactionID" FilterControlAltText="Filter TransactionID column"
                                                HeaderText="TransactionID" SortExpression="TransactionID" UniqueName="TransactionID">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Type" FilterControlAltText="Filter Type column"
                                                HeaderText="Type" SortExpression="Type" UniqueName="Type">
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
                                                HeaderText="Quantity (On-Hand)" SortExpression="Quantity" UniqueName="Quantity">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="InTransitQuantity" FilterControlAltText="Filter InTransitQuantity column"
                                                Visible="false" HeaderText="Quantity (In-Transit)" SortExpression="InTransitQuantity"
                                                UniqueName="InTransitQuantity">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AdjustmentQuantity" FilterControlAltText="Filter AdjustmentQuantity column"
                                                HeaderText="Adjustment" SortExpression="AdjustmentQuantity" UniqueName="AdjustmentQuantity">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CreatedDate" FilterControlAltText="Filter CreatedDate column"
                                                HeaderText="Created Date" SortExpression="CreatedDate" UniqueName="CreatedDate">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CreatedUser" FilterControlAltText="Filter CreatedUser column"
                                                HeaderText="Created User" SortExpression="CreatedUser" UniqueName="CreatedUser">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ModifiedDate" FilterControlAltText="Filter ModifiedDate column"
                                                HeaderText="Modified Date" SortExpression="ModifiedDate" UniqueName="ModifiedDate">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ModifiedUser" FilterControlAltText="Filter ModifiedUser column"
                                                HeaderText="Modified User" SortExpression="ModifiedUser" UniqueName="ModifiedUser">
                                            </telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn DataField="AdjustmentReason" FilterControlAltText="Filter AdjustmentReason column"
                                                HeaderText="Adjustment Reason" SortExpression="AdjustmentReason" UniqueName="AdjustmentReason">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
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
                    <telerik:RadTextBox ID="txtTransactionNumber" runat="server" Visible="false">
                    </telerik:RadTextBox>
                </div>
            </td>
        </tr>
    </table>
   <%-- </ContentTemplate>
</asp:UpdatePanel>--%>

</asp:Content>
