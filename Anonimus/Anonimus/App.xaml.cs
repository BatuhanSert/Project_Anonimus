using Anonimus.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Anonimus
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PasswordPage());
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
