using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{

    public interface IProdutoRepository
    {
        Task SaveProdutos(List<Livro> livros);
        IList<Produto> GetProdutos();
        Task<List<Produto>> GetProdutos(string filtro);


    }

    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {

        public readonly ICategoriaRepository CategoriaRepository;
        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {

            CategoriaRepository = categoriaRepository;

        }

        public IList<Produto> GetProdutos()
        {
            return dbSet
                .Include(p => p.Categoria)
                .ToList();
        }
        public async Task<List<Produto>> GetProdutos(string filtro)
        {

            if (string.IsNullOrEmpty(filtro))
            {
                return await dbSet
                .Include(p => p.Categoria)
                .ToListAsync();
            }

            return await dbSet
                .Include(p => p.Categoria)
                .Where(p => Regex.IsMatch(p.Nome.ToUpper(), filtro.ToUpper()) || Regex.IsMatch(p.Categoria.Nome.ToUpper(), filtro.ToUpper()))
                .ToListAsync();

        }


        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {

                    var categoria = CategoriaRepository.AddCategoria(livro.Categoria);

                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));

                }
            }
            await contexto.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
