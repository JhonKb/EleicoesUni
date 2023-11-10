using EleicoesUni.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EleicoesUni.ViewModel
{
    public class AlunosViewModel:GenericViewModel
    {
        public ObservableCollection<Aluno> Alunos { get; } = new ObservableCollection<Aluno>();

        private int idTurma;
        public AlunosViewModel(int idTurma)
        {
            this.idTurma = idTurma;
            _=LoadAlunos();
        }

        public async Task LoadAlunos()
        {
            var alunos = await ApiServiceAluno.GetObjectsAsync(true);
            if (alunos != null)
            {
                var alunosFiltrados = alunos.Where(a => a.IdTurma == idTurma);
                foreach (var aluno in alunosFiltrados)
                {                   
                    Alunos.Add(aluno);                
                }
            }
        }
    }
}
