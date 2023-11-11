using EleicoesUni.ViewModel;
using Xamarin.Forms;

namespace EleicoesUni
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var viewModel = new MainPageViewModel();
            BindingContext = viewModel;

            //Inicializando página
            InitializeComponent();
        }
    }
}
