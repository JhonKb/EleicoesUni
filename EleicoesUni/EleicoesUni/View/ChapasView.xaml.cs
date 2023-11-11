using EleicoesUni.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EleicoesUni.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChapasView : ContentPage
    { 
        public ChapasView(int idTurma)
        {
            BindingContext = new ChapasViewModel(idTurma);
            InitializeComponent();
        }

        private void BTNAdicionarChapa(object sender, EventArgs e)
        {
            //TODO Inserir Token de acesso

            //TODO Validar de token

            //TODO Navegar para page de adicionar chapa
        }
    }
}