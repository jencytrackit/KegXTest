<%@ Page Language="VB" AutoEventWireup="false" Inherits="KEG_X.TrackITKTS.frmLogin" Codebehind="frmLogin.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="css/style1.css" />
  
</head>
<body>
        
    <form id="form1" runat="server" method="post" autocomplete="off">
   
 <!-- WRAPER STARTS HERE -->
<div class="wraper">

<!-- HEADER STARTS HERE -->
	<div class="header">
	 <div class="header-inner">
	   <h1><a href="#"><img src="images/logonew.png" width="142" height="24" border="0" alt="" /></a>
           <span style="font-size:28px;color:White;font-weight:bold; padding-left:150px; padding-bottom:30px;">Prod</span>
	   </h1>
	 
		<div  class="loginform-block">
		<form action="" >                 
        <input type="text" name="username" style="display:none !important;" value="" disabled="disabled"  />     
        <input type="password" name="password" style="display:none !important;" value="" disabled="disabled" />    
		<asp:TextBox  id="txtUserName" CssClass="email-box" runat ="server" autocomplete="off"></asp:TextBox>
        <asp:TextBox  id="txtPassword"  TextMode ="Password" CssClass="pass-box" runat ="server" autocomplete="off"></asp:TextBox>
        <asp:Button ID="btnLogin" class="submit-btn"  runat ="server"  Text ="LOG IN" />&nbsp;<asp:Label ID="lblMsg" runat="server" CssClass="email-box" Font-Size="Smaller" ForeColor="Red"></asp:Label>
	    <span class="forgot-pass-txt">Forget Password</span>        
		</form>
		</div>
	
	</div>
	
	</div>
<!-- HEADER ENDS HERE -->

<!-- BANNER CONTAINER STARTS HERE -->	
				
	  <div id="Promos">
		 <div id="PromoSlides">
				
            <div class="aslide">
                <img src="images/bnr1.jpg" alt="" />
                <div class="text">
				<h2>TRACKING ACUMEN</h2>
				<ul>
				<li>Track each and every kegs from anywhere</li>
				<li>Express implementation</li>
				</ul>
				<a href="#" class="signup-btn">Sign Up</a>
				</div>
            </div>   
            <div class="aslide">
                <img src="images/bnr2.jpg" alt="" />
                <div class="text">
				<h2>INTEGRATION BENEFITS</h2>
				<ul>
				<li>Integrates with any ERP system</li>
				<li>Simple, Robust and Seamless integration</li>
				<li>High standards of data security</li>
				</ul>
				<a href="#" class="signup-btn">Sign Up</a>
				</div>
            </div>   
            <div class="aslide">
                <img src="images/bnr3.jpg" alt="" />
                <div class="text">
				<h2>SySTEM BENEFITS</h2>
				<ul>
				<li>Keg tracking on cloud</li>
				<li>Innovative approach to track beer kegs</li>
				<li>Developed by leading keg distributors in middle east</li>
				<li>Industry specific solution</li>
				<li>Increases visibility and turnaround time</li>
				</ul>
				<a href="#" class="signup-btn">Sign Up</a>
				</div>
            </div>    
           </div>          
        </div>
<!-- BANNER CONTAINER ENDS HERE -->	
		
</div>
<!-- WRAPER ENDS HERE -->


<!--Banner JS -->
<script type="text/javascript" src="Js/jquery-1.11.0.js"></script>
<!--[if lt IE 7]><link rel="stylesheet" type="text/css" href="public/css/ie6.css" /><![endif]-->
  <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function() {
            $("#Promos").slideshow({ 'speed': 4000, 'timeout': 8000, 'fx': 'fade', 'pause': 0 });                       
      });

      
      
        // ]]>
</script> 
 <asp:RequiredFieldValidator ID="reqUserName" runat="server" Display="None" ControlToValidate="txtUserName" ErrorMessage="UserName is Required."></asp:RequiredFieldValidator>
 <asp:RequiredFieldValidator ID="reqPassword" runat="server" Display="None" ControlToValidate="txtPassWord" ErrorMessage="Password is Required."></asp:RequiredFieldValidator>
 <asp:ValidationSummary ID="summary" runat="server" ShowSummary="False" ShowMessageBox="True" HeaderText="Please enter the following information" DisplayMode="BulletList">
 </asp:ValidationSummary>
    </form>
</body>
</html>
