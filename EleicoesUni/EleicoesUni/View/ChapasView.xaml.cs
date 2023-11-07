using EleicoesUni.ViewModel;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EleicoesUni.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChapasView : ContentPage
    {
        private ChapasViewModel viewModel;
        private int idTurma;

        public ChapasView(int idTurma)
        {
            this.idTurma = idTurma;
            CarregarDados();
            InitializeComponent();
        }

        public async void CarregarDados()
        {
            viewModel = new ChapasViewModel(idTurma);

            var porcentagem = await viewModel.ObterPorcentagemVotosTurma();
            PorcentagemTotal.Text = porcentagem.ToString() + "% dos votos totalizados";

            var votosRestantes = await viewModel.ObterQuantidadeVotosRestantesTurma();
            VotosRestantes.Text = "(" + votosRestantes.ToString() + " votos restantes)";

            ProgressVotos.Progress = await viewModel.ObterProgressoBarra();
            
            var chapas = await viewModel.ObterChapasTurma();
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