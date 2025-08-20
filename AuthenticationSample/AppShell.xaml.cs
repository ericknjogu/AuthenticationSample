using AuthenticationSample.Views;

namespace AuthenticationSample
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
           Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));

        }
    }
}
