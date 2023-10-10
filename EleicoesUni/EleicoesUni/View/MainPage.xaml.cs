using EleicoesUni.Model;
using EleicoesUni.Utils;
using EleicoesUni.View;
using System;
using Xamarin.Forms;

namespace EleicoesUni
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            cursos.Items.Add("ADS");
            cursos.Items.Add("Fisioterapia");
            cursos.Items.Add("Psicologia");
        }

        public void PKRSelecionado(object sender, EventArgs e)
        {
            turmas.Items.Clear();
            turmas.Items.Add("ADS 3/4");
            turmas.Items.Add("ADS 1/2");
        }

        private void BTNEntrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TurmaView());
            Carregamento carregamento = new Carregamento();
            var tela = TelaPreta.IsVisible;
            var roda = RodaCarregamento.IsVisible;
            var giro = RodaCarregamento.IsRunning;
            carregamento.telaCarregamento(tela,roda,giro);
        }
    }
}
