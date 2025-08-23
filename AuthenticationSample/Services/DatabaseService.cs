using AuthenticationSample.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace AuthenticationSample.Services
{
    public class DatabaseService
    {

        private readonly string connectionString = "Data Source=192.168.1.22;Initial Catalog=ATCHRMLOCAL;User ID=ritesh;Password=ritesh;TrustServerCertificate=True;";
     

       public async Task<List<EmpDetail>> FetchEmpDetails()
        {
            List<EmpDetail> empDetails = new List<EmpDetail>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("SELECT EmpId, EmpName,Status FROM EmpDetails", connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                empDetails.Add(new EmpDetail
                                {
                                    EmpId = reader.GetDecimal(0),
                                    EmpName = reader.GetString(1),
                                    Status=reader.GetString(2),

                                });
                            }
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }


            return empDetails;
        }

    }
}
