<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmAddEditRoles.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmAddEditRoles" %>
    <%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
 <%--   <ats:session id="chkSession" runat="Server" />--%>
    <script language="javascript" type="text/javascript">
        function redirectme(sender, args) {
            location.href = 'frmRoles.aspx';
        }
        function OnClientClicked(button, args) {
            document.forms[0].reset();
            button.set_autoPostBack(false);
            // if (window.confirm("Are you sure you want to submit the page?")) {
            // button.set_autoPostBack(true);
            // }
            // else {
            // button.set_autoPostBack(false);
            // }
        }
    </script>
    <script language="javascript">
        function ChangeCheckBoxState(id, checkState) {
            var cb = document.getElementById(id);
            if (cb != null && cb.disabled == false)
                cb.checked = checkState;
        }

        function ChangeAllCheckBoxStatesView(id) {
            var masterTable = $find("<%= RadgridTABPrevileges.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == true) {
                //alert(row.length);
                for (var i = 0; i < row.length; i++) {
                    if (masterTable.get_dataItems()[i].findElement("chkView").disabled == true) {
                        masterTable.get_dataItems()[i].findElement("chkView").checked = false;

                    }
                    else {
                        masterTable.get_dataItems()[i].findElement("chkView").checked = true;
                    }
                }
            }
            else {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("chkView").checked = false;
                }
            }

        }

        function ChangeAllCheckBoxStatesAdd(id) {
            var masterTable = $find("<%= RadgridTABPrevileges.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == true) {
                //alert(row.length);

                for (var i = 0; i < row.length; i++) {
                    if (masterTable.get_dataItems()[i].findElement("chkAdd").disabled == true) {
                        masterTable.get_dataItems()[i].findElement("chkAdd").checked = false;
                    }
                    else {
                        masterTable.get_dataItems()[i].findElement("chkAdd").checked = true;

                    }

                }
            }
            else {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("chkAdd").checked = false;
                }
            }
        }
        function ChangeAllCheckBoxStatesEdit(id) {
            var masterTable = $find("<%= RadgridTABPrevileges.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == true) {
                //alert(row.length);
                for (var i = 0; i < row.length; i++) {
                    if (masterTable.get_dataItems()[i].findElement("chkEdit").disabled == true) {
                        masterTable.get_dataItems()[i].findElement("chkEdit").checked = false;

                    }
                    else {
                        masterTable.get_dataItems()[i].findElement("chkEdit").checked = true;
                    }

                }
            }
            else {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("chkEdit").checked = false;
                }
            }
        }
        function ChangeAllCheckBoxStatesDelete(id) {
            var masterTable = $find("<%= RadgridTABPrevileges.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == true) {
                //alert(row.length);
                for (var i = 0; i < row.length; i++) {
                    if (masterTable.get_dataItems()[i].findElement("chkDelete").disabled == true) {
                        masterTable.get_dataItems()[i].findElement("chkDelete").checked = false;

                    }
                    else {
                        masterTable.get_dataItems()[i].findElement("chkDelete").checked = true;
                    }


                }
            }
            else {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("chkDelete").checked = false;
                }
            }
        }
    </script>
   <%--  <ats:session ID="chkSession" runat="Server" />
         <ats:session ID="Session1" runat="Server" />--%>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Textbox"
        EnableRoundedCorners="true" />
    <h2><asp:Label ID="addATSAssetPMSchedule" Text="<%$ Resources:lang, addTABRole %>" runat="server" /></h2>
    <em>* Required Fields</em>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table id="TABLE13" cellspacing="0" cellpadding="0" height="100%" width="100%" border="0"
         runat="server">
        <tr>
            <td height="100%" valign="Top">
                <ATSMenu:Menu ID="MENULIST" runat="Server" />
                <%--  <iiscc:SiteCompass ID="SiteCompass105" runat="server" CssClass="compassStyle1" DisplayTitle="<%$ Resources:lang, addTABRole %>"
                    CssClassLink="compassLinkStyle1" MaxItems="5" DivName="compassDiv"></iiscc:SiteCompass>--%>
                <table id="Table1" cellspacing="0" cellpadding="0" width="97%" align="left" border="0"
                     runat="server">
                        
                 
                        
                      <tr>
                            <td valign="bottom" width="100%" align="center">
                                <asp:Label ID="Label5" Text="From Company" runat="server" Font-Bold="true" />&nbsp;&nbsp; <font color='red'>*</font>&nbsp;&nbsp;
                              <telerik:RadComboBox ID="rcbFromCmpny" runat="server" AutoPostBack="true" EnableViewState ="true" Filter="Contains"
                                    Width="195px">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID='rfvrdcCmpnny'  ControlToValidate='rcbFromCmpny' ErrorMessage="Select Company"
                                    Display='None' runat='server' ValidationGroup="Add" InitialValue="--Select One--" />
                            </td>
                        </tr>

                        <tr>
                        <td>  &nbsp; &nbsp <br /> </td>
                        </tr>


                    <tr>
                        <td valign="bottom" width="100%" align="center">
                        
                            <asp:Label ID="lblRoleName" Text="<%$ Resources:lang, lblRoleName %>" Font-Bold="true"
                                runat="server" />&nbsp;&nbsp; <font color='red'>*</font>&nbsp;&nbsp;
                            <telerik:RadTextBox ID='radRoleName' MaxLength='50' runat='server' />
                            <asp:RequiredFieldValidator ID='rfvRoName' Display ="None" ControlToValidate='radRoleName' ErrorMessage="Enter Role Name"
                                runat='server' ValidationGroup="Add" />


                                <br />
                                <asp:Label ID="lblMsg" runat="server" CssClass="noteMsg" />  
                                <br />
                        </td>
                    </tr>


                </table>
                <table id="Table2" cellspacing="0" cellpadding="0" width="97%" align="left" border="0"
                     runat="server">
                    <tr>
                        <td><br />
                            <table id="Table3" cellspacing="1" cellpadding="0" width="100%" align="center" border="0"
                                 runat="server">
                                <tr bgcolor="white">
                                    <td>
                                        <table id="Table7" cellspacing="1" cellpadding="0" width="750" align="center" border="0"
                                             runat="server">
                                            <tr>
                                                <td bgcolor="#ffffff">
                                                        <telerik:RadGrid ID="RadgridTABPrevileges" runat="server" CellSpacing="0" Skin="WebBlue" DataSourceID="ObjDSTABPrevileges"
                                                            GridLines="None">
                                                            <MasterTableView AutoGenerateColumns="False" DataSourceID="ObjDSTABPrevileges">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="PrivilegeID" FilterControlAltText="Filter PrivilegeID column"
                                                                        Visible="false" HeaderText="<%$ Resources:lang, gridPrivilegeID %>" SortExpression="PrivilegeID"
                                                                        UniqueName="PrivilegeID">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Feature_en" FilterControlAltText="Filter Feature_en column"
                                                                        HeaderText="<%$ Resources:lang, gridFeature_en %>" SortExpression="Feature_en"
                                                                        UniqueName="Feature_en">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn  AllowFiltering="false">
                                                                        <ItemTemplate>
                                                                              <asp:CheckBox ID="chkView" Enabled='<%# Convert.ToBoolean(Eval("ImpView")) %>' Checked='<%# Convert.ToBoolean(Eval("AllowView")) %>'
                                                                            runat="server" />
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="checkAllView" Text="<%$ Resources:lang, gridAllowView %>" runat="server" onclick="ChangeAllCheckBoxStatesView(this)"/>
                                                                        </HeaderTemplate>
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn AllowFiltering="false">
                                                                        <ItemTemplate>
                                                                              <asp:CheckBox ID="chkAdd" Enabled='<%# Convert.ToBoolean(Eval("ImpAdd")) %>' Checked='<%# Convert.ToBoolean(Eval("AllowAdd")) %>'
                                                                            runat="server" />
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="checkAllAdd" Text="<%$ Resources:lang, gridAllowAdd %>" runat="server" onclick="ChangeAllCheckBoxStatesAdd(this)"/>
                                                                        </HeaderTemplate>
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    </telerik:GridTemplateColumn>
                                                                     <telerik:GridTemplateColumn  AllowFiltering="false">
                                                                        <ItemTemplate>
                                                                          <asp:CheckBox ID="chkEdit" Enabled='<%# Convert.ToBoolean(Eval("ImpEdit")) %>' Checked='<%# Convert.ToBoolean(Eval("AllowEdit")) %>'
					                                                            runat="server" />
                                                                        </ItemTemplate>
                                                                         <HeaderTemplate>
                                                                            <asp:CheckBox ID="checkAllEdit" Text="<%$ Resources:lang, gridAllowEdit %>" runat="server" onclick="ChangeAllCheckBoxStatesEdit(this)"/>
                                                                        </HeaderTemplate>
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn  AllowFiltering="false">
                                                                        <ItemTemplate>
                                                                           <asp:CheckBox ID="chkDelete" Enabled='<%# Convert.ToBoolean(Eval("ImpDelete")) %>'
                                                                            Checked='<%# Convert.ToBoolean(Eval("AllowDelete")) %>' runat="server" />
                                                                        </ItemTemplate>
                                                                         <HeaderTemplate>
                                                                            <asp:CheckBox ID="checkAllDelete" Text="<%$ Resources:lang, gridAllowDelete %>" runat="server" onclick="ChangeAllCheckBoxStatesDelete(this)"/>
                                                                        </HeaderTemplate>
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    </telerik:GridTemplateColumn>
                                                                   <telerik:GridTemplateColumn  AllowFiltering="false" Visible="false">
                                                                        <ItemTemplate>
                                                                              <asp:Label ID="lblgridMenuID" runat="server" Text='<%# Eval("MenuID") %>' ></asp:Label>
                                                                        </ItemTemplate>
                                  
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" bgcolor="#ffffff"><br /><br />
                                                    <telerik:RadButton ID='radSave'  ValidationGroup ="Add" CausesValidation ="true" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" Width="100px" Text="<%$ Resources:lang, btnSave %>"
                                                        runat='server' />
                                                    &nbsp;&nbsp;
                                                    <telerik:RadButton ID="radClear" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" runat="server" Width="100px" Text="<%$ Resources:lang, btnClear %>"
                                                        OnClientClicked="OnClientClicked" CausesValidation="False"  />
                                                    &nbsp;&nbsp;
                                                    <telerik:RadButton ID="radCancel" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px" runat="server" Width="100px" AutoPostBack="false"
                                                        OnClientClicked="redirectme" Text="<%$ Resources:lang, btnCancel %>" CausesValidation="False" />
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID='summary' ValidationGroup ="Add" runat='server' ShowSummary='False' ShowMessageBox='True'
                    HeaderText="<%$ Resources:lang, Errors %>"></asp:ValidationSummary>
                <asp:ObjectDataSource ID="ObjDSTABPrevileges" runat="server" SelectMethod="GetAllTABPrivilegeMasterByRoleID"
                    DeleteMethod="DeletetTest" TypeName="BLL.TrackITKTS.clsTABRolePrivileges"
                    OldValuesParameterFormatString="{0}">
                    <SelectParameters>
                        <asp:Parameter Name="RoleID" Type="Int32" />
                        <asp:Parameter Name="SchemaName" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
               
            </td>
        </tr>
    </table>
   </ContentTemplate> 
   </asp:UpdatePanel> 
</asp:Content>
