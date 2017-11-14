using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace pcms_web
{
    public partial class PetCashRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected SqlConnection getConnection()
        {
            SqlConnection cnn = new SqlConnection(SqlDataSource1.ConnectionString);

            return cnn;
        }

        protected void addClaimRequest(string title, String timestamp, string description, string amount, int userId) {
            SqlConnection cnn = getConnection();
            cnn.Open();

            String sqlCmdStr = "insert into petty_cash_requests(Title, TransactionDate, Amount, Description, UserId) values(@title, @transactionDate, @amount, @description, @userId)";
            SqlCommand sqlCmd = new SqlCommand(sqlCmdStr, cnn);
            sqlCmd.Parameters.AddWithValue("@title", title);
            sqlCmd.Parameters.AddWithValue("@transactionDate", timestamp);
            sqlCmd.Parameters.AddWithValue("@amount", amount);
            sqlCmd.Parameters.AddWithValue("@description", description);
            sqlCmd.Parameters.AddWithValue("@userId", userId);

            sqlCmd.ExecuteNonQuery();
            cnn.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {
                int userId = Convert.ToInt16(Request.Cookies["userId"].Value);
                addClaimRequest(title.Text, transactionDate.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss"), description.Text, amount.Text, userId);
            }
        }
    }
}