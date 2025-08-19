using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationSample.Services;
using AuthenticationSample.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AuthenticationSample.ViewModels
{
    
    public partial class LoginViewModel: ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        public LoginViewModel()
        {
        }

        [RelayCommand]
        private async void Login()
        {
            DatabaseService dal = new DatabaseService();

            DataTable dt =  dal.GetUsers(username, password);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Username and password cannot be empty.", "OK");
                return;
            }

            if(username == dt.Rows[0]["Username"].ToString() && password == dt.Rows[0]["Password"].ToString())
            {
                await App.Current.MainPage.DisplayAlert("Success", "Login successful!", "OK");
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
    }
}
