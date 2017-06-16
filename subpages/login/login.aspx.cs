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

                Response.Redirect("../edit-profile/edit_service.aspx");
                text_label.InnerText = "Succesful (Service)" + "user session id: " + Session["user_session_id"].ToString() + " " + Session["service"].ToString();

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
                Response.Redirect("../edit-profile/edit_user.aspx");
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
