using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace pcms_web.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.Page.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void ValidateUser(object sender, EventArgs e)
        {
            int userId = 0;
            string roles = string.Empty;
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from user_tbl where UserName=@Username and Password=@Password"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Username", LoginUser.UserName);
                    cmd.Parameters.AddWithValue("@Password", LoginUser.Password);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        userId = Convert.ToInt32(reader["UserId"]);
                        CustomUserContext.userId = userId;
                        roles = reader["RoleId"].ToString();
                        con.Close();

                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, LoginUser.UserName, DateTime.Now, DateTime.Now.AddMinutes(2880), LoginUser.RememberMeSet, roles, FormsAuthentication.FormsCookiePath);
                        string hash = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                        if (ticket.IsPersistent)
                        {
                            cookie.Expires = ticket.Expiration;
                        }
                        Response.Cookies.Add(cookie);
                        Response.Redirect(FormsAuthentication.GetRedirectUrl(LoginUser.UserName, LoginUser.RememberMeSet));
                    }
                    else {
                        LoginUser.FailureText = "Username and/or password is incorrect";
                    }        
                }
            }
        }
    }
}
