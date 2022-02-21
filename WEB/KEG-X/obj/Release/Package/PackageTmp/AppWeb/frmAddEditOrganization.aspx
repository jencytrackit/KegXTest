<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Templates/MasterPage.Master"
    CodeBehind="frmAddEditOrganization.aspx.vb" Inherits="KEG_X.TrackITKTS.frmAddEditOrganization" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<%--<%@ Register TagPrefix="ATSMenu" TagName="Menu" Src="../controls/MenuList.ascx" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <ATS:Session ID="chkSession" runat="Server" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function OnClientFilesUploaded(sender, args) {
                $find('<%= RadAjaxManager1.ClientID %>').ajaxRequest();
            }
            function OnClientFileUploadRemoved(sender, args) {
                document.getElementById('<%= RadBinaryImage1.ClientID %>').style.visibility = "hidden";
            }

            function validationFailed(sender, args) {
                $telerik.$(".ruCancel").parent().remove();
                sender.addFileInput();
                var fileExtention = args.get_fileName().substring(args.get_fileName().lastIndexOf('.') + 1, args.get_fileName().length);
                if (args.get_fileName().lastIndexOf('.') != -1) {//this checks if the extension is correct
                    if (sender.get_allowedFileExtensions().indexOf(fileExtention)) {
                        alert(document.getElementById('<%= employExt.ClientID %>').value);
                    }
                    else {
                        alert(document.getElementById('<%= hdnfileSize.ClientID %>').value);
                    }
                }
                else {
                    alert(document.getElementById('<%= employExt.ClientID %>').value);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <script language="javascript" type="text/javascript">

        function OnlyNumbers(e) {
            e = e || window.event;
            ch = e.which || e.keyCode;
            if (ch != null) {
                if ((ch >= 48 && ch <= 57) || ch == 0 || ch == 8 || ch == 13 || ch == 9 || ch == 43 || ch == 45)
                    return true;
            }

            return false;
        }
    </script>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <asp:HiddenField ID="employExt" Value="<%$ Resources:lang, empImageExt %>" runat="server" />
    <asp:HiddenField ID="hdnfileSize" Value="<%$ Resources:lang, lblFileSize %>" runat="server" />
    <asp:Label ID="lblImageValue" runat="server" Visible="false" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <script language="javascript" type="text/javascript">
        function redirectme(sender, args) {
            location.href = 'frmOrganisationMasterList.aspx';
        }
        function OnClientClicked(button, args) {
            document.forms[0].reset();
            button.set_autoPostBack(false);
        }
    </script>
    <h2>
        <asp:Label ID="label22" Text="Organization Record" runat="server" /></h2>
         <em>* Required Fields</em>
    <table id="TABLE1" cellspacing="0" cellpadding="0" height="450px" width="100%" border="0"
        runat="server">
        <tr>
            <td height="450px" valign="Top">
                <br />
                <table id="TABLE2" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                    class="table" rules='none' width="80%" border='0' runat="server">
                    <tr>
                        <td bgcolor='white' valign="top" colspan="4">
                            <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class='sideHead' width="20%">
                            <asp:Label ID="Label2" Text="Organization Code" runat="server" />*
                        </td>
                        <td class='sideHead1' width="30%">
                            <telerik:RadTextBox ID="txtOrganizationCode" runat="server" Width="195px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID='RequiredFieldValidator5' ControlToValidate='txtOrganizationCode'
                                ErrorMessage="Organization Code is Required" Display='None' runat='server' />
                        </td>
                        <td class='sideHead' width="20%">
                            <asp:Label ID="Label5" Text="Organization Name" runat="server" />*
                        </td>
                        <td class='sideHead1' width="30%">
                            <telerik:RadTextBox ID="txtOrganizationName" runat="server" Width="195px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID='RequiredFieldValidator6' ControlToValidate='txtOrganizationName'
                                ErrorMessage="Organization Name is Required" Display='None' runat='server' />
                        </td>
                    </tr>
                    <tr>
                        <td class='sideHead' width="20%">
                            <asp:Label ID="Label4" Text="City" runat="server" />
                        </td>
                        <td class='sideHead1' width="30%">
                            <telerik:RadTextBox ID="txtCity" runat="server" Width="195px">
                            </telerik:RadTextBox>
                        </td>
                        <td class='sideHead' width="20%">
                            <asp:Label ID="Label10" Text="State" runat="server" />
                        </td>
                        <td class='sideHead1' width="30%">
                            <telerik:RadTextBox ID="txtState" runat="server" Width="195px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class='sideHead' width="20%">
                            <asp:Label ID="Label13" Text="Country" runat="server" />
                        </td>
                        <td class='sideHead1' width="30%">
                            <telerik:RadTextBox ID="txtCountry" runat="server" Width="195px">
                            </telerik:RadTextBox>
                        </td>
                        <td class='sideHead' width="20%">
                            <asp:Label ID="Label15" Text="Phone" runat="server" />
                        </td>
                        <td class='sideHead1' width="30%">
                            <telerik:RadTextBox ID="txtPhone" runat="server" Width="195px" onkeyPress="return OnlyNumbers(event);">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class='sideHead' width="20%">
                            <asp:Label ID="Label1" Text="Address1" runat="server" />
                        </td>
                        <td class='sideHead1' width="30%">
                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="195px" TextMode="MultiLine">
                            </telerik:RadTextBox>
                        </td>
                        <td class='sideHead' width="20%">
                            <asp:Label ID="Label3" Text="Address2" runat="server" />
                        </td>
                        <td class='sideHead1' width="30%">
                            <telerik:RadTextBox ID="txtAddress2" runat="server" Width="195px" TextMode="MultiLine">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class='sideHead' width="20%" valign="top">
                            <asp:Label ID="Label18" Text="Email" runat="server" />
                        </td>
                        <td class='sideHead1' width="30%" valign="top">
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="195px">
                            </telerik:RadTextBox>
                            <asp:RegularExpressionValidator ID="RegEmail" runat="server" ControlToValidate="txtEmail"
                                Display="None" ErrorMessage="Email address is invalid." ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"></asp:RegularExpressionValidator>
                        </td>
                        <td class='sideHead' valign="top" width="20%">
                            <asp:Label ID="lblFileName" Text="Organization Image" runat="server" />
                        </td>
                        <td class='sideHead1' width="30%">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <telerik:RadAsyncUpload ID="AsyncUpload1" runat="server" OnClientFilesUploaded="OnClientFilesUploaded"
                                            Localization-Clear="" MaxFileSize="524288" OnFileUploaded="AsyncUpload1_FileUploaded"
                                            OnClientValidationFailed="validationFailed" OnClientFileUploadRemoved="OnClientFileUploadRemoved"
                                            Skin="Default" AutoAddFileInputs="false">
                                            <FileFilters>
                                                <telerik:FileFilter Description="Pictures(jpeg;jpg;gif)" Extensions="jpeg,jpg,gif,JPG,JPEG,GIF,png,PNG" />
                                            </FileFilters>
                                        </telerik:RadAsyncUpload>
                                    </div>
                                    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                    <div id="empimgpos" runat="server">
                                        <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" Visible="false" AutoAdjustImageControlSize="false"
                                            Width="50px" Height="50px" ToolTip='' AlternateText='' /></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="#ffffff" colspan="4">
                            <telerik:RadButton ID='radSave' HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                Width="100px" Text="<%$ Resources:lang, btnSave %>" runat='server' />
                            &nbsp;&nbsp;
                            <%-- <telerik:RadButton ID='radSaveAdd' Width="125px" Text="<%$ Resources:lang, btnSaveAdd %>"
                                    runat='server' />
                                &nbsp;&nbsp;--%>
                            <telerik:RadButton ID="radClear" HoveredCssClass="normal-btnh" ForeColor="White"
                                Font-Size="17px" runat="server" Width="100px" Text="<%$ Resources:lang, btnClear %>"
                                OnClientClicked="OnClientClicked" CausesValidation="False" />
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="radCancel" HoveredCssClass="normal-btnh" ForeColor="White"
                                Font-Size="17px" runat="server" Width="100px" AutoPostBack="false" OnClientClicked="redirectme"
                                Text="<%$ Resources:lang, btnCancel %>" CausesValidation="False" />
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID='summary' runat='server' ShowSummary='False' ShowMessageBox='True'
                    HeaderText="<%$ Resources:lang, Errors %>"></asp:ValidationSummary>
            </td>
        </tr>
    </table>
</asp:Content>
