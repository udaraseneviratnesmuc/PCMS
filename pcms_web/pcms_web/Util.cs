using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace pcms_web
{
    public class Util
    {
        public static Double getCurrentBalance(SqlConnection cnn)
        {
            cnn.Open();

            string maxIdCommandStr = "select max(id) from petty_cash_acc";
            SqlCommand maxIdCommand = new SqlCommand(maxIdCommandStr, cnn);
            int maxId = Convert.ToInt32(maxIdCommand.ExecuteScalar().ToString());

            string currentBalanceCommandStr = "select balance from petty_cash_acc where id='" + maxId + "'";
            SqlCommand currentBalCommand = new SqlCommand(currentBalanceCommandStr, cnn);
            SqlDataReader dataReader = currentBalCommand.ExecuteReader();

            String currentBal = "0";

            while (dataReader.Read())
            {
                currentBal = dataReader.GetSqlValue(0).ToString();
            }

            cnn.Close();
            return Convert.ToDouble(currentBal);
        }
    }
}