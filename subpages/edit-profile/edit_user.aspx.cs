using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;    // SQL Query classes - such as SqlConnection, SqlCommand
using System.Configuration; // ConfigurationManager class - to access information saved in the web.config file         
using System.Data;  // namespace for the CommandType enumerator  


// Challenge is how to get values from join tables



public partial class edit_user : System.Web.UI.Page
{

    SqlConnection connectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Session["user_session_id"] != null)
        //{

            //values for basic info
            string user_name = "";
            string user_email = "";
            string user_phone = "";

            string user_address = "";
            string user_city = "";
            string user_post_code = "";


            SqlCommand user_basic_info = new SqlCommand();
            user_basic_info.CommandText = "SELECT lastexam_usertable.user_name, lastexam_usertable.user_email, lastexam_usertable.user_phone, lastexam_useraddresstable.user_address, lastexam_useraddresstable.user_city, lastexam_useraddresstable.user_post_code  "
            + "FROM lastexam_usertable CROSS JOIN lastexam_useraddresstable " + "WHERE lastexam_usertable.user_id ='" + Session["user_session_id"] + "'";

            user_basic_info.CommandType = CommandType.Text;
            user_basic_info.Connection = connectionString;
            connectionString.Open();

            SqlDataReader user_basic_info_reader = user_basic_info.ExecuteReader();
            while (user_basic_info_reader.Read())
            {
                user_name = user_basic_info_reader["user_name"].ToString();
                user_email = user_basic_info_reader["user_email"].ToString();
                user_phone = user_basic_info_reader["user_phone"].ToString();
                user_address = user_basic_info_reader["user_address"].ToString();
                user_city = user_basic_info_reader["user_city"].ToString();
                user_post_code = user_basic_info_reader["user_post_code"].ToString();
            }

            user_basic_info_reader.Close();
            connectionString.Close();


            //if any of non mandatory values are equal to default values, these messages will appear by default
            if (user_city == "0")
            {
                city_label.Text = "Please specify your city";
            }
            else
            {
                city_label.Text = user_city;
            }


            // ---

            if (user_address == "")
            {
                address_label.Text = "Please specify your address";
            }
            else
            {
                address_label.Text = user_address;
            }

            // ---

            if (user_post_code == "0")
            {
                postcode_label.Text = "Please specify your postal code";
            }
            else
            {
                postcode_label.Text = user_post_code;
            }

            //mandaroty values
            name_label.Text = user_name;
            email_label.Text = user_email;
            phone_label.Text = user_phone;

        //} else
        //{
        //    Response.Redirect("login_both.aspx");
        //}
    }

    //updating password
    protected void update_pass_button_Click(object sender, EventArgs e)
    {
        bool password_match = false; // value for checking if the pass matches with the one in SLQ accordingly to the present session id
        bool old_password_match = false; // value which states if the new password is mathing in both text areas,the first and repeat

        //here we check if the old password is correct. if it is, we change old_password_match to true.
        SqlCommand password_validation_command = new SqlCommand();
        password_validation_command.CommandText = "SELECT user_pass " + "FROM lastexam_userTable " + "WHERE user_id ='" + Session["user_session_id"] + "'";
        password_validation_command.CommandType = CommandType.Text;
        password_validation_command.Connection = connectionString;
        connectionString.Open();

        SqlDataReader password_validation_reader = password_validation_command.ExecuteReader();
        while (password_validation_reader.Read())
        {
            var user_pass = password_validation_reader["user_pass"].ToString();
            if (user_pass == old_pass_textbox.Text)
            {
                old_password_match = true;
            }
        }
        password_validation_reader.Close();
        connectionString.Close();

        //here we check if the new password matches first and repeat box values
        if (new_pass_textbox.Text == repeat_pass_textbox.Text)
        {
            password_match = true;
        };

        //and if everything is fine, we execute this query and we are changing the password for the simple user
        if (password_match && old_password_match) {
            string commandText = "UPDATE lastexam_usertable " +
            "SET user_pass=@user_pass WHERE user_id=@user_id";

            SqlCommand command = new SqlCommand(commandText, connectionString);
            
            command.Parameters.AddWithValue("@user_pass", repeat_pass_textbox.Text);
            command.Parameters.AddWithValue("@user_id", Session["user_session_id"]);

            try
            {
                connectionString.Open();
                command.ExecuteNonQuery();
            }
            catch
            { }
            finally
            {

                connectionString.Close();
                //Response.Redirect(".aspx");
                info_label.Text = "Password changed!";
            }

        };

        if (!password_match)
        {
            
            info_label.Text = "Passwords do not match, try again!";
           
        }

        if (!old_password_match)
        {
            info_label.Text = "incorrect present password, try again!";
        }
    }

    //updating basic information fot the user.

    protected void update_contact_button_Click(object sender, EventArgs e)
    {
        //updating location information
        string basic_update_query_address = "UPDATE lastexam_useraddresstable " +
            "SET user_address=@user_address WHERE usertable_user_id=@usertable_user_id";

        string basic_update_query_city = "UPDATE lastexam_useraddresstable " +
            "SET user_city=@user_city WHERE usertable_user_id=@usertable_user_id";

        string basic_update_query_post_code = "UPDATE lastexam_useraddresstable " +
            "SET user_post_code=@user_post_code WHERE usertable_user_id=@usertable_user_id";

        string phone_number_update = "UPDATE lastexam_usertable " +
                    "SET user_phone=@user_phone WHERE user_id=@user_id";

        //user_city=@user_city, user_address=@user_address WHERE usertable_user_id=@usertable_user_id";

        SqlCommand basic_update_address = new SqlCommand(basic_update_query_address, connectionString);
        SqlCommand basic_update_city = new SqlCommand(basic_update_query_city, connectionString);
        SqlCommand basic_update_post_code = new SqlCommand(basic_update_query_post_code, connectionString);
        SqlCommand phone_number_command = new SqlCommand(phone_number_update, connectionString);


        basic_update_address.Parameters.AddWithValue("@user_address", address_textbox.Text);
        basic_update_address.Parameters.AddWithValue("@usertable_user_id", Session["user_session_id"]);

        basic_update_city.Parameters.AddWithValue("@user_city", city_ddl.SelectedValue);
        basic_update_city.Parameters.AddWithValue("@usertable_user_id", Session["user_session_id"]);

        basic_update_post_code.Parameters.AddWithValue("@user_post_code", postnumber_textbox.Text);
        basic_update_post_code.Parameters.AddWithValue("@usertable_user_id", Session["user_session_id"]);

        //this should include in the registartion asone of primary values as discussed.
        phone_number_command.Parameters.AddWithValue("@user_phone", phone_textbox.Text);
        phone_number_command.Parameters.AddWithValue("@user_id", Session["user_session_id"]);

        connectionString.Open();

        basic_update_address.ExecuteNonQuery();
        basic_update_city.ExecuteNonQuery();
        basic_update_post_code.ExecuteNonQuery();
        phone_number_command.ExecuteNonQuery();


        connectionString.Close();

        info_label.Text = "Basic info updated!";

    }

    protected void logout_button_Click(object sender, EventArgs e)
    {
            Session.Abandon();
            Response.Redirect("login_both.aspx");
    }
}