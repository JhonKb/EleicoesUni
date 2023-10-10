using System;
using System.Collections.Generic;
using System.Text;

namespace EleicoesUni.Model
{
    public class Candidaturas
    {
        private int idCandidatura;
        private int idTurma;
        private string nomeLider;
        private string nomeViceLider; 

        public int IdCandidatura { get => idCandidatura; set => idCandidatura = value; }
        public int IdTurma { get => idTurma; set => idTurma = value; }
        public string NomeLider { get => nomeLider; set => nomeLider = value; }
        public string NomeViceLider { get => nomeViceLider; set => nomeViceLider = value; }
    }
}
