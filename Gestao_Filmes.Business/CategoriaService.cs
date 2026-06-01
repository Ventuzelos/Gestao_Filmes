using Gestao_Filmes.Domain;

namespace Gestao_Filmes.Business
{
    public class CategoriaService
    {
        private ICategoriaRepository repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            this.repository = repository;
        }

        public void AdicionarCategoria(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("Nome obrigatório.");
            }

            Categoria categoriaExistente = repository.ProcurarPorNome(nome);

            if (categoriaExistente != null)
            {
                throw new Exception("Categoria já existe.");
            }

            Categoria categoria = new Categoria();
            categoria.Nome = nome;

            repository.Adicionar(categoria);
        }

        public List<Categoria> ListarCategorias()
        {
            return repository.Listar();
        }

        public void RemoverCategoria(int id)
        {
            repository.Remover(id);
        }
    }
}