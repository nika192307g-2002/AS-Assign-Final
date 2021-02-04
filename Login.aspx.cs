using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Drawing;

namespace AS_Assign_Final
{
    public partial class Login : System.Web.UI.Page
    {

        string asDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["asDBConnection"].ConnectionString;

        public bool ValidateInput()
        {
            lbl_msg.Text = String.Empty;
            lbl_msg.ForeColor = Color.Red;

            if (String.IsNullOrEmpty(tb_login_email.Text))
            {
                lbl_msg.Text += "Email is required" + "<br/>";
            }

            if (String.IsNullOrEmpty(tb_login_pwd.Text))
            {
                lbl_msg.Text += "Password is required" + "<br/>";
            }

            if (String.IsNullOrEmpty(lbl_msg.Text))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }

        public bool ValidateCaptcha()
        {
            bool result = true;

            string captchaResponse = Request.Form["g-recaptcha-response"];

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
                ("https://www.google.com/recaptcha/api/siteverify?secret=6LeYPkQaAAAAAM3YERuLxRRTqMaPoDBymBkSzuea &response=" + captchaResponse);

            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();
                        lbl_msg.Text = jsonResponse.ToString();

                        JavaScriptSerializer js = new JavaScriptSerializer();

                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);

                        result = Convert.ToBoolean(jsonObject.success);
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        protected string getDBHash(string email)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(asDBConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);

            try
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["passwordHash"] != null)
                        {
                            if (reader["passwordHash"] != DBNull.Value)
                            {
                                h = reader["passwordHash"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }


        protected string getDBSalt(string email)
        {
            string s = null;

            SqlConnection connection = new SqlConnection(asDBConnectionString);
            string sql = "Select passwordSalt FROM ACCOUNT WHERE email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);

            try
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["passwordSalt"] != null)
                        {
                            if (reader["passwordSalt"] != DBNull.Value)
                            {
                                s = reader["passwordSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            if (ValidateInput()) { 
            string pwd = tb_login_pwd.Text.ToString();
            string email = tb_login_email.Text.ToString();

            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(email);
            string dbSalt = getDBSalt(email);



            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);

                    if (userHash.Equals(dbHash))
                    {
                        if (ValidateCaptcha())
                        {

                            Session["email"] = email;
                            Response.Redirect("Success.aspx", false);

                                //prevent session fixation: 
                                string guid = Guid.NewGuid().ToString();
                                Session["AuthToken"] = guid;
                                Response.Cookies.Add(new HttpCookie("AuthToken", guid));


                        }
                        else
                        {
                            lbl_msg.Text += "Captcha is not valid. Please try again." + "<br/>";
                        }
                    }
                    else
                    {
                        lbl_msg.Text = "Email or Password not vaild. Please try again." + "<br/>";
                        Response.Redirect("Login.aspx");
                        tb_login_email.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
        }
        }

        protected void btn_register_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

      

        
    }

}
    


