using System.ComponentModel;
using System;
using System.Data.SqlClient;

namespace MainRepository.App_Code
{
    public static class Utility
    {
        public static SqlConnection CreateMSSQLConnection()
        {
            SqlConnection cnn = new SqlConnection("Data Source=***;Initial Catalog=dbDatabase;Persist Security Info=True;User ID=userName;Password=passWord");
            cnn.Open();
            return cnn;
        }

        public static void GetAll()
        {

            SqlConnection cnn = CreateMSSQLConnection();
            SqlCommand cmd = new SqlCommand(@"Select Id, Name From Unit Where 1=1", cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Convert.ToInt16(rdr["Id"]);
                Convert.ToString(rdr["Name"]);
            }

            rdr.Close();
            cnn.Close();
        }

        //internal static MySqlConnection CreateMySQLConnection()
        //{
        //    MySqlConnection cnn = new MySqlConnection("Server=localhost;Database=blog;Uid=root;Pwd='';Encrypt=false;AllowUserVariables=True;UseCompression=True;chartset=utf8");
        //    cnn.Open();
        //    return cnn;
        //}
    }
}