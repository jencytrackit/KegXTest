<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmFullKegTransferList_BPtoBP.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmFullKegTransferList_BPtoBP" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
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
                                                                            <asp:Literal ID="Literal1" runat="server" Text="Full Keg Transfer - BP to BP" /></h2>
                                                                              </div>
    <div style="clear: both;">
    </div>
                                                                    <telerik:RadGrid ID="RadgvFullKegTransfer" runat="server" CellSpacing="0" GridLines="None" DataSourceID="ObjFullKegTransferBP"
                                                                        AllowPaging="True" AllowFilteringByColumn="true" Skin="WebBlue" AllowSorting="true">
                                                                        <GroupingSettings CaseSensitive="false" />
                                                                        <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
                                                                            NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>" DataSourceID="ObjFullKegTransferBP">
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="CompanyCode" Visible="false" FilterControlAltText="Filter CompanyCode column"
                                                                                    HeaderText="Company Code" SortExpression="CompanyCode" UniqueName="CompanyCode">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="CompanyName" Visible="false" FilterControlAltText="Filter CompanyName column"
                                                                                    HeaderText="Company Name" SortExpression="CompanyName" UniqueName="CompanyName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="FromBranchCode" FilterControlAltText="Filter FromBranchCode column"
                                                                                    HeaderText="From Branch Plant Code" SortExpression="FromBranchCode" UniqueName="FromBranchCode">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="FromBranchPlantName" FilterControlAltText="Filter FromBranchPlantName column"
                                                                                    HeaderText="From Branch Plant Name" SortExpression="FromBranchPlantName" UniqueName="FromBranchPlantName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="ToBranchCode" FilterControlAltText="Filter ToBranchCode column"
                                                                                    HeaderText="To Branch Plant Code" SortExpression="ToBranchCode" UniqueName="ToBranchCode">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="ToBranchPlantName" FilterControlAltText="Filter ToBranchPlantName column"
                                                                                    HeaderText="To Branch Plant Name" SortExpression="ToBranchPlantName" UniqueName="ToBranchPlantName">
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
                                                                                    HeaderText="Quantity(On-hand)" SortExpression="OnHandQuantity" UniqueName="OnHandQuantity">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="InTransitQuantity" FilterControlAltText="Filter InTransitQuantity column"
                                                                                    HeaderText="Quantity(In-Transit)" SortExpression="InTransitQuantity" UniqueName="InTransitQuantity">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="TransactionDate" FilterControlAltText="Filter TransactionDate column"
                                                                                    HeaderText="Transaction Date" SortExpression="TransactionDate" UniqueName="TransactionDate">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Batch" FilterControlAltText="Filter Batch column"
                                                                                    AllowFiltering="false" HeaderText="Batch" SortExpression="Batch" UniqueName="Batch">
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                                <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" >
            </Scrolling>
            <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
        </ClientSettings>
                                                                    </telerik:RadGrid>
                                                              
    <asp:ObjectDataSource ID="ObjFullKegTransferBP" runat="server" SelectMethod="GetAllTabKBPTransferBPByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabKBPTransferBP" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />  
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
