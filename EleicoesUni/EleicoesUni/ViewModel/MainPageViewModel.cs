using EleicoesUni.Model;
using EleicoesUni.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EleicoesUni.ViewModel
{
    public class MainPageViewModel
    {
        private ApiService<Turma> apiServiceTurma;
        public MainPageViewModel()
        {
            apiServiceTurma = new ApiService<Turma>();
        }

        public async Task<List<string>> ListarCursosTurma()
        {
            var cursos = new List<string>();
            var listaTurmas = await apiServiceTurma.GetObjectsAsync(true);

            if (listaTurmas != null)
            {
                foreach (var turma in listaTurmas)
                {
                    if (turma.CursoTurma != null && !cursos.Contains(turma.CursoTurma))
                    {
                        cursos.Add(turma.CursoTurma);
                    }
                }
            }

            return cursos;
        }
        public async Task<List<string>> ListarNomeTurmas(string curso)
        {
            var turmas = new List<string>();
            var listaTurmas = await apiServiceTurma.GetObjectsAsync(true);

            if (listaTurmas != null)
            {
                foreach (var turma in listaTurmas)
                {
                    if (turma.CursoTurma.Equals(curso) && turma.NomeTurma != null)
                    {
                        turmas.Add(turma.NomeTurma);
                    }
                }
            }

            return turmas;
        }

        public async Task<Turma> SelecionarTurma(string nomeTurma)
        {
            var turma = new Turma();
            var listaTurmas = await apiServiceTurma.GetObjectsAsync(true);

            if (listaTurmas != null)
            {
                foreach (var t in listaTurmas)
                {
                    if (t.NomeTurma.Equals(nomeTurma) && t.NomeTurma != null)
                    {
                        turma = t;
                        break;
                    }
                }
            }

            return turma;
        }
    }
}
