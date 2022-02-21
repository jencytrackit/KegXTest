<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="frmKegPurchaseModule_SupToCus.aspx.vb" Inherits="KEG_X.TrackITKTS.frmKegPurchaseModule_SupToCus" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">

        function DeleteConfirm() {
            if (confirm("Are you sure you want to delete this record?")) {
                return true;
            }
            return false;
        }
    </script>
    <ATS:Session ID="chkSession" runat="Server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="f-left">
    <asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />
    </div>
    <div id="first-div" class="div-right">
        <telerik:RadButton ID="RadButton1" OnClientClicking="OnClientClicking" NavigateUrl="frmAddEditKegPurchase_SupToCus.aspx"
            HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Skin="WebBlue"
            Width="100px" Text="Add" runat="server" />
    </div>
    <div id="second-div" class="div-left">    
    <h2>
        <asp:Literal ID="Literal1" runat="server" Text="Keg Purchase From Supplier To Customer" /></h2>
        </div>
        <div style="clear: both;"></div>
    <telerik:RadGrid ID="RadgvFullKegReceive" runat="server" CellSpacing="0" GridLines="None" Width="100%"
        DataSourceID="ObjFullKegReceive" Skin="WebBlue" AllowPaging="True" AllowFilteringByColumn="true">
        <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
            NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>" DataSourceID="ObjFullKegReceive"
            DataKeyNames="TransactionNumber">
            <DetailTables>
                <telerik:GridTableView DataKeyNames="FullKegID" Name="ChildItems" Width="100%" AutoGenerateColumns="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="FullKegID" FilterControlAltText="Filter FullKegID column"
                            HeaderText="FullKegID" SortExpression="FullKegID" UniqueName="FullKegID" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ItemID" FilterControlAltText="Filter ItemID column"
                            HeaderText="ItemID" SortExpression="ItemID" UniqueName="ItemID" Visible="false">
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
                        <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter Quantity column"
                            HeaderText="Quantity" SortExpression="Quantity" UniqueName="Quantity">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="DeleteButton" CommandName="deletechild" CommandArgument='<%# Eval("FullKegID") %>'
                                    ImageUrl="../Images/Delete.gif" OnClientClick="return DeleteConfirm()" />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
            <Columns>
                <telerik:GridBoundColumn DataField="CompanyCode" FilterControlAltText="Filter CompanyCode column"
                    HeaderText="Company Code" SortExpression="CompanyCode" UniqueName="CompanyCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CompanyName" FilterControlAltText="Filter CompanyName column"
                    HeaderText="Company Name" SortExpression="CompanyName" UniqueName="CompanyName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TransactionNumber" FilterControlAltText="Filter TransactionNumber column"
                    HeaderText="Transaction Number" SortExpression="TransactionNumber" UniqueName="TransactionNumber">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SupplierCode" FilterControlAltText="Filter SupplierCode column"
                    HeaderText="Supplier Code" SortExpression="SupplierCode" UniqueName="SupplierCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SupplierName" FilterControlAltText="Filter SupplierName column"
                    HeaderText="SupplierName" SortExpression="SupplierName" UniqueName="SupplierName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CustomerCode" FilterControlAltText="Filter CustomerCode column"
                    HeaderText="Customer Code" SortExpression="CustomerCode" UniqueName="CustomerCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CustomerName" FilterControlAltText="Filter CustomerName column"
                    HeaderText="Customer Name" SortExpression="CustomerName" UniqueName="CustomerName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ReceiptDate" FilterControlAltText="Filter ReceiptDate column"
                    HeaderText="Receipt date" SortExpression="ReceiptDate" UniqueName="ReceiptDate">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BOLNumber" FilterControlAltText="Filter BOLNumber column"
                    HeaderText="BOL No." SortExpression="BOLNumber" UniqueName="BOLNumber">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BOLDate" FilterControlAltText="Filter BOLDate column"
                    HeaderText="BOL Date" SortExpression="BOLDate" UniqueName="BOLDate">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="POtype" FilterControlAltText="Filter POtype column"
                    HeaderText="PO type" SortExpression="POtype" UniqueName="POtype">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PONumber" FilterControlAltText="Filter PONumber column"
                    HeaderText="PO Number" SortExpression="PONumber" UniqueName="PONumber">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ContainerNumber" FilterControlAltText="Filter ContainerNumber column"
                    HeaderText="Container Number" SortExpression="ContainerNumber" UniqueName="ContainerNumber">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="View/Edit" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="EditButton" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container,"RowIndex") %>'
                            ImageUrl="../Images/Edit.gif" />
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="DeleteButton" CommandName="delete" CommandArgument='<%# Eval("TransactionNumber") %>'
                            ImageUrl="../Images/Delete.gif" OnClientClick="return DeleteConfirm()" />
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
    <asp:ObjectDataSource ID="ObjFullKegReceive" runat="server" SelectMethod="GetAllTabFullKegSuppToCust"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabFullKegSuppToCust"
        OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
            <asp:Parameter Name="TransactionNumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
