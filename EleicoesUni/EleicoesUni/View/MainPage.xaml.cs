using EleicoesUni.Utils;
using EleicoesUni.View;
using EleicoesUni.ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EleicoesUni
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;
        private TelaCarregamento telaCarregamento;
        public MainPage()
        {
            //Inicializando página
            InitializeComponent();
            viewModel = new MainPageViewModel();

            //Inicializa a classe da tela de carregamento
            telaCarregamento = new TelaCarregamento(TelaPreta, Roda);

            //Adicionando cursos ao seletor de cursos
            CarregarCursos();
        }

        public async void CarregarCursos()
        {
            var cursosTurma = await viewModel.ListarCursosTurma();

            foreach (var curso in cursosTurma)
            {
                cursos.Items.Add(curso);
            }
        }

        public async void CarregarTurmas(string curso)
        {
            var nomeTurmas = await viewModel.ListarNomeTurmas(curso);

            foreach (var turma in nomeTurmas)
            {
                turmas.Items.Add(turma);
            }
        }

        //Método que atualiza o seletor de turma de acordo com o curso selecionado
        public void PKRSelecionado(object sender, EventArgs e)
        {
            turmas.Items.Clear();
            CarregarTurmas(cursos.SelectedItem.ToString());
        }

        //Ação do botão principal de entrada
        private async void BTNEntrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Ativando a tela de carregamento
                telaCarregamento.CarregarTela();

                //Verifica se turma esta selecionada
                if (turmas.SelectedItem != null && cursos.SelectedItem != null)
                {
                    //Atribui a turma selecionada
                    var turma = await viewModel.SelecionarTurma(turmas.SelectedItem.ToString());

                    cursos.SelectedItem = string.Empty;
                    turmas.SelectedItem = string.Empty;

                    //Navegando para a próxima página
                    await Navigation.PushModalAsync(new NavigationPage(new TurmaView(turma)));
                    telaCarregamento.CarregarTela();
                }
                else
                {
                    telaCarregamento.CarregarTela();
                    await DisplayAlert("Erro", "Selecione uma turma válida!", "OK");
                }
            } catch
            {
                telaCarregamento.CarregarTela();
                await DisplayAlert("Erro", "Ocorreu um erro ineperado!", "OK");
            } 
        }
    }
}
