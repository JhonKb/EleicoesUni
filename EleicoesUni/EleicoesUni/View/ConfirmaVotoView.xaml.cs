using EleicoesUni.Model;
using EleicoesUni.ViewModel;
using Xamarin.Forms;

namespace EleicoesUni
{
    public partial class ConfirmaVotoView : ContentPage
    {
        public ConfirmaVotoView(int votante, Chapa chapa)
        {
            BindingContext = new CofirmaVotoViewModel(votante, chapa);
            InitializeComponent();
        }
    }
}