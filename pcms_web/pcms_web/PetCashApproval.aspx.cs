using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace pcms_web
{
    public partial class PetCashApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            requestId.Text = row.Cells[1].Text;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            approveClaim(Convert.ToInt32(requestId.Text));
        }

        protected SqlConnection getConnection()
        {
            SqlConnection cnn = new SqlConnection(SqlDataSource1.ConnectionString);

            return cnn;
        }

        protected void approveClaim(int claimId) {
            SqlConnection cnn = getConnection();
            cnn.Open();

            string pettyCashReqStatus = "update petty_cash_requests set Status=@status where ClaimId=@claimId";
            SqlCommand pettyCashReqStatusCmd = new SqlCommand(pettyCashReqStatus, cnn);
            pettyCashReqStatusCmd.Parameters.AddWithValue("@claimId", claimId);
            pettyCashReqStatusCmd.Parameters.AddWithValue("@status", ConstantVars.APPROVED);
            pettyCashReqStatusCmd.ExecuteNonQuery();

            GridView1.DataBind();
            cnn.Close();
            sendNotification(claimId, ConstantVars.APPROVED);
        }

        protected void rejectClaim(int claimId)
        {
            SqlConnection cnn = getConnection();
            cnn.Open();

            string pettyCashReqStatus = "update petty_cash_requests set Status=@status where ClaimId=@claimId";
            SqlCommand pettyCashReqStatusCmd = new SqlCommand(pettyCashReqStatus, cnn);
            pettyCashReqStatusCmd.Parameters.AddWithValue("@claimId", claimId);
            pettyCashReqStatusCmd.Parameters.AddWithValue("@status", ConstantVars.REJECTED);
            pettyCashReqStatusCmd.ExecuteNonQuery();

            GridView1.DataBind();
            cnn.Close();

            sendNotification(claimId, ConstantVars.REJECTED);
        }

        protected void reject_Click(object sender, EventArgs e)
        {
            rejectClaim(Convert.ToInt32(requestId.Text));
        }

        protected void setClaimPending(int claimId)
        {
            SqlConnection cnn = getConnection();
            cnn.Open();

            string pettyCashReqStatus = "update petty_cash_requests set Status=@status where ClaimId=@claimId";
            SqlCommand pettyCashReqStatusCmd = new SqlCommand(pettyCashReqStatus, cnn);
            pettyCashReqStatusCmd.Parameters.AddWithValue("@claimId", claimId);
            pettyCashReqStatusCmd.Parameters.AddWithValue("@status", ConstantVars.PENDING);
            pettyCashReqStatusCmd.ExecuteNonQuery();

            GridView1.DataBind();
            cnn.Close();

            sendNotification(claimId, ConstantVars.PENDING);
        }

        protected void pend_Click(object sender, EventArgs e)
        {
            setClaimPending(Convert.ToInt32(requestId.Text));
        }

        protected void sendNotification(int claimId, string claimStatus) {
            Dictionary<string, string> claimerData = getClaimersDetails(claimId);
            var username = claimerData["username"];
            var email = claimerData["email"];
            var subject = "Petty Cash Claim ";
            var body = "Dear " + username + ", \n";

            if (claimStatus == ConstantVars.PENDING) {
                subject = subject + "[Pending]";
                body = body + "Your petty claim request status changed to PENDING state. If you have any issue regarding that please contact the admin";
            }
            else if (claimStatus == ConstantVars.APPROVED)
            {
                subject = subject + "[Approved]";
                body = body + "Your petty claim request status changed to APPROVED state. Please collect your money";
            }else if(claimStatus == ConstantVars.REJECTED){
                subject = subject + "[Rejected]";
                body = body + "Your petty claim request status changed to REJECTED state. Please contact the admin";
            }

            SendMail sendMail = new SendMail();
            sendMail.sendViaGmail("udara.seneviratne@hsenidmobile.com", email, subject, body);
        }

        private Dictionary<string, string> getClaimersDetails(int claimId) {
            SqlConnection cnn = getConnection();
            cnn.Open();

            string claimersIdCmdStr = "select UserId from petty_cash_requests where ClaimId=@claimId";
            SqlCommand claimerIdCmd = new SqlCommand(claimersIdCmdStr, cnn);
            claimerIdCmd.Parameters.AddWithValue("@claimId", claimId);
            SqlDataReader dataReader = claimerIdCmd.ExecuteReader();

            int userId = 0;

            while (dataReader.Read())
            {
                userId = Convert.ToInt32(dataReader.GetSqlValue(0).ToString());
            }

            cnn.Close();

            cnn.Open();
            string claimersDetailsCmdStr = "select UserName, Email from user_tbl where UserId=@userId";
            SqlCommand claimersDetailsCmd = new SqlCommand(claimersDetailsCmdStr, cnn);
            claimersDetailsCmd.Parameters.AddWithValue("@userId", userId);
            SqlDataReader claimerDetailsDr = claimersDetailsCmd.ExecuteReader();

            Dictionary<string, string> claimerData = new Dictionary<string, string>();
            while (claimerDetailsDr.Read())
            {
                claimerData.Add("username", claimerDetailsDr.GetSqlValue(0).ToString());
                claimerData.Add("email", claimerDetailsDr.GetSqlValue(1).ToString());
            }

            cnn.Close();

            return claimerData;
        }

    }
}