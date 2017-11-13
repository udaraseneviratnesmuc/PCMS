using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace pcms_web
{
    public partial class PettyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PACurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            PACurrentDay.Text = DateTime.Now.DayOfWeek.ToString();
            PACurrentBal.Text = getCurrentBalance()+"";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {
                Double newBalanceWithCreditedAmount = creditToAccount(Convert.ToDouble(amount.Text));
                PACurrentBal.Text = newBalanceWithCreditedAmount + "";
            }
        }

        protected Double creditToAccount(Double amountToCredit) {

            Double availableBalance = getCurrentBalance();
            Double newBalance = availableBalance + amountToCredit;

            SqlConnection cnn = getConnection();
            cnn.Open();

            String sqlCmdStr = "insert into petty_cash_acc(timestamp, balance) values(@timestamp, @balance)";
            SqlCommand sqlCmd = new SqlCommand(sqlCmdStr, cnn);
            sqlCmd.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sqlCmd.Parameters.AddWithValue("@balance", newBalance);

            sqlCmd.ExecuteNonQuery();
            cnn.Close();

            return getCurrentBalance();
        }

        protected Double getCurrentBalance() {

            SqlConnection cnn = getConnection();
            cnn.Open();

            string maxIdCommandStr = "select max(id) from petty_cash_acc";
            SqlCommand maxIdCommand = new SqlCommand(maxIdCommandStr, cnn);
            int maxId = Convert.ToInt32(maxIdCommand.ExecuteScalar().ToString());

            string currentBalanceCommandStr = "select balance from petty_cash_acc where id='"+maxId+"'";
            SqlCommand currentBalCommand = new SqlCommand(currentBalanceCommandStr, cnn);
            SqlDataReader dataReader = currentBalCommand.ExecuteReader();

            String currentBal = "0";

            while(dataReader.Read()){
                currentBal = dataReader.GetSqlValue(0).ToString();
            }

            cnn.Close();
            return Convert.ToDouble(currentBal);
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
            }
        }

        protected Double debitFromAcc(Double amoumntToDebit){
            Double availableBalance = getCurrentBalance();
            Double newBalance = availableBalance - amoumntToDebit;

            SqlConnection cnn = getConnection();
            cnn.Open();

            String sqlCmdStr = "insert into petty_cash_acc(timestamp, balance) values(@timestamp, @balance)";
            SqlCommand sqlCmd = new SqlCommand(sqlCmdStr, cnn);
            sqlCmd.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sqlCmd.Parameters.AddWithValue("@balance", newBalance);

            sqlCmd.ExecuteNonQuery();
            cnn.Close();

            return getCurrentBalance();

        }
    }
}