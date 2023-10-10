using System;
using System.Collections.Generic;
using System.Text;

namespace EleicoesUni.Utils
{
    public class Carregamento
    {
        bool _carregamento;

        public void telaCarregamento(bool visibilityView, bool visibilityCircle, bool runnigCircle)
        {
            if (_carregamento)
            {
                visibilityView = false;
                visibilityCircle = false;
                runnigCircle = false;

                _carregamento = false;
            }
            else
            {
                visibilityView = true;
                visibilityCircle = true;
                runnigCircle = true;

                _carregamento = true;
            }

        }
    }
}
