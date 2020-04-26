using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaDeProdutosViewModel
    {
        public BuscaDeProdutosViewModel(IList<Produto> produtos, string filtro)
        {
            Produtos = produtos;
            Filtro = filtro;
        }

        public IList<Produto> Produtos { get; private set; }

        public string Filtro { get; private set; }


    }
}
