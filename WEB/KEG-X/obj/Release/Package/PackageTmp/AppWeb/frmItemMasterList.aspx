<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master" CodeBehind="frmItemMasterList.aspx.vb" Inherits="KEG_X.TrackITKTS.frmItemMasterList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>

                                <h2>
                                    <asp:Literal ID="Literal1" runat="server" Text="Item Details" /></h2>
                            <telerik:RadGrid ID="RadgvItemMaster" runat="server" CellSpacing="0" GridLines="None"
                                DataSourceID="ObjItemMaster" AllowPaging="True" AllowFilteringByColumn="true" 
                                 Skin="WebBlue" AllowSorting="true">
                                 <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
                                    NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>" DataSourceID="ObjItemMaster">
                                    <Columns>
                                         <telerik:GridBoundColumn DataField="CompanyCode"  Visible = "False"  FilterControlAltText="Filter CompanyCode column"
                                            HeaderText="Company Code" SortExpression="CompanyCode" 
                                            UniqueName="CompanyCode">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CompanyName" Visible = "False" FilterControlAltText="Filter CompanyName column"
                                            HeaderText="Company Name" SortExpression="CompanyName"
                                            UniqueName="CompanyName">
                                        </telerik:GridBoundColumn>
                                        
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
                                    </Columns>
                                </MasterTableView>
                                        <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" >
            </Scrolling>
            <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
        </ClientSettings>
                            </telerik:RadGrid>

    <asp:ObjectDataSource ID="ObjItemMaster" runat="server" SelectMethod="GetTabItemsByEmployeeID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabItems" OldValuesParameterFormatString="{0}">
        <SelectParameters>
                <asp:Parameter Name="EmployeeID" Type="Int32" />
               <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>