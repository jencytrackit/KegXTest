<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="rptDownloadCustApprovedData.aspx.vb" Inherits="KEG_X.TrackITKTS.rptDownloadCustApprovedData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
     <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/AppWeb/LookupWebService.asmx" />
        </Services>
    </telerik:RadScriptManager>
    
    
    <h2>
        <asp:Label ID="addATSEmployees" Text="Download Customer Approved Quantity Dump" runat="server" Font-Size ="Medium" /></h2
        <table id="TABLE100" cellspacing="0" cellpadding="0" height="100%" width="100%" border="0"
        runat="server">
        <tr>
            <td height="450px" valign="Top">
                <div>
                    <table id="TABLE1" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                        class="table" rules='none' width="80%" border='0' runat="server">
                        <tr>
                            <td bgcolor='white' colspan="4">
                                <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                                    <td class="sideHead">
                                        <asp:Label ID="lblDate" Text="Date From" runat="server" />*
                                    </td>
                                    <td class="sideHead1">
                                        <asp:Label ID="Label2" Text="From" Visible="false" runat="server" />
                                        <telerik:RadDatePicker ID="FromDt" runat="server" DateInput-DateFormat="dd/MM/yy"
                                            ShowPopupOnFocus="true">
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                            <td class="sideHead">
                                <asp:Label ID="Label4" Text="Items" runat="server" />*
                            </td>
                            <td class="sideHead1">
                                <asp:HiddenField ID="hdfItemIDs" runat="server" />
                                <telerik:RadComboBox runat="server" ID="rcbItems" Width="500px" EmptyMessage="Select Items(s)"
                                    CheckBoxes="true" Filter="Contains" EnableTextSelection="true" EnableCheckAllItemsCheckBox="True"
                                    EnableItemCaching="True" AutoCompleteSeparator="," Localization-AllItemsCheckedString="All Items Selected"
                                    Localization-ItemsCheckedString="Items Selected" Localization-CheckAllString="Select All">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                       
                         <tr>
                            <td bgcolor='white' colspan="4">
                                <asp:Label ID='Label1' runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                          <td class="sideHead">
                                   </td>
                            <td class="sideHead" colspan="4"> 
                                <telerik:RadButton ID="radSave" ButtonType="SkinnedButton" BorderColor="Black" Font-Bold="true"  ForeColor="gray" Font-Size="8px"
                                    Width="100px" Text="Download" runat="server"
                                    AutoPostBack="true" />
                                
                          
                            </td>
                        </tr>
                        
                    </table>
                    <br />
                  
                </div>
            </td>
        </tr>
    </table>


</asp:Content>
