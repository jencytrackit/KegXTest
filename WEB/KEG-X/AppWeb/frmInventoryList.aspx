<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmInventoryList.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmInventoryList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>

                                <h2>
                          
                                    <asp:Literal ID="Literal1" runat="server" Text="Manage Inventory Details" /></h2>
                            <telerik:RadGrid ID="RadgvInventory" runat="server" CellSpacing="0" GridLines="None"
                                DataSourceID="ObjInventory" AllowPaging="True" AllowFilteringByColumn="true"
                                 Skin="WebBlue" AllowSorting="true">
                                 <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
                                    NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>" DataSourceID="ObjInventory">
                                    <Columns>
                                         <telerik:GridBoundColumn DataField="CompanyCode" Visible ="False" FilterControlAltText="Filter CompanyCode column"
                                            HeaderText="Company Code" SortExpression="CompanyCode"
                                            UniqueName="CompanyCode">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CompanyName" Visible ="False" FilterControlAltText="Filter CompanyName column"
                                            HeaderText="Company Name" SortExpression="CompanyName"
                                            UniqueName="CompanyName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BranchPlantCode" FilterControlAltText="Filter BranchPlantCode column"
                                            HeaderText="Branch Plant Code" SortExpression="BranchPlantCode"
                                            UniqueName="BranchPlantCode">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BranchPlantName" FilterControlAltText="Filter BranchPlantName column"
                                            HeaderText="Branch Plant" SortExpression="BranchPlantName" UniqueName="BranchPlantName">
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
                                        <telerik:GridBoundColumn DataField="OnHandQuantity" FilterControlAltText="Filter OnHandQuantity column"
                                            HeaderText="FullQty (On-hand)" SortExpression="OnHandQuantity" UniqueName="OnHandQuantity">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TransitQuantity" FilterControlAltText="Filter TransitQuantity column"
                                            HeaderText="FullQty (In-Transit)" SortExpression="TransitQuantity" UniqueName="TransitQuantity">
                                        </telerik:GridBoundColumn>
                                       
                                        <telerik:GridBoundColumn DataField="EmptyQuantityOnHand" FilterControlAltText="Filter EmptyQuantityOnHand column"
                                            HeaderText="EmptyQty (On-hand)" SortExpression="EmptyQuantityOnHand" UniqueName="EmptyQuantityOnHand">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmptyQuantityInTransit" FilterControlAltText="Filter EmptyQuantityInTransit column"
                                            HeaderText="EmptyQty (In-Transit)" SortExpression="EmptyQuantityInTransit" UniqueName="EmptyQuantityInTransit">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                        <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" >
            </Scrolling>
            <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
        </ClientSettings>
                            </telerik:RadGrid>
                       
    <asp:ObjectDataSource ID="ObjInventory" runat="server" SelectMethod="GetTabKInventoryByEmployeeID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabKInventory" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
