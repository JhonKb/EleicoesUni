using EleicoesUni.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EleicoesUni.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TurmaView : TabbedPage
    {
        public TurmaView(Turma turma)
        {
            InitializeComponent();
            Children.Add(new ChapasView(turma.Id));
            Children.Add(new AlunosView(turma.Id));
            Children.Add(new VotoView(turma.Id));
            TurmaNome.Text = turma.NomeTurma;
        }
    }
}