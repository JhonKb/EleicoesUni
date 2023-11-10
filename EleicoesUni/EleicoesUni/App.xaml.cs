using EleicoesUni.Services;
using EleicoesUni.View;
using Xamarin.Forms;

namespace EleicoesUni
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            DependencyService.Get<ILodingPageService>();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
