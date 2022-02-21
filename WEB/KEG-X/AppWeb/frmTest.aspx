<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="frmTest.aspx.vb" Inherits="KEG_X.TrackITKTS.frmTest" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table id="TABLE100" cellspacing="0" cellpadding="0" height="100%" width="100%" border="0"
        runat="server">
        <tr>
            <td height="450px" valign="Top">
                <div>
                    <br />
                    <br />
                    <table id="TABLE1" align='center' bordercolor='#ccddef' height="1%" cellspacing='0'
                        class="table" rules='none' width="80%" border='1' runat="server">
                        <tr valign='top' height="5%">
                            <td class='callOutStyle' colspan="6">
                                <asp:Label ID="addATSEmployees" Text="Empty Kegs Returns Customer to BP" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor='white' colspan="6">
                                <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="6">
                        Load :<asp:Label ID="lblPostback" runat="server" />
                        <br />
                        postback:<asp:Label ID="lblnotpostback"  runat="server" />
                        <br />
                        
                        </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label21" Text="Customer" runat="server" />
                            </td>
                            <td class='sideHead1 MandatoryField' style="width: 10px">
                                *
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadComboBox ID="rcbCustomer" runat="server" Filter="Contains" Width="195px"
                                   >
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator21' ControlToValidate='rcbCustomer'
                                    ErrorMessage="Customer is Required" Display='None' runat='server' ValidationGroup="Save"
                                    InitialValue="--Select One--" />
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label2" Text="Branch Plant" runat="server" />
                            </td>
                            <td class='sideHead1 MandatoryField' style="width: 10px">
                                *
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadComboBox ID="rcbToBP" runat="server" Filter="Contains" Width="195px"
                                    >
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator2' ControlToValidate='rcbToBP'
                                    ErrorMessage="BranchPlant is Required" Display='None' runat='server' ValidationGroup="Save"
                                    InitialValue="--Select One--" />
                                <telerik:RadTextBox ID="txtTransactionNumber" runat="server" Width="195px" Visible="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                            <td colspan="6">
                                                textchangedstart:<asp:Label ID="lbltextchangedstart" runat="server" />
                                                <br />
                                                textchangedstop:<asp:Label ID="lbltextchangedstop" runat="server" />
                                            </td>
                                            </tr>
                                            <tr>
                                                <td class='sideHead' width="20%">
                                                    <asp:Label ID="Label1" Text="Item Code" runat="server" />
                                                </td>
                                                <td class='sideHead1 MandatoryField' style="width: 10px">
                                                    *
                                                </td>
                                                <td class='sideHead1' width="30%">
                                                    <telerik:RadTextBox ID="txtItemCode1" runat="server" Width="195px">
                                                    </telerik:RadTextBox>
                                                  
                                                    <asp:RequiredFieldValidator ID='RequiredFieldValidator1' ControlToValidate='txtItemCode1'
                                                        ErrorMessage="ItemCode is Required" Display='None' runat='server' ValidationGroup="Add" />
                                                </td>
                                                <td>
                                                <telerik:RadButton ID="btnGet" Width="100px" Text="Get" runat="server"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='sideHead' width="20%">
                                                    <asp:Label ID="lblItemID" Text="Item Code" runat="server" />
                                                </td>
                                                <td class='sideHead1 MandatoryField' style="width: 10px">
                                                    *
                                                </td>
                                                <td class='sideHead1' width="30%">
                                                    <telerik:RadTextBox ID="txtItemCode" runat="server" Width="195px" AutoPostBack="true">
                                                    </telerik:RadTextBox>
                                                    <telerik:RadTextBox ID="txtItemID" runat="server" Visible="false">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID='RequiredFieldValidator3' ControlToValidate='txtItemCode'
                                                        ErrorMessage="ItemCode is Required" Display='None' runat='server' ValidationGroup="Add" />
                                                </td>
                                                <td class='sideHead' width="20%">
                                                    <asp:Label ID="lblItemDescription" Text="Item Name1" runat="server" />
                                                </td>
                                                <td class='sideHead1 MandatoryField' style="width: 10px">
                                                    *
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
                                                    <asp:Label ID="Label4" Text="Item Name2" runat="server" />
                                                </td>
                                                <td class='sideHead1 MandatoryField' style="width: 10px">
                                                    *
                                                </td>
                                                <td class='sideHead1' width="30%">
                                                    <telerik:RadTextBox ID="txtItemName2" runat="server" Width="195px" Enabled="false">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID='RequiredFieldValidator9' ControlToValidate='txtItemName2'
                                                        ErrorMessage="Item Name2 is Required" Display='None' runat='server' ValidationGroup="Add" />
                                                </td>
                                                <td class='sideHead' width="20%">
                                                    <asp:Label ID="lblQuantity" Text="Quantity" runat="server" />
                                                </td>
                                                <td class='sideHead1 MandatoryField' style="width: 10px">
                                                    *
                                                </td>
                                                <td class='sideHead1' width="30%">
                                                    <telerik:RadTextBox ID="txtQuantity" runat="server" Width="195px" onkeyPress="return OnlyNumbers(event);">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID='RequiredFieldValidator5' ControlToValidate='txtQuantity'
                                                        ErrorMessage="Quantity is Required" Display='None' runat='server' ValidationGroup="Add" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='sideHead' width="20%">
                                                    <asp:Label ID="lblUOM" Text="UOM" runat="server" />
                                                </td>
                                                <td class='sideHead1 MandatoryField' style="width: 10px">
                                                    *
                                                </td>
                                                <td class='sideHead1' width="30%">
                                                    <telerik:RadComboBox ID="ddlUOM" runat="server" Filter="Contains" Width="195px">
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID='RequiredFieldValidator6' ControlToValidate='ddlUOM'
                                                        ErrorMessage="UOM is Required" Display='None' runat='server' ValidationGroup="Add"
                                                        InitialValue="--Select One--" />
                                                </td>
                                                <td class='sideHead' width="20%">
                                                    <asp:Label ID="Label6" Text="Barcode" runat="server" />
                                                </td>
                                                <td class='sideHead1 MandatoryField' style="width: 10px">
                                                </td>
                                                <td class='sideHead1' width="30%">
                                                    <telerik:RadTextBox ID="txtSerialNo" runat="server" Width="195px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                   
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtItemCode" EventName="TextChanged" />
                                    </Triggers>

                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td class='sideHead'>
                                                </td>
                                                <td class='sideHead1 MandatoryField' style="width: 10px">
                                                </td>
                                                <td class='sideHead1'>
                                                    <telerik:RadButton ID="btnAdd" Width="100px" Text="Add" runat="server" ValidationGroup="Add" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <telerik:RadGrid ID="RadgvEmptyKegTransfer" runat="server" CellSpacing="0" GridLines="None"
                                                        AllowPaging="True" Skin="WebBlue">
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
                                                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Serial Number">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtSerialNumber" runat="server" Text='<%# Eval("SerialNumber") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn AllowFiltering="false" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgridItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn AllowFiltering="false" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgridUOMID" runat="server" Text='<%# Eval("UOMID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PrevQty" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPrevQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="EOrderID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEOrderID" runat="server" Text='<%# Eval("EOrderID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
