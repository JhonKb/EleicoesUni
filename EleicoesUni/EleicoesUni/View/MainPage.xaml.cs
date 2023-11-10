using EleicoesUni.ViewModel;
using Xamarin.Forms;

namespace EleicoesUni
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //Inicializando página
            InitializeComponent();

            var viewModel = new MainPageViewModel();
            

            BindingContext = viewModel;
        }
    }
}
