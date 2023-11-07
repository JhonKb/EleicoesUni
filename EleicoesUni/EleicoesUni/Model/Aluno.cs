namespace EleicoesUni.Model
{
    public class Aluno
    {
        private int id;
        private string nomeAluno;
        private int matriculaAluno;
        private int idTurma;

        public int Id { get => id; set => id = value; }
        public string NomeAluno { get => nomeAluno; set => nomeAluno = value; }
        public int MatriculaAluno { get => matriculaAluno; set => matriculaAluno = value; }
        public int IdTurma { get => idTurma; set => idTurma = value; }
    }
}
