<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="frmAddEditReceiveEmptyKeg_BPtoBP.aspx.vb" Inherits="KEG_X.TrackITKTS.frmAddEditReceiveEmptyKeg_BPtoBP1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">
        function redirectme(sender, args) {
            location.href = 'frmEmptyKegReceive_BPtoBP.aspx';
        }
        function OnClientClicked(button, args) {
//            document.forms[0].reset();
//            button.set_autoPostBack(false);
            location.href = "<%= Request.RawUrl %>";

        }
        function ChangeAllCheckBoxStatesView(id) {
            var masterTable = $find("<%= RadgvEmptyKegReceive.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == true) {
                //alert(row.length);
                if (confirm("Are you sure you want to complete the selected item transaction?")) {
                    for (var i = 0; i < row.length; i++) {

                        masterTable.get_dataItems()[i].findElement("chkselect").checked = true;

                    }
                    return true;
                }
                return false;


            }
            else {

                for (var i = 0; i < row.length; i++) {
                    if (masterTable.get_dataItems()[i].findElement("chkselect").disabled == false) {
                        masterTable.get_dataItems()[i].findElement("chkselect").checked = false;
                    }
                }
            }

        }
        function ChangeAllCheckBoxStatesView1(id) {
            if (id.checked == true) {
                if (confirm("Are you sure you want to complete the selected item transaction?")) {
                    return true;
                }
                else {
                    id.checked = false;
                    return false;
                }


            }

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
        function clientListShownBP(source, eventArgs) {
            var completionList = $find("AutoCompleteBP").get_completionList();
            completionList.style.width = 'auto';
        }
    </script>
    <ATS:Session ID="chkSession" runat="Server" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/AppWeb/LookupWebService.asmx" />
        </Services>
    </telerik:RadScriptManager>
    <h2>
        <asp:Label ID="addATSEmployees" Text="Receive Empty Kegs" runat="server" /></h2>
    <em>* Required Fields</em>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                <%--<tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblCompany" Text="<%$ Resources:lang, gridCompanyName %>" runat="server" />
                            </td>
                            <td class='sideHead1 MandatoryField' style="width: 10px">
                                *
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadComboBox ID="rcbCompany" runat="server" Filter="Contains" Width="195px">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="0" Text="African+Eastern" Selected="true" />
                                        <telerik:RadComboBoxItem Value="1" Text="TrackIT" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='rfvCompany' ControlToValidate='rcbCompany' ErrorMessage="<%$ Resources:lang, rfvCompanyName %>"
                                    Display='None' runat='server' />
                            </td>
                            
                        </tr>--%>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label5" Text="From Company" runat="server" />*
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <telerik:RadComboBox ID="rcbFromCmpny" runat="server" AutoPostBack="true" Filter="Contains"
                                            Width="195px">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID='rfvrdcCmpnny' ControlToValidate='rcbFromCmpny' ErrorMessage="From Company is Required"
                                            Display='None' runat='server' ValidationGroup="Show" InitialValue="--Select One--" />
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
                                            ErrorMessage="From BranchPlant is Required" Display='None' runat='server' ValidationGroup="Show" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label7" Text="To Company" runat="server" />*
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <telerik:RadComboBox ID="rcbToCmpny" runat="server" Filter="Contains" AutoPostBack="true"
                                            Width="195px">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID='rfvrcToCmpny' ControlToValidate='rcbToCmpny' ErrorMessage="To Company is Required"
                                            Display='None' runat='server' ValidationGroup="Show" InitialValue="--Select One--" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label2" Text="To Branch Plant" runat="server" />*
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <div id="divToBP" runat="server" class="autocomplete_completionListElement">
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
                                        <asp:RequiredFieldValidator ID='RequiredFieldValidator2' ControlToValidate='txtToBP'
                                            ErrorMessage="To BranchPlant is Required" Display='None' runat='server' ValidationGroup="Show" />
                                        &nbsp &nbsp &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td class='sideHead' width="20%">
                                        <asp:Label ID="Label3" Text="Transaction Number" runat="server" />*
                                    </td>
                                    <td class='sideHead1' width="30%">
                                        <telerik:RadComboBox ID="rcbTransactionNumber" runat="server" Filter="Contains" Width="195px"
                                            AutoPostBack="true" DataSourceID="objTransactionNumber" DataTextField="TransactionNumber"
                                            DataValueField="TransactionNumber">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID='RequiredFieldValidator3' ControlToValidate='rcbTransactionNumber'
                                            ErrorMessage="Transaction Number is Required" Display='None' runat='server' ValidationGroup="Show"
                                            InitialValue="--Select One--" />
                                        &nbsp &nbsp &nbsp
                                    </td>
                                    <td class='sideHead' width="20%" colspan="2">
                                        <telerik:RadButton ID="btnShow" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                            Width="100px" Text="Show" runat="server" SingleClick="true" SingleClickText="wait..."  DisabledButtonCssClass="btnDisable" ValidationGroup="Show" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <telerik:RadGrid ID="RadgvEmptyKegReceive" runat="server" CellSpacing="0" GridLines="None"
                                            Width="100%" Skin="WebBlue">
                                            <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
                                                NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>">
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="false">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkselectALL" runat="server" Text="Select All" onclick="ChangeAllCheckBoxStatesView(this)" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkselect" runat="server" onclick="ChangeAllCheckBoxStatesView1(this)" />
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="CompanyName" FilterControlAltText="Filter CompanyName column"
                                                        Visible="false" HeaderText="<%$ Resources:lang, gridCompanyName %>" SortExpression="CompanyName"
                                                        UniqueName="CompanyName">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ToBranchPlantName" FilterControlAltText="Filter ToBranchPlantName column"
                                                        Visible="false" HeaderText="Receive Branch Plant" SortExpression="ToBranchPlantName"
                                                        UniqueName="ToBranchPlantName">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ItemCode" FilterControlAltText="Filter ItemCode column"
                                                        HeaderStyle-Width="80px" HeaderText="Item Code" SortExpression="ItemCode" UniqueName="ItemCode">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ItemName1" FilterControlAltText="Filter ItemName1 column"
                                                        HeaderStyle-Width="200px" HeaderText="Item Name1" SortExpression="ItemName1"
                                                        UniqueName="ItemName1">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ItemName2" FilterControlAltText="Filter ItemName2 column"
                                                        HeaderStyle-Width="200px" HeaderText="Item Name2" SortExpression="ItemName2"
                                                        UniqueName="ItemName2">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="UOM" FilterControlAltText="Filter UOM column"
                                                        HeaderText="UOM" SortExpression="UOM" UniqueName="UOM">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Barcode" FilterControlAltText="Filter Barcode column"
                                                        Visible="false" HeaderText="Barcode" SortExpression="Barcode" UniqueName="Barcode">
                                                    </telerik:GridBoundColumn>
                                                    <%--    <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter Quantity column" ItemStyle-VerticalAlign="Middle" 
                                                    ItemStyle-HorizontalAlign="Center"
                                                        HeaderText="Transfered Quantity" SortExpression="Transfered Quantity" UniqueName="Quantity">
                                                    </telerik:GridBoundColumn>--%>
                                                    <telerik:GridTemplateColumn HeaderText="Transferredqty" UniqueName="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransferredQty" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Receive Quantity" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <div style="padding-top: 10px;">
                                                            </div>
                                                            <asp:TextBox ID="txtgridqty" runat="server" Text='<%#Eval("ReceiveQty1")%>' Width="50px"
                                                                onkeyPress="return OnlyNumbers(event);"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID='RequiredFieldValidator8' ControlToValidate='txtgridqty' ErrorMessage="Quantity is Required"
                                                                    Display='None' runat='server' ValidationGroup="Save" /><asp:RegularExpressionValidator
                                                                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtgridqty"
                                                                        ErrorMessage="Quantity should b greater than 0" ValidationExpression="^\d+$"
                                                                        ValidationGroup="Save" ForeColor="Red" Font-Size="Small"></asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Received Date" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <div style="width: 230px;">
                                                            </div>
                                                            <telerik:RadDatePicker ID="dpReceivedDate" runat="server" DateInput-DateFormat="MM/dd/yyyy"
                                                                ShowPopupOnFocus="true" Width="100px" DbSelectedDate='<%# Bind("ReceiveDate") %>'>
                                                            </telerik:RadDatePicker>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgridItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="EBPOrderID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEBPOrderID" runat="server" Text='<%#Eval("EBPOrderID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="PrevQty" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrevQty" runat="server" Text='<%#Eval("ReceiveQty1")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="PrevQtyFirst" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrevQtyFirst" runat="server" Text='<%#Eval("ReceiveQty2")%>'></asp:Label>
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
                                            Width="100px" Text="<%$ Resources:lang, btnSave %>" SingleClick="true" SingleClickText="wait..."  DisabledButtonCssClass="btnDisable"  ValidationGroup="Save" runat="server" />
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
                                ShowMessageBox='True' HeaderText='<%$ Resources:lang, Errors %>' ValidationGroup="Show">
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
    <asp:ObjectDataSource ID="ObjBranchPlant" runat="server" SelectMethod="GetTabMBranchPlantByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMBranchPlant" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjEmptyKegsReceiveBP" runat="server" SelectMethod="GetTabKEmptyTransferBPByFromToBranchID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabKEmptyTransferBP" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="FromBranchID" Type="Int32" />
            <asp:Parameter Name="ToBranchID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="objTransactionNumber" runat="server" SelectMethod="GetAllTransactionNumbersByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabKEmptyTransferBP" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="valCompanyID" Type="Int32" />
            <asp:Parameter Name="FromBranchID" Type="Int32" />
            <asp:Parameter Name="ToBranchID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
