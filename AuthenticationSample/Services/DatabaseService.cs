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
                                    EmpId = reader.GetInt32(0),
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


        public async Task<List<EmpDetail>> LoadDetails(int locid )
        {
            List<EmpDetail> empDetails = new List<EmpDetail>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(string.Format(@"SELECT LocationName location,EmpId, EmpName,Status FROM EmpDetails
                                                                        where locationID = {0}",locid), connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                empDetails.Add(new EmpDetail
                                {
                                    LocationName =reader.GetString(0),
                                    EmpId = reader.GetInt32(1),
                                    EmpName = reader.GetString(2),
                                    Status = reader.GetString(3),

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

        //fetch empids to populate picker
        public async Task<List<EmpDetail>> FetchEmpId(int location)
        {
            List<EmpDetail> empIds = new List<EmpDetail>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(string.Format("SELECT EmpId,EmpName FROM EmpDetails where locationID = '{0}' ", location), connection))
                    {
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                empIds.Add(new EmpDetail
                                {
                                    EmpId = reader.GetInt32(0),
                                    EmpName = reader.GetString(1),
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

            return empIds;
        }

        public async Task<List<EmpDetail>> FetchLocation()
        {
            List<EmpDetail> locations = new List<EmpDetail>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("SELECT distinct LocationID, LocationName FROM EmpDetails ", connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                locations.Add(new EmpDetail
                                {
                                    LocationID = reader.GetInt32(0),
                                    LocationName = reader.GetString(1),

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

            return locations;
        }


        //save in Database
        public async Task<int> SaveQrCode(string qrcode, DateTime savedate)
        {
            if (!string.IsNullOrEmpty(qrcode))
            {

                try
                {
                    int r = 0;


                    using (var connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (var transaction = connection.BeginTransaction())
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandText = "INSERT INTO Qrcodes_tbl VALUES (@QrCode, @SaveDate)";
                            command.Parameters.AddWithValue("@QrCode", qrcode);
                            command.Parameters.AddWithValue("@SaveDate", savedate);

                            try
                            {

                                r = await command.ExecuteNonQueryAsync();

                                await transaction.CommitAsync();

                                return r;

                            }
                            catch
                            {
                                transaction.Rollback();
                                return 0;

                            }
                        }
                    }

                }
                catch (Exception)
                {
                    return 0;
                    throw;

                }
            }
            else
            {
                return 0;
            }
        }


    }
}
