<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="frmAddEditSuppliers.aspx.vb" Inherits="KEG_X.TrackITKTS.frmAddEditSuppliers" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <script language="javascript" type="text/javascript">
        function redirectme(sender, args) {
            location.href = 'frmSuppliersList.aspx';
        }
        function OnClientClicked(button, args) {
            document.forms[0].reset();
            button.set_autoPostBack(false);
        }
    </script>
    <h2>
        <asp:Label ID="addATSUsers" Text="Suppliers Record" runat="server" /></h2>
         <em>* Required Fields</em>
    <table id="TABLE1" cellspacing="0" cellpadding="0" height="450px" width="100%" border="0"
        runat="server">
        <tr>
            <td height="450px" valign="Top">
                <div>
                    <table id="TABLE2" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                        class="table" rules='none' width="80%" border='0' runat="server">
                        <tr>
                            <td bgcolor='white' valign="top" colspan="4">
                                <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label><asp:Label ID='txtMsg'
                                    runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label2" Text="Supplier Code" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtSupplierCode" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator4' ControlToValidate='txtSupplierCode'
                                    ErrorMessage="Supplier Code is Required" Display='None' runat='server' />
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label5" Text="Supplier Name" runat="server" />*
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtSupplierName" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID='RequiredFieldValidator5' ControlToValidate='txtSupplierName'
                                    ErrorMessage="Supplier Name is Required" Display='None' runat='server' />
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label7" Text="Address1" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtAddress1" runat="server" Width="195px" Enabled="false"
                                    TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label3" Text="Address2" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtAddress2" runat="server" Width="195px" Enabled="false"
                                    TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label4" Text="Address3" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtAddress3" runat="server" Width="195px" Enabled="false"
                                    TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label8" Text="Address4" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtAddress4" runat="server" Width="195px" Enabled="false"
                                    TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label1" Text="City" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtCity" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label10" Text="State" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtState" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label13" Text="Country" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtCountry" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label15" Text="Phone" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtPhone" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label18" Text="Email" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtEmail" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label20" Text="Fax" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtFax" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label23" Text="Website" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtWebsite" runat="server" Width="195px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label25" Text="Shipment Port" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtShipmentPort" runat="server" Width="195px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label6" Text="Destination Port" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtDestinationPort" runat="server" Width="195px">
                                </telerik:RadTextBox>
                            </td>
                            <td class='sideHead' width="20%">
                                <asp:Label ID="Label9" Text="Terms of Return" runat="server" />
                            </td>
                            <td class='sideHead1' width="30%">
                                <telerik:RadTextBox ID="txtTermsofreturn" runat="server" Width="195px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" bgcolor="#ffffff" colspan="4">
                                <telerik:RadButton ID='radSave' HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                    Width="100px" Text="<%$ Resources:lang, btnSave %>" runat='server' />
                                &nbsp;&nbsp;
                                <telerik:RadButton ID="radClear" HoveredCssClass="normal-btnh" ForeColor="White"
                                    Font-Size="17px" runat="server" Width="100px" Text="<%$ Resources:lang, btnClear %>"
                                    OnClientClicked="OnClientClicked" CausesValidation="False" />
                                &nbsp;&nbsp;
                                <telerik:RadButton ID="radCancel" HoveredCssClass="normal-btnh" ForeColor="White"
                                    Font-Size="17px" runat="server" Width="100px" AutoPostBack="false" OnClientClicked="redirectme"
                                    Text="<%$ Resources:lang, btnCancel %>" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID='summary' runat='server' ShowSummary='False' ShowMessageBox='True'
                        HeaderText="<%$ Resources:lang, Errors %>"></asp:ValidationSummary>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
