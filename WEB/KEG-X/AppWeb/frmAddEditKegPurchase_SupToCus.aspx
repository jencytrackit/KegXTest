<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="frmAddEditKegPurchase_SupToCus.aspx.vb" Inherits="KEG_X.TrackITKTS.frmAddEditKegPurchase_SupToCus" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">
        function redirectme(sender, args) {
            location.href = 'frmKegPurchaseModule_SupToCus.aspx';
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
        function OnClientClicked(button, args) {
            document.forms[0].reset();
            button.set_autoPostBack(false);
        }
    </script>
    <ATS:Session ID="chkSession" runat="Server" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <h2>
        <asp:Label ID="addATSEmployees" Text="Full Kegs Receive From Supplier to Customer"
            runat="server" /></h2>
             <em>* Required Fields</em>
    <table id="TABLE100" cellspacing="0" cellpadding="0" height="100%" width="100%" border="0"
        dir="<%$ Resources:lang, TextDirection %>" runat="server">
        <tr>
            <td height="450px" valign="Top">
                <%-- <iiscc:SiteCompass ID="SiteCompass12" runat="server" CssClass="compassStyle1" DisplayTitle="<%$ Resources:lang, addATSEmployees %>"
                    CssClassLink="compassLinkStyle1" MaxItems="5" DivName="compassDiv"></iiscc:SiteCompass>--%>
                <div>
                    <table id="TABLE1" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                        class="table" rules='none' width="80%" border='0' dir="<%$ Resources:lang, TextDirection %>"
                        runat="server">
                        <tr>
                            <td bgcolor='white' colspan="4">
                                <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblCompany" Text="Supplier" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadComboBox ID="rcbSupplier" runat="server" Filter="Contains" Width="195px">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='rfvCompany' ControlToValidate='rcbSupplier' ErrorMessage="Supplier is required"
                                    Display='None' runat='server' ValidationGroup="Save" InitialValue="--Select One--" />
                                <telerik:RadTextBox ID="txtTransactionNumber" runat="server" Width="195px" Visible="false">
                                </telerik:RadTextBox>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label1" Text="Customer" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadComboBox ID="rcbToCustomer" runat="server" Filter="Contains" Width="195px">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator1' ControlToValidate='rcbToCustomer'
                                    ErrorMessage="Customer is Required" Display='None' runat='server' ValidationGroup="Save"
                                    InitialValue="--Select One--" />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label6" Text="BOL Number" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtBOLNo" runat="server" Width="195px">
                                </telerik:RadTextBox>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label2" Text="BOL Date" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadDatePicker ID="dtBOLDate" runat="server" Width="195px">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label4" Text="PO Type" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtPOType" runat="server" Width="195px">
                                </telerik:RadTextBox>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label5" Text="PO Number" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtPONumber" runat="server" Width="195px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label7" Text="Container Number" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtContainerNO" runat="server" Width="195px">
                                </telerik:RadTextBox>
                            </td>
                            <td class='sideHead'>
                                <asp:Label ID="Label3" Text="Receipt Date" runat="server" />*
                            </td>
                            <td class='sideHead1'>
                                <telerik:RadDatePicker ID="dtReceiptDate" runat="server" Width="195px" DateInput-DateFormat="dd/MM/yyyy">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator12' ControlToValidate='dtReceiptDate'
                                    ErrorMessage="Receipt Date is Required" Display='None' runat='server' ValidationGroup="Save" />
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
                                <telerik:RadTextBox ID="txtItemCode" runat="server" Width="195px" AutoPostBack="true">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txtItemID" runat="server" Visible="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator8' ControlToValidate='txtItemCode'
                                    ErrorMessage="ItemCode is Required" Display='None' runat='server' ValidationGroup="Add" />
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblItemDescription" Text="Item Name1" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtItemDesc" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator9' ControlToValidate='txtItemDesc'
                                    ErrorMessage="Item Name1 is Required" Display='None' runat='server' ValidationGroup="Add" />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label8" Text="Item Name2" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtItemName2" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator10' ControlToValidate='txtItemName2'
                                    ErrorMessage="Item Name2 is Required" Display='None' runat='server' ValidationGroup="Add" />
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblQuantity" Text="Quantity" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtQuantity" runat="server" Width="195px" onkeyPress="return OnlyNumbers(event);">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator11' ControlToValidate='txtQuantity'
                                    ErrorMessage="Quantity is Required" Display='None' runat='server' ValidationGroup="Add" />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="lblUOM" Text="UOM" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%" colspan="3">
                                <telerik:RadTextBox ID="txtUOM" Width="195px" MaxLength='100' runat="server" Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator6' ControlToValidate='txtUOM'
                                    ValidationGroup="Add" ErrorMessage="UOM is Required" Display='None' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' colspan="2">
                                <telerik:RadButton ID="btnAdd" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Width="100px" Text="Add" runat="server" ValidationGroup="Add" />
                            </td>
                            <td class='sideHead1 MandatoryField' style="width: 10px">
                            </td>
                            <td class='sideHead1'>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <telerik:RadGrid ID="RadgvFullKegReceive" runat="server" CellSpacing="0" GridLines="None"
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
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="FullKegID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFullKegID" runat="server" Text='<%# Eval("FullKegID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" bgcolor="#ffffff" colspan="4">
                                <telerik:RadButton ID="radSave" Width="100px" HoveredCssClass="normal-btnh" ForeColor="White"
                                    Font-Size="17px" Text="Save" ValidationGroup="Save" runat="server" />
                                &nbsp;&nbsp;
                                <telerik:RadButton ID="radClear" Width="100px" HoveredCssClass="normal-btnh" ForeColor="White"
                                    Font-Size="17px" Text="Reset" runat="server" AutoPostBack="false" OnClientClicked="OnClientClicked"
                                    CausesValidation="False" />
                                &nbsp;&nbsp;
                                <telerik:RadButton ID="radCancel" runat="server" HoveredCssClass="normal-btnh" ForeColor="White"
                                    Font-Size="17px" Width="100px" AutoPostBack="false" OnClientClicked="redirectme"
                                    Text="Cancel" CausesValidation="False" />
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
</asp:Content>
