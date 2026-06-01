using Gestao_Filmes.Domain;

namespace Gestao_Filmes.Data
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private List<Categoria> categorias = new List<Categoria>();
        private int proximoId = 1;

        public void Adicionar(Categoria categoria)
        {
            categoria.Id = proximoId;
            proximoId++;

            categorias.Add(categoria);
        }

        public List<Categoria> Listar()
        {
            return categorias;
        }

        public Categoria ProcurarPorNome(string nome)
        {
            foreach (Categoria categoria in categorias)
            {
                if (categoria.Nome == nome)
                {
                    return categoria;
                }
            }

            return null;
        }

        public void Remover(int id)
        {
            Categoria categoriaRemover = null;

            foreach (Categoria categoria in categorias)
            {
                if (categoria.Id == id)
                {
                    categoriaRemover = categoria;
                }
            }

            if (categoriaRemover != null)
            {
                categorias.Remove(categoriaRemover);
            }
        }
    }
}