using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;
using System.Drawing;

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AS_Assign_Final
{
    public partial class Registration : System.Web.UI.Page
    {

        
        string asDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["asDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV; 
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool ValidateInput()
        {
            lbl_error.Text = String.Empty;
            lbl_error.ForeColor = Color.Red;

            if (String.IsNullOrEmpty(tb_fname.Text))
            {
                lbl_error.Text += "First Name is required" + "<br/>";
            }

            if (String.IsNullOrEmpty(tb_lname.Text))
            {
                lbl_error.Text += "Last Name is required" + "<br/>";
            }

            if (String.IsNullOrEmpty(tb_cardNo.Text))
            {
                lbl_error.Text += "Credit Card Number is required" + "<br/>";
            }
            if (String.IsNullOrEmpty(tb_cardCVV.Text))
            {
                lbl_error.Text += "CVV is required" + "<br/>";
            }

            if (String.IsNullOrEmpty(tb_email.Text))
            {
                lbl_error.Text += "Email Address is required" + "<br/>";
            }



            if (String.IsNullOrEmpty(tb_password.Text))
            {
                lbl_error.Text += "Password is required" + "<br/>";
            }

            if (tb_cardCVV.Text.Length != 3)
            {
                lbl_error.Text += "CVV is incorrect (it should be 3 digits)" + "<br/>";
            }

            if (tb_cardNo.Text.Length != 16)
            {
                lbl_error.Text += "Credit Card Number is incorrect (it should be 16 digits)" + "<br/>";
            }

            if(cal_dob.SelectedDate == null)
            {
                lbl_error.Text += "Date of Birth is not selected!" + "<br/>";
            }

            if (cal_expirydate.SelectedDate == null)
            {
                lbl_error.Text += "Credit Card Expiry Date is not selected!" + "<br/>";
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

        protected void btn_register_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                string pwd = tb_password.Text.ToString().Trim();
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

                createAccount();
            }
           
        }

        public void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(asDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@fname, @lname, @creditNo, @cvv, @email, @passwordHash, @dob, @passwordSalt, @creditexpirydate, @IV, @Key)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            string text_fname = HttpUtility.HtmlEncode(tb_fname.Text);
                            string text_lname = HttpUtility.HtmlEncode(tb_lname.Text);
                            string text_cardNo = HttpUtility.HtmlEncode(tb_cardNo.Text);
                            string text_cardCVV = HttpUtility.HtmlEncode(tb_cardCVV.Text);
                            string text_email = HttpUtility.HtmlEncode(tb_email.Text);

                            string dob = cal_dob.SelectedDate.ToString("dd-MM-yyyy");
                            DateTime text_dob = Convert.ToDateTime(dob);

                            string expirydate = cal_expirydate.SelectedDate.ToString("dd-MM-yyyy");
                            DateTime text_expirydate = Convert.ToDateTime(expirydate);

                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@fname", text_fname);
                            cmd.Parameters.AddWithValue("@lname", text_lname);
                            cmd.Parameters.AddWithValue("@creditNo", Convert.ToBase64String(encryptData(text_cardNo.Trim())));
                            cmd.Parameters.AddWithValue("@cvv", Convert.ToBase64String(encryptData(text_cardCVV.Trim())));
                            cmd.Parameters.AddWithValue("@email", text_email);
                            cmd.Parameters.AddWithValue("@passwordHash", finalHash);
                            cmd.Parameters.AddWithValue("@dob", text_dob);
                            cmd.Parameters.AddWithValue("@passwordSalt", salt);
                            cmd.Parameters.AddWithValue("@creditexpirydate", Convert.ToBase64String(encryptData(text_expirydate.ToString())));
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

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null; 
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length); 
                             
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString()); 
            }
            finally { }
            return cipherText; 
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        
    }
}