<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmBranchPlantList.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmBranchPlantList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">

        function ConfirmBatch() {
            if (confirm("Are you sure you want to disable/enable batch?")) {
                return true;
            }
            return false;
        }
    </script>
    <ATS:Session ID="chkSession" runat="Server" />
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
     <div class="f-left"><asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />
     </div>
      <div id="first-div" class="div-right">
    </div>
    <div id="second-div" class="div-left">
    <h2>
        <asp:Literal ID="Literal1" runat="server" Text="MANAGE BRANCH PLANTS" /></h2>
         </div>
    <div style="clear: both;">
    </div>
    <telerik:RadGrid ID="RadgvBranchPlant" runat="server" CellSpacing="0" DataSourceID="ObjBranchPlant"
        GridLines="None" AllowPaging="True" AllowFilteringByColumn="true" Skin="WebBlue" Width="95%"
        AllowSorting="true">
        <GroupingSettings CaseSensitive="false" />
        <MasterTableView AutoGenerateColumns="false" DataSourceID="ObjBranchPlant" NoMasterRecordsText="No Records to display"
            NoDetailRecordsText="No Records to display">
            <Columns>
                <telerik:GridBoundColumn DataField="CompanyCode" FilterControlAltText="Filter CompanyCode column"
                    HeaderText="Company Code" SortExpression="CompanyCode" Visible ="false" UniqueName="CompanyCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CompanyName" FilterControlAltText="Filter CompanyName column"
                    HeaderText="Company Name" SortExpression="CompanyName" Visible ="False" UniqueName="CompanyName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BranchCode" FilterControlAltText="Filter BranchCode column"
                    HeaderText="BranchPlant Code" SortExpression="BranchCode" UniqueName="BranchCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BranchName" FilterControlAltText="Filter BranchName column"
                    HeaderText="BranchPlant Name" SortExpression="BranchName" UniqueName="BranchName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Address1" FilterControlAltText="Filter Address1 column"
                    HeaderText="Address1" SortExpression="Address1" UniqueName="Address1">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Batch" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbatch" CommandName="batch" runat="server" Text='<%#Eval("Batch")%>'
                            Font-Underline="true" ForeColor="Blue" OnClientClick="return ConfirmBatch()"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="BranchID" AllowFiltering="false" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblBranchID" CommandName="batch" runat="server" Text='<%#Eval("BranchID")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
          <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
            </Scrolling>
        </ClientSettings>
    </telerik:RadGrid>
    <asp:ObjectDataSource ID="ObjBranchPlant" runat="server" SelectMethod="GetTabMBranchPlantByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMBranchPlant" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
