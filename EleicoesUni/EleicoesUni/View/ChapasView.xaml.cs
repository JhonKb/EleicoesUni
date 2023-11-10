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
        private ChapasViewModel viewModel;

        public ChapasView(int idTurma)
        {
            viewModel = new ChapasViewModel(idTurma);
            _ = CarregarDados();
            InitializeComponent();
        }

        public async Task CarregarDados()
        {
            var porcentagem = await viewModel.ObterPorcentagemVotosTurma();
            PorcentagemTotal.Text = porcentagem.ToString() + "% dos votos totalizados";

            var votosRestantes = await viewModel.ObterQuantidadeVotosRestantesTurma();
            VotosRestantes.Text = "(" + votosRestantes.ToString() + " votos restantes)";

            ProgressVotos.Progress = await viewModel.ObterProgressoBarra();
            
            var chapas = await viewModel.ObterChapasViewTurma();
            ChapaListView.ItemsSource = chapas;

            if (chapas.Count() == 0)
            {
                Candidatos.IsVisible = false;
                SemCandidatos.IsVisible = true;
            }
        }

        private void BTNAdicionarChapa(object sender, EventArgs e)
        {
            //TODO Inserir Token de acesso

            //TODO Validar de token

            //TODO Navegar para page de adicionar chapa
        }
    }
}