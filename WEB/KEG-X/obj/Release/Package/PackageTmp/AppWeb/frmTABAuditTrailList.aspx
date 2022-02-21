<%@ Page Language="vb" MasterPageFile="~/Templates/MasterPage.Master" AutoEventWireup="false"
    Inherits="KEG_X.TrackITKTS.frmTABAuditTrailList" Title="Manage AuditTrail"
    CodeBehind="frmTABAuditTrailList.aspx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
    
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
  
                                                                        <h2>
                                                                            <asp:Literal ID="Literal1" runat="server" Text="Manage Audit Trail" /></h2>
                                                                    <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" AllowFilteringByColumn="true" DataSourceID="ObjDSTABAuditTrail" 
                                                                        GridLines="None" AllowPaging="true" skin="WebBlue">
                                                                        <MasterTableView AutoGenerateColumns="False" DataSourceID="ObjDSTABAuditTrail" >
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="AuditID" FilterControlAltText="Filter AuditID column"
                                                                                    AllowFiltering="true" HeaderText="AuditID" Visible="false" SortExpression="AuditID"
                                                                                    UniqueName="AuditID">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="UserName" FilterControlAltText="Filter UserName column"
                                                                                    HeaderText="User Name" SortExpression="UserName" UniqueName="UserName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="ActionName" FilterControlAltText="Filter ActionName column"
                                                                                    HeaderText="Action Name" SortExpression="ActionName"
                                                                                    UniqueName="ActionName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="FunctionName" FilterControlAltText="Filter FunctionName column"
                                                                                    HeaderText="Function Name" SortExpression="FunctionName"
                                                                                    UniqueName="FunctionName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="ActionDate" FilterControlAltText="Filter ActionDate column"
                                                                                    HeaderText="ActionDate" SortExpression="ActionDate"
                                                                                    UniqueName="ActionDate"  HtmlEncode="false">
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                                <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" >
            </Scrolling>
            <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
        </ClientSettings>
                                                                    </telerik:RadGrid>
                                                               
    <asp:ObjectDataSource ID="ObjDSTABAuditTrail" runat="server" SelectMethod="GetAllTABAuditTrail" DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTABAuditTrail" OldValuesParameterFormatString="{0}">
			<SelectParameters>
			<asp:Parameter Name="valEmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
			</SelectParameters>
			</asp:ObjectDataSource>
</asp:Content>
