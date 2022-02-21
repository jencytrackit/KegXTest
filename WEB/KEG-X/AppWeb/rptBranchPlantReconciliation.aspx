<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="rptBranchPlantReconciliation.aspx.vb" Inherits="KEG_X.TrackITKTS.rptBranchPlantReconciliation" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral,
PublicKeyToken=692FBEA5521E1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
<style type="text/css">
    .overlay {
        background-color: #000;
        cursor: wait;
        display: none;
        height: 100%;
        left: 0;
        opacity: 0.4;
        position: fixed;
        top: 0;
        width: 100%;
        z-index: 9999998;
    }
</style>
    <script language="javascript" type="text/javascript">


        function showOverlay() {
            document.getElementById("overlay").style.display = "block";
        }

        function hideOverlay() {
            document.getElementById("overlay").style.display = "none";
        }

    </script>
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <h2><asp:Label ID="addATSUsers" Text="Branch Plant Reconciliation Report" runat="server" /></h2>
    <em>* Required Fields</em>
    <table id="TABLE2" cellspacing="0" cellpadding="0" height="450px" width="100%" border="0"
         runat="server">
        <tr>
            <td height="450px" valign="Top">
                <div>
                    <br />
                    <table id="TABLE4" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                        rules='none' width="50%" border='0' 
                        runat="server">
                        <tr>
                            <td bgcolor='white' valign="top" colspan="2">
                                <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="sideHead" >
                                <asp:Label ID="lblCompanyName" Text="<%$ Resources:lang, lblCompanyName %>" runat="server" />*
                            </td>
                            <td class="sideHead1" >
                                <telerik:RadComboBox ID="ddlCompanyName" runat="server" Filter="Contains" Width="195px"
                                    AutoPostBack="true" CausesValidation="false">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvCompanyName" ControlToValidate="ddlCompanyName"
                                    ErrorMessage="<%$ Resources:lang, rfvCompanyName %>" Display="None" InitialValue="--Select One--"
                                    runat="server"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="sideHead">
                                <asp:Label ID="Label1" Text="Branch Plant Code" runat="server" />
                            </td>
                            <td class="sideHead1">
                                <telerik:RadComboBox ID="ddlBranchPlantCode" runat="server" Filter="Contains" Width="195px">
                                </telerik:RadComboBox>
                                &nbsp;&nbsp;<br />
                            </td>
                        </tr>
                         <tr>
                            <td colspan="2" >&nbsp;
                            </td>
                        </tr>
                                              <tr>
                            <td colspan="2" align="center">


                                <div id="overlay" class="overlay"></div>
                               <asp:Button ID="btn" Text ="Show" Width="90px" Font-Size="16px" runat="server" CssClass="btnNew"  />



                            <%--<telerik:RadButton ID="btnShow" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" runat="server" Width="100px" Text="show" />
                            --%>
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="border-bottom: 1px solid #ccddef">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                    </table>
                      <table id="TABLE1" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                        rules='none' width="90%" border='0' runat="server">
                         <tr>
                            <td colspan="2" style="display: block; padding-top: 5px; padding-bottom: 5px; padding-right: 5px;
                                padding-left: 0px">
                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                                    HasDrilldownTabs="False" Height="800px" Width="900px" HasToggleGroupTreeButton="false"
                                    ToolPanelView="None" ReuseParameterValuesOnRefresh="True" EnableDatabaseLogonPrompt="False"
                                    EnableParameterPrompt="False" BestFitPage="false" HasCrystalLogo="False" />
                            </td>
                        </tr>
                        </table>
                    <asp:ValidationSummary ID='summary' runat='server' ShowSummary='False' ShowMessageBox='True'
                        HeaderText="<%$ Resources:lang, Errors %>"></asp:ValidationSummary>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
