using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Categoria AddCategoria(string categoriaNome);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {

        }
        public Categoria AddCategoria(string categoriaNome)
        {

            var categoria = dbSet.Where(c => c.Nome == categoriaNome).SingleOrDefault();

            if (categoria == null)
            {

                categoria = new Categoria(categoriaNome);
                dbSet.Add(categoria);
                contexto.SaveChanges();
                
            }

            return categoria;
        }
    }
}
