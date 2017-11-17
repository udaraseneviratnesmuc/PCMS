using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace pcms_web
{
    public partial class Payment : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Label2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            SqlConnection cnn = getConnection();
            Label1.Text = Util.getCurrentBalance(cnn)+"";
        }

        protected SqlConnection getConnection()
        {
            SqlConnection cnn = new SqlConnection(SqlDataSource1.ConnectionString);

            return cnn;
        }

        protected void markClaimPaid(int claimId)
        {
            SqlConnection cnn = getConnection();

            Label1.Text = debitFromAcc(claimId) + "";
            Label2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            cnn.Open();
            string pettyCashReqStatus = "update petty_cash_requests set Status=@status where ClaimId=@claimId";
            SqlCommand pettyCashReqStatusCmd = new SqlCommand(pettyCashReqStatus, cnn);
            pettyCashReqStatusCmd.Parameters.AddWithValue("@claimId", claimId);
            pettyCashReqStatusCmd.Parameters.AddWithValue("@status", ConstantVars.PAID);
            pettyCashReqStatusCmd.ExecuteNonQuery();

            GridView1.DataBind();
            cnn.Close();

        }

        protected Double debitFromAcc(int claimId) {
            SqlConnection cnn = getConnection();

            double amountToDebit = getClaimAmount(claimId);
            double currentAmount = Util.getCurrentBalance(cnn);

            cnn.Open();

            String sqlCmdStr = "insert into petty_cash_acc(timestamp, balance) values(@timestamp, @balance)";
            SqlCommand sqlCmd = new SqlCommand(sqlCmdStr, cnn);
            sqlCmd.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sqlCmd.Parameters.AddWithValue("@balance", currentAmount - amountToDebit);

            sqlCmd.ExecuteNonQuery();
            cnn.Close();

            return Util.getCurrentBalance(cnn);

        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            markClaimPaid(Convert.ToInt32(row.Cells[1].Text));
        }

        private double getClaimAmount(int claimId) 
        {
            double claimAmount = 0;
            SqlConnection cnn = getConnection();
            cnn.Open();

            string claimDetailsCmdStr = "select Amount from petty_cash_requests where ClaimId=@claimId";
            SqlCommand claimDetailsCmd = new SqlCommand(claimDetailsCmdStr, cnn);
            claimDetailsCmd.Parameters.AddWithValue("@claimId", claimId);
            SqlDataReader claimDetailsDr = claimDetailsCmd.ExecuteReader();

            while (claimDetailsDr.Read())
            {
                claimAmount = Convert.ToDouble(claimDetailsDr.GetSqlValue(0).ToString());
            }

            cnn.Close();

            return claimAmount;
        }
    }
}