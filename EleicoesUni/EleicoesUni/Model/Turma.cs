using System;
using System.Collections.Generic;
using System.Text;

namespace EleicoesUni.Model
{
    public class Turma
    {
        int idTurma;
        string nomeTurma;
        int qntAlunos;

        public int IdTurma { get => idTurma; set => idTurma = value; }
        public string NomeTurma { get => nomeTurma; set => nomeTurma = value; }
        public int QntAlunos { get => qntAlunos; set => qntAlunos = value; }
    }
}
