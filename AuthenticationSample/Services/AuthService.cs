using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationSample.Services
{
    internal class AuthService
    {
        //private readonly HttpClient _http = new() { BaseAddress = new Uri("http://10.0.2.2:2030/") };
        private readonly HttpClient _http = new() { BaseAddress = new Uri("http://192.168.1.22:2030/") };
        public  async Task<bool> LoginAsync(string username, string password)
        {
            var req = new { Username = username, Password = password };

            var response = await _http.PostAsJsonAsync("api/Auth/login", req);

            return response.IsSuccessStatusCode;

        }   
    }
}
