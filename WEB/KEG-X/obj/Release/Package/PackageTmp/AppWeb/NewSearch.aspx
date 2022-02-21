<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewSearch.aspx.vb" Inherits="KEG_X.TrackITKTS.NewSearch" MasterPageFile="~/Templates/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="ATS" TagName="Session" Src="../controls/chkuserpresent.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
<asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>

    <style type="text/css" >
        .AutoCompleteFlyout
        {

        margin : 0px!important;
        background-color : inherit;
        color : windowtext;
        border : buttonshadow;
        border-width : 1px;
        border-style : solid;
        cursor : 'cursor';
        overflow : auto;
        height : 200px;
        text-align : left;
        list-style-type : none;

        }

        .AutoCompleteFlyoutItem
        {
        background-color :#fff;
        color : windowtext;
        padding : 1px;

        }

        .AutoCompleteFlyoutHilightedItem
        {

        background-color: #dedede;
        color: black;
        padding: 1px;

        }

</style>

<asp:TextBox ID="txtGoingTo" runat="server" CssClass="textbginput" onBlur = "javascript:return validatetxtGoingTo();" ></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtGoingTo_AutoCompleteExtender" runat="server" 
                            DelimiterCharacters="" Enabled="True" TargetControlID="txtGoingTo"
                            ServicePath="~/AppWeb/LookupWebService.asmx"
                            ServiceMethod="GetCompletionList"
                            CompletionInterval="100"
                            MinimumPrefixLength="1"
                            EnableCaching="true"
                            CompletionSetCount="4"
                            CompletionListItemCssClass="AutoCompleteFlyoutItem"
                            CompletionListCssClass="AutoCompleteFlyout"
                            CompletionListHighlightedItemCssClass="AutoCompleteFlyoutHilightedItem"
                            >
                        </asp:AutoCompleteExtender>
</asp:Content>