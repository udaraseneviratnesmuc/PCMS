using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace pcms_web
{
    public partial class PettyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = getConnection();

            PACurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            PACurrentDay.Text = DateTime.Now.DayOfWeek.ToString();
            PACurrentBal.Text = Util.getCurrentBalance(cnn)+"";
            bindGridView();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {
                Double newBalanceWithCreditedAmount = creditToAccount(Convert.ToDouble(amount.Text));
                PACurrentBal.Text = newBalanceWithCreditedAmount + "";
                bindGridView();
            }
        }

        protected Double creditToAccount(Double amountToCredit) {
            SqlConnection cnn = getConnection();

            Double availableBalance = Util.getCurrentBalance(cnn);
            Double newBalance = availableBalance + amountToCredit;
            
            cnn.Open();

            String sqlCmdStr = "insert into petty_cash_acc(timestamp, balance) values(@timestamp, @balance)";
            SqlCommand sqlCmd = new SqlCommand(sqlCmdStr, cnn);
            sqlCmd.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sqlCmd.Parameters.AddWithValue("@balance", newBalance);

            sqlCmd.ExecuteNonQuery();
            cnn.Close();

            return Util.getCurrentBalance(cnn);
        }

        protected SqlConnection getConnection() {
            SqlConnection cnn = new SqlConnection(SqlDataSource1.ConnectionString);

            return cnn;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {
                Double newBalanceWithDebitedAmount = debitFromAcc(Convert.ToDouble(amount.Text));
                PACurrentBal.Text = newBalanceWithDebitedAmount + "";
                bindGridView();
            }
        }

        protected Double debitFromAcc(Double amoumntToDebit){
            SqlConnection cnn = getConnection();

            Double availableBalance = Util.getCurrentBalance(cnn);
            Double newBalance = availableBalance - amoumntToDebit;

            cnn.Open();

            String sqlCmdStr = "insert into petty_cash_acc(timestamp, balance) values(@timestamp, @balance)";
            SqlCommand sqlCmd = new SqlCommand(sqlCmdStr, cnn);
            sqlCmd.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sqlCmd.Parameters.AddWithValue("@balance", newBalance);

            sqlCmd.ExecuteNonQuery();
            cnn.Close();

            return Util.getCurrentBalance(cnn);

        }

        protected DataTable getData(){
            SqlConnection cnn = getConnection();
            cnn.Open();

            string viewPettyCashAccRecStr = "select * from petty_cash_acc order by id desc";
            SqlCommand viewPettyCashRecCmd = new SqlCommand(viewPettyCashAccRecStr, cnn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(viewPettyCashRecCmd);
            cnn.Close();

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            return dt;
        }

        protected void bindGridView() {
            GridView1.DataSourceID = null;
            GridView1.DataSource = getData();
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bindGridView();
        }
    }
}