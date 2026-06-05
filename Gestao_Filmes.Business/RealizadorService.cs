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


            //estava a criar 2 realizadores iguais por isso tive que add uma maneira de tratar  erro.
            Realizador realizadorExistente = repository.ProcurarPorNome(nome);

            if (realizadorExistente != null)
            {
                throw new Exception("Já existe um realizador com esse nome.");
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