using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace BankingLib
{
    public class LoginDAL
    {

        public bool VerifyData(Login lg)
        {
            bool loginStatus = false;
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Banking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from login where Account_NO ='" + lg.Account_No + "' and CRN='" + lg.CRN + "' and Password='" + lg.Password + "'", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                cn.Close();
                loginStatus = true;
            }
            else
            {
                cn.Close();
                loginStatus = false;
            }

            return loginStatus;
        }

        public List<Login> GetLoginDetails()
        {
            List<Login> lgList = new List<Login>();
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Banking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from Login", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Login lg = new Login();
                lg.Account_No = Convert.ToInt64(dr["Account_NO"]);
                lg.CRN = Convert.ToInt64(dr["CRN"]);
                if (dr["LastLogin"] is DBNull)
                    lg.LastLogin = null;
                else
                    lg.LastLogin=Convert.ToDateTime(dr["LastLogin"]);
                lgList.Add(lg);
            }
            cn.Close();
            return lgList;

        }

        public void UpdateLoginDetails(Login lgdata)
        {

            //UpdateEmployee
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Banking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("UpdateLoginDetails", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Account_NO", lgdata.Account_No);
            cmd.Parameters.AddWithValue("@CRN", lgdata.CRN);
            cmd.Parameters.AddWithValue("@Password", lgdata.Password);
            cmd.Parameters.AddWithValue("@LastLogin", lgdata.LastLogin);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public void changepassword(Login lg)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Banking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Change_Pwd", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Account_NO", lg.Account_No);

            cmd.Parameters.AddWithValue("@new_pwd", lg.Password);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            
        }
    }
}