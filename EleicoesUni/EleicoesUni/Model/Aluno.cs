namespace EleicoesUni.Model
{
    public class Aluno
    {
        private int id;
        private string nomeAluno;
        private string matriculaAluno;
        private int idTurma;

        public int Id { get => id; set => id = value; }
        public string NomeAluno { get => nomeAluno; set => nomeAluno = value; }
        public string MatriculaAluno { get => matriculaAluno; set => matriculaAluno = value; }
        public int IdTurma { get => idTurma; set => idTurma = value; }
    }
}
