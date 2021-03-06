<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="frmCustomersList.aspx.vb" Inherits="KEG_X.TrackITKTS.frmCustomersList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
   
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>

                                         <div class="f-left"><asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" /></div>
                                          <div id="first-div" class="div-right">
    </div>
    <div id="second-div" class="div-left">
                                                                        <h2>
                                                                            <asp:Literal ID="Literal1" runat="server" Text="MANAGE CUSTOMERS" /></h2>
                                                                             </div>
    <div style="clear: both;">
    </div>
                                                                    <telerik:RadGrid ID="RadgvCustomers" runat="server" CellSpacing="0" GridLines="None" Skin="WebBlue" DataSourceID="ObjDSCustomers"
                                                                        AllowPaging="True" AllowFilteringByColumn ="true" AllowSorting="true" Width="95%">
                                                                       <GroupingSettings CaseSensitive="false" />
                                                                        <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>" DataSourceID="ObjDSCustomers"
                                                                            NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>" DataKeyNames="CustomerID">
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="CustomerID" FilterControlAltText="Filter CustomerID column"
                                                                                    Visible="false" HeaderText="CustomerID" SortExpression="CustomerID" UniqueName="CustomerID">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="CompanyCode"   Visible = "False" FilterControlAltText="Filter CompanyCode column"
                                                                                    HeaderText="Company Code" SortExpression="CompanyCode"
                                                                                    UniqueName="CompanyCode">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="CompanyName"   Visible = "False" FilterControlAltText="Filter CompanyName column"
                                                                                    HeaderText="Company Name" SortExpression="CompanyName"
                                                                                    UniqueName="CompanyName">
                                                                                </telerik:GridBoundColumn>
                                                                                
                                                                                <telerik:GridBoundColumn DataField="CustomerCode" FilterControlAltText="Filter CustomerCode column" 
                                                                                    HeaderText="Customer Code" SortExpression="CustomerCode"
                                                                                    UniqueName="CustomerCode">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="CustomerName" FilterControlAltText="Filter CustomerName column"
                                                                                    HeaderText="Customer Name" SortExpression="CustomerName"
                                                                                    UniqueName="CustomerName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Address1" FilterControlAltText="Filter Address1 column" Visible="false"
                                                                                    HeaderText="Address1" SortExpression="Address1" UniqueName="Address1">
                                                                                </telerik:GridBoundColumn>
                                                                                 <telerik:GridBoundColumn DataField="City" FilterControlAltText="Filter City column" Visible="false"
                                                                                    HeaderText="City" SortExpression="City" UniqueName="City">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="State" FilterControlAltText="Filter State column" Visible="false"
                                                                                    HeaderText="State" SortExpression="State" UniqueName="State">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Country" FilterControlAltText="Filter Country column" Visible="false"
                                                                                    HeaderText="Country" SortExpression="Country" UniqueName="Country">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Phone" FilterControlAltText="Filter Phone column" Visible="false"
                                                                                    HeaderText="Phone" SortExpression="Phone" UniqueName="Phone">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column" Visible="false"
                                                                                    HeaderText="Email" SortExpression="Email" UniqueName="Email">
                                                                                </telerik:GridBoundColumn>
                                                                                 <telerik:GridBoundColumn DataField="Fax" FilterControlAltText="Filter Fax column" Visible="false"
                                                                                    HeaderText="Fax" SortExpression="Fax" UniqueName="Fax">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="WebSite" FilterControlAltText="Filter WebSite column" Visible="false"
                                                                                    HeaderText="WebSite" SortExpression="WebSite" UniqueName="WebSite">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridTemplateColumn HeaderText="<%$ Resources:lang, gridEdit %>" AllowFiltering="false">
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
                                                             
    <asp:ObjectDataSource ID="ObjDSCustomers" runat="server" SelectMethod="GetTabMCustomersByCompanyID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMCustomers" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <%--</form>--%>
</asp:Content>
