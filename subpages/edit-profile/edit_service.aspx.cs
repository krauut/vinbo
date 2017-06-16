using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;    // SQL Query classes - such as SqlConnection, SqlCommand
using System.Configuration; // ConfigurationManager class - to access information saved in the web.config file         
using System.Data;  // namespace for the CommandType enumerator  

public partial class edit_service : System.Web.UI.Page
{
    SqlConnection connectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
   
    protected void Page_Load(object sender, EventArgs e)
    {


        //if (Session["user_session_id"] != null)
        //{

            //values for basic info
            string service_name = "";
            string service_email = "";
            string service_phone = "";
            string service_cvr = "";

            SqlCommand user_basic_info = new SqlCommand();
            user_basic_info.CommandText = "SELECT service_name, service_email, service_phone, service_cvr "
            + "FROM lastexam_servicetable " + "WHERE service_id ='" + Session["user_session_id"] + "'";

            user_basic_info.CommandType = CommandType.Text;
            user_basic_info.Connection = connectionString;
            connectionString.Open();

            SqlDataReader user_basic_info_reader = user_basic_info.ExecuteReader();
            while (user_basic_info_reader.Read())
            {
                service_name = user_basic_info_reader["service_name"].ToString();
                service_email = user_basic_info_reader["service_email"].ToString();
                service_phone = user_basic_info_reader["service_phone"].ToString();
                service_cvr = user_basic_info_reader["service_cvr"].ToString();
            }

            user_basic_info_reader.Close();
            connectionString.Close();

            //fullfiled mandatory values 
            name_label.Text = service_name;
            email_label.Text = service_email;
            phone_label.Text = service_phone;
            cvr_label.Text = service_cvr;
        //} else
        //{
        //    Response.Redirect("../login_both.aspx");
        //}
    }

    protected void update_pass_button_Click(object sender, EventArgs e)
    {
        bool password_match = false; // value for checking if the pass matches with the one in SLQ accordingly to the present session id
        bool old_password_match = false; // value which states if the new password is mathing in both text areas,the first and repeat

        //here we check if the old password is correct. if it is, we change old_password_match to true.
        SqlCommand password_validation_command = new SqlCommand();
        password_validation_command.CommandText = "SELECT service_pass " + "FROM lastexam_servicetable " + "WHERE service_id ='" + Session["user_session_id"] + "'";
        password_validation_command.CommandType = CommandType.Text;
        password_validation_command.Connection = connectionString;
        connectionString.Open();

        SqlDataReader password_validation_reader = password_validation_command.ExecuteReader();
        while (password_validation_reader.Read())
        {
            var service_pass = password_validation_reader["service_pass"].ToString();
            if (service_pass == old_pass_textbox.Text)
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

        //and if everything is fine, we execute this query and we are changing the password for the service
        if (password_match && old_password_match)
        {
            string commandText = "UPDATE lastexam_servicetable " +
            "SET service_pass=@service_pass WHERE service_id=@service_id";

            SqlCommand command = new SqlCommand(commandText, connectionString);

            command.Parameters.AddWithValue("@service_pass", repeat_pass_textbox.Text);
            command.Parameters.AddWithValue("@service_id", Session["user_session_id"]);

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

    protected void description_button_Click(object sender, EventArgs e)
    {
        //updating location information
        string description_update_query = "UPDATE lastexam_servicetable " +
            "SET service_description=@service_description WHERE service_id=@service_id";

        SqlCommand service_description_command = new SqlCommand(description_update_query, connectionString);

        //this should include in the registartion asone of primary values as discussed.
        service_description_command.Parameters.AddWithValue("@service_description", description_textbox.Text);
        service_description_command.Parameters.AddWithValue("@service_id", Session["user_session_id"]);

        connectionString.Open();
        service_description_command.ExecuteNonQuery();
        connectionString.Close();

        info_label.Text = "Description updated";
    }

    protected void update_contact_button_Click(object sender, EventArgs e)
    {

        //updating basic info

        string company_name = "UPDATE lastexam_servicetable " +
            "SET service_company_name=@service_company_name WHERE service_id=@service_id";

        string responsible_person_name = "UPDATE lastexam_servicetable " +
            "SET service_name=@service_responsible_name WHERE service_id=@service_id";

        string responsible_person_surname = "UPDATE lastexam_servicetable " +
            "SET service_surname=@service_responsible_surname WHERE service_id=@service_id";

        string phone_number_update = "UPDATE lastexam_servicetable " +
            "SET service_phone=@service_phone WHERE service_id=@service_id";

        string basic_update_query_address = "UPDATE lastexam_servicetable " +
            "SET service_address=@service_address WHERE service_id=@service_id";

        string basic_update_query_city = "UPDATE lastexam_servicetable_city " +
            "SET service_city_name=@service_city_name WHERE lastexam_servicetable_service_id=@service_id";

        string basic_update_query_post_code = "UPDATE lastexam_servicetable " +
            "SET service_postcode=@service_postcode WHERE service_id=@service_id";

  

        SqlCommand basic_company_name = new SqlCommand(company_name, connectionString);
        SqlCommand basic_responsible_person_name = new SqlCommand(responsible_person_name, connectionString);
        SqlCommand basic_responsible_person_surname = new SqlCommand(responsible_person_surname, connectionString);
        SqlCommand phone_number_command = new SqlCommand(phone_number_update, connectionString);
        SqlCommand basic_update_address = new SqlCommand(basic_update_query_address, connectionString);
        SqlCommand basic_update_city = new SqlCommand(basic_update_query_city, connectionString);
        SqlCommand basic_update_post_code = new SqlCommand(basic_update_query_post_code, connectionString);
      


        basic_company_name.Parameters.AddWithValue("@service_company_name", company_name_textbox.Text);
        basic_company_name.Parameters.AddWithValue("@service_id", Session["user_session_id"]);

        basic_responsible_person_name.Parameters.AddWithValue("@service_responsible_name", responsible_name_textbox.Text);
        basic_responsible_person_name.Parameters.AddWithValue("@service_id", Session["user_session_id"]);

        basic_responsible_person_surname.Parameters.AddWithValue("@service_responsible_surname", responsible_surname_textbox.Text);
        basic_responsible_person_surname.Parameters.AddWithValue("@service_id", Session["user_session_id"]);

        phone_number_command.Parameters.AddWithValue("@service_phone", phone_textbox.Text);
        phone_number_command.Parameters.AddWithValue("@service_id", Session["user_session_id"]);

        basic_update_address.Parameters.AddWithValue("@service_address", address_textbox.Text);
        basic_update_address.Parameters.AddWithValue("@service_id", Session["user_session_id"]);

        basic_update_city.Parameters.AddWithValue("@service_city_name", city_ddl.SelectedValue);
        basic_update_city.Parameters.AddWithValue("@service_id", Session["user_session_id"]);

        basic_update_post_code.Parameters.AddWithValue("@service_postcode", postnumber_textbox.Text);
        basic_update_post_code.Parameters.AddWithValue("@service_id", Session["user_session_id"]);

        

        connectionString.Open();

        basic_company_name.ExecuteNonQuery();
        basic_responsible_person_name.ExecuteNonQuery();
        basic_responsible_person_surname.ExecuteNonQuery();
        phone_number_command.ExecuteNonQuery();
        basic_update_address.ExecuteNonQuery();
        basic_update_city.ExecuteNonQuery();
        basic_update_post_code.ExecuteNonQuery();

        connectionString.Close();

        info_label.Text = "Basic info updated!";
    }

    protected void logout_button_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("login_both.aspx");
    }
}