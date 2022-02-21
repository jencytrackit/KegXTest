<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmAddEditEmployees.aspx.vb"
    MasterPageFile="~/Templates/MasterPage.Master" Inherits="KEG_X.TrackITKTS.frmAddEditEmployees" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>
<%@ Register Src="~/controls/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="msg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
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
    <ATS:Session ID="chkSession" runat="Server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <asp:HiddenField ID="employExt" Value="Please select image, types accepted are .gif,.jpg,.jpeg,.png. File size limit 500 KB."
        runat="server" />
    <asp:HiddenField ID="hdnfileSize" Value="File size limit 500 KB." runat="server" />
    <asp:Label ID="lblImageValue" runat="server" Visible="false" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <script language="javascript" type="text/javascript">
        function redirectme(sender, args) {
            location.href = 'frmEmployeeList.aspx';
        }
        function OnClientClicked(button, args) {
            document.forms[0].reset();
            button.set_autoPostBack(false);
        }
    </script>
    <h2>
        <asp:Label ID="addATSEmployees" Text="EMPLOYEES RECORD" runat="server" /></h2>
         <em>* Required Fields</em>
    <table id="TABLE100" cellspacing="0" cellpadding="0" height="100%" width="100%" border="0"
        runat="server">
        <tr>
            <td valign="Top">
                <table id="TABLE1" align='left' bordercolor='#ccddef' height="1%" cellspacing='0'
                    class="table" rules='none' width="80%" border='0' runat="server">
                    <tr>
                        <td bgcolor='white' colspan="4">
                            <asp:Label ID='lblMsg' runat='server' CssClass='noteMsg'></asp:Label>
                        </td>
                    </tr>
                    <tr id="trOrganization">
                        <td class='sideHead' width="25%">
                            <asp:Label ID="Label2" Text="Organization" runat="server" />*
                        </td>
                        <td class='sideHead1' width="30%" colspan="2">
                            <telerik:RadComboBox ID="rcbOrganization" runat="server" Width="195px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class='sideHead' width="25%">
                            <asp:Label ID="lblEmployeeName" Text="Employee Name" runat="server" />*
                        </td>
                        <td class='sideHead1' width="25%">
                            <telerik:RadTextBox ID="radEmployeeName" Width="195px" MaxLength='100' runat="server">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID='rfvEmployeeName' ControlToValidate='radEmployeeName'
                                ErrorMessage="EmployeeName is Required." Display='None' runat='server' />
                        </td>
                        <td class='sideHead' width="25%">
                            <asp:Label ID="lblEmployeeNo" Text="Employee No" runat="server" />*
                        </td>
                        <td class='sideHead1' width="25%">
                            <telerik:RadTextBox ID="radEmployeeNo" Width="195px" MaxLength='100' runat="server" />
                            <asp:RequiredFieldValidator ID='rfvEmployeeNo' ControlToValidate='radEmployeeNo'
                                ErrorMessage="Employee No is Required." Display='None' runat='server' />
                        </td>
                    </tr>
                    <tr>
                        <td class='sideHead'>
                            <asp:Label ID="lblPosition" Text="Designation" runat="server" />
                        </td>
                        <td class='sideHead1'>
                            <telerik:RadTextBox ID="radPosition" Width="195px" MaxLength='100' runat="server" />
                        </td>
                        <td class='sideHead'>
                            <asp:Label ID="lblEmail" Text="Email" runat="server" />*
                        </td>
                        <td class='sideHead1'>
                            <telerik:RadTextBox ID="radEmail" runat="server" MaxLength="50" Width="195px" />
                            <asp:RequiredFieldValidator ID='rfvEmail' ControlToValidate='radEmail' ErrorMessage="Email is Required."
                                Display='None' runat='server' />
                            <asp:RegularExpressionValidator ID="valEmailAddress" runat="server" ControlToValidate="radEmail"
                                Display="None" EnableClientScript="true" ErrorMessage="Email address is invalid."
                                ValidationExpression=".*@.*\..*" />
                        </td>
                    </tr>
                    <tr>
                        <td class='sideHead'>
                            <asp:Label ID="lblMobile" Text="Mobile" runat="server" />
                        </td>
                        <td class='sideHead1'>
                            <telerik:RadTextBox ID="radMobile" Width="195px" MaxLength='100' runat="server" onkeyPress="return OnlyNumbers(event);" />
                        </td>
                        <td class='sideHead'>
                            <asp:Label ID="lblPhone" Text="Phone" runat="server" />
                        </td>
                        <td class='sideHead1'>
                            <telerik:RadTextBox ID="radPhone" Width="195px" MaxLength='100' runat="server" onkeyPress="return OnlyNumbers(event);" />
                        </td>
                    </tr>
                    <tr id="trCompanyAssociation">
                        <td class='sideHead'>
                            <asp:Label ID="lblCompaniesassigned" Text="Companies Association" runat="server" />*
                        </td>
                        <td class='sideHead1'>
                            <telerik:RadListBox ID="radlstCompanyAssociation" runat="server" CheckBoxes="True"
                                Width="250px">
                            </telerik:RadListBox>
                        </td>
                        <td class='sideHead'>
                            <asp:Label ID="lblRole" Text="Role" runat="server" />*
                        </td>
                        <td class='sideHead1'>
                            <telerik:RadListBox ID="rlbRoles" runat="server" CheckBoxes="True" Width="250px">
                            </telerik:RadListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class='sideHead'>
                            <asp:Label ID="lblComments" Text="Comments" runat="server" />
                        </td>
                        <td class='sideHead1'>
                            <telerik:RadTextBox ID="RadComments" Width="195px" MaxLength='100' runat="server" />
                        </td>
                        <td class='sideHead' valign="top">
                            <asp:Label ID="lblFileName" Text="Employee Image" runat="server" />
                        </td>
                        <td class='sideHead1'>
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
                                    &nbsp &nbsp &nbsp
                                    <div id="empimgpos" runat="server">
                                        <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" Visible="false" AutoAdjustImageControlSize="false"
                                            Width="50px" Height="50px" ToolTip='' AlternateText='' /></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-bottom: 1px solid #ccddef">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td class='sideHead'>
                            <asp:Label ID="lblUserName" Text="User Name" runat="server" />*
                        </td>
                        <td class='sideHead1'>
                            <telerik:RadTextBox ID='radUserName' Width="195px" MaxLength='20' runat='server' />
                            <telerik:RadTextBox ID='radOldUserName' MaxLength='20' runat='server' Visible="false" />
                            <asp:RequiredFieldValidator ID='rfvUserName' ControlToValidate='radUserName' ErrorMessage="UserName is Required."
                                Display='None' runat='server' />
                        </td>
                        <td class='sideHead'>
                            <asp:Label ID="lblActive" Text="Active" runat="server" />*
                        </td>
                        <td class='sideHead1'>
                            <asp:RadioButtonList ID="rbtActive" runat="server" Width="100px" RepeatDirection="Horizontal"
                                CssClass="nopadding">
                                <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class='sideHead'>
                            <asp:Label ID="lblPassword" Text="Password" runat="server" />*
                        </td>
                        <td class='sideHead1'>
                            <telerik:RadTextBox ID='radPassword' Width="195px" TextMode='Password' MaxLength='20'
                                runat='server' />
                            <asp:RequiredFieldValidator ID='rfvPassword' ControlToValidate='radPassword' ErrorMessage="Password is Required."
                                Display='None' runat='server' />
                        </td>
                        <td class='sideHead'>
                            <asp:Label ID="Label1" Text="Confirm Password" runat="server" />*
                        </td>
                        <td class='sideHead1'>
                            <telerik:RadTextBox ID='radCPassword' Width="195px" TextMode='Password' MaxLength='20'
                                runat='server' />
                            <asp:RequiredFieldValidator ID='rfvConfirmPassword' ControlToValidate='radCPassword'
                                ErrorMessage="Confirm Password is Required." Display='None' runat='server' />
                            <asp:CompareValidator ID='valComparePassword' ControlToValidate='radCPassword' ErrorMessage="Password fields must match."
                                ControlToCompare='radPassword' Display='None' EnableClientScript='true' runat='server' />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="#ffffff" colspan="4">
                            <telerik:RadButton ID="radSave" HoveredCssClass="normal-btnh" ForeColor="White" Font-Size="17px"
                                Width="100px" Text="Save" runat="server" />
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="radClear" HoveredCssClass="normal-btnh" ForeColor="White"
                                Font-Size="17px" Width="100px" Text="Reset" runat="server" AutoPostBack="false"
                                OnClientClicked="OnClientClicked" CausesValidation="False" />
                            &nbsp;&nbsp;
                            <telerik:RadButton ID="radCancel" HoveredCssClass="normal-btnh" ForeColor="White"
                                Font-Size="17px" runat="server" Width="100px" AutoPostBack="false" OnClientClicked="redirectme"
                                Text="Cancel" CausesValidation="False" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID='ValidationSummary1' runat='server' ShowSummary='False'
                    ShowMessageBox='True' HeaderText='Errors...'></asp:ValidationSummary>
            </td>
        </tr>
    </table>
</asp:Content>
