<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet">
    <script src="https://use.fontawesome.com/0f358b3c56.js"></script>
    <link href="../../css/styles.css" rel="stylesheet">
    <link href="css/styles.css" rel="stylesheet">

</head>
<body>
    <form id="form1" runat="server">


        <div class="navbar">
  <a href="../../index.html">
    <div class="logo-box">
      <img src="../../img/logo.png">
    </div></a>
  <div class="link-box">
    <div class="links">
                <a class="nav-link" href="../about/how-it-works.html">How it works</a>
                <a class="nav-link" href="../about/about.html">About</a>
                <a class="nav-link" href="../sign-up/sign-up.html">Sign up</a>
                <a class="nav-link" href="../login/login.aspx">Login</a>
    </div>
  </div>
  <div class="burger-box">
    <div class="burger-button">
      <span></span>
      <span></span>
      <span></span>
    </div>
  </div>
</div>
<div class="burger-links">
    <a class="burger-nav-link" href="#">How it works</a>
    <a class="burger-nav-link" href="#">Become a partner</a>
    <a class="burger-nav-link" href="#">About</a>
    <a class="burger-nav-link" href="#">Sign Up</a>
</div>







<div class="registration-container">

     <div class="reg-fields-l">
      <div class="just-holder">
        <div class="option email">
          <h5 id="login_text_label" runat="server"></h5>
             <h4 class="">Email:</h4>
             <asp:TextBox ID="login_email_textbox" runat="server" ></asp:TextBox>
        </div>
        <div class="option password">
          <h4 class="">Password:</h4>
          <asp:TextBox ID="login_password_textbox" runat="server" TextMode="Password"></asp:TextBox>
        </div>
                <asp:Button ID="login_button" Text="Sign in" runat="server" OnClick="login_button_Click" />
      </div>
 
  
    </div>

<%--        This has to be deleted later--%>
       <asp:Button ID="login_logout_button" runat="server" Text="Log out" OnClick="logout_button_Click" />
    </div>

  </div>

</div>































<%--        ------------------------------------------------------------------------------------------------------------------------%>
    <div>

        <h5 id="text_label" runat="server"></h5><br />

       
        <a href="login_both.aspx">Go to log in</a> <br />

        <h5 id="text_label2" runat="server"></h5>

    
    </div>

        
<%--        ------------------------------------------------------------------------------------------------------------------------%>


    </form>


    <footer>
  <div class="footer-container-three">Copyright © 2017 Vinbo A/S. Alle rettigheder forbeholdes.</div>
</footer>
<script src="../../js/jquery.js"></script>
<!-- <script src="js/js.js"></script> -->
</body>
</html>
