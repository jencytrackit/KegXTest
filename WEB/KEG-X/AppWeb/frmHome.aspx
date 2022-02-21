<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmHome.aspx.vb" Inherits="KEG_X.TrackITKTS.frmHome"
    MasterPageFile="~/Templates/MasterPage.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        td
        {
            font-size: 12px;
        }
    </style>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbRegions" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbRegions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rCustomerInOut" />
                    <telerik:AjaxUpdatedControl ControlID="rCustomerTop10" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="pnlMain" runat="server" ScrollBars="Auto">
        <table id="TABLE1" cellspacing="0" cellpadding="0" height="100%" width="100%" align="center"
            border="0">
            <tr>
                <td valign="top" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2">
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" height="100%" width="100%" align="center"
                                    border="0">
                                    <tr>
                                        <td valign="middle" width="80px;">
                                            Region:
                                        </td>
                                        <td align="left" valign="middle" width="250px">
                                            <telerik:RadComboBox ID="rcbRegions" runat="server" Filter="Contains" Width="195px"
                                                DataValueField="Channel" DataTextField="Channel" AutoPostBack="true" CausesValidation="false">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 300px" align="left">
                                            For Customer: <b>OUT=Full Kegs, IN=Empty Kegs</b>
                                        </td>
                                        <td>
                                            For Supplier: <b>IN=Full Kegs, OUT=Empty Kegs</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <telerik:RadHtmlChart runat="server" ID="rCustomerInOut" Width="500px" Height="400px"
                                    Skin="Windows7" EnableAjaxSkinRendering="true" Transitions="true">
                                    <ChartTitle Text="Customer IN/OUT - Region Wise">
                                        <Appearance BackgroundColor="White">
                                            <TextStyle FontSize="12" Bold="true" />
                                        </Appearance>
                                    </ChartTitle>
                                    <Appearance>
                                        <FillStyle BackgroundColor="White"></FillStyle>
                                    </Appearance>
                                    <Legend>
                                        <Appearance BackgroundColor="White" Position="Bottom" Visible="true">
                                        </Appearance>
                                    </Legend>
                                    <PlotArea>
                                        <Appearance>
                                            <FillStyle BackgroundColor="AliceBlue"></FillStyle>
                                        </Appearance>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="FullKegs" Spacing="0" Gap="0.5" Name="OUT">
                                                <LabelsAppearance DataFormatString="{0:N0}" RotationAngle="0">
                                                </LabelsAppearance>
                                                <Appearance FillStyle-BackgroundColor="#4F81BD">
                                                </Appearance>
                                                <TooltipsAppearance BackgroundColor="#4F81BD" Color="White">
                                                    <ClientTemplate>
                                    <div style="width:250px;">
                                         <span style="font-weight:bold; text-decoration:underline;">
                                           Month-Year :  #=dataItem.MNTHYR#
                                        </span><p />
                                        OUT - IN : #=kendo.format(\'{0:N0}\',dataItem.FullKegs)# - #=kendo.format(\'{0:N0}\',dataItem.EmptyKegs)#
                                        <br />
                                        Pending : #=kendo.format(\'{0:N0}\',dataItem.DIFF)# 
                                    </div>
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                            </telerik:ColumnSeries>
                                            <telerik:ColumnSeries DataFieldY="EmptyKegs" Spacing="0" Gap="0" Name="IN">
                                                <LabelsAppearance DataFormatString="{0:N0}" RotationAngle="0">
                                                </LabelsAppearance>
                                                <Appearance FillStyle-BackgroundColor="#C0504D">
                                                </Appearance>
                                                <TooltipsAppearance BackgroundColor="#C0504D" Color="White">
                                                    <ClientTemplate>
                                    <div style="width:250px">
                                       <span style="font-weight:bold; text-decoration:underline;">
                                           Month-Year :  #=dataItem.MNTHYR#
                                        </span><p />
                                        OUT - IN : #=kendo.format(\'{0:N0}\',dataItem.FullKegs)# - #=kendo.format(\'{0:N0}\',dataItem.EmptyKegs)#
                                        <br />
                                        Pending : #=kendo.format(\'{0:N0}\',dataItem.DIFF)# 
                                    </div>
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                            </telerik:ColumnSeries>
                                        </Series>
                                        <XAxis DataLabelsField="MNTHYR">
                                            <%--<TitleAppearance  Position="Center" RotationAngle="0" Text="Month-Year"></TitleAppearance>--%>
                                            <MinorGridLines Visible="false" />
                                            <LabelsAppearance Color="Green">
                                            </LabelsAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <%--<TitleAppearance  Position="Center" RotationAngle="0" Text="FullKegs"></TitleAppearance>--%>
                                            <MinorGridLines Visible="false" />
                                            <LabelsAppearance Color="Green">
                                            </LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                </telerik:RadHtmlChart>
                            </td>
                            <td valign="top">
                                <telerik:RadHtmlChart runat="server" ID="rCustomerTop10" Width="500px" Height="400px"
                                    Skin="Windows7" EnableAjaxSkinRendering="true" Transitions="true">
                                    <ChartTitle Text="Top 10 Default Customers (3 Months) - Region Wise">
                                        <Appearance BackgroundColor="White">
                                            <TextStyle FontSize="12" Bold="true" />
                                        </Appearance>
                                    </ChartTitle>
                                    <Appearance>
                                        <FillStyle BackgroundColor="White"></FillStyle>
                                    </Appearance>
                                    <Legend>
                                        <Appearance BackgroundColor="White" Position="Bottom" Visible="true">
                                        </Appearance>
                                    </Legend>
                                    <PlotArea>
                                        <Appearance>
                                            <FillStyle BackgroundColor="AliceBlue"></FillStyle>
                                        </Appearance>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="FullKegs" Spacing="0" Gap="0.5" Name="OUT">
                                                <LabelsAppearance DataFormatString="{0:N0}" RotationAngle="-50">
                                                </LabelsAppearance>
                                                <Appearance FillStyle-BackgroundColor="#4F81BD">
                                                </Appearance>
                                                <TooltipsAppearance BackgroundColor="#4F81BD" Color="White">
                                                    <ClientTemplate>
                                    <div style="width:310px">
                                        <span style="font-weight:bold; text-decoration:underline;">
                                            #=dataItem.CustomerCode# - #=dataItem.CustomerName#
                                        </span><p />
                                        OUT - IN : #=kendo.format(\'{0:N0}\',dataItem.FullKegs)# - #=kendo.format(\'{0:N0}\',dataItem.EmptyKegs)#
                                        <br />
                                        Pending : #=kendo.format(\'{0:N0}\',dataItem.DIFF)#
                                    </div>
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                            </telerik:ColumnSeries>
                                            <telerik:ColumnSeries DataFieldY="EmptyKegs" Spacing="0" Gap="0" Name="IN">
                                                <LabelsAppearance DataFormatString="{0:N0}" RotationAngle="-50">
                                                </LabelsAppearance>
                                                <Appearance FillStyle-BackgroundColor="#C0504D">
                                                </Appearance>
                                                <TooltipsAppearance BackgroundColor="#C0504D" Color="White">
                                                    <ClientTemplate>
                                    <div style="width:310px">
                                        <span style="font-weight:bold; text-decoration:underline;">
                                            #=dataItem.CustomerCode# - #=dataItem.CustomerName#
                                        </span><p />
                                        OUT - IN : #=kendo.format(\'{0:N0}\',dataItem.FullKegs)# - #=kendo.format(\'{0:N0}\',dataItem.EmptyKegs)#
                                        <br />
                                        Pending : #=kendo.format(\'{0:N0}\',dataItem.DIFF)#
                                    </div>
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                            </telerik:ColumnSeries>
                                        </Series>
                                        <XAxis DataLabelsField="CustomerCode">
                                            <%--<TitleAppearance  Position="Center" RotationAngle="0" Text="Month-Year"></TitleAppearance>--%>
                                            <MinorGridLines Visible="false" />
                                            <LabelsAppearance Color="Green" RotationAngle="-5">
                                            </LabelsAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <%--<TitleAppearance  Position="Center" RotationAngle="0" Text="FullKegs"></TitleAppearance>--%>
                                            <MinorGridLines Visible="false" />
                                            <LabelsAppearance Color="Green">
                                            </LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                </telerik:RadHtmlChart>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <telerik:RadHtmlChart runat="server" ID="rSupplierInOut" Width="500px" Height="400px"
                        Skin="Windows7" EnableAjaxSkinRendering="true" Transitions="true">
                        <ChartTitle Text="Supplier IN/OUT - Across Complete Organization">
                            <Appearance BackgroundColor="White">
                                <TextStyle FontSize="12" Bold="true" />
                            </Appearance>
                        </ChartTitle>
                        <Appearance>
                            <FillStyle BackgroundColor="White"></FillStyle>
                        </Appearance>
                        <Legend>
                            <Appearance BackgroundColor="White" Position="Bottom" Visible="true">
                            </Appearance>
                        </Legend>
                        <PlotArea>
                            <Appearance>
                                <FillStyle BackgroundColor="AliceBlue"></FillStyle>
                            </Appearance>
                            <Series>
                                <telerik:ColumnSeries DataFieldY="FullKegs" Spacing="0" Gap="0.5" Name="IN">
                                    <LabelsAppearance DataFormatString="{0:N0}" RotationAngle="0">
                                    </LabelsAppearance>
                                    <Appearance FillStyle-BackgroundColor="#4F81BD">
                                    </Appearance>
                                    <TooltipsAppearance BackgroundColor="#4F81BD" Color="White">
                                        <ClientTemplate>
                                    <div style="width:250px">
                                         <span style="font-weight:bold; text-decoration:underline;">
                                           Month-Year :  #=dataItem.MNTHYR#
                                        </span><p />
                                        IN - OUT : #=kendo.format(\'{0:N0}\',dataItem.FullKegs)# - #=kendo.format(\'{0:N0}\',dataItem.EmptyKegs)#
                                        <br />
                                        Pending : #=kendo.format(\'{0:N0}\',dataItem.DIFF)# 
                                    </div>
                                        </ClientTemplate>
                                    </TooltipsAppearance>
                                </telerik:ColumnSeries>
                                <telerik:ColumnSeries DataFieldY="EmptyKegs" Spacing="0" Gap="0" Name="OUT">
                                    <LabelsAppearance DataFormatString="{0:N0}" RotationAngle="0">
                                    </LabelsAppearance>
                                    <Appearance FillStyle-BackgroundColor="#C0504D">
                                    </Appearance>
                                    <TooltipsAppearance BackgroundColor="#C0504D" Color="White">
                                        <ClientTemplate>
                                    <div style="width:250px">
                                       <span style="font-weight:bold; text-decoration:underline;">
                                           Month-Year :  #=dataItem.MNTHYR#
                                        </span><p />
                                        IN - OUT : #=kendo.format(\'{0:N0}\',dataItem.FullKegs)# - #=kendo.format(\'{0:N0}\',dataItem.EmptyKegs)#
                                        <br />
                                        Pending : #=kendo.format(\'{0:N0}\',dataItem.DIFF)# 
                                    </div>
                                        </ClientTemplate>
                                    </TooltipsAppearance>
                                </telerik:ColumnSeries>
                            </Series>
                            <XAxis DataLabelsField="MNTHYR">
                                <%--<TitleAppearance  Position="Center" RotationAngle="0" Text="Month-Year"></TitleAppearance>--%>
                                <MinorGridLines Visible="false" />
                                <LabelsAppearance Color="Green">
                                </LabelsAppearance>
                            </XAxis>
                            <YAxis>
                                <%--<TitleAppearance  Position="Center" RotationAngle="0" Text="FullKegs"></TitleAppearance>--%>
                                <MinorGridLines Visible="false" />
                                <LabelsAppearance Color="Green">
                                </LabelsAppearance>
                            </YAxis>
                        </PlotArea>
                    </telerik:RadHtmlChart>
                </td>
                <td valign="top">
                    <telerik:RadHtmlChart runat="server" ID="rSupplierTop10" Width="500px" Height="400px"
                        Skin="Windows7" EnableAjaxSkinRendering="true" Transitions="true">
                        <ChartTitle Text="Top 10 Suppliers Across Complete Organization (3 Months)">
                            <Appearance BackgroundColor="White">
                                <TextStyle FontSize="12" Bold="true" />
                            </Appearance>
                        </ChartTitle>
                        <Appearance>
                            <FillStyle BackgroundColor="White"></FillStyle>
                        </Appearance>
                        <Legend>
                            <Appearance BackgroundColor="White" Position="Bottom" Visible="true">
                            </Appearance>
                        </Legend>
                        <PlotArea>
                            <Appearance>
                                <FillStyle BackgroundColor="AliceBlue"></FillStyle>
                            </Appearance>
                            <Series>
                                <telerik:ColumnSeries DataFieldY="FullKegs" Spacing="0" Gap="0.5" Name="IN">
                                    <LabelsAppearance DataFormatString="{0:N0}" RotationAngle="-50">
                                    </LabelsAppearance>
                                    <Appearance FillStyle-BackgroundColor="#4F81BD">
                                    </Appearance>
                                    <TooltipsAppearance BackgroundColor="#4F81BD" Color="White">
                                        <ClientTemplate>
                                    <div style="width:310px;">
                                        <span style="font-weight:bold; text-decoration:underline;">
                                            #=dataItem.SupplierCode# - #=dataItem.SupplierName#
                                        </span><p />
                                        IN - OUT : #=kendo.format(\'{0:N0}\',dataItem.FullKegs)# - #=kendo.format(\'{0:N0}\',dataItem.EmptyKegs)#
                                        <br />
                                        Pending : #=kendo.format(\'{0:N0}\',dataItem.DIFF)#
                                    </div>
                                        </ClientTemplate>
                                    </TooltipsAppearance>
                                </telerik:ColumnSeries>
                                <telerik:ColumnSeries DataFieldY="EmptyKegs" Spacing="0" Gap="0" Name="OUT">
                                    <LabelsAppearance DataFormatString="{0:N0}" RotationAngle="-50">
                                    </LabelsAppearance>
                                    <Appearance FillStyle-BackgroundColor="#C0504D">
                                    </Appearance>
                                    <TooltipsAppearance BackgroundColor="#C0504D" Color="White">
                                        <ClientTemplate>
                                    <div style="width:310px">
                                        <span style="font-weight:bold; text-decoration:underline;">
                                            #=dataItem.SupplierCode# - #=dataItem.SupplierName#
                                        </span><p />
                                        IN - OUT : #=kendo.format(\'{0:N0}\',dataItem.FullKegs)# - #=kendo.format(\'{0:N0}\',dataItem.EmptyKegs)#
                                        <br />
                                        Pending : #=kendo.format(\'{0:N0}\',dataItem.DIFF)#
                                    </div>
                                        </ClientTemplate>
                                    </TooltipsAppearance>
                                </telerik:ColumnSeries>
                            </Series>
                            <XAxis DataLabelsField="SupplierCode">
                                <%--<TitleAppearance  Position="Center" RotationAngle="0" Text="Month-Year"></TitleAppearance>--%>
                                <MinorGridLines Visible="false" />
                                <LabelsAppearance Color="Green" RotationAngle="-5">
                                </LabelsAppearance>
                            </XAxis>
                            <YAxis>
                                <%--<TitleAppearance  Position="Center" RotationAngle="0" Text="FullKegs"></TitleAppearance>--%>
                                <MinorGridLines Visible="false" />
                                <LabelsAppearance Color="Green">
                                </LabelsAppearance>
                            </YAxis>
                        </PlotArea>
                    </telerik:RadHtmlChart>
                </td>
            </tr>
            <tr>
               <td valign="top">
                <telerik:RadHtmlChart runat="server" ID="rBPEmptiesInYard" Width="500px" Height="400px"
                        Skin="Windows7" EnableAjaxSkinRendering="true" Transitions="true">
                        <ChartTitle Text="Empty Kegs in Yard for BP - 1000 (Top 5 Items)">
                            <Appearance BackgroundColor="White">
                                <TextStyle FontSize="12" Bold="true" />
                            </Appearance>
                        </ChartTitle>
                        <Appearance>
                            <FillStyle BackgroundColor="White"></FillStyle>
                        </Appearance>
                        <Legend>
                            <Appearance BackgroundColor="White" Position="Bottom" Visible="true">
                            </Appearance>
                        </Legend>
                        <PlotArea>
                            <Appearance>
                                <FillStyle BackgroundColor="AliceBlue"></FillStyle>
                            </Appearance>
                            <Series>
                                <telerik:ColumnSeries DataFieldY="DIFF" Spacing="0" Gap="1.5" Name="Total Empties">
                                    <LabelsAppearance DataFormatString="{0:N0}" RotationAngle="0">
                                    </LabelsAppearance>
                                    <Appearance FillStyle-BackgroundColor="#4F81BD">
                                    </Appearance>
                                    <TooltipsAppearance BackgroundColor="#4F81BD" Color="White">
                                        <ClientTemplate>
                                    <div style="width:250px">
                                         <span style="font-weight:bold; text-decoration:underline;">
                                            #=dataItem.ItemCode# - #=dataItem.ItemDescription#
                                        </span><p />                                        
                                        Quantity: #=kendo.format(\'{0:N0}\',dataItem.DIFF)# 
                                    </div>
                                        </ClientTemplate>
                                    </TooltipsAppearance>
                                </telerik:ColumnSeries>                                
                            </Series>
                            <XAxis DataLabelsField="ItemCode">
                                <%--<TitleAppearance  Position="Center" RotationAngle="0" Text="Month-Year"></TitleAppearance>--%>
                                <MinorGridLines Visible="false" />
                                <LabelsAppearance Color="Green">
                                </LabelsAppearance>
                            </XAxis>
                            <YAxis>
                                <%--<TitleAppearance  Position="Center" RotationAngle="0" Text="FullKegs"></TitleAppearance>--%>
                                <MinorGridLines Visible="false" />
                                <LabelsAppearance Color="Green">
                                </LabelsAppearance>
                            </YAxis>
                        </PlotArea>
                    </telerik:RadHtmlChart>
                </td>
                <td valign="top">
                
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
