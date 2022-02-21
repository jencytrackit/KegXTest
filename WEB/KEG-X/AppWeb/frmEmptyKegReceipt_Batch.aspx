<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmEmptyKegReceipt_Batch.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmEmptyKegReceipt_Batch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <%--<ATS:Session ID="chkSession" runat="Server" />--%>
    <form id="frmATS" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <table id="TABLE1" cellspacing="0" cellpadding="0" height="100%" width="100%" align="center"
        border="0"  runat="server">
        <tr height="100%">
            <td height="100%" valign="Top">
                <ATSMenu:Menu ID="MENU1" runat="Server" />
                <table id="TABLE2" cellspacing="0" cellpadding="0" height="100%" width="100%" align="center"
                    border="0"  runat="server">
                    <tr>
                        <td colspan="3">
                            <%--   <iiscc:SiteCompass ID="SiteCompass2" runat="server" CssClass="compassStyle1" DisplayTitle="<%$ Resources:lang, ManageOrganization %>"
                                CssClassLink="compassLinkStyle1" MaxItems="5" DivName="compassDiv"></iiscc:SiteCompass>--%>
                            <%-- <msg:MyMessageBox ID="MessageBox" runat="server" ShowCloseButton="true" />--%>
                        </td>
                    </tr>
                    <tr height="100%">
                        <td height="100%" class="sideHead1" valign="TOP">
                            <%--<ATS:OrgTree id="OrgTree" runat="Server"/>--%>
                        </td>
                        <td valign="bottom" height="100%" width="5">
                            &nbsp;
                        </td>
                        <td valign="top" height="100%" width="100%">
                            <table id="TABLE3" cellspacing="0" cellpadding="0" width="97%" align="center" border="0"
                                 runat="server">
                                <tr>
                                    <td valign="bottom" width="40%">
                                        &nbsp;
                                    </td>
                                    <td width="35%">
                                        &nbsp;&nbsp;<asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />
                                    </td>
                                    <td width="25%" align="center" class="addNew">
                                        <br>
                                        <%--  <asp:ImageButton ID="ImgBtn1" runat="server" ImageUrl="~/Images/Insert.gif" ToolTip="" /><b>
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl="frmAddEditEmployees.aspx" runat="server"
                                                Text="<%$ Resources:lang, lblAdd %>" /></b>--%>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table id="TABLE4" cellspacing="0" cellpadding="0" width="97%" align="center" border="0"
                                 runat="server">
                                <tr>
                                    <td>
                                        <table id="TABLE5" cellspacing="1" cellpadding="0" width="100%" align="center" border="0"
                                             runat="server">
                                            <tr bgcolor="white">
                                                <td>
                                                    <table id="Table7" cellspacing="1" cellpadding="0" width="100%" align="center" border="0"
                                                         runat="server">
                                                        <tr>
                                                            <td bgcolor="#ffffff" width="95%">
                                                                <div>
                                                                    <div class='addGridHead'>
                                                                        <b>
                                                                            <asp:Literal ID="Literal1" runat="server" Text="Empty Keg Receipt - Batch" /></b></div>
                                                                    <telerik:RadGrid ID="RadgvEmptyKeg" runat="server" CellSpacing="0" GridLines="None"
                                                                        AllowPaging="True" AllowFilteringByColumn="true" Skin="WebBlue">
                                                                        <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
                                                                            NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>">
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="CompanyName" FilterControlAltText="Filter CompanyName column"
                                                                                    HeaderText="<%$ Resources:lang, gridCompanyName %>" SortExpression="CompanyName"
                                                                                    UniqueName="CompanyName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="BranchPlantCode" FilterControlAltText="Filter BranchPlantCode column"
                                                                                    HeaderText="Branch Plant Code" SortExpression="BranchPlantCode" UniqueName="BranchPlantCode">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="BranchPlant" FilterControlAltText="Filter BranchPlant column"
                                                                                    HeaderText="Branch Plant" SortExpression="BranchPlant" UniqueName="BranchPlant">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="CustomerName" FilterControlAltText="Filter CustomerName column"
                                                                                    HeaderText="Customer Name" SortExpression="CustomerName" UniqueName="CustomerName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="ItemName" FilterControlAltText="Filter ItemName column"
                                                                                    HeaderText="Item Name" SortExpression="ItemName" UniqueName="ItemName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Barcode" FilterControlAltText="Filter Barcode column"
                                                                                    HeaderText="Barcode" SortExpression="Barcode" UniqueName="Barcode">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter Quantity column"
                                                                                    HeaderText="Quantity" SortExpression="Quantity" UniqueName="Quantity">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="UOM" FilterControlAltText="Filter UOM column"
                                                                                    HeaderText="UOM" SortExpression="UOM" UniqueName="UOM">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Date" FilterControlAltText="Filter Date column"
                                                                                    HeaderText="Date" SortExpression="Date" UniqueName="Date">
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                    </telerik:RadGrid>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
