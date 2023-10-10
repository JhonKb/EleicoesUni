using System;
using System.Collections.Generic;
using System.Text;

namespace EleicoesUni.Model
{
    public class Curso
    {
        private int idCurso;
        private string nomeCurso;

        public int IdCurso { get => idCurso; set => idCurso = value; }
        public string NomeCurso { get => nomeCurso; set => nomeCurso = value; }
    }
}
