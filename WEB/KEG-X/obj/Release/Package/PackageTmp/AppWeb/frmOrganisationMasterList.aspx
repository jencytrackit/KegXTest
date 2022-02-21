<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="frmOrganisationMasterList.aspx.vb" Inherits="KEG_X.TrackITKTS.frmOrganisationMasterList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%--<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="f-left">
    <asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />
    </div>
    <div id="first-div" class="div-right">
        <telerik:RadButton ID="RadButton1" OnClientClicking="OnClientClicking" NavigateUrl="frmAddEditOrganization.aspx"
            HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Skin="WebBlue"
            Width="100px" Text="Add" runat="server" />
    </div>
    <div id="second-div" class="div-left">    
    <h2>
        <asp:Literal ID="Literal1" runat="server" Text="Manage Organization" /></h2>
        </div>
        <div style="clear: both;"></div>
    <telerik:RadGrid ID="RadgvOrganisation" runat="server" CellSpacing="0" Skin="WebBlue"
        GridLines="None" AllowPaging="True" AllowFilteringByColumn="true" DataSourceID="ObjOrganization"
        AllowSorting="true">
        <GroupingSettings CaseSensitive="false" />
        <MasterTableView AutoGenerateColumns="false" BorderWidth="0"  DataSourceID="ObjOrganization" NoMasterRecordsText="No Records to display"
            NoDetailRecordsText="No Records to display" DataKeyNames="OrganizationID">
            <Columns>
                <telerik:GridBoundColumn DataField="OrganizationID" FilterControlAltText="Filter OrganizationID column"
                    Visible="false" HeaderText="OrganizationID" SortExpression="OrganizationID" UniqueName="OrganizationID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OrganizationCode" FilterControlAltText="Filter OrganizationCode column"
                    HeaderText="Organization Code" SortExpression="OrganizationCode" UniqueName="OrganizationCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OrganizationName" FilterControlAltText="Filter OrganizationName column"
                    HeaderText="Organization Name" SortExpression="OrganizationName" UniqueName="OrganizationName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address1" FilterControlAltText="Filter Address1 column"
                    HeaderText="Address1" SortExpression="Address1" UniqueName="Address1">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address2" FilterControlAltText="Filter Address2 column"
                    HeaderText="Address2" SortExpression="Address2" UniqueName="Address2">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="City" FilterControlAltText="Filter City column"
                    HeaderText="City" SortExpression="City" UniqueName="City">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="State" FilterControlAltText="Filter State column"
                    HeaderText="State" SortExpression="State" UniqueName="State">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Country" FilterControlAltText="Filter Country column"
                    HeaderText="Country" SortExpression="Country" UniqueName="Country">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Phone" FilterControlAltText="Filter Phone column"
                    HeaderText="Phone" SortExpression="Phone" UniqueName="Phone">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column"
                    HeaderText="Email" SortExpression="Email" UniqueName="Email">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="View/Edit" AllowFiltering="false">
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
    <asp:ObjectDataSource ID="ObjOrganization" runat="server" SelectMethod="GetAllTabMOrganizationMasterByEmployeeID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMOrganisationMaster"
        OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
