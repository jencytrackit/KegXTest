<%@ Page Language="VB" AutoEventWireup="false" Inherits="KEG_X.TrackITKTS.ADLogin" CodeBehind="ADLogin.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KEG-X Login</title>
    <link rel="stylesheet" type="text/css" href="css/style1.css" />
</head>
<body style="background-color:white; ">

    <form id="form1" runat="server" method="post" autocomplete="off">

        <!-- WRAPER STARTS HERE -->
        <div class="wraper" >

            <!-- HEADER STARTS HERE -->
            <div class="header">
                <div class="header-inner">
                    <h1><a href="#">
                        <img src="images/logonew.png" width="142" height="24" border="0" alt="" /></a>
                        <span style="font-size: 28px; color: White; font-weight: bold; padding-left: 150px; padding-bottom: 30px;">Prod</span>
                    </h1>

                    <div class="loginform-block">
                        
                        
                    </div>

                </div>

            </div>
            <!-- HEADER ENDS HERE -->

            <!-- BANNER CONTAINER STARTS HERE -->

            <div style="text-align:center; color:red; height:450px; margin-top:20px; font-weight:bold;">
               <asp:Label ID="lblMsg" runat="server" CssClass="email-box" Font-Size="Smaller" ></asp:Label>
                <a href="ADLogin.aspx" runat="server" id="hrefLogin">Click here to login with different user.</a>
            </div>
            <!-- BANNER CONTAINER ENDS HERE -->

            <div class="header">
                </div>
        </div>
        <!-- WRAPER ENDS HERE -->
               
    </form>
</body>
</html>
