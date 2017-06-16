<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register_user.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet">
    <script src="https://use.fontawesome.com/0f358b3c56.js"></script>
    <link href="../../../css/styles.css" rel="stylesheet">
    <link href="css/styles.css" rel="stylesheet">

</head>
<body>
    <form id="form1" runat="server">


        <div class="navbar">
  <a href="../../../index.html">
    <div class="logo-box">
      <img src="../../../img/logo.png">
    </div></a>
  <div class="link-box">
    <div class="links">
        <a class="nav-link" href="#">How it works</a>
        <a class="nav-link" href="#">Become a partner</a>
        <a class="nav-link" href="#">About</a>
        <a class="nav-link" href="#">Log in</a>
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

  <div class="registration-box">
    <div class="reg-type">
      <div class="bigger-box">Sign-Up</div>
      <div class="smaller-box">Login</div>
    </div>
    <div class="reg-fields-s">
      <div class="just-holder">
        <div class="option name">
        <div style="margin-right:15px;">
         <h4 class="">Name:</h4><br />
        <asp:TextBox ID="name_textbox" runat="server"></asp:TextBox>
        </div>
        <div>
          <h4 class="">Surname:</h4><br />
        <asp:TextBox ID="surname_textbox" runat="server"></asp:TextBox>
        </div>
      </div>
        <div class="option email">
         <h4 class="">Email:</h4><h2 id="email_comment" runat="server"></h2><br />
        <asp:TextBox ID="email_textbox" runat="server"></asp:TextBox>






            <h4 class="">Phone number:</h4><br />
        <asp:TextBox ID="phone_textbox" runat="server"></asp:TextBox>
       
      </div>
        <div class="option password">
         <h4 class="">Password:</h4><br />
        <asp:TextBox ID="password_textbox" runat="server"></asp:TextBox>
         <h4 class="">Repeat password:</h4><br />
        <asp:TextBox ID="repeat_password_textbox" runat="server"></asp:TextBox><br />
      </div>
      </div>

       <asp:Button ID="signup_button" Text="Sign up" runat="server" OnClick="signup_button_Click" />
    </div>

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
      </div>
       <asp:Button ID="login_button" Text="Sign in" runat="server" OnClick="login_button_Click" />
        

<%--        This has to be deleted later--%>
       <asp:Button ID="login_logout_button" runat="server" Text="Log out" OnClick="logout_button_Click" />
    </div>

  </div>

</div>




<footer>
  <div class="footer-container-three">Copyright © 2017 Vinbo A/S. Alle rettigheder forbeholdes.</div>
</footer>
<script src="../../js/jquery.js"></script>
<!-- <script src="js/js.js"></script> -->


























<%--        ------------------------------------------------------------------------------------------------------------------------%>
    <div>

        <h5 id="text_label" runat="server"></h5><br />

        
        
        

       
        


        

     
       



       



       


       
        <a href="login_both.aspx">Go to log in</a> <br />

        <h5 id="text_label2" runat="server"></h5>

    
    </div>

        
<%--        ------------------------------------------------------------------------------------------------------------------------%>


    </form>
</body>
</html>
