using EleicoesUni.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EleicoesUni.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewTurma : ContentPage
    {
        public ViewTurma(Turma turma)
        {
            InitializeComponent();
            nomeTurma.Text = turma.NomeTurma.ToString();
        }
    }
}