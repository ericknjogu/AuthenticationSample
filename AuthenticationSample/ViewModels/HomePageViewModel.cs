using AuthenticationSample.Models;
using AuthenticationSample.Services;
using AuthenticationSample.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AuthenticationSample.ViewModels
{
    public partial class HomePageViewModel: ObservableObject

    {
        //private readonly DbService _dbService;
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private ObservableCollection<EmpDetail> employees  = new ();
        public HomePageViewModel()
        {
            //_dbService = new DbService();
            _dbService = new DatabaseService();
        }

        [RelayCommand]
        public async Task  FetchEmployees()
        {

            //var empdetails = await _dbService.FethEmployeesAsync();
            var empdetails = await _dbService.FetchEmpDetails();
            Employees.Clear();

            foreach (var emp in empdetails)
            {
                Employees.Add(emp);
            }
        }

        [RelayCommand]

        public async void Logout()
        {
            var authservice = new AuthService();

            var response = await App.Current.MainPage.DisplayAlert("Logout", "Continue to Logout? .", "OK","Cancel");

            if (response)
            {
                authservice.Logout();
                Employees.Clear();  
            }
            else
            {
                return;
            }



                await Shell.Current.GoToAsync("///LoginPage");
        }
    }
}
