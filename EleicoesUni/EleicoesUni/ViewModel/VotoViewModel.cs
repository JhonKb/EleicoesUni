using EleicoesUni.Model;
using EleicoesUni.Service;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EleicoesUni.ViewModel
{
    public class VotoViewModel:GenericViewModel
    {
        public ObservableCollection<string> Chapas { get; } = new ObservableCollection<string>();

        private string _chapaSelecionada;
        public string ChapaSelecionada
        {
            get => _chapaSelecionada;
            set
            {
                _chapaSelecionada = value;
                OnPropertyChanged(nameof(ChapaSelecionada));
            }
        }

        private string _nomeAluno;
        public string NomeAluno
        {
            get => _nomeAluno;
            set
            {
                if (_nomeAluno != value)
                {
                    _nomeAluno = value;
                    OnPropertyChanged(nameof(NomeAluno));
                }
            }
        }

        private string _matricula;

        public string Matricula
        {
            get => _matricula;
            set
            {
                if (_matricula != value)
                {
                    _matricula = value;
                    OnPropertyChanged(nameof(Matricula));
                }
                OnPropertyChanged(nameof(IsVisibleMatricula));
            }
        }

        public bool IsVisibleMatricula
        {
            get => !ProcessMatricula(Matricula);
        } 

        public ICommand Avancar { get; private set; }

        private int _idTurma;

        public VotoViewModel(int idTurma)
        {
            _idTurma = idTurma;
            _=LoadChapas();
            Avancar = new Command(async () => await LoadNextPage());
        }

        private async Task LoadNextPage()
        {
            try
            {
                DependencyService.Get<ILodingPageService>().ShowLoadingPage();
                if (!string.IsNullOrEmpty(_chapaSelecionada) &&  !string.IsNullOrEmpty(_nomeAluno) && !string.IsNullOrEmpty(_matricula))
                {
                    if (ProcessMatricula(Matricula))
                    {
                        if (await ValidationAluno())
                        {
                            if (!await ValidationVoto())
                            {
                                var chapaSeparator = ChapaSelecionada.Split(' ', '_');
                                var chapa = await ApiServiceChapa.GetObjectAsync(Int32.Parse(chapaSeparator[0]));

                                var votante = await GetAluno();

                                await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ConfirmaVotoView(votante.Id, chapa)));
                                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                            }
                            else
                            {
                                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                                await Application.Current.MainPage.DisplayAlert("Erro", "Aluno já votou!", "OK");
                            }
                        }
                        else
                        {
                            DependencyService.Get<ILodingPageService>().HideLoadingPage();
                            await Application.Current.MainPage.DisplayAlert("Erro", "Aluno não pertence a turma ou dados não correspondem!", "OK");
                        }
                    }
                    else
                    {
                        DependencyService.Get<ILodingPageService>().HideLoadingPage();
                        await Application.Current.MainPage.DisplayAlert("Erro", "Matrícula Inválida!", "OK");
                    }
                }
                else
                {
                    DependencyService.Get<ILodingPageService>().HideLoadingPage();
                    await Application.Current.MainPage.DisplayAlert("Erro", "Preencha os campos!", "OK");
                }
            } catch 
            {
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                await Application.Current.MainPage.DisplayAlert("Erro", "Ocorreu um erro inesperado!", "OK");
            }
        }

        private async Task<Aluno> GetAluno()
        {
            var alunos = await ApiServiceAluno.GetObjectsAsync(true);
            if (alunos != null)
            {
                return alunos.FirstOrDefault(a => a.NomeAluno == NomeAluno && a.MatriculaAluno == Matricula && a.IdTurma == _idTurma);
            }
            return null;
        }

        private async Task<bool> ValidationAluno()
        {
            var votante = await GetAluno();
            if (votante != null)
            {
                return true;
            }
        
            return false;
        }

        private async Task<bool> ValidationVoto()
        {
            var votante = await GetAluno();
            var votos = await ApiServiceVoto.GetObjectsAsync(true);
            var votoExiste = votos.FirstOrDefault(v => v.IdVotante == votante.Id);

            if (votoExiste != null)
            {
                return true;
            }
            return false;
        }

        private async Task LoadChapas()
        {
            var chapas = await ApiServiceChapa.GetObjectsAsync(true);
            if (chapas != null)
            {
                var chapasFiltradas = chapas.Where(c => c.IdTurma == _idTurma);
                foreach (var chapa in chapasFiltradas)
                {
                    var aluno = await ApiServiceAluno.GetObjectAsync(chapa.IdLider);
                    var c = chapa.Id.ToString() + " - " + aluno.NomeAluno.ToString();
                    Chapas.Add(c);
                }
            }
        }

        private bool ProcessMatricula(string matricula)
        {
            return matricula != null && matricula.Length == 8 && matricula[0] != '0';
        }
    }
}
