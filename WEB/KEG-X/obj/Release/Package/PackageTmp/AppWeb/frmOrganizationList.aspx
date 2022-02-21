<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="frmOrganizationList.aspx.vb" Inherits="KEG_X.TrackITKTS.frmOrganizationList" %>

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
     <asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" /></div>
     <div id="first-div" class="div-right">
    </div>
    <div id="second-div" class="div-left">
    <h2>
        <asp:Literal ID="Literal1" runat="server" Text="MANAGE COMPANIES" /></h2>
         </div>
    <div style="clear: both;">
    </div>
    <telerik:RadGrid ID="RadgvOrganisation" runat="server" CellSpacing="0" DataSourceID="ObjOrganization"
        GridLines="None" AllowPaging="True" AllowFilteringByColumn="true" Skin="WebBlue"
        AllowSorting="true">
        <GroupingSettings CaseSensitive="false" />
        <MasterTableView AutoGenerateColumns="false" DataSourceID="ObjOrganization" NoMasterRecordsText="No Records to display"
            NoDetailRecordsText="No Records to display" OverrideDataSourceControlSorting="true">
            <Columns>
                <telerik:GridBoundColumn DataField="CompanyCode" FilterControlAltText="Filter CompanyCode column"
                    HeaderText="Company Code" SortExpression="CompanyCode" UniqueName="CompanyCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CompanyName" FilterControlAltText="Filter CompanyName column"
                    HeaderText="Company Name" SortExpression="CompanyName" UniqueName="CompanyName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address1" FilterControlAltText="Filter Address1 column"
                    HeaderText="Address1" SortExpression="Address1" UniqueName="Address1">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address2" FilterControlAltText="Filter Address2 column"
                    HeaderText="Address2" SortExpression="Address2" UniqueName="Address2">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address3" FilterControlAltText="Filter Address3 column"
                    HeaderText="Address3" SortExpression="Address3" UniqueName="Address3">
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
            </Columns>
        </MasterTableView>
               <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" >
            </Scrolling>
            <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:ObjectDataSource ID="ObjOrganization" runat="server" SelectMethod="GetAllTabMOrganizationByEmployeeID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMOrganization" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
