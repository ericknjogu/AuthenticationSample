using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationSample.Services
{
    public class DatabaseService
    {

        public SqlConnection con = new SqlConnection("Data Source=DESKTOP-SCLSUI7;Initial Catalog=ATCHRMLOCAL;User ID=ritesh;Password=ritesh");
        DataTable dt = null;
        SqlCommand cmd = null;


        public DataTable NewMethod(string q)
        {
            DataTable dtGet = new DataTable();
            cmd = new SqlCommand(q, con);
            cmd.CommandTimeout = 50000;
            new SqlDataAdapter(cmd).Fill(dtGet);
            return dtGet;
        }

        public DataTable GetUsers(string username,string password)
        {
            string q = string.Format("select * from UserLogin where Username = {0} and Password={1}",username,password);
            try
            {
                con.Open();
                dt = NewMethod(q);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
               
                    con.Close();
               
            }
            return dt;
        }

    }
}
