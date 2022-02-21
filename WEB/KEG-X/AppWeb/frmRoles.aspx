<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmRoles.aspx.vb" MasterPageFile="~/Templates/MasterPage.Master"
    Inherits="KEG_X.TrackITKTS.frmRoles" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel id="upnl" runat ="server">
     <ContentTemplate>
    <div class="f-left">
		<asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />
		</div>
    <div id="first-div" class="div-right" style ="padding-right :50px">
        <telerik:RadButton ID="RadButton1" OnClientClicking="OnClientClicking" NavigateUrl="frmAddEditRoles.aspx" 
            HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Skin="WebBlue"
            Width="100px" Text="Add" runat="server" />
    </div>
    <div id="second-div" class="div-left">    
    <h2>
        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:lang, ManTABRole %>" /></h2>
        </div>
        <div style="clear: both;"></div>
    <telerik:RadGrid ID="RadgvRoles" runat="server" CellSpacing="0" GridLines="None" Width="95%"
        AllowPaging="True" Skin="WebBlue" DataSourceID="ObjDSTABUserPrivileges" AllowFilteringByColumn="true"
        AllowSorting="true">
        <GroupingSettings CaseSensitive="false" />
        <MasterTableView AutoGenerateColumns="False" DataSourceID="ObjDSTABUserPrivileges"
            DataKeyNames="RoleID" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
            NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>">
            <Columns>
                <telerik:GridBoundColumn DataField="RoleID" FilterControlAltText="Filter RoleID column"
                    Visible="false" HeaderText="RoleID" SortExpression="RoleID" UniqueName="RoleID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CompanyCode" Visible ="false" FilterControlAltText="Filter CompanyCode column" 
                    HeaderText="Company Code" SortExpression="CompanyCode" UniqueName="CompanyCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CompanyName" Visible ="false" FilterControlAltText="Filter CompanyName column"
                    HeaderText="Company Name" SortExpression="CompanyName" UniqueName="CompanyName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoleCompany" FilterControlAltText="Filter RoleName column"
                    HeaderText="<%$ Resources:lang, lblRoleName %>" SortExpression="RoleCompany" UniqueName="RoleCompany">
                </telerik:GridBoundColumn>
                <%--<telerik:GridBoundColumn DataField="Feature_en" FilterControlAltText="Filter Feature_en column"
                    HeaderText="<%$ Resources:lang, gridFeature_en %>" SortExpression="Feature_en"
                    UniqueName="Feature_en">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AllowView" FilterControlAltText="Filter AllowView column"
                    HeaderText="<%$ Resources:lang, gridAllowView %>" SortExpression="AllowView"
                    UniqueName="AllowView">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AllowAdd" FilterControlAltText="Filter AllowAdd column"
                    HeaderText="<%$ Resources:lang, gridAllowAdd %>" SortExpression="AllowAdd" UniqueName="AllowAdd">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AllowEdit" FilterControlAltText="Filter AllowEdit column"
                    HeaderText="<%$ Resources:lang, gridAllowEdit %>" SortExpression="AllowEdit"
                    UniqueName="AllowEdit">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AllowDelete" FilterControlAltText="Filter AllowDelete column"
                    HeaderText="<%$ Resources:lang, gridAllowDelete %>" SortExpression="AllowDelete"
                    UniqueName="AllowDelete">
                </telerik:GridBoundColumn>--%>
                <telerik:GridTemplateColumn HeaderText="<%$ Resources:lang, gridEdit %>" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="EditButton" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container,"RowIndex") %>'
                            ImageUrl="../Images/Edit.gif" />
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
             <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" >
            </Scrolling>
            <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:ObjectDataSource ID="ObjDSTABUserPrivileges" runat="server" SelectMethod="GetAllTabRoleByEmployeeID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabRole" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
