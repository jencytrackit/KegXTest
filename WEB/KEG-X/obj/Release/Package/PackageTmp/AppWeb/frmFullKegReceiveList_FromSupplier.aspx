<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmFullKegReceiveList_FromSupplier.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmFullKegReceiveList_FromSupplier" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
      <div class="f-left"><asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />
      </div>
        <div id="first-div" class="div-right">
    </div>
    <div id="second-div" class="div-left">
    <h2>
        <asp:Literal ID="Literal1" runat="server" Text="Full Keg Receive List - From Supplier" /></h2>
          </div>
    <div style="clear: both;">
    </div>
    <telerik:RadGrid ID="RadgvFullKegReceive" runat="server" CellSpacing="0" GridLines="None"
        DataSourceID="ObjSupplierReceive" Skin="WebBlue" AllowPaging="True" AllowFilteringByColumn="true"
        AllowSorting="true">
        <GroupingSettings CaseSensitive="false" />
        <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
            NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>" DataSourceID="ObjSupplierReceive">
            <Columns>
                <telerik:GridBoundColumn DataField="CompanyCode" Visible="False" FilterControlAltText="Filter CompanyCode column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Company Code" SortExpression="CompanyCode" UniqueName="CompanyCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CompanyName" Visible="False" FilterControlAltText="Filter CompanyName column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Company Name" SortExpression="CompanyName" UniqueName="CompanyName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SupplierCode" FilterControlAltText="Filter SupplierCode column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Supplier Code" SortExpression="SupplierCode" UniqueName="SupplierCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SupplierName" FilterControlAltText="Filter SupplierName column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="SupplierName" SortExpression="SupplierName" UniqueName="SupplierName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PONumber" FilterControlAltText="Filter PONumber column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="PO Number" SortExpression="PONumber" UniqueName="PONumber">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="POType" FilterControlAltText="Filter POType column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Order Type" SortExpression="POType" UniqueName="POType">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BranchPlantCode" FilterControlAltText="Filter BranchPlantCode column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Receiving Branch Plant Code" SortExpression="BranchPlantCode" UniqueName="BranchPlantCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BranchPlantName" FilterControlAltText="Filter BranchPlantName column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Receiving Branch Plant" SortExpression="BranchPlantName" UniqueName="BranchPlantName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemCode" FilterControlAltText="Filter ItemCode column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Item Code" SortExpression="ItemCode" UniqueName="ItemCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName1" FilterControlAltText="Filter ItemName1 column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Item Name1" SortExpression="ItemName1" UniqueName="ItemName1">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName2" FilterControlAltText="Filter ItemName2 column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Item Name2" SortExpression="ItemName2" UniqueName="ItemName2">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UOM" FilterControlAltText="Filter UOM column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="UOM" SortExpression="UOM" UniqueName="UOM">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter Quantity column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Quantity" SortExpression="Quantity" UniqueName="Quantity">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ReceivedDate" FilterControlAltText="Filter ReceivedDate column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="JDE - Received Date" SortExpression="ReceivedDate" UniqueName="ReceivedDate">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ContainerNumber" FilterControlAltText="Filter ContainerNumber column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Container No." SortExpression="ContainerNumber" UniqueName="ContainerNumber">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BOLNumber" FilterControlAltText="Filter BOLNumber column" ItemStyle-HorizontalAlign="Left"
                    HeaderText="BOL No." SortExpression="BOLNumber" UniqueName="BOLNumber">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
            </Scrolling>
            <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:ObjectDataSource ID="ObjSupplierReceive" runat="server" SelectMethod="GetTabKSupplierReceiveByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabKSupplierReceive" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
