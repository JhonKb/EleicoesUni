namespace EleicoesUni.Model
{
    public class Voto
    {
        private int id;
        private int idVotante;
        private int idChapa;

        public int Id { get => id; set => id = value; }
        public int IdVotante { get => idVotante; set => idVotante = value; }
        public int IdChapa { get => idChapa; set => idChapa = value; }
    }
}
