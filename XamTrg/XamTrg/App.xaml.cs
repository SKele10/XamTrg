using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamTrg
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MVVMexercise.Views.Users();
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
