using Microsoft.Data.SqlClient;
using System.Data;


namespace AuthenticationSample.Services
{
    public class DatabaseService
    {

        public SqlConnection con = new SqlConnection("Data Source=192.168.1.250;Initial Catalog=ATCHRM;User ID=sa;Password=admin_123;TrustServerCertificate=True;");
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
           
            try
            {
                string q = string.Format("select * from UsersMaster_tbl where Empid = {0} and Password = '{1}'", username, password);
                return NewMethod(q);
            }
            catch (Exception ex)
            {
                return null;
            }
      
           
        }

    }
}
