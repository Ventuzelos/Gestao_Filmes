using Gestao_Filmes.Domain;

namespace Gestao_Filmes.Data
{
    public class RealizadorRepository : IRealizadorRepository
    {
        private List<Realizador> realizadores = new List<Realizador>();
        private int proximoId = 1;

        public void Adicionar(Realizador realizador)
        {
            realizador.Id = proximoId;
            proximoId++;

            realizadores.Add(realizador);
        }

        public List<Realizador> Listar()
        {
            return realizadores;
        }

        public Realizador ProcurarPorNome(string nome)
        {
            foreach (Realizador realizador in realizadores)
            {
                if (realizador.Nome == nome)
                {
                    return realizador;
                }
            }

            return null;
        }

        public void Remover(int id)
        {
            Realizador realizadorRemover = null;

            foreach (Realizador realizador in realizadores)
            {
                if (realizador.Id == id)
                {
                    realizadorRemover = realizador;
                }
            }

            if (realizadorRemover != null)
            {
                realizadores.Remove(realizadorRemover);
            }
        }
    }
}