using EleicoesUni.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EleicoesUni.ViewModel
{
    public class ChapasViewModel : GenericViewModel
    {
        public ObservableCollection<ChapaInfoModel> ChapasInfo { get; } = new ObservableCollection<ChapaInfoModel>();

        private string _porcentagemTotal;
        public string PorcentagemTotal
        {
            get => _porcentagemTotal;
            private set
            {
               if (value != _porcentagemTotal)
               {
                    _porcentagemTotal = value;
               }
               OnPropertyChanged(nameof(PorcentagemTotal));
            }
        }

        private string _qtdVotosRestantes;
        public string QtdVotosRestantes
        {
            get => _qtdVotosRestantes;
            private set
            {
                if (_qtdVotosRestantes != value)
                {
                    _qtdVotosRestantes = value;
                }
                OnPropertyChanged(nameof(QtdVotosRestantes));
            }
        }

        private float _progressBar;
        public float ProgressBar
        {
            get => _progressBar;
            private set
            {
                if (_progressBar != value)
                {
                    _progressBar = value;
                }
                OnPropertyChanged(nameof(ProgressBar));
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing 
        {
            get => _isRefreshing; 
            private set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                }
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand Refresh { get; private set; }

        public bool IsEmpty
        {
            get => ChapasInfo.Count() > 0;
        }

        private IEnumerable<Chapa> Chapas { get; set; }

        private string _nomeLider;

        private string _nomeVice;

        private string _porcentagem;

        private int _qtdVotos;

        private int _idTurma;

        private int _qtdAlunos;

        private int _qtdVotosTotais;

        public ChapasViewModel(int idTurma)
        {
            _idTurma = idTurma;
            _= LoadDados();
            Refresh = new Command(async () =>
            {
                await LoadDados();
                IsRefreshing = false;
            });
        }
        
        public async Task LoadDados()
        {
            ChapasInfo.Clear();
            await GetChapas();
            await GetDadosTotais();
            if (Chapas.Count() > 0)
            {
                foreach (var chapa in Chapas)
                {
                    await GetNomeAlunos(chapa.IdLider, chapa.IdVice);
                    await GetStatisticsChapas(chapa.Id);
                    
                    ChapasInfo.Add(new ChapaInfoModel(chapa.Id, _nomeLider, _nomeVice, _porcentagem, _qtdVotos));
                }
            }
        }
        private async Task GetChapas()
        {

            var chapas = await ApiServiceChapa.GetObjectsAsync(true);
            Chapas = chapas.Where(c => c.IdTurma == _idTurma);
        }

        public async Task GetDadosTotais()
        {
            await GetQtdAlunos();
            await GetQtdVotosTotais();

            //Calculando porcentagem
            var porcentagem = 0;            
            if (_qtdVotosTotais > 0)            
                porcentagem = (_qtdVotosTotais * 100) / _qtdAlunos;    
            
            //Calculando métrica da barra de progresso
            if (porcentagem > 0)
                ProgressBar = (float)porcentagem / 100;

            PorcentagemTotal = porcentagem.ToString() + "%";

            //Calculando votos restantes
            var qtdRestante = 0;
            if (_qtdAlunos > 0)           
                qtdRestante = _qtdAlunos - _qtdVotosTotais;
          
            QtdVotosRestantes = "(" + qtdRestante.ToString() + " votos restantes)";
        }

        private async Task GetQtdAlunos()
        {
            var alunos = await ApiServiceAluno.GetObjectsAsync(true);
            if (alunos.Count() > 0)
            {
                var alunosFiltrados = alunos.Where(a => a.IdTurma == _idTurma);
                _qtdAlunos = alunosFiltrados.Count();
            }
        }

        private async Task GetQtdVotosTotais()
        {
            _qtdVotosTotais = 0;
            foreach (var chapa in Chapas)
            {
                await GetQtdVotos(chapa.Id);
                _qtdVotosTotais += _qtdVotos;
            }
        }

        private async Task GetNomeAlunos(int idLider, int idVice)
        {
            var lider = await ApiServiceAluno.GetObjectAsync(idLider);
            _nomeLider = lider.NomeAluno;

            var vice = await ApiServiceAluno.GetObjectAsync(idVice);
            _nomeVice = vice.NomeAluno;
        }

        public async Task GetStatisticsChapas(int idChapa)
        {
            await GetQtdVotos(idChapa);
            var p = 0;
            if (_qtdVotos > 0)
                p = (_qtdVotos * 100) / _qtdVotosTotais;
            
            _porcentagem = p.ToString() + "%";
        }

        private async Task GetQtdVotos(int idChapa)
        {
            var votos = await ApiServiceVoto.GetObjectsAsync(true);
            if (votos.Count() > 0)
            {
                var votosFiltrados = votos.Where(v => v.IdChapa == idChapa);
                _qtdVotos = votosFiltrados.Count();
            }
        }      
    }

    public class ChapaInfoModel
    {
        public int Id { get; set; }
        public string NomeLider { get; set; }
        public string NomeVice { get; set; }
        public string PorcentagemVotos { get; set; }
        public int QuantidadeVotos { get; set; }

        public ChapaInfoModel(int id, string lider, string vice, string porcentagem, int quantidadeVotos)
        {
            Id = id;
            NomeLider = lider;
            NomeVice = vice;
            PorcentagemVotos = porcentagem;
            QuantidadeVotos = quantidadeVotos;
        }
    }
}
