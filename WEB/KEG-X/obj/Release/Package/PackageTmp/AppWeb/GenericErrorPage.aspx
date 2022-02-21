<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GenericErrorPage.aspx.vb" Inherits="KEG_X.GenericErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" style="width: 100%;" align="center">
        <tr>
            <td style="padding-top: 30px;">
                <table cellpadding="0" cellspacing="0" style="width: 80%;" id="TblRowStyle" align="center">
                    <tr >
                        <td>
                            Error
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 150px; padding: 20px; vertical-align: top;">
                            <div style="font-size: 12px">
                                We are sorry, but an unhandled error occurred on the server. 
                                <br />
                                <br />
                                If the same problem occurs again, contact Administrator.
                                <br />
                                <br />                                
                                <a href="frmHome.aspx" style="color:Red; font-weight:bold;" >
                                back to Home page.</a>
                                <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>
                                </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
