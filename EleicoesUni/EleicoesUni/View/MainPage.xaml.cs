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
            turma.Items.Add("Turma ADS 1/2");
            turma.Items.Add("Turma ADS 3/4");
            turma.Items.Add("Turma Fisio 1");
            turma.Items.Add("Turma Fisio 2");
            turma.Items.Add("Turma Direito 5");
        }
    }
}
