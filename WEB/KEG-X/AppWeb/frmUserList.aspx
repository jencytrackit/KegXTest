<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmUserList.aspx.vb" MasterPageFile="~/Templates/MasterPage.Master"
    Inherits="KEG_X.TrackITKTS.frmUserList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <%--<ats:session id="chkSession" runat="Server" />--%>
    <form id="frmATS" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <table id="Table1" cellspacing="0" cellpadding="0" style="height: 450px" width="100%"
        align="center" border="0"  runat="server">
        <tr>
            <td height="100%" valign="Top">
                <ATSMenu:Menu ID="MENU1" runat="Server" />
                <table id="Table2" cellspacing="0" cellpadding="0" height="100%" width="100%" align="center"
                    border="0"  runat="server">
                    <tr>
                        <td colspan="3">
                            <%-- <msg:MyMessageBox ID="MessageBox" runat="server" ShowCloseButton="true" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td height="100%" class="sideHead1" valign="TOP">
                            <%--<ATS:OrgTree id="OrgTree" runat="Server"/>--%>
                        </td>
                        <td valign="bottom" width="5">
                            &nbsp;
                        </td>
                        <td valign="top" height="100%" width="100%">
                            <table id="Table3" cellspacing="0" cellpadding="0" width="97%" align="center" border="0"
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
                                        <asp:ImageButton ID="ImgBtn1" runat="server" ImageUrl="~/Images/Insert.gif" ToolTip="" /><b>
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl="frmAddEditATSUsers.aspx" runat="server"
                                                Text="<%$ Resources:lang, lblAdd %>" /></b>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table id="Table4" cellspacing="0" cellpadding="0" width="97%" align="center" border="0"
                                 runat="server">
                                <tr>
                                    <td>
                                        <table id="Table5" cellspacing="1" cellpadding="0" width="100%" align="center" border="0"
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
                                                                            <asp:Literal runat="server" Text="<%$ Resources:lang, ManageUsers %>" /></b></div>
                                                                    <telerik:RadGrid ID="RadUserList" runat="server" CellSpacing="0" GridLines="None" AllowFilteringByColumn ="true" Skin="WebBlue">
                                                                        <MasterTableView AutoGenerateColumns="False">
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="CompanyName" FilterControlAltText="Filter CompanyName column"
                                                                                    HeaderText="<%$ Resources:lang, gridCompanyName %>" SortExpression="CompanyName"
                                                                                    UniqueName="CompanyName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="UserID" FilterControlAltText="Filter UserID column"
                                                                                    Visible="false" HeaderText="UserID" SortExpression="UserID" UniqueName="UserID">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="UserName" FilterControlAltText="Filter UserName column"
                                                                                    HeaderText="<%$ Resources:lang, gridUserName %>" SortExpression="UserName" UniqueName="UserName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="EmployeeName" FilterControlAltText="Filter EmployeeName column"
                                                                                    HeaderText="<%$ Resources:lang, gridEmployeeName %>" SortExpression="EmployeeName"
                                                                                    UniqueName="EmployeeName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Role" FilterControlAltText="Filter Role column"
                                                                                    HeaderText="<%$ Resources:lang, gridRole %>" SortExpression="Role" UniqueName="Role">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridButtonColumn ButtonType="LinkButton" DataTextField="Disable" UniqueName="Disable"
                                                                                    HeaderText="<%$ Resources:lang, gridDisable %>" CommandName="Disable" ItemStyle-ForeColor="Blue"
                                                                                    ItemStyle-Font-Underline='True' ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                                <telerik:GridBoundColumn DataField="Logged" FilterControlAltText="Filter Logged column"
                                                                                    AllowFiltering="false" HeaderText="<%$ Resources:lang, gridLogged %>" SortExpression="Logged"
                                                                                    UniqueName="Logged" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridTemplateColumn HeaderText="<%$ Resources:lang, gridEdit %>" AllowFiltering="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton runat="server" ID="EditButton" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container,"RowIndex") %>'
                                                                                            ImageUrl="../Images/Edit.gif" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                                </telerik:GridTemplateColumn>
                                                                                <telerik:GridTemplateColumn HeaderText="<%$ Resources:lang, gridDelete %>" AllowFiltering="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton runat="server" ID="DeleteButton" CommandName="deleteno" CommandArgument='<%# DataBinder.Eval(Container,"RowIndex") %>'
                                                                                            ImageUrl="../Images/Delete.gif" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                                </telerik:GridTemplateColumn>
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
    <%--  <asp:ObjectDataSource ID="ObjDSATSUsers" runat="server" SelectMethod="GetAllATSUsers"
        DeleteMethod="DeletetTest" TypeName="TrackIT.TrackITATS.clsATSUsers" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="valUserName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
    </form>
</asp:Content>
