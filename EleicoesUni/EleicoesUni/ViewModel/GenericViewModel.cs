using EleicoesUni.Model;
using EleicoesUni.Service;
using System.ComponentModel;

namespace EleicoesUni.ViewModel
{
    public class GenericViewModel:INotifyPropertyChanged
    {
        protected ApiService<Turma> ApiServiceTurma = new ApiService<Turma>();
        protected ApiService<Chapa> ApiServiceChapa = new ApiService<Chapa>();
        protected ApiService<Aluno> ApiServiceAluno = new ApiService<Aluno>();
        protected ApiService<Voto> ApiServiceVoto = new ApiService<Voto>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
