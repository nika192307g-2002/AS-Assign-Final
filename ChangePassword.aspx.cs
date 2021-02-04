using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Data;


namespace AS_Assign_Final
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        string asDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["asDBConnection"].ConnectionString;
        string email = null;

        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    email = (string)Session["email"];

                    displayMyAccount(email);
                }

            }
        }

        protected void displayMyAccount(string email)
        {
            SqlConnection connection = new SqlConnection(asDBConnectionString);
            string sql = "SELECT * FROM Account WHERE email=@email";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["email"] != DBNull.Value)
                        {
                            lbl_email.Text = reader["email"].ToString();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

        }

        public bool ValidateInput()
        {
            lbl_error.Text = String.Empty; 

            if (String.IsNullOrEmpty(tb_newPwd.Text))
            {
                lbl_error.Text += "New Password is required" + "<br/>";
            }
            if (String.IsNullOrEmpty(tb_cfmNewPwd.Text))
            {
                lbl_error.Text += "Confirm New Password is required" + "<br/>";
            }
            if (tb_newPwd.Text != tb_cfmNewPwd.Text)
            {
                lbl_error.Text += "Passwords entered do not match" + "<br/>";
            }
            if (String.IsNullOrEmpty(lbl_error.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void btnSavePwd_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                string pwd = tb_cfmNewPwd.Text.ToString().Trim();
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];

                rng.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);

                SHA512Managed hashing = new SHA512Managed();

                string pwdWithSalt = pwd + salt;
                byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

                finalHash = Convert.ToBase64String(hashWithSalt);

                RijndaelManaged cipher = new RijndaelManaged();
                cipher.GenerateKey();
                Key = cipher.Key;
                IV = cipher.IV;
                string email = lbl_email.Text.ToString();

                updatePassword(email);

            }
        }

        public void updatePassword(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(asDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Account SET passwordHash=@passwordHash, passwordSalt=@passwordSalt WHERE email=@email"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@passwordHash", finalHash);
                            cmd.Parameters.AddWithValue("@passwordSalt", salt);
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}