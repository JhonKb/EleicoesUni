using EleicoesUni.Model;
using EleicoesUni.Service;
using EleicoesUni.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EleicoesUni.ViewModel
{
    public class MainPageViewModel:GenericViewModel
    {
        public ObservableCollection<string> Cursos { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Turmas { get; } = new ObservableCollection<string>();
        
        private string _cursoSelecionado;
        public string CursoSelecionado
        {
            get => _cursoSelecionado;
            set
            {
                if (_cursoSelecionado != value)
                {
                    _cursoSelecionado = value;
                    if (!string.IsNullOrEmpty(_cursoSelecionado))
                    {
                        _=LoadTurmas(_cursoSelecionado);
                    }
                    OnPropertyChanged(nameof(CursoSelecionado));
                }
            }
        }

        private string _turmaSelecionada;
        public string TurmaSelecionada
        {
            get => _turmaSelecionada;
            set
            {
                _turmaSelecionada = value;
                OnPropertyChanged(nameof(TurmaSelecionada));
            }
        }

        public ICommand Entrar { get; private set; }

        public MainPageViewModel()
        {
            _=LoadCursos();
            Entrar = new Command(async () => await LoadNextPage());
        }

        private async Task LoadCursos()
        {
            var turmas = await ApiServiceTurma.GetObjectsAsync(true);
            if (turmas.Count() > 0)
            {
                var cursos = turmas.Select(t => t.CursoTurma).Distinct();
                foreach (var curso in cursos)
                {
                    Cursos.Add(curso);
                }                  
            }
        }

        private async Task LoadTurmas(string curso)
        {
            Turmas.Clear();
            var turmas = await ApiServiceTurma.GetObjectsAsync(true);

            if (turmas.Count() > 0)
            {
                var turmasFiltradas = turmas.Where(t => t.CursoTurma == curso);
                foreach (var turma in turmasFiltradas)
                {                   
                    Turmas.Add(turma.NomeTurma);                   
                }
            }
        }

        private async Task LoadNextPage()
        {
            try
            {
                DependencyService.Get<ILodingPageService>().ShowLoadingPage();

                if (!string.IsNullOrEmpty(_cursoSelecionado) && !string.IsNullOrEmpty(_turmaSelecionada))
                {
                    var turmaSelecionada = await SelectTurma(_turmaSelecionada);

                    await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new TurmaView(turmaSelecionada)));
                    
                    CursoSelecionado = string.Empty;
                    TurmaSelecionada = string.Empty;
                    
                    DependencyService.Get<ILodingPageService>().HideLoadingPage();
                }
                else
                {
                    DependencyService.Get<ILodingPageService>().HideLoadingPage();
                    await Application.Current.MainPage.DisplayAlert("Erro", "Selecione uma turma válida!", "OK");
                }
            } catch
            {
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                await Application.Current.MainPage.DisplayAlert("Erro", "Ocorreu um erro inesperado!", "OK");
            }        
        }

        private async Task<Turma> SelectTurma(string nomeTurma)
        {
            var turmas = await ApiServiceTurma.GetObjectsAsync(true);

            return turmas.FirstOrDefault(t => t.NomeTurma.Equals(nomeTurma));
        }
    }
}
