using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BankingLib
{
    public class RegisterDAL
    {
        public List<Registration> PeekDetails()
        {
            List<Registration> rgList = new List<Registration>();
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Banking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from Registration", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Registration rg = new Registration();
                rg.Account_No = Convert.ToInt64(dr["Account_NO"]);
                rg.Account_Name = Convert.ToString(dr["Account_Name"]);
                rg.Mobile_No = Convert.ToInt64(dr["Mobile_No"]);
                rg.Email = Convert.ToString(dr["Email"]);
                rg.Password = Convert.ToString(dr["Password"]);
                rg.Address = Convert.ToString(dr["Address"]);
                rg.Bank_Branch = Convert.ToString(dr["Bank_Branch"]);
                rg.IFSC = Convert.ToString(dr["IFSC"]);
                rgList.Add(rg);
            }
            cn.Close();
            return rgList;

        }
        public void GetDetails(Registration obj)
        {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Banking"].ConnectionString);
                SqlCommand cmd = new SqlCommand("sp_SaveChanges", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Account_No", obj.Account_No);
                cmd.Parameters.AddWithValue("@Account_Name", obj.Account_Name);
                cmd.Parameters.AddWithValue("@Mobile_No", obj.Mobile_No);
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                cmd.Parameters.AddWithValue("@Password", obj.Password);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@Bank_Branch", obj.Bank_Branch);
                cmd.Parameters.AddWithValue("@IFSC", obj.IFSC);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();



        }



        
    }
}