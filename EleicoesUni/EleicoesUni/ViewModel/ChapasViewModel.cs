using System.Collections.Generic;
using System.Threading.Tasks;

namespace EleicoesUni.ViewModel
{
    public class ChapasViewModel:GenericViewModel
    {
        private int idTurma;

        public ChapasViewModel(int idTurma):base()
        {
            this.idTurma = idTurma;
        }

        public async Task<IEnumerable<ChapaInfoModel>> ObterChapasViewTurma()
        {
            var chapasTurma = new List<ChapaInfoModel>();

            var chapas = await ApiServiceChapa.GetObjectsAsync(true);

            if (chapas != null)
            {
                foreach (var chapa in chapas)
                {
                    if (chapa.IdTurma == idTurma)
                    {
                        var votos = await ObterQuantidadeVotosChapa(chapa.Id);

                        var p = await ObterPorcentagemVotosChapa(chapa.Id);
                        var porcentagem = p.ToString() + "%";

                        var lider = await ObterNomeAluno(chapa.IdLider);
                        var vice = await ObterNomeAluno(chapa.IdVice);

                        chapasTurma.Add(new ChapaInfoModel(chapa.Id, lider, vice, porcentagem, votos));
                    }
                }
            }

            return chapasTurma;
        }

        private async Task<string> ObterNomeAluno(int idAluno)
        {
            var aluno = await ApiServiceAluno.GetObjectAsync(idAluno);

            var nomeAluno = aluno.NomeAluno;

            return nomeAluno;
        }

        private async Task<int> ObterQuantidadeAlunosTurma()
        {
            int alunosTurma = 0;
            var alunos = await ApiServiceAluno.GetObjectsAsync(true);

            if (alunos != null)
            {
                foreach (var aluno in alunos)
                {
                    if (aluno.IdTurma == idTurma)
                    {
                        alunosTurma++;
                    }
                }
            }

            return alunosTurma;
        }

        private async Task<int> ObterQuantidadeVotosChapa(int idChapa)
        {
            int votosChapa = 0;
            var votos = await ApiServiceVoto.GetObjectsAsync(true);

            if (votos != null)
            {
                foreach (var voto in votos)
                {
                    if (voto.IdChapa == idChapa)
                    {
                        votosChapa++;
                    }
                }
            }

            return votosChapa;
        }

        private async Task<int> ObterQuantidadeVotosTurma()
        {
            var soma = 0;
            var chapas = await ApiServiceChapa.GetObjectsAsync(true);

            if (chapas != null)
            {
                foreach (var chapa in chapas)
                {
                    if (chapa.IdTurma == idTurma)
                    {
                        soma += await ObterQuantidadeVotosChapa(chapa.Id);
                    }
                }
            }

            return soma;
        }

        public async Task<double> ObterPorcentagemVotosChapa(int idChapa)
        {
            double porcentagem = 0;
            var votosTurma = await ObterQuantidadeVotosTurma();
            var votosChapa = await ObterQuantidadeVotosChapa(idChapa);

            if (votosChapa > 0)
            {
                porcentagem = (votosChapa * 100) / votosTurma;
            }

            return porcentagem;
        }

        public async Task<double> ObterPorcentagemVotosTurma()
        {
            var qtdAlunos = await ObterQuantidadeAlunosTurma();
            var qtdVotos = await ObterQuantidadeVotosTurma();
            var porcentagem = 0;

            if (qtdVotos > 0)
            {
                porcentagem = (qtdVotos * 100) / qtdAlunos;
            }

            return porcentagem;
        }

        public async Task<double> ObterProgressoBarra()
        {
            double progresso = 0;
            var porcentagem = await ObterPorcentagemVotosTurma();

            if (porcentagem > 0)
            {
                progresso = porcentagem / 100;
            }

            return progresso;
        }

        public async Task<int> ObterQuantidadeVotosRestantesTurma()
        {
            var votosTurma = await ObterQuantidadeVotosTurma();
            var alunosTurma = await ObterQuantidadeAlunosTurma();
            var votosRestantes = alunosTurma;

            if (alunosTurma > 0)
            {
                votosRestantes = alunosTurma - votosTurma;
            }

            return votosRestantes;
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
