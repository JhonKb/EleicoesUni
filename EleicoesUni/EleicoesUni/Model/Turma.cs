namespace EleicoesUni.Model
{
    public class Turma
    {
        private int id;
        private string nomeTurma;
        private string cursoTurma;

        public int Id { get => id; set => id = value; }
        public string NomeTurma { get => nomeTurma; set => nomeTurma = value; }
        public string CursoTurma { get => cursoTurma; set => cursoTurma = value; }
    }
}
