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

            if (response.IsSuccessStatusCode)
            {
                
                await SecureStorage.SetAsync("isLoggedIn", "true");
                await SecureStorage.SetAsync("username", username);
                await SecureStorage.SetAsync("login_time", DateTime.Now.ToString());

                return true;
            }

            return false;

        }   

        public async Task<bool> IsLoggedInAsync()
        {
            var flag = await SecureStorage.GetAsync("isLoggedIn");

            var loginTimeStr = await SecureStorage.GetAsync("login_time");

            if(DateTime.TryParse(loginTimeStr, out var logintime))
            {
                if (DateTime.Now - logintime > TimeSpan.FromMinutes(20))
                {
                    Logout();
                    return false;
                }
            }

            return flag == "true";
        }

        public async Task<bool> UpdatePasswordAsync(string username, string newPassword)
        {
            var req = new { NewPassword = newPassword };

            //var response = await _http.PostAsJsonAsync($"api/Auth/updatePass/{username}", req);

            var response = await _http.PutAsJsonAsync($"api/Auth/updatePass/{username}", req);

            return response.IsSuccessStatusCode;
        }

        public void Logout()
        {
            SecureStorage.Remove("isLoggedIn");
            SecureStorage.Remove("username");

            SecureStorage.Remove("login_time");
        }
            
    }
}
