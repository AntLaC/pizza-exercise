using Pizza_Exercise.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pizza_Exercise
{
    public partial class App : Application
    {
        //In actual application this would be in a settings file
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "https://localhost:5001";
        public static bool UseMockDataStore = false;
        // this was set to handle certificate issues in running web api from localhost and to use on an actual device
        public static bool UseWebApi = false;
        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
