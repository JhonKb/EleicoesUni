using EleicoesUni.Model;
using EleicoesUni.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EleicoesUni
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            nome.Items.Add("Turma ADS 1/2");
            nome.Items.Add("Turma ADS 3/4");
            nome.Items.Add("Turma Fisio 1");
            nome.Items.Add("Turma Fisio 2");
            nome.Items.Add("Turma Direito 5");
        }

        public void Button_Avancar(object sender, EventArgs e)
        {
            Turma turma = new Turma();
            turma.NomeTurma = nome.SelectedItem.ToString();
            _= Navigation.PushModalAsync(new ViewTurma(turma));
        }
        public void Button_Cadasto(object sender, EventArgs e)
        {
            _= Navigation.PushModalAsync(new ViewCadastro());
        }
    }
}
