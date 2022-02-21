<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmReportRequestsX.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmReportRequestsX" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script language="javascript" type="text/javascript">

        function ConfirmExcelDownload() {
            if (confirm("Are you sure you want to download the data in Excel format?")) {
                return true;
            }
            return false;
        }
        function ConfirmPDFDownload() {
            if (confirm("Are you sure you want to download the data in PDF format?")) {
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
        <asp:Literal ID="Literal1" runat="server" Text="REPORT REQUESTS - QUEUED" /></h2>
         </div>
    <div style="clear: both;">
    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
                        <ContentTemplate>
    <telerik:RadGrid ID="RadgvReportRequests" runat="server" CellSpacing="0" DataSourceID="ObjReportRequests"
        GridLines="None" AllowPaging="True" AllowFilteringByColumn="true" Skin="WebBlue" Width="100%"
        AllowSorting="true">
        <GroupingSettings CaseSensitive="false" />
        <MasterTableView AutoGenerateColumns="false" DataSourceID="ObjReportRequests" NoMasterRecordsText="No Records to display"
            NoDetailRecordsText="No Records to display">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="RequestID" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="lblRequestID" CommandName="batch" runat="server" Text='<%#Eval("RequestID")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="RequestName" FilterControlAltText="Filter RequestName column"
                    HeaderText="Request Name" SortExpression="RequestName" Visible="false" UniqueName="RequestName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" FilterControlAltText="Filter Description column"
                    HeaderText="Report Name" SortExpression="Description" UniqueName="Description">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Parameters" FilterControlAltText="Filter Parameters column"
                    HeaderText="Parameters" SortExpression="Parameters" UniqueName="Parameters">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EmployeeName" FilterControlAltText="Filter EmployeeName column" 
                    HeaderText="Requested By" SortExpression="EmployeeName" UniqueName="EmployeeName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RequestedDate" FilterControlAltText="Filter RequestedDate column"
                    HeaderText="Requested Date" SortExpression="RequestedDate"
                    UniqueName="RequestedDate"  HtmlEncode="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Status" FilterControlAltText="Filter Status column"
                    HeaderText="Status" SortExpression="Status" UniqueName="Status">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Excel" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkExcelDownload" CommandName="ExcelDownload" runat="server" Text='<%# IIf(Eval("Download") = "0", "", "Download")%>'
                            Font-Underline="true" ForeColor="Blue" OnClientClick="return ConfirmExcelDownload()" Visible='<%# Eval("Download") %>' ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="PDF" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkPDFDownload" CommandName="PDFDownload" runat="server" Text='<%# IIf(Eval("Download") = "0", "", "Download")%>'
                            Font-Underline="true" ForeColor="Blue" OnClientClick="return ConfirmPDFDownload()" Visible='<%# Eval("Download") %>' ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ProcessedDate" FilterControlAltText="Filter ProcessedDate column"
                    HeaderText="Processed Date" SortExpression="ProcessedDate"
                    UniqueName="ProcessedDate"  HtmlEncode="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Comments" FilterControlAltText="Filter Comments column"
                    HeaderText="Comments" SortExpression="Comments" UniqueName="Comments">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
          <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
            </Scrolling>
        </ClientSettings>
    </telerik:RadGrid>
    <asp:ObjectDataSource ID="ObjReportRequests" runat="server" SelectMethod="GetTabReportRequestsByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsReports" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
                        </ContentTemplate>
                           <Triggers>
                                <asp:PostBackTrigger ControlID="RadgvReportRequests" />
                            </Triggers>
                    </asp:UpdatePanel>
</asp:Content>
