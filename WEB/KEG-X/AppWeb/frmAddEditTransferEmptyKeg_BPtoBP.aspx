<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master" MaintainScrollPositionOnPostback ="true"
    CodeBehind="frmAddEditTransferEmptyKeg_BPtoBP.aspx.vb" Inherits="KEG_X.TrackITKTS.frmAddEditTransferEmptyKeg_BPtoBP" %>
        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">
        function redirectme(sender, args) {
            location.href = 'frmEmptyKegTransfer_BPtoBP.aspx';
        }
        function OnClientClicked(button, args) {
//            document.forms[0].reset();
//            button.set_autoPostBack(false);
            location.href = "<%= Request.RawUrl %>";

        }
        function clientListShown(source, eventArgs) {
            var completionList = $find("AutoCompleteEx").get_completionList();
            completionList.style.width = 'auto';
        }
       
        function clientListShownBP(source, eventArgs) {
            var completionList = $find("AutoCompleteBP").get_completionList();
            completionList.style.width = 'auto';
        }
    </script>
    <ATS:Session ID="chkSession" runat="Server" />
      <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
        <Services>
            <asp:ServiceReference Path="~/AppWeb/LookupWebService.asmx" />
        </Services>
    </telerik:RadScriptManager>

    <h2>
        <asp:Label ID="addATSEmployees" Text="Transfer Empty Kegs" runat="server" /></h2>
    <em>* Required Fields</em>
    <asp:UpdatePanel ID="updatepanel1" runat ="server">
    <ContentTemplate >
    <table id="TABLE100" cellspacing="0" cellpadding="0" height="100%" width="100%" border="0"
        runat="server">
        <tr>
            <td valign="Top">
                <ATSMenu:Menu ID="MENULIST" runat="Server" />
                <%-- <iiscc:SiteCompass ID="SiteCompass12" runat="server" CssClass="compassStyle1" DisplayTitle="<%$ Resources:lang, addATSEmployees %>"
                    CssClassLink="compassLinkStyle1" MaxItems="5" DivName="compassDiv"></iiscc:SiteCompass>--%>
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
                                <asp:RequiredFieldValidator ID='rfvrdcCmpnny' ControlToValidate='rcbFromCmpny' ErrorMessage="Company is Required"
                                    Display='None' runat='server' ValidationGroup="Add" InitialValue="--Select One--" />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label1" Text="From Branch Plant" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">

                                   <div id="divFromBP" runat="server" class="autocomplete_completionListElement">
                                    <asp:TextBox ID="txtFromBP" runat="server" Width="195px" MaxLength='100' AutoPostBack="true"
                                        onFocus="this.select()" onclick="this.select()" />
                                    <asp:HiddenField ID="hdfFromBPID" runat="server" />
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtFromBP" runat="server" DelimiterCharacters=""
                                        Enabled="True" TargetControlID="txtFromBP" BehaviorID="AutoCompleteBP" OnClientShown="clientListShownBP"
                                        ServicePath="~/AppWeb/LookupWebService.asmx" ServiceMethod="BranchPlantLookup"
                                        CompletionInterval="100" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="8"
                                        FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem" CompletionListCssClass="AutoCompleteFlyout"
                                        CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem" UseContextKey="true">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator1' ControlToValidate='txtFromBP'
                                    ErrorMessage="From BranchPlant is Required" Display='None' runat='server' ValidationGroup="Add" />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label7" Text="To Company" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">

                           


                                <telerik:RadComboBox ID="rcbToCmpny" runat="server" Filter="Contains" AutoPostBack="true" Width="195px">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='rfvrcToCmpny' ControlToValidate='rcbToCmpny' ErrorMessage="Company is Required"
                                    Display='None' runat='server' ValidationGroup="Add" InitialValue="--Select One--" />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label2" Text="To Branch Plant" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">

                             <div id="div1" runat="server" class="autocomplete_completionListElement">
                                    <asp:TextBox ID="txtToBP" runat="server" Width="195px" MaxLength='100' AutoPostBack="true"
                                        onFocus="this.select()" onclick="this.select()" />
                                    <asp:HiddenField ID="hdfToBPID" runat="server" />
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender_txtToBP" runat="server" DelimiterCharacters=""
                                        Enabled="True" TargetControlID="txtToBP" BehaviorID="AutoCompleteBP" OnClientShown="clientListShownBP"
                                        ServicePath="~/AppWeb/LookupWebService.asmx" ServiceMethod="BranchPlantLookup"
                                        CompletionInterval="100" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="8"
                                        FirstRowSelected="true" CompletionListItemCssClass="AutoCompleteFlyoutItem" CompletionListCssClass="AutoCompleteFlyout"
                                        CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem" UseContextKey="true">
                                    </asp:AutoCompleteExtender>
                                </div>
                          <%--      <telerik:RadComboBox ID="rcbToBP" runat="server" Filter="Contains" Width="195px">
                                </telerik:RadComboBox>--%>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator2' ControlToValidate='txtToBP'
                                    ErrorMessage="To BranchPlant is Required" Display='None' runat='server' ValidationGroup="Add"/>
                                <telerik:RadTextBox ID="txtTransactionNumber" runat="server" Visible="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead'>
                                <asp:Label ID="Label3" Text="Transfer Date" runat="server" />*
                            </td>
                            <td class='sideHead1' colspan="3">
                                <telerik:RadDatePicker ID="dpTransferDate" runat="server" Width="195px" DateInput-DateFormat="MM/dd/yyyy" ShowPopupOnFocus ="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator7' ControlToValidate='dpTransferDate'
                                    ErrorMessage="Transfer Date is Required" Display='None' runat='server' ValidationGroup="Save" />
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
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator8' ControlToValidate='txtItemCode'
                                        ErrorMessage="ItemCode is Required" Display='None' runat='server' ValidationGroup="Add" />
                                <%--<telerik:RadTextBox ID="txtItemCode" runat="server" Width="195px" AutoPostBack="true">
                                </telerik:RadTextBox>--%>
                                <telerik:RadTextBox ID="txtItemID" runat="server" Visible="false">
                                </telerik:RadTextBox>
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
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label11" Text="Item Name2" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtItemName2" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator13' ControlToValidate='txtItemName2'
                                    ErrorMessage="Item Name2 is Required" Display='None' runat='server' ValidationGroup="Add" />
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblQuantity" Text="Quantity" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtQuantity" runat="server" Width="195px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator5' ControlToValidate='txtQuantity'
                                    ErrorMessage="Quantity is Required" Display='None' runat='server' ValidationGroup="Add" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQuantity"
                                    ErrorMessage="Quantity should b greater than 0" ValidationExpression="^\d+$"
                                    ValidationGroup="Add" ForeColor="Red" Font-Size="Small"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblUOM" Text="UOM" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtUOM" Width="195px" MaxLength='100' runat="server" Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator6' ControlToValidate='txtUOM'
                                    ValidationGroup="Add" ErrorMessage="UOM is Required" Display='None' runat='server' />
                            </td>
                            <td class='sideHead'>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAdd" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                    Width="100px" Text="Add" runat="server" ValidationGroup="Add" />
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
                                               <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                    </ItemTemplate>                                                
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="EBPOrderID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEBPOrderID" runat="server" Text='<%# Eval("EBPOrderID") %>'></asp:Label>
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
                                    Width="100px" Text="<%$ Resources:lang, btnSave %>" ValidationGroup="Save" runat="server" />
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
    
   <%-- <asp:ObjectDataSource ID="ObjBranchPlant" runat="server" SelectMethod="GetTabMBranchPlantByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMBranchPlant" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
</asp:Content>
