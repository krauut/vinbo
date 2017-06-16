<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register_service.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <link href="https://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet"/>
  <script src="https://use.fontawesome.com/0f358b3c56.js"></script>
  <link href="../../../css/styles.css" rel="stylesheet"/>
  <link href="css/styles.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">



        <div class="navbar">
  <a href="../../../index.html">
    <div class="logo-box">
      <img src="../../../img/logo.png"/>
    </div></a>
  <div class="link-box">
    <div class="links">
        <a class="nav-link" href="../../about/how-it-works.html">How it works</a>
        <a class="nav-link" href="../../about/about.html"">About</a>
        <a class="nav-link" href="../../login/login.aspx">Log in</a>
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

            
          <h5 id="text_label" runat="server"></h5>

          <h4>First Name</h4>
          <asp:TextBox ID="name_textbox" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator" controltovalidate="name_textbox" errormessage="Please enter your name!" />

          <h4>Last Name</h4>
          <asp:TextBox ID="surname_textbox" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="surname_textbox" errormessage="Please enter your last name!" />

          <h4>CVR</h4>
          <asp:TextBox ID="cvr_textbox" runat="server" onfocusout="CheckCVR()"></asp:TextBox><p id="cvr_info"></p>
          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="cvr_textbox" errormessage="Please enter CVR!" />

          <h4>Company name</h4>
          <asp:TextBox ID="company_name_textbox" runat="server"></asp:TextBox>
           <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="cvr_textbox" errormessage="Please enter company name!" />

          <h4>Address</h4>
          <asp:TextBox ID="address_textbox" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="cvr_textbox" errormessage="Please enter company Address!" />

          <h4>City/Region</h4>
          <asp:DropDownList ID="city_ddl" runat="server">

            <asp:ListItem Text="Copenhagen" Value="1"></asp:ListItem>
            <asp:ListItem Text="Aalborg" Value="2"></asp:ListItem>
            <asp:ListItem Text="Aarhus" Value="3"></asp:ListItem>
            <asp:ListItem Text="Odense" Value="4"></asp:ListItem>

          </asp:DropDownList>

          <h4>Price for cleaning 1 outter window</h4>
           <asp:TextBox ID="outter_textbox" runat="server"></asp:TextBox>

          <h4>Price for cleaning 1 inner window</h4>
          <asp:TextBox ID="inner_textbox" runat="server"></asp:TextBox>

          <h4>Highest floor you are able to clean</h4>
         <asp:DropDownList ID="highest_ddl" runat="server">
            <asp:ListItem Text="3rd" Value="3"></asp:ListItem>
            <asp:ListItem Text="4th" Value="4"></asp:ListItem>
            <asp:ListItem Text="5th" Value="5"></asp:ListItem>
            <asp:ListItem Text="6th" Value="6"></asp:ListItem>
            <asp:ListItem Text="7th" Value="7"></asp:ListItem>
            <asp:ListItem Text="8th" Value="8"></asp:ListItem>
            <asp:ListItem Text="9th" Value="9"></asp:ListItem>
        </asp:DropDownList>


          <h4>Approx. time to react to orders</h4>
         <asp:DropDownList ID="react_ddl" runat="server">
            <asp:ListItem Text="1 Day" Value="1"></asp:ListItem>
            <asp:ListItem Text="2 Days" Value="2"></asp:ListItem>
            <asp:ListItem Text="3 Days" Value="3"></asp:ListItem>
         </asp:DropDownList>


          <h4>Does your price change in higher floors?</h4>

         <asp:DropDownList ID="price_higher_floors_radio" runat="server">
            <asp:ListItem  Text="Yes" Value="1"></asp:ListItem>
            <asp:ListItem  Text="No" Value="0"></asp:ListItem>
         </asp:DropDownList>


          <h4>From which floor you start charging extra</h4>
          <asp:DropDownList ID="from_floor_ddl" runat="server">
                    <asp:ListItem Text="3rd floor" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4th floor" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5th floor" Value="5"></asp:ListItem>
          </asp:DropDownList>



          <h4>Specify your way of charging</h4>
          <asp:RadioButtonList ID="way_of_charge_radio" runat="server" OnSelectedIndexChanged="way_of_charge_radio_SelectedIndexChanged">
                    <asp:ListItem  Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem  Text="No" Value="0"></asp:ListItem>
           </asp:RadioButtonList>

          <h4>How much extra per floors above?</h4>
         <asp:TextBox ID="extra_fixed_textbox" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator10" controltovalidate="extra_fixed_textbox" errormessage="Please enter price range!" />
          

          <h4>how much extra per each floor above?</h4>
         <asp:TextBox ID="extra_abstract_textbox" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" controltovalidate="extra_abstract_textbox" errormessage="Please enter price range!" />

          <h4>Email</h4>
           <asp:TextBox ID="email_textbox" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator8" controltovalidate="email_textbox" errormessage="Please enter email!" />

          <h4>Phone nr.</h4>
          <asp:TextBox ID="phone_textbox" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" controltovalidate="phone_textbox" errormessage="Please enter phone number!" />

          <h4>Password</h4>
           <asp:TextBox ID="password_textbox" runat="server"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" controltovalidate="password_textbox" errormessage="Please enter password!" />

          <h4>Repeat pw</h4>
         <asp:TextBox ID="repeat_password_textbox" runat="server"></asp:TextBox><br />
          <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="repeat_password_textbox" errormessage="Please repeat password!" />

        <div class="terms-and-conds" style="width:100%;">
            <input type="checkbox" name="terms" value="yes" required>  I accept the <a href="#">Terms and Conditions</a>.
        </div>

         <asp:Button ID="signup_button" Text="Sign up" runat="server" onclick="signup_button_Click" />
    </div>


    <!-- <div class="reg-fields-l">
      <div class="just-holder">
        <div class="option email">
          <h4>Email</h4>
          <input type="text" name="email" min="0" max="100" required>
        </div>
        <div class="option password">
          <h4>Password</h4>
          <input type="password" name="psw">
        </div>
      </div>
      <input class="submit-login" type="submit" value="Submit">
    </div> -->
  </div>

</div>



<footer>
  <div class="footer-container-three">Copyright © 2017 Vinbo A/S. Alle rettigheder forbeholdes.</div>
</footer>
<script src="../../js/jquery.js"></script>
<!-- <script src="js/js.js"></script> -->


        <%--CVR API--%>
 <script>
            //disabled button until CVR will be found.
            document.getElementById("signup_button").disabled = true;

            
            function CheckCVR (){

                var cvrNumber = document.getElementById("cvr_textbox").value;
                var cvrRequest = new XMLHttpRequest();

                cvrRequest.open("GET", "http://cvrapi.dk/api?vat=" + cvrNumber + "&country=dk&version=6.json");
                cvrRequest.onload = function(){
                    var cvrData = JSON.parse(cvrRequest.responseText);


                    if (cvrData.t != "100") {
                        //if company CVR is not found, registration button will be displayed
                        document.getElementById("cvr_info").innerHTML = "CVR number not found, try again."
                        //if wrong number was typed again, setting button bact to disabled
                        document.getElementById("signup_button").disabled = true;
                        
                    } else {
                        document.getElementById("cvr_info").innerHTML = "CVR number found";

                        document.getElementById("signup_button").disabled = false;
                        console.log(cvrData.name);

                        let cvrName = cvrData.name;
                        let cvrAddress = cvrData.address;
                        let cvrPhone = cvrData.phone;
                        let cvrEmail = cvrData.email;

                        document.getElementById("company_name_textbox").value = cvrName;
                        document.getElementById("address_textbox").value = cvrAddress;
                        document.getElementById("phone_textbox").value = "+45" + cvrPhone;
                        document.getElementById("email_textbox").value = cvrEmail;
                        // I believe zip code would be easier to use, but if we let people to choose city, it will be much easier because there are many post numbers in the same area.
                    }
                }
                cvrRequest.send();
            }
          </script>

    </form>
</body>
</html>
