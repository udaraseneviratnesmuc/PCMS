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
        }

        protected void pend_Click(object sender, EventArgs e)
        {
            setClaimPending(Convert.ToInt32(requestId.Text));
        }

    }
}