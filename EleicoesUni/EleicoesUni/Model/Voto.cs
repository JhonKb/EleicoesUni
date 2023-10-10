using System;
using System.Collections.Generic;
using System.Text;

namespace EleicoesUni.Model
{
    public class Voto
    {
        private int idVoto;
        private int idCandidatura;
        private int registroAluno;
        private string nomeAluno; 

        public int IdVoto { get => idVoto; set => idVoto = value; }
        public int IdCandidatura { get => idCandidatura; set => idCandidatura = value; }
        public int RegistroAluno { get => registroAluno; set => registroAluno = value; }
        public string NomeAluno { get => nomeAluno; set => nomeAluno = value; }
    }
}
