using EleicoesUni.Model;
using EleicoesUni.Services;
using EleicoesUni.View;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EleicoesUni.ViewModel
{
    public class CofirmaVotoViewModel:GenericViewModel
    {
        private Chapa _chapa;
        private int _votante;

        public int IdChapa
        {
            get => _chapa.Id;
        }

        private string _nomeLider;
        public string NomeLider
        {
            get => _nomeLider;
            private set
            {
                _nomeLider = value;
                OnPropertyChanged(nameof(NomeLider));
            }
        }

        private string _nomeVice;
        public string NomeVice
        {
            get => _nomeVice;
            private set
            {
                _nomeVice = value;
                OnPropertyChanged(nameof(NomeVice));
            }
        }

        public ICommand Confirm { get; private set; }
        public ICommand Cancel { get; private set; }

        public CofirmaVotoViewModel(int votante, Chapa chapa)
        {
            _chapa = chapa;
            _votante = votante;
            _=GetDados();
            Confirm = new Command(async () => await ConfirmVoto());
            Cancel = new Command(async () => await CancelConfirmVoto());
        }

        private async Task GetDados()
        {
            var lider = await ApiServiceAluno.GetObjectAsync(_chapa.IdLider);
            NomeLider = lider.NomeAluno;

            var vice = await ApiServiceAluno.GetObjectAsync(_chapa.IdVice);
            NomeVice = vice.NomeAluno;
        }
       
        public async Task ConfirmVoto()
        {
            try
            {
                DependencyService.Get<ILodingPageService>().ShowLoadingPage();
                if (await ApiServiceVoto.AddObjectAsync(new Voto(_votante, _chapa.Id)))
                {
                    new TurmaView(await ApiServiceTurma.GetObjectAsync(_chapa.IdTurma));
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Voto confirmado com sucesso!", "OK");
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    
                    DependencyService.Get<ILodingPageService>().HideLoadingPage();
                }
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Ocorreu um erro inesperado!", "OK");
            }
        }

        public async Task CancelConfirmVoto()
        {
            try
            {
                DependencyService.Get<ILodingPageService>().ShowLoadingPage();
                await Application.Current.MainPage.Navigation.PopModalAsync();
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
            }
            catch
            {
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                await Application.Current.MainPage.DisplayAlert("Erro", "Ocorreu um erro inesperado!", "OK");
            }
        }
    }
}
