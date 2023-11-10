using EleicoesUni.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EleicoesUni.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VotoView : ContentPage
    {
        public VotoView(int idTurma)
        {
            InitializeComponent();
            BindingContext = new VotoViewModel(idTurma);
        }
    }
}