using Gestao_Filmes.Domain;


namespace Gestao_Filmes.Data
{
    public class FilmeRepositoryMemoria : IFilmeRepository
    {
        private readonly List<Filme> _filmes = new();
        private int _proximoId = 1;

        public void Adicionar(Filme filme)
        {
            filme.Id = _proximoId++;
            _filmes.Add(filme);
        }

        public List<Filme> Listar()
        {
            return _filmes;
        }

        public Filme? ProcurarPorTitulo(string titulo)
        {
            return _filmes.FirstOrDefault(f =>
                f.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        }

        public void Atualizar(Filme filme)
        {
            Filme? existente = ProcurarPorTitulo(filme.Titulo);

            if (existente != null)
            {
                existente.Classificacao = filme.Classificacao;
                // podemos adicionar para atualizar outros campos se for necessario
            }
        }

        public void Remover(Filme filme)
        {
            _filmes.Remove(filme);
        }
    }
}