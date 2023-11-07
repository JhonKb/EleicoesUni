using System;
using System.Collections.Generic;
using System.Text;

namespace EleicoesUni.Services
{
    public class ApiResponse<T>
    {
        private string tipo;
        private List<T> resposta;

        public string Tipo { get => tipo; set => tipo = value; }
        public List<T> Resposta { get => resposta; set => resposta = value; }
    }

    public class ApiResponseUnic<T>
    {
        private string tipo;
        private T resposta;

        public string Tipo { get => tipo; set => tipo = value; }
        public T Resposta { get => resposta; set => resposta = value; }
    }
}
