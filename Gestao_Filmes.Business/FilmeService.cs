using Gestao_Filmes.Domain;

namespace Gestao_Filmes.Business
{
    public class FilmeService
    {

        ////regras de negocio:
        //private readonly IFilmeRepository _repository;

        //public FilmeService(IFilmeRepository repository)
        //{
        //    _repository = repository;
        //}

        //-------------Adicionado na Parte 3:
        private readonly IFilmeRepository _repository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IRealizadorRepository _realizadorRepository;

        public FilmeService(
            IFilmeRepository repository,
            ICategoriaRepository categoriaRepository,
            IRealizadorRepository realizadorRepository)
        {
            _repository = repository;
            _categoriaRepository = categoriaRepository;
            _realizadorRepository = realizadorRepository;
        }


        public void AdicionarFilme(Filme filme)
        {
            if (filme == null)
            {
                throw new Exception("O filme não pode ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(filme.Titulo))
            {
                throw new Exception("O título é obrigatório.");
            }

            if (filme.Classificacao < 0 || filme.Classificacao > 5)
            {
                throw new Exception("A classificação deve estar entre 0 e 5.");
            }

            if (_repository.ProcurarPorTitulo(filme.Titulo) != null)
            {
                throw new Exception("Já existe um filme com esse título.");
            }
            if (filme.Categoria == null)
            {
                throw new Exception("A categoria é obrigatória.");
            }

            if (filme.Realizador == null)
            {
                throw new Exception("O realizador é obrigatório.");
            }

            if (_categoriaRepository.ProcurarPorNome(filme.Categoria.Nome) == null)
            {
                throw new Exception("A categoria indicada não existe.");
            }

            if (_realizadorRepository.ProcurarPorNome(filme.Realizador.Nome) == null)
            {
                throw new Exception("O realizador indicado não existe.");
            }

            _repository.Adicionar(filme);
        }

        public List<Filme> ListarFilmes()
        {
            return _repository.Listar();
        }

        public Filme? ProcurarFilme(string titulo)
        {
            return _repository.ProcurarPorTitulo(titulo);
        }

        public void AtualizarFilme(Filme filme)
        {
            if (filme == null)
            {
                throw new Exception("O filme não pode ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(filme.Titulo))
            {
                throw new Exception("O título é obrigatório.");
            }

            if (filme.Classificacao < 0 || filme.Classificacao > 5)
            {
                throw new Exception("A classificação deve estar entre 0 e 5.");
            }

            _repository.Atualizar(filme);
        }

        public void RemoverFilme(string titulo)
        {
            var filme = _repository.ProcurarPorTitulo(titulo);

            if (filme == null)
            {
                throw new Exception("Filme não encontrado.");
            }

            _repository.Remover(filme);
        }
    }
}
