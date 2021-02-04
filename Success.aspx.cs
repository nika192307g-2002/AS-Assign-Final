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
    public partial class Success : System.Web.UI.Page
    {

        string asDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["asDBConnection"].ConnectionString;
        string email = null;

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

        protected void displayMyAccount (string email)
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
                        if (reader["fname"] != DBNull.Value)
                        {
                            lbl_fname.Text = reader["fname"].ToString();
                        }
                        if (reader["lname"] != DBNull.Value)
                        {
                            lbl_lname.Text = reader["lname"].ToString();
                        }
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

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("Login.aspx", false);

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
        }

        protected void btnchngPwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");
        }
    }
}