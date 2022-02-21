<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmEmployeeList.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmEmployeeList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="f-left">
  <asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />
                        </div>           
  <div id="first-div" class="div-right">
        <telerik:RadButton ID="RadButton1" OnClientClicking="OnClientClicking" NavigateUrl="frmAddEditEmployees.aspx"
            HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Skin="WebBlue"
            Width="100px" Text="Add" runat="server" />
    </div>
    <div id="second-div" class="div-left">
                                                                        <h2>
                                                                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:lang, ManageEmployees %>" /></h2>
                                                                            </div>
                                                                            <div style="clear: both;"></div>
                                                                    <%--  <div style="clear: both;">
                                                                      
                                                                         <asp:Label ID="Label2" Text="From Company" runat="server" />*
                                                                      
                                                                      </div>--%>
                                                                     
                                    <div class="f-left"></div> 

  
                                                                    <telerik:RadGrid ID="RadgvEmployees" runat="server" CellSpacing="0" GridLines="None" DataSourceID="ObjEmployees"
                                                                        Skin="WebBlue" AllowPaging="True" AllowFilteringByColumn="true" AllowSorting="true">
                                                                        <GroupingSettings CaseSensitive="false" />
                                                                        <MasterTableView AutoGenerateColumns="false" NoMasterRecordsText="<%$ Resources:lang, msgNoRecords %>"
                                                                            NoDetailRecordsText="<%$ Resources:lang, msgNoRecords %>"  DataSourceID="ObjEmployees" DataKeyNames="UserName">
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="EmployeeID" FilterControlAltText="Filter EmployeeID column"
                                                                                    Visible="false" HeaderText="EmployeeID" SortExpression="EmployeeID" UniqueName="EmployeeID">
                                                                                </telerik:GridBoundColumn>
                                                                                
                                                                                <telerik:GridBoundColumn DataField="EmployeeNo" FilterControlAltText="Filter EmployeeNo column"
                                                                                    HeaderText="<%$ Resources:lang, lblEmployeeNo %>" SortExpression="EmployeeNo"
                                                                                    UniqueName="EmployeeNo">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="EmployeeName" FilterControlAltText="Filter EmployeeName column"
                                                                                    HeaderText="<%$ Resources:lang, gridEmployeeName %>" SortExpression="EmployeeName"
                                                                                    UniqueName="EmployeeName">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="UserName" FilterControlAltText="Filter UserName column"
                                                                                    HeaderText="User Name" SortExpression="UserName" UniqueName="UserName">
                                                                                </telerik:GridBoundColumn>
                                                                              
                                                                                <telerik:GridBoundColumn DataField="Position" FilterControlAltText="Filter Position column"
                                                                                    HeaderText="<%$ Resources:lang, gridPosition %>" SortExpression="Position" UniqueName="Position">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Mobile" FilterControlAltText="Filter Mobile column"
                                                                                    HeaderText="<%$ Resources:lang, lblMobile %>" SortExpression="Mobile" UniqueName="Mobile">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column"
                                                                                    HeaderText="<%$ Resources:lang, gridEmail %>" SortExpression="Email" UniqueName="Email">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridTemplateColumn HeaderText="IsOrg" AllowFiltering="false" Visible="false">
                                                                                    <ItemTemplate>
                                                                                       <asp:Label ID="lblIsOrg" runat="server" Text='<%# Eval("IsOrganisation") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                                </telerik:GridTemplateColumn>
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
                                                              
    <asp:ObjectDataSource ID="ObjEmployees" runat="server" SelectMethod="GetALLTabMEmployeesByEmployeeID"
        DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTabMEmployees" OldValuesParameterFormatString="{0}">
        <SelectParameters>
            <asp:Parameter Name="valUserName" Type="String" />
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="SchemaName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
