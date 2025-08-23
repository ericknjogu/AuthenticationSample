using AuthenticationSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationSample.Services
{
    internal class DbService
    {
        private readonly HttpClient _http = new() { BaseAddress = new Uri("http://192.168.1.22:2030/") };

        public async Task<List<EmpDetail>> FethEmployeesAsync()
        {
            try
            {
                var response = await _http.GetAsync("api/Home/employees");
                if (response.IsSuccessStatusCode)
                {
                    var employeeDetails = await response.Content.ReadFromJsonAsync<List<EmpDetail>>();
                    return employeeDetails ?? new List<EmpDetail>();
                }
                else
                {
                    throw new Exception("Failed to fetch employee details.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching employee details: " + ex.Message);
            }
        }
    }
}
