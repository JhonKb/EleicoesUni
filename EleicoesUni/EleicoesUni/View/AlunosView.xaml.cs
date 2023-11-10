using EleicoesUni.ViewModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EleicoesUni.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlunosView : ContentPage
    {
        public AlunosView(int idTurma)
        {
            InitializeComponent();

            BindingContext = new AlunosViewModel(idTurma);
        }
    }
}