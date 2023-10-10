using System;
using System.Collections.Generic;
using System.Text;

namespace EleicoesUni.Model
{
    public class Turma
    {
        private int idTurma;
        private string nomeTurma;
        private int idCurso;
        private int qntAlunos;

        public int IdTurma { get => idTurma; set => idTurma = value; }
        public string NomeTurma { get => nomeTurma; set => nomeTurma = value; }
        public int IdCurso { get => idCurso; set => idCurso = value; }
        public int QntAlunos { get => qntAlunos; set => qntAlunos = value; }
    }
}
