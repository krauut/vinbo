using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

// operators cheat sheet

// || - or 
// && - and

public partial class _Default : System.Web.UI.Page
{

    SqlConnection connectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

        //checking, if user has been logged in. if no, there will be no sign out button
        if (Session["user_session_id"] == null)
        {
            login_logout_button.Visible = false;
            //Application.DoEvents();
        }

        //this will hide the button. but what if i have a cookie? 
        login_logout_button.Visible = false;
    }

    protected void signup_button_Click(object sender, EventArgs e)
    {
        //-------------- Variables
        bool password_match = false; // normaly it's false, but if there is any match, then it's true
        bool input_fields_validation = true; //true means it passes the filter, else it's false and has to stop the action after clicking the button
        bool existing_email = false; //this is default value, but if there is any email match, then this becomes true and it's used to stop code from fire
        bool next_step_existing_email = false; // this will change to true if no email was found in email check to continue to next table check.
        string email = email_textbox.Text;//keeps email text input value in the variable for checking if it's equal to any other emails in the SQL

        //-------------- Validations

        //pasword boxes. checking if they are equal
        if (repeat_password_textbox.Text == password_textbox.Text){
                password_match = true;
            };
    
            // also checking are those input values are not empty
            if (email_textbox.Text == "" || email_textbox.Text == " "){
                input_fields_validation = false;
            };

            // email validation - looking for same email in the database of users. Later on, in the services
            SqlCommand user_email_validation_check = new SqlCommand();
            user_email_validation_check.CommandText = "SELECT user_email " + "FROM lastexam_usertable " + "WHERE user_email ='" + email_textbox.Text + "'";
            user_email_validation_check.CommandType = CommandType.Text;
            user_email_validation_check.Connection = connectionString;
            connectionString.Open();

            SqlDataReader user_email_reader = user_email_validation_check.ExecuteReader();
                while (user_email_reader.Read()) {
                    string user_email_result = user_email_reader["user_email"].ToString();

                       if (string.Equals(user_email_result, email)) {
                          // if value from db is equal to email, it's true and we catch the mistake.
                          existing_email = true; //break
                        } else {
                          //advancing to the next step in order to check the service table
                          next_step_existing_email = true;
                        }
                }
            user_email_reader.Close();
            connectionString.Close();

        // if there were no results in the first table, lets move on to the next one
        if (next_step_existing_email == true) {

            SqlCommand service_email_validation_check = new SqlCommand();
            service_email_validation_check.CommandText = "SELECT service_email" + "FROM lastexam_serviceTable " + "WHERE service_email ='" + email_textbox.Text + "'";
            service_email_validation_check.CommandType = CommandType.Text;
            service_email_validation_check.Connection = connectionString;
            connectionString.Open();

            SqlDataReader service_email_reader = service_email_validation_check.ExecuteReader();
                while (service_email_reader.Read())
                {
                  string service_email_result = service_email_reader["service_email"].ToString();
                      if (string.Equals(service_email_result, email))
                      {
                          //if condition is met, we set the filter to true and we are not allowing the same email.
                          existing_email = true;
                      } 
                }
            service_email_reader.Close();
            connectionString.Close();
        }

        //-------------- result after validations

        //if (password_match == true && input_fields_validation == true && existing_email == false
        if (password_match && input_fields_validation && !existing_email)
        {
                connectionString.Open();

            int level = 0;
            DateTime signup_date = DateTime.Now;
            string formated_signup_date = signup_date.ToString("yyyy-MM-dd HH:mm");
            string user_signup_query = "INSERT INTO lastexam_usertable (user_email,user_pass,user_date_created,user_level,user_picture,user_name,user_surname,user_phone) VALUES (@user_email, @user_pass, @user_date_created, @user_level, @user_picture, @user_name, @user_surname,@user_phone)";

            SqlCommand user_registration_command = new SqlCommand(user_signup_query, connectionString);

            user_registration_command.Parameters.AddWithValue("@user_email", email_textbox.Text);
            user_registration_command.Parameters.AddWithValue("@user_pass", repeat_password_textbox.Text);
            user_registration_command.Parameters.AddWithValue("@user_date_created", formated_signup_date.ToString());
            user_registration_command.Parameters.AddWithValue("@user_level", level);
            user_registration_command.Parameters.AddWithValue("@user_picture", "empty");
            user_registration_command.Parameters.AddWithValue("@user_name", name_textbox.Text);
            user_registration_command.Parameters.AddWithValue("@user_surname", surname_textbox.Text);
            user_registration_command.Parameters.AddWithValue("@user_phone", phone_textbox.Text);

            user_registration_command.ExecuteNonQuery();
            connectionString.Close();


            //getting fk id for addresstable
            int fk_user_id = -1;
            SqlCommand getting_user_id_for_adresstable = new SqlCommand();
            getting_user_id_for_adresstable.CommandText = "SELECT user_id " + "FROM lastexam_usertable " + "WHERE user_email ='" + email_textbox.Text + "'";
            getting_user_id_for_adresstable.CommandType = CommandType.Text;
            getting_user_id_for_adresstable.Connection = connectionString;
            connectionString.Open();

            SqlDataReader user_id_reader = getting_user_id_for_adresstable.ExecuteReader();
            while (user_id_reader.Read())
            {
                fk_user_id = (Int32)user_id_reader["user_id"];
            }
            user_id_reader.Close();
            connectionString.Close();


            //adding default values into lastexam_useraddresstable

            // the reason of this activity is to send some default values to the database, which will be later read by edit profile actions.
            // If these values would be null, we would not get any response with neccesary data it is pretty imporatant, because we are joining tables
            // and if any of those valeus have null, the whole query execute activity just fails.

            connectionString.Open();
            string useraddress_signup_query = "INSERT INTO lastexam_useraddresstable (usertable_user_id,user_address,user_city,user_post_code) VALUES (@usertable_user_id, @user_address, @user_city, @user_post_code)";
            SqlCommand useradresss_registration_command = new SqlCommand(useraddress_signup_query, connectionString);

            useradresss_registration_command.Parameters.AddWithValue("@usertable_user_id", fk_user_id);
            useradresss_registration_command.Parameters.AddWithValue("@user_address", "");
            useradresss_registration_command.Parameters.AddWithValue("@user_city", 0);
            useradresss_registration_command.Parameters.AddWithValue("@user_post_code", 0);
            
            useradresss_registration_command.ExecuteNonQuery();
            connectionString.Close();

            // finally, sending message:

            text_label.InnerText = "Member Created!"; /* + " " + email_textbox.Text + " " + repeat_password_textbox.Text + " " + name_textbox.Text + " " + surname_textbox.Text + " " + signup_date.ToString();*/
             //text_label2.InnerText = user_registration_command.ToString();
        }
        else {  

                if (password_match == false){

                  text_label.InnerText = "Password did not match, try again!";

                } else if (input_fields_validation == false) {

                  text_label.InnerText = "You forgot to add email!";

                } else if (existing_email == true) {

                  text_label.InnerText = "Email is already in use!";

                }

            }
    }

    protected void login_button_Click(object sender, EventArgs e)
    {


        int user_id = -1;
        //this default var value is for safety in case someone enters wrong password and still manage to connect. later on it's used as a value to  
        bool password_validation_status = false;
        bool service_type = false;
        //bool if_user = true;

        SqlCommand email_id_check = new SqlCommand(); // AND ++ remember me
        email_id_check.CommandText = "SELECT user_id " + "FROM lastexam_userTable " + "WHERE user_email ='" + login_email_textbox.Text + "'";
        email_id_check.CommandType = CommandType.Text;
        email_id_check.Connection = connectionString;
        connectionString.Open();

        SqlDataReader id_reader = email_id_check.ExecuteReader();
        while (id_reader.Read())
        {
            user_id = (Int32)id_reader["user_id"];
            Console.Write("hello" + user_id.ToString());
        }
        id_reader.Close();

        //if no user found, we are heading for services database to find service providers. 

        if (user_id < 0)
        {
            SqlCommand service_email_id_check = new SqlCommand();
            service_email_id_check.CommandText = "SELECT service_id " + "FROM lastexam_serviceTable " + "WHERE service_email ='" + login_email_textbox.Text + "'";
            service_email_id_check.CommandType = CommandType.Text;
            service_email_id_check.Connection = connectionString;

            SqlDataReader service_id_reader = service_email_id_check.ExecuteReader();
            while (service_id_reader.Read())
            {
                user_id = (Int32)service_id_reader["service_id"];
                if (user_id > 0)
                {
                    service_type = true;
                }
            }
            service_id_reader.Close();
        }

        if (user_id > 0)
        {
            SqlCommand password_reader = new SqlCommand();
            password_reader.CommandText = "SELECT user_pass " + "FROM lastexam_userTable " + "WHERE user_id ='" + user_id.ToString() + "'";
            password_reader.CommandType = CommandType.Text;
            password_reader.Connection = connectionString;

            SqlDataReader password_validator = password_reader.ExecuteReader();
            while (password_validator.Read())
            {
                if (password_validator["user_pass"].ToString() == login_password_textbox.Text)
                {
                    password_validation_status = true;
                }
            }
            password_validator.Close();

            if (password_validation_status == false)
            {
                // ------

                SqlCommand service_password_reader = new SqlCommand();
                service_password_reader.CommandText = "SELECT service_pass " + "FROM lastexam_servicetable " + "WHERE service_id ='" + user_id.ToString() + "'";
                service_password_reader.CommandType = CommandType.Text;
                service_password_reader.Connection = connectionString;

                SqlDataReader service_password_validator = service_password_reader.ExecuteReader();
                while (service_password_validator.Read())
                {
                    if (service_password_validator["service_pass"].ToString() == login_password_textbox.Text)
                    {
                        password_validation_status = true;
                        service_type = true;
                    }
                }
                service_password_validator.Close();

                // ------
            }
        }
        connectionString.Close();

        if (password_validation_status == true)
        {

            //Session["user_session"] = user_id;
            if (service_type)
            {
                //cookie should only be saved after user marks that he wants to remember his login credentials
                //HttpCookie user_cookie = new HttpCookie("login_cookie");
                //user_cookie["id"] = user_id.ToString();
                //user_cookie["service_type"] = "true";
                //user_cookie.Expires = DateTime.Now.AddDays(1d);
                //Response.Cookies.Add(user_cookie);

                // it is a better practise to have a session instead cookies while logging in

                login_logout_button.Visible = true;

                Session["user_session_id"] = user_id.ToString();
                Session["user_session_email"] = login_email_textbox.Text;
                Session["service"] = true;

                Response.Redirect("edit_service.aspx");
                //text_label.InnerText = "Succesful (Service)" + "user session id: " + Session["user_session_id"].ToString() + " " + Session["service"].ToString();

            }
            else
            {
                //HttpCookie user_cookie = new HttpCookie("login_cookie");
                //user_cookie["id"] = user_id.ToString();
                //user_cookie.Expires = DateTime.Now.AddDays(1d);
                //Response.Cookies.Add(user_cookie);

                Session["user_session_id"] = user_id.ToString();
                Session["user_session_email"] = login_email_textbox.Text;
                Session["user"] = true;


                //text_label.InnerText = "Succesful (User)" + "user session id: " + Session["user_session_id"].ToString();
                Response.Redirect("edit_user.aspx");
            }
        }
        else
        {
            login_text_label.InnerText = "User not found!";
        }
    }

    protected void logout_button_Click(object sender, EventArgs e)
    {
        //if (Request.Cookies["login_cookie"] != null)
        //{
        //    HttpCookie user_cookie = new HttpCookie("login_cookie");
        //    user_cookie.Expires = DateTime.Now.AddDays(-1d);
        //    Response.Cookies.Add(user_cookie);
        //    Response.Redirect("login_both.aspx");
        //}


        Session.Abandon();
        //Response.Redirect("login_both.aspx");

    }


}
