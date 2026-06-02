using Gestao_Filmes.Domain;

namespace Gestao_Filmes.Business
{
    public class RealizadorService
    {
        private IRealizadorRepository repository;

        public RealizadorService(IRealizadorRepository repository)
        {
            this.repository = repository;
        }

        public void AdicionarRealizador(string nome, string pais)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("Nome obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(pais))
            {
                throw new Exception("País obrigatório.");
            }

            Realizador realizador = new Realizador();
            realizador.Nome = nome;
            realizador.Pais = pais;

            repository.Adicionar(realizador);
        }

        public List<Realizador> ListarRealizadores()
        {
            return repository.Listar();
        }

        public Realizador ProcurarRealizador(string nome)
        {
            return repository.ProcurarPorNome(nome);
        }

        public void RemoverRealizador(int id)
        {
            repository.Remover(id);
        }
    }
}