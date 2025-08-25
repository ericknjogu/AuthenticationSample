using AuthenticationSample.Models;
using AuthenticationSample.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationSample.ViewModels
{
    public partial class EmployeesViewModel:ObservableObject
    {

        private readonly DatabaseService _dbService;


        [ObservableProperty]
        private ObservableCollection<EmpDetail> employees = new ();

        [ObservableProperty]
        private EmpDetail selectedEmployee;


        [ObservableProperty]
        private ObservableCollection<EmpDetail> location = new();

        [ObservableProperty]
        private EmpDetail selectedLocation;

        [ObservableProperty]
        private ObservableCollection<EmpDetail> employeeDetails = new();



        public  EmployeesViewModel()
        {
           
            _dbService = new DatabaseService();

            // Load employees when the ViewModel is instantiated
            LoadLocationAsync().ConfigureAwait(false);

            this.PropertyChanged += EmployeesViewModel_PropertyChanged;
        }

        private async void EmployeesViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedLocation))
            {
                await LoadEmpIDAsync(SelectedLocation.LocationID);

                Console.WriteLine($"selected item is {SelectedLocation}");
            }
            else if (e.PropertyName == nameof(SelectedEmployee))
            {
                Console.WriteLine($"selected employee is {SelectedEmployee}");
            }
        }
       
        public async Task LoadEmpIDAsync(int locationID)
        {
            var empdetails = await _dbService.FetchEmpId(locationID);

            Employees.Clear();

            foreach (var emp in empdetails)
            {
                Employees.Add(emp);
            }

        }

        public async Task LoadLocationAsync()
        {
            var locations = await _dbService.FetchLocation();

            Location.Clear();

            foreach (var loc in locations)
            {
                Location.Add(loc);
            }

        }

        [RelayCommand]
        public async Task LoadDetails()
        {
            if (SelectedEmployee == null || SelectedLocation == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please select both Employee and Location.", "OK");
                return;
            }
            try
            {
                var details = await _dbService.LoadDetails( SelectedLocation.LocationID, SelectedEmployee.EmpId);
                if (details.Count == 0)
                {
                    await App.Current.MainPage.DisplayAlert("Info", "No details found for the selected employee", "OK");
                    return;
                }

                EmployeeDetails.Clear();
                foreach (var det in details)
                {
                    EmployeeDetails.Add(det);

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"An error occurred while loading details: {ex.Message}", "OK");
            }
        }
    }
}
