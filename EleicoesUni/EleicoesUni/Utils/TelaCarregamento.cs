using Xamarin.Forms;

namespace EleicoesUni.Utils
{
    public class TelaCarregamento
    {
        //Variável de controle
        bool _carregando;
        BoxView tela;
        ActivityIndicator roda;

        public TelaCarregamento(BoxView tela, ActivityIndicator roda)
        {
            this.tela = tela;
            this.roda = roda;
        }

        public void CarregarTela()
        {
            //Mudando estado da tela de carregamento
            if (_carregando)
            {
                tela.IsVisible = false;
                roda.IsVisible = false;
                roda.IsRunning = false;

                _carregando = false;
            }
            else
            {
                tela.IsVisible = true;
                roda.IsVisible = true;
                roda.IsRunning = true;

                _carregando = true;
            }
        }
    }
}
