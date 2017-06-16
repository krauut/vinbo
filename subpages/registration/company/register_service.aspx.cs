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

//terms and conditions


public partial class _Default : System.Web.UI.Page
{

    SqlConnection connectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {


        //price_range_container.Visible = true;

        //from_floor_ddl.Visible = false;
        //way_of_charge_radio.Visible = false;
        //extra_fixed_textbox.Visible = false;
        //extra_abstract_textbox.Visible = false;

        //it is a bit dangerous with some advanced users because it is apossible to disable this function through source

        //company_name_textbox.Enabled = false;
        //address_textbox.Enabled = false;
        //phone_textbox.Enabled = false;
        //email_textbox.Enabled = false;

        //company_name_textbox.ReadOnly = true;
        //address_textbox.ReadOnly = true;
        //phone_textbox.ReadOnly = true;
        //email_textbox.ReadOnly = true;


        



    }

    protected void way_of_charge_radio_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (price_higher_floors_radio.SelectedValue == "0")
        //{
        //    price_range_container.Visible = false;

        //    //from_floor_ddl.Visible = true;
        //    //way_of_charge_radio.Visible = true;
        //    //extra_fixed_textbox.Visible = true;
        //    //extra_abstract_textbox.Visible = true;
        //}
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
        if (repeat_password_textbox.Text == password_textbox.Text)
        {
            password_match = true;
        };

        // also checking are those input values are not empty
        if (email_textbox.Text == "" || email_textbox.Text == " ")
        {
            input_fields_validation = false;
        };

        // email validation - looking for same email in the database of users. Later on, in the services
        SqlCommand user_email_validation_check = new SqlCommand();
        user_email_validation_check.CommandText = "SELECT user_email " + "FROM lastexam_userTable " + "WHERE user_email ='" + email_textbox.Text + "'";
        user_email_validation_check.CommandType = CommandType.Text;
        user_email_validation_check.Connection = connectionString;
        connectionString.Open();

        SqlDataReader user_email_reader = user_email_validation_check.ExecuteReader();
        while (user_email_reader.Read())
        {
            string user_email_result = user_email_reader["user_email"].ToString();

            if (string.Equals(user_email_result, email))
            {
                // if value from db is equal to email, it's true and we catch the mistake.
                existing_email = true;
            }
            else
            {
                //advancing to the next step in order to check the service table
                next_step_existing_email = true;
            }
        }
        user_email_reader.Close();
        connectionString.Close();

        // if there were no results in the first table, lets move on to the next one
        if (next_step_existing_email == true)
        {

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

        /*
text_label
------
1. name_textbox
2. surname_textbox
X. company_name_textbox
3. cvr_textbox
4. address_textbox
5. city_ddl > 1 2 3 4
6. outter_textbox
7. inner_textbox
8. highest_ddl > 3 4 5 6 7 8 9
9. react_ddl > 1 2 3
10. price_higher_floors_radio > (Selected="True") (Selected="False")
11. from_floor_ddl > 1 2 3
12. way_of_charge_radio > (Selected="True") (Selected="False")
13. extra_fixed_textbox
14. extra_abstract_textbox
------
15. email_textbox
16. password_textbox
17. repeat_password_textbox
------
signup_button
*/



        //-------------- result after validations
        if (password_match == true && input_fields_validation == true && existing_email == false)
        {

            //This functionality for later. 

            //extra_fixed_textbox
            //extra_abstract_textbox
            //from_floor_ddl


            connectionString.Open();

            
            DateTime signup_date = DateTime.Now;
            string formated_signup_date = signup_date.ToString("yyyy-MM-dd HH:mm");
            string user_signup_query = "INSERT INTO lastexam_servicetable (service_name,service_surname,service_company_name,service_cvr,service_address,service_postcode,service_price_outter,service_price_inner,service_price_highest_floor,service_reaction_day,service_price_range_bool,service_price_range_min_floor,service_price_range_way_of_charging,service_price_range_way_extra_all,service_price_range_way_extra_each,service_email,service_pass,service_status,service_img,service_phone,service_description)"
            + " VALUES (@service_name, @service_surname, @service_company_name, @service_cvr, @service_address, @service_postcode, @service_price_outter, @service_price_inner, @service_price_highest_floor, @service_reaction_day, @service_price_range_bool, @service_price_range_min_floor, @service_price_range_way_of_charging, @service_price_range_way_extra_all, @service_price_range_way_extra_each, @service_email, @service_pass, @service_status, @service_img, @service_phone,@service_description)";

            SqlCommand service_registration_command = new SqlCommand(user_signup_query, connectionString);

                service_registration_command.Parameters.AddWithValue("@service_name", name_textbox.Text);
                service_registration_command.Parameters.AddWithValue("@service_surname", surname_textbox.Text);
                service_registration_command.Parameters.AddWithValue("@service_company_name", company_name_textbox.Text);
                service_registration_command.Parameters.AddWithValue("@service_cvr", Int32.Parse(cvr_textbox.Text));
                service_registration_command.Parameters.AddWithValue("@service_address", address_textbox.Text); // SERVICE ADDRESS
                service_registration_command.Parameters.AddWithValue("@service_postcode", 9000); //ddl   -    Service city name? Another table? (city_ddl.SelectedValue)
                service_registration_command.Parameters.AddWithValue("@service_price_outter", Int32.Parse(outter_textbox.Text));
                service_registration_command.Parameters.AddWithValue("@service_price_inner", Int32.Parse(inner_textbox.Text));
                service_registration_command.Parameters.AddWithValue("@service_price_highest_floor", Int32.Parse(highest_ddl.SelectedValue)); //ddl
                service_registration_command.Parameters.AddWithValue("@service_reaction_day", Int32.Parse(react_ddl.SelectedValue)); //ddl
                service_registration_command.Parameters.AddWithValue("@service_price_range_bool", price_higher_floors_radio.SelectedValue); //radio - T F (Int32.Parse())
                service_registration_command.Parameters.AddWithValue("@service_price_range_min_floor", Int32.Parse(from_floor_ddl.SelectedValue)); //ddl
                service_registration_command.Parameters.AddWithValue("@service_price_range_way_of_charging", Int32.Parse(way_of_charge_radio.SelectedValue)); //radio T F
                service_registration_command.Parameters.AddWithValue("@service_price_range_way_extra_all", Int32.Parse(extra_fixed_textbox.Text));
                service_registration_command.Parameters.AddWithValue("@service_price_range_way_extra_each", Int32.Parse(extra_abstract_textbox.Text));
                service_registration_command.Parameters.AddWithValue("@service_email", email_textbox.Text);
                service_registration_command.Parameters.AddWithValue("@service_pass", repeat_password_textbox.Text);
                service_registration_command.Parameters.AddWithValue("@service_status", 0);
                service_registration_command.Parameters.AddWithValue("@service_img", "empty");
                service_registration_command.Parameters.AddWithValue("@service_phone", phone_textbox.Text); 
                service_registration_command.Parameters.AddWithValue("@service_description", "");

            service_registration_command.ExecuteNonQuery();

            connectionString.Close();


            //i need to get id of service where inputed email is equal to email found.


            string service_id_for_city = "";

            SqlCommand email_id_check = new SqlCommand(); // AND ++ remember me
            email_id_check.CommandText = "SELECT service_id " + "FROM lastexam_servicetable " + "WHERE service_email ='" + email_textbox.Text + "'";
            email_id_check.CommandType = CommandType.Text;
            email_id_check.Connection = connectionString;
            connectionString.Open();

            SqlDataReader id_reader = email_id_check.ExecuteReader();
            while (id_reader.Read())
            {
                service_id_for_city = id_reader["service_id"].ToString();
            }
            id_reader.Close();
            connectionString.Close();

            //service city. 

            // The reason why it's not in the same table its due to many to one relationship.
            // Same company can have more than one headquarter in whole country.

            string service_city_query = "INSERT INTO lastexam_servicetable_city (lastexam_servicetable_service_id,service_city_name)"
            + " VALUES (@lastexam_servicetable_service_id, @service_city_name)";

            SqlCommand service_city_command = new SqlCommand(service_city_query, connectionString);

            service_city_command.Parameters.AddWithValue("@lastexam_servicetable_service_id", service_id_for_city);
            service_city_command.Parameters.AddWithValue("@service_city_name", (city_ddl.SelectedValue).ToString());

            connectionString.Open();
            service_city_command.ExecuteNonQuery();
            connectionString.Close();

            text_label.InnerText = "Service " + company_name_textbox.Text + " Created!";

            //default values for servicetablecity?
        }
        else
        {

            if (password_match == false)
            {

                text_label.InnerText = "Password did not match, try again!";

            }
            else if (input_fields_validation == false)
            {

                text_label.InnerText = "Email field is empty!";

            }
            else if (existing_email == true)
            {

                text_label.InnerText = "Email address is already in use!";

            }

        }
    }
}